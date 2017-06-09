namespace CtrDotNet.Pokemon.Randomization.Config
{
	public static class ConfigExtensions
	{
		public static RandomizerConfig AsEditable( this IConfig config )
		{
			return new RandomizerConfig {
				Abilities = {
					AllowWonderGuard = config.Abilities.AllowWonderGuard
				},
				EggMoves = {
					FavorSameType = config.EggMoves.FavorSameType
				},
				Encounters = {
					AllowLegendaries = config.Encounters.AllowLegendaries,
					LevelMultiplier = config.Encounters.LevelMultiplier,
					TypePerSubArea = config.Encounters.TypePerSubArea,
					TypeThemedAreas = config.Encounters.TypeThemedAreas
				},
				Learnsets = {
					AtLeast4Moves = config.Learnsets.AtLeast4Moves,
					FavorSameType = config.Learnsets.FavorSameType,
					LearnAllMovesBy = config.Learnsets.LearnAllMovesBy,
					RandomizeLevels = config.Learnsets.RandomizeLevels
				},
				Starters = {
					AllowLegendaries = config.Starters.AllowLegendaries,
					StartersOnly = config.Starters.StartersOnly
				},
				Trainers = {
					FriendKeepsStarter = config.Trainers.FriendKeepsStarter,
					LevelMultiplier = config.Trainers.LevelMultiplier,
					TypeThemed = config.Trainers.TypeThemed
				}
			};
		}
	}
}