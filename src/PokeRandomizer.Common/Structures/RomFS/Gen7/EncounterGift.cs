using System.IO;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.RomFS.Gen7
{
	public class EncounterGift : Common.EncounterStatic
	{
		public EncounterGift(GameVersion gameVersion) : base(gameVersion) { }

		public override ushort Species  { get; set; }
		public          byte   Form     { get; set; }
		public          byte   Level    { get; set; }
		public          byte   Gender   { get; set; }
		public          byte[] Unused1  { get; private set; }
		public override short  HeldItem { get; set; }

		protected override void ReadData(BinaryReader br)
		{
			Species = br.ReadUInt16();
			Form = br.ReadByte();
			Level = br.ReadByte();
			Gender = br.ReadByte();
			Unused1 = br.ReadBytes(3);
			HeldItem = (short) br.ReadUInt16();
		}

		protected override void WriteData(BinaryWriter bw)
		{
			bw.Write(Species);
			bw.Write(Form);
			bw.Write(Level);
			bw.Write(Gender);
			bw.Write(Unused1, 0, 3);
			bw.Write((ushort) HeldItem);
		}
	}
}