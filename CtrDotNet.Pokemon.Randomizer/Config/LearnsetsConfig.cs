namespace CtrDotNet.Pokemon.Randomization.Config
{
    public class LearnsetsConfig : ILearnsets
    {
	    public bool FavorSameType { get; set; } = true;
		public bool RandomizeLevels { get; set; }
    }
}
