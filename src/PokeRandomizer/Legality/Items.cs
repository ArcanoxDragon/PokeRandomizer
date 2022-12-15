using System;
using System.Linq;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Legality
{
	public partial class Items
	{
		public static readonly ushort[] MegaStones = { 659, 660, 678, 661, 679, 656, 675, 671, 676, 672, 662, 663, 658, 670, 680, 666, 669, 664, 657, 681, 667, 665, 682, 668, 677, 684, 685, 683, 673, 674, 770, 762, 760, 761, 753, 752, 754, 759, 767, 755, 763, 769, 758, 768, 756, 757, 764, };

		public static ushort[] GetValidOverworldItems(GameVersion gameVersion)
		{
			switch (gameVersion)
			{
				case GameVersion.XY:
					return Pouch_Items_XY.Concat(Pouch_Medicine_XY).Concat(TM_XY).ToArray();
				case GameVersion.ORAS:
					return Pouch_Items_ORAS.Concat(Pouch_Medicine_ORAS).Concat(TM_ORAS).ToArray();
				default:
					throw new NotSupportedException($"No overworld item data for game version \"{gameVersion}\"");
			}
		}
	}
}