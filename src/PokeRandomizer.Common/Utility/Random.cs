using System;

namespace PokeRandomizer.Common.Utility
{
	public static class RandomUtil
	{
		private static readonly Random Random = new();

		public static uint UInt32() => (uint) Random.NextDouble() * uint.MaxValue;
	}
}