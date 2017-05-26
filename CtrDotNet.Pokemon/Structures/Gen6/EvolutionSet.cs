using System;
using System.IO;
using CtrDotNet.Pokemon.Structures.Common;

namespace CtrDotNet.Pokemon.Structures.Gen6
{
	public class EvolutionSet : BaseEvolutionSet
	{
		protected override int EntrySize => 6;
		protected override int EntryCount => 8;

		public EvolutionSet( byte[] data )
		{
			this.Read( data );
		}

		public sealed override void Read( byte[] data )
		{
			if ( data.Length != this.Size )
				throw new ArgumentOutOfRangeException( nameof(data), $"Data array length must be {this.Size}, but was {data.Length}" );

			this.PossibleEvolutions = new EvolutionMethod[ this.EntryCount ];

			using ( var ms = new MemoryStream( data ) )
			using ( var br = new BinaryReader( ms ) )
			{
				for ( int i = 0; i < this.PossibleEvolutions.Length; i++ )
					this.PossibleEvolutions[ i ] = this.ReadMethod( br );
			}
		}

		public override byte[] Write()
		{
			byte[] data;

			using ( MemoryStream ms = new MemoryStream() )
			using ( BinaryWriter bw = new BinaryWriter( ms ) )
			{
				foreach ( EvolutionMethod evo in this.PossibleEvolutions )
					this.WriteMethod( evo, bw );

				data = ms.ToArray();
			}

			return data;
		}

		protected virtual EvolutionMethod ReadMethod( BinaryReader r )
		{
			return new EvolutionMethod {
				Method = r.ReadUInt16(),
				Argument = r.ReadUInt16(),
				Species = r.ReadUInt16()
			};
		}

		protected virtual void WriteMethod( EvolutionMethod evo, BinaryWriter w )
		{
			w.Write( (ushort) evo.Method );
			w.Write( (ushort) evo.Argument );
			w.Write( (ushort) evo.Species );
		}
	}
}