using System;
using System.Collections.Generic;
using System.IO;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Structures.RomFS.Gen6.ORAS;
using PokeRandomizer.Common.Structures.RomFS.Gen6.XY;
using PokeRandomizer.Common.Utility;

namespace PokeRandomizer.Common.Structures.RomFS.Gen6
{
	public abstract class EncounterWild : BaseDataStructure
	{
		#region Static

		public const int DataOffset = 0x10;

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

			#region Equality

			protected bool Equals( Entry other )
			{
				return this.Species == other.Species && this.Form == other.Form;
			}

			public override bool Equals( object obj )
			{
				if ( object.ReferenceEquals( null, obj ) )
					return false;
				if ( object.ReferenceEquals( this, obj ) )
					return true;
				return obj.GetType() == this.GetType() && this.Equals( (Entry) obj );
			}

			public override int GetHashCode()
			{
				unchecked
				{
					return ( this.Species.GetHashCode() * 397 ) ^ this.Form.GetHashCode();
				}
			}

			public static bool operator ==( Entry left, Entry right )
			{
				return object.Equals( left, right );
			}

			public static bool operator !=( Entry left, Entry right )
			{
				return !object.Equals( left, right );
			}

			#endregion
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

		private byte[] buffer;
		private int actualDataStart;

		protected EncounterWild( GameVersion gameVersion, int zoneId ) : base( gameVersion )
		{
			this.ZoneId = zoneId;
		}

		public int ZoneId { get; }
		public bool HasEntries { get; private set; }

		public abstract int DataStart { get; }
		public abstract int DataLength { get; }
		public abstract int NumEntries { get; }
		public abstract Entry[][] EntryArrays { get; set; }

		public IEnumerable<Entry> GetAllEntries() => this.AssembleEntries();

		public void SetAllEntries( Entry[] entries )
		{
			Assertions.AssertLength( (uint) this.NumEntries, entries, exact: true );

			this.ProcessEntries( entries );
		}

		protected override void ReadData( BinaryReader br )
		{
			this.buffer = new byte[ br.BaseStream.Length ];
			br.Read( this.buffer, 0, this.buffer.Length );

			br.BaseStream.Seek( DataOffset, SeekOrigin.Begin );

			this.actualDataStart = br.ReadInt32() + this.DataStart;
			int dataEnd = br.ReadInt32();
			int totalLength = dataEnd - this.actualDataStart;
			br.BaseStream.Seek( this.actualDataStart, SeekOrigin.Begin );

			if ( totalLength < this.DataLength )
				return; // No encounter data

			Entry[] entries = new Entry[ this.NumEntries ];
			this.HasEntries = true;

			entries.Fill( () => new Entry( this.GameVersion ) );

			for ( int i = 0; i < this.NumEntries; i++ )
			{
				byte[] entryData = new byte[ Entry.Size ];
				br.Read( entryData, 0, Entry.Size );

				entries[ i ].Read( entryData );
			}

			this.ProcessEntries( entries );
		}

		protected override void WriteData( BinaryWriter bw )
		{
			bw.Write( this.buffer, 0, this.buffer.Length );
			bw.BaseStream.Seek( DataOffset, SeekOrigin.Begin );

			bw.Write( this.actualDataStart - this.DataStart );
			bw.BaseStream.Seek( this.actualDataStart, SeekOrigin.Begin );

			if ( this.HasEntries )
			{
				this.AssembleEntries().ForEach( entry => {
					byte[] entryData = entry.Write();
					bw.Write( entryData, 0, entryData.Length );
				} );
			}
		}

		protected abstract void ProcessEntries( Entry[] entries );
		protected abstract IEnumerable<Entry> AssembleEntries();
	}
}