using PokeRandomizer.Config.Abstract;

namespace PokeRandomizer.Config
{
	public class EggMovesConfig : IEggMoves
	{
		public bool    RandomizeEggMoves  { get; set; } = true;
		public bool    FavorSameType      { get; set; } = true;
		public decimal SameTypePercentage { get; set; } = 0.5m;
	}
}