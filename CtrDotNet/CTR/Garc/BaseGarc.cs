using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CtrDotNet.CTR.Garc
{
	public abstract class BaseGarc : IGarcFile
	{
		public byte[] Data { get; protected set; }
		public GarcDef Def { get; protected set; }
		public int FileCount => this.Def.Fato.EntryCount;

		public virtual Task Read( byte[] data )
		{
			this.Data = data;
			this.Def = GarcUtil.UnpackGarc( data );

			return Task.CompletedTask;
		}

		public virtual Task<byte[]> Write() => Task.FromResult( this.Data );

		public virtual Task<byte[][]> GetFiles()
			=> Task.WhenAll( Enumerable.Range( 0, this.FileCount ).Select( i => this.GetFile( i ) ) );

		public virtual async Task SetFiles( byte[][] files )
		{
			if ( files == null || files.Length != this.FileCount )
				throw new ArgumentException();

			var memGarc = await GarcUtil.PackGarc( files, this.Def.Version, (int) this.Def.ContentPadToNearest );
			this.Def = memGarc.Def;
			this.Data = memGarc.Data;
		}

		public virtual async Task<byte[]> GetFile( int file, int subfile )
		{
			var entry = this.Def.Fatb.Entries[ file ];
			var subEntry = entry.SubEntries[ subfile ];

			if ( !subEntry.Exists )
				throw new ArgumentException( "SubFile does not exist." );

			long offset = subEntry.Start + this.Def.DataOffset;
			byte[] data = new byte[ subEntry.Length ];

			using ( var mainStr = new MemoryStream( this.Data ) )
			{
				mainStr.Seek( offset, SeekOrigin.Begin );
				await mainStr.ReadAsync( data, 0, data.Length );
			}

			return data;
		}

		public Task<byte[]> GetFile( int file ) => this.GetFile( file, 0 );

		public virtual async Task SetFile( int file, byte[] data )
		{
			byte[][] files = await this.GetFiles();
			files[ file ] = data;
			await this.SetFiles( files );
		}

		public virtual async Task SaveFile()
		{
			byte[][] data = new byte[ this.FileCount ][];

			for ( int i = 0; i < data.Length; i++ )
			{
				data[ i ] = await this.GetFile( i );
			}

			var memGarc = await GarcUtil.PackGarc( data, this.Def.Version, (int) this.Def.ContentPadToNearest );
			this.Def = memGarc.Def;
			this.Data = memGarc.Data;
		}

		public virtual Task SaveFileTo( string path ) => this.SaveFile();
	}
}