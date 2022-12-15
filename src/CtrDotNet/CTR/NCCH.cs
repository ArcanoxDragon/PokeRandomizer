using System;

namespace CtrDotNet.CTR
{
	public class Ncch
	{
		public       Header   HeaderData;
		public       ExeFS    Exefs;
		public       RomFS    Romfs;
		public       Exheader Exheader;
		public       byte[]   Logo;
		public       byte[]   Plainregion;
		public const uint     MediaUnitSize = 0x200;

		public class Header
		{
			public byte[] Signature; //Size: 0x100
			public uint   Magic;
			public uint   Size;
			public ulong  TitleId;
			public ushort MakerCode;

			public ushort FormatVersion;

			//public uint padding0;
			public ulong ProgramId;

			//public byte[0x10] padding1;
			public byte[] LogoHash; // Size: 0x20

			public byte[] ProductCode;  // Size: 0x10
			public byte[] ExheaderHash; // Size: 0x20

			public uint ExheaderSize;

			//public uint padding2;
			public byte[] Flags; // Size: 8

			public uint PlainRegionOffset;
			public uint PlainRegionSize;
			public uint LogoOffset;
			public uint LogoSize;
			public uint ExefsOffset;
			public uint ExefsSize;

			public uint ExefsSuperBlockSize;

			//public uint padding4;
			public uint RomfsOffset;

			public uint RomfsSize;

			public uint RomfsSuperBlockSize;

			//public uint padding5;
			public byte[] ExefsHash; // Size: 0x20

			public byte[] RomfsHash; // Size: 0x20

			public byte[] Data;

			public void BuildHeader()
			{
				this.Data = new byte[0x200];
				Array.Copy(this.Signature, this.Data, 0x100);
				Array.Copy(BitConverter.GetBytes(this.Magic), 0, this.Data, 0x100, 4);
				Array.Copy(BitConverter.GetBytes(this.Size), 0, this.Data, 0x104, 4);
				Array.Copy(BitConverter.GetBytes(this.TitleId), 0, this.Data, 0x108, 8);
				Array.Copy(BitConverter.GetBytes(this.MakerCode), 0, this.Data, 0x110, 2);
				Array.Copy(BitConverter.GetBytes(this.FormatVersion), 0, this.Data, 0x112, 2);
				//4 Byte Padding
				Array.Copy(BitConverter.GetBytes(this.ProgramId), 0, this.Data, 0x118, 8);
				//0x10 Byte Padding
				Array.Copy(this.LogoHash, 0, this.Data, 0x130, 0x20);
				Array.Copy(this.ProductCode, 0, this.Data, 0x150, 0x10);
				Array.Copy(this.ExheaderHash, 0, this.Data, 0x160, 0x20);
				Array.Copy(BitConverter.GetBytes(this.ExheaderSize), 0, this.Data, 0x180, 4);
				//4 Byte Padding
				Array.Copy(this.Flags, 0, this.Data, 0x188, 0x8);
				uint ofs = 0x190;
				foreach (uint val in new uint[] { this.PlainRegionOffset, this.PlainRegionSize, this.LogoOffset, this.LogoSize, this.ExefsOffset, this.ExefsSize, this.ExefsSuperBlockSize, 0, this.RomfsOffset, this.RomfsSize, this.RomfsSuperBlockSize, 0 })
				{
					Array.Copy(BitConverter.GetBytes(val), 0, this.Data, ofs, 4);
					ofs += 4;
				}

				Array.Copy(this.ExefsHash, 0, this.Data, 0x1C0, 0x20);
				Array.Copy(this.RomfsHash, 0, this.Data, 0x1E0, 0x20);
			}
		}
	}
}