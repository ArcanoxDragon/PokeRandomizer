using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace CtrDotNet.CTR
{
	internal class Cro
	{
		internal static int IndexOfBytes( byte[] array, byte[] pattern, int startIndex, int count )
		{
			int i = startIndex;
			int endIndex = count > 0
							   ? startIndex + count
							   : array.Length;
			int fidx = 0;

			while ( i++ != endIndex - 1 )
			{
				if ( array[ i ] != pattern[ fidx ] )
					i -= fidx;
				fidx = array[ i ] == pattern[ fidx ]
						   ? ++fidx
						   : 0;
				if ( fidx == pattern.Length )
					return i - fidx + 1;
			}
			return -1;
		}

		internal static string GetHexString( byte[] data )
		{
			return BitConverter.ToString( data ).Replace( "-", "" );
		}

		internal static byte[] StringToByteArray( string hex )
		{
			return Enumerable.Range( 0, hex.Length )
							 .Where( x => x % 2 == 0 )
							 .Select( x => Convert.ToByte( hex.Substring( x, 2 ), 16 ) )
							 .ToArray();
		}

		// Checking
		internal static string[] VerifyCrr( string pathCrr, string pathCro )
		{
			// Get CRO files
			string[] croFiles = Directory.GetFiles( pathCro );

			// Weed out anything that isn't a .cro
			List<string> cros = new List<string>();
			for ( int i = 0; i < croFiles.Length; i++ )
				if ( Path.GetExtension( cros[ i ] ) == ".cro" )
					cros.Add( cros[ i ] );
			croFiles = cros.ToArray();

			// Store Hashes as Strings (hacky way to sort byte[]'s against eachother
			string[] hashes = new string[ croFiles.Length ];
			for ( int i = 0; i < hashes.Length; i++ )
			{
				byte[] data = File.ReadAllBytes( croFiles[ i ] );
				byte[] hash = Cro.HashCro( ref data );
				hashes[ i ] = Cro.GetHexString( hash ).ToUpper();
			}
			Array.Sort( hashes, string.Compare );
			// Convert Hash Strings to Bytes
			byte[][] hashData = new byte[ hashes.Length ][];
			for ( int i = 0; i < hashes.Length; i++ )
				hashData[ i ] = Cro.StringToByteArray( hashes[ i ] );

			// Open the CRR
			byte[] crr = File.ReadAllBytes( pathCrr );
			int hashTableOffset = BitConverter.ToInt32( crr, 0x350 );
			int hashCount = BitConverter.ToInt32( crr, 0x354 );

			// A little validation...
			if ( hashCount != hashData.Length )
				throw new Exception(
					$"Amount of input file-hashes does not equal the hash count in CRR. Expected {hashCount}, got {hashData.Length}." );

			string[] results = new string[ hashData.Length ];
			// Store Hashes in CRR
			for ( int i = 0; i < hashData.Length; i++ )
			{
				byte[] crrEntryHash = new byte[ 0x20 ];
				Array.Copy( crr, i * 0x20 + hashTableOffset, crrEntryHash, 0, 0x20 );
				results[ i ] = "Hash @ {0} is " + ( crrEntryHash.SequenceEqual( hashData[ i ] )
														? "valid."
														: "invalid." );
				Array.Copy( hashData, 0, crr, hashTableOffset + 0x20 * i, 0x20 );
			}
			return results;
		}

		internal static bool RehashCrr( string pathCrr, string pathCro, bool saveCro = true, bool saveCrr = true )
		{
			// Get CRO files
			string[] croFiles = Directory.GetFiles( pathCro );

			// Weed out anything that isn't a .cro
			croFiles = croFiles.Where( t => Path.GetExtension( t ) == ".cro" ).ToArray();
			// Open the CRR
			byte[] crr = File.ReadAllBytes( pathCrr );
			int hashTableOffset = BitConverter.ToInt32( crr, 0x350 );
			int hashCount = BitConverter.ToInt32( crr, 0x354 );

			// A little validation...
			if ( hashCount != croFiles.Length )
			{
				return false;
			}

			// Store Hashes as Strings (hacky way to sort byte[]'s against eachother
			string[] hashes = new string[ croFiles.Length ];
			for ( int i = 0; i < hashes.Length; i++ )
			{
				byte[] data = File.ReadAllBytes( croFiles[ i ] );
				byte[] hash = Cro.HashCro( ref data );
				hashes[ i ] = Cro.GetHexString( hash ).ToUpper();
				if ( saveCro )
					File.WriteAllBytes( croFiles[ i ], data );
			}
			Array.Sort( hashes, string.Compare );
			// Convert Hash Strings to Bytes
			byte[][] hashData = new byte[ hashes.Length ][];
			for ( int i = 0; i < hashes.Length; i++ )
				hashData[ i ] = Cro.StringToByteArray( hashes[ i ] );

			// Loop to check which CROs have to be updated. Do this separate from overwriting so we don't overwrite hashes for other CROs (yet).
			int updatedCtr = hashData.Select( t => Cro.IndexOfBytes( crr, t, 0, crr.Length ) ).Count( index => index < 0 );

			// Store Hashes in CRR
			for ( int i = 0; i < hashData.Length; i++ )
				Array.Copy( hashData[ i ], 0, crr, hashTableOffset + 0x20 * i, 0x20 );

			// Save File
			if ( saveCrr && updatedCtr > 0 )
			{
				File.WriteAllBytes( pathCrr, crr );
			}

			return true;
		}

		internal static byte[] HashCro( ref byte[] cro )
		{
			// Allocate new byte array to store modified CRO
			SHA256 mySha = SHA256.Create();

			// Compute the hashes
			byte[] hashH = mySha.ComputeHash( cro, 0x80, 0x100 );
			byte[] hash0 = mySha.ComputeHash( cro, BitConverter.ToInt32( cro, 0xB0 ), BitConverter.ToInt32( cro, 0xB4 ) );
			byte[] hash1 = mySha.ComputeHash( cro, BitConverter.ToInt32( cro, 0xC0 ), BitConverter.ToInt32( cro, 0xB8 ) - BitConverter.ToInt32( cro, 0xC0 ) );
			byte[] hash2 = mySha.ComputeHash( cro, BitConverter.ToInt32( cro, 0xB8 ), BitConverter.ToInt32( cro, 0xBC ) );

			// Set the hashes
			Array.Copy( hashH, 0, cro, 0x00, 0x20 );
			Array.Copy( hash0, 0, cro, 0x20, 0x20 );
			Array.Copy( hash1, 0, cro, 0x40, 0x20 );
			Array.Copy( hash2, 0, cro, 0x60, 0x20 );

			// Return the fixed overall hash
			return mySha.ComputeHash( cro, 0, 0x80 );
		}


		public Cro( byte[] data )
		{
			this.data = (byte[]) data.Clone();
		}

		private readonly byte[] data;

		private byte[] Sha2Hash
		{
			get
			{
				byte[] hashData = new byte[ 0x80 ];
				Array.Copy( this.data, hashData, 0x80 );
				return hashData;
			}
			set
			{
				if ( value.Length != 0x80 )
					throw new ArgumentOutOfRangeException( value.Length.ToString( "X5" ) );
				Array.Copy( value, this.data, value.Length );
			}
		}

		private string Magic => new string( this.data.Skip( 0x80 ).Take( 4 ).Select( c => (char) c ).ToArray() );
	}
}