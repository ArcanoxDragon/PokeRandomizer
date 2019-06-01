using System;
using System.Collections.Generic;
using System.Linq;

namespace CtrDotNet.CTR
{
	public class Ncsd
	{
		public Header HeaderData;
		public CardInfoHeader Cardinfoheader;
		public List<Ncch> NcchArray;
		public bool Card2;
		public byte[] Data;

		public class Header
		{
			public byte[] Signature; //Size 0x100;
			public uint Magic;
			public uint MediaSize;

			public ulong TitleId;

			//public byte[] padding; //Size: 0x10
			public NcchMeta[] OffsetSizeTable; //Size: 8

			//public byte[] padding; //Size: 0x28
			public byte[] Flags; //Size: 0x8

			public ulong[] NcchIdTable; //Size: 0x8;
			//public byte[] Padding2; //Size: 0x30;
		}

		public class CardInfoHeader
		{
			public uint WritableAddress;
			public uint CardInfoBitmask;
			public CardInfoNotes Cin;
			public ulong Ncch0TitleId;
			public ulong Reserved0;
			public byte[] InitialData; // Size: 0x30
			public byte[] Reserved1; // Size: 0xC0
			public byte[] Ncch0Header; // Size: 0x100

			public class CardInfoNotes
			{
				public byte[] Reserved0; // Size: 0xF8;
				public ulong MediaSizeUsed;
				public ulong Reserved1;
				public uint Unknown;
				public byte[] Reserved2; //Size: 0xC;
				public ulong CVerTitleId;
				public ushort CVerTitleVersion;
				public byte[] Reserved3; //Size: 0xCD6;
			}
		}

		public class NcchMeta
		{
			public uint Offset;
			public uint Size;
		}

		public ulong GetWritableAddress()
		{
			const ulong mediaUnitSize = 0x200;
			return this.Card2
					   ? Ncsd.Align( this.HeaderData.OffsetSizeTable[ this.NcchArray.Count - 1 ].Offset * Ncch.MediaUnitSize
									 + this.HeaderData.OffsetSizeTable[ this.NcchArray.Count - 1 ].Size * Ncch.MediaUnitSize + 0x1000, 0x10000 ) / mediaUnitSize
					   : 0x00000000FFFFFFFF;
		}

		public void BuildHeader()
		{
			this.Data = new byte[ 0x4000 ];
			Array.Copy( this.HeaderData.Signature, this.Data, 0x100 );
			Array.Copy( BitConverter.GetBytes( this.HeaderData.Magic ), 0, this.Data, 0x100, 4 );
			Array.Copy( BitConverter.GetBytes( this.HeaderData.MediaSize ), 0, this.Data, 0x104, 4 );
			Array.Copy( BitConverter.GetBytes( this.HeaderData.TitleId ), 0, this.Data, 0x108, 8 );
			for ( int i = 0; i < this.HeaderData.OffsetSizeTable.Length; i++ )
			{
				Array.Copy( BitConverter.GetBytes( this.HeaderData.OffsetSizeTable[ i ].Offset ), 0, this.Data, 0x120 + 8 * i, 4 );
				Array.Copy( BitConverter.GetBytes( this.HeaderData.OffsetSizeTable[ i ].Size ), 0, this.Data, 0x124 + 8 * i, 4 );
			}
			Array.Copy( this.HeaderData.Flags, 0, this.Data, 0x188, this.HeaderData.Flags.Length );
			for ( int i = 0; i < this.HeaderData.NcchIdTable.Length; i++ )
			{
				Array.Copy( BitConverter.GetBytes( this.HeaderData.NcchIdTable[ i ] ), 0, this.Data, 0x190 + 8 * i, 8 );
			}
			//CardInfoHeader
			Array.Copy( BitConverter.GetBytes( this.Cardinfoheader.WritableAddress ), 0, this.Data, 0x200, 4 );
			Array.Copy( BitConverter.GetBytes( this.Cardinfoheader.CardInfoBitmask ), 0, this.Data, 0x204, 4 );
			Array.Copy( this.Cardinfoheader.Cin.Reserved0, 0, this.Data, 0x208, this.Cardinfoheader.Cin.Reserved0.Length );
			Array.Copy( BitConverter.GetBytes( this.Cardinfoheader.Cin.MediaSizeUsed ), 0, this.Data, 0x300, 8 );
			Array.Copy( BitConverter.GetBytes( this.Cardinfoheader.Cin.Reserved1 ), 0, this.Data, 0x308, 8 );
			Array.Copy( BitConverter.GetBytes( this.Cardinfoheader.Cin.Unknown ), 0, this.Data, 0x310, 4 );
			Array.Copy( this.Cardinfoheader.Cin.Reserved2, 0, this.Data, 0x314, this.Cardinfoheader.Cin.Reserved2.Length );
			Array.Copy( BitConverter.GetBytes( this.Cardinfoheader.Cin.CVerTitleId ), 0, this.Data, 0x320, 8 );
			Array.Copy( BitConverter.GetBytes( this.Cardinfoheader.Cin.CVerTitleVersion ), 0, this.Data, 0x328, 2 );
			Array.Copy( this.Cardinfoheader.Cin.Reserved3, 0, this.Data, 0x32A, this.Cardinfoheader.Cin.Reserved3.Length );
			Array.Copy( BitConverter.GetBytes( this.Cardinfoheader.Ncch0TitleId ), 0, this.Data, 0x1000, 8 );
			Array.Copy( BitConverter.GetBytes( this.Cardinfoheader.Reserved0 ), 0, this.Data, 0x1008, 8 );
			Array.Copy( this.Cardinfoheader.InitialData, 0, this.Data, 0x1010, this.Cardinfoheader.InitialData.Length );
			Array.Copy( this.Cardinfoheader.Reserved1, 0, this.Data, 0x1040, this.Cardinfoheader.Reserved1.Length );
			Array.Copy( this.Cardinfoheader.Ncch0Header, 0, this.Data, 0x1100, this.Cardinfoheader.Ncch0Header.Length );
			Array.Copy( Enumerable.Repeat( (byte) 0xFF, 0x2E00 ).ToArray(), 0, this.Data, 0x1200, 0x2E00 );
		}

		internal static ulong Align( ulong input, ulong alignsize )
		{
			ulong output = input;
			if ( output % alignsize != 0 )
			{
				output += alignsize - output % alignsize;
			}
			return output;
		}
	}
}