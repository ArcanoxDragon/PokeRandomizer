using System.Diagnostics.CodeAnalysis;
using System.IO;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.RomFS.Gen6
{
	[SuppressMessage("ReSharper", "AutoPropertyCanBeMadeGetOnly.Global")]
	public class EncounterGift : Common.EncounterStatic
	{
		#region Common

		public override ushort Species    { get; set; }
		public          ushort Unused2    { get; set; }
		public          byte   Form       { get; set; }
		public          byte   Level      { get; set; }
		public          sbyte  Ability    { get; set; }
		public          sbyte  Nature     { get; set; }
		public          byte   Shiny      { get; set; }
		public          byte   Unused9    { get; set; }
		public          byte   UnusedA    { get; set; }
		public          byte   UnusedB    { get; set; }
		public override short  HeldItem   { get; set; }
		public          sbyte  Gender     { get; set; }
		public          byte   Unused22   { get; set; }
		public          byte   UnusedLast { get; set; }

		// ReSharper disable once InconsistentNaming
		public sbyte[] IVs { get; set; } = new sbyte[6];

		#endregion

		#region ORAS

		public byte   Unused11     { get; set; }
		public short  MetLocation  { get; set; }
		public ushort Move         { get; set; }
		public byte[] ContestStats { get; set; }

		#endregion

		public EncounterGift(GameVersion gameVersion) : base(gameVersion) { }

		protected override void ReadData(BinaryReader br)
		{
			Species = br.ReadUInt16();
			Unused2 = br.ReadUInt16();
			Form = br.ReadByte();
			Level = br.ReadByte();
			Shiny = br.ReadByte();
			Ability = br.ReadSByte();
			Nature = br.ReadSByte();
			Unused9 = br.ReadByte();
			UnusedA = br.ReadByte();
			UnusedB = br.ReadByte();
			HeldItem = (short) br.ReadInt32();
			Gender = br.ReadSByte();

			if (GameVersion.IsORAS())
			{
				Unused11 = br.ReadByte();
				MetLocation = br.ReadInt16();
				Move = br.ReadUInt16();
			}

			for (int i = 0; i < 6; i++)
				IVs[i] = br.ReadSByte();

			if (GameVersion.IsORAS())
			{
				ContestStats = br.ReadBytes(6);
				Unused22 = br.ReadByte();
			}

			UnusedLast = br.ReadByte();
		}

		protected override void WriteData(BinaryWriter bw)
		{
			bw.Write(Species);
			bw.Write(Unused2);
			bw.Write(Form);
			bw.Write(Level);
			bw.Write(Shiny);
			bw.Write(Ability);
			bw.Write(Nature);
			bw.Write(Unused9);
			bw.Write(UnusedA);
			bw.Write(UnusedB);
			bw.Write(HeldItem);
			bw.Write(Gender);

			if (GameVersion.IsORAS())
			{
				bw.Write(Unused11);
				bw.Write(MetLocation);
				bw.Write(Move);
			}

			for (int i = 0; i < 6; i++)
				bw.Write(IVs[i]);

			if (GameVersion.IsORAS())
			{
				bw.Write(ContestStats);
				bw.Write(Unused22);
			}

			bw.Write(UnusedLast);
		}
	}
}