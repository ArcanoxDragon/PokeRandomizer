using System.IO;
using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen6
{
	public class EggMoves : Common.EggMoves
	{
		public EggMoves( GameVersion gameVersion ) : base( gameVersion ) { }

		protected override void ReadData( BinaryReader br )
		{
			if ( br.BaseStream.Length < 2 )
			{
				this.Empty = true;
				this.Moves = new ushort[ 0 ];
				return;
			}

			int numMoves = br.ReadUInt16();
			this.Empty = false;
			this.Moves = new ushort[ numMoves ];

			for ( int i = 0; i < numMoves; i++ )
				this.Moves[ i ] = br.ReadUInt16();
		}

		protected override void WriteData( BinaryWriter bw )
		{
			if ( this.Empty )
				return;

			bw.Write( (ushort) this.Count );

			for ( int i = 0; i < this.Count; i++ )
				bw.Write( this.Moves[ i ] );
		}
	}
}