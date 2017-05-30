namespace CtrDotNet.Pokemon.Randomizer.Config
{
	public class RandomizerConfig
	{
		public RandomizerConfig()
		{
			this.Starters = new StartersConfig();
		}

		public StartersConfig Starters { get; set; }
	}
}