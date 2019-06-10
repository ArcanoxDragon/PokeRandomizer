using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
		public override async Task RandomizeEncounters( ProgressNotifier progressNotifier, CancellationToken token )
		{
			var config = this.ValidateAndGetConfig().Encounters;

			if ( !config.RandomizeEncounters )
				return;

			progressNotifier?.NotifyUpdate( ProgressUpdate.StatusOnly( "Randomizing wild Pokémon encounters..." ) );

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
					var areaType = PokemonTypes.AllPokemonTypes.ToArray().GetRandom( this.Random );
					speciesChoose = speciesChoose.Where( s => speciesInfo[ s.Id ].HasType( areaType ) )
												 .ToList();
				}

				var entryArrays = encounter.EntryArrays;

				foreach ( var entryArray in entryArrays )
				{
					foreach ( var entry in entryArray.Where( entry => entry.Species > 0 ) )
					{
						entry.Species = (ushort) this.GetRandomSpecies( speciesChoose ).Id;

						if ( config.LevelMultiplier != 1.0m )
						{
							entry.MinLevel = (byte) MathUtil.Clamp( (int) ( config.LevelMultiplier * entry.MinLevel ), 2, 100 );
							entry.MaxLevel = (byte) MathUtil.Clamp( (int) ( config.LevelMultiplier * entry.MaxLevel ), 2, 100 );
						}
					}

					if ( config.TypeThemedAreas && config.TypePerSubArea ) // Re-generate type for the new sub-area
					{
						var areaType = PokemonTypes.AllPokemonTypes.ToArray().GetRandom( this.Random );
						speciesChoose = speciesChoose.Where( s => speciesInfo[ s.Id ].HasType( areaType ) )
													 .ToList();
					}
				}

				encounter.EntryArrays = entryArrays;
			}

			progressNotifier?.NotifyUpdate( ProgressUpdate.Update( $"Randomizing encounters...\nCompressing encounter data...this may take a while", 1.0 ) );

			await this.Game.SaveEncounterData( encounters );
		}
	}
}