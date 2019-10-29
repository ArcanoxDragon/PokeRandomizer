namespace PokeRandomizer.Config.Abstract
{
	public interface IOverworldItems
	{
		bool RandomizeOverworldItems { get; }
		bool AllowMasterBalls        { get; }
		bool RandomizeTMs            { get; }
		bool AllowMegaStones         { get; }
	}
}