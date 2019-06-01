using PokeRandomizer.Common.Game;
using PokeRandomizer.Reflection;

namespace PokeRandomizer.Gen6.ORAS
{
	[ HandlesGame( GameVersion.ORAS ) ]
	[ HandlesGame( GameVersion.ORASDemo ) ]
	public partial class OrasRandomizer : Gen6Randomizer
	{
		public OrasRandomizer() { }
		public OrasRandomizer( int seed ) : base( seed ) { }
	}
}