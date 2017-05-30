using System.Collections.Generic;
using System.IO;
using System.Linq;
using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Structures.RomFS.Common
{
	public abstract class Learnset : BaseDataStructure
	{
		public int Count { get; set; }
		public int[] Moves { get; set; }
		public int[] Levels { get; set; }

		protected Learnset( GameVersion gameVersion ) : base( gameVersion ) { }

		public IEnumerable<int> GetMoves( int level )
		{
			return this.Moves.TakeWhile( ( move, i ) => this.Levels[ i ] <= level ).Distinct().ToArray();
		}

		public int[] GetCurrentMoves( int level )
		{
			return this.GetMoves( level ).Reverse().Take( 4 ).Reverse().ToArray();
		}

		public override void Read( byte[] data )
		{
			if ( data.Length < 4 || data.Length % 4 != 0 )
			{
				this.Count = 0;
				this.Levels = new int[ 0 ];
				this.Moves = new int[ 0 ];
				return;
			}

			this.Count = data.Length / 4 - 1;
			this.Moves = new int[ this.Count ];
			this.Levels = new int[ this.Count ];

			base.Read( data );
		}

		protected override void ReadData( BinaryReader br )
		{
			for ( int i = 0; i < this.Count; i++ )
			{
				this.Moves[ i ] = br.ReadInt16();
				this.Levels[ i ] = br.ReadInt16();
			}
		}

		protected override void WriteData( BinaryWriter bw )
		{
			this.Count = (ushort) this.Moves.Length;

			for ( int i = 0; i < this.Count; i++ )
			{
				bw.Write( (short) this.Moves[ i ] );
				bw.Write( (short) this.Levels[ i ] );
			}

			bw.Write( -1 );
		}
	}
}