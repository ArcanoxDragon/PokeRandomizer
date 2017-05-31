using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CtrDotNet.CTR.Garc
{
	public class LzGarc : BaseGarc
	{
		#region Static

		private class Entry
		{
			public bool Accessed { get; set; }
			public bool Saved { get; set; }
			public byte[] Data { get; set; }
			public bool WasCompressed { get; set; }

			public Entry() { }

			public async Task Read( byte[] data )
			{
				this.Data = data;
				this.Accessed = true;

				if ( data.Length == 0 )
					return;

				if ( data[ 0 ] != 0x11 )
					return;

				try
				{
					using ( var msCompressed = new MemoryStream( data ) )
					using ( var msDecompressed = new MemoryStream() )
					{
						await Lzss.Decompress( msCompressed, data.Length, msDecompressed );
						this.Data = msDecompressed.ToArray();
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
			if ( this.storage[ fileIndex ] == null )
			{
				this.storage[ fileIndex ] = new Entry();
				await this.storage[ fileIndex ].Read( await base.GetFile( fileIndex ) );
			}

			return this.storage[ fileIndex ].Data;
		}

		public override Task<byte[][]> GetFiles()
			=> Task.WhenAll( Enumerable.Range( 0, this.FileCount ).Select( i => this.GetFile( i ) ) );

		public override async Task SetFiles( byte[][] files )
		{
			if ( files.Length != this.FileCount )
				throw new NotSupportedException( "Cannot change number of entries" );

			for ( int i = 0; i < files.Length; i++ )
			{
				if ( this.storage[ i ] == null )
					await this.GetFile( i );

				// ReSharper disable once PossibleNullReferenceException
				this.storage[ i ].Data = files[ i ];
			}

			await base.SetFiles( await Task.WhenAll( this.storage.Select( e => e.Save() ) ) );
		}
	}
}