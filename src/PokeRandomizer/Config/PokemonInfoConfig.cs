using PokeRandomizer.Config.Abstract;

namespace PokeRandomizer.Config
{
	public class PokemonInfoConfig : IPokemonInfo
	{
		public bool RandomizeAbilities      { get; set; }
		public bool AllowWonderGuard        { get; set; }
		public bool RandomizeTypes          { get; set; }
		public bool RandomizePrimaryTypes   { get; set; }
		public bool RandomizeSecondaryTypes { get; set; }
		public bool EnsureMinimumCatchRate  { get; set; }
	}
}