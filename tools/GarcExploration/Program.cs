using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Garc;
using PokeRandomizer.Common.Reference;

namespace GarcExploration
{
	class Program
	{
		private static readonly string[] GarcsToSkip = {
			@"a\0\0\8", // Giant 1.1GB GARC
			@"a\0\0\7", // Ton of files; pokemon models
		};

		private static async Task Main()
		{
			string romPath = Path.GetFullPath( Settings.RomPath );

			GameConfig game = new GameConfig( GameVersion.XY );
			await game.Initialize( romPath, Language.English );

			if ( Directory.Exists( "Garc" ) )
				Directory.Delete( "Garc", true );

			await Task.Delay( 2000 );

			Directory.CreateDirectory( "Garc" );

			await Task.Delay( 2000 );

			for ( int gIndex = 0; gIndex <= 999; gIndex++ )
			{
				var            gRef = new GarcReference( gIndex, GarcNames.Unknown );
				ReferencedGarc garc;

				try
				{
					garc = await game.GetGarc( gRef );
					WriteLine( $"Garc file {gRef.RomFsPath} has {garc.Garc.FileCount} files" );

					await WriteGarc( garc, gRef );
				}
				catch ( FileNotFoundException )
				{
					WriteLine( $"Max GARC: {gIndex - 1}" );
					break;
				}
				catch
				{
					try
					{
						garc = await game.GetGarc( gRef, useLz: true );
						WriteLine( $"Garc file {gRef.RomFsPath} has {garc.Garc.FileCount} files and was compressed" );

						await WriteGarc( garc, gRef );
					}
					catch
					{
						// ignored
					}
				}
			}
		}

		private static async Task WriteGarc( ReferencedGarc garc, GarcReference gRef )
		{
			if ( GarcsToSkip.Contains( gRef.RomFsPath ) )
				return;

			var garcData = await garc.Garc.Write();

			await File.WriteAllBytesAsync( $"Garc\\{gRef.RomFsPath.Replace( '\\', '_' )}.bin", garcData );

			for ( var file = 0; file < garc.Garc.FileCount; file++ )
			{
				var fileData = await garc.GetFile( file );

				await File.WriteAllBytesAsync( $"Garc\\{gRef.RomFsPath.Replace( '\\', '_' )}.{file}.bin", fileData );
			}
		}

		private static void WriteLine( string line )
		{
			Debug.WriteLine( line );
			Console.WriteLine( line );
		}
	}
}