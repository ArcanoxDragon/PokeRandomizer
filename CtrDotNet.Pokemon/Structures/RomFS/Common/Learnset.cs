using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CtrDotNet.Pokemon.Structures.RomFS.Common
{
	public abstract class Learnset : IDataStructure
	{
		#region Static

		public static T[] GetArray<T>( byte[][] entries ) where T : Learnset, new()
		{
			T[] data = new T[ entries.Length ];

			for ( int i = 0; i < data.Length; i++ )
			{
				T learnset = new T();
				learnset.Read( entries[ i ] );
				data[ i ] = learnset;
			}

			return data;
		}

		#endregion

		public int Count { get; set; }
		public int[] Moves { get; set; }
		public int[] Levels { get; set; }

		protected Learnset() { }

		protected Learnset( byte[] data )
		{
			this.Read( data );
		}

		public IEnumerable<int> GetMoves( int level )
		{
			return this.Moves.TakeWhile( ( move, i ) => this.Levels[ i ] <= level ).Distinct().ToArray();
		}

		public int[] GetCurrentMoves( int level )
		{
			return this.GetMoves( level ).Reverse().Take( 4 ).Reverse().ToArray();
		}

		public void Read( byte[] data )
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

			using ( var ms = new MemoryStream( data ) )
			using ( var br = new BinaryReader( ms ) )
			{
				for ( int i = 0; i < this.Count; i++ )
				{
					this.Moves[ i ] = br.ReadInt16();
					this.Levels[ i ] = br.ReadInt16();
				}
			}
		}

		public byte[] Write()
		{
			this.Count = (ushort) this.Moves.Length;
			using ( MemoryStream ms = new MemoryStream() )
			using ( BinaryWriter bw = new BinaryWriter( ms ) )
			{
				for ( int i = 0; i < this.Count; i++ )
				{
					bw.Write( (short) this.Moves[ i ] );
					bw.Write( (short) this.Levels[ i ] );
				}
				bw.Write( -1 );
				return ms.ToArray();
			}
		}
	}
}