using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CtrDotNet.CTR
{
	public class RomFS
	{
		// Get Info
		public readonly string FileName;

		public readonly bool isTempFile;
		public readonly byte[] SuperBlockHash;
		public readonly uint SuperBlockLen;

		public RomFS( string fn )
		{
			this.FileName = fn;
			this.isTempFile = true;
			using ( var fs = File.OpenRead( fn ) )
			{
				fs.Seek( 0x8, SeekOrigin.Begin );
				uint mhlen = (uint) ( fs.ReadByte() | ( fs.ReadByte() << 8 ) | ( fs.ReadByte() << 16 ) | ( fs.ReadByte() << 24 ) );
				this.SuperBlockLen = mhlen + 0x50;
				if ( this.SuperBlockLen % 0x200 != 0 )
					this.SuperBlockLen += 0x200 - this.SuperBlockLen % 0x200;
				byte[] superblock = new byte[ this.SuperBlockLen ];
				fs.Seek( 0, SeekOrigin.Begin );
				fs.Read( superblock, 0, superblock.Length );
				using ( SHA256 sha = SHA256.Create() )
				{
					this.SuperBlockHash = sha.ComputeHash( superblock );
				}
			}
		}

		// Build
		internal const int PaddingAlign = 16;

		internal static string RootDir;
		internal const string TempFile = "tempRomFS.bin";
		internal static string OutFile;
		internal const uint RomfsUnusedEntry = 0xFFFFFFFF;

		internal static void BuildRomFS( string infile, string outfile )
		{
			OutFile = outfile;
			RootDir = infile;
			if ( File.Exists( TempFile ) )
				File.Delete( TempFile );

			FileNameTable fnt = new FileNameTable( RootDir );
			RomfsFile[] romFiles = new RomfsFile[ fnt.NumFiles ];
			LayoutManager.Input[] In = new LayoutManager.Input[ fnt.NumFiles ];
			for ( int i = 0; i < fnt.NumFiles; i++ )
			{
				In[ i ] = new LayoutManager.Input { FilePath = fnt.NameEntryTable[ i ].FullName, AlignmentSize = 0x10 };
			}
			LayoutManager.Output[] Out = LayoutManager.Create( In );
			for ( int i = 0; i < Out.Length; i++ )
			{
				romFiles[ i ] = new RomfsFile {
					Offset = Out[ i ].Offset,
					PathName = Out[ i ].FilePath.Replace( Path.GetFullPath( RootDir ), "" ).Replace( "\\", "/" ),
					FullName = Out[ i ].FilePath,
					Size = Out[ i ].Size
				};
			}
			using ( MemoryStream memoryStream = new MemoryStream() )
			{
				RomFS.BuildRomFSHeader( memoryStream, romFiles, RootDir );
				RomFS.MakeRomFSData( romFiles, memoryStream );
			}
		}

		internal static ulong Align( ulong input, ulong alignsize )
		{
			ulong output = input;
			if ( output % alignsize != 0 )
			{
				output += alignsize - output % alignsize;
			}
			return output;
		}

		internal static void MakeRomFSData( RomfsFile[] romFiles, MemoryStream metadata )
		{
			IvfcInfo ivfc = new IvfcInfo { Levels = new IvfcLevel[ 3 ] };
			for ( int i = 0; i < ivfc.Levels.Length; i++ )
			{
				ivfc.Levels[ i ] = new IvfcLevel { BlockSize = 0x1000 };
			}
			ivfc.Levels[ 2 ].DataLength = RomfsFile.GetDataBlockLength( romFiles, (ulong) metadata.Length );
			ivfc.Levels[ 1 ].DataLength = RomFS.Align( ivfc.Levels[ 2 ].DataLength, ivfc.Levels[ 2 ].BlockSize ) / ivfc.Levels[ 2 ].BlockSize * 0x20; //0x20 per SHA256 hash
			ivfc.Levels[ 0 ].DataLength = RomFS.Align( ivfc.Levels[ 1 ].DataLength, ivfc.Levels[ 1 ].BlockSize ) / ivfc.Levels[ 1 ].BlockSize * 0x20; //0x20 per SHA256 hash
			ulong masterHashLen = RomFS.Align( ivfc.Levels[ 0 ].DataLength, ivfc.Levels[ 0 ].BlockSize ) / ivfc.Levels[ 0 ].BlockSize * 0x20;
			ulong lofs = 0;
			foreach ( IvfcLevel t in ivfc.Levels )
			{
				t.HashOffset = lofs;
				lofs += RomFS.Align( t.DataLength, t.BlockSize );
			}
			const uint ivfcMagic = 0x43465649; //IVFC
			const uint reserved = 0x0;
			const uint headerLen = 0x5C;
			const uint mediaUnitSize = 0x200;
			FileStream outFileStream = new FileStream( TempFile, FileMode.Create, FileAccess.ReadWrite );
			try
			{
				outFileStream.Seek( 0, SeekOrigin.Begin );
				outFileStream.Write( BitConverter.GetBytes( ivfcMagic ), 0, 0x4 );
				outFileStream.Write( BitConverter.GetBytes( 0x10000 ), 0, 0x4 );
				outFileStream.Write( BitConverter.GetBytes( masterHashLen ), 0, 0x4 );
				foreach ( IvfcLevel t in ivfc.Levels )
				{
					outFileStream.Write( BitConverter.GetBytes( t.HashOffset ), 0, 0x8 );
					outFileStream.Write( BitConverter.GetBytes( t.DataLength ), 0, 0x8 );
					outFileStream.Write( BitConverter.GetBytes( (int) Math.Log( t.BlockSize, 2 ) ), 0, 0x4 );
					outFileStream.Write( BitConverter.GetBytes( reserved ), 0, 0x4 );
				}
				outFileStream.Write( BitConverter.GetBytes( headerLen ), 0, 0x4 );
				//IVFC Header is Written.
				outFileStream.Seek( (long) RomFS.Align( masterHashLen + 0x60, ivfc.Levels[ 0 ].BlockSize ), SeekOrigin.Begin );
				byte[] metadataArray = metadata.ToArray();
				outFileStream.Write( metadataArray, 0, metadataArray.Length );
				long baseOfs = outFileStream.Position;

				foreach ( RomfsFile t in romFiles )
				{
					outFileStream.Seek( baseOfs + (long) t.Offset, SeekOrigin.Begin );
					using ( FileStream inStream = new FileStream( t.FullName, FileMode.Open, FileAccess.Read ) )
					{
						while ( inStream.Position < inStream.Length )
						{
							byte[] buffer = new byte[ inStream.Length - inStream.Position > 0x100000
														  ? 0x100000
														  : inStream.Length - inStream.Position ];
							inStream.Read( buffer, 0, buffer.Length );
							outFileStream.Write( buffer, 0, buffer.Length );
						}
					}
				}
				long hashBaseOfs = (long) RomFS.Align( (ulong) outFileStream.Position, ivfc.Levels[ 2 ].BlockSize );
				long hOfs = (long) RomFS.Align( masterHashLen, ivfc.Levels[ 0 ].BlockSize );
				long cOfs = hashBaseOfs + (long) ivfc.Levels[ 1 ].HashOffset;
				SHA256Managed sha = new SHA256Managed();
				for ( int i = ivfc.Levels.Length - 1; i >= 0; i-- )
				{
					byte[] buffer = new byte[ (int) ivfc.Levels[ i ].BlockSize ];

					for ( long ofs = 0; ofs < (long) ivfc.Levels[ i ].DataLength; ofs += ivfc.Levels[ i ].BlockSize )
					{
						outFileStream.Seek( hOfs, SeekOrigin.Begin );
						outFileStream.Read( buffer, 0, (int) ivfc.Levels[ i ].BlockSize );
						hOfs = outFileStream.Position;
						byte[] hash = sha.ComputeHash( buffer );
						outFileStream.Seek( cOfs, SeekOrigin.Begin );
						outFileStream.Write( hash, 0, hash.Length );
						cOfs = outFileStream.Position;
					}

					if ( i <= 0 )
						continue;

					if ( i == 2 )
					{
						long len = outFileStream.Position;
						if ( len % 0x1000 != 0 )
						{
							len = (long) RomFS.Align( (ulong) len, 0x1000 );
							byte[] buf = new byte[ len - outFileStream.Position ];
							outFileStream.Write( buf, 0, buf.Length );
						}
					}

					hOfs = hashBaseOfs + (long) ivfc.Levels[ i - 1 ].HashOffset;
					if ( i > 1 )
						cOfs = hashBaseOfs + (long) ivfc.Levels[ i - 2 ].HashOffset;
					else
						cOfs = (long) RomFS.Align( headerLen, PaddingAlign );
				}
				outFileStream.Seek( 0, SeekOrigin.Begin );
				uint superBlockLen = (uint) RomFS.Align( masterHashLen + 0x60, mediaUnitSize );
				byte[] masterHashes = new byte[ superBlockLen ];
				outFileStream.Read( masterHashes, 0, (int) superBlockLen );
				sha.ComputeHash( masterHashes );
			}
			finally
			{
				outFileStream.Dispose();
			}

			if ( OutFile == TempFile )
				return;
			if ( File.Exists( OutFile ) )
				File.Delete( OutFile );
			File.Move( TempFile, OutFile );
		}

		internal static void WriteBinary( string tempFile, string outFile )
		{
			using ( FileStream fs = new FileStream( outFile, FileMode.Create ) )
			{
				using ( BinaryWriter writer = new BinaryWriter( fs ) )
				{
					using ( FileStream fileStream = new FileStream( tempFile, FileMode.Open, FileAccess.Read ) )
					{
						const uint bufferSize = 0x400000; // 4MB Buffer

						byte[] buffer = new byte[ bufferSize ];
						while ( true )
						{
							int count = fileStream.Read( buffer, 0, buffer.Length );
							if ( count != 0 )
								writer.Write( buffer, 0, count );
							else
								break;
						}
					}
					writer.Flush();
				}
			}
			File.Delete( TempFile );
		}

		internal static string ByteArrayToString( IEnumerable<byte> input )
		{
			StringBuilder sb = new StringBuilder();
			foreach ( byte b in input )
				sb.Append( b.ToString( "X2" ) + " " );

			return sb.ToString();
		}

		internal static void BuildRomFSHeader( MemoryStream romfsStream, RomfsFile[] entries, string dir )
		{
			RootDir = dir;
			RomfsMetaData metaData = new RomfsMetaData();

			RomFS.InitializeMetaData( metaData );
			RomFS.CalcRomfsSize( metaData );
			RomFS.PopulateRomfs( metaData, entries );
			RomFS.WriteMetaDataToStream( metaData, romfsStream );
		}

		internal static void InitializeMetaData( RomfsMetaData metaData )
		{
			metaData.InfoHeader = new RomfsInfoHeader();
			metaData.DirTable = new RomfsDirTable();
			metaData.DirTableLen = 0;
			metaData.MDirTableLen = 0;
			metaData.FileTable = new RomfsFileTable();
			metaData.FileTableLen = 0;
			metaData.DirTable.DirectoryTable = new List<RomfsDirEntry>();
			metaData.FileTable.FileTable = new List<RomfsFileEntry>();
			metaData.InfoHeader.HeaderLength = 0x28;
			metaData.InfoHeader.Sections = new RomfsSectionHeader[ 4 ];
			metaData.DirHashTable = new List<uint>();
			metaData.FileHashTable = new List<uint>();
		}

		internal static void CalcRomfsSize( RomfsMetaData metaData )
		{
			metaData.DirNum = 1;
			DirectoryInfo rootDi = new DirectoryInfo( RootDir );
			RomFS.CalcDirSize( metaData, rootDi );

			metaData.MDirHashTableEntry = RomFS.GetHashTableEntryCount( metaData.DirNum );
			metaData.MFileHashTableEntry = RomFS.GetHashTableEntryCount( metaData.FileNum );

			uint metaDataSize = (uint) RomFS.Align( 0x28 + metaData.MDirHashTableEntry * 4 + metaData.MDirTableLen + metaData.MFileHashTableEntry * 4 + metaData.MFileTableLen, PaddingAlign );
			for ( int i = 0; i < metaData.MDirHashTableEntry; i++ )
				metaData.DirHashTable.Add( RomfsUnusedEntry );

			for ( int i = 0; i < metaData.MFileHashTableEntry; i++ )
				metaData.FileHashTable.Add( RomfsUnusedEntry );

			uint pos = metaData.InfoHeader.HeaderLength;
			for ( int i = 0; i < 4; i++ )
			{
				metaData.InfoHeader.Sections[ i ].Offset = pos;
				uint size = 0;
				switch ( i )
				{
					case 0:
						size = metaData.MDirHashTableEntry * 4;
						break;
					case 1:
						size = metaData.MDirTableLen;
						break;
					case 2:
						size = metaData.MFileHashTableEntry * 4;
						break;
					case 3:
						size = metaData.MFileTableLen;
						break;
				}
				metaData.InfoHeader.Sections[ i ].Size = size;
				pos += size;
			}
			metaData.InfoHeader.DataOffset = metaDataSize;
		}

		internal static uint GetHashTableEntryCount( uint entries )
		{
			uint count = entries;
			if ( entries < 3 )
				count = 3;
			else if ( count < 19 )
				count |= 1;
			else
			{
				while ( count % 2 == 0 || count % 3 == 0 || count % 5 == 0 || count % 7 == 0 || count % 11 == 0 || count % 13 == 0 || count % 17 == 0 )
				{
					count++;
				}
			}
			return count;
		}

		internal static void CalcDirSize( RomfsMetaData metaData, DirectoryInfo dir )
		{
			if ( metaData.MDirTableLen == 0 )
				metaData.MDirTableLen = 0x18;
			else
				metaData.MDirTableLen += 0x18 + (uint) RomFS.Align( (ulong) dir.Name.Length * 2, 4 );

			FileInfo[] files = dir.GetFiles();
			foreach ( FileInfo t in files )
				metaData.MFileTableLen += 0x20 + (uint) RomFS.Align( (ulong) t.Name.Length * 2, 4 );

			DirectoryInfo[] subDirectories = dir.GetDirectories();
			foreach ( DirectoryInfo t in subDirectories )
				RomFS.CalcDirSize( metaData, t );

			metaData.FileNum += (uint) files.Length;
			metaData.DirNum += (uint) subDirectories.Length;
		}

		internal static void PopulateRomfs( RomfsMetaData metaData, RomfsFile[] entries )
		{
			//Recursively Add All Directories to DirectoryTable
			RomFS.AddDir( metaData, new DirectoryInfo( RootDir ), 0, RomfsUnusedEntry );

			//Iteratively Add All Files to FileTable
			RomFS.AddFiles( metaData, entries );

			//Set Weird Offsets, Build HashKeyPointers, Build HashTables
			RomFS.PopulateHashTables( metaData );

			//Thats it.
		}

		internal static void PopulateHashTables( RomfsMetaData metaData )
		{
			for ( int i = 0; i < metaData.DirTable.DirectoryTable.Count; i++ )
				RomFS.AddDirHashKey( metaData, i );
			for ( int i = 0; i < metaData.FileTable.FileTable.Count; i++ )
				RomFS.AddFileHashKey( metaData, i );
		}

		internal static void AddDirHashKey( RomfsMetaData metaData, int index )
		{
			uint parent = metaData.DirTable.DirectoryTable[ index ].ParentOffset;
			string name = metaData.DirTable.DirectoryTable[ index ].Name;
			byte[] nArr = index == 0
							  ? Encoding.Unicode.GetBytes( "" )
							  : Encoding.Unicode.GetBytes( name );
			uint hash = RomFS.CalcPathHash( parent, nArr, 0 );
			int ind2 = (int) ( hash % metaData.MDirHashTableEntry );
			if ( metaData.DirHashTable[ ind2 ] == RomfsUnusedEntry )
			{
				metaData.DirHashTable[ ind2 ] = metaData.DirTable.DirectoryTable[ index ].Offset;
			}
			else
			{
				int i = RomFS.GetRomfsDirEntry( metaData, metaData.DirHashTable[ ind2 ] );
				int tempindex = index;
				metaData.DirHashTable[ ind2 ] = metaData.DirTable.DirectoryTable[ index ].Offset;
				while ( true )
				{
					if ( metaData.DirTable.DirectoryTable[ tempindex ].HashKeyPointer == RomfsUnusedEntry )
					{
						metaData.DirTable.DirectoryTable[ tempindex ].HashKeyPointer = metaData.DirTable.DirectoryTable[ i ].Offset;
						break;
					}
					i = tempindex;
					tempindex = RomFS.GetRomfsDirEntry( metaData, metaData.DirTable.DirectoryTable[ i ].HashKeyPointer );
				}
			}
		}

		internal static void AddFileHashKey( RomfsMetaData metaData, int index )
		{
			uint parent = metaData.FileTable.FileTable[ index ].ParentDirOffset;
			string name = metaData.FileTable.FileTable[ index ].Name;
			byte[] nArr = Encoding.Unicode.GetBytes( name );
			uint hash = RomFS.CalcPathHash( parent, nArr, 0 );
			int ind2 = (int) ( hash % metaData.MFileHashTableEntry );
			if ( metaData.FileHashTable[ ind2 ] == RomfsUnusedEntry )
			{
				metaData.FileHashTable[ ind2 ] = metaData.FileTable.FileTable[ index ].Offset;
			}
			else
			{
				int i = RomFS.GetRomfsFileEntry( metaData, metaData.FileHashTable[ ind2 ] );
				int tempindex = index;
				metaData.FileHashTable[ ind2 ] = metaData.FileTable.FileTable[ index ].Offset;
				while ( true )
				{
					if ( metaData.FileTable.FileTable[ tempindex ].HashKeyPointer == RomfsUnusedEntry )
					{
						metaData.FileTable.FileTable[ tempindex ].HashKeyPointer = metaData.FileTable.FileTable[ i ].Offset;
						break;
					}
					i = tempindex;
					tempindex = RomFS.GetRomfsFileEntry( metaData, metaData.FileTable.FileTable[ i ].HashKeyPointer );
				}
			}
		}

		internal static uint CalcPathHash( uint parentOffset, byte[] nameArray, int start )
		{
			uint hash = parentOffset ^ 123456789;
			for ( int i = 0; i < nameArray.Length; i += 2 )
			{
				hash = ( hash >> 5 ) | ( hash << 27 );
				hash ^= (ushort) ( nameArray[ start + i ] | ( nameArray[ start + i + 1 ] << 8 ) );
			}
			return hash;
		}

		internal static void AddDir( RomfsMetaData metaData, DirectoryInfo dir, uint parent, uint sibling )
		{
			RomFS.AddDir( metaData, dir, parent, sibling, false );
			RomFS.AddDir( metaData, dir, parent, sibling, true );
		}

		internal static void AddDir( RomfsMetaData metaData, DirectoryInfo dir, uint parent, uint sibling, bool doSubs )
		{
			DirectoryInfo[] subDirectories = dir.GetDirectories();
			if ( !doSubs )
			{
				uint currentDir = metaData.DirTableLen;
				RomfsDirEntry entry = new RomfsDirEntry { ParentOffset = parent };
				entry.ChildOffset = entry.HashKeyPointer = entry.FileOffset = RomfsUnusedEntry;
				entry.SiblingOffset = sibling;
				entry.FullName = dir.FullName;
				entry.Name = entry.FullName == RootDir
								 ? ""
								 : dir.Name;
				entry.Offset = currentDir;
				metaData.DirTable.DirectoryTable.Add( entry );
				metaData.DirTableLen += currentDir == 0
											? 0x18
											: 0x18 + (uint) RomFS.Align( (ulong) dir.Name.Length * 2, 4 );
				// int ParentIndex = GetRomfsDirEntry(MetaData, Dir.FullName);
				// uint poff = MetaData.DirTable.DirectoryTable[ParentIndex].Offset;
			}
			else
			{
				int curIndex = RomFS.GetRomfsDirEntry( metaData, dir.FullName );
				uint currentDir = metaData.DirTable.DirectoryTable[ curIndex ].Offset;
				for ( int i = 0; i < subDirectories.Length; i++ )
				{
					RomFS.AddDir( metaData, subDirectories[ i ], currentDir, sibling, false );
					if ( i <= 0 )
						continue;

					string prevFullName = subDirectories[ i - 1 ].FullName;
					string thisName = subDirectories[ i ].FullName;
					int prevIndex = RomFS.GetRomfsDirEntry( metaData, prevFullName );
					int thisIndex = RomFS.GetRomfsDirEntry( metaData, thisName );
					metaData.DirTable.DirectoryTable[ prevIndex ].SiblingOffset =
						metaData.DirTable.DirectoryTable[ thisIndex ].Offset;
				}
				foreach ( DirectoryInfo t in subDirectories )
					RomFS.AddDir( metaData, t, currentDir, sibling, true );
			}

			if ( subDirectories.Length <= 0 )
				return;

			int curindex = RomFS.GetRomfsDirEntry( metaData, dir.FullName );
			int childindex = RomFS.GetRomfsDirEntry( metaData, subDirectories[ 0 ].FullName );
			if ( curindex > -1 && childindex > -1 )
				metaData.DirTable.DirectoryTable[ curindex ].ChildOffset =
					metaData.DirTable.DirectoryTable[ childindex ].Offset;
		}

		internal static void AddFiles( RomfsMetaData metaData, RomfsFile[] entries )
		{
			string prevDirPath = "";
			for ( int i = 0; i < entries.Length; i++ )
			{
				FileInfo file = new FileInfo( entries[ i ].FullName );
				RomfsFileEntry entry = new RomfsFileEntry();
				string dirPath = Path.GetDirectoryName( entries[ i ].FullName );
				int parentIndex = RomFS.GetRomfsDirEntry( metaData, dirPath );
				entry.FullName = entries[ i ].FullName;
				entry.Offset = metaData.FileTableLen;
				entry.ParentDirOffset = metaData.DirTable.DirectoryTable[ parentIndex ].Offset;
				entry.SiblingOffset = RomfsUnusedEntry;
				if ( dirPath == prevDirPath )
				{
					metaData.FileTable.FileTable[ i - 1 ].SiblingOffset = entry.Offset;
				}
				if ( metaData.DirTable.DirectoryTable[ parentIndex ].FileOffset == RomfsUnusedEntry )
				{
					metaData.DirTable.DirectoryTable[ parentIndex ].FileOffset = entry.Offset;
				}
				entry.HashKeyPointer = RomfsUnusedEntry;
				entry.NameSize = (uint) file.Name.Length * 2;
				entry.Name = file.Name;
				entry.DataOffset = entries[ i ].Offset;
				entry.DataSize = entries[ i ].Size;
				metaData.FileTable.FileTable.Add( entry );
				metaData.FileTableLen += 0x20 + (uint) RomFS.Align( (ulong) file.Name.Length * 2, 4 );
				prevDirPath = dirPath;
			}
		}

		internal static void WriteMetaDataToStream( RomfsMetaData metaData, MemoryStream stream )
		{
			//First, InfoHeader.
			stream.Write( BitConverter.GetBytes( metaData.InfoHeader.HeaderLength ), 0, 4 );
			foreach ( RomfsSectionHeader sh in metaData.InfoHeader.Sections )
			{
				stream.Write( BitConverter.GetBytes( sh.Offset ), 0, 4 );
				stream.Write( BitConverter.GetBytes( sh.Size ), 0, 4 );
			}
			stream.Write( BitConverter.GetBytes( metaData.InfoHeader.DataOffset ), 0, 4 );

			//DirHashTable
			foreach ( uint u in metaData.DirHashTable )
			{
				stream.Write( BitConverter.GetBytes( u ), 0, 4 );
			}

			//DirTable
			foreach ( RomfsDirEntry dir in metaData.DirTable.DirectoryTable )
			{
				stream.Write( BitConverter.GetBytes( dir.ParentOffset ), 0, 4 );
				stream.Write( BitConverter.GetBytes( dir.SiblingOffset ), 0, 4 );
				stream.Write( BitConverter.GetBytes( dir.ChildOffset ), 0, 4 );
				stream.Write( BitConverter.GetBytes( dir.FileOffset ), 0, 4 );
				stream.Write( BitConverter.GetBytes( dir.HashKeyPointer ), 0, 4 );
				uint nlen = (uint) dir.Name.Length * 2;
				stream.Write( BitConverter.GetBytes( nlen ), 0, 4 );
				byte[] nameArray = new byte[ (int) RomFS.Align( nlen, 4 ) ];
				Array.Copy( Encoding.Unicode.GetBytes( dir.Name ), 0, nameArray, 0, nlen );
				stream.Write( nameArray, 0, nameArray.Length );
			}

			//FileHashTable
			foreach ( uint u in metaData.FileHashTable )
			{
				stream.Write( BitConverter.GetBytes( u ), 0, 4 );
			}

			//FileTable
			foreach ( RomfsFileEntry file in metaData.FileTable.FileTable )
			{
				stream.Write( BitConverter.GetBytes( file.ParentDirOffset ), 0, 4 );
				stream.Write( BitConverter.GetBytes( file.SiblingOffset ), 0, 4 );
				stream.Write( BitConverter.GetBytes( file.DataOffset ), 0, 8 );
				stream.Write( BitConverter.GetBytes( file.DataSize ), 0, 8 );
				stream.Write( BitConverter.GetBytes( file.HashKeyPointer ), 0, 4 );
				uint nlen = (uint) file.Name.Length * 2;
				stream.Write( BitConverter.GetBytes( nlen ), 0, 4 );
				byte[] nameArray = new byte[ (int) RomFS.Align( nlen, 4 ) ];
				Array.Copy( Encoding.Unicode.GetBytes( file.Name ), 0, nameArray, 0, nlen );
				stream.Write( nameArray, 0, nameArray.Length );
			}

			//Padding
			while ( stream.Position % PaddingAlign != 0 )
				stream.Write( new byte[ PaddingAlign - stream.Position % 0x10 ], 0, (int) ( PaddingAlign - stream.Position % 0x10 ) );
			//All Done.
		}

		//GetRomfs[...]Entry Functions are all O(n)
		internal static int GetRomfsDirEntry( RomfsMetaData metaData, string fullName )
		{
			for ( int i = 0; i < metaData.DirTable.DirectoryTable.Count; i++ )
				if ( metaData.DirTable.DirectoryTable[ i ].FullName == fullName )
					return i;

			return -1;
		}

		internal static int GetRomfsDirEntry( RomfsMetaData metaData, uint offset )
		{
			for ( int i = 0; i < metaData.DirTable.DirectoryTable.Count; i++ )
				if ( metaData.DirTable.DirectoryTable[ i ].Offset == offset )
					return i;

			return -1;
		}

		internal static int GetRomfsFileEntry( RomfsMetaData metaData, uint offset )
		{
			for ( int i = 0; i < metaData.FileTable.FileTable.Count; i++ )
				if ( metaData.FileTable.FileTable[ i ].Offset == offset )
					return i;

			return -1;
		}

		#region Support Class/Struct

		public class RomfsMetaData
		{
			public RomfsInfoHeader InfoHeader;
			public uint DirNum;
			public uint FileNum;
			public List<uint> DirHashTable;
			public uint MDirHashTableEntry;
			public RomfsDirTable DirTable;
			public uint DirTableLen;
			public uint MDirTableLen;
			public List<uint> FileHashTable;
			public uint MFileHashTableEntry;
			public RomfsFileTable FileTable;
			public uint FileTableLen;
			public uint MFileTableLen;
		}

		public struct RomfsSectionHeader
		{
			public uint Offset;
			public uint Size;
		}

		public struct RomfsInfoHeader
		{
			public uint HeaderLength;
			public RomfsSectionHeader[] Sections;
			public uint DataOffset;
		}

		public class RomfsDirTable
		{
			public List<RomfsDirEntry> DirectoryTable;
		}

		public class RomfsFileTable
		{
			public List<RomfsFileEntry> FileTable;
		}

		public class RomfsDirEntry
		{
			public uint ParentOffset;
			public uint SiblingOffset;
			public uint ChildOffset;
			public uint FileOffset;
			public uint HashKeyPointer;
			public string Name;
			public string FullName;
			public uint Offset;
		}

		public class RomfsFileEntry
		{
			public uint ParentDirOffset;
			public uint SiblingOffset;
			public ulong DataOffset;
			public ulong DataSize;
			public uint HashKeyPointer;
			public uint NameSize;
			public string Name;
			public string FullName;
			public uint Offset;
		}

		public class RomfsFile
		{
			public string PathName;
			public ulong Offset;
			public ulong Size;
			public string FullName;

			public static ulong GetDataBlockLength( RomfsFile[] files, ulong preData )
			{
				return files.Length == 0
						   ? preData
						   : preData + files[ files.Length - 1 ].Offset + files[ files.Length - 1 ].Size;
			}
		}

		public class IvfcInfo
		{
			public IvfcLevel[] Levels;
		}

		public class IvfcLevel
		{
			public ulong HashOffset;
			public ulong DataLength;
			public uint BlockSize;
		}

		public class FileNameTable
		{
			public List<FileInfo> NameEntryTable { get; }
			public int NumFiles => this.NameEntryTable.Count;

			internal FileNameTable( string rootPath )
			{
				this.NameEntryTable = new List<FileInfo>();
				this.AddDirectory( new DirectoryInfo( rootPath ) );
			}

			internal void AddDirectory( DirectoryInfo dir )
			{
				foreach ( FileInfo fileInfo in dir.GetFiles() )
				{
					this.NameEntryTable.Add( fileInfo );
				}
				foreach ( DirectoryInfo subdir in dir.GetDirectories() )
				{
					this.AddDirectory( subdir );
				}
			}
		}

		public static class LayoutManager
		{
			public static Output[] Create( IEnumerable<Input> inputs )
			{
				List<Output> list = new List<Output>();
				ulong len = 0;
				foreach ( Input input in inputs )
				{
					Output output = new Output();
					FileInfo fileInfo = new FileInfo( input.FilePath );
					ulong ofs = LayoutManager.AlignInput( len, input.AlignmentSize );
					output.FilePath = input.FilePath;
					output.Offset = ofs;
					output.Size = (ulong) fileInfo.Length;
					list.Add( output );
					len = ofs + (ulong) fileInfo.Length;
				}
				return list.ToArray();
			}

			private static ulong AlignInput( ulong input, ulong alignsize )
			{
				ulong output = input;
				if ( output % alignsize != 0 )
					output += alignsize - output % alignsize;

				return output;
			}

			public class Input
			{
				public string FilePath;
				public uint AlignmentSize;
			}

			public class Output
			{
				public string FilePath;
				public ulong Offset;
				public ulong Size;
			}
		}

		#endregion
	}
}