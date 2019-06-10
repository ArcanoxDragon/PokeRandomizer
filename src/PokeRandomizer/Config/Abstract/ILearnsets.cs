namespace PokeRandomizer.Config.Abstract
{
	public interface ILearnsets : IMoves
	{
		bool RandomizeLearnsets { get; }
		bool AtLeast4Moves      { get; }

		[ MinValue( 10 ), MaxValue( 100 ) ]
		int LearnAllMovesBy { get; }

		bool RandomizeLevels { get; }
	}
}