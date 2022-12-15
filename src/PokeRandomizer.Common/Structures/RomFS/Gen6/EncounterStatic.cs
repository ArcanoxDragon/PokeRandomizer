using System.IO;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.RomFS.Gen6
{
	public class EncounterStatic : Common.EncounterStatic
	{
		public EncounterStatic(GameVersion gameVersion) : base(gameVersion) { }

		public override short  HeldItem  { get; set; }
		public override ushort Species   { get; set; }
		public          byte   Form      { get; set; }
		public          byte   Level     { get; set; }
		public          int    Gender    { get; set; }
		public          int    Ability   { get; set; }
		public          bool   ShinyLock { get; set; }
		public          bool   IVLower   { get; set; }
		public          bool   IVUpper   { get; set; }

		protected override void ReadData(BinaryReader br)
		{
			Species = br.ReadUInt16();
			Form = br.ReadByte();
			Level = br.ReadByte();

			short heldItem = br.ReadInt16();
			if (heldItem <= 0)
				heldItem = 0;
			HeldItem = heldItem;

			byte flag6 = br.ReadByte();
			Gender = ( flag6 & 0b1100 ) >> 2;
			Ability = ( flag6 & 0b1110000 ) >> 4;
			ShinyLock = ( flag6 & 0b10 ) > 0;

			byte flag7 = br.ReadByte();
			IVLower = ( flag7 & 0b01 ) > 0;
			IVUpper = ( flag7 & 0b10 ) > 0;
		}

		protected override void WriteData(BinaryWriter bw)
		{
			bw.Write(Species);
			bw.Write(Form);
			bw.Write(Level);
			bw.Write((short) ( HeldItem == 0 ? -1 : HeldItem ));

			int flag6 = 0;
			flag6 |= ( Gender & 0b11 ) << 2;
			flag6 |= ( Ability & 0b111 ) << 4;
			flag6 |= ShinyLock ? 0b10 : 0;

			int flag7 = 0;
			flag7 |= IVUpper ? 0b10 : 0;
			flag7 |= IVLower ? 0b01 : 0;

			bw.Write((byte) flag6);
			bw.Write((byte) flag7);
		}
	}
}