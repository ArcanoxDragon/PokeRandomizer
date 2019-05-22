using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Randomization.Legality;
using CtrDotNet.Pokemon.Randomization.Progress;
using CtrDotNet.Pokemon.Randomization.Utility;
using CtrDotNet.Pokemon.Reference;
using CtrDotNet.Pokemon.Structures.RomFS.Gen6.XY;

namespace CtrDotNet.Pokemon.Randomization.Gen6.XY
{
	public partial class XyRandomizer
	{
		public override async Task RandomizeEncounters( ProgressNotifier progressNotifier, CancellationToken token )
		{
			const int MaxUniqueSpecies = 18;

			progressNotifier?.NotifyUpdate( ProgressUpdate.StatusOnly( "Randomizing wild Pokémon encounters..." ) );

			var config        = this.ValidateAndGetConfig().Encounters;
			var species       = Species.ValidSpecies.ToList();
			var speciesInfo   = await this.Game.GetPokemonInfo( edited: true );
			var encounterData = await this.Game.GetEncounterData();
			var encounters    = encounterData.Cast<XyEncounterWild>().ToList();
			var zoneNames     = ( await this.Game.GetTextFile( TextNames.EncounterZoneNames ) ).Lines;

			if ( !config.AllowLegendaries )
				species = species.Except( Legendaries.AllLegendaries )
								 .ToList();

			var cur = -1;
			foreach ( var encounter in encounters )
			{
				var name = zoneNames[ encounter.ZoneId ];
				progressNotifier?.NotifyUpdate( ProgressUpdate.Update( $"Randomizing encounters...\n{name}", ++cur / (double) encounters.Count ) );

				var entries = encounter.GetAllEntries().Where( e => e != null ).ToList();

				if ( entries.Count == 0 )
					continue;

				var speciesChoose = species.ToList();

				if ( config.TypeThemedAreas )
				{
					var areaType = PokemonTypes.AllPokemonTypes.ToArray().GetRandom( this.rand );
					speciesChoose = speciesChoose.Where( s => speciesInfo[ s.Id ].HasType( areaType ) )
												 .ToList();
				}

				// DexNav can only handle up to 18 unique species per encounter area,
				// so we pick 18 random and unique species from our current choice list
				var uniqueList  = new List<SpeciesType>();
				var entryArrays = encounter.EntryArrays;
				int[] orderedIndices = entryArrays.OrderByDescending( ea => ea.Length )
												  .Select( ea => Array.IndexOf( entryArrays, ea ) )
												  .ToArray();

				while ( uniqueList.Count < MaxUniqueSpecies )
					// Find a new unique species from our current determined list of choices
					uniqueList.Add( this.GetRandomSpecies( speciesChoose.Except( uniqueList ) ) );

				foreach ( var entryArray in entryArrays )
				{
					foreach ( var entry in entryArray.Where( entry => entry.Species > 0 ) )
					{
						entry.Species = (ushort) this.GetRandomSpecies( uniqueList ).Id;

						if ( config.LevelMultiplier != 1.0m )
						{
							entry.MinLevel = (byte) MathUtil.Clamp( (int) ( config.LevelMultiplier * entry.MinLevel ), 2, 100 );
							entry.MaxLevel = (byte) MathUtil.Clamp( (int) ( config.LevelMultiplier * entry.MaxLevel ), 2, 100 );
						}
					}

					if ( config.TypeThemedAreas && config.TypePerSubArea ) // Re-generate type for the new sub-area
					{
						var areaType = PokemonTypes.AllPokemonTypes.ToArray().GetRandom( this.rand );
						speciesChoose = speciesChoose.Where( s => speciesInfo[ s.Id ].HasType( areaType ) )
													 .ToList();
					}
				}

				/*int GetUniqueAllSections() => entryArrays.Sum( ea => ea.Select( e => e.Species ).Distinct().Count( sp => sp > 0 ) );

				// DexNav crashes if there are more than 18 unique species in an encounter zone.
				// If the same species appears in two different sub-zones, it counts as two
				// unique species.
				while ( GetUniqueAllSections() > MaxUniqueSpecies )
				{
					// Starting with the largest array, reduce the number of unique species taken up by that array by one
					foreach ( int i in orderedIndices )
					{
						var entryArray        = entryArrays[ i ];
						int uniqueThisSection = entryArray.Select( e => e.Species ).Distinct().Count( sp => sp > 0 );

						// If there are less than two unique species in this array, we don't have enough
						// species to reduce, so skip it.
						if ( uniqueThisSection < 2 )
							continue;

						// Pick a random slot to overwrite
						int randomTo = -1;
						while ( randomTo < 0 || entryArray[ randomTo ].Species == 0 )
							randomTo = this.rand.Next( entryArray.Length );

						// Get a species that's different than the slot we're overwriting so we make a duplicate
						var donorEntry = entryArray.First( e => e.Species > 0 && e.Species != entryArray[ randomTo ].Species );

						entryArray[ randomTo ].Species = donorEntry.Species;
						entryArrays[ i ]               = entryArray;

						// Don't bother with the rest of the arrays if we fixed the problem
						if ( GetUniqueAllSections() == MaxUniqueSpecies )
							break;
					}
				}*/

				encounter.EntryArrays = entryArrays;
			}

			progressNotifier?.NotifyUpdate( ProgressUpdate.Update( $"Randomizing encounters...\nCompressing encounter data...this may take a while", 1.0 ) );

			await this.Game.SaveEncounterData( encounters );
		}
	}
}