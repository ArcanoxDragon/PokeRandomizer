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

namespace PokeRandomizer.Gen6.ORAS
{
	public partial class OrasRandomizer
	{
		private static readonly int[] HordeSubZones    = { 8, 9, 10 };
		private const           int   GrassSubZone     = 0;
		private const           int   GrassEntries     = 12;
		private const           int   MaxUniqueSpecies = 18;

		public override async Task RandomizeEncounters( Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token )
		{
			var config = this.ValidateAndGetConfig().Encounters;

			if ( !config.RandomizeEncounters )
				return;

			await this.LogAsync( $"======== Beginning Encounter randomization ========{Environment.NewLine}" );
			progressNotifier?.NotifyUpdate( ProgressUpdate.StatusOnly( "Randomizing wild Pokémon encounters..." ) );

			var species        = Species.ValidSpecies.ToList();
			var speciesInfo    = await this.Game.GetPokemonInfo( edited: true );
			var encounterData  = await this.Game.GetEncounterData();
			var encounterZones = encounterData.Cast<OrasEncounterWild>().ToList();
			var zoneNames      = ( await this.Game.GetTextFile( TextNames.EncounterZoneNames ) ).Lines;
			var speciesNames   = ( await this.Game.GetTextFile( TextNames.SpeciesNames ) ).Lines;

			(int zone, int subZone) dittoSpot = ( -1, -1 );

			// If "Ensure Dittos in Grass" is enabled, we need to make sure dittos appear in grass *somewhere* in the game.
			// Pick a random valid encounter zone/slot in which to place Ditto
			if ( config.EnsureDittosInGrass )
			{
				var foundPlaceForDitto = false;

				while ( !foundPlaceForDitto )
				{
					var dittoZone = encounterZones.GetRandom( taskRandom );

					if ( dittoZone.GetAllEntries().Any( e => e != null ) )
					{
						var entryArrays  = dittoZone.EntryArrays;
						var dittoSubZone = entryArrays.FirstOrDefault(); // Grass is always first sub-zone

						if ( dittoSubZone != null && !dittoSubZone.All( e => e.Species == Species.Egg.Id ) )
						{
							var dittoEntry = taskRandom.Next( Math.Min( dittoSubZone.Length, GrassEntries ) );

							// Store zone and entry index for later
							dittoSpot          = ( encounterZones.IndexOf( dittoZone ), dittoEntry );
							foundPlaceForDitto = true;
						}
					}
				}
			}

			if ( !config.AllowLegendaries )
				species = species.Except( Legendaries.AllLegendaries )
								 .ToList();

			foreach ( var (iEncounter, encounterZone) in encounterZones.Pairs() )
			{
				var name = zoneNames[ encounterZone.ZoneId ];

				if ( !encounterZone.GetAllEntries().Any( e => e != null ) )
					continue;

				progressNotifier?.NotifyUpdate( ProgressUpdate.Update( $"Randomizing encounters...\n{name}", iEncounter / (double) encounterZones.Count ) );

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

				var subZoneArrays = encounterZone.EntryArrays;
				int[] orderedIndices = subZoneArrays.OrderByDescending( ea => ea.Length )
													.Select( ea => Array.IndexOf( subZoneArrays, ea ) )
													.ToArray();

				foreach ( var (iSubZone, subZoneArray) in subZoneArrays.Pairs() )
				{
					if ( subZoneArray.All( e => e.Species == Species.Egg.Id ) )
						continue;

					if ( areaType != default && config.TypePerSubArea )
						await this.LogAsync( $"  Sub-zone {iSubZone + 1} ({areaType.Name}-type):" );
					else
						await this.LogAsync( $"  Sub-zone {iSubZone + 1}:" );

					// In horde battles, one of the slots could be a different species, and we treat the value "5" as "all the same"
					var isHordeEncounter = HordeSubZones.Contains( iSubZone );
					var hordeSlot        = isHordeEncounter ? taskRandom.Next( 0, 6 ) : -1;
					var hordeSpecies     = -1;

					foreach ( var (iEntry, entry) in subZoneArray.Where( entry => entry.Species != Species.Egg.Id ).Pairs() )
					{
						var (dittoEncounter, dittoEntry) = dittoSpot;

						if ( config.EnsureDittosInGrass && iEncounter == dittoEncounter && iSubZone == GrassSubZone && iEntry == dittoEntry )
						{
							entry.Species = (ushort) Species.Ditto.Id;
						}
						else
						{
							if ( config.ProperHordes && isHordeEncounter )
							{
								// For horde battles, every slot has the same species, except for optionally a random slot which can be unique

								if ( iEntry == hordeSlot )
								{
									entry.Species = (ushort) this.GetRandomSpecies( taskRandom, speciesChoose ).Id;
								}
								else
								{
									if ( hordeSpecies < 0 )
										hordeSpecies = this.GetRandomSpecies( taskRandom, speciesChoose ).Id;

									entry.Species = (ushort) hordeSpecies;
								}
							}
							else
							{
								entry.Species = (ushort) this.GetRandomSpecies( taskRandom, speciesChoose ).Id;
							}
						}

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

					if ( iSubZone != subZoneArrays.Length - 1 )
						await this.LogAsync();
				}

				int GetUniqueAllSections() => subZoneArrays.Sum( ea => ea.Select( e => e.Species ).Distinct().Count( sp => sp > 0 ) );

				// DexNav crashes if there are more than 18 unique species in an encounter zone.
				// If the same species appears in two different sub-zones, it counts as two
				// unique species.
				while ( GetUniqueAllSections() > MaxUniqueSpecies )
				{
					// Starting with the largest array, reduce the number of unique species taken up by that array by one
					foreach ( int i in orderedIndices )
					{
						var entryArray        = subZoneArrays[ i ];
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
						subZoneArrays[ i ]             = entryArray;

						// Don't bother with the rest of the arrays if we fixed the problem
						if ( GetUniqueAllSections() == MaxUniqueSpecies )
							break;
					}
				}

				await this.LogAsync();
				encounterZone.EntryArrays = subZoneArrays;
			}

			await this.LogAsync();
			progressNotifier?.NotifyUpdate( ProgressUpdate.Update( "Randomizing encounters...\nCompressing encounter data...this may take a while", 1.0 ) );

			await this.Game.SaveEncounterData( encounterZones );
			await this.LogAsync( $"======== Finished Encounter randomization ========{Environment.NewLine}" );
		}
	}
}