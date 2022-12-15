using System.IO;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.RomFS.Gen7
{
	public class EggMoves : Gen6.EggMoves
	{
		public EggMoves(GameVersion gameVersion) : base(gameVersion) { }

		public int FormTableIndex { get; set; }

		protected override void ReadData(BinaryReader r)
		{
			FormTableIndex = r.ReadUInt16();
			base.ReadData(r);
		}

		protected override void WriteData(BinaryWriter w)
		{
			w.Write((ushort) FormTableIndex);
			base.WriteData(w);
		}
	}
}