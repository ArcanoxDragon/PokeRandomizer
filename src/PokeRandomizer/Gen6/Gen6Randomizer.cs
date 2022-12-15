using System.Collections.Generic;
using PokeRandomizer.Common;
using PokeRandomizer.Tasks;

namespace PokeRandomizer.Gen6
{
	public abstract partial class Gen6Randomizer : BaseRandomizer
	{
		protected Gen6Randomizer() { }
		protected Gen6Randomizer(int seed) : base(seed) { }

		#region Randomization Tasks (Gen6 Specific)

		protected override IEnumerable<TaskRunner.TaskFunction> GetRandomizationTasks()
		{
			// Include all the base tasks first
			foreach (var task in base.GetRandomizationTasks())
				yield return task;

			// Also randomize overworld item Pokéballs
			yield return RandomizeOverworldItems;
		}

		#endregion
	}
}