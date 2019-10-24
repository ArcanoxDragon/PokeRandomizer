namespace PokeRandomizer.Config.Abstract
{
	public interface IStarters
	{
		bool RandomizeStarters  { get; }
		bool AllowLegendaries   { get; }
		bool StartersOnly       { get; }
		bool ElementalTypeTriangle { get; }
	}
}