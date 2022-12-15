using System;
using System.IO;

namespace CtrDotNet.Utility
{
	static class Assertions
	{
		public static void AssertLength<T>(uint expected, T[] array, bool exact = false)
		{
			if (array.Length < expected)
				throw new InvalidDataException($"Data too short. Expected {( exact ? "exactly" : "at least" )} {expected}, but got {array.Length}");

			if (exact && array.Length > expected)
				throw new InvalidDataException($"Data too long. Expected exactly {expected}, but got {array.Length}");
		}

		public static void AssertIn<T>(T min, T max, T actual) where T : IComparable
		{
			if (actual.CompareTo(min) < 0 || actual.CompareTo(max) > 0)
				throw new ArgumentOutOfRangeException($"Argument out of range. Expected [{min}, {max}] but got {actual}");
		}
	}
}