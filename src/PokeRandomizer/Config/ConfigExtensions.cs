using PokeRandomizer.Config.Abstract;

namespace PokeRandomizer.Config
{
	public static class ConfigExtensions
	{
		public static RandomizerConfig AsEditable(this IConfig config)
		{
			return new RandomizerConfig {
				EggMoves = { RandomizeEggMoves = config.EggMoves.RandomizeEggMoves, FavorSameType = config.EggMoves.FavorSameType, SameTypePercentage = config.EggMoves.SameTypePercentage, },
				Encounters = {
					RandomizeEncounters = config.Encounters.RandomizeEncounters,
					AllowLegendaries = config.Encounters.AllowLegendaries,
					LevelMultiplier = config.Encounters.LevelMultiplier,
					TypePerSubArea = config.Encounters.TypePerSubArea,
					TypeThemedAreas = config.Encounters.TypeThemedAreas,
				},
				Learnsets = {
					RandomizeLearnsets = config.Learnsets.RandomizeLearnsets,
					AtLeast4Moves = config.Learnsets.AtLeast4Moves,
					FavorSameType = config.Learnsets.FavorSameType,
					LearnAllMovesBy = config.Learnsets.LearnAllMovesBy,
					RandomizeLevels = config.Learnsets.RandomizeLevels,
					SameTypePercentage = config.Learnsets.SameTypePercentage,
				},
				OverworldItems = {
					RandomizeOverworldItems = config.OverworldItems.RandomizeOverworldItems, AllowMasterBalls = config.OverworldItems.AllowMasterBalls, RandomizeTMs = config.OverworldItems.RandomizeTMs, AllowMegaStones = config.OverworldItems.AllowMegaStones,
				},
				PokemonInfo = {
					RandomizeAbilities = config.PokemonInfo.RandomizeAbilities,
					AllowWonderGuard = config.PokemonInfo.AllowWonderGuard,
					RandomizeTypes = config.PokemonInfo.RandomizeTypes,
					RandomizePrimaryTypes = config.PokemonInfo.RandomizePrimaryTypes,
					RandomizeSecondaryTypes = config.PokemonInfo.RandomizeSecondaryTypes,
				},
				Starters = {
					RandomizeStarters = config.Starters.RandomizeStarters, AllowLegendaries = config.Starters.AllowLegendaries, StartersOnly = config.Starters.StartersOnly, ElementalTypeTriangle = config.Starters.ElementalTypeTriangle,
				},
				Trainers = {
					RandomizeTrainers = config.Trainers.RandomizeTrainers,
					FriendKeepsStarter = config.Trainers.FriendKeepsStarter,
					LevelMultiplier = config.Trainers.LevelMultiplier,
					TypeThemed = config.Trainers.TypeThemed,
					TypeThemedGyms = config.Trainers.TypeThemedGyms,
				}
			};
		}
	}
}