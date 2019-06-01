using System;

namespace PokeRandomizer.Common.Utility
{
	public static class NumberExtensions
	{
		public static bool In<N>( this N val, N min, N max ) where N : IComparable
			=> val.CompareTo( min ) >= 0 && val.CompareTo( max ) <= 0;
	}
}