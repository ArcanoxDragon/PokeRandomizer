using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrDotNet.CTR.Garc
{
	public class GarcFile<T> where T : BaseGarc
	{
		private readonly T garc;
		private readonly string path;

		public GarcFile( T g, string p )
		{
			this.garc = g;
			this.path = p;
		}

		// Shorthand Alias
		public async Task<byte[]> GetFile( int file ) => ( await this.GetFiles() )[ file ];

		public Task<byte[][]> GetFiles() => this.garc.GetFiles();
		public void SetFiles( byte[][] files ) => this.garc.SetFiles( files );

		public int FileCount => this.garc.FileCount;

		public async Task Save()
		{
			using ( var fs = new FileStream( this.path, FileMode.Create, FileAccess.Write, FileShare.None ) )
			{
				byte[] data = await this.garc.Save();
				await fs.WriteAsync( data, 0, data.Length );
			}
		}
	}
}