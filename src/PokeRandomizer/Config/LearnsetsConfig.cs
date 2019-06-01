namespace PokeRandomizer.Config
{
    public class LearnsetsConfig : ILearnsets
    {
	    public bool AtLeast4Moves { get; set; }
	    public int LearnAllMovesBy { get; set; } = 65;
	    public bool FavorSameType { get; set; } = true;
		public bool RandomizeLevels { get; set; }
    }
}
