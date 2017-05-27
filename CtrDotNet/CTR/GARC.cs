using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CtrDotNet.CTR
{
	#region GARC Class & Struct

	public static class Garc
	{
		public const ushort Version6 = 0x0600;
		public const ushort Version4 = 0x0400;

		public static async Task<bool> GarcPackMS( string folderPath, string garcPath, int version, int bytesPadding )
		{
			// Check to see if our input folder exists.
			if ( !new DirectoryInfo( folderPath ).Exists )
			{
				return false;
			}

			if ( version != Version4 && version != Version6 )
				throw new FormatException( "Invalid GARC Version: 0x" + version.ToString( "X4" ) );

			// Okay some basic proofing is done. Proceed.
			int filectr = 0;
			// Get the paths of the files to pack up. 
			string[] files = Directory.GetFiles( folderPath );
			string[] folders = Directory.GetDirectories( folderPath, "*.*", SearchOption.TopDirectoryOnly );

			string[] packOrder = new string[ files.Length + folders.Length ];

			#region Reassemble a list of filenames.

			try
			{
				foreach ( string f in files )
				{
					string fn = Path.GetFileNameWithoutExtension( f );

					if ( fn == null )
						continue;

					int compressed = fn.IndexOf( "dec_", StringComparison.Ordinal );
					int fileNumber = compressed < 0
										 ? int.Parse( fn )
										 : int.Parse( fn.Substring( compressed + 4 ) );

					packOrder[ fileNumber ] = f;
					filectr++;
				}
				foreach ( string f in folders )
				{
					packOrder[ int.Parse( new DirectoryInfo( f ).Name ) ] = f;
					filectr += Directory.GetFiles( f ).Length;
				}
			}
			catch ( Exception )
			{
				return false;
			}

			#endregion

			// Set Up the GARC template.
			GarcFile garc = new GarcFile {
				ContentPadToNearest = 4,
				Fato = {
					// Magic = new[] { 'O', 'T', 'A', 'F' },
					Entries = new FatoEntry[ packOrder.Length ],
					EntryCount = (ushort) packOrder.Length,
					HeaderSize = 0xC + packOrder.Length * 4,
					Padding = 0xFFFF
				},
				Fatb = {
					// Magic = new[] { 'B', 'T', 'A', 'F' },
					Entries = new FatbEntry[ packOrder.Length ],
					FileCount = filectr
				}
			};
			if ( version == Version6 )
			{
				// Some files have larger bytes-to-pad values (ex/ 0x80 for a109)
				// Hopefully there's no problems defining this with a constant number.
				garc.ContentPadToNearest = 4;
			}

			#region Start Reassembling the FAT* tables.

			{
				int op = 0;
				int od = 0;
				int v = 0;
				for ( int i = 0; i < garc.Fatb.Entries.Length; i++ )
				{
					garc.Fato.Entries[ i ].Offset = op; // FATO offset
					garc.Fatb.Entries[ i ].SubEntries = new FatbSubEntry[ 32 ];
					op += 4; // Vector
					if ( !Directory.Exists( packOrder[ i ] ) ) // is not folder
					{
						garc.Fatb.Entries[ i ].IsFolder = false;
						garc.Fatb.Entries[ i ].SubEntries[ 0 ].Exists = true;

						string fn = Path.GetFileNameWithoutExtension( packOrder[ i ] );

						if ( fn == null )
							continue;

						int compressed = fn.IndexOf( "dec_", StringComparison.Ordinal );
						int fileNumber = compressed < 0
											 ? int.Parse( fn )
											 : int.Parse( fn.Substring( compressed + 4 ) );

						if ( compressed >= 0 )
						{
							string old = packOrder[ i ];
							await Lzss.Compress( packOrder[ i ], packOrder[ i ] = Path.Combine( Path.GetDirectoryName( packOrder[ i ] ), fileNumber.ToString() ) );
							File.Delete( old );
						}

						// Assemble Vector
						v = 1;

						// Assemble Entry
						FileInfo fi = new FileInfo( packOrder[ i ] );
						int actualLength = (int) ( fi.Length % 4 == 0
													   ? fi.Length
													   : fi.Length + 4 - fi.Length % 4 );
						garc.Fatb.Entries[ i ].SubEntries[ 0 ].Start = od;
						garc.Fatb.Entries[ i ].SubEntries[ 0 ].End = actualLength + garc.Fatb.Entries[ i ].SubEntries[ 0 ].Start;
						garc.Fatb.Entries[ i ].SubEntries[ 0 ].Length = (int) fi.Length;
						od += actualLength;

						op += 12;
					}
					else
					{
						garc.Fatb.Entries[ i ].IsFolder = true;
						string[] subFiles = Directory.GetFiles( packOrder[ i ] );
						foreach ( string f in subFiles )
						{
							string s = f;
							string fn = Path.GetFileNameWithoutExtension( f );

							if ( fn == null )
								continue;

							int compressed = fn.IndexOf( "dec_", StringComparison.Ordinal );
							int fileNumber = compressed < 0
												 ? int.Parse( fn )
												 : int.Parse( fn.Substring( compressed + 4 ) );
							garc.Fatb.Entries[ i ].SubEntries[ fileNumber ].Exists = true;

							if ( compressed >= 0 )
							{
								await Lzss.Compress( f, s = Path.Combine( Path.GetDirectoryName( f ), fileNumber.ToString() ) );
								File.Delete( f );
							}

							// Assemble Vector
							v |= 1 << fileNumber;

							// Assemble Entry
							FileInfo fi = new FileInfo( s );
							int actualLength = (int) ( fi.Length % 4 == 0
														   ? fi.Length
														   : fi.Length + 4 - fi.Length % 4 );
							garc.Fatb.Entries[ i ].SubEntries[ fileNumber ].Start = od;
							garc.Fatb.Entries[ i ].SubEntries[ fileNumber ].End = actualLength + garc.Fatb.Entries[ i ].SubEntries[ fileNumber ].Start;
							garc.Fatb.Entries[ i ].SubEntries[ fileNumber ].Length = (int) fi.Length;
							od += actualLength;

							op += 12;
						}
					}
					garc.Fatb.Entries[ i ].Vector = (uint) v;
				}
				garc.Fatb.HeaderSize = 0xC + op;
			}

			#endregion

			// Delete the old garc if it exists, then begin writing our new one 
			try
			{
				File.Delete( garcPath );
			}
			catch
			{
				// ignored
			}

			// Set up the Header Info
			using ( var newGarc = new FileStream( garcPath, FileMode.Create ) )
			using ( var ms = new MemoryStream() )
			using ( BinaryWriter gw = new BinaryWriter( ms ) )
			{
				#region Write GARC Headers

				// Write GARC
				gw.Write( (uint) 0x47415243 ); // GARC
				gw.Write( (uint) ( version == Version6
									   ? 0x24
									   : 0x1C ) ); // Header Length
				gw.Write( (ushort) 0xFEFF ); // Endianness BOM
				gw.Write( (ushort) version ); // Version
				gw.Write( (uint) 0x00000004 ); // Section Count (4)
				gw.Write( (uint) 0x00000000 ); // Data Offset (temp)
				gw.Write( (uint) 0x00000000 ); // File Length (temp)
				gw.Write( (uint) 0x00000000 ); // Largest File Size (temp)

				if ( version == Version6 )
				{
					gw.Write( (uint) 0x0 );
					gw.Write( (uint) 0x0 );
				}

				// Write FATO
				gw.Write( (uint) 0x4641544F ); // FATO
				gw.Write( garc.Fato.HeaderSize ); // Header Size 
				gw.Write( garc.Fato.EntryCount ); // Entry Count
				gw.Write( garc.Fato.Padding ); // Padding
				for ( int i = 0; i < garc.Fato.Entries.Length; i++ )
					gw.Write( (uint) garc.Fato.Entries[ i ].Offset );

				// Write FATB
				gw.Write( (uint) 0x46415442 ); // FATB
				gw.Write( garc.Fatb.HeaderSize ); // Header Size
				gw.Write( garc.Fatb.FileCount ); // File Count
				foreach ( var e in garc.Fatb.Entries )
				{
					gw.Write( e.Vector );
					foreach ( var s in e.SubEntries.Where( s => s.Exists ) )
					{
						gw.Write( (uint) s.Start );
						gw.Write( (uint) s.End );
						gw.Write( (uint) s.Length );
					}
				}

				// Write FIMB
				gw.Write( (uint) 0x46494D42 ); // FIMB
				gw.Write( (uint) 0x0000000C ); // Header Length
				var dataLen = gw.BaseStream.Position;
				gw.Write( (uint) 0 ); // Data Length - TEMP

				gw.Seek( 0x10, SeekOrigin.Begin ); // Goto the start of the un-set 0 data we set earlier and set it.
				var hdrLen = gw.BaseStream.Position;
				gw.Write( (uint) 0 ); // Write Data Offset - TEMP
				gw.Write( (uint) 0 ); // Write total GARC Length - TEMP

				// Write Handling information
				if ( version == Version4 )
				{
					gw.Write( garc.ContentLargestUnpadded ); // Write Largest File stat
				}
				else if ( version == Version6 )
				{
					gw.Write( garc.ContentLargestPadded ); // Write Largest With Padding
					gw.Write( garc.ContentLargestUnpadded ); // Write Largest Without Padding
					gw.Write( garc.ContentPadToNearest );
				}

				newGarc.Seek( 0, SeekOrigin.End ); // Goto the end so we can copy the filedata after the GARC headers.

				#endregion

				#region Write Files

				var ghLength = gw.BaseStream.Length;

				long largestSize = 0; // Required memory to allocate to handle the largest file
				long largestPadded = 0; // Required memory to allocate to handle the largest PADDED file (Ver6 only)
				foreach ( string e in packOrder )
				{
					string[] fa = Directory.Exists( e )
									  ? Directory.GetFiles( e )
									  : new[] { e };
					foreach ( string f in fa )
					{
						// Update largest file length if necessary
						long len = new FileInfo( f ).Length;
						int padding = (int) ( len % bytesPadding );
						if ( padding != 0 )
							padding = bytesPadding - padding;
						bool largest = len > largestSize;
						if ( largest )
						{
							largestSize = len;
							largestPadded = len + padding;
						}

						// Write to FIMB
						using ( var x = File.OpenRead( f ) )
							x.CopyTo( newGarc );

						// While length is not divisible by 4, pad with FF (unused byte)
						while ( padding-- > 0 )
							gw.Write( (byte) 0xFF );
					}
				}
				garc.ContentLargestUnpadded = (uint) largestSize;
				garc.ContentLargestPadded = (uint) largestPadded;
				var gdLength = gw.BaseStream.Length - ghLength;

				#endregion

				gw.Seek( (int) dataLen, SeekOrigin.Begin );
				gw.Write( (uint) gdLength ); // Data Length
				gw.Seek( (int) hdrLen, SeekOrigin.Begin );
				gw.Write( (uint) ghLength ); // Write Data Offset
				gw.Write( (uint) gw.BaseStream.Length ); // Write total GARC Length

				// Write Handling information
				switch ( version )
				{
					case Version4:
						gw.Write( garc.ContentLargestUnpadded ); // Write Largest File stat
						break;
					case Version6:
						gw.Write( garc.ContentLargestPadded ); // Write Largest With Padding
						gw.Write( garc.ContentLargestUnpadded ); // Write Largest Without Padding
						gw.Write( garc.ContentPadToNearest );
						break;
				}

				await ms.CopyToAsync( newGarc );

				return true;
			}
		}

		public static async Task<bool> GarcUnpack( string garcPath, string outPath, bool skipDecompression, bool supress = false )
		{
			if ( !File.Exists( garcPath ) && !supress )
			{
				return false;
			}

			// Unpack the GARC
			GarcFile garc = await Garc.UnpackGarc( garcPath );
			const string ext = "bin"; // Default Extension Name
			int fileCount = garc.Fatb.FileCount;
			string format = "D" + Math.Ceiling( Math.Log10( fileCount ) );
			if ( outPath == "gametext" )
				format = "D3";

			using ( BinaryReader br = new BinaryReader( File.OpenRead( garcPath ) ) )
			{
				// Create Extraction folder if it does not exist.
				if ( !Directory.Exists( outPath ) )
					Directory.CreateDirectory( outPath );

				// Pull out all the files
				for ( int o = 0; o < garc.Fato.EntryCount; o++ )
				{
					var entry = garc.Fatb.Entries[ o ];
					// Set Entry File Name
					string fileName = o.ToString( format );

					#region OutDirectory Determination

					string parentFolder = entry.IsFolder
											  ? Path.Combine( outPath, fileName )
											  : outPath;
					if ( entry.IsFolder ) // Process Folder
						Directory.CreateDirectory( parentFolder );

					#endregion

					uint vector = entry.Vector;
					for ( int i = 0; i < 32; i++ ) // For each bit in vector
					{
						var subEntry = entry.SubEntries[ i ];
						if ( !subEntry.Exists )
							continue;

						// Seek to Offset
						br.BaseStream.Position = subEntry.Start + garc.DataOffset;

						// Check if Compressed
						bool compressed = false;
						if ( !skipDecompression )
							try
							{
								compressed = (byte) br.PeekChar() == 0x11;
							}
							catch
							{
								// ignored
							}

						// Write File
						string fileOut = Path.Combine( parentFolder, ( entry.IsFolder
																		   ? i.ToString( "00" )
																		   : fileName ) + "." + ext );
						using ( BinaryWriter bw = new BinaryWriter( File.OpenWrite( fileOut ) ) )
						{
							// Write out the data for the file
							br.BaseStream.Position = subEntry.Start + garc.DataOffset;
							bw.Write( br.ReadBytes( subEntry.Length ) );
						}
						if ( compressed )

							#region Decompression

						{
							string decout = Path.Combine( Path.GetDirectoryName( fileOut ), "dec_" + Path.GetFileName( fileOut ) );
							try
							{
								Lzss.Decompress( fileOut, decout );
								try
								{
									File.Delete( fileOut );
								}
								catch ( Exception )
								{
									// ignored
								}
							}
							catch
							{
								// File is really not encrypted.
								try
								{
									File.Delete( decout );
								}
								catch ( Exception )
								{
									// ignored
								}
							}
						}

						#endregion

						if ( ( vector >>= 1 ) == 0 )
							break;
					}
				}
			}

			return true;
		}

		public static async Task<GarcFile> UnpackGarc( string path )
		{
			using ( var fs = new FileStream( path, FileMode.Open, FileAccess.Read ) )
			using ( var ms = new MemoryStream() )
			{
				await fs.CopyToAsync( ms );
				return Garc.UnpackGarc( ms );
			}
		}

		private static GarcFile UnpackGarc( byte[] data )
		{
			using ( var ms = new MemoryStream( data ) )
			{
				return Garc.UnpackGarc( ms );
			}
		}

		private static GarcFile UnpackGarc( Stream stream )
		{
			GarcFile garc = new GarcFile();
			using ( BinaryReader br = new BinaryReader( stream ) )
			{
				// GARC Header
				garc.Magic = br.ReadChars( 4 );
				garc.HeaderSize = br.ReadUInt32();
				garc.Endianess = br.ReadUInt16();
				garc.Version = br.ReadUInt16();
				garc.ChunkCount = br.ReadUInt32();

				garc.DataOffset = br.ReadUInt32();
				garc.FileSize = br.ReadUInt32();
				if ( garc.Version == Version4 )
				{
					garc.ContentLargestUnpadded = br.ReadUInt32();
					garc.ContentPadToNearest = 4;
				}
				else if ( garc.Version == Version6 )
				{
					garc.ContentLargestPadded = br.ReadUInt32();
					garc.ContentLargestUnpadded = br.ReadUInt32();
					garc.ContentPadToNearest = br.ReadUInt32();
				}
				else
					throw new FormatException( "Invalid GARC Version: 0x" + garc.Version.ToString( "X4" ) );
				if ( garc.ChunkCount != 4 )
					throw new FormatException( "Invalid GARC Chunk Count: " + garc.ChunkCount );

				// FATO (File Allocation Table Offsets)
				garc.Fato.Magic = br.ReadChars( 4 );
				garc.Fato.HeaderSize = br.ReadInt32();
				garc.Fato.EntryCount = br.ReadUInt16();
				garc.Fato.Padding = br.ReadUInt16();

				garc.Fato.Entries = new FatoEntry[ garc.Fato.EntryCount ];
				for ( int i = 0; i < garc.Fato.EntryCount; i++ )
					garc.Fato.Entries[ i ].Offset = br.ReadInt32();

				// FATB (File Allocation Table Bits)
				garc.Fatb.Magic = br.ReadChars( 4 );
				garc.Fatb.HeaderSize = br.ReadInt32();
				garc.Fatb.FileCount = br.ReadInt32();

				garc.Fatb.Entries = new FatbEntry[ garc.Fato.EntryCount ];
				for ( int i = 0; i < garc.Fato.EntryCount; i++ ) // Loop through all FATO entries
				{
					garc.Fatb.Entries[ i ].Vector = br.ReadUInt32();
					garc.Fatb.Entries[ i ].SubEntries = new FatbSubEntry[ 32 ];
					uint bitvector = garc.Fatb.Entries[ i ].Vector;
					int ctr = 0;
					for ( int b = 0; b < 32; b++ )
					{
						garc.Fatb.Entries[ i ].SubEntries[ b ].Exists = ( bitvector & 1 ) == 1;
						bitvector >>= 1;
						if ( !garc.Fatb.Entries[ i ].SubEntries[ b ].Exists )
							continue;
						garc.Fatb.Entries[ i ].SubEntries[ b ].Start = br.ReadInt32();
						garc.Fatb.Entries[ i ].SubEntries[ b ].End = br.ReadInt32();
						garc.Fatb.Entries[ i ].SubEntries[ b ].Length = br.ReadInt32();
						ctr++;
					}
					garc.Fatb.Entries[ i ].IsFolder = ctr > 1;
				}

				// FIMB (File IMage Bytes)
				garc.Fimg.Magic = br.ReadChars( 4 );
				garc.Fimg.HeaderSize = br.ReadInt32();
				garc.Fimg.DataSize = br.ReadInt32();

				// Files data
				// Oftentimes too large to toss into a byte array. Fetch as needed with a BinaryReader.
			}
			return garc;
		}

		public static MemGarc PackGarc( byte[][] data, int version, int contentpadnearest )
		{
			if ( contentpadnearest < 0 )
				contentpadnearest = 4;
			// Set Up the GARC template.
			GarcFile garc = new GarcFile {
				ContentPadToNearest = (uint) contentpadnearest,
				Fato = {
					// Magic = new[] { 'O', 'T', 'A', 'F' },
					Entries = new FatoEntry[ data.Length ],
					EntryCount = (ushort) data.Length,
					HeaderSize = 0xC + data.Length * 4,
					Padding = 0xFFFF
				},
				Fatb = {
					// Magic = new[] { 'B', 'T', 'A', 'F' },
					Entries = new FatbEntry[ data.Length ],
					FileCount = data.Length
				}
			};

			if ( version == Version6 )
				garc.ContentPadToNearest = 4;

			int op = 0;
			int od = 0;
			for ( int i = 0; i < garc.Fatb.Entries.Length; i++ )
			{
				garc.Fato.Entries[ i ].Offset = op; // FATO offset
				garc.Fatb.Entries[ i ].SubEntries = new FatbSubEntry[ 32 ];
				op += 4; // Vector
				garc.Fatb.Entries[ i ].IsFolder = false;
				garc.Fatb.Entries[ i ].SubEntries[ 0 ].Exists = true;

				// Assemble Entry
				var paddingRequired = data[ i ].Length % garc.ContentPadToNearest;
				if ( paddingRequired != 0 )
					paddingRequired = garc.ContentPadToNearest - paddingRequired;
				int actualLength = data[ i ].Length + (int) paddingRequired;
				garc.Fatb.Entries[ i ].SubEntries[ 0 ].Start = od;
				garc.Fatb.Entries[ i ].SubEntries[ 0 ].End = actualLength + garc.Fatb.Entries[ i ].SubEntries[ 0 ].Start;
				garc.Fatb.Entries[ i ].SubEntries[ 0 ].Length = data[ i ].Length;
				garc.Fatb.Entries[ i ].SubEntries[ 0 ].Padding = (int) paddingRequired;
				od += actualLength;

				op += 12;
				garc.Fatb.Entries[ i ].Vector = 1;
			}
			garc.Fatb.HeaderSize = 0xC + op;

			// Set up the Header Info
			string tempFile = Path.GetTempFileName();
			using ( var newGarc = new FileStream( tempFile, FileMode.Create ) )
			using ( BinaryWriter gw = new BinaryWriter( newGarc ) )
			{
				#region Write GARC Headers

				// Write GARC
				gw.Write( (uint) 0x47415243 ); // GARC
				gw.Write( (uint) ( version == Version6
									   ? 0x24
									   : 0x1C ) ); // Header Length
				gw.Write( (ushort) 0xFEFF ); // Endianness BOM
				gw.Write( (ushort) version ); // Version
				gw.Write( (uint) 0x00000004 ); // Section Count (4)
				gw.Write( (uint) 0x00000000 ); // Data Offset (temp)
				gw.Write( (uint) 0x00000000 ); // File Length (temp)
				gw.Write( (uint) 0x00000000 ); // Largest File Size (temp)

				if ( version == Version6 )
				{
					gw.Write( (uint) 0x0 );
					gw.Write( (uint) 0x0 );
				}

				// Write FATO
				gw.Write( (uint) 0x4641544F ); // FATO
				gw.Write( garc.Fato.HeaderSize ); // Header Size 
				gw.Write( garc.Fato.EntryCount ); // Entry Count
				gw.Write( garc.Fato.Padding ); // Padding
				for ( int i = 0; i < garc.Fato.Entries.Length; i++ )
					gw.Write( (uint) garc.Fato.Entries[ i ].Offset );

				// Write FATB
				gw.Write( (uint) 0x46415442 ); // FATB
				gw.Write( garc.Fatb.HeaderSize ); // Header Size
				gw.Write( garc.Fatb.FileCount ); // File Count
				foreach ( var e in garc.Fatb.Entries )
				{
					gw.Write( e.Vector );
					foreach ( var s in e.SubEntries.Where( s => s.Exists ) )
					{
						gw.Write( (uint) s.Start );
						gw.Write( (uint) s.End );
						gw.Write( (uint) s.Length );
					}
				}

				gw.Write( (uint) 0x46494D42 ); // FIMB
				gw.Write( (uint) 0x0000000C ); // Header Length
				var dataLen = gw.BaseStream.Position;
				gw.Write( (uint) 0 ); // Data Length - TEMP

				gw.Seek( 0x10, SeekOrigin.Begin ); // Goto the start of the un-set 0 data we set earlier and set it.
				var hdrLen = gw.BaseStream.Position;
				gw.Write( (uint) 0 ); // Write Data Offset - TEMP
				gw.Write( (uint) 0 ); // Write total GARC Length - TEMP

				// Write Handling information
				if ( version == Version4 )
				{
					gw.Write( garc.ContentLargestUnpadded ); // Write Largest File stat
				}
				else if ( version == Version6 )
				{
					gw.Write( garc.ContentLargestPadded ); // Write Largest With Padding
					gw.Write( garc.ContentLargestUnpadded ); // Write Largest Without Padding
					gw.Write( garc.ContentPadToNearest );
				}

				newGarc.Seek( 0, SeekOrigin.End ); // Goto the end so we can copy the filedata after the GARC headers.

				#endregion

				#region Write Files

				var ghLength = gw.BaseStream.Length;

				long largestSize = 0; // Required memory to allocate to handle the largest file
				long largestPadded = 0; // Required memory to allocate to handle the largest PADDED file (Ver6 only)
				for ( int i = 0; i < data.Length; i++ )
				{
					byte[] e = data[ i ];

					// Update largest file length if necessary
					int len = e.Length;
					int padding = garc.Fatb.Entries[ i ].SubEntries[ 0 ].Padding;
					bool largest = len > largestSize;
					if ( largest )
					{
						largestSize = len;
						largestPadded = len + padding;
					}

					// Write to FIMB
					gw.Write( e );

					// Pad with FF (unused byte)
					while ( padding-- > 0 )
						gw.Write( (byte) 0xFF );
				}
				garc.ContentLargestUnpadded = (uint) largestSize;
				garc.ContentLargestPadded = (uint) largestPadded;
				var gdLength = gw.BaseStream.Length - ghLength;

				#endregion

				gw.Seek( (int) dataLen, SeekOrigin.Begin );
				gw.Write( (uint) gdLength ); // Data Length
				gw.Seek( (int) hdrLen, SeekOrigin.Begin );
				gw.Write( (uint) ghLength ); // Write Data Offset
				gw.Write( (uint) gw.BaseStream.Length ); // Write total GARC Length

				switch ( version )
				{
					case Version4:
						gw.Write( garc.ContentLargestUnpadded ); // Write Largest File stat
						break;
					case Version6:
						gw.Write( garc.ContentLargestPadded ); // Write Largest With Padding
						gw.Write( garc.ContentLargestUnpadded ); // Write Largest Without Padding
						gw.Write( garc.ContentPadToNearest );
						break;
				}
			}

			byte[] garCdata = File.ReadAllBytes( tempFile );
			File.Delete( tempFile );
			return new MemGarc( garCdata );
		}

		public class MemGarc
		{
			internal GarcFile garc;
			public int FileCount => this.garc.Fato.EntryCount;

			public MemGarc( byte[] data )
			{
				this.Data = data;
				this.garc = Garc.UnpackGarc( data );
			}

			internal byte[] Data { get; set; }

			// Returns an individual file
			public byte[] GetFile( int file, int subfile = 0 )
			{
				var entry = this.garc.Fatb.Entries[ file ];
				var subEntry = entry.SubEntries[ subfile ];
				if ( !subEntry.Exists )
					throw new ArgumentException( "SubFile does not exist." );
				var offset = subEntry.Start + this.garc.DataOffset;
				byte[] data = new byte[ subEntry.Length ];
				Array.Copy( this.Data, offset, data, 0, data.Length );
				return data;
			}

			// Returns all files (excluding language vectorized)
			public byte[][] Files
			{
				get
				{
					byte[][] data = new byte[ this.FileCount ][];
					for ( int i = 0; i < data.Length; i++ )
						data[ i ] = this.GetFile( i );
					return data;
				}
				set
				{
					if ( value == null || value.Length != this.FileCount )
						throw new ArgumentException();

					var ng = Garc.PackGarc( value, this.garc.Version, (int) this.garc.ContentPadToNearest );
					this.garc = ng.garc;
					this.Data = ng.Data;
				}
			}
		}

		/// <summary>
		/// GARC Class that is heavier on OOP to allow for compression tracking for faster edits
		/// </summary>
		public class LzGarc
		{
			private readonly GarcEntry[] storage;
			private GarcFile garc;
			public int FileCount => this.garc.Fato.EntryCount;

			public LzGarc( byte[] data )
			{
				this.Data = data;
				this.garc = Garc.UnpackGarc( data );
				this.storage = new GarcEntry[ this.FileCount ];
			}

			public byte[] Data { get; private set; }

			private class GarcEntry
			{
				public bool Accessed { get; }
				public bool Saved { get; set; }
				public byte[] Data { get; set; }
				public bool WasCompressed { get; }

				public GarcEntry() { }

				public GarcEntry( byte[] data )
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

			private byte[] GetFile( int file, int subfile = 0 )
			{
				var entry = this.garc.Fatb.Entries[ file ];
				var subEntry = entry.SubEntries[ subfile ];
				if ( !subEntry.Exists )
					throw new ArgumentException( "SubFile does not exist." );
				var offset = subEntry.Start + this.garc.DataOffset;
				byte[] data = new byte[ subEntry.Length ];
				Array.Copy( this.Data, offset, data, 0, data.Length );
				return data;
			}

			public byte[] this[ int file ]
			{
				get
				{
					if ( file > this.FileCount )
						throw new ArgumentException();

					if ( this.storage[ file ] == null )
						this.storage[ file ] = new GarcEntry( this.GetFile( file ) );
					return this.storage[ file ].Data;
				}
				set
				{
					if ( this.storage[ file ] == null )
						this.storage[ file ] = new GarcEntry( value );
					this.storage[ file ].Data = value;
					this.storage[ file ].Saved = true;
				}
			}

			public async Task<byte[]> Save()
			{
				byte[][] data = new byte[ this.FileCount ][];
				for ( int i = 0; i < data.Length; i++ )
				{
					if ( this.storage[ i ] == null || !this.storage[ i ].Saved ) // retrieve original
						data[ i ] = this.GetFile( i );
					else // use modified
						data[ i ] = await this.storage[ i ].Save();
				}

				var ng = Garc.PackGarc( data, this.garc.Version, (int) this.garc.ContentPadToNearest );
				this.garc = ng.garc;
				this.Data = ng.Data;
				return this.Data;
			}
		}

		public struct GarcFile
		{
			public char[] Magic; // Always GARC = 0x4E415243
			public uint HeaderSize; // Always 0x001C
			public ushort Endianess; // 0xFFFE
			public ushort Version; // 4: gen6, 6: gen7
			public uint ChunkCount; // Always 0x0400 chunk count

			public uint DataOffset;
			public uint FileSize;

			public uint ContentLargestPadded; // Format 6 Only
			public uint ContentLargestUnpadded;
			public uint ContentPadToNearest; // Format 6 Only (4 bytes is standard in VER_4, and is not stored)

			public Fato Fato;
			public Fatb Fatb;
			public Fimg Fimg;
		}

		public struct Fato
		{
			public char[] Magic;
			public int HeaderSize;
			public ushort EntryCount;
			public ushort Padding;

			public FatoEntry[] Entries;
		}

		public struct FatoEntry
		{
			public int Offset;
		}

		public struct Fatb
		{
			public char[] Magic;
			public int HeaderSize;
			public int FileCount;

			public FatbEntry[] Entries;
		}

		public struct FatbEntry
		{
			public uint Vector;
			public bool IsFolder;
			public FatbSubEntry[] SubEntries;
		}

		public struct FatbSubEntry
		{
			public bool Exists;
			public int Start;
			public int End;
			public int Length;

			// Unsaved Properties
			public int Padding { get; set; }
		}

		public struct Fimg
		{
			public char[] Magic;
			public int HeaderSize;
			public int DataSize;
		}
	}

	#endregion
}