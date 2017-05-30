using System.IO;
using System.Threading.Tasks;

namespace CtrDotNet.CTR.Garc
{
	public class GarcFile : IGarcFile
	{
		#region Static

		public static async Task<GarcFile> FromFile( string path, bool useLz = false )
		{
			using ( var fs = new FileStream( path, FileMode.Open, FileAccess.Read, FileShare.Read ) )
			{
				byte[] buffer = new byte[ fs.Length ];
				await fs.ReadAsync( buffer, 0, buffer.Length );
				GarcFile file = new GarcFile( path, useLz );
				file.Read( buffer );
				return file;
			}
		}

		#endregion

		internal GarcFile( string p, bool useLz = false )
		{
			if ( useLz )
				this.GarcData = new LzGarc();
			else
				this.GarcData = new MemGarc();

			this.Path = p;
		}

		public int FileCount => this.GarcData.FileCount;
		public BaseGarc GarcData { get; }
		public string Path { get; }

		public void Read( byte[] data ) => this.GarcData.Read( data );

		public Task<byte[][]> GetFiles() => this.GarcData.GetFiles();

		public async Task<byte[]> GetFile( int file ) => ( await this.GetFiles() )[ file ];

		public Task SetFiles( byte[][] files ) => this.GarcData.SetFiles( files );

		public async Task SetFile( int file, byte[] data )
		{
			byte[][] files = await this.GetFiles();
			files[ file ] = data;
			await this.SetFiles( files );
		}

		public async Task Save()
		{
			using ( var fs = new FileStream( this.Path, FileMode.Create, FileAccess.Write, FileShare.None ) )
			{
				byte[] data = await this.GarcData.Save();
				await fs.WriteAsync( data, 0, data.Length );
			}
		}
	}
}