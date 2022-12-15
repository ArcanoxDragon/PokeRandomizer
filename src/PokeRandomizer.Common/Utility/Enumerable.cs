using System;
using System.Collections.Generic;

namespace PokeRandomizer.Common.Utility
{
	public static class EnumerableExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T, int> action)
		{
			int i = 0;
			foreach (T val in enumerable)
				action(val, i++);
		}

		public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
			=> enumerable.ForEach((v, _) => action(v));
	}
}