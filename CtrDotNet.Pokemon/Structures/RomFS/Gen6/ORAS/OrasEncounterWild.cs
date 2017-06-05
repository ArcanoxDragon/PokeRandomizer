using System.Collections.Generic;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen6.ORAS
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
		public const int NumDexNav = 3;
		public const int NumSurf = 5;
		public const int NumRockSmash = 5;
		public const int NumOldRod = 3;
		public const int NumGoodRod = 3;
		public const int NumSuperRod = 3;
		public const int NumHordeA = 5;
		public const int NumHordeB = 5;
		public const int NumHordeC = 5;

		#endregion

		public OrasEncounterWild( GameVersion gameVersion, int zoneId ) : base( gameVersion, zoneId )
		{
			this.FillEmpty();
		}

		public override int DataStart => 0xE;
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

		public Entry[] TallGrass { get; private set; }
		public Entry[] VeryTallGrass { get; private set; }
		public Entry[] DexNav { get; private set; }
		public Entry[] Surf { get; private set; }
		public Entry[] RockSmash { get; private set; }
		public Entry[] OldRod { get; private set; }
		public Entry[] GoodRod { get; private set; }
		public Entry[] SuperRod { get; private set; }
		public Entry[] HordeA { get; private set; }
		public Entry[] HordeB { get; private set; }
		public Entry[] HordeC { get; private set; }

		public override Entry[][] EntryArrays
		{
			get => new[] {
				this.TallGrass,
				this.VeryTallGrass,
				this.DexNav,
				this.Surf,
				this.RockSmash,
				this.OldRod,
				this.GoodRod,
				this.SuperRod,
				this.HordeA,
				this.HordeB,
				this.HordeC
			};
			set
			{
				Assertions.AssertLength( 11, value, true );

				this.TallGrass = value[ 0 ];
				this.VeryTallGrass = value[ 1 ];
				this.DexNav = value[ 2 ];
				this.Surf = value[ 3 ];
				this.RockSmash = value[ 4 ];
				this.OldRod = value[ 5 ];
				this.GoodRod = value[ 6 ];
				this.SuperRod = value[ 7 ];
				this.HordeA = value[ 8 ];
				this.HordeB = value[ 9 ];
				this.HordeC = value[ 10 ];
			}
		}

		private void FillEmpty()
		{
			this.TallGrass = new Entry[ NumTallGrass ];
			this.VeryTallGrass = new Entry[ NumVeryTallGrass ];
			this.DexNav = new Entry[ NumDexNav ];
			this.Surf = new Entry[ NumSurf ];
			this.RockSmash = new Entry[ NumRockSmash ];
			this.OldRod = new Entry[ NumOldRod ];
			this.GoodRod = new Entry[ NumGoodRod ];
			this.SuperRod = new Entry[ NumSuperRod ];
			this.HordeA = new Entry[ NumHordeA ];
			this.HordeB = new Entry[ NumHordeB ];
			this.HordeC = new Entry[ NumHordeC ];
		}

		protected override void ProcessEntries( Entry[] entries )
		{
			Assertions.AssertLength( (uint) this.NumEntries, entries, exact: true );

			ArrayStream<Entry> stream = new ArrayStream<Entry>( entries );

			// See above for layout details

			this.TallGrass = stream.Read( NumTallGrass );
			this.VeryTallGrass = stream.Read( NumVeryTallGrass );
			this.DexNav = stream.Read( NumDexNav );
			this.Surf = stream.Read( NumSurf );
			this.RockSmash = stream.Read( NumRockSmash );
			this.OldRod = stream.Read( NumOldRod );
			this.GoodRod = stream.Read( NumGoodRod );
			this.SuperRod = stream.Read( NumSuperRod );
			this.HordeA = stream.Read( NumHordeA );
			this.HordeB = stream.Read( NumHordeB );
			this.HordeC = stream.Read( NumHordeC );
		}

		protected override IEnumerable<Entry> AssembleEntries()
		{
			Entry[] entries = new Entry[ this.NumEntries ];
			ArrayStream<Entry> stream = new ArrayStream<Entry>( entries );

			// See above for layout details

			stream.Write( this.TallGrass );
			stream.Write( this.VeryTallGrass );
			stream.Write( this.DexNav );
			stream.Write( this.Surf );
			stream.Write( this.RockSmash );
			stream.Write( this.OldRod );
			stream.Write( this.GoodRod );
			stream.Write( this.SuperRod );
			stream.Write( this.HordeA );
			stream.Write( this.HordeB );
			stream.Write( this.HordeC );

			return entries;
		}
	}
}