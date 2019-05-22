using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Randomization.Legality;
using CtrDotNet.Pokemon.Randomization.Progress;
using CtrDotNet.Pokemon.Utility;
using Starters = CtrDotNet.Pokemon.Randomization.Legality.Starters;

namespace CtrDotNet.Pokemon.Randomization.Gen6
{
	public abstract partial class Gen6Randomizer
	{
		public override async Task RandomizeStarters( ProgressNotifier progressNotifier, CancellationToken token )
		{
			progressNotifier?.NotifyUpdate( ProgressUpdate.StatusOnly( "Randomizing starter Pokémon..." ) );

			var config = this.ValidateAndGetConfig().Starters;
			var starters = await this.Game.GetStarters();
			var species = Species.ValidSpecies.ToList();
			var chosen = new List<SpeciesType>( starters.Generations.Count() * 3 );

			if ( config.StartersOnly )
				species = species.Intersect( Starters.AllStarters )
								 .ToList();

			if ( !config.AllowLegendaries )
				species = species.Except( Legendaries.AllLegendaries )
								 .ToList();

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