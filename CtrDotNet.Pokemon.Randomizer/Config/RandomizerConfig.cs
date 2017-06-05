namespace CtrDotNet.Pokemon.Randomization.Config
{
	public class RandomizerConfig : IConfig
	{
		#region Static

		public static IConfig Default = new RandomizerConfig();

		#endregion

		public RandomizerConfig()
		{
			this.Starters = new StartersConfig();
			this.Encounters = new EncountersConfig();
			this.Learnsets = new LearnsetsConfig();
		}

		#region Read-only contract implementation

		IStarters IConfig.Starters => this.Starters;
		IEncounters IConfig.Encounters => this.Encounters;
		ILearnsets IConfig.Learnsets => this.Learnsets;

		#endregion

		public StartersConfig Starters { get; set; }
		public EncountersConfig Encounters { get; set; }
		public LearnsetsConfig Learnsets { get; set; }
	}
}