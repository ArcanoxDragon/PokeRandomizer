namespace PokeRandomizer.Config
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
		bool AllowLegendaries { get; }
		bool StartersOnly { get; }
	}

	public interface IEncounters
	{
		bool AllowLegendaries { get; }

		[ MinValue( 0.5 ) ]
		decimal LevelMultiplier { get; }

		bool TypePerSubArea { get; }
		bool TypeThemedAreas { get; }
	}

	public interface IMoves
	{
		bool FavorSameType { get; }
	}

	public interface ILearnsets : IMoves
	{
		bool AtLeast4Moves { get; }

		[ MinValue( 10 ), MaxValue( 100 ) ]
		int LearnAllMovesBy { get; }

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

		[ MinValue( 0.5 ) ]
		decimal LevelMultiplier { get; }

		bool TypeThemed { get; }
	}
}