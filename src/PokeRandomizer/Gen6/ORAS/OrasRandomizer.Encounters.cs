using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CtrDotNet.Utility.Extensions;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Reference;
using PokeRandomizer.Common.Structures.RomFS.Gen6.ORAS;
using PokeRandomizer.Legality;
using PokeRandomizer.Progress;
using PokeRandomizer.Utility;
using Species = PokeRandomizer.Common.Data.Partial.Species;

namespace PokeRandomizer.Gen6.ORAS
{
	public partial class OrasRandomizer
	{
		public override async Task RandomizeEncounters( Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token )
		{
			const int MaxUniqueSpecies = 18;

			var config = this.ValidateAndGetConfig().Encounters;

			if ( !config.RandomizeEncounters )
				return;

			await this.LogAsync( $"======== Beginning Encounter randomization ========{Environment.NewLine}" );
			progressNotifier?.NotifyUpdate( ProgressUpdate.StatusOnly( "Randomizing wild Pokémon encounters..." ) );

			var species       = Species.ValidSpecies.ToList();
			var speciesInfo   = await this.Game.GetPokemonInfo( edited: true );
			var encounterData = await this.Game.GetEncounterData();
			var encounters    = encounterData.Cast<OrasEncounterWild>().ToList();
			var zoneNames     = ( await this.Game.GetTextFile( TextNames.EncounterZoneNames ) ).Lines;
			var speciesNames  = ( await this.Game.GetTextFile( TextNames.SpeciesNames ) ).Lines;

			if ( !config.AllowLegendaries )
				species = species.Except( Legendaries.AllLegendaries )
								 .ToList();

			var cur = -1;
			foreach ( var encounter in encounters )
			{
				var name    = zoneNames[ encounter.ZoneId ];
				var entries = encounter.GetAllEntries().Where( e => e != null ).ToList();

				if ( entries.Count == 0 )
					continue;

				progressNotifier?.NotifyUpdate( ProgressUpdate.Update( $"Randomizing encounters...\n{name}", ++cur / (double) encounters.Count ) );

				PokemonType       areaType = default;
				List<SpeciesType> speciesChoose;

				void UpdateChoiceList( IList<SpeciesType> choices )
				{
					var uniqueList = new List<SpeciesType>();

					// DexNav can only handle up to 18 unique species per encounter area,
					// so we pick 18 random and unique species from our current choice list
					while ( uniqueList.Count < MaxUniqueSpecies )
						// Find a new unique species from our current determined list of choices
						uniqueList.Add( this.GetRandomSpecies( taskRandom, choices.Except( uniqueList ) ) );

					speciesChoose = uniqueList.ToList();
				}

				if ( config.TypeThemedAreas )
				{
					areaType = PokemonTypes.AllPokemonTypes.ToArray().GetRandom( taskRandom );

					UpdateChoiceList( species.Where( s => speciesInfo[ s.Id ].HasType( areaType ) ).ToList() );
				}
				else
				{
					UpdateChoiceList( species );
				}

				if ( areaType == default || config.TypePerSubArea )
					await this.LogAsync( $"Randomizing zone: {name}" );
				else
					await this.LogAsync( $"Randomizing zone: {name} ({areaType.Name}-type)" );

				var entryArrays = encounter.EntryArrays;
				int[] orderedIndices = entryArrays.OrderByDescending( ea => ea.Length )
												  .Select( ea => Array.IndexOf( entryArrays, ea ) )
												  .ToArray();

				foreach ( var (i, entryArray) in entryArrays.Pairs() )
				{
					if ( entryArray.All( e => e.Species == Common.Data.Species.Egg.Id ) )
						continue;

					if ( areaType != default && config.TypePerSubArea )
						await this.LogAsync( $"  Sub-zone {i + 1} ({areaType.Name}-type):" );
					else
						await this.LogAsync( $"  Sub-zone {i + 1}:" );

					foreach ( var entry in entryArray.Where( entry => entry.Species != Common.Data.Species.Egg.Id ) )
					{
						entry.Species = (ushort) this.GetRandomSpecies( taskRandom, speciesChoose ).Id;

						if ( config.LevelMultiplier != 1.0m )
						{
							entry.MinLevel = (byte) MathUtil.Clamp( (int) ( config.LevelMultiplier * entry.MinLevel ), 2, 100 );
							entry.MaxLevel = (byte) MathUtil.Clamp( (int) ( config.LevelMultiplier * entry.MaxLevel ), 2, 100 );
						}

						await this.LogAsync( $"    Entry: {speciesNames[ entry.Species ]}, levels {entry.MinLevel} to {entry.MaxLevel}" );
					}

					if ( config.TypeThemedAreas && config.TypePerSubArea ) // Re-generate type for the new sub-area
					{
						areaType = PokemonTypes.AllPokemonTypes.ToArray().GetRandom( taskRandom );

						UpdateChoiceList( species.Where( s => speciesInfo[ s.Id ].HasType( areaType ) ).ToList() );
					}

					if ( i != entryArrays.Length - 1 )
						await this.LogAsync();
				}

				int GetUniqueAllSections() => entryArrays.Sum( ea => ea.Select( e => e.Species ).Distinct().Count( sp => sp > 0 ) );

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
							randomTo = taskRandom.Next( entryArray.Length );

						// Get a species that's different than the slot we're overwriting so we make a duplicate
						var donorEntry = entryArray.First( e => e.Species > 0 && e.Species != entryArray[ randomTo ].Species );

						entryArray[ randomTo ].Species = donorEntry.Species;
						entryArrays[ i ]               = entryArray;

						// Don't bother with the rest of the arrays if we fixed the problem
						if ( GetUniqueAllSections() == MaxUniqueSpecies )
							break;
					}
				}

				await this.LogAsync();
				encounter.EntryArrays = entryArrays;
			}

			progressNotifier?.NotifyUpdate( ProgressUpdate.Update( "Randomizing encounters...\nCompressing encounter data...this may take a while", 1.0 ) );

			await this.Game.SaveEncounterData( encounters );
			await this.LogAsync( $"======== Finished Encounter randomization ========{Environment.NewLine}" );
		}
	}
}