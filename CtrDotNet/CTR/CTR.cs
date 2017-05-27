using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using CtrDotNet.Properties;

namespace CtrDotNet.CTR
{
	public class Ctr
	{
		internal const uint MediaUnitSize = 0x200;

		// Main wrapper that assembles the ROM based on the following specifications:
		internal static bool BuildROM( bool card2, string logoName,
									   string exefsPath, string romfsPath, string exheaderPath,
									   string serialText, string savePath )
		{
			// Sanity check the input files.
			if ( !
					 ( ( File.Exists( exefsPath ) || Directory.Exists( exefsPath ) )
					   && ( File.Exists( romfsPath ) || Directory.Exists( romfsPath ) )
					   && File.Exists( exheaderPath ) ) )
				return false;

			// If ExeFS and RomFS are not built, build.
			if ( !File.Exists( exefsPath ) && Directory.Exists( exefsPath ) )
				ExeFS.Set( Directory.GetFiles( exefsPath ), exefsPath = "exefs.bin" );
			if ( !File.Exists( romfsPath ) && Directory.Exists( romfsPath ) )
				RomFS.BuildRomFS( romfsPath, romfsPath = "romfs.bin" );

			Ncch ncch = Ctr.SetNcch( exefsPath, romfsPath, exheaderPath, serialText, logoName );
			Ncsd ncsd = Ctr.SetNcsd( ncch, card2 );
			bool success = Ctr.WriteROM( ncsd, savePath );
			return success;
		}

		// Sub methods that drive the operation
		internal static Ncch SetNcch( string exefsPath, string romfsPath, string exheaderPath, string tbSerial, string logoName )
		{
			Ncch ncch = new Ncch {
				Exheader = new Exheader( exheaderPath ),
				Plainregion = new byte[ 0 ]
			};
			if ( ncch.Exheader.IsPokemon() )
			{
				if ( ncch.Exheader.IsXy() )
					ncch.Plainregion = (byte[]) Resources.ResourceManager.GetObject( "XY" );
				else if ( ncch.Exheader.IsOras() )
					ncch.Plainregion = (byte[]) Resources.ResourceManager.GetObject( "ORAS" );
			}
			ncch.Exefs = new ExeFS( exefsPath );
			ncch.Romfs = new RomFS( romfsPath );

			ncch.Logo = (byte[]) Resources.ResourceManager.GetObject( logoName );
			ulong len = 0x200; //NCCH Signature + NCCH Header
			ncch.HeaderData = new Ncch.Header { Signature = new byte[ 0x100 ], Magic = 0x4843434E };
			ncch.HeaderData.TitleId = ncch.HeaderData.ProgramId = ncch.Exheader.TitleID;
			ncch.HeaderData.MakerCode = 0x3130; //01
			ncch.HeaderData.FormatVersion = 0x2; //Default
			ncch.HeaderData.LogoHash = new SHA256Managed().ComputeHash( ncch.Logo );
			ncch.HeaderData.ProductCode = Encoding.ASCII.GetBytes( tbSerial );
			Array.Resize( ref ncch.HeaderData.ProductCode, 0x10 );
			ncch.HeaderData.ExheaderHash = ncch.Exheader.GetSuperBlockHash();
			ncch.HeaderData.ExheaderSize = (uint) ncch.Exheader.Data.Length;
			len += ncch.HeaderData.ExheaderSize + (uint) ncch.Exheader.AccessDescriptor.Length;
			ncch.HeaderData.Flags = new byte[ 0x8 ];
			//FLAGS
			ncch.HeaderData.Flags[ 3 ] = 0; // Crypto: 0 = <7.x, 1=7.x;
			ncch.HeaderData.Flags[ 4 ] = 1; // Content Platform: 1 = CTR;
			ncch.HeaderData.Flags[ 5 ] = 0x3; // Content Type Bitflags: 1=Data, 2=Executable, 4=SysUpdate, 8=Manual, 0x10=Trial;
			ncch.HeaderData.Flags[ 6 ] = 0; // MEDIA_UNIT_SIZE = 0x200*Math.Pow(2, Content.header.Flags[6]);
			ncch.HeaderData.Flags[ 7 ] = 1; // FixedCrypto = 1, NoMountRomfs = 2; NoCrypto=4;
			ncch.HeaderData.LogoOffset = (uint) ( len / MediaUnitSize );
			ncch.HeaderData.LogoSize = (uint) ( ncch.Logo.Length / MediaUnitSize );
			len += (uint) ncch.Logo.Length;
			ncch.HeaderData.PlainRegionOffset = (uint) ( ncch.Plainregion?.Length > 0
															 ? len / MediaUnitSize
															 : 0 );
			ncch.HeaderData.PlainRegionSize = (uint) ( ncch.Plainregion?.Length ?? 0 ) / MediaUnitSize;
			len += (uint) ( ncch.Plainregion?.Length ?? 0 );
			ncch.HeaderData.ExefsOffset = (uint) ( len / MediaUnitSize );
			ncch.HeaderData.ExefsSize = (uint) ( ncch.Exefs.Data.Length / MediaUnitSize );
			ncch.HeaderData.ExefsSuperBlockSize = 0x200 / MediaUnitSize; //Static 0x200 for exefs superblock
			len += (uint) ncch.Exefs.Data.Length;
			len = (uint) Ctr.Align( len, 0x1000 ); //Romfs Start is aligned to 0x1000
			ncch.HeaderData.RomfsOffset = (uint) ( len / MediaUnitSize );
			ncch.HeaderData.RomfsSize = (uint) ( new FileInfo( ncch.Romfs.FileName ).Length / MediaUnitSize );
			ncch.HeaderData.RomfsSuperBlockSize = ncch.Romfs.SuperBlockLen / MediaUnitSize;
			len += ncch.HeaderData.RomfsSize * MediaUnitSize;
			ncch.HeaderData.ExefsHash = ncch.Exefs.SuperBlockHash;
			ncch.HeaderData.RomfsHash = ncch.Romfs.SuperBlockHash;
			ncch.HeaderData.Size = (uint) ( len / MediaUnitSize );
			//Build the Header byte[].
			ncch.HeaderData.BuildHeader();

			return ncch;
		}

		internal static Ncsd SetNcsd( Ncch ncch, bool card2 )
		{
			Ncsd ncsd = new Ncsd {
				NcchArray = new List<Ncch> { ncch },
				Card2 = card2,
				HeaderData = new Ncsd.Header { Signature = new byte[ 0x100 ], Magic = 0x4453434E }
			};
			ulong length = 0x80 * 0x100000; // 128 MB
			while ( length <= ncch.HeaderData.Size * MediaUnitSize + 0x400000 ) //Extra 4 MB for potential save data
			{
				length *= 2;
			}
			ncsd.HeaderData.MediaSize = (uint) ( length / MediaUnitSize );
			ncsd.HeaderData.TitleId = ncch.Exheader.TitleID;
			ncsd.HeaderData.OffsetSizeTable = new Ncsd.NcchMeta[ 8 ];
			ulong osOfs = 0x4000;
			for ( int i = 0; i < ncsd.HeaderData.OffsetSizeTable.Length; i++ )
			{
				Ncsd.NcchMeta ncchm = new Ncsd.NcchMeta();
				if ( i < ncsd.NcchArray.Count )
				{
					ncchm.Offset = (uint) ( osOfs / MediaUnitSize );
					ncchm.Size = ncsd.NcchArray[ i ].HeaderData.Size;
				}
				else
				{
					ncchm.Offset = 0;
					ncchm.Size = 0;
				}
				ncsd.HeaderData.OffsetSizeTable[ i ] = ncchm;
				osOfs += ncchm.Size * MediaUnitSize;
			}
			ncsd.HeaderData.Flags = new byte[ 0x8 ];
			ncsd.HeaderData.Flags[ 0 ] = 0; // 0-255 seconds of waiting for save writing.
			ncsd.HeaderData.Flags[ 3 ] = (byte) ( ncsd.Card2
													  ? 2
													  : 1 ); // Media Card Device: 1 = NOR Flash, 2 = None, 3 = BT
			ncsd.HeaderData.Flags[ 4 ] = 1; // Media Platform Index: 1 = CTR
			ncsd.HeaderData.Flags[ 5 ] = (byte) ( ncsd.Card2
													  ? 2
													  : 1 ); // Media Type Index: 0 = Inner Device, 1 = Card1, 2 = Card2, 3 = Extended Device
			ncsd.HeaderData.Flags[ 6 ] = 0; // Media Unit Size. Same as NCCH.
			ncsd.HeaderData.Flags[ 7 ] = 0; // Old Media Card Device.
			ncsd.HeaderData.NcchIdTable = new ulong[ 8 ];
			for ( int i = 0; i < ncsd.NcchArray.Count; i++ )
			{
				ncsd.HeaderData.NcchIdTable[ i ] = ncsd.NcchArray[ i ].HeaderData.TitleId;
			}
			ncsd.Cardinfoheader = new Ncsd.CardInfoHeader {
				WritableAddress = (uint) ncsd.GetWritableAddress(),
				CardInfoBitmask = 0,
				Cin = new Ncsd.CardInfoHeader.CardInfoNotes {
					Reserved0 = new byte[ 0xF8 ],
					MediaSizeUsed = osOfs,
					Reserved1 = 0,
					Unknown = 0,
					Reserved2 = new byte[ 0xC ],
					CVerTitleId = 0,
					CVerTitleVersion = 0,
					Reserved3 = new byte[ 0xCD6 ]
				},
				Ncch0TitleId = ncsd.NcchArray[ 0 ].HeaderData.TitleId,
				Reserved0 = 0,
				InitialData = new byte[ 0x30 ]
			};
			RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
			byte[] randbuffer = new byte[ 0x2C ];
			rng.GetBytes( randbuffer );
			Array.Copy( randbuffer, ncsd.Cardinfoheader.InitialData, randbuffer.Length );
			ncsd.Cardinfoheader.Reserved1 = new byte[ 0xC0 ];
			ncsd.Cardinfoheader.Ncch0Header = new byte[ 0x100 ];
			Array.Copy( ncsd.NcchArray[ 0 ].HeaderData.Data, 0x100, ncsd.Cardinfoheader.Ncch0Header, 0, 0x100 );

			ncsd.BuildHeader();

			//NCSD is Initialized
			return ncsd;
		}

		internal static bool WriteROM( Ncsd ncsd, string savePath )
		{
			using ( FileStream outFileStream = new FileStream( savePath, FileMode.Create ) )
			{
				outFileStream.Write( ncsd.Data, 0, ncsd.Data.Length );
				outFileStream.Write( ncsd.NcchArray[ 0 ].HeaderData.Data, 0, ncsd.NcchArray[ 0 ].HeaderData.Data.Length ); //Write NCCH header
				//AES time.
				byte[] key = new byte[ 0x10 ]; //Fixed-Crypto key is all zero.
				for ( int i = 0; i < 3; i++ )
				{
					AesCtr aesctr = new AesCtr( key, ncsd.NcchArray[ 0 ].HeaderData.ProgramId, (ulong) ( i + 1 ) << 56 ); //CTR is ProgramID, section id<<88
					switch ( i )
					{
						case 0: //Exheader + AccessDesc
							byte[] inEncExheader = new byte[ ncsd.NcchArray[ 0 ].Exheader.Data.Length + ncsd.NcchArray[ 0 ].Exheader.AccessDescriptor.Length ];
							byte[] outEncExheader = new byte[ ncsd.NcchArray[ 0 ].Exheader.Data.Length + ncsd.NcchArray[ 0 ].Exheader.AccessDescriptor.Length ];
							Array.Copy( ncsd.NcchArray[ 0 ].Exheader.Data, inEncExheader, ncsd.NcchArray[ 0 ].Exheader.Data.Length );
							Array.Copy( ncsd.NcchArray[ 0 ].Exheader.AccessDescriptor, 0, inEncExheader, ncsd.NcchArray[ 0 ].Exheader.Data.Length, ncsd.NcchArray[ 0 ].Exheader.AccessDescriptor.Length );
							aesctr.TransformBlock( inEncExheader, 0, inEncExheader.Length, outEncExheader, 0 );
							outFileStream.Write( outEncExheader, 0, outEncExheader.Length ); // Write Exheader
							break;
						case 1: //Exefs
							outFileStream.Seek( 0x4000 + ncsd.NcchArray[ 0 ].HeaderData.ExefsOffset * MediaUnitSize, SeekOrigin.Begin );
							byte[] outExefs = new byte[ ncsd.NcchArray[ 0 ].Exefs.Data.Length ];
							aesctr.TransformBlock( ncsd.NcchArray[ 0 ].Exefs.Data, 0, ncsd.NcchArray[ 0 ].Exefs.Data.Length, outExefs, 0 );
							outFileStream.Write( outExefs, 0, outExefs.Length );
							break;
						case 2: //Romfs
							outFileStream.Seek( 0x4000 + ncsd.NcchArray[ 0 ].HeaderData.RomfsOffset * MediaUnitSize, SeekOrigin.Begin );
							using ( FileStream inFileStream = new FileStream( ncsd.NcchArray[ 0 ].Romfs.FileName, FileMode.Open, FileAccess.Read ) )
							{
								uint bufferSize;
								ulong romfsLen = ncsd.NcchArray[ 0 ].HeaderData.RomfsSize * MediaUnitSize;
								for ( ulong j = 0; j < romfsLen; j += bufferSize )
								{
									bufferSize = romfsLen - j > 0x400000
													 ? 0x400000
													 : (uint) ( romfsLen - j );
									byte[] buf = new byte[ bufferSize ];
									byte[] outbuf = new byte[ bufferSize ];
									inFileStream.Read( buf, 0, (int) bufferSize );
									aesctr.TransformBlock( buf, 0, (int) bufferSize, outbuf, 0 );
									outFileStream.Write( outbuf, 0, (int) bufferSize );
								}
							}
							break;
					}
				}
				outFileStream.Seek( 0x4000 + ncsd.NcchArray[ 0 ].HeaderData.LogoOffset * MediaUnitSize, SeekOrigin.Begin );
				outFileStream.Write( ncsd.NcchArray[ 0 ].Logo, 0, ncsd.NcchArray[ 0 ].Logo.Length );
				if ( ncsd.NcchArray[ 0 ].Plainregion.Length > 0 )
				{
					outFileStream.Seek( 0x4000 + ncsd.NcchArray[ 0 ].HeaderData.PlainRegionOffset * MediaUnitSize, SeekOrigin.Begin );
					outFileStream.Write( ncsd.NcchArray[ 0 ].Plainregion, 0, ncsd.NcchArray[ 0 ].Plainregion.Length );
				}

				//NCSD Padding
				outFileStream.Seek( ncsd.HeaderData.OffsetSizeTable[ ncsd.NcchArray.Count - 1 ].Offset * MediaUnitSize + ncsd.HeaderData.OffsetSizeTable[ ncsd.NcchArray.Count - 1 ].Size * MediaUnitSize, SeekOrigin.Begin );
				ulong totalLen = ncsd.HeaderData.MediaSize * MediaUnitSize;
				byte[] buffer = Enumerable.Repeat( (byte) 0xFF, 0x400000 ).ToArray();
				while ( (ulong) outFileStream.Position < totalLen )
				{
					int bufferLen = totalLen - (ulong) outFileStream.Position < 0x400000
										? (int) ( totalLen - (ulong) outFileStream.Position )
										: 0x400000;
					outFileStream.Write( buffer, 0, bufferLen );
				}
			}

			//Delete Temporary Romfs File
			if ( ncsd.NcchArray[ 0 ].Romfs.isTempFile )
				File.Delete( ncsd.NcchArray[ 0 ].Romfs.FileName );

			return true;
		}

		// Utility
		internal static bool IsValid( string exeFS, string romFS, string exeheader, string path, string serial, bool card2 )
		{
			bool isSerialValid = true;
			if ( serial.Length == 10 )
			{
				string[] subs = serial.Split( '-' );
				if ( subs.Length != 3 )
					isSerialValid = false;
				else
				{
					if ( subs[ 0 ].Length != 3 || subs[ 1 ].Length != 1 || subs[ 2 ].Length != 4 )
						isSerialValid = false;
					else if ( subs[ 0 ] != "CTR" && subs[ 0 ] != "KTR" )
						isSerialValid = false;
					else if ( subs[ 1 ] != "P" && subs[ 1 ] != "N" && subs[ 2 ] != "U" )
						isSerialValid = false;
					else
					{
						if ( subs[ 2 ].Any( c => !char.IsLetterOrDigit( c ) ) )
							isSerialValid = false;
					}
				}
			}
			else
			{
				isSerialValid = false;
			}
			if ( exeFS == string.Empty
				 || romFS == string.Empty
				 || exeheader == string.Empty
				 || path == string.Empty
				 || !isSerialValid )
				return false;

			Exheader exh = new Exheader( exeheader );
			return !exh.IsPokemon() || card2;
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
	}
}