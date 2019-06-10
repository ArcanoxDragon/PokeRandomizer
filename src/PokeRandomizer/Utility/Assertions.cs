using System;

namespace PokeRandomizer.Utility
{
	static class Assertions
	{
		public static void AssertIn<T>( T min, T max, T actual ) where T : IComparable
		{
			if ( actual.CompareTo( min ) < 0 || actual.CompareTo( max ) > 0 )
				throw new ArgumentOutOfRangeException( $"Argument out of range. Expected [{min}, {max}] but got {actual}" );
		}
	}
}