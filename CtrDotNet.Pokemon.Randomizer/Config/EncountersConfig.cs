namespace CtrDotNet.Pokemon.Randomization.Config
{
	public class EncountersConfig : IEncounters
	{
		public bool AllowLegendaries { get; set; } = true;
		public bool TypeThemedAreas { get; set; } 
		public decimal LevelMultiplier { get; set; } = 1.0m;
	}
}