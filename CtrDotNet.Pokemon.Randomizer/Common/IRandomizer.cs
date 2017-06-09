using System.Threading;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Randomization.Config;
using CtrDotNet.Pokemon.Randomization.Progress;

namespace CtrDotNet.Pokemon.Randomization.Common
{
	public interface IRandomizer
	{
		GameConfig Game { get; }
		RandomizerConfig Config { get; set; }

		Task RandomizeAll( ProgressNotifier progressNotifier = null, CancellationToken? token = null );

		Task RandomizeAbilities();
		Task RandomizeEggMoves();
		Task RandomizeEncounters();
		Task RandomizeLearnsets();
		Task RandomizeStarters();
		Task RandomizeTrainers();
	}
}