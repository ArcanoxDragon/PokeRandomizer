using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Structures.RomFS.Gen6;

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

		public static void AssertLength<T>( uint expected, T[] array, bool exact = false )
		{
			if ( array.Length < expected )
				throw new InvalidDataException( $"Data too short. Expected {( exact ? "exactly" : "at least" )} {expected}" );

			if ( exact && array.Length > expected )
				throw new InvalidDataException( $"Data too long. Expected exactly {expected}" );
		}

		public static void AssertLength( int expected, EncounterWild[] array, bool exact = false ) => AssertLength( (uint) expected, array, exact );

		public static void AssertIn<T>( T min, T max, T actual ) where T : IComparable
		{
			if ( actual.CompareTo( min ) < 0 || actual.CompareTo( max ) > 0 )
				throw new ArgumentOutOfRangeException( $"Argument out of range. Expected [{min}, {max}] but got {actual}" );
		}
	}
}