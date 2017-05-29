using System.Threading.Tasks;
using CtrDotNet.CTR.Garc;
using CtrDotNet.Pokemon.Reference;

namespace CtrDotNet.Pokemon.Garc
{
	public abstract class ReferencedGarc : IGarcFile
	{
		protected ReferencedGarc( GarcReference reference )
		{
			this.Reference = reference;
		}

		public GarcReference Reference { get; }

		public abstract Task<byte[][]> GetFiles();
		public abstract Task<byte[]> GetFile( int file );
		public abstract Task SetFiles( byte[][] files );
		public abstract Task SetFile( int file, byte[] data );
		public abstract Task Save();
	}

	public class ReferencedGarc<T> : ReferencedGarc where T : BaseGarc
	{
		public ReferencedGarc( GarcFile<T> garcFile, GarcReference reference ) : base( reference )
		{
			this.Garc = garcFile;
		}

		public GarcFile<T> Garc { get; }

		public override Task<byte[][]> GetFiles() => this.Garc.GetFiles();
		public override Task<byte[]> GetFile( int file ) => this.Garc.GetFile( file );
		public override Task SetFiles( byte[][] files ) => this.Garc.SetFiles( files );
		public override Task SetFile( int file, byte[] data ) => this.Garc.SetFile( file, data );
		public override Task Save() => this.Garc.Save();
	}
}