using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen6.XY
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
		public const int NumRedFlowers = 12;
		public const int NumRoughTerrain = 12;
		public const int NumSurf = 5;
		public const int NumRockSmash = 5;
		public const int NumOldRod = 3;
		public const int NumGoodRod = 3;
		public const int NumSuperRod = 3;
		public const int NumHordeA = 5;
		public const int NumHordeB = 5;
		public const int NumHordeC = 5;

		#endregion

		public XyEncounterWild( GameVersion gameVersion, int zoneId ) : base( gameVersion, zoneId ) { }

		protected override int DataStart => 0xE;
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

		public Entry[] TallGrass { get; private set; }
		public Entry[] YellowFlowers { get; private set; }
		public Entry[] PurpleFlowers { get; private set; }
		public Entry[] RedFlowers { get; private set; }
		public Entry[] RoughTerrain { get; private set; }
		public Entry[] Surf { get; private set; }
		public Entry[] RockSmash { get; private set; }
		public Entry[] OldRod { get; private set; }
		public Entry[] GoodRod { get; private set; }
		public Entry[] SuperRod { get; private set; }
		public Entry[] HordeA { get; private set; }
		public Entry[] HordeB { get; private set; }
		public Entry[] HordeC { get; private set; }

		protected override void ProcessEntries( Entry[] entries )
		{
			Assertions.AssertLength( (uint) this.NumEntries, entries, exact: true );

			ArrayStream<Entry> stream = new ArrayStream<Entry>( entries );

			// See above for layout details

			this.TallGrass = stream.Read( NumTallGrass );
			this.YellowFlowers = stream.Read( NumYellowFlowers );
			this.PurpleFlowers = stream.Read( NumPurpleFlowers );
			this.RedFlowers = stream.Read( NumRedFlowers );
			this.RoughTerrain = stream.Read( NumRoughTerrain );
			this.Surf = stream.Read( NumSurf );
			this.RockSmash = stream.Read( NumRockSmash );
			this.OldRod = stream.Read( NumOldRod );
			this.GoodRod = stream.Read( NumGoodRod );
			this.SuperRod = stream.Read( NumSuperRod );
			this.HordeA = stream.Read( NumHordeA );
			this.HordeB = stream.Read( NumHordeB );
			this.HordeC = stream.Read( NumHordeC );
		}

		protected override Entry[] AssembleEntries()
		{
			Entry[] entries = new Entry[ this.NumEntries ];
			ArrayStream<Entry> stream = new ArrayStream<Entry>( entries );

			// See above for layout details

			stream.Write( this.TallGrass );
			stream.Write( this.YellowFlowers );
			stream.Write( this.PurpleFlowers );
			stream.Write( this.RedFlowers );
			stream.Write( this.RoughTerrain );
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