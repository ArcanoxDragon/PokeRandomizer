namespace PokeRandomizer.Config.Abstract
{
	public interface IPokemonInfo
	{
		bool RandomizeAbilities      { get; }
		bool AllowWonderGuard        { get; }
		bool RandomizeTypes          { get; }
		bool RandomizePrimaryTypes   { get; }
		bool RandomizeSecondaryTypes { get; }
		bool EnsureMinimumCatchRate  { get; }
	}
}