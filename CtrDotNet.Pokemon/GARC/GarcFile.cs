using System;
using System.IO;
using System.Threading.Tasks;
using CtrDotNet.CTR;

namespace CtrDotNet.Pokemon.GARC
{
	public class GarcFile
	{
		private readonly Garc.MemGarc garc;
		private readonly GarcReference reference;
		private readonly string path;

		public GarcFile( Garc.MemGarc g, GarcReference r, string p )
		{
			this.garc = g;
			this.reference = r;
			this.path = p;
		}

		// Shorthand Alias
		public byte[] GetFile( int file, int subfile = 0 )
		{
			return this.garc.GetFile( file, subfile );
		}

		public byte[][] Files
		{
			get => this.garc.Files;
			set => this.garc.Files = value;
		}

		public int FileCount => this.garc.FileCount;

		public void Save()
		{
			File.WriteAllBytes( this.path, this.garc.Data );
			Console.WriteLine( $"Wrote {this.reference.Name} to {this.reference.Reference}" );
		}
	}

	public class LZGarcFile
	{
		private readonly Garc.LzGarc garc;
		private readonly GarcReference reference;
		private readonly string path;

		public LZGarcFile( Garc.LzGarc g, GarcReference r, string p )
		{
			this.garc = g;
			this.reference = r;
			this.path = p;
		}

		public int FileCount => this.garc.FileCount;

		public byte[][] Files
		{
			get
			{
				byte[][] data = new byte[ this.FileCount ][];
				for ( int i = 0; i < data.Length; i++ )
					data[ i ] = this.garc[ i ];
				return data;
			}
			set
			{
				for ( int i = 0; i < value.Length; i++ )
					this.garc[ i ] = value[ i ];
			}
		}

		public byte[] this[ int file ]
		{
			get => this.garc[ file ];
			set => this.garc[ file ] = value;
		}

		public async Task Save()
		{
			using ( var fs = new FileStream( this.path, FileMode.Create, FileAccess.Write ) )
			{
				byte[] data = await this.garc.Save();
				await fs.WriteAsync( data, 0, data.Length );
				Console.WriteLine( $"Wrote {this.reference.Name} to {this.reference.Reference}" );
			}
		}
	}
}