using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Structures.ExeFS.Common;

namespace CtrDotNet.Pokemon.Extensions
{
	public static class TmsHmsExtensions
	{
		public static Move GetMove( this TmsHms tmsHms, int tmHm )
			=> tmHm >= tmsHms.TmIds.Length
				   ? (Move) tmsHms.HmIds[ tmHm - tmsHms.TmIds.Length ]
				   : (Move) tmsHms.TmIds[ tmHm ];
	}
}