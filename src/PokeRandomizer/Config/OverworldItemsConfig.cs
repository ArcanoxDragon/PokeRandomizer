﻿using PokeRandomizer.Config.Abstract;

namespace PokeRandomizer.Config
{
	public class OverworldItemsConfig : IOverworldItems
	{
		public bool RandomizeOverworldItems { get; set; } = true;
		public bool AllowMasterBalls        { get; set; }
		public bool RandomizeTMs            { get; set; } = true;
		public bool AllowMegaStones         { get; set; }
	}
}