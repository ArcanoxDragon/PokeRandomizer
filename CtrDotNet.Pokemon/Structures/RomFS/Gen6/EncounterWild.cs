using System;
using System.IO;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Structures.RomFS.Gen6.ORAS;
using CtrDotNet.Pokemon.Structures.RomFS.Gen6.XY;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen6
{
	public abstract class EncounterWild : BaseDataStructure
	{
		#region Static

		private const int DataOffset = 0x10;

		public class Entry : BaseDataStructure
		{
			public const int Size = 0x4;

			public Entry( GameVersion gameVersion ) : base( gameVersion ) { }

			public ushort Species { get; set; }
			public ushort Form { get; set; }
			public byte MinLevel { get; set; }
			public byte MaxLevel { get; set; }

			protected override void ReadData( BinaryReader br )
			{
				ushort flag0 = br.ReadUInt16();
				this.Species = (ushort) ( flag0 & 0x7FF );
				this.Form = (ushort) ( flag0 >> 11 );

				this.MinLevel = br.ReadByte();
				this.MaxLevel = br.ReadByte();
			}

			protected override void WriteData( BinaryWriter bw )
			{
				ushort flag0 = 0;
				flag0 |= (ushort) ( this.Species & 0x7FF );
				flag0 |= (ushort) ( this.Form << 11 );
				bw.Write( flag0 );

				bw.Write( this.MinLevel );
				bw.Write( this.MaxLevel );
			}
		}

		public static EncounterWild New( GameVersion version, int zoneId )
		{
			switch ( version )
			{
				case GameVersion.ORAS:
				case GameVersion.ORASDemo:
					return new OrasEncounterWild( version, zoneId );
				case GameVersion.XY:
					return new XyEncounterWild( version, zoneId );
				default:
					throw new NotSupportedException( $"Version not supported: {version}" );
			}
		}

		#endregion

		private byte[] header;
		private byte[] footer;
		private int actualDataStart;

		protected EncounterWild( GameVersion gameVersion, int zoneId ) : base( gameVersion )
		{
			this.ZoneId = zoneId;
		}

		public int ZoneId { get; }

		protected abstract int DataStart { get; }
		protected abstract int DataLength { get; }
		protected abstract int NumEntries { get; }

		public Entry[] GetAllEntries() => this.AssembleEntries();

		public void SetAllEntries( Entry[] entries )
		{
			Assertions.AssertLength( (uint) this.NumEntries, entries, exact: true );

			this.ProcessEntries( entries );
		}

		protected override void ReadData( BinaryReader br )
		{
			br.BaseStream.Seek( DataOffset, SeekOrigin.Begin );

			this.actualDataStart = br.ReadInt32() + this.DataStart;
			int dataEnd = br.ReadInt32();
			int totalLength = dataEnd - this.actualDataStart;

			if ( totalLength < this.DataLength )
				return; // No encounter data

			this.header = new byte[ this.actualDataStart ];
			br.BaseStream.Seek( 0, SeekOrigin.Begin );
			br.Read( this.header, 0, this.actualDataStart );

			Entry[] entries = new Entry[ this.NumEntries ];

			for ( int i = 0; i < this.NumEntries; i++ )
			{
				byte[] entryData = new byte[ Entry.Size ];
				br.Read( entryData, 0, Entry.Size );

				entries[ i ].Read( entryData );
			}

			this.ProcessEntries( entries );

			int footerLength = (int) ( br.BaseStream.Length - br.BaseStream.Position );
			this.footer = new byte[ footerLength ];
			br.Read( this.footer, 0, footerLength );
		}

		protected override void WriteData( BinaryWriter bw )
		{
			bw.Write( this.header, 0, this.header.Length );
			bw.BaseStream.Seek( DataOffset, SeekOrigin.Begin );

			bw.Write( this.actualDataStart - this.DataStart );
			bw.BaseStream.Seek( this.actualDataStart, SeekOrigin.Begin );

			this.AssembleEntries().ForEach( entry => {
				byte[] entryData = entry.Write();
				bw.Write( entryData, 0, entryData.Length );
			} );

			bw.Write( this.footer, 0, this.footer.Length );
		}

		protected abstract void ProcessEntries( Entry[] entries );
		protected abstract Entry[] AssembleEntries();
	}
}