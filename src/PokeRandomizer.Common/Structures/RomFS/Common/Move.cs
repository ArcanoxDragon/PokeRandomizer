using System.Diagnostics.CodeAnalysis;
using System.IO;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.RomFS.Common
{
	public class Move : BaseDataStructure
	{
		#region Static

		public const int CategoryStatus = 0;
		public const int CategoryPhysical = 1;
		public const int CategorySpecial = 2;

		#endregion

		public Move( GameVersion gameVersion ) : base( gameVersion ) { }

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
		public byte Unused22 { get; set; }
		public byte Unused23 { get; set; }

		protected override void ReadData( BinaryReader br )
		{
			this.Type = br.ReadByte();
			this.Quality = br.ReadByte();
			this.Category = br.ReadByte();
			this.Power = br.ReadByte();
			this.Accuracy = br.ReadByte();
			this.PP = br.ReadByte();
			this.Priority = br.ReadByte();

			byte f7 = br.ReadByte();
			this.HitMin = (byte) ( f7 & 0b1111 );
			this.HitMax = (byte) ( f7 >> 4 );

			this.Inflict = br.ReadUInt16();
			this.InflictPercent = br.ReadByte();
			this.UnusedB = br.ReadByte();
			this.TurnMin = br.ReadByte();
			this.TurnMax = br.ReadByte();
			this.CritStage = br.ReadByte();
			this.Flinch = br.ReadByte();
			this.Effect = br.ReadUInt16();
			this.Recoil = br.ReadByte();
			this.Healing = new Heal( br.ReadByte() );
			this.Targeting = br.ReadByte();
			this.Stat1 = br.ReadByte();
			this.Stat2 = br.ReadByte();
			this.Stat3 = br.ReadByte();
			this.Stat1Stage = br.ReadByte();
			this.Stat2Stage = br.ReadByte();
			this.Stat3Stage = br.ReadByte();
			this.Stat1Percent = br.ReadByte();
			this.Stat2Percent = br.ReadByte();
			this.Stat3Percent = br.ReadByte();
			this.Unused1E = br.ReadByte();
			this.Unused1F = br.ReadByte();
			this.Unused20 = br.ReadByte();
			this.Unused21 = br.ReadByte();
			this.Unused22 = br.ReadByte();
			this.Unused23 = br.ReadByte();
		}

		protected override void WriteData( BinaryWriter bw )
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
			bw.Write( this.Unused22 );
			bw.Write( this.Unused23 );
		}

		public class Heal
		{
			public byte Raw { get; set; }
			public bool Full => this.Raw == 0xFF;
			public bool Half => this.Raw == 0xFE;
			public bool Quarter => this.Raw == 0xFD;
			public bool Value => this.Raw < 0xFD;

			public Heal( byte raw )
			{
				this.Raw = raw;
			}

			[ SuppressMessage( "ReSharper", "ConvertIfStatementToReturnStatement" ) ]
			public byte Write()
			{
				if ( this.Value )
					return this.Raw;
				if ( this.Full )
					return 0xFF;
				if ( this.Half )
					return 0xFE;
				if ( this.Quarter )
					return 0xFD;
				return this.Raw;
			}
		}
	}
}