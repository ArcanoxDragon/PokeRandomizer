using System.IO;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen7
{
	public class EggMoves : Gen6.EggMoves
	{
		protected EggMoves() { }

		public EggMoves( byte[] data ) : base( data ) { }

		protected override void ReadEggMoves( BinaryReader r )
		{
			this.FormTableIndex = r.ReadUInt16();
			base.ReadEggMoves( r );
		}

		protected override void WriteEggMoves( BinaryWriter w )
		{
			w.Write( (ushort) this.FormTableIndex );
			base.WriteEggMoves( w );
		}
	}
}