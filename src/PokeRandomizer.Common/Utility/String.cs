using System.Linq;

namespace PokeRandomizer.Common.Utility
{
	public static class StringExtensions
	{
		private static readonly char[] Vowels = { 'a', 'e', 'i', 'o', 'u' };

		public static bool IsVowel(this char c) => Vowels.Contains(char.ToLower(c));
		public static bool IsConsonant(this char c) => !c.IsVowel();

		public static string Repeat(this string s, int count) => count switch {
			0 => string.Empty,
			1 => s,
			_ => s + s.Repeat(count - 1),
		};

		public static string Article(this string noun) => noun.First().IsVowel() ? "an" : "a";
	}
}