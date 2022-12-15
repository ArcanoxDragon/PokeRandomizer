using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CtrDotNet.Utility.Extensions;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Reference;
using PokeRandomizer.Common.Structures.RomFS.Gen6;
using PokeRandomizer.Progress;
using PokeRandomizer.Utility;
using EvolutionSet = PokeRandomizer.Common.Structures.RomFS.Common.EvolutionSet;

namespace PokeRandomizer.Gen6
{
	public partial class Gen6Randomizer
	{
		private List<EvolutionSet> evolutionData;

		protected async Task<ushort> GetEvolutionOfAsync(Random taskRandom, ushort speciesId, int evoIndex)
		{
			this.evolutionData ??= ( await Game.GetEvolutions() ).ToList();

			if (evoIndex == 0)
				return speciesId;

			var evoSet = this.evolutionData[speciesId];
			var possible = evoSet.PossibleEvolutions.Where(e => e.Species > 0).ToArray();

			if (possible.Length == 0)
				return speciesId;

			var evo = possible.GetRandom(taskRandom);

			return await GetEvolutionOfAsync(taskRandom, (ushort) evo.Species, evoIndex - 1);
		}

		public override async Task RandomizeTrainers(Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token)
		{
			var config = ValidateAndGetConfig().Trainers;

			if (!config.RandomizeTrainers)
				return;

			await LogAsync($"======== Beginning Trainer randomization ========{Environment.NewLine}");
			progressNotifier?.NotifyUpdate(ProgressUpdate.StatusOnly("Randomizing trainer teams..."));

			var trainers = ( await Game.GetTrainerData() ).ToList();
			var trainerNames = await Game.GetTextFile(TextNames.TrainerNames, languageOverride: Language.English);
			var localizedTrainerNames = await Game.GetTextFile(TextNames.TrainerNames);
			var speciesNames = await Game.GetTextFile(TextNames.SpeciesNames);
			var itemNames = await Game.GetTextFile(TextNames.ItemNames);
			var moveNames = await Game.GetTextFile(TextNames.MoveNames);
			var availableSpecies = Species.ValidSpecies.ToList();
			var availableTypes = PokemonTypes.AllPokemonTypes.ToList();

			// Get already-edited versions of the info so that we get the shuffled version
			var starters = await Game.GetStarters(edited: true);
			var pokeInfo = await Game.GetPokemonInfo(edited: true);
			var learnsets = ( await Game.GetLearnsets(edited: true) ).ToList();

			// Decide a type for each gym for use later on if the user has chosen to type-theme entire gyms
			var gymTypes = Enumerable.Range(0, 8).Select(_ => availableTypes.GetRandom(taskRandom)).ToArray();
			var friendStarterGender = new[] { -1, -1, -1 };
			var friendStarterAbility = new[] { -1, -1, -1 };

			if (config.TypeThemed)
			{
				foreach (var (gymIndex, gymType) in gymTypes.Pairs())
				{
					await LogAsync($"Gym {gymIndex + 1}: {gymType.Name}-type");
				}

				await LogAsync();
			}

			foreach (var (i, trainer) in trainers.Pairs())
			{
				var name = i < trainerNames.LineCount ? trainerNames[i] : "UNKNOWN";
				var localizedName = i < localizedTrainerNames.LineCount ? localizedTrainerNames[i] : "UNKNOWN";

				await LogAsync($"{name}: ");
				progressNotifier?.NotifyUpdate(ProgressUpdate.Update($"Randomizing trainer teams...\n{localizedName}", i / (double) trainers.Count));

				var isFriend = IsTrainerFriend(name);
				var gymId = GetGymId(name, i);
				var chooseFrom = availableSpecies;

				if (config.TypeThemed)
				{
					PokemonType type;

					if (config.TypeThemedGyms && gymId >= 0)
						type = gymTypes[gymId];
					else
						type = availableTypes.GetRandom(taskRandom);

					chooseFrom = chooseFrom.Where(s => pokeInfo[s.Id].HasType(type)).ToList();
				}

				if (!await HandleTrainerSpecificLogicAsync(taskRandom, name, trainer))
				{
					// If there's no item specified for the trainer, randomize the move list instead for more variety
					// Otherwise, the game will pick moves at random
					if (!trainer.Item)
						trainer.Moves = true;

					foreach (var pokemon in trainer.Team)
					{
						if (config.FriendKeepsStarter && isFriend && IsStarter(pokemon.Species))
						{
							// Figure out which battle scenario we're in (one per starter per story-battle with friend)
							// Then, get the starter from the slot this scenario represents and figure out
							// if it needs to be evolved at all
							var (starterSlot, starterEvo) = GetStarterIndexAndEvolution(pokemon.Species);
							var newStarterBaseSpecies = starters.StarterSpecies[MainStarterGen - 1][starterSlot];
							var newStarter = await GetEvolutionOfAsync(taskRandom, newStarterBaseSpecies, starterEvo);
							var info = pokeInfo[newStarter];

							pokemon.Species = newStarter;

							if (friendStarterGender[starterSlot] < 0)
								friendStarterGender[starterSlot] = info.GetRandomGender();

							if (friendStarterAbility[starterSlot] < 0)
								friendStarterAbility[starterSlot] = info.Abilities.GetRandom(taskRandom);

							pokemon.Gender = friendStarterGender[starterSlot];
							pokemon.Ability = friendStarterAbility[starterSlot];
						}
						else
						{
							// Just pick a random species, making sure it conforms to any specified criteria
							pokemon.Species = (ushort) GetRandomSpecies(taskRandom, chooseFrom).Id;

							var info = pokeInfo[pokemon.Species];

							pokemon.Gender = info.GetRandomGender();
							pokemon.Ability = info.Abilities.GetRandom(taskRandom);
						}

						pokemon.Level = (ushort) MathUtil.Clamp((int) ( pokemon.Level * config.LevelMultiplier ), 2, 100);

						// Fill the Pokemon's available moves with random choices from its learnset (up to its current level)
						var moveset = learnsets[pokemon.Species].GetPossibleMoves(pokemon.Level).ToList();

						if (trainer.Moves)
						{
							pokemon.HasItem = false;
							pokemon.HasMoves = true;
							pokemon.Moves = new ushort[] { 0, 0, 0, 0 };

							for (var m = 0; m < Math.Min(moveset.Count, 4); m++)
								pokemon.Moves[m] = moveset.Except(pokemon.Moves).ToList().GetRandom(taskRandom);
						}
					}
				}

				foreach (var (pkIndex, pokemon) in trainer.Team.Pairs())
				{
					var speciesName = speciesNames[pokemon.Species];
					var genderChar = pokemon.Gender switch {
						0 => "♂", // Male
						1 => "♀", // Female
						_ => "",  // Genderless
					};
					var ability = Abilities.AllAbilities.Single(a => a.Id == pokemon.Ability);
					var abilityName = ability.Name;

					if (pokemon.HasItem)
					{
						var itemName = itemNames[pokemon.Item];

						await LogAsync($"  Pokémon {pkIndex + 1}: Lv. {pokemon.Level} {speciesName}{genderChar}, {abilityName}, holding {itemName}");
					}
					else
					{
						await LogAsync($"  Pokémon {pkIndex + 1}: Lv. {pokemon.Level} {speciesName}{genderChar}, {abilityName}: ");

						foreach (var move in pokemon.Moves)
						{
							await LogAsync($"    - {moveNames[move]}");
						}

						if (pkIndex != trainer.Team.Length - 1)
							await LogAsync();
					}
				}

				await LogAsync();
			}

			await Game.SaveTrainerData(trainers);
			await LogAsync($"======== Finished Trainer randomization ========{Environment.NewLine}");
		}

		#region Starter Methods

		public abstract int MainStarterGen { get; }
		public abstract (int Slot, int Evolution) GetStarterIndexAndEvolution(int speciesId);
		public bool IsStarter(int speciesId) => GetStarterIndexAndEvolution(speciesId).Slot >= 0;

		#endregion

		#region Trainer Methods

		public abstract bool IsTrainerFriend(string trainerName);
		public abstract int GetGymId(string trainerName, int trainerId);
		public abstract Task<bool> HandleTrainerSpecificLogicAsync(Random taskRandom, string trainerName, TrainerData trainer);

		#endregion
	}
}