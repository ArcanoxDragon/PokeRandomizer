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
		GameConfig       Game   { get; }
		RandomizerConfig Config { get; set; }
		Random           Random { get; }
		ILogger          Logger { get; set; }

		void Reseed( int seed );
		void Reseed();

		Task RandomizeAll( ProgressNotifier progressNotifier, CancellationToken token );

		Task RandomizePokemonInfo( ProgressNotifier progressNotifier, CancellationToken token );
		Task RandomizeEggMoves( ProgressNotifier progressNotifier, CancellationToken token );
		Task RandomizeEncounters( ProgressNotifier progressNotifier, CancellationToken token );
		Task RandomizeLearnsets( ProgressNotifier progressNotifier, CancellationToken token );
		Task RandomizeStarters( ProgressNotifier progressNotifier, CancellationToken token );
		Task RandomizeTrainers( ProgressNotifier progressNotifier, CancellationToken token );
	}
}