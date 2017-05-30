using System;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Randomizer.Config;

namespace CtrDotNet.Pokemon.Randomizer.Common
{
	public abstract class BaseRandomizer
	{
		protected readonly Random rand;

		protected BaseRandomizer( GameConfig game, RandomizerConfig randomizerConfig )
		{
			this.rand = new Random();

			this.Game = game;
			this.RandomizerConfig = randomizerConfig;
		}

		public GameConfig Game { get; }
		public RandomizerConfig RandomizerConfig { get; }

		public abstract Task RandomizeStarters();
	}
}