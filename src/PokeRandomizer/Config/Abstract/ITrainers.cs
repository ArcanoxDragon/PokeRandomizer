namespace PokeRandomizer.Config.Abstract
{
	public interface ITrainers
	{
		bool RandomizeTrainers  { get; }
		bool FriendKeepsStarter { get; }

		[ MinValue( 0.5 ) ]
		decimal LevelMultiplier { get; }

		bool TypeThemed     { get; }
		bool TypeThemedGyms { get; }
	}
}