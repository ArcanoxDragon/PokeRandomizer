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
		 * 3 Swarm
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
		public const int NumSwarm = 3;
		public const int NumSurf = 5;
		public const int NumRockSmash = 5;
		public const int NumOldRod = 3;
		public const int NumGoodRod = 3;
		public const int NumSuperRod = 3;
		public const int NumHordeA = 5;
		public const int NumHordeB = 5;
		public const int NumHordeC = 5;

		#endregion

		public OrasEncounterWild( GameVersion gameVersion, int zoneId ) : base( gameVersion, zoneId ) { }

		protected override int DataStart => 0xE;
		protected override int DataLength => 0xF4;

		protected override int NumEntries => NumTallGrass +
											 NumVeryTallGrass +
											 NumSwarm +
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
		public Entry[] Swarm { get; private set; }
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
			this.VeryTallGrass = stream.Read( NumVeryTallGrass );
			this.Swarm = stream.Read( NumSwarm );
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
			stream.Write( this.VeryTallGrass );
			stream.Write( this.Swarm );
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