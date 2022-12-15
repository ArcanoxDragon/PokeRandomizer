using PokeRandomizer.Config.Abstract;

namespace PokeRandomizer.Config
{
	public class RandomizerConfig : IConfig
	{
		#region Static

		public static IConfig Default = new RandomizerConfig();

		#endregion

		public RandomizerConfig()
		{
			EggMoves = new EggMovesConfig();
			Encounters = new EncountersConfig();
			Learnsets = new LearnsetsConfig();
			OverworldItems = new OverworldItemsConfig();
			PokemonInfo = new PokemonInfoConfig();
			Starters = new StartersConfig();
			Trainers = new TrainersConfig();
		}

		#region Read-only contract implementation

		IEggMoves IConfig.      EggMoves       => EggMoves;
		IEncounters IConfig.    Encounters     => Encounters;
		ILearnsets IConfig.     Learnsets      => Learnsets;
		IOverworldItems IConfig.OverworldItems => OverworldItems;
		IPokemonInfo IConfig.   PokemonInfo    => PokemonInfo;
		IStarters IConfig.      Starters       => Starters;
		ITrainers IConfig.      Trainers       => Trainers;

		#endregion

		public EggMovesConfig       EggMoves       { get; set; }
		public EncountersConfig     Encounters     { get; set; }
		public LearnsetsConfig      Learnsets      { get; set; }
		public OverworldItemsConfig OverworldItems { get; set; }
		public PokemonInfoConfig    PokemonInfo    { get; set; }
		public StartersConfig       Starters       { get; set; }
		public TrainersConfig       Trainers       { get; set; }
	}
}