using System.Threading.Tasks;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Randomizer.Config;

namespace CtrDotNet.Pokemon.Randomizer.Common
{
	public interface IRandomizer
	{
		GameConfig Game { get; }
		RandomizerConfig RandomizerConfig { get; }

		Task RandomizeStarters();
	}
}