using System.IO;

namespace CtrDotNet.Pokemon.Structures.RomFS.Common
{
	public abstract class EggMoves : IDataStructure
	{
		#region Static

		public static T[] GetArray<T>( byte[][] entries ) where T : EggMoves, new()
		{
			T[] data = new T[ entries.Length ];

			for ( int i = 0; i < data.Length; i++ )
			{
				T eggMoves = new T();
				eggMoves.Read( entries[ i ] );
				data[ i ] = eggMoves;
			}

			return data;
		}

		#endregion

		protected EggMoves() { }

		protected EggMoves( byte[] data )
		{
			this.Read( data );
		}

		public int Count { get; set; }
		public int[] Moves { get; set; }
		public int FormTableIndex { get; set; }

		public void Read( byte[]data )
		{
			if ( data.Length < 2 || data.Length % 2 != 0 )
			{
				this.Count = 0;
				this.Moves = new int[ 0 ];
				return;
			}

			using ( var ms = new MemoryStream() )
			using ( var br = new BinaryReader( ms ) )
			{
				this.ReadEggMoves( br );
			}
		}

		public byte[] Write()
		{
			this.Count = this.Moves.Length;

			if ( this.Count == 0 )
				return new byte[ 0 ];

			using ( var ms = new MemoryStream() )
			using ( var bw = new BinaryWriter( ms ) )
			{
				this.WriteEggMoves( bw );
				return ms.ToArray();
			}
		}

		protected abstract void ReadEggMoves( BinaryReader r );
		protected abstract void WriteEggMoves( BinaryWriter w );
	}
}