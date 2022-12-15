using System.Diagnostics.CodeAnalysis;
using System.IO;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.RomFS.Common
{
	public class Move : BaseDataStructure
	{
		#region Static

		public const int CategoryStatus   = 0;
		public const int CategoryPhysical = 1;
		public const int CategorySpecial  = 2;

		#endregion

		public Move(GameVersion gameVersion) : base(gameVersion) { }

		public byte   Type           { get; set; }
		public byte   Quality        { get; set; }
		public byte   Category       { get; set; }
		public byte   Power          { get; set; }
		public byte   Accuracy       { get; set; }
		public byte   PP             { get; set; }
		public byte   Priority       { get; set; }
		public byte   InflictPercent { get; set; }
		public byte   HitMin         { get; set; }
		public byte   HitMax         { get; set; }
		public byte   TurnMin        { get; set; }
		public byte   TurnMax        { get; set; }
		public byte   CritStage      { get; set; }
		public byte   Flinch         { get; set; }
		public byte   Recoil         { get; set; }
		public byte   Targeting      { get; set; }
		public byte   Stat1          { get; set; }
		public byte   Stat2          { get; set; }
		public byte   Stat3          { get; set; }
		public byte   Stat1Stage     { get; set; }
		public byte   Stat2Stage     { get; set; }
		public byte   Stat3Stage     { get; set; }
		public byte   Stat1Percent   { get; set; }
		public byte   Stat2Percent   { get; set; }
		public byte   Stat3Percent   { get; set; }
		public Heal   Healing        { get; set; }
		public ushort Inflict        { get; set; }
		public ushort Effect         { get; set; }
		public byte   UnusedB        { get; set; }
		public byte   Unused1E       { get; set; }
		public byte   Unused1F       { get; set; }
		public byte   Unused20       { get; set; }
		public byte   Unused21       { get; set; }
		public byte   Unused22       { get; set; }
		public byte   Unused23       { get; set; }

		protected override void ReadData(BinaryReader br)
		{
			Type = br.ReadByte();
			Quality = br.ReadByte();
			Category = br.ReadByte();
			Power = br.ReadByte();
			Accuracy = br.ReadByte();
			PP = br.ReadByte();
			Priority = br.ReadByte();

			byte f7 = br.ReadByte();
			HitMin = (byte) ( f7 & 0b1111 );
			HitMax = (byte) ( f7 >> 4 );

			Inflict = br.ReadUInt16();
			InflictPercent = br.ReadByte();
			UnusedB = br.ReadByte();
			TurnMin = br.ReadByte();
			TurnMax = br.ReadByte();
			CritStage = br.ReadByte();
			Flinch = br.ReadByte();
			Effect = br.ReadUInt16();
			Recoil = br.ReadByte();
			Healing = new Heal(br.ReadByte());
			Targeting = br.ReadByte();
			Stat1 = br.ReadByte();
			Stat2 = br.ReadByte();
			Stat3 = br.ReadByte();
			Stat1Stage = br.ReadByte();
			Stat2Stage = br.ReadByte();
			Stat3Stage = br.ReadByte();
			Stat1Percent = br.ReadByte();
			Stat2Percent = br.ReadByte();
			Stat3Percent = br.ReadByte();
			Unused1E = br.ReadByte();
			Unused1F = br.ReadByte();
			Unused20 = br.ReadByte();
			Unused21 = br.ReadByte();
			Unused22 = br.ReadByte();
			Unused23 = br.ReadByte();
		}

		protected override void WriteData(BinaryWriter bw)
		{
			bw.Write(Type);
			bw.Write(Quality);
			bw.Write(Category);
			bw.Write(Power);
			bw.Write(Accuracy);
			bw.Write(PP);
			bw.Write(Priority);
			bw.Write((byte) ( HitMin | ( HitMax << 4 ) ));
			bw.Write(Inflict);
			bw.Write(InflictPercent);
			bw.Write(UnusedB);
			bw.Write(TurnMin);
			bw.Write(TurnMax);
			bw.Write(CritStage);
			bw.Write(Flinch);
			bw.Write(Effect);
			bw.Write(Recoil);
			bw.Write(Healing.Write());
			bw.Write(Targeting);
			bw.Write(Stat1);
			bw.Write(Stat2);
			bw.Write(Stat3);
			bw.Write(Stat1Stage);
			bw.Write(Stat2Stage);
			bw.Write(Stat3Stage);
			bw.Write(Stat1Percent);
			bw.Write(Stat2Percent);
			bw.Write(Stat3Percent);
			bw.Write(Unused1E);
			bw.Write(Unused1F);
			bw.Write(Unused20);
			bw.Write(Unused21);
			bw.Write(Unused22);
			bw.Write(Unused23);
		}

		public class Heal
		{
			public byte Raw     { get; set; }
			public bool Full    => Raw == 0xFF;
			public bool Half    => Raw == 0xFE;
			public bool Quarter => Raw == 0xFD;
			public bool Value   => Raw < 0xFD;

			public Heal(byte raw)
			{
				Raw = raw;
			}

			[SuppressMessage("ReSharper", "ConvertIfStatementToReturnStatement")]
			public byte Write()
			{
				if (Value)
					return Raw;
				if (Full)
					return 0xFF;
				if (Half)
					return 0xFE;
				if (Quarter)
					return 0xFD;
				return Raw;
			}
		}
	}
}