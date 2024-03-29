﻿using System.IO;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Utility;

namespace PokeRandomizer.Common.Structures
{
	public abstract class BaseDataStructure : IDataStructure
	{
		protected BaseDataStructure(GameVersion gameVersion)
		{
			GameVersion = gameVersion;
		}

		public GameVersion GameVersion { get; }

		public virtual void Read(byte[] data)
		{
			data.WithReader(ReadData);
		}

		public virtual byte[] Write()
		{
			using var ms = new MemoryStream();
			using var bw = new BinaryWriter(ms);
			WriteData(bw);
			return ms.ToArray();
		}

		protected virtual void ReadData(BinaryReader br) { }
		protected virtual void WriteData(BinaryWriter bw) { }
	}
}