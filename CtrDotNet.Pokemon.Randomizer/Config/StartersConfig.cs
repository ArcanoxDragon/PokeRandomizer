namespace CtrDotNet.Pokemon.Randomization.Config
{
	public class StartersConfig : IStarters
	{
		public bool AllowLegendaries { get; set; }
		public bool StartersOnly { get; set; }
	}
}