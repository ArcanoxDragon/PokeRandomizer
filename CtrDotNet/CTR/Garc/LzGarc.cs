using System;
using System.IO;
using System.Threading.Tasks;

namespace CtrDotNet.CTR.Garc
{
	/// <summary>
	/// GARC Class that is heavier on OOP to allow for compression tracking for faster edits
	/// </summary>
	public class LzGarc : BaseGarc
	{
		#region Static

		private class Entry
		{
			public bool Accessed { get; }
			public bool Saved { get; set; }
			public byte[] Data { get; set; }
			public bool WasCompressed { get; }

			public Entry() { }

			public Entry( byte[] data )
			{
				this.Data = data;
				this.Accessed = true;

				if ( data.Length == 0 )
					return;

				if ( data[ 0 ] != 0x11 )
					return;

				try
				{
					using ( MemoryStream newMS = new MemoryStream() )
					{
						Lzss.Decompress( new MemoryStream( data ), data.Length, newMS );
						this.Data = newMS.ToArray();
					}
					this.WasCompressed = true;
				}
				catch
				{
					// ignored
				}
			}

			public async Task<byte[]> Save()
			{
				if ( !this.WasCompressed )
					return this.Data;

				byte[] data;
				try
				{
					using ( MemoryStream newMS = new MemoryStream() )
					{
						await Lzss.Compress( new MemoryStream( this.Data ), this.Data.Length, newMS, original: true );
						data = newMS.ToArray();
					}
				}
				catch
				{
					data = new byte[ 0 ];
				}
				return data;
			}
		}

		#endregion

		private Entry[] storage;

		internal LzGarc() { }

		public override void Read( byte[] data )
		{
			base.Read( data );
			this.storage = new Entry[ this.FileCount ];
		}

		protected override async Task<byte[]> GetFile( int fileIndex, int subfileIndex = 0 )
		{
			if ( this.storage[ fileIndex ] == null || !this.storage[ fileIndex ].Saved ) // retrieve original
				return await base.GetFile( fileIndex, subfileIndex );

			// use modified
			return await this.storage[ fileIndex ].Save();
		}

		//public byte[] this[ int file ]
		//{
		public override async Task<byte[][]> GetFiles()
		{
			byte[][] files = new byte[ this.FileCount ][];

			for ( int i = 0; i < this.FileCount; i++ )
			{
				if ( i > this.FileCount )
					throw new ArgumentException();

				if ( this.storage[ i ] == null )
					this.storage[ i ] = new Entry( await this.GetFile( i ) );

				files[ i ] = this.storage[ i ].Data;
			}

			return files;
		}

		public override Task SetFiles( byte[][] files )
		{
			for ( int file = 0; file < files.Length; file++ )
			{
				if ( this.storage[ file ] == null )
					this.storage[ file ] = new Entry( files[ file ] );

				this.storage[ file ].Data = files[ file ];
				this.storage[ file ].Saved = true;
			}

			return Task.CompletedTask;
		}

		//}
	}
}