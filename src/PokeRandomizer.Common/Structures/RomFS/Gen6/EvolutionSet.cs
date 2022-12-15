﻿using System;
using System.IO;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Structures.RomFS.Common;

namespace PokeRandomizer.Common.Structures.RomFS.Gen6
{
	public class EvolutionSet : Common.EvolutionSet
	{
		public EvolutionSet(GameVersion gameVersion) : base(gameVersion) { }

		protected override int EntrySize  => 6;
		protected override int EntryCount => 8;

		public override void Read(byte[] data)
		{
			if (data.Length != Size)
				throw new ArgumentOutOfRangeException(nameof(data), $"Data array length must be {Size}, but was {data.Length}");

			base.Read(data);
		}

		protected override void ReadData(BinaryReader br)
		{
			PossibleEvolutions = new EvolutionMethod[EntryCount];
			for (int i = 0; i < PossibleEvolutions.Length; i++)
				PossibleEvolutions[i] = ReadMethod(br);
		}

		protected override void WriteData(BinaryWriter bw)
		{
			foreach (EvolutionMethod evo in PossibleEvolutions)
				WriteMethod(evo, bw);
		}

		protected virtual EvolutionMethod ReadMethod(BinaryReader r)
		{
			return new EvolutionMethod { Method = r.ReadUInt16(), Argument = r.ReadUInt16(), Species = r.ReadUInt16() };
		}

		protected virtual void WriteMethod(EvolutionMethod evo, BinaryWriter w)
		{
			w.Write((ushort) evo.Method);
			w.Write((ushort) evo.Argument);
			w.Write((ushort) evo.Species);
		}
	}
}