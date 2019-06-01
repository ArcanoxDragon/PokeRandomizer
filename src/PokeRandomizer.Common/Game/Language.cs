namespace PokeRandomizer.Common.Game
{
	public enum Language
	{
		JapaneseKana,
		JapaneseKanji,
		English,
		French,
		Italian,
		German,
		Spanish,
		Korean,
		ChineseSimplified,
		ChineseTraditional
	}

	public static class LanguageExtensions
	{
		public static string GetDisplayName( this Language lang )
		{
			switch ( lang )
			{
				case Language.JapaneseKana:
					return "Japanese (Kana)";
				case Language.JapaneseKanji:
					return "Japanese (Kanji)";
				case Language.ChineseSimplified:
					return "Chinese (Simplified)";
				case Language.ChineseTraditional:
					return "Chinese (Traditional)";
				default:
					return lang.ToString();
			}
		}
	}
}