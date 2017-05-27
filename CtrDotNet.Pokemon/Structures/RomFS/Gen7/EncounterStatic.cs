using System.IO;
using System.Linq;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen7
{
	public class EncounterStatic : Gen6.EncounterStatic
	{
		public EncounterStatic( byte[] data ) : base( data ) { }

		// ReSharper disable once InconsistentNaming
		public sbyte[] IVs { get; set; }

		public ushort[] RelearnMoves { get; set; }
		public int Unused1 { get; set; }

		protected override void Read( BinaryReader r )
		{
			base.Read( r );

			this.Unused1 = r.ReadInt32();
			this.RelearnMoves = Enumerable.Range( 0, 4 ).Select( i => r.ReadUInt16() ).ToArray();
			this.IVs = Enumerable.Range( 0, 6 ).Select( i => r.ReadSByte() ).ToArray();
		}

		protected override void Write( BinaryWriter w )
		{
			base.Write( w );

			w.Write( this.Unused1 );
			this.RelearnMoves.ToList().ForEach( w.Write );
			this.IVs.ToList().ForEach( w.Write );
		}
	}
}