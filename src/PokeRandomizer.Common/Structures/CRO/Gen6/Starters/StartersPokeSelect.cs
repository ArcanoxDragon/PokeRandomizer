using System;
using System.IO;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Utility;

namespace PokeRandomizer.Common.Structures.CRO.Gen6.Starters
{
	public class StartersPokeSelect : BaseStarters
	{
		#region Static

		public const int XyOffset  = 0x10;
		public const int EntrySize = 0x54;

		#endregion

		public StartersPokeSelect(GameVersion gameVersion) : base(gameVersion) { }

		/// <param name="br">Reader for the .data section of the DllPoke3Select file</param>
		public override void ReadData(BinaryReader br)
		{
			ClearStarters();

			if (!GameVersion.IsORAS())
				br.BaseStream.Seek(XyOffset, SeekOrigin.Begin);

			// Generations array is in order of how each generation appears in the file
			Generations.ForEach(gen => {
				for (int entry = 0; entry < StartersPerGen; entry++)
				{
					byte[] entryData = new byte[EntrySize];
					br.Read(entryData, 0, entryData.Length);
					StarterSpecies[gen - 1][entry] = BitConverter.ToUInt16(entryData, 0);
				}
			});
		}

		/// <param name="bw">Writer for the .data section of the DllPoke3Select file</param>
		public override void WriteData(BinaryWriter bw)
		{
			if (!GameVersion.IsORAS())
				bw.BaseStream.Seek(XyOffset, SeekOrigin.Begin);

			// Generations array is in order of how each generation appears in the file
			Generations.ForEach(gen => {
				for (int entry = 0; entry < StartersPerGen; entry++)
				{
					byte[] entryData = BitConverter.GetBytes(StarterSpecies[gen - 1][entry]);
					Array.Resize(ref entryData, EntrySize);
					bw.Write(entryData, 0, entryData.Length);
				}
			});
		}
	}
};