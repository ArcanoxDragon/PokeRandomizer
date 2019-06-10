using PokeRandomizer.Config.Abstract;

namespace PokeRandomizer.Config
{
	public class StartersConfig : IStarters
	{
		public bool RandomizeStarters  { get; set; } = true;
		public bool AllowLegendaries   { get; set; }
		public bool StartersOnly       { get; set; }
		public bool OnlyElementalTypes { get; set; }
	}
}