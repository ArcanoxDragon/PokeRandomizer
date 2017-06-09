using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtrDotNet.Pokemon.Randomization.Progress
{
	public class ProgressNotifier
	{
		private readonly List<TaskCompletionSource<ProgressUpdate>> awaiting;
		private Exception failureException;

		public ProgressNotifier()
		{
			this.awaiting = new List<TaskCompletionSource<ProgressUpdate>>();
		}

		public bool IsComplete { get; private set; }
		public bool IsCancelled { get; private set; }
		public bool IsFailed { get; private set; }
		public double Progress { get; private set; }
		public string Status { get; private set; }

		public Task<ProgressUpdate> AwaitUpdate()
		{
			if ( this.IsFailed )
				return Task.FromException<ProgressUpdate>( this.failureException );

			if ( this.IsComplete )
				return Task.FromResult( ProgressUpdate.Completed() );

			if ( this.IsComplete )
				return Task.FromResult( ProgressUpdate.Cancelled() );

			var source = new TaskCompletionSource<ProgressUpdate>();

			this.awaiting.Add( source );

			return source.Task;
		}

		public void NotifyUpdate( ProgressUpdate update )
		{
			if ( this.IsComplete || this.IsFailed || this.IsCancelled )
				return;

			this.IsComplete = update.Type == ProgressUpdate.UpdateType.Completed;
			this.IsCancelled = update.Type == ProgressUpdate.UpdateType.Cancelled;
			this.Progress = ( update.Progress >= 0 ) ? update.Progress : this.Progress;
			this.Status = update.Status;

			foreach ( var source in this.awaiting )
				source.SetResult( update );

			this.awaiting.Clear();
		}

		public void NotifyFailure( Exception e )
		{
			if ( this.IsComplete || this.IsFailed || this.IsCancelled )
				return;

			this.IsFailed = true;
			this.failureException = e;

			foreach ( var source in this.awaiting )
				source.SetException( e );

			this.awaiting.Clear();
		}
	}
}