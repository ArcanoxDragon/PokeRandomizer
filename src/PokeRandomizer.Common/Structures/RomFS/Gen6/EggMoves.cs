using System;
using System.IO;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.RomFS.Gen6
{
	public class EggMoves : Common.EggMoves
	{
		public EggMoves(GameVersion gameVersion) : base(gameVersion) { }

		protected override void ReadData(BinaryReader br)
		{
			if (br.BaseStream.Length < 2)
			{
				Empty = true;
				Moves = Array.Empty<ushort>();
				return;
			}

			int numMoves = br.ReadUInt16();
			Empty = false;
			Moves = new ushort[numMoves];

			for (int i = 0; i < numMoves; i++)
				Moves[i] = br.ReadUInt16();
		}

		protected override void WriteData(BinaryWriter bw)
		{
			if (Empty)
				return;

			bw.Write((ushort) Count);

			for (int i = 0; i < Count; i++)
				bw.Write(Moves[i]);
		}
	}
}