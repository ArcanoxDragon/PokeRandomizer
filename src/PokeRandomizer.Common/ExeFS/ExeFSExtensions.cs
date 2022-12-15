using System;
using System.Linq;
using PokeRandomizer.Common.Structures.ExeFS.Common;
using PokeRandomizer.Common.Utility;

namespace PokeRandomizer.Common.ExeFS
{
	public static class ExeFSExtensions
	{
		public static void WriteStructure(this CodeBin codeBin, BaseExeFsStructure structure)
		{
			byte[] exeData = codeBin.Data;
			var matches = exeData.FindSequence(structure.Signature, 0x400000).ToList();

			if (!matches.Any())
				throw new InvalidOperationException("Could not find signature in binary file");

			int offset = matches.First();

			if (!structure.IncludeSignature)
				offset += structure.Signature.Length;

			byte[] structureData = structure.Write();

			Array.Copy(structureData, 0, exeData, offset, structureData.Length);
		}

		public static void ReadFromCodeBin(this BaseExeFsStructure structure, CodeBin codeBin)
		{
			byte[] data = codeBin.Data;
			var matches = data.FindSequence(structure.Signature, 0x400000).ToList();

			if (!matches.Any())
				throw new InvalidOperationException("Could not find signature in binary file");

			int offset = matches.First();

			if (!structure.IncludeSignature)
				offset += structure.Signature.Length;

			byte[] structData = data.Skip(offset).Take(structure.Length).ToArray();

			structure.Read(structData);
		}
	}
}