namespace PokeRandomizer.Config.Abstract
{
	public interface IOverworldItems
	{
		bool RandomizeOverworldItems { get; }
		bool AllowMasterBalls        { get; }
		bool AllowTMs                { get; }
		bool AllowMegaStones         { get; }
	}
}