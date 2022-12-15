using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Config;
using PokeRandomizer.Progress;
using PokeRandomizer.Tasks;
using PokeRandomizer.Utility;

namespace PokeRandomizer.Common
{
	public abstract class BaseRandomizer : IRandomizer
	{
		protected BaseRandomizer(int seed)
		{
			RandomSeed = seed;
			Random = new Random(seed);
		}

		protected BaseRandomizer() : this(Environment.TickCount) { }

		public RandomizerConfig Config     { get; set; }
		public GameConfig       Game       { get; private set; }
		public int              RandomSeed { get; private set; }
		public ILogger          Logger     { get; set; }

		private Random Random { get; set; }

		internal void Initialize(GameConfig game, RandomizerConfig randomizerConfig)
		{
			Game = game;
			Config = randomizerConfig;
		}

		public void Reseed(int seed)
		{
			RandomSeed = seed;
			Random = new Random(seed);
		}

		public void Reseed() => Reseed(Environment.TickCount);

		protected RandomizerConfig ValidateAndGetConfig()
		{
			Validator.ValidateConfig(Config);
			return Config;
		}

		protected async Task LogAsync(string text = "")
		{
			if (Logger != null)
				await Logger.WriteLineAsync(text);
		}

		public async Task RandomizeAll(ProgressNotifier progress, CancellationToken token)
		{
			await LogAsync($"Randomizer started. Using seed: {RandomSeed}{Environment.NewLine}");

			var runner = new TaskRunner(GetRandomizationTasks());

			if (progress != null)
				runner.ProgressNotifier.ProgressUpdated += (_, u) => progress.NotifyUpdate(u);

			await runner.Run(Random, token);

			progress?.NotifyUpdate(ProgressUpdate.Completed());
			await LogAsync("Randomization has finished.");

			if (Logger != null)
				await Logger.FlushAsync();
		}

		#region Randomization tasks

		protected virtual IEnumerable<TaskRunner.TaskFunction> GetRandomizationTasks()
		{
			yield return RandomizePokemonInfo;
			yield return RandomizeEggMoves;
			yield return RandomizeLearnsets;
			yield return RandomizeEncounters;
			yield return RandomizeStarters;
			yield return RandomizeTrainers;
		}

		public abstract Task RandomizePokemonInfo(Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token);
		public abstract Task RandomizeEggMoves(Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token);
		public abstract Task RandomizeEncounters(Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token);
		public abstract Task RandomizeLearnsets(Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token);
		public abstract Task RandomizeStarters(Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token);
		public abstract Task RandomizeTrainers(Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token);

		#endregion

		#region Helpers

		public SpeciesType GetRandomSpecies(Random taskRandom, IEnumerable<SpeciesType> chooseFrom)
		{
			var available = chooseFrom.Where(st => st.Id <= Game.Version.GetInfo().SpeciesCount)
									  .ToList();

			if (available.Count <= 0)
				throw new InvalidDataException("No species available matching the given constraints");

			return available.GetRandom(taskRandom);
		}

		#endregion
	}
}