namespace PokeRandomizer.Config.Abstract
{
	public interface IMoves
	{
		bool FavorSameType { get; }

		[ MinValue( 0.01 ) ]
		[ MaxValue( 1.0 ) ]
		decimal SameTypePercentage { get; }
	}
}