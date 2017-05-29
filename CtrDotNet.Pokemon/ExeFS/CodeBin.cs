using System;
using System.IO;
using System.Threading.Tasks;

namespace CtrDotNet.Pokemon.ExeFS
{
	public class CodeBin
	{
		private byte[] data;
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

				return this.data;
			}
			set
			{
				this.data = value;
				this.loaded = ( value != null );
			}
		}

		public async Task Load()
		{
			using ( var fs = new FileStream( this.Path, FileMode.Open, FileAccess.Read, FileShare.Read ) )
			{
				this.data = new byte[ fs.Length ];
				await fs.ReadAsync( this.data, 0, this.data.Length );
			}

			this.loaded = true;
		}

		public async Task Save()
		{
			if ( !this.loaded )
				return;

			using ( var fs = new FileStream( this.Path, FileMode.Create, FileAccess.Write, FileShare.None ) )
				await fs.WriteAsync( this.data, 0, this.data.Length );
		}
	}
}