using System;
using System.Collections.Generic;

namespace CtrDotNet.Pokemon.Utility
{
	public static class EnumerableExtensions
	{
		public static void ForEach<T>( this IEnumerable<T> enumerable, Action<T, int> action )
		{
			int i = 0;
			foreach ( T val in enumerable )
				action( val, i++ );
		}

		public static void ForEach<T>( this IEnumerable<T> enumerable, Action<T> action )
			=> enumerable.ForEach( ( v, i ) => action( v ) );
	}
}