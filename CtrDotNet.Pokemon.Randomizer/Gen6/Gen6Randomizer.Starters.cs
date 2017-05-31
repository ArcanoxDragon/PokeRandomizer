using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Structures.CRO.Gen6.Starters;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Randomizer.Gen6
{
	public partial class Gen6Randomizer
	{
		public override async Task RandomizeStarters()
		{
			Starters starters = await this.Game.GetStarters();
			List<SpeciesType> chosen = new List<SpeciesType>( starters.Generations.Count() * 3 );

			for ( int i = 0; i < chosen.Capacity; i++ )
			{
				SpeciesType ret = this.GetRandomSpecies( sp => this.RandomizerConfig.Starters.StartersOnly
																   ? sp.Intersect( Legality.Starters.AllStarters )
																   : sp, chosen );

				chosen.Add( ret );
			}

			starters.Generations.ForEach( ( gen, genIndex ) => {
				for ( int i = 0; i < 3; i++ )
					starters[ gen ][ i ] = (ushort) chosen[ genIndex * 3 + i ];
			} );
		}
	}
}