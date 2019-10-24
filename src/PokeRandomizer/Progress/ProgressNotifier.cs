using System;
using System.Threading.Tasks;
using PokeRandomizer.Utility;

namespace PokeRandomizer.Progress
{
	public class ProgressNotifier
	{
		public delegate void ProgressUpdatedEvent( ProgressNotifier sender, ProgressUpdate update );

		public event ProgressUpdatedEvent ProgressUpdated;

		private readonly object completionSourceLock = new object();

		private TaskCompletionSource<ProgressUpdate> completionSource;

		public ProgressNotifier() { }

		public Exception FailureException { get; private set; }
		public bool      IsComplete       { get; private set; }
		public bool      IsCancelled      { get; private set; }
		public bool      IsFailed         { get; private set; }
		public double    Progress         { get; private set; }
		public string    Status           { get; private set; }

		public Task<ProgressUpdate> AwaitUpdate()
		{
			if ( this.IsFailed )
				return Task.FromException<ProgressUpdate>( this.FailureException );

			if ( this.IsComplete )
				return Task.FromResult( ProgressUpdate.Completed() );

			if ( this.IsComplete )
				return Task.FromResult( ProgressUpdate.Cancelled() );

			lock ( this.completionSourceLock )
			{
				if ( this.completionSource == null )
					this.completionSource = new TaskCompletionSource<ProgressUpdate>();

				return this.completionSource.Task;
			}
		}

		public void NotifyUpdate( ProgressUpdate update )
		{
			if ( this.IsComplete || this.IsFailed || this.IsCancelled )
				return;

			this.IsComplete       = update.Type == ProgressUpdate.UpdateType.Completed;
			this.IsCancelled      = update.Type == ProgressUpdate.UpdateType.Cancelled;
			this.IsFailed         = update.Type == ProgressUpdate.UpdateType.Failed;
			this.Progress         = ( update.Progress >= 0 ) ? update.Progress : this.Progress;
			this.Status           = update.Status ?? this.Status;
			this.FailureException = update.FailureCause;
			this.Progress         = MathUtil.Clamp( this.Progress, 0.0, 1.0 );

			lock ( this.completionSourceLock )
			{
				this.completionSource?.TrySetResult( update );
				this.completionSource = null;
			}

			this.ProgressUpdated?.Invoke( this, update );
		}

		public void NotifyFailure( Exception e )
		{
			if ( this.IsComplete || this.IsFailed || this.IsCancelled )
				return;

			this.IsFailed         = true;
			this.FailureException = e;

			lock ( this.completionSourceLock )
			{
				this.completionSource?.TrySetException( e );
				this.completionSource = null;
			}

			this.ProgressUpdated?.Invoke( this, ProgressUpdate.Failure( e ) );
		}
	}
}