using System;
using System.Linq;
using System.Threading.Tasks;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Structures.RomFS.Gen6;
using PokeRandomizer.Common.Utility;
using PokeRandomizer.Utility;

namespace PokeRandomizer.Gen6.XY
{
	public partial class XyRandomizer
	{
		#region Starters

		public override int MainStarterGen => 6;

		public override (int Slot, int Evolution) GetStarterIndexAndEvolution(int speciesId)
		{
			if (speciesId.InRange(Species.Chespin.Id, Species.Chesnaught.Id))
				return ( 0, speciesId - Species.Chespin.Id );
			if (speciesId.InRange(Species.Fennekin.Id, Species.Delphox.Id))
				return ( 1, speciesId - Species.Fennekin.Id );
			if (speciesId.InRange(Species.Froakie.Id, Species.Greninja.Id))
				return ( 2, speciesId - Species.Froakie.Id );
			if (speciesId.InRange(Species.Bulbasaur.Id, Species.Venusaur.Id))
				return ( 0, speciesId - Species.Bulbasaur.Id );
			if (speciesId.InRange(Species.Charmander.Id, Species.Charizard.Id))
				return ( 1, speciesId - Species.Charmander.Id );
			if (speciesId.InRange(Species.Squirtle.Id, Species.Blastoise.Id))
				return ( 2, speciesId - Species.Squirtle.Id );

			return ( -1, -1 );
		}

		#endregion

		#region Trainers

		private static readonly string[] FriendNames = { "Shauna", "Serena", "Calem", };

		public override bool IsTrainerFriend(string trainerName)
			=> FriendNames.Contains(trainerName, StringComparer.OrdinalIgnoreCase);

		public override int GetGymId(string trainerName, int trainerId) => trainerName switch {
			// Santalune
			"David"     => 0, // Youngster
			"Zachary"   => 0, // Youngster
			"Charlotte" => 0, // Lass
			"Viola"     => 0, // Leader

			// Cyllage
			"Didier"  => 1, // Rising Star
			"Manon"   => 1, // Rising Star
			"Craig"   => 1, // Hiker
			"Bernard" => 1, // Hiker
			"Grant"   => 1, // Leader

			// Shalour
			"Shun"    => 2, // Roller Skater
			"Dash"    => 2, // Roller Skater
			"Rolanda" => 2, // Roller Skater
			"Kate"    => 2, // Roller Skater
			"Korrina" => 2, // Leader

			// Coumarine
			"Chaise"  => 3, // Pokémon Ranger
			"Brooke"  => 3, // Pokémon Ranger
			"Maurice" => 3, // Pokémon Ranger
			"Twiggy"  => 3, // Pokémon Ranger
			"Ramos"   => 3, // Leader

			// Lumiose
			"Arno"     => 4, // 2F Schoolboy
			"Sherlock" => 4, // 2F Schoolboy
			"Finnian"  => 4, // 2F Schoolboy
			"Estel"    => 4, // 3F Rising Star
			"Nelly"    => 4, // 3F Rising Star
			"Helene"   => 4, // 3F Rising Star
			"Mathis"   => 4, // 4F Ace Trainer
			"Maxim"    => 4, // 4F Ace Trainer
			"Rico"     => 4, // 4F Ace Trainer
			"Abigail"  => 4, // 5F Poké Fan
			"Lydie"    => 4, // 5F Poké Fan
			"Tara"     => 4, // 5F Poké Fan
			"Clemont"  => 4, // 6F Leader

			// Laverre
			"Kali"      => 5, // Furisode Girl
			"Linnea"    => 5, // Furisode Girl
			"Blossom"   => 5, // Furisode Girl
			"Katherine" => 5, // Furisode Girl
			"Valerie"   => 5, // Leader

			// Anistar
			"Paschal" => 6, // Psychic
			"Harry"   => 6, // Psychic
			"Arthur"  => 6, // Psychic
			"Arachna" => 6, // Hex Maniac
			"Melanie" => 6, // Hex Maniac
			"Olympia" => 6, // Leader

			// Showbelle
			"Imelda"  => 7, // Ace Trainer
			"Viktor"  => 7, // Ace Trainer
			"Shannon" => 7, // Ace Trainer
			"Theo"    => 7, // Ace Trainer
			"Wulfric" => 7, // Leader

			_ => -1
		};

		public override async Task<bool> HandleTrainerSpecificLogicAsync(Random taskRandom, string trainerName, TrainerData trainer)
		{
			var config = ValidateAndGetConfig().Trainers;

			// Get already-edited versions of the info so that we get the shuffled version
			var starters = await Game.GetStarters(edited: true);
			var pokeInfo = await Game.GetPokemonInfo(edited: true);
			var learnsets = ( await Game.GetLearnsets(edited: true) ).ToList();

			switch (trainerName)
			{
				case "Sycamore":
					{
						if (!trainer.Item)
							trainer.Moves = true;

						foreach (var pokemon in trainer.Team)
						{
							// Professor Sycamore has all three Kanto starters as his battle team.
							// We synchronize his battle team with the randomized selection of Kanto
							// starters to match what the unrandomized game does.

							var (starterSlot, starterEvo) = GetStarterIndexAndEvolution(pokemon.Species);
							var newStarterBaseSpecies = starters.StarterSpecies[0 /* First Gen */][starterSlot];
							var newStarter = await GetEvolutionOfAsync(taskRandom, newStarterBaseSpecies, starterEvo);
							var info = pokeInfo[newStarter];

							pokemon.Species = newStarter;
							pokemon.Gender = info.GetRandomGender();
							pokemon.Ability = info.Abilities.GetRandom(taskRandom);
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

						return true;
					}
				default: return false;
			}
		}

		#endregion
	}
}