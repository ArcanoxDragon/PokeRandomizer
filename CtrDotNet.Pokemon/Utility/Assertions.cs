using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Utility
{
	[ SuppressMessage( "ReSharper", "UnusedParameter.Global" ) ]
	public static class Assertions
	{
		public static void AssertVersion( GameVersion expected, GameVersion actual )
		{
			if ( actual != expected )
				throw new ArgumentException( $"Version mismatch. Expected version {expected} but got {actual}." );
		}

		public static void AssertLength<T>( uint expected, T[] array )
		{
			if ( array.Length < expected )
				throw new InvalidDataException( $"Data too short. Expected at least {expected}" );
		}

		public static void AssertIn<T>( T min, T max, T actual ) where T : IComparable
		{
			if ( actual.CompareTo( min ) < 0 || actual.CompareTo( max ) > 0 )
				throw new ArgumentOutOfRangeException( $"Argument out of range. Expected [{min}, {max}] but got {actual}" );
		}
	}
}