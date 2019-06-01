using System;

namespace PokeRandomizer.Common.Utility
{
	public static class Bitwise
	{
		public static uint SetBitfield( this uint i, uint val, uint width, int pos )
		{
			if ( width <= 0 || width > 32 )
				throw new ArgumentOutOfRangeException( nameof(width) );

			if ( width + pos >= 32 )
				throw new ArgumentOutOfRangeException( nameof(pos) );

			uint mask = (uint) Math.Pow( 2, width ) - 1;
			return ( i & ~( mask << pos ) ) | ( ( val & mask ) << pos );
		}

		public static uint SetBitfield( this uint i, int val, int width, int pos ) => i.SetBitfield( (uint) val, (uint) width, pos );

		public static uint GetBitfield( this uint i, uint width, int pos )
		{
			uint mask = (uint) Math.Pow( 2, width ) - 1;
			return ( i >> pos ) & mask;
		}
	}
}