using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CtrDotNet.Utility.Extensions;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Reference;
using PokeRandomizer.Common.Structures.RomFS.Gen6.XY;
using PokeRandomizer.Legality;
using PokeRandomizer.Progress;
using PokeRandomizer.Utility;
using Species = PokeRandomizer.Common.Data.Partial.Species;

namespace PokeRandomizer.Gen6.XY
{
	public partial class XyRandomizer
	{
		public override async Task RandomizeEncounters( Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token )
		{
			var config = this.ValidateAndGetConfig().Encounters;

			if ( !config.RandomizeEncounters )
				return;

			await this.LogAsync( $"======== Beginning Encounter randomization ========{Environment.NewLine}" );
			progressNotifier?.NotifyUpdate( ProgressUpdate.StatusOnly( "Randomizing wild Pokémon encounters..." ) );

			var species       = Species.ValidSpecies.ToList();
			var speciesInfo   = await this.Game.GetPokemonInfo( edited: true );
			var encounterData = await this.Game.GetEncounterData();
			var encounters    = encounterData.Cast<XyEncounterWild>().ToList();
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

				if ( config.TypeThemedAreas )
				{
					areaType = PokemonTypes.AllPokemonTypes.ToArray().GetRandom( taskRandom );

					speciesChoose = species.Where( s => speciesInfo[ s.Id ].HasType( areaType ) ).ToList();
				}
				else
				{
					speciesChoose = species.ToList();
				}

				if ( areaType == default || config.TypePerSubArea )
					await this.LogAsync( $"Randomizing zone: {name}" );
				else
					await this.LogAsync( $"Randomizing zone: {name} ({areaType.Name}-type)" );

				var entryArrays = encounter.EntryArrays;

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
						areaType      = PokemonTypes.AllPokemonTypes.ToArray().GetRandom( taskRandom );
						speciesChoose = species.Where( s => speciesInfo[ s.Id ].HasType( areaType ) ).ToList();
					}

					if ( i != entryArrays.Length - 1 )
						await this.LogAsync();
				}

				await this.LogAsync();
				encounter.EntryArrays = entryArrays;
			}

			await this.LogAsync();
			progressNotifier?.NotifyUpdate( ProgressUpdate.Update( $"Randomizing encounters...\nCompressing encounter data...this may take a while", 1.0 ) );

			await this.Game.SaveEncounterData( encounters );
			await this.LogAsync( $"======== Finished Encounter randomization ========{Environment.NewLine}" );
		}
	}
}