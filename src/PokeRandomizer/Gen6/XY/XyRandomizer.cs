using PokeRandomizer.Common.Game;
using PokeRandomizer.Reflection;

namespace PokeRandomizer.Gen6.XY
{
	[HandlesGame(GameVersion.XY)]
	public partial class XyRandomizer : Gen6Randomizer
	{
		public XyRandomizer() { }
		public XyRandomizer(int seed) : base(seed) { }
	}
}