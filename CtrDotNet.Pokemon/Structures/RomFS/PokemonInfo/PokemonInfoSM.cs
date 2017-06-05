using System;
using System.Linq;

namespace CtrDotNet.Pokemon.Structures.RomFS.PokemonInfo
{
	public class PokemonInfoSM : PokemonInfoXY
	{
		public new const int Size = 0x54;

		public override void Read( byte[] data )
		{
			if ( data.Length != Size )
				return;
			this.Data = data;

			this.TmHm = PokemonInfo.GetBits( this.Data.Skip( 0x28 ).Take( 0x10 ).ToArray() ); // 36-39
			this.TypeTutors = PokemonInfo.GetBits( this.Data.Skip( 0x38 ).Take( 0x4 ).ToArray() ); // 40
		}

		public override byte[] Write()
		{
			PokemonInfo.SetBits( this.TmHm ).CopyTo( this.Data, 0x28 );
			PokemonInfo.SetBits( this.TypeTutors ).CopyTo( this.Data, 0x38 );
			return this.Data;
		}

		// No accessing for 3C-4B

		public int SpecialZItem
		{
			get => BitConverter.ToUInt16( this.Data, 0x4C );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.Data, 0x4C );
		}

		public int SpecialZBaseMove
		{
			get => BitConverter.ToUInt16( this.Data, 0x4E );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.Data, 0x4E );
		}

		public int SpecialZzMove
		{
			get => BitConverter.ToUInt16( this.Data, 0x50 );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.Data, 0x50 );
		}

		public bool LocalVariant
		{
			get => this.Data[ 0x52 ] == 1;
			set => this.Data[ 0x52 ] = (byte) ( value ? 1 : 0 );
		}
	}
}