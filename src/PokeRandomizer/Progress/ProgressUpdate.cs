using System;

namespace PokeRandomizer.Progress
{
	public class ProgressUpdate
	{
		#region Static

		private static readonly ProgressUpdate CancelledUpdate = new ProgressUpdate( ProgressUpdate.UpdateType.Cancelled );
		private static readonly ProgressUpdate CompletedUpdate = new ProgressUpdate( ProgressUpdate.UpdateType.Completed );

		public enum UpdateType
		{
			Failed = -1,
			Status,
			Cancelled,
			Completed
		}

		public static ProgressUpdate StatusOnly( string message ) => new ProgressUpdate( UpdateType.Status, message );
		public static ProgressUpdate Update( string message, double progress ) => new ProgressUpdate( UpdateType.Status, message, progress );
		public static ProgressUpdate Failure( Exception ex ) => new ProgressUpdate( ex );
		public static ProgressUpdate Cancelled() => CancelledUpdate;
		public static ProgressUpdate Completed() => CompletedUpdate;

		#endregion

		private ProgressUpdate( UpdateType type, string status = null, double progress = -1 )
		{
			if ( status == null && type == UpdateType.Status )
				throw new ArgumentNullException( nameof(status), "Status cannot be null for a progress update" );

			this.Type = type;
			this.Status = status;
			this.Progress = progress;
		}

		private ProgressUpdate( Exception exception )
		{
			this.Type = UpdateType.Failed;
			this.FailureCause = exception;
		}

		public UpdateType Type { get; }
		public Exception FailureCause { get; }
		public string Status { get; }
		public double Progress { get; }
	}
}