using System;
using System.Threading.Tasks;
using PokeRandomizer.Utility;

namespace PokeRandomizer.Progress
{
	public class ProgressNotifier
	{
		public delegate void ProgressUpdatedEvent(ProgressNotifier sender, ProgressUpdate update);

		public event ProgressUpdatedEvent ProgressUpdated;

		private readonly object completionSourceLock = new();

		private TaskCompletionSource<ProgressUpdate> completionSource;

		public Exception FailureException { get; private set; }
		public bool      IsComplete       { get; private set; }
		public bool      IsCancelled      { get; private set; }
		public bool      IsFailed         { get; private set; }
		public double    Progress         { get; private set; }
		public string    Status           { get; private set; }

		public Task<ProgressUpdate> AwaitUpdate()
		{
			if (IsFailed)
				return Task.FromException<ProgressUpdate>(FailureException);

			if (IsComplete)
				return Task.FromResult(ProgressUpdate.Completed());

			if (IsComplete)
				return Task.FromResult(ProgressUpdate.Cancelled());

			lock (this.completionSourceLock)
			{
				this.completionSource ??= new TaskCompletionSource<ProgressUpdate>();

				return this.completionSource.Task;
			}
		}

		public void NotifyUpdate(ProgressUpdate update)
		{
			if (IsComplete || IsFailed || IsCancelled)
				return;

			IsComplete = update.Type == ProgressUpdate.UpdateType.Completed;
			IsCancelled = update.Type == ProgressUpdate.UpdateType.Cancelled;
			IsFailed = update.Type == ProgressUpdate.UpdateType.Failed;
			Progress = ( update.Progress >= 0 ) ? update.Progress : Progress;
			Status = update.Status ?? Status;
			FailureException = update.FailureCause;
			Progress = MathUtil.Clamp(Progress, 0.0, 1.0);

			lock (this.completionSourceLock)
			{
				this.completionSource?.TrySetResult(update);
				this.completionSource = null;
			}

			ProgressUpdated?.Invoke(this, update);
		}

		public void NotifyFailure(Exception e)
		{
			if (IsComplete || IsFailed || IsCancelled)
				return;

			IsFailed = true;
			FailureException = e;

			lock (this.completionSourceLock)
			{
				this.completionSource?.TrySetException(e);
				this.completionSource = null;
			}

			ProgressUpdated?.Invoke(this, ProgressUpdate.Failure(e));
		}
	}
}