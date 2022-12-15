using System.IO;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.RomFS.Common
{
	public class MegaEvolutions : BaseDataStructure
	{
		public MegaEvolutions(GameVersion gameVersion) : base(gameVersion) { }

		public ushort[] Form     { get; set; }
		public ushort[] Method   { get; set; }
		public ushort[] Argument { get; set; }
		public ushort[] Unused6  { get; set; }

		public override void Read(byte[] data)
		{
			if (data.Length < 0x10 || data.Length % 8 != 0)
				throw new InvalidDataException($"Invalid length for Mega Evolution structure: {data.Length}");

			base.Read(data);
		}

		protected override void ReadData(BinaryReader br)
		{
			Form = new ushort[br.BaseStream.Length / 8];
			Method = new ushort[br.BaseStream.Length / 8];
			Argument = new ushort[br.BaseStream.Length / 8];
			Unused6 = new ushort[br.BaseStream.Length / 8];

			for (int i = 0; i < Form.Length; i++)
			{
				Form[i] = br.ReadUInt16();
				Method[i] = br.ReadUInt16();
				Argument[i] = br.ReadUInt16();
				Unused6[i] = br.ReadUInt16();
			}
		}

		protected override void WriteData(BinaryWriter bw)
		{
			for (int i = 0; i < Form.Length; i++)
			{
				if (Method[i] == 0) // No method to evolve, clear information.
				{
					Form[i] = Argument[i] = 0;
				}

				bw.Write(Form[i]);
				bw.Write(Method[i]);
				bw.Write(Argument[i]);
				bw.Write(Unused6[i]);
			}
		}
	}
}