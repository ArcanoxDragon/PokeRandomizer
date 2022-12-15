namespace PokeRandomizer.Common.Game
{
	public enum GameVersion
	{
		Invalid = -2,

		XY       = 106,
		ORASDemo = 107,
		ORAS     = 108,
		SunMoon  = 109,
		SunMoonDemo
	}

	public static class GameVersionExtensions
	{
		public static string GetDisplayName(this GameVersion game) => game switch {
			GameVersion.XY          => "X/Y",
			GameVersion.ORAS        => "Omega Ruby/Alpha Sapphire",
			GameVersion.ORASDemo    => "Omega Ruby/Alpha Sapphire (Demo)",
			GameVersion.SunMoon     => "Sun/Moon",
			GameVersion.SunMoonDemo => "Sun/Moon (Demo)",
			_                       => "Unknown"
		};

		public static GameGeneration GetGeneration(this GameVersion game) => game switch {
			GameVersion.XY          => GameGeneration.Generation6,
			GameVersion.ORAS        => GameGeneration.Generation6,
			GameVersion.ORASDemo    => GameGeneration.Generation6,
			GameVersion.SunMoon     => GameGeneration.Generation7,
			GameVersion.SunMoonDemo => GameGeneration.Generation7,
			_                       => GameGeneration.Unknown
		};

		public static GenerationInfo GetInfo(this GameVersion game) => game.GetGeneration().GetInfo();

		public static bool IsXY(this GameVersion game) => game == GameVersion.XY;
		public static bool IsORAS(this GameVersion game) => game == GameVersion.ORAS || game == GameVersion.ORASDemo;
		public static bool IsSunMoon(this GameVersion game) => game == GameVersion.SunMoon || game == GameVersion.SunMoonDemo;
		public static bool IsGen6(this GameVersion game) => game.GetGeneration() == GameGeneration.Generation6;
		public static bool IsGen7(this GameVersion game) => game.GetGeneration() == GameGeneration.Generation7;
	}
}