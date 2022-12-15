using System;
using System.Collections.Generic;

namespace PokeRandomizer.Utility
{
	public static class ArrayUtils
	{
		public static T GetRandom<T>(this IList<T> from, Random random)
			=> from[random.Next(from.Count)];
	}
}