using System;
using System.Threading;
using System.Threading.Tasks;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Config;
using PokeRandomizer.Progress;
using PokeRandomizer.Utility;

namespace PokeRandomizer.Common
{
	public interface IRandomizer
	{
		RandomizerConfig Config     { get; set; }
		GameConfig       Game       { get; }
		int              RandomSeed { get; }
		ILogger          Logger     { get; set; }

		void Reseed(int seed);
		void Reseed();

		Task RandomizeAll(ProgressNotifier progressNotifier, CancellationToken token);

		Task RandomizePokemonInfo(Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token);
		Task RandomizeEggMoves(Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token);
		Task RandomizeEncounters(Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token);
		Task RandomizeLearnsets(Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token);
		Task RandomizeStarters(Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token);
		Task RandomizeTrainers(Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token);
	}
}