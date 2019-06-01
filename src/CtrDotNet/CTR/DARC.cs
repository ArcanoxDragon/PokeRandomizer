using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CtrDotNet.CTR
{
	public class Darc
	{
		public readonly DarcHeader Header;
		public byte[] Data;
		public FileTableEntry[] Entries;
		public NameTableEntry[] FileNameTable;

		public Darc( byte[] data = null )
		{
			if ( data == null )
				return;
			using ( BinaryReader br = new BinaryReader( new MemoryStream( data ) ) )
				try
				{
					this.Header = new DarcHeader( br );
					br.BaseStream.Position = this.Header.FileTableOffset;
					FileTableEntry root = new FileTableEntry( br );
					this.Entries = new FileTableEntry[ root.DataLength ];
					this.Entries[ 0 ] = root;
					for ( int i = 1; i < root.DataLength; i++ )
						this.Entries[ i ] = new FileTableEntry( br );
					this.FileNameTable = new NameTableEntry[ root.DataLength ];
					uint offs = 0;
					for ( int i = 0; i < root.DataLength; i++ )
					{
						char c;
						string s = string.Empty;
						while ( ( c = (char) br.ReadUInt16() ) > 0 )
							s += c;

						this.FileNameTable[ i ] = new NameTableEntry( offs, s );
						offs += (uint) s.Length * 2 + 2;
					}
					br.BaseStream.Position = this.Header.FileDataOffset;
					this.Data = br.ReadBytes( (int) ( this.Header.FileSize - this.Header.FileDataOffset ) );
				}
				catch ( Exception )
				{
					br.Close();
				}
		}

		public class DarcHeader
		{
			public DarcHeader( BinaryReader br = null )
			{
				if ( br == null )
					return;
				this.Signature = new string( br.ReadChars( 4 ) );
				if ( this.Signature != "darc" )
					throw new Exception( this.Signature );
				this.Endianness = br.ReadUInt16();
				this.HeaderSize = br.ReadUInt16();
				this.Version = br.ReadUInt32();
				this.FileSize = br.ReadUInt32();
				this.FileTableOffset = br.ReadUInt32();
				this.FileTableLength = br.ReadUInt32();
				this.FileDataOffset = br.ReadUInt32();
			}

			public string Signature;
			public ushort Endianness;
			public ushort HeaderSize;
			public uint Version;
			public uint FileSize;
			public uint FileTableOffset;
			public uint FileTableLength;
			public uint FileDataOffset;
		}

		public class FileTableEntry
		{
			public FileTableEntry( BinaryReader br = null )
			{
				if ( br == null )
					return;
				this.NameOffset = br.ReadUInt32();
				this.IsFolder = this.NameOffset >> 24 == 1;
				this.NameOffset &= 0xFFFFFF;
				this.DataOffset = br.ReadUInt32();
				this.DataLength = br.ReadUInt32();
			}

			public uint NameOffset;
			public bool IsFolder;
			public uint DataOffset; // FOLDER: Parent Entry Index
			public uint DataLength; // FOLDER: Next Folder Index
		}

		public class NameTableEntry
		{
			public uint NameOffset { get; }
			public string FileName { get; }

			public NameTableEntry( uint offset, string fileName )
			{
				this.NameOffset = offset;
				this.FileName = fileName;
			}
		}

		// DARC r/w
		internal static byte[] SetDarc( Darc darc )
		{
			// Package DARC into a writable array.
			using ( MemoryStream ms = new MemoryStream() )
			using ( BinaryWriter bw = new BinaryWriter( ms ) )
			{
				// Write Header
				bw.Write( Encoding.ASCII.GetBytes( darc.Header.Signature ) );
				bw.Write( darc.Header.Endianness );
				bw.Write( darc.Header.HeaderSize );
				bw.Write( darc.Header.Version );
				bw.Write( darc.Header.FileSize );
				bw.Write( darc.Header.FileTableOffset );
				bw.Write( darc.Header.FileTableLength );
				bw.Write( darc.Header.FileDataOffset );
				// Write FileTableEntries
				foreach ( FileTableEntry entry in darc.Entries )
				{
					bw.Write( entry.NameOffset | ( entry.IsFolder
													   ? (uint) 1 << 24
													   : 0 ) );
					bw.Write( entry.DataOffset );
					bw.Write( entry.DataLength );
				}
				foreach ( NameTableEntry entry in darc.FileNameTable )
				{
					bw.Write( Encoding.Unicode.GetBytes( entry.FileName + "\0" ) );
				}
				while ( bw.BaseStream.Position < darc.Header.FileDataOffset )
					bw.Write( (byte) 0 );

				// Write Data
				bw.Write( darc.Data );

				return ms.ToArray();
			}
		}

		internal static Darc GetDarc( string folderName )
		{
			// Package Folder into a DARC.
			List<FileTableEntry> entryList = new List<FileTableEntry>();
			List<NameTableEntry> nameList = new List<NameTableEntry>();
			byte[] data = new byte[ 0 ];
			uint nameOffset = 6; // 00 00 + 00 2E 00 00

			#region Build FileTable/NameTables

			{
				// Null First File
				{
					entryList.Add( new FileTableEntry { DataOffset = 0, DataLength = 0, IsFolder = true, NameOffset = 0 } );
					nameList.Add( new NameTableEntry( 0, "" ) );
				}
				// "." Second File
				{
					entryList.Add( new FileTableEntry { DataOffset = 0, DataLength = 0, IsFolder = true, NameOffset = 2 } );
					nameList.Add( new NameTableEntry( 6, "." ) );
				}
				foreach ( string folder in Directory.GetDirectories( folderName ) )
				{
					string parentName = new DirectoryInfo( folder ).Name;
					string[] files = Directory.GetFiles( folder );
					nameList.Add( new NameTableEntry( nameOffset, parentName ) );
					entryList.Add( new FileTableEntry {
						DataOffset = 1,
						DataLength = (uint) ( files.Length + entryList.Count ),
						IsFolder = true,
						NameOffset = nameOffset
					} );
					nameOffset += (uint) parentName.Length + 2; // Account for null terminator

					foreach ( string file in files )
					{
						FileInfo fi = new FileInfo( file );
						string fileName = fi.Name;
						nameList.Add( new NameTableEntry( nameOffset, parentName ) );

						entryList.Add( new FileTableEntry {
							DataOffset = (uint) data.Length,
							DataLength = (uint) fi.Length,
							IsFolder = false,
							NameOffset = nameOffset
						} );
						data = data.Concat( File.ReadAllBytes( file ) ).ToArray();
						nameOffset += (uint) fileName.Length + 2; // Account for null terminator
					}
				}
			}

			#endregion

			// Compute Necessary DARC information
			int darcFileCount = nameList.Count;
			int nameListOffset = darcFileCount * 0xC;
			int nameListLength = (int) ( nameOffset + nameListOffset );
			int dataOffset = nameListLength % 4 == 0
								 ? nameListLength
								 : nameListLength + ( 4 - nameListLength % 4 );
			Array.Resize( ref data, data.Length % 4 == 0
										? data.Length
										: data.Length + 4 - data.Length % 4 );
			int finalSize = dataOffset + data.Length;

			// Create New DARC
			Darc darc = new Darc {
				Header = {
					Signature = "darc",
					Endianness = 0xFEFF,
					HeaderSize = 0x1C,
					Version = 1,
					FileSize = (uint) finalSize,
					FileTableOffset = 0x1C,
					FileTableLength = (uint) nameListLength,
					FileDataOffset = (uint) dataOffset,
				},
				Entries = entryList.ToArray(),
				FileNameTable = nameList.ToArray(),
				Data = data,
			};
			// Fix the First two folders to specify the number of files
			darc.Entries[ 0 ].DataLength = (uint) darcFileCount;
			darc.Entries[ 1 ].DataLength = (uint) darcFileCount;

			// Fix the Data Offset of the files to point to actual destination
			foreach ( FileTableEntry f in darc.Entries.Where( x => !x.IsFolder ) )
				f.DataOffset += darc.Header.FileDataOffset;
			return darc;
		}

		internal static bool Darc2Files( string path, string folderName )
		{
			try
			{
				return Darc.Darc2Files( File.ReadAllBytes( path ), folderName );
			}
			catch ( Exception )
			{
				return false;
			}
		}

		internal static bool Darc2Files( byte[] darcData, string folderName )
		{
			// Save all contents of a DARC to a folder, assuming there's only 1 layer of folders.
			try
			{
				// Clear existing contents
				string root = folderName;
				if ( Directory.Exists( root ) )
					Directory.Delete( root, true );

				// Create new DARC object from input data
				Darc darcFile = new Darc( darcData );

				// Output data
				for ( int i = 2; i < darcFile.FileNameTable.Length; )
				{
					bool isFolder = darcFile.Entries[ i ].IsFolder;
					if ( !isFolder )
						return false;
					// uint level = DARC.Entries[i].DataOffset; Only assuming 1 layer of folders.
					string parentName = darcFile.FileNameTable[ i ].FileName;
					Directory.CreateDirectory( Path.Combine( root, parentName ) );

					int nextFolder = (int) darcFile.Entries[ i++ ].DataLength;

					// Extract all Contents of said folder
					while ( i < nextFolder )
					{
						string fileName = darcFile.FileNameTable[ i ].FileName;
						int offset = (int) darcFile.Entries[ i ].DataOffset;
						int length = (int) darcFile.Entries[ i ].DataLength;
						byte[] data = darcFile.Data.Skip( (int) ( offset - darcFile.Header.FileDataOffset ) ).Take( length ).ToArray();

						string outPath = Path.Combine( root, parentName, fileName );
						File.WriteAllBytes( outPath, data );
						i++; // Advance to next Entry
					}
				}
				return true;
			}
			catch ( Exception )
			{
				return false;
			}
		}

		internal static bool Files2Darc( string folderName, bool delete = false, string originalDarc = null, string outFile = null )
		{
			// Save all contents of a folder to a darc.
			try
			{
				byte[] darcData;
				Darc orig;
				string root = folderName;
				if ( originalDarc != null )
				{
					// Fetch offset of DARC within file.
					byte[] darc = File.ReadAllBytes( originalDarc );
					int darcPos = Darc.GetDarCposition( darc );
					if ( darcPos < 0 )
						return false;
					byte[] origData = darc.Skip( darcPos ).ToArray();

					orig = new Darc( origData );
					orig = Darc.InsertFiles( orig, folderName );
					byte[] newDarc = Darc.SetDarc( orig );
					darcData = darc.Take( darcPos ).Concat( newDarc ).ToArray();
				}
				else // no existing darc to get
				{
					orig = Darc.GetDarc( folderName );
					darcData = Darc.SetDarc( orig );
				}

				// Fetch final name if not specified
				outFile = outFile ?? originalDarc ?? new DirectoryInfo( folderName ).Name.Replace( "_d", "" ) + ".darc";

				if ( darcData == null )
					return false;
				File.WriteAllBytes( outFile, darcData );

				if ( Directory.Exists( root ) && delete )
					Directory.Delete( root, true );
				return true;
			}
			catch ( Exception )
			{
				return false;
			}
		}

		// DARC Utility
		internal static int GetDarCposition( byte[] data )
		{
			int pos = 0;
			while ( BitConverter.ToUInt32( data, pos ) != 0x63726164 )
			{
				pos += 4;
				if ( pos >= data.Length )
					return -1;
			}
			return pos;
		}

		internal static bool InsertFile( ref Darc orig, int index, string path )
		{
			try
			{
				return Darc.InsertFile( ref orig, index, File.ReadAllBytes( path ) );
			}
			catch ( Exception )
			{
				return false;
			}
		}

		internal static bool InsertFile( ref Darc orig, int index, byte[] data )
		{
			if ( index < 0 )
				return false;

			try
			{
				uint oldLength = orig.Entries[ index ].DataLength;
				uint offset = orig.Entries[ index ].DataOffset - orig.Header.FileDataOffset;
				int diff = (int) ( data.Length - oldLength );

				// Insert into Data Block
				byte[] pre = orig.Data.Take( (int) offset ).ToArray();
				byte[] post = orig.Data.Skip( (int) ( offset + oldLength ) ).ToArray();

				// Reassemble data
				orig.Data = pre.Concat( data ).Concat( post ).ToArray();

				// Fix Offset references of other files
				foreach ( var x in orig.Entries.Where( x => x.DataOffset >= offset + oldLength ) )
					x.DataOffset += (uint) diff;
				orig.Entries[ index ].DataLength = (uint) data.Length;
				orig.Header.FileSize += (uint) diff;
				return true;
			}
			catch ( Exception )
			{
				return false;
			}
		}

		internal static Darc InsertFiles( Darc orig, string folderName )
		{
			string[] fileNames = new string[ orig.Entries.Length ];
			for ( int i = 0; i < fileNames.Length; i++ )
				fileNames[ i ] = orig.FileNameTable[ i ].FileName;

			string[] files = Directory.GetFiles( folderName, "*", SearchOption.AllDirectories );
			foreach ( string file in files )
			{
				FileInfo fi = new FileInfo( file );
				string fileName = fi.Name;

				// Get Index of file
				int index = Array.IndexOf( fileNames, fileName );
				if ( orig.Entries[ index ].IsFolder )
					throw new Exception( file + " is not a valid file to reinsert!" );

				Darc.InsertFile( ref orig, index, file );
			}
			// Fix Data layout
			Array.Resize( ref orig.Data, orig.Data.Length % 4 == 0
											 ? orig.Data.Length
											 : orig.Data.Length + 4 - orig.Data.Length % 4 );
			orig.Header.FileSize = (uint) ( orig.Data.Length + orig.Header.FileDataOffset );
			return orig;
		}
	}
}