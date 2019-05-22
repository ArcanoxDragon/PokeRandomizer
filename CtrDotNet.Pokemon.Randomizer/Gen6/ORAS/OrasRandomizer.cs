using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Randomization.Reflection;

namespace CtrDotNet.Pokemon.Randomization.Gen6.ORAS
{
	[ HandlesGame( GameVersion.ORAS ) ]
	[ HandlesGame( GameVersion.ORASDemo ) ]
	public partial class OrasRandomizer : Gen6Randomizer
	{
		public OrasRandomizer() { }
		public OrasRandomizer( int seed ) : base( seed ) { }
	}
}