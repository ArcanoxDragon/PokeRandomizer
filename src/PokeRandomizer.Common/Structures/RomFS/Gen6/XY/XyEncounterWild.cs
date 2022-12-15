using System.Collections.Generic;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Utility;

namespace PokeRandomizer.Common.Structures.RomFS.Gen6.XY
{
	public class XyEncounterWild : EncounterWild
	{
		#region Layout Data (XY)

		/**
		 * 12 Tall Grass
		 * 12 Yellow Flowers
		 * 12 Purple Flowers
		 * 12 Red Flowers
		 * 12 Rough Terrain
		 * 5 Surf
		 * 5 Rock Smash (Becky, please)
		 * 3 Old Rod
		 * 3 Good Rod
		 * 3 Super Rod
		 * 5 "A" Horde
		 * 5 "B" Horde
		 * 5 "C" Horde
		 */
		public const int NumTallGrass = 12;

		public const int NumYellowFlowers = 12;
		public const int NumPurpleFlowers = 12;
		public const int NumRedFlowers    = 12;
		public const int NumRoughTerrain  = 12;
		public const int NumSurf          = 5;
		public const int NumRockSmash     = 5;
		public const int NumOldRod        = 3;
		public const int NumGoodRod       = 3;
		public const int NumSuperRod      = 3;
		public const int NumHordeA        = 5;
		public const int NumHordeB        = 5;
		public const int NumHordeC        = 5;

		#endregion

		public XyEncounterWild(GameVersion gameVersion, int zoneId) : base(gameVersion, zoneId)
		{
			FillEmpty();
		}

		public override int DataStart  => 0x10;
		public override int DataLength => 0x178;

		public override int NumEntries => NumTallGrass +
										  NumYellowFlowers +
										  NumPurpleFlowers +
										  NumRedFlowers +
										  NumRoughTerrain +
										  NumSurf +
										  NumRockSmash +
										  NumOldRod +
										  NumGoodRod +
										  NumSuperRod +
										  NumHordeA +
										  NumHordeB +
										  NumHordeC;

		public Entry[] TallGrass     { get; private set; }
		public Entry[] YellowFlowers { get; private set; }
		public Entry[] PurpleFlowers { get; private set; }
		public Entry[] RedFlowers    { get; private set; }
		public Entry[] RoughTerrain  { get; private set; }
		public Entry[] Surf          { get; private set; }
		public Entry[] RockSmash     { get; private set; }
		public Entry[] OldRod        { get; private set; }
		public Entry[] GoodRod       { get; private set; }
		public Entry[] SuperRod      { get; private set; }
		public Entry[] HordeA        { get; private set; }
		public Entry[] HordeB        { get; private set; }
		public Entry[] HordeC        { get; private set; }

		public override Entry[][] EntryArrays
		{
			get => new[] { TallGrass, YellowFlowers, PurpleFlowers, RedFlowers, RoughTerrain, Surf, RockSmash, OldRod, GoodRod, SuperRod, HordeA, HordeB, HordeC };
			set
			{
				Assertions.AssertLength(13, value, true);

				TallGrass = value[0];
				YellowFlowers = value[1];
				PurpleFlowers = value[2];
				RedFlowers = value[3];
				RoughTerrain = value[4];
				Surf = value[5];
				RockSmash = value[6];
				OldRod = value[7];
				GoodRod = value[8];
				SuperRod = value[9];
				HordeA = value[10];
				HordeB = value[11];
				HordeC = value[12];
			}
		}

		private void FillEmpty()
		{
			TallGrass = new Entry[NumTallGrass];
			YellowFlowers = new Entry[NumYellowFlowers];
			PurpleFlowers = new Entry[NumPurpleFlowers];
			RedFlowers = new Entry[NumRedFlowers];
			RoughTerrain = new Entry[NumRoughTerrain];
			Surf = new Entry[NumSurf];
			RockSmash = new Entry[NumRockSmash];
			OldRod = new Entry[NumOldRod];
			GoodRod = new Entry[NumGoodRod];
			SuperRod = new Entry[NumSuperRod];
			HordeA = new Entry[NumHordeA];
			HordeB = new Entry[NumHordeB];
			HordeC = new Entry[NumHordeC];
		}

		protected override void ProcessEntries(Entry[] entries)
		{
			Assertions.AssertLength((uint) NumEntries, entries, exact: true);

			ArrayStream<Entry> stream = new ArrayStream<Entry>(entries);

			// See above for layout details

			TallGrass = stream.Read(NumTallGrass);
			YellowFlowers = stream.Read(NumYellowFlowers);
			PurpleFlowers = stream.Read(NumPurpleFlowers);
			RedFlowers = stream.Read(NumRedFlowers);
			RoughTerrain = stream.Read(NumRoughTerrain);
			Surf = stream.Read(NumSurf);
			RockSmash = stream.Read(NumRockSmash);
			OldRod = stream.Read(NumOldRod);
			GoodRod = stream.Read(NumGoodRod);
			SuperRod = stream.Read(NumSuperRod);
			HordeA = stream.Read(NumHordeA);
			HordeB = stream.Read(NumHordeB);
			HordeC = stream.Read(NumHordeC);
		}

		protected override IEnumerable<Entry> AssembleEntries()
		{
			Entry[] entries = new Entry[NumEntries];
			ArrayStream<Entry> stream = new ArrayStream<Entry>(entries);

			// See above for layout details

			stream.Write(TallGrass);
			stream.Write(YellowFlowers);
			stream.Write(PurpleFlowers);
			stream.Write(RedFlowers);
			stream.Write(RoughTerrain);
			stream.Write(Surf);
			stream.Write(RockSmash);
			stream.Write(OldRod);
			stream.Write(GoodRod);
			stream.Write(SuperRod);
			stream.Write(HordeA);
			stream.Write(HordeB);
			stream.Write(HordeC);

			return entries;
		}
	}
}