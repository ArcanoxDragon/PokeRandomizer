using System.Diagnostics;
using System.IO;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Utility;

namespace PokeRandomizer.Common.Structures.CRO.Common
{
	public abstract class BaseCroDataStructure : BaseDataStructure
	{
		protected BaseCroDataStructure(GameVersion gameVersion) : base(gameVersion) { }

		protected abstract int EntryCount  { get; }
		protected abstract int EntrySize   { get; }
		protected virtual  int EntryOffset => 0;

		protected virtual void ReadEntry(int entry, BinaryReader br) { }
		protected virtual void WriteEntry(int entry, BinaryWriter bw) { }

		protected override void ReadData(BinaryReader br)
		{
			br.BaseStream.Seek(EntryOffset, SeekOrigin.Begin);

			for (int i = 0; i < EntryCount; i++)
			{
				byte[] entry = new byte[EntrySize];
				int bytesRead = br.Read(entry, 0, entry.Length);
				int entryIndex = i;

				Debug.Assert(bytesRead == entry.Length);
				entry.WithReader(er => ReadEntry(entryIndex, er));
			}
		}

		protected override void WriteData(BinaryWriter bw)
		{
			bw.BaseStream.Seek(EntryOffset, SeekOrigin.Begin);

			for (int i = 0; i < EntryCount; i++)
			{
				using var ms = new MemoryStream();
				using var ew = new BinaryWriter(ms);
				WriteEntry(i, ew);
				byte[] entry = ms.ToArray();

				if (entry.Length != EntrySize)
					throw new InvalidDataException($"Entry size mismatch. Expected {EntrySize} but got {entry.Length}.");

				bw.Write(entry, 0, entry.Length);
			}
		}
	}
}