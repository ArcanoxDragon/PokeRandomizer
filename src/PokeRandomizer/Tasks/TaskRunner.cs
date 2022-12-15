using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PokeRandomizer.Progress;

namespace PokeRandomizer.Tasks
{
	public class TaskRunner : IEnumerable<TaskRunner.TaskFunction>
	{
		public delegate Task TaskFunction(Random taskRandom, ProgressNotifier notifier, CancellationToken token);

		private readonly List<TaskFunction> tasks;

		public TaskRunner()
		{
			this.tasks = new List<TaskFunction>();

			ProgressNotifier = new ProgressNotifier();
		}

		public TaskRunner(IEnumerable<TaskFunction> tasks) : this()
		{
			this.tasks.AddRange(tasks);
		}

		public ProgressNotifier ProgressNotifier { get; }

		public void Clear() => this.tasks.Clear();
		public void Add(TaskFunction task) => this.tasks.Add(task);

		public async Task Run(Random masterRandom, CancellationToken token)
		{
			int cur = 0;

			foreach (var task in this.tasks)
			{
				int thisTask = cur;

				if (token.IsCancellationRequested)
					ProgressNotifier.NotifyUpdate(ProgressUpdate.Cancelled());

				if (ProgressNotifier.IsCancelled || ProgressNotifier.IsFailed)
					break;

				ProgressNotifier subNotifier = new ProgressNotifier();

				subNotifier.ProgressUpdated += (_, u) => {
					if (u.Progress < 0)
					{
						ProgressNotifier.NotifyUpdate(u);
					}
					else
					{
						double progWithSub = ( thisTask + subNotifier.Progress ) / this.tasks.Count;
						ProgressNotifier.NotifyUpdate(ProgressUpdate.Update(u.Status, progWithSub));
					}
				};

				try
				{
					var taskRandom = new Random(masterRandom.Next());

					await task(taskRandom, subNotifier, token);
				}
				catch (Exception e)
				{
					ProgressNotifier?.NotifyFailure(e);
					throw;
				}

				ProgressNotifier.NotifyUpdate(ProgressUpdate.Update(ProgressNotifier.Status ?? string.Empty, ++cur / (double) this.tasks.Count));
			}
		}

		#region Implementation of IEnumerable

		public IEnumerator<TaskFunction> GetEnumerator() => this.tasks.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

		#endregion
	}
}