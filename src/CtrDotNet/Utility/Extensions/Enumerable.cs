using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CtrDotNet.Utility.Extensions
{
	public static class EnumerableExtensions
	{
		public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, Task<TResult>> projector)
		{
			var results = new List<TResult>();

			foreach (var value in source)
				results.Add(await projector(value));

			return results;
		}

		public static int FindIndex<T>(this IEnumerable<T> source, Func<T, bool> predicate)
		{
			var index = 0;

			foreach (var value in source)
			{
				if (predicate(value))
					return index;

				index++;
			}

			return -1;
		}

		public static IEnumerable<(int, T)> Pairs<T>(this IEnumerable<T> enumerable)
		{
			int current = 0;

			foreach (T item in enumerable)
				yield return ( current++, item );
		}
	}
}