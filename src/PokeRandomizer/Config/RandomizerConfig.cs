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
			this.EggMoves       = new EggMovesConfig();
			this.Encounters     = new EncountersConfig();
			this.Learnsets      = new LearnsetsConfig();
			this.OverworldItems = new OverworldItemsConfig();
			this.PokemonInfo    = new PokemonInfoConfig();
			this.Starters       = new StartersConfig();
			this.Trainers       = new TrainersConfig();
		}

		#region Read-only contract implementation

		IEggMoves IConfig.      EggMoves       => this.EggMoves;
		IEncounters IConfig.    Encounters     => this.Encounters;
		ILearnsets IConfig.     Learnsets      => this.Learnsets;
		IOverworldItems IConfig.OverworldItems => this.OverworldItems;
		IPokemonInfo IConfig.   PokemonInfo    => this.PokemonInfo;
		IStarters IConfig.      Starters       => this.Starters;
		ITrainers IConfig.      Trainers       => this.Trainers;

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