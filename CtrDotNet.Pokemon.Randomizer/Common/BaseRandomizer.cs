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

		public async Task RandomizeAll( ProgressNotifier progress = null, CancellationToken? token = null )
		{
			var t = token ?? CancellationToken.None;
			int subOp = 0;
			double Progress( int op ) => ( op / (double) NumSubOperations );

			bool DoUpdate( string message )
			{
				if ( t.IsCancellationRequested )
				{
					progress?.NotifyUpdate( ProgressUpdate.Cancelled() );
					return false;
				}

				progress?.NotifyUpdate( ProgressUpdate.Update( message, Progress( subOp++ ) ) );
				return true;
			}

			if ( !DoUpdate( "Randomizing abilities" ) )
				return;

			await this.RandomizeAbilities();

			if ( !DoUpdate( "Randomizing egg moves" ) )
				return;

			await this.RandomizeEggMoves();

			if ( !DoUpdate( "Randomizing wild encounters" ) )
				return;

			await this.RandomizeEncounters();

			if ( !DoUpdate( "Randomizing Pokémon learnsets" ) )
				return;

			await this.RandomizeLearnsets();

			if ( !DoUpdate( "Randomizing starter Pokémon" ) )
				return;

			await this.RandomizeStarters();

			if ( !DoUpdate( "Randomizing trainer battles" ) )
				return;

			await this.RandomizeTrainers();

			progress?.NotifyUpdate( ProgressUpdate.Completed() );
		}

		#region Randomization tasks

		public abstract Task RandomizeAbilities();
		public abstract Task RandomizeEggMoves();
		public abstract Task RandomizeEncounters();
		public abstract Task RandomizeLearnsets();
		public abstract Task RandomizeStarters();
		public abstract Task RandomizeTrainers();

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