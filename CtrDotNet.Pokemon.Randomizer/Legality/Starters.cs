using System.Linq;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Randomization.Utility;

namespace CtrDotNet.Pokemon.Randomization.Legality
{
	public static class Starters
	{
		public static readonly SpeciesType[] AllStarters = {
			// Gen 1
			Species.Bulbasaur, Species.Charmander, Species.Squirtle,

			// Gen 2
			Species.Chikorita, Species.Cyndaquil, Species.Totodile,

			// Gen 3
			Species.Treecko, Species.Torchic, Species.Mudkip,

			// Gen 4
			Species.Turtwig, Species.Chimchar, Species.Piplup,

			// Gen 5
			Species.Snivy, Species.Tepig, Species.Oshawott,

			// Gen 6
			Species.Chespin, Species.Fennekin, Species.Froakie
		};

		public static SpeciesType[] StartersForGen( int gen )
		{
			Assertions.AssertIn( 1, 6, gen );

			return AllStarters.Skip( ( gen - 1 ) * 3 ).Take( 3 ).ToArray();
		}
	}
}