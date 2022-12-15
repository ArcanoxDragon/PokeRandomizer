using System;
using PokeRandomizer.Common.Game;
using PkNxGameVersion = pkNX.Structures.GameVersion;

namespace PokeRandomizer.Common.Extension
{
	public static class GameVersionExtensions
	{
		public static PkNxGameVersion ToPkNxVersion(this GameVersion version)
		{
			var name = version.ToString();

			if (!Enum.TryParse<PkNxGameVersion>(name, ignoreCase: true, out var pknxVersion))
				throw new InvalidOperationException($"Invalid PkNX game version: {name}");

			return pknxVersion;
		}
	}
}