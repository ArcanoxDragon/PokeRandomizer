using System.Linq;

namespace PokeRandomizer.Tests.Utility
{
	public static class StringExtensions
	{
		public static string Repeat( this string s, int count, string joiner = "" )
		{
			switch ( count )
			{
				case 0: return "";
				case 1: return s;
				default:
					return string.Join( joiner, Enumerable.Range( 0, count ).Select( i => s ) );
			}
		}
	}
}