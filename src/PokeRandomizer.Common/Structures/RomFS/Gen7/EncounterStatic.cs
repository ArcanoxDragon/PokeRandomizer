using System.IO;
using System.Linq;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.RomFS.Gen7
{
	public class EncounterStatic : Gen6.EncounterStatic
	{
		public EncounterStatic(GameVersion gameVersion) : base(gameVersion) { }

		// ReSharper disable once InconsistentNaming
		public sbyte[] IVs { get; set; }

		public ushort[] RelearnMoves { get; set; }
		public int      Unused1      { get; set; }

		protected override void ReadData(BinaryReader r)
		{
			base.ReadData(r);

			Unused1 = r.ReadInt32();
			RelearnMoves = Enumerable.Range(0, 4).Select(_ => r.ReadUInt16()).ToArray();
			IVs = Enumerable.Range(0, 6).Select(_ => r.ReadSByte()).ToArray();
		}

		protected override void WriteData(BinaryWriter w)
		{
			base.WriteData(w);

			w.Write(Unused1);
			RelearnMoves.ToList().ForEach(w.Write);
			IVs.ToList().ForEach(w.Write);
		}
	}
}