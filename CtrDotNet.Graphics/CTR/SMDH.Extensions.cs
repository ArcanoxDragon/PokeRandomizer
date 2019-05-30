using System.Drawing;
using Icon = CtrDotNet.CTR.Icon;

namespace CtrDotNet.Graphics.CTR
{
	public static class SmdhExtensions
	{
		public static bool ChangeIcon( this Icon icon, Bitmap img )
		{
			if ( img.Width != icon.Size || img.Height != icon.Size )
				return false;

			icon.Bytes = Bclim.GetPixelData( img, 0x5 );
			return true;
		}
	}
}