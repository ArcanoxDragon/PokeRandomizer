namespace CtrDotNet.CTR.Garc
{
	public struct GarcDef
	{
		#region Structs

		public struct FatoDef
		{
			public char[] Magic;
			public int HeaderSize;
			public ushort EntryCount;
			public ushort Padding;

			public FatoEntry[] Entries;
		}

		public struct FatoEntry
		{
			public int Offset;
		}

		public struct FatbDef
		{
			public char[] Magic;
			public int HeaderSize;
			public int FileCount;

			public FatbEntry[] Entries;
		}

		public struct FatbEntry
		{
			public uint Vector;
			public bool IsFolder;
			public FatbSubEntry[] SubEntries;
		}

		public struct FatbSubEntry
		{
			public bool Exists;
			public int Start;
			public int End;
			public int Length;

			// Unsaved Properties
			public int Padding { get; set; }
		}

		public struct FimgDef
		{
			public char[] Magic;
			public int HeaderSize;
			public int DataSize;
		}

		#endregion

		public char[] Magic;
		public uint HeaderSize;
		public ushort Endianess;
		public ushort Version;
		public uint ChunkCount;

		public uint DataOffset;
		public uint FileSize;

		public uint ContentLargestPadded;
		public uint ContentLargestUnpadded;
		public uint ContentPadToNearest;

		public FatoDef Fato;
		public FatbDef Fatb;
		public FimgDef Fimg;
	}
}