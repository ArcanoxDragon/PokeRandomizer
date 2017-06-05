using System;
using IO = System.IO;
using System.Threading.Tasks;
using CtrDotNet.CTR;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Cro
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

		#endregion

		private byte[] buffer;

		public CroFile( string path )
		{
			this.Path = path;
		}

		public byte[] Data => this.SafeGetBuffer();
		public string Path { get; }
		public uint CodeOffset => BitConverter.ToUInt32( this.SafeGetBuffer(), PointerCodeOffset );
		public uint CodeSize => BitConverter.ToUInt32( this.SafeGetBuffer(), PointerCodeSize );
		public uint DataOffset => BitConverter.ToUInt32( this.SafeGetBuffer(), PointerDataOffset );
		public uint DataSize => BitConverter.ToUInt32( this.SafeGetBuffer(), PointerDataSize );

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
		}

		public Task<byte[]> Write() => Task.FromResult( this.Data );

		public Task SaveFile() => this.SaveFileTo( this.Path );

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

			Assertions.AssertLength( this.DataOffset + this.DataSize, data );

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

		public Task<byte[]> GetCodeSection()
			=> this.GetSection( this.CodeOffset, this.CodeSize );

		public Task WriteCodeSection( byte[] section )
			=> this.WriteSection( PointerCodeOffset, PointerCodeSize, this.CodeOffset, this.CodeSize, section );

		public Task<byte[]> GetDataSection()
			=> this.GetSection( this.DataOffset, this.DataSize );

		public Task WriteDataSection( byte[] section )
			=> this.WriteSection( PointerDataOffset, PointerDataSize, this.DataOffset, this.DataSize, section );
	}
}