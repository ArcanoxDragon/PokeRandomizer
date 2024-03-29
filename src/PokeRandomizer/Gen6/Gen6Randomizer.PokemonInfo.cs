﻿using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Reference;
using PokeRandomizer.Progress;
using PokeRandomizer.Utility;

namespace PokeRandomizer.Gen6
{
	public partial class Gen6Randomizer
	{
		private const int MinCatchRate = 0x19; // That of rare Pokémon such as Snorlax

		public override async Task RandomizePokemonInfo(Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token)
		{
			var config = ValidateAndGetConfig().PokemonInfo;

			if (!config.RandomizeTypes && !config.RandomizeAbilities)
				return;

			await LogAsync($"======== Beginning Pokémon Info randomization ========{Environment.NewLine}");
			progressNotifier?.NotifyUpdate(ProgressUpdate.StatusOnly("Randomizing Pokémon personal information..."));

			var pokeInfoTable = await Game.GetPokemonInfo();
			var avilableAbilities = Abilities.AllAbilities.ToList();
			var availableTypes = PokemonTypes.AllPokemonTypes.ToList();
			var pokeNames = ( await Game.GetTextFile(TextNames.SpeciesNames) ).Lines;
			var typeNames = ( await Game.GetTextFile(TextNames.Types) ).Lines;

			if (!config.AllowWonderGuard)
				avilableAbilities = avilableAbilities.Where(a => a.Id != Abilities.WonderGuard.Id).ToList();

			// Randomize abilities for each entry in the Info table (which includes all forms)
			for (var i = 0; i < pokeInfoTable.Table.Length; i++)
			{
				var name = pokeNames[pokeInfoTable.GetSpeciesForEntry(i)];
				var thisStatus = $"Randomizing Pokémon personal information...\n{name}";
				var thisProgress = i / (double) pokeInfoTable.Table.Length;

				await LogAsync($"{name}: ");
				progressNotifier?.NotifyUpdate(ProgressUpdate.Update(thisStatus, thisProgress));

				var pokeInfo = pokeInfoTable[i];

				if (config.RandomizeAbilities)
				{
					progressNotifier?.NotifyUpdate(ProgressUpdate.Update($"{thisStatus}\nRandomizing abilities...", thisProgress));

					var abilities = pokeInfo.Abilities;
					var abilityNames = new string[abilities.Length];

					for (var a = 0; a < abilities.Length; a++)
					{
						var ability = avilableAbilities.GetRandom(taskRandom);

						abilities[a] = (byte) ability.Id;
						abilityNames[a] = ability.Name;
					}

					pokeInfo.Abilities = abilities;
					await LogAsync($"  - Available abilities: {string.Join(", ", abilityNames)}");
				}

				if (config.RandomizeTypes)
				{
					progressNotifier?.NotifyUpdate(ProgressUpdate.Update($"{thisStatus}\nRandomizing types...", thisProgress));

					var types = pokeInfo.Types;

					if (types.Length > 0) // Just to be safe
					{
						if (config.RandomizePrimaryTypes)
						{
							types[0] = (byte) availableTypes.GetRandom(taskRandom).Id;
						}

						if (config.RandomizeSecondaryTypes && types.Length > 1)
						{
							types[1] = (byte) availableTypes.Where(type => type.Id != types[0])
															.ToList()
															.GetRandom(taskRandom)
															.Id;
						}

						if (config.RandomizePrimaryTypes || config.RandomizeSecondaryTypes)
						{
							await LogAsync($"  - Types: {string.Join("/", types.Select(t => typeNames[t]))}");
						}
					}

					pokeInfo.Types = types;
				}

				if (config.EnsureMinimumCatchRate && i > 0 && pokeInfo.CatchRate < MinCatchRate)
				{
					await LogAsync($"  - Increased catch rate from {pokeInfo.CatchRate:000} to {MinCatchRate:000}");
					pokeInfo.CatchRate = MinCatchRate;
				}

				await LogAsync();
				pokeInfoTable[i] = pokeInfo;
			}

			await Game.SavePokemonInfo(pokeInfoTable);
			await LogAsync($"======== Finished Pokémon Info randomization ========{Environment.NewLine}");
		}
	}
}