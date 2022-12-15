using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.RomFS.Common
{
	public abstract class Learnset : BaseDataStructure
	{
		#region Static

		public static Learnset New(GameVersion version)
		{
			switch (version.GetGeneration())
			{
				case GameGeneration.Generation7:
					return new Gen7.Learnset(version);
				default:
					return new Gen6.Learnset(version);
			}
		}

		#endregion

		public int      Count  { get; set; }
		public ushort[] Moves  { get; set; }
		public ushort[] Levels { get; set; }

		protected Learnset(GameVersion gameVersion) : base(gameVersion) { }

		public IEnumerable<ushort> GetPossibleMoves(int level)
		{
			return Moves.Where(m => m > 0).TakeWhile((_, i) => Levels[i] <= level).Distinct();
		}

		public ushort[] GetMostLikelyMoves(int level)
		{
			return GetPossibleMoves(level).Reverse().Take(4).Reverse().ToArray();
		}

		public override void Read(byte[] data)
		{
			if (data.Length < 4 || data.Length % 4 != 0)
			{
				Count = 0;
				Levels = Array.Empty<ushort>();
				Moves = Array.Empty<ushort>();
				return;
			}

			Count = data.Length / 4 - 1;
			Levels = new ushort[Count];
			Moves = new ushort[Count];

			base.Read(data);
		}

		protected override void ReadData(BinaryReader br)
		{
			for (int i = 0; i < Count; i++)
			{
				Moves[i] = br.ReadUInt16();
				Levels[i] = br.ReadUInt16();
			}
		}

		protected override void WriteData(BinaryWriter bw)
		{
			Count = (ushort) Moves.Length;

			for (int i = 0; i < Count; i++)
			{
				bw.Write(Moves[i]);
				bw.Write(Levels[i]);
			}

			bw.Write(-1);
		}
	}
}