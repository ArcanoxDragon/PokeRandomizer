namespace PokeRandomizer.Config.Abstract
{
	public interface IEncounters
	{
		bool RandomizeEncounters { get; }
		bool AllowLegendaries    { get; }

		[ MinValue( 0.5 ) ]
		decimal LevelMultiplier { get; }

		bool TypePerSubArea  { get; }
		bool TypeThemedAreas { get; }
	}
}