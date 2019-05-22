using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Randomization.Utility;

namespace CtrDotNet.Pokemon.Randomization.Progress
{
	public class ProgressNotifier
	{
		public delegate void ProgressUpdatedEvent( ProgressNotifier sender, ProgressUpdate update );

		public event ProgressUpdatedEvent ProgressUpdated;

		private readonly List<TaskCompletionSource<ProgressUpdate>> awaiting;

		public ProgressNotifier()
		{
			this.awaiting = new List<TaskCompletionSource<ProgressUpdate>>();
		}

		public Exception FailureException { get; private set; }
		public bool IsComplete { get; private set; }
		public bool IsCancelled { get; private set; }
		public bool IsFailed { get; private set; }
		public double Progress { get; private set; }
		public string Status { get; private set; }

		public Task<ProgressUpdate> AwaitUpdate()
		{
			if ( this.IsFailed )
				return Task.FromException<ProgressUpdate>( this.FailureException );

			if ( this.IsComplete )
				return Task.FromResult( ProgressUpdate.Completed() );

			if ( this.IsComplete )
				return Task.FromResult( ProgressUpdate.Cancelled() );

			var source = new TaskCompletionSource<ProgressUpdate>();

			lock ( this.awaiting )
				this.awaiting.Add( source );

			return source.Task;
		}

		public void NotifyUpdate( ProgressUpdate update )
		{
			if ( this.IsComplete || this.IsFailed || this.IsCancelled )
				return;

			this.IsComplete = update.Type == ProgressUpdate.UpdateType.Completed;
			this.IsCancelled = update.Type == ProgressUpdate.UpdateType.Cancelled;
			this.IsFailed = update.Type == ProgressUpdate.UpdateType.Failed;
			this.Progress = ( update.Progress >= 0 ) ? update.Progress : this.Progress;
			this.Status = update.Status ?? this.Status;
			this.FailureException = update.FailureCause;

			this.Progress = MathUtil.Clamp( this.Progress, 0.0, 1.0 );

			List<TaskCompletionSource<ProgressUpdate>> curAwaiting;

			lock ( this.awaiting )
			{
				curAwaiting = new List<TaskCompletionSource<ProgressUpdate>>( this.awaiting );
				this.awaiting.Clear();
			}

			foreach ( var source in curAwaiting )
				source.SetResult( update );

			this.ProgressUpdated?.Invoke( this, update );
		}

		public void NotifyFailure( Exception e )
		{
			if ( this.IsComplete || this.IsFailed || this.IsCancelled )
				return;

			this.IsFailed = true;
			this.FailureException = e;

			List<TaskCompletionSource<ProgressUpdate>> curAwaiting;

			lock ( this.awaiting )
			{
				curAwaiting = new List<TaskCompletionSource<ProgressUpdate>>( this.awaiting );
				this.awaiting.Clear();
			}

			foreach ( var source in curAwaiting )
				source.SetException( e );

			this.ProgressUpdated?.Invoke( this, ProgressUpdate.Failure( e ) );
		}
	}
}