﻿namespace CtrDotNet.Pokemon.GameData
{
	public enum GameVersion
	{
		Invalid = -2,

		XY = 106,
		ORASDemo = 107,
		ORAS = 108,
		SunMoon = 109,
		SunMoonDemo
	}

	public static class GameVersionExtensions
	{
		public static GameGeneration GetGeneration( this GameVersion game )
		{
			switch ( game )
			{
				case GameVersion.XY:
				case GameVersion.ORAS:
				case GameVersion.ORASDemo:
					return GameGeneration.Generation6;
				case GameVersion.SunMoon:
				case GameVersion.SunMoonDemo:
					return GameGeneration.Generation7;
				default:
					return GameGeneration.Unknown;
			}
		}

		public static bool IsXY( this GameVersion game ) => game == GameVersion.XY;
		public static bool IsORAS( this GameVersion game ) => game == GameVersion.ORAS || game == GameVersion.ORASDemo;
		public static bool IsSunMoon( this GameVersion game ) => game == GameVersion.SunMoon || game == GameVersion.SunMoonDemo;
	}
}