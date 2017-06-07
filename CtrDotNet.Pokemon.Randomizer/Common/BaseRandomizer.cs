using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Randomization.Config;
using CtrDotNet.Pokemon.Randomization.Utility;

namespace CtrDotNet.Pokemon.Randomization.Common
{
	public abstract class BaseRandomizer : IRandomizer
	{
		protected readonly Random rand;

		protected BaseRandomizer()
		{
			this.rand = new Random();
		}

		public GameConfig Game { get; private set; }
		public IConfig RandomizerConfig { get; private set; }

		internal void Initialize( GameConfig game, IConfig randomizerConfig )
		{
			this.Game = game;
			this.RandomizerConfig = randomizerConfig;
		}

		public async Task RandomizeAll()
		{
			await this.RandomizeAbilities();
			await this.RandomizeEggMoves();
			await this.RandomizeEncounters();
			await this.RandomizeLearnsets();
			await this.RandomizeStarters();
			await this.RandomizeTrainers();
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