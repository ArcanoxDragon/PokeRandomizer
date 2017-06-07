namespace CtrDotNet.Pokemon.Randomization.Config
{
	public class EncountersConfig : IEncounters
	{
		public bool AllowLegendaries { get; set; } = true;
		public decimal LevelMultiplier { get; set; } = 1.0m;
		public bool TypePerSubArea { get; set; }
		public bool TypeThemedAreas { get; set; }
	}
}