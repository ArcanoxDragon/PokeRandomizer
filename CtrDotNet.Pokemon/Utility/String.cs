using System.Linq;

namespace CtrDotNet.Pokemon.Utility
{
	public static class StringExtensions
	{
		private static readonly char[] Vowels = { 'a', 'e', 'i', 'o', 'u' };

		public static bool IsVowel( this char c ) => Vowels.Contains( c );
		public static bool IsConsonant( this char c ) => !c.IsVowel();
	}
}