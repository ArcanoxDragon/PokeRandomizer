using CtrDotNet.CTR.Garc;

namespace CtrDotNet.Pokemon.Game
{
	public enum GameGeneration
	{
		Unknown = -1,
		Generation6,
		Generation7
	}

	public class GenerationInfo
	{
		public ushort GarcVersion { get; set; }
		public int SpeciesCount { get; set; }
	}

	public static class GameGenerationExtensions
	{
		public static GenerationInfo GetInfo( this GameGeneration gen )
		{
			switch ( gen )
			{
				case GameGeneration.Generation6:
					return new GenerationInfo {
						GarcVersion = GarcUtil.Version4,
						SpeciesCount = 721
					};
				case GameGeneration.Generation7:
					return new GenerationInfo {
						GarcVersion = GarcUtil.Version6,
						SpeciesCount = 802
					};
				default:
					return new GenerationInfo {
						GarcVersion = 0,
						SpeciesCount = -1
					};
			}
		}
	}
}