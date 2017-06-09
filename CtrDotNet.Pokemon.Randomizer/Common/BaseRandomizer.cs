using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Randomization.Config;
using CtrDotNet.Pokemon.Randomization.Progress;
using CtrDotNet.Pokemon.Randomization.Tasks;
using CtrDotNet.Pokemon.Randomization.Utility;

namespace CtrDotNet.Pokemon.Randomization.Common
{
	public abstract class BaseRandomizer : IRandomizer
	{
		private const int NumSubOperations = 6;

		protected readonly Random rand;

		protected BaseRandomizer()
		{
			this.rand = new Random();
		}

		public RandomizerConfig Config { get; set; }
		public GameConfig Game { get; private set; }

		internal void Initialize( GameConfig game, RandomizerConfig randomizerConfig )
		{
			this.Game = game;
			this.Config = randomizerConfig;
		}

		protected RandomizerConfig ValidateAndGetConfig()
		{
			Validator.ValidateConfig( this.Config );
			return this.Config;
		}

		public async Task RandomizeAll( ProgressNotifier progress, CancellationToken token )
		{
			var runner = new TaskRunner {
				this.RandomizeAbilities,
				this.RandomizeEggMoves,
				this.RandomizeEncounters,
				this.RandomizeLearnsets,
				this.RandomizeStarters,
				this.RandomizeTrainers
			};

			if ( progress != null )
				runner.ProgressNotifier.ProgressUpdated += ( s, u ) => progress.NotifyUpdate( u );

			await runner.Run( token );

			progress?.NotifyUpdate( ProgressUpdate.Completed() );
		}

		#region Randomization tasks

		public abstract Task RandomizeAbilities( ProgressNotifier progressNotifier, CancellationToken token );
		public abstract Task RandomizeEggMoves( ProgressNotifier progressNotifier, CancellationToken token );
		public abstract Task RandomizeEncounters( ProgressNotifier progressNotifier, CancellationToken token );
		public abstract Task RandomizeLearnsets( ProgressNotifier progressNotifier, CancellationToken token );
		public abstract Task RandomizeStarters( ProgressNotifier progressNotifier, CancellationToken token );
		public abstract Task RandomizeTrainers( ProgressNotifier progressNotifier, CancellationToken token );

		#endregion

		#region Helpers

		public SpeciesType GetRandomSpecies( IEnumerable<SpeciesType> chooseFrom )
		{
			var available = chooseFrom.Where( st => st.Id <= this.Game.Version.GetInfo().SpeciesCount )
									  .ToList();

			if ( available.Count <= 0 )
				throw new InvalidDataException( "No species available matching the given constraints" );

			return available.GetRandom( this.rand );
		}

		#endregion
	}
}