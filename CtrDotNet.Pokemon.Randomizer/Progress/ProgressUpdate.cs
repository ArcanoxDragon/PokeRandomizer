namespace CtrDotNet.Pokemon.Randomization.Progress
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
		public static ProgressUpdate ProgressOnly( double progress ) => new ProgressUpdate( UpdateType.Status, progress: progress );
		public static ProgressUpdate Update( string message, double progress ) => new ProgressUpdate( UpdateType.Status, message, progress );
		public static ProgressUpdate Cancelled() => CancelledUpdate;
		public static ProgressUpdate Completed() => CompletedUpdate;

		#endregion

		public ProgressUpdate( UpdateType type, string status = null, double progress = -1 )
		{
			this.Type = type;
			this.Status = status;
			this.Progress = progress;
		}

		public UpdateType Type { get; }
		public string Status { get; set; }
		public double Progress { get; set; }
	}
}