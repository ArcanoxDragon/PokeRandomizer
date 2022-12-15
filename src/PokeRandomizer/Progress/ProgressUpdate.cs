using System;

namespace PokeRandomizer.Progress
{
	public class ProgressUpdate
	{
		#region Static

		private static readonly ProgressUpdate CancelledUpdate = new(UpdateType.Cancelled);
		private static readonly ProgressUpdate CompletedUpdate = new(UpdateType.Completed);

		public enum UpdateType
		{
			Failed = -1,
			Status,
			Cancelled,
			Completed
		}

		public static ProgressUpdate StatusOnly(string message) => new(UpdateType.Status, message);
		public static ProgressUpdate Update(string message, double progress) => new(UpdateType.Status, message, progress);
		public static ProgressUpdate Failure(Exception ex) => new(ex);
		public static ProgressUpdate Cancelled() => CancelledUpdate;
		public static ProgressUpdate Completed() => CompletedUpdate;

		#endregion

		private ProgressUpdate(UpdateType type, string status = null, double progress = -1)
		{
			if (status == null && type == UpdateType.Status)
				throw new ArgumentNullException(nameof(status), "Status cannot be null for a progress update");

			Type = type;
			Status = status;
			Progress = progress;
		}

		private ProgressUpdate(Exception exception)
		{
			Type = UpdateType.Failed;
			FailureCause = exception;
		}

		public UpdateType Type         { get; }
		public Exception  FailureCause { get; }
		public string     Status       { get; }
		public double     Progress     { get; }
	}
}