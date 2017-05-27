using System;
using System.IO;

namespace CtrDotNet.Pokemon.Structures.RomFS.Common
{
	public class Move : IDataStructure
	{
		public byte Type { get; set; }
		public byte Quality { get; set; }
		public byte Category { get; set; }
		public byte Power { get; set; }
		public byte Accuracy { get; set; }
		public byte PP { get; set; }
		public byte Priority { get; set; }
		public byte InflictPercent { get; set; }
		public byte HitMin { get; set; }
		public byte HitMax { get; set; }
		public byte TurnMin { get; set; }
		public byte TurnMax { get; set; }
		public byte CritStage { get; set; }
		public byte Flinch { get; set; }
		public byte Recoil { get; set; }
		public byte Targeting { get; set; }
		public byte Stat1 { get; set; }
		public byte Stat2 { get; set; }
		public byte Stat3 { get; set; }
		public byte Stat1Stage { get; set; }
		public byte Stat2Stage { get; set; }
		public byte Stat3Stage { get; set; }
		public byte Stat1Percent { get; set; }
		public byte Stat2Percent { get; set; }
		public byte Stat3Percent { get; set; }
		public Heal Healing { get; set; }
		public ushort Inflict { get; set; }
		public ushort Effect { get; set; }
		public byte UnusedB { get; set; }
		public byte Unused1E { get; set; }
		public byte Unused1F { get; set; }
		public byte Unused20 { get; set; }
		public byte Unused21 { get; set; }

		public Move( byte[] data )
		{
			this.Read( data );
		}

		public void Read( byte[] data )
		{
			this.Type = data[ 0 ];
			this.Quality = data[ 1 ];
			this.Category = data[ 2 ];
			this.Power = data[ 3 ];
			this.Accuracy = data[ 4 ];
			this.PP = data[ 5 ];
			this.Priority = data[ 6 ];
			this.HitMin = (byte) ( data[ 7 ] & 0xF );
			this.HitMax = (byte) ( data[ 7 ] >> 4 );
			this.Inflict = BitConverter.ToUInt16( data, 0x8 );
			this.InflictPercent = data[ 0xA ];
			this.UnusedB = data[ 0xB ];
			this.TurnMin = data[ 0xC ];
			this.TurnMax = data[ 0xD ];
			this.CritStage = data[ 0xE ];
			this.Flinch = data[ 0xF ];
			this.Effect = BitConverter.ToUInt16( data, 0x10 );
			this.Recoil = data[ 0x12 ];
			this.Healing = new Heal( data[ 0x13 ] );
			this.Targeting = data[ 0x14 ];
			this.Stat1 = data[ 0x15 ];
			this.Stat2 = data[ 0x16 ];
			this.Stat3 = data[ 0x17 ];
			this.Stat1Stage = data[ 0x18 ];
			this.Stat2Stage = data[ 0x19 ];
			this.Stat3Stage = data[ 0x1A ];
			this.Stat1Percent = data[ 0x1B ];
			this.Stat2Percent = data[ 0x1C ];
			this.Stat3Percent = data[ 0x1D ];
			this.Unused1E = data[ 0x1E ];
			this.Unused1F = data[ 0x1F ];
			this.Unused20 = data[ 0x20 ];
			this.Unused21 = data[ 0x21 ];
		}

		public byte[] Write()
		{
			using ( MemoryStream ms = new MemoryStream() )
			using ( BinaryWriter bw = new BinaryWriter( ms ) )
			{
				bw.Write( this.Type );
				bw.Write( this.Quality );
				bw.Write( this.Category );
				bw.Write( this.Power );
				bw.Write( this.Accuracy );
				bw.Write( this.PP );
				bw.Write( this.Priority );
				bw.Write( (byte) ( this.HitMin | ( this.HitMax << 4 ) ) );
				bw.Write( this.Inflict );
				bw.Write( this.InflictPercent );
				bw.Write( this.UnusedB );
				bw.Write( this.TurnMin );
				bw.Write( this.TurnMax );
				bw.Write( this.CritStage );
				bw.Write( this.Flinch );
				bw.Write( this.Effect );
				bw.Write( this.Recoil );
				bw.Write( this.Healing.Write() );
				bw.Write( this.Targeting );
				bw.Write( this.Stat1 );
				bw.Write( this.Stat2 );
				bw.Write( this.Stat3 );
				bw.Write( this.Stat1Stage );
				bw.Write( this.Stat2Stage );
				bw.Write( this.Stat3Stage );
				bw.Write( this.Stat1Percent );
				bw.Write( this.Stat2Percent );
				bw.Write( this.Stat3Percent );
				bw.Write( this.Unused1E );
				bw.Write( this.Unused1F );
				bw.Write( this.Unused20 );
				bw.Write( this.Unused21 );
				return ms.ToArray();
			}
		}

		public class Heal
		{
			public byte Val { get; set; }
			public bool Full, Half, Quarter, Value;

			public Heal( byte val )
			{
				this.Val = val;
				this.Full = this.Val == 0xFF;
				this.Half = this.Val == 0xFE;
				this.Quarter = this.Val == 0xFD;
				this.Value = this.Val < 0xFD;
			}

			public byte Write()
			{
				if ( this.Value )
					return this.Val;
				if ( this.Full )
					return 0xFF;
				if ( this.Half )
					return 0xFE;
				if ( this.Quarter )
					return 0xFD;
				return this.Val;
			}
		}
	}
}