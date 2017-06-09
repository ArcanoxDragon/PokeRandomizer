using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Randomization.Progress;

namespace CtrDotNet.Pokemon.Randomization.Tasks
{
	public class TaskRunner : IEnumerable<TaskRunner.TaskFunction>
	{
		public delegate Task TaskFunction( ProgressNotifier notifier, CancellationToken token );

		private readonly List<TaskFunction> tasks;

		public TaskRunner()
		{
			this.tasks = new List<TaskFunction>();

			this.ProgressNotifier = new ProgressNotifier();
		}

		public ProgressNotifier ProgressNotifier { get; }

		public void Clear() => this.tasks.Clear();
		public void Add( TaskFunction task ) => this.tasks.Add( task );

		public async Task Run( CancellationToken token )
		{
			int cur = 0;

			foreach ( var task in this.tasks )
			{
				int thisTask = cur;

				if ( token.IsCancellationRequested )
					this.ProgressNotifier.NotifyUpdate( ProgressUpdate.Cancelled() );

				if ( this.ProgressNotifier.IsCancelled || this.ProgressNotifier.IsFailed )
					break;

				ProgressNotifier subNotifier = new ProgressNotifier();
				subNotifier.ProgressUpdated += ( s, u ) => {
					if ( u.Progress < 0 )
						this.ProgressNotifier.NotifyUpdate( u );
					else
					{
						double progWithSub = ( thisTask + subNotifier.Progress ) / this.tasks.Count;
						this.ProgressNotifier.NotifyUpdate( ProgressUpdate.Update( u.Status, progWithSub ) );
					}
				};

				await task( subNotifier, token );

				this.ProgressNotifier.NotifyUpdate( ProgressUpdate.Update( this.ProgressNotifier.Status, ( cur++ ) / (double) this.tasks.Count ) );
			}
		}

		#region Implementation of IEnumerable

		public IEnumerator<TaskFunction> GetEnumerator() => this.tasks.GetEnumerator();
		IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

		#endregion
	}
}