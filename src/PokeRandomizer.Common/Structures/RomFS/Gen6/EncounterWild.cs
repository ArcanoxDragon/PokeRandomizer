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

			public Entry(GameVersion gameVersion) : base(gameVersion) { }

			public ushort Species  { get; set; }
			public ushort Form     { get; set; }
			public byte   MinLevel { get; set; }
			public byte   MaxLevel { get; set; }

			protected override void ReadData(BinaryReader br)
			{
				ushort flag0 = br.ReadUInt16();
				Species = (ushort) ( flag0 & 0x7FF );
				Form = (ushort) ( flag0 >> 11 );

				MinLevel = br.ReadByte();
				MaxLevel = br.ReadByte();
			}

			protected override void WriteData(BinaryWriter bw)
			{
				ushort flag0 = 0;
				flag0 |= (ushort) ( Species & 0x7FF );
				flag0 |= (ushort) ( Form << 11 );
				bw.Write(flag0);

				bw.Write(MinLevel);
				bw.Write(MaxLevel);
			}

			#region Equality

			protected bool Equals(Entry other)
			{
				return Species == other.Species && Form == other.Form;
			}

			public override bool Equals(object obj)
			{
				if (ReferenceEquals(null, obj))
					return false;
				if (ReferenceEquals(this, obj))
					return true;
				return obj.GetType() == GetType() && Equals((Entry) obj);
			}

			public override int GetHashCode()
			{
				unchecked
				{
					return ( Species.GetHashCode() * 397 ) ^ Form.GetHashCode();
				}
			}

			public static bool operator ==(Entry left, Entry right)
			{
				return Equals(left, right);
			}

			public static bool operator !=(Entry left, Entry right)
			{
				return !Equals(left, right);
			}

			#endregion
		}

		public static EncounterWild New(GameVersion version, int zoneId)
		{
			switch (version)
			{
				case GameVersion.ORAS:
				case GameVersion.ORASDemo:
					return new OrasEncounterWild(version, zoneId);
				case GameVersion.XY:
					return new XyEncounterWild(version, zoneId);
				default:
					throw new NotSupportedException($"Version not supported: {version}");
			}
		}

		#endregion

		private byte[] buffer;
		private int    actualDataStart;

		protected EncounterWild(GameVersion gameVersion, int zoneId) : base(gameVersion)
		{
			ZoneId = zoneId;
		}

		public int  ZoneId     { get; }
		public bool HasEntries { get; private set; }

		public abstract int       DataStart   { get; }
		public abstract int       DataLength  { get; }
		public abstract int       NumEntries  { get; }
		public abstract Entry[][] EntryArrays { get; set; }

		public IEnumerable<Entry> GetAllEntries() => AssembleEntries();

		public void SetAllEntries(Entry[] entries)
		{
			Assertions.AssertLength((uint) NumEntries, entries, exact: true);

			ProcessEntries(entries);
		}

		protected override void ReadData(BinaryReader br)
		{
			this.buffer = new byte[br.BaseStream.Length];
			br.Read(this.buffer, 0, this.buffer.Length);

			br.BaseStream.Seek(DataOffset, SeekOrigin.Begin);

			this.actualDataStart = br.ReadInt32() + DataStart;
			int dataEnd = br.ReadInt32();
			int totalLength = dataEnd - this.actualDataStart;
			br.BaseStream.Seek(this.actualDataStart, SeekOrigin.Begin);

			if (totalLength < DataLength)
				return; // No encounter data

			Entry[] entries = new Entry[NumEntries];
			HasEntries = true;

			entries.Fill(() => new Entry(GameVersion));

			for (int i = 0; i < NumEntries; i++)
			{
				byte[] entryData = new byte[Entry.Size];
				br.Read(entryData, 0, Entry.Size);

				entries[i].Read(entryData);
			}

			ProcessEntries(entries);
		}

		protected override void WriteData(BinaryWriter bw)
		{
			bw.Write(this.buffer, 0, this.buffer.Length);
			bw.BaseStream.Seek(DataOffset, SeekOrigin.Begin);

			bw.Write(this.actualDataStart - DataStart);
			bw.BaseStream.Seek(this.actualDataStart, SeekOrigin.Begin);

			if (HasEntries)
			{
				AssembleEntries().ForEach(entry => {
					byte[] entryData = entry.Write();
					bw.Write(entryData, 0, entryData.Length);
				});
			}
		}

		protected abstract void ProcessEntries(Entry[] entries);
		protected abstract IEnumerable<Entry> AssembleEntries();
	}
}