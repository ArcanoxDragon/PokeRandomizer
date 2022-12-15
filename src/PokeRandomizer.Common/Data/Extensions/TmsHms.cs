using PokeRandomizer.Common.Structures.ExeFS.Common;

namespace PokeRandomizer.Common.Data.Extensions
{
	public static class TmsHmsExtensions
	{
		public static Move GetMove(this TmsHms tmsHms, int tmHm)
			=> tmHm >= tmsHms.TmIds.Length
				   ? (Move) tmsHms.HmIds[tmHm - tmsHms.TmIds.Length]
				   : (Move) tmsHms.TmIds[tmHm];
	}
}