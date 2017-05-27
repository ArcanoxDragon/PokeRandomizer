using System.IO;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen6
{
	public class EggMoves : Common.EggMoves
	{
		protected EggMoves() { }

		public EggMoves( byte[] data ) : base( data ) { }

		protected override void ReadEggMoves( BinaryReader r )
		{
			this.Count = r.ReadUInt16();
			this.Moves = new int[ this.Count ];
			for ( int i = 0; i < this.Count; i++ )
				this.Moves[ i ] = r.ReadUInt16();
		}

		protected override void WriteEggMoves( BinaryWriter w )
		{
			w.Write( (ushort) this.Count );
			for ( int i = 0; i < this.Count; i++ )
				w.Write( (ushort) this.Moves[ i ] );
		}
	}
}