using System.Collections.Generic;

namespace CtrDotNet.Pokemon.Randomization.Utility
{
	public static class LinqUtils
	{
		public static IEnumerable<(int, T)> Pairs<T>( this IEnumerable<T> enumerable )
		{
			int current = 0;

			foreach ( T item in enumerable )
				yield return (current++, item);
		}
	}
}