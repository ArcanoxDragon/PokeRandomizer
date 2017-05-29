using System.IO;
using System.Threading.Tasks;

namespace CtrDotNet.CTR.Garc
{
	public class GarcFile<T> : IGarcFile where T : BaseGarc
	{
		public GarcFile( T g, string p )
		{
			this.GarcData = g;
			this.Path = p;
		}

		public int FileCount => this.GarcData.FileCount;
		public T GarcData { get; }
		public string Path { get; }

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