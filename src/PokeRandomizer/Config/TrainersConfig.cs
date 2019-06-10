using PokeRandomizer.Config.Abstract;

namespace PokeRandomizer.Config
{
	public class TrainersConfig : ITrainers
	{
		public bool    RandomizeTrainers  { get; set; } = true;
		public bool    FriendKeepsStarter { get; set; } = true;
		public decimal LevelMultiplier    { get; set; } = 1.0m;
		public bool    TypeThemed         { get; set; } = true;
		public bool    TypeThemedGyms     { get; set; } = true;
	}
}