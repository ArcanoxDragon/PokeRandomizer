using System.Threading.Tasks;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Randomization.Config;

namespace CtrDotNet.Pokemon.Randomization.Common
{
	public interface IRandomizer
	{
		GameConfig Game { get; }
		RandomizerConfig RandomizerConfig { get; }

		void Initialize( GameConfig game, RandomizerConfig randomizerConfig );

		Task RandomizeStarters();
		Task RandomizeEncounters();
		Task RandomizeLearnsets();
	}
}