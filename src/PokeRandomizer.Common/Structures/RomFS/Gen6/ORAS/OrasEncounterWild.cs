using System.Collections.Generic;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Utility;

namespace PokeRandomizer.Common.Structures.RomFS.Gen6.ORAS
{
	public class OrasEncounterWild : EncounterWild
	{
		#region Layout Data (ORAS)

		/**
		 * 12 Normal Tall Grass
		 * 12 Very Tall Grass
		 * 3 DexNav
		 * 5 Surf
		 * 5 Rock Smash (lemme smash, please)
		 * 3 Old Rod
		 * 3 Good Rod
		 * 3 Super Rod
		 * 5 "A" Horde
		 * 5 "B" Horde
		 * 5 "C" Horde
		 */
		public const int NumTallGrass = 12;

		public const int NumVeryTallGrass = 12;
		public const int NumDexNav        = 3;
		public const int NumSurf          = 5;
		public const int NumRockSmash     = 5;
		public const int NumOldRod        = 3;
		public const int NumGoodRod       = 3;
		public const int NumSuperRod      = 3;
		public const int NumHordeA        = 5;
		public const int NumHordeB        = 5;
		public const int NumHordeC        = 5;

		#endregion

		public OrasEncounterWild(GameVersion gameVersion, int zoneId) : base(gameVersion, zoneId)
		{
			FillEmpty();
		}

		public override int DataStart  => 0xE;
		public override int DataLength => 0xF4;

		public override int NumEntries => NumTallGrass +
										  NumVeryTallGrass +
										  NumDexNav +
										  NumSurf +
										  NumRockSmash +
										  NumOldRod +
										  NumGoodRod +
										  NumSuperRod +
										  NumHordeA +
										  NumHordeB +
										  NumHordeC;

		public Entry[] TallGrass     { get; private set; }
		public Entry[] VeryTallGrass { get; private set; }
		public Entry[] DexNav        { get; private set; }
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
			get => new[] { TallGrass, VeryTallGrass, DexNav, Surf, RockSmash, OldRod, GoodRod, SuperRod, HordeA, HordeB, HordeC };
			set
			{
				Assertions.AssertLength(11, value, true);

				TallGrass = value[0];
				VeryTallGrass = value[1];
				DexNav = value[2];
				Surf = value[3];
				RockSmash = value[4];
				OldRod = value[5];
				GoodRod = value[6];
				SuperRod = value[7];
				HordeA = value[8];
				HordeB = value[9];
				HordeC = value[10];
			}
		}

		private void FillEmpty()
		{
			TallGrass = new Entry[NumTallGrass];
			VeryTallGrass = new Entry[NumVeryTallGrass];
			DexNav = new Entry[NumDexNav];
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
			VeryTallGrass = stream.Read(NumVeryTallGrass);
			DexNav = stream.Read(NumDexNav);
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
			stream.Write(VeryTallGrass);
			stream.Write(DexNav);
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