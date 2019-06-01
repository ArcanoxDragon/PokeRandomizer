using System;
using System.IO;
using System.Threading.Tasks;
using CtrDotNet.CTR;
using CtrDotNet.Utility;
using IO = System.IO;

namespace PokeRandomizer.Common.ExeFS
{
	public class CodeBin : IWritableFile
	{
		private byte[] buffer;
		private bool loaded;

		internal CodeBin( string path )
		{
			this.Path = path;
		}

		public string Path { get; }

		public byte[] Data
		{
			get
			{
				if ( !this.loaded )
					throw new InvalidOperationException( "File has not been loaded yet" );

				return this.buffer;
			}
			set
			{
				this.buffer = value;
				this.loaded = ( value != null );
			}
		}

		public async Task Load()
		{
			using ( var fs = new FileStream( this.Path, FileMode.Open, FileAccess.Read, FileShare.Read ) )
			{
				this.buffer = new byte[ fs.Length ];
				await fs.ReadAsync( this.buffer, 0, this.buffer.Length );
			}

			this.loaded = true;
		}

		public Task<byte[]> Write() => Task.FromResult( this.Data );

		public async Task SaveFileTo( string path )
		{
			string filename = IO.Path.GetFileName( this.Path ) ?? ""; // literally will never be null, wtf

			if ( filename.StartsWith( "." ) ) // If the file is ".code.bin" change it to "code.bin"
				filename = filename.Substring( 1 );

			string outPath = IO.Path.Combine( path, "ExeFS" );
			byte[] data = this.Data;

			if ( !IO.Directory.Exists( outPath ) )
				IO.Directory.CreateDirectory( outPath );

			using ( var fs = new FileStream( IO.Path.Combine( outPath, filename ), FileMode.Create, FileAccess.Write, FileShare.None ) )
				await fs.WriteAsync( data, 0, data.Length );
		}

		public Task SaveFile() => this.SaveFileTo( PathUtil.GetPathBase( this.Path, "ExeFS" ) );
	}
}