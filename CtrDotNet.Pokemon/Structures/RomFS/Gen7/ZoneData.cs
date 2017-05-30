using System;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen7
{
	public class ZoneData : IDataStructure
	{
		public const int Size = 0x54;

		public ZoneData( byte[] data )
		{
			this.Data = (byte[]) ( data ?? new byte[ Size ] ).Clone();
		}

		public ZoneData( byte[] data, int index )
		{
			this.Data = new byte[ Size ];
			Array.Copy( data, index * Size, this.Data, 0, Size );
		}

		private byte[] Data { get; set; }
		public int WorldIndex { get; set; }
		public int AreaIndex { get; set; }
		public string Name { get; private set; }
		public string LocationName { get; private set; }

		// ZoneData Attributes
		public int ParentMap
		{
			get => BitConverter.ToInt32( this.Data, 0x1C );
			set => BitConverter.GetBytes( value ).CopyTo( this.Data, 0x1C );
		}

		// Info Tracking
		public void SetName( string[] locationList, int index )
		{
			this.LocationName = locationList[ this.ParentMap ];
			this.Name = $"{index:000} - {this.LocationName}";
		}

		public void Read( byte[] data ) => this.Data = (byte[]) data.Clone();

		public byte[] Write() => (byte[]) this.Data.Clone();
	}
}