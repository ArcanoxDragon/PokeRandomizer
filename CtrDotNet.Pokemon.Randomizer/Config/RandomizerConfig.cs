namespace CtrDotNet.Pokemon.Randomization.Config
{
	public class RandomizerConfig : IConfig
	{
		#region Static

		public static IConfig Default = new RandomizerConfig();

		#endregion

		public RandomizerConfig()
		{
			this.Abilities = new AbilitiesConfig();
			this.EggMoves = new EggMovesConfig();
			this.Encounters = new EncountersConfig();
			this.Learnsets = new LearnsetsConfig();
			this.Starters = new StartersConfig();
			this.Trainers = new TrainersConfig();
		}

		#region Read-only contract implementation

		IAbilities IConfig.Abilities => this.Abilities;
		IEggMoves IConfig.EggMoves => this.EggMoves;
		IEncounters IConfig.Encounters => this.Encounters;
		ILearnsets IConfig.Learnsets => this.Learnsets;
		IStarters IConfig.Starters => this.Starters;
		ITrainers IConfig.Trainers => this.Trainers;

		#endregion

		public AbilitiesConfig Abilities { get; set; }
		public EggMovesConfig EggMoves { get; set; }
		public EncountersConfig Encounters { get; set; }
		public LearnsetsConfig Learnsets { get; set; }
		public StartersConfig Starters { get; set; }
		public TrainersConfig Trainers { get; set; }
	}
}