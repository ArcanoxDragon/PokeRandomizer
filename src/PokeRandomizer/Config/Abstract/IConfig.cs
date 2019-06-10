namespace PokeRandomizer.Config.Abstract
{
	public interface IConfig
	{
		IEggMoves       EggMoves       { get; }
		IEncounters     Encounters     { get; }
		ILearnsets      Learnsets      { get; }
		IOverworldItems OverworldItems { get; }
		IPokemonInfo    PokemonInfo    { get; }
		IStarters       Starters       { get; }
		ITrainers       Trainers       { get; }
	}
}