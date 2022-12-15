using System;

namespace PokeRandomizer.Common.Structures.RomFS.Gen7
{
	public class ZoneData : IDataStructure
	{
		public const int Size = 0x54;

		public ZoneData(byte[] data)
		{
			Data = (byte[]) ( data ?? new byte[Size] ).Clone();
		}

		public ZoneData(byte[] data, int index)
		{
			Data = new byte[Size];
			Array.Copy(data, index * Size, Data, 0, Size);
		}

		private byte[] Data         { get; set; }
		public  int    WorldIndex   { get; set; }
		public  int    AreaIndex    { get; set; }
		public  string Name         { get; private set; }
		public  string LocationName { get; private set; }

		// ZoneData Attributes
		public int ParentMap
		{
			get => BitConverter.ToInt32(Data, 0x1C);
			set => BitConverter.GetBytes(value).CopyTo(Data, 0x1C);
		}

		// Info Tracking
		public void SetName(string[] locationList, int index)
		{
			LocationName = locationList[ParentMap];
			Name = $"{index:000} - {LocationName}";
		}

		public void Read(byte[] data) => Data = (byte[]) data.Clone();

		public byte[] Write() => (byte[]) Data.Clone();
	}
}