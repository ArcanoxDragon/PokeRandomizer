using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Randomizer.Config;
using CtrDotNet.Pokemon.Randomizer.Reflection;

namespace CtrDotNet.Pokemon.Randomizer.Gen6.ORAS
{
	[ HandlesGame( GameVersion.ORAS ) ]
	[ HandlesGame( GameVersion.ORASDemo ) ]
	public class OrasRandomizer : Gen6Randomizer
	{
		public OrasRandomizer( GameConfig game, RandomizerConfig randomizerConfig ) : base( game, randomizerConfig ) { }
	}
}