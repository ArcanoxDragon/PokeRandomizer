namespace PokeRandomizer.Config.Abstract
{
	public interface IEggMoves : IMoves
	{
		bool RandomizeEggMoves { get; }
	}
}