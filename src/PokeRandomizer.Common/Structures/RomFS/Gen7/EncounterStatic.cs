using System.IO;
using System.Linq;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.RomFS.Gen7
{
	public class EncounterStatic : Gen6.EncounterStatic
	{
		public EncounterStatic( GameVersion gameVersion ) : base( gameVersion ) { }

		// ReSharper disable once InconsistentNaming
		public sbyte[] IVs { get; set; }

		public ushort[] RelearnMoves { get; set; }
		public int Unused1 { get; set; }

		protected override void ReadData( BinaryReader r )
		{
			base.ReadData( r );

			this.Unused1 = r.ReadInt32();
			this.RelearnMoves = Enumerable.Range( 0, 4 ).Select( i => r.ReadUInt16() ).ToArray();
			this.IVs = Enumerable.Range( 0, 6 ).Select( i => r.ReadSByte() ).ToArray();
		}

		protected override void WriteData( BinaryWriter w )
		{
			base.WriteData( w );

			w.Write( this.Unused1 );
			this.RelearnMoves.ToList().ForEach( w.Write );
			this.IVs.ToList().ForEach( w.Write );
		}
	}
}