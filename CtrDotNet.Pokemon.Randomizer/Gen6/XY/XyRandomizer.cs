using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Randomization.Reflection;

namespace CtrDotNet.Pokemon.Randomization.Gen6.XY
{
	[ HandlesGame( GameVersion.XY ) ]
	public partial class XyRandomizer : Gen6Randomizer
	{
		public XyRandomizer() { }
		public XyRandomizer( int seed ) : base( seed ) { }
	}
}