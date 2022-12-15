using System.IO;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.RomFS.Gen7
{
	public class EncounterTrade : Common.EncounterStatic
	{
		#region Static

		public const int Size = 0x34;

		#endregion

		public EncounterTrade(GameVersion gameVersion) : base(gameVersion) { }

		protected override void ReadData(BinaryReader br)
		{
			Species = br.ReadUInt16();
			Unused0 = br.ReadInt16();
			Form = br.ReadByte();
			Level = br.ReadByte();

			IVs = new sbyte[6];
			for (int i = 0; i < IVs.Length; i++)
				IVs[i] = br.ReadSByte();

			Ability = br.ReadByte();
			Nature = br.ReadByte();
			Gender = br.ReadByte();
			TID = br.ReadUInt16();
			SID = br.ReadUInt16();

			short heldItem = br.ReadInt16();
			if (heldItem <= 0)
				heldItem = 0;
			HeldItem = heldItem;

			Unused1 = br.ReadInt32();
			TrainerGender = br.ReadByte();

			Unused2 = new byte[18];
			br.Read(Unused2, 0, Unused2.Length);

			TradeRequestSpecies = br.ReadUInt16();
		}

		protected override void WriteData(BinaryWriter bw)
		{
			bw.Write(Unused0);
			bw.Write(Form);
			bw.Write(Level);

			foreach (sbyte iv in IVs)
				bw.Write(iv);

			bw.Write(Ability);
			bw.Write(Nature);
			bw.Write(Gender);
			bw.Write(TID);
			bw.Write(SID);

			bw.Write(HeldItem == 0 ? -1 : HeldItem);

			bw.Write(Unused1);
			bw.Write(TrainerGender);

			bw.Write(Unused2, 0, Unused2.Length);

			bw.Write(TradeRequestSpecies);
		}

		public override ushort Species { get; set; }
		public          short  Unused0 { get; set; }
		public          byte   Form    { get; set; }
		public          byte   Level   { get; set; }

		// ReSharper disable once InconsistentNaming
		public sbyte[] IVs { get; set; }

		public          byte   Ability             { get; set; }
		public          byte   Nature              { get; set; }
		public          byte   Gender              { get; set; }
		public          ushort TID                 { get; set; }
		public          ushort SID                 { get; set; }
		public override short  HeldItem            { get; set; }
		public          int    Unused1             { get; set; }
		public          byte   TrainerGender       { get; set; }
		public          byte[] Unused2             { get; set; }
		public          ushort TradeRequestSpecies { get; set; }
	}
}