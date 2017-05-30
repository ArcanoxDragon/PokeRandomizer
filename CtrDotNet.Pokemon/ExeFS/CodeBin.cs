using System;
using IO = System.IO;
using System.IO;
using System.Threading.Tasks;
using CtrDotNet.CTR;

namespace CtrDotNet.Pokemon.ExeFS
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
			if ( IO.Path.GetExtension( path )?.ToLower() != ".bin" )
				path = IO.Path.Combine( path, IO.Path.GetFileName( this.Path ) );

			byte[] data = this.Data;

			using ( var fs = new FileStream( path, FileMode.Create, FileAccess.Write, FileShare.None ) )
				await fs.WriteAsync( data, 0, data.Length );
		}

		public Task SaveFile() => this.SaveFileTo( this.Path );
	}
}