using System;
using System.IO;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.RomFS.Common
{
	public abstract class EggMoves : BaseDataStructure
	{
		#region Static

		public static EggMoves New(GameVersion version)
		{
			switch (version.GetGeneration())
			{
				case GameGeneration.Generation7:
					return new Gen7.EggMoves(version);
				default:
					return new Gen6.EggMoves(version);
			}
		}

		#endregion

		protected EggMoves(GameVersion gameVersion) : base(gameVersion) { }

		public bool     Empty { get; protected set; }
		public int      Count => Moves.Length;
		public ushort[] Moves { get; set; }

		protected override void ReadData(BinaryReader br)
		{
			Moves = Array.Empty<ushort>();
			Empty = true;
		}
	}
}