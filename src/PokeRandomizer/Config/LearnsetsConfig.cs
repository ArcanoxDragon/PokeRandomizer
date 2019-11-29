using PokeRandomizer.Config.Abstract;

namespace PokeRandomizer.Config
{
	public class LearnsetsConfig : ILearnsets
	{
		public bool    RandomizeLearnsets { get; set; } = true;
		public bool    AtLeast4Moves      { get; set; }
		public bool    NoOneHitMoves      { get; set; }
		public int     LearnAllMovesBy    { get; set; } = 65;
		public bool    FavorSameType      { get; set; } = true;
		public decimal SameTypePercentage { get; set; } = 0.5m;
		public bool    RandomizeLevels    { get; set; }
	}
}