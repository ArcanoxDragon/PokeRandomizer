using System.Collections.Generic;
using System.IO;
using System.Linq;
using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Structures.RomFS.Common
{
	public abstract class Learnset : BaseDataStructure
	{
		#region Static

		public static Learnset New( GameVersion version )
		{
			switch ( version.GetGeneration() )
			{
				case GameGeneration.Generation7:
					return new Gen7.Learnset( version );
				default:
					return new Gen6.Learnset( version );
			}
		}

		#endregion

		public int Count { get; set; }
		public ushort[] Moves { get; set; }
		public ushort[] Levels { get; set; }

		protected Learnset( GameVersion gameVersion ) : base( gameVersion ) { }

		public IEnumerable<ushort> GetPossibleMoves( int level )
		{
			return this.Moves.TakeWhile( ( move, i ) => this.Levels[ i ] <= level ).Distinct();
		}

		public ushort[] GetMostLikelyMoves( int level )
		{
			return this.GetPossibleMoves( level ).Reverse().Take( 4 ).Reverse().ToArray();
		}

		public override void Read( byte[] data )
		{
			if ( data.Length < 4 || data.Length % 4 != 0 )
			{
				this.Count = 0;
				this.Levels = new ushort[ 0 ];
				this.Moves = new ushort[ 0 ];
				return;
			}

			this.Count = data.Length / 4 - 1;
			this.Levels = new ushort[ this.Count ];
			this.Moves = new ushort[ this.Count ];

			base.Read( data );
		}

		protected override void ReadData( BinaryReader br )
		{
			for ( int i = 0; i < this.Count; i++ )
			{
				this.Moves[ i ] = br.ReadUInt16();
				this.Levels[ i ] = br.ReadUInt16();
			}
		}

		protected override void WriteData( BinaryWriter bw )
		{
			this.Count = (ushort) this.Moves.Length;

			for ( int i = 0; i < this.Count; i++ )
			{
				bw.Write( this.Moves[ i ] );
				bw.Write( this.Levels[ i ] );
			}

			bw.Write( -1 );
		}
	}
}