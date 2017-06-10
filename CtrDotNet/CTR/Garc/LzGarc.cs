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
                    using ( MemoryStream oldMS = new MemoryStream( this.Data ) )
                    using ( MemoryStream newMS = new MemoryStream() )
                    {
                        await Lzss.Compress( oldMS, this.Data.Length, newMS, original: true );
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

        public override bool? IsFileCompressed( int file ) => this.storage[ file ]?.WasCompressed;

        public override async Task Read( byte[] data )
        {
            await base.Read( data );
            this.storage = new Entry[ this.FileCount ];

            for ( int i = 0; i < this.FileCount; i++ )
            {
                this.storage[ i ] = new Entry();
                await this.storage[ i ].Read( await base.GetFile( i, 0 ) );
            }
        }

        public override Task<byte[]> GetFile( int fileIndex, int subfileIndex )
            => Task.FromResult( this.storage[ fileIndex ].Data );

        public override Task<byte[][]> GetFiles()
            => Task.WhenAll( Enumerable.Range( 0, this.FileCount ).Select( i => this.GetFile( i ) ) );

        public override async Task SetFiles( byte[][] files )
        {
            if ( files.Length != this.FileCount )
                throw new NotSupportedException( "Cannot change number of entries" );

            for ( int i = 0; i < files.Length; i++ )
            {
                if ( this.storage[ i ] == null )
                    this.storage[ i ] = new Entry();

                this.storage[ i ].Data = files[ i ];
            }

            await base.SetFiles( await Task.WhenAll( this.storage.Select( e => e.Save() ) ) );
        }

        public override async Task SetFile( int file, byte[] data )
        {
            if ( file < 0 || file >= this.FileCount )
                throw new ArgumentOutOfRangeException( nameof(file), "File index not valid" );

            if ( this.storage[ file ] == null )
                await this.GetFile( file );

            // ReSharper disable once PossibleNullReferenceException
            this.storage[ file ].Data = data;

            await base.SetFile( file, await this.storage[ file ].Save() );
        }

        public override async Task SaveFile()
        {
            byte[][] data = await Task.WhenAll( this.storage.Select( ( s, i ) => s.Save() ) );

            var memGarc = await GarcUtil.PackGarc( data, this.Def.Version, (int) this.Def.ContentPadToNearest );
            this.Def = memGarc.Def;
            this.Data = memGarc.Data;
        }
    }
}