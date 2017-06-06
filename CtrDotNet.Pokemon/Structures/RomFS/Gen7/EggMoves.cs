using System.IO;
using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen7
{
	public class EggMoves : Gen6.EggMoves
	{
		public EggMoves( GameVersion gameVersion ) : base( gameVersion ) { }

		public int FormTableIndex { get; set; }

		protected override void ReadData( BinaryReader r )
		{
			this.FormTableIndex = r.ReadUInt16();
			base.ReadData( r );
		}

		protected override void WriteData( BinaryWriter w )
		{
			w.Write( (ushort) this.FormTableIndex );
			base.WriteData( w );
		}
	}
}