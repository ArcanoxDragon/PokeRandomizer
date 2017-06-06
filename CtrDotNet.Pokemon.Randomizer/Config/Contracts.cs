namespace CtrDotNet.Pokemon.Randomization.Config
{
	public interface IConfig
	{
		IAbilities Abilities { get; }
		IEggMoves EggMoves { get; }
		IEncounters Encounters { get; }
		ILearnsets Learnsets { get; }
		IStarters Starters { get; }
		ITrainers Trainers { get; }
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

	public interface IMoves
	{
		bool FavorSameType { get; }
	}

	public interface ILearnsets : IMoves
	{
		bool RandomizeLevels { get; }
	}

	public interface IEggMoves : IMoves { }

	public interface IAbilities
	{
		bool AllowWonderGuard { get; }
	}

	public interface ITrainers
	{
		bool FriendKeepsStarter { get; }
		decimal LevelMultiplier { get; }
		bool TypeThemed { get; }
	}
}