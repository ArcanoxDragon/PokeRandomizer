namespace CtrDotNet.Pokemon.Randomization.Config
{
	public interface IConfig
	{
		IStarters Starters { get; }
		IEncounters Encounters { get; }
		ILearnsets Learnsets { get; }
	}

	public interface IStarters
	{
		bool StartersOnly { get; }
	}

	public interface IEncounters
	{
		bool AllowLegendaries { get; }
		decimal LevelMultiplier { get; }
		bool TypeThemedAreas { get; }
	}

	public interface ILearnsets
	{
		bool FavorSameType { get; }
		bool RandomizeLevels { get; }
	}
}