using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CtrDotNet.Utility;
using IO = System.IO;

namespace CtrDotNet.CTR.Cro
{
    public class CroFile : IWritableFile
    {
        #region Static

        public static async Task<CroFile> FromFile( string path )
        {
            using ( var fs = new IO.FileStream( path, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read ) )
            {
                byte[] buffer = new byte[ fs.Length ];
                await fs.ReadAsync( buffer, 0, buffer.Length );
                CroFile file = new CroFile( path );
                file.Read( buffer );
                return file;
            }
        }

        private const int SizeOfPointer = 0x04;
        private const int SizeOfHeader = 0x134 + SizeOfPointer;
        private const int PointerCodeOffset = 0xB0;
        private const int PointerCodeSize = 0xB4;
        private const int PointerDataOffset = 0xB8;
        private const int PointerDataSize = 0xBC;

        public enum SectionType
        {
            Code,
            Data
        }

        public struct Section
        {
            public uint Size;
            public uint Offset;
            public int SizePointer;
            public int OffsetPointer;
        }

        #endregion

        private byte[] buffer;
        private Dictionary<SectionType, Section> sections;

        public CroFile( string path )
        {
            this.Path = path;
        }

        public byte[] Data => this.SafeGetBuffer();
        public string Path { get; }
        public IReadOnlyDictionary<SectionType, Section> Sections => this.sections;

//		public uint CodeOffset => BitConverter.ToUInt32( this.SafeGetBuffer(), PointerCodeOffset );
//		public uint CodeSize => BitConverter.ToUInt32( this.SafeGetBuffer(), PointerCodeSize );
//		public uint DataOffset => BitConverter.ToUInt32( this.SafeGetBuffer(), PointerDataOffset );
//		public uint DataSize => BitConverter.ToUInt32( this.SafeGetBuffer(), PointerDataSize );

        private byte[] SafeGetBuffer()
        {
            if ( this.buffer == null )
                throw new InvalidOperationException( "Data cannot be read before the file is loaded" );

            return this.buffer;
        }

        public void Read( byte[] file )
        {
            // Make sure the file contains at least a full header
            Assertions.AssertLength( SizeOfHeader, file );

            this.buffer = file;

            this.sections = new Dictionary<SectionType, Section> {
                {
                    SectionType.Code,
                    new Section {
                        SizePointer = PointerCodeSize,
                        OffsetPointer = PointerCodeOffset,
                        Size = BitConverter.ToUInt32( file, PointerCodeSize ),
                        Offset = BitConverter.ToUInt32( file, PointerCodeOffset )
                    }
                }, {
                    SectionType.Data,
                    new Section {
                        SizePointer = PointerDataSize,
                        OffsetPointer = PointerDataOffset,
                        Size = BitConverter.ToUInt32( file, PointerDataSize ),
                        Offset = BitConverter.ToUInt32( file, PointerDataOffset )
                    }
                }
            };
        }

        public Task<byte[]> Write() => Task.FromResult( this.Data );

        public Task SaveFile() => this.SaveFileTo( PathUtil.GetPathBase( this.Path, "RomFS" ) );

        public async Task SaveFileTo( string path )
        {
            string filename = IO.Path.GetFileName( this.Path );
            string outPath = IO.Path.Combine( path, "RomFS" );
            byte[] data = this.SafeGetBuffer();

            if ( !IO.Directory.Exists( outPath ) )
                IO.Directory.CreateDirectory( outPath );

            using ( var fs = new IO.FileStream( IO.Path.Combine( outPath, filename ), IO.FileMode.Create, IO.FileAccess.Write, IO.FileShare.None ) )
                await fs.WriteAsync( data, 0, data.Length );
        }

        private async Task<byte[]> GetSection( uint offset, uint size )
        {
            byte[] data = this.SafeGetBuffer();

            using ( var ms = new IO.MemoryStream() )
            {
                await ms.WriteAsync( data, (int) offset, (int) size );
                return ms.ToArray();
            }
        }

        private async Task WriteSection( int offsetPointer, int sizePointer, uint offset, uint size, byte[] section )
        {
            byte[] data = this.SafeGetBuffer();
            uint oldSize = size;

            using ( var ms = new IO.MemoryStream() )
            using ( var bw = new IO.BinaryWriter( ms ) )
            {
                // Write up to offset pointer
                await ms.WriteAsync( data, 0, offsetPointer );

                // Write new section coordinates
                bw.Write( offset );
                bw.Write( (uint) section.Length );

                // Write up to beginning of section
                await ms.WriteAsync( data, sizePointer + SizeOfPointer, (int) ( offset - ( sizePointer + SizeOfPointer ) ) );

                // Write new section
                await ms.WriteAsync( section, 0, section.Length );

                // Write stuff after old section
                await ms.WriteAsync( data, (int) ( offset + oldSize ), (int) ( data.Length - ( offset + oldSize ) ) );

                this.buffer = ms.ToArray();
            }
        }

        public Task<byte[]> GetSection( SectionType section )
        {
            if ( !this.sections.ContainsKey( section ) )
                throw new NotSupportedException( $"Unknown section {section}" );

            var sec = this.sections[ section ];
            return this.GetSection( sec.Offset, sec.Size );
        }

        public Task WriteSection( SectionType section, byte[] data )
        {
            if ( !this.sections.ContainsKey( section ) )
                throw new NotSupportedException( $"Unknown section {section}" );

            var sec = this.sections[ section ];
            return this.WriteSection( sec.OffsetPointer, sec.SizePointer, sec.Offset, sec.Size, data );
        }

        public Task<byte[]> GetCodeSection()
            => this.GetSection( SectionType.Code );

        public Task WriteCodeSection( byte[] section )
            => this.WriteSection( SectionType.Code, section );

        public Task<byte[]> GetDataSection()
            => this.GetSection( SectionType.Data );

        public Task WriteDataSection( byte[] section )
            => this.WriteSection( SectionType.Data, section );
    }
}