using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Randomization.Legality;
using CtrDotNet.Pokemon.Randomization.Utility;
using CtrDotNet.Pokemon.Structures.RomFS.Gen6.ORAS;

namespace CtrDotNet.Pokemon.Randomization.Gen6.ORAS
{
	public partial class OrasRandomizer
	{
		public override async Task RandomizeEncounters()
		{
			const int MaxUniqueSpecies = 18;

			var config = this.RandomizerConfig.Encounters;
			var species = Species.AllSpecies.ToList();
			var speciesInfo = await this.Game.GetPokemonInfo();
			var encounters = ( await this.Game.GetEncounterData() ).Cast<OrasEncounterWild>().ToList();
			var types = await this.Game.GetTypes();

			if ( !config.AllowLegendaries )
				species = species.Except( Legendaries.AllLegendaries )
								 .ToList();

			foreach ( var encounter in encounters )
			{
				var entries = encounter.GetAllEntries().Where( e => e != null ).ToList();

				if ( entries.Count == 0 )
					continue;

				var speciesChoose = species.ToList();

				if ( config.TypeThemedAreas )
				{
					var areaType = types.GetRandom( this.rand );
					speciesChoose = speciesChoose.Where( s => speciesInfo[ s.Id ].HasType( areaType ) )
												 .ToList();
				}

				// DexNav can only handle up to 18 unique species per encounter area,
				// so we pick 18 random and unique species from our current choice list
				var uniqueList = new List<SpeciesType>();
				var entryArrays = encounter.EntryArrays;
				int[] orderedIndices = entryArrays.OrderByDescending( ea => ea.Length )
												  .Select( ea => Array.IndexOf( entryArrays, ea ) )
												  .ToArray();

				while ( uniqueList.Count < MaxUniqueSpecies )
					// Find a new unique species from our current determined list of choices
					uniqueList.Add( this.GetRandomSpecies( speciesChoose.Except( uniqueList ) ) );

				foreach ( var entry in entryArrays.SelectMany( entryArray => entryArray.Where( entry => entry.Species > 0 ) ) )
				{
					entry.Species = (ushort) this.GetRandomSpecies( uniqueList ).Id;

					if ( config.LevelMultiplier != 1.0m )
					{
						entry.MinLevel = (byte) MathUtil.Clamp( (int) ( config.LevelMultiplier * entry.MinLevel ), 2, 100 );
						entry.MaxLevel = (byte) MathUtil.Clamp( (int) ( config.LevelMultiplier * entry.MaxLevel ), 2, 100 );
					}
				}

				int GetUniqueAllSections() => entryArrays.Sum( ea => ea.Distinct().Count( e => e.Species != 0 ) );

				// DexNav crashes if there are more than 18 unique species in an encounter zone.
				// If the same species appears in two different sub-zones, it counts as two
				// unique species.
				while ( GetUniqueAllSections() > MaxUniqueSpecies )
				{
					// Starting with the largest array, reduce the number of unique species taken up by that array by one
					foreach ( int i in orderedIndices )
					{
						var entryArray = entryArrays[ i ];
						int uniqueThisSection = entryArray.Distinct().Count( e => e.Species != 0 );

						// If there are less than two unique species in this array, we don't have enough
						// species to reduce, so skip it.
						if ( uniqueThisSection < 2 )
							continue;

						// Pick a random slot to overwrite
						int randomTo = this.rand.Next( entryArray.Length );
						// Get a species that's different than the slot we're overwriting so we make a duplicate
						var donorEntry = entryArray.First( e => e.Species != entryArray[ randomTo ].Species );

						entryArray[ randomTo ].Species = donorEntry.Species;
						entryArrays[ i ] = entryArray;

						// Don't bother with the rest of the arrays if we fixed the problem
						if ( GetUniqueAllSections() == MaxUniqueSpecies )
							break;
					}
				}

				encounter.EntryArrays = entryArrays;
			}

			await this.Game.SaveEncounterData( encounters );
		}
	}
}