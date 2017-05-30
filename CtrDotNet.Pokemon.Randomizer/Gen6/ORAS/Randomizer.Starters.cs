using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Structures.CRO.Gen6.Starters;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Randomizer.Gen6.ORAS
{
	public partial class Randomizer
	{
		public override async Task RandomizeStarters()
		{
			Starters starters = await this.Game.GetStarters();

			starters.StarterSpecies.ForEach( arr => arr.Fill( (ushort) 0 ) );

			bool UsedAlready( SpeciesType species )
				=> starters.StarterSpecies.Any( arr => arr.Contains( (ushort) species.Id ) );

			SpeciesType GetRandom()
			{
				int tries = 0;
				SpeciesType ret = null;
				SpeciesType[] species = ( this.RandomizerConfig.Starters.StartersOnly
											  // All legal starters
											  ? Legality.Starters.AllStarters
											  // All species for the current generation, except Egg
											  : Species.AllSpecies.Skip( 1 ).Take( this.Game.Version.GetInfo().SpeciesCount ) ).ToArray();

				while ( ret == null || UsedAlready( ret ) && tries++ < 10 )
					ret = species[ this.rand.Next( species.Length ) ];

				if ( tries >= 10 )
					throw new InvalidDataException( $"Could not find a unique starter candidate after {tries} tries" );

				return ret;
			}

			starters.Generations.ForEach( gen => {
				for ( int i = 0; i < 3; i++ )
					starters[ gen ][ i ] = (ushort) GetRandom().Id;
			} );
		}
	}
}