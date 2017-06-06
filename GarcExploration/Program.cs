using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Garc;
using CtrDotNet.Pokemon.Reference;
using GarcExploration.Properties;

namespace GarcExploration
{
	class Program
	{
		private static void Main()
		{
			AsyncMain().Wait();
		}

		private static async Task AsyncMain()
		{
			string romPath = Path.GetFullPath( Settings.Default.RomPath );

			GameConfig game = new GameConfig( GameVersion.ORAS );
			await game.Initialize( romPath, Language.English );

			for ( int gIndex = 0; gIndex <= 298; gIndex++ )
			{
				var gRef = new GarcReference( gIndex, GarcNames.Unknown );
				ReferencedGarc garc;

				try
				{
					garc = await game.GetGarc( gRef );
					Debug.WriteLine( $"Garc file {gRef.RomFsPath} has {garc.Garc.FileCount} files" );
				}
				catch
				{
					try
					{
						garc = await game.GetGarc( gRef, useLz: true );
						Debug.WriteLine( $"Garc file {gRef.RomFsPath} has {garc.Garc.FileCount} files and was compressed" );
					}
					catch
					{
						// ignored
					}
				}
			}
		}
	}
}