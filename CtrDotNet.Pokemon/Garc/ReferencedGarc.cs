using System.IO;
using System.Threading.Tasks;
using CtrDotNet.CTR.Garc;
using CtrDotNet.Pokemon.Reference;
using CtrDotNet.Utility;

namespace CtrDotNet.Pokemon.Garc
{
	public class ReferencedGarc : IGarcFile
	{
		public ReferencedGarc( GarcFile garc, GarcReference reference )
		{
			this.Garc = garc;
			this.Reference = reference;
		}

		public GarcFile Garc { get; }
		public GarcReference Reference { get; }

		public Task<byte[][]> GetFiles() => this.Garc.GetFiles();
		public Task<byte[]> GetFile( int file ) => this.Garc.GetFile( file );
		public Task SetFiles( byte[][] files ) => this.Garc.SetFiles( files );
		public Task SetFile( int file, byte[] data ) => this.Garc.SetFile( file, data );
		public Task<byte[]> Write() => this.Garc.Write();
		public Task SaveFile() => this.SaveFileTo( PathUtil.GetPathBase( this.Garc.Path, this.Reference.RomFsPath ) );

		public Task SaveFileTo( string path )
		{
			string dirName = Path.GetDirectoryName( this.Reference.RomFsPath );
			string outPath = Path.Combine( path, "RomFS", dirName );

			if ( !Directory.Exists( outPath ) )
				Directory.CreateDirectory( outPath );

			return this.Garc.SaveFileTo( outPath );
		}
	}
}