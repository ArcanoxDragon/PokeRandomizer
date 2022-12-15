using System;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Reflection
{
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public class HandlesGameAttribute : Attribute
	{
		public HandlesGameAttribute(GameVersion gameVersion)
		{
			GameVersion = gameVersion;
		}

		public GameVersion GameVersion { get; }
	}
}