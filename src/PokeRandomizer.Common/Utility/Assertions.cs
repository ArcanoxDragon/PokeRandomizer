using System;
using System.IO;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Structures.RomFS.Gen6;

namespace PokeRandomizer.Common.Utility
{
	static class Assertions
	{
		public static void AssertVersion( GameVersion expected, GameVersion actual )
		{
			if ( actual != expected )
				throw new ArgumentException( $"Version mismatch. Expected version {expected} but got {actual}." );
		}

		public static void AssertLength<T>( uint expected, T[] array, bool exact = false )
		{
			if ( array.Length < expected )
				throw new InvalidDataException( $"Data too short. Expected {( exact ? "exactly" : "at least" )} {expected}, but got {array.Length}" );

			if ( exact && array.Length > expected )
				throw new InvalidDataException( $"Data too long. Expected exactly {expected}, but got {array.Length}" );
		}

		public static void AssertLength( int expected, EncounterWild[] array, bool exact = false ) => Assertions.AssertLength( (uint) expected, array, exact );
	}
}