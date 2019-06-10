using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Utility;
using PokeRandomizer.Legality;
using PokeRandomizer.Progress;
using Species = PokeRandomizer.Common.Data.Partial.Species;
using Starters = PokeRandomizer.Legality.Starters;

namespace PokeRandomizer.Gen6
{
	public abstract partial class Gen6Randomizer
	{
		public override async Task RandomizeStarters( ProgressNotifier progressNotifier, CancellationToken token )
		{
			var config = this.ValidateAndGetConfig().Starters;

			if ( !config.RandomizeStarters )
				return;

			progressNotifier?.NotifyUpdate( ProgressUpdate.StatusOnly( "Randomizing starter Pokémon..." ) );

			var starters    = await this.Game.GetStarters();
			var species     = Species.ValidSpecies.ToList();
			var speciesInfo = await this.Game.GetPokemonInfo( true );
			var chosen      = new List<SpeciesType>( starters.Generations.Count() * 3 );

			if ( config.StartersOnly )
			{
				species = species.Intersect( Starters.AllStarters )
								 .ToList();
			}
			else if ( config.OnlyElementalTypes )
			{
				species = species.Where( s => {
					var info = speciesInfo[ s ];

					return info.HasType( PokemonTypes.Fire ) ||
						   info.HasType( PokemonTypes.Water ) ||
						   info.HasType( PokemonTypes.Grass );
				} ).ToList();
			}

			if ( !config.AllowLegendaries )
			{
				species = species.Except( Legendaries.AllLegendaries )
								 .ToList();
			}

			for ( int i = 0; i < chosen.Capacity; i++ )
			{
				var ret = this.GetRandomSpecies( species.Except( chosen ) );

				chosen.Add( ret );
			}

			starters.Generations.ForEach( ( gen, genIndex ) => {
				for ( int i = 0; i < 3; i++ )
					starters[ gen ][ i ] = (ushort) chosen[ genIndex * 3 + i ];
			} );

			await this.Game.SaveStarters( starters );
		}
	}
}