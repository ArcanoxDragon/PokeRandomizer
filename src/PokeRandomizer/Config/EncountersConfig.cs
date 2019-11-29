using PokeRandomizer.Config.Abstract;

namespace PokeRandomizer.Config
{
	public class EncountersConfig : IEncounters
	{
		public bool    RandomizeEncounters { get; set; } = true;
		public bool    AllowLegendaries    { get; set; } = true;
		public decimal LevelMultiplier     { get; set; } = 1.0m;
		public bool    TypePerSubArea      { get; set; }
		public bool    TypeThemedAreas     { get; set; } = true;
		public bool    ProperHordes        { get; set; }
		public bool    EnsureDittosInGrass { get; set; }
	}
}