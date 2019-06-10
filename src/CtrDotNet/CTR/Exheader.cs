using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace CtrDotNet.CTR
{
	public class Exheader
	{
		public readonly byte[] Data;
		public readonly byte[] AccessDescriptor;
		public readonly ulong TitleID;

		public Exheader( string exheaderPath )
		{
			this.Data = File.ReadAllBytes( exheaderPath );
			this.AccessDescriptor = this.Data.Skip( 0x400 ).Take( 0x400 ).ToArray();
			this.Data = this.Data.Take( 0x400 ).ToArray();
			this.TitleID = BitConverter.ToUInt64( this.Data, 0x200 );
		}

		public byte[] GetSuperBlockHash()
		{
			SHA256Managed sha = new SHA256Managed();
			return sha.ComputeHash( this.Data, 0, 0x400 );
		}

		public bool IsPokemon()
		{
			return this.IsOras() || this.IsXy();
		}

		public bool IsOras()
		{
			return ( ( this.TitleID & 0xFFFFFFFF ) >> 8 == 0x11C5 ) || ( ( this.TitleID & 0xFFFFFFFF ) >> 8 == 0x11C4 );
		}

		public bool IsXy()
		{
			return ( ( this.TitleID & 0xFFFFFFFF ) >> 8 == 0x55D ) || ( ( this.TitleID & 0xFFFFFFFF ) >> 8 == 0x55E );
		}

		public string GetPokemonSerial()
		{
			if ( !this.IsPokemon() )
				return "CTR-P-XXXX";
			string name;
			switch ( ( this.TitleID & 0xFFFFFFFF ) >> 8 )
			{
				case 0x11C5: //Alpha Sapphire
					name = "ECLA";
					break;
				case 0x11C4: //Omega Ruby
					name = "ECRA";
					break;
				case 0x55D: //X
					name = "EKJA";
					break;
				case 0x55E: //Y
					name = "EK2A";
					break;
				default:
					name = "XXXX";
					break;
			}
			return "CTR-P-" + name;
		}
	}
}