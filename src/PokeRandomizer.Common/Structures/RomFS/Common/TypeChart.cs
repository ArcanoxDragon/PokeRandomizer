namespace PokeRandomizer.Common.Structures.RomFS.Common
{
	public static class TypeChart
	{
		public enum EffectivenessType
		{
			Unknown = -1,
			Normal,
			NoEffect,
			SuperEffective,
			NotVeryEffective
		}

		private static readonly EffectivenessType[] EffectivenessTypes = {
			EffectivenessType.NoEffect,
			EffectivenessType.Unknown, // unused
			EffectivenessType.NotVeryEffective,
			EffectivenessType.Unknown, // unused
			EffectivenessType.Normal,
			EffectivenessType.Unknown, // unused
			EffectivenessType.Unknown, // unused
			EffectivenessType.Unknown, // unused
			EffectivenessType.SuperEffective
		};

		public static EffectivenessType[ , ] GetGrid( int gridSize, byte[] vals )
		{
			EffectivenessType[ , ] grid = new EffectivenessType[ gridSize, gridSize ];

			for ( int i = 0; i < vals.Length; i++ )
			{
				int x = i % gridSize;
				int y = i / gridSize;

				// Plop into image
				grid[ x, y ] = EffectivenessTypes[ vals[ i ] ];
			}

			return grid;
		}
	}
}