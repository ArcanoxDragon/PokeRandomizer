using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using CommandLine.Options;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Randomization.Common;
using CtrDotNet.Pokemon.Randomization.Config;
using Newtonsoft.Json;

namespace CtrDotNet.Pokemon.RandomizerCLI
{
	class Program
	{
		private static OptionSet OptionSet;
		private static string ConfigFile;
		private static string RomPath;
		private static string GameLanguage;
		private static string OutputDir;

		private static void Main( string[] args )
		{
			List<Action> actions = new List<Action>();

			OptionSet = new OptionSet {
				{ "output-dir=|out-dir|output|out", "Specifies the output directory to which the patched files will be written", ( v ) => OutputDir = v },
				{ "language=", "Specifies the game language to use", ( v ) => GameLanguage = v },
				{ "rom-path=|rom", "Specifies the path to the ROM (must be an extracted 3DS ROM)", ( v ) => RomPath = v },
				{ "config=|cfg", "Specifies the path to a JSON configuration file for the randomizer", ( v ) => ConfigFile = v },
				{ "randomize", "Runs the randomizer on the specified ROM", s => actions.Add( Program.Randomize ) },
				{ "write-default-config|default-config", "Writes the default Randomizer configuration to a new JSON file", s => actions.Add( Program.WriteDefaultConfig ) },
				{ "help", "Prints this help message", s => actions.Add( Program.PrintHelp ) }
			};

			try
			{
				OptionSet.Parse( args );
			}
			catch ( Exception ex )
			{
				Console.WriteLine( ex.Message );
				return;
			}

			if ( actions.Count == 0 )
			{
				Program.PrintHelp();
				return;
			}

			actions.ForEach( a => a() );
		}

		private static void Randomize()
		{
			RandomizeAsync().Wait();
		}

		private static async Task RandomizeAsync()
		{
			if ( string.IsNullOrEmpty( RomPath ) )
			{
				Console.WriteLine( "ROM path is required" );
				return;
			}

			if ( !Directory.Exists( RomPath ) )
			{
				Console.WriteLine( $"ROM path not found: {Path.GetFullPath( RomPath )}" );
				return;
			}

			RandomizerConfig config;
			Language lang = Language.English;

			if ( !string.IsNullOrEmpty( GameLanguage ) )
			{
				if ( !Enum.TryParse( GameLanguage, out lang ) )
				{
					Console.WriteLine( $"Invalid game language: {GameLanguage}" );
					return;
				}
			}

			if ( ConfigFile == null )
			{
				config = RandomizerConfig.Default.AsEditable();
			}
			else
			{
				try
				{
					var json = JsonSerializer.CreateDefault();

					using ( var fs = new FileStream( Path.GetFullPath( ConfigFile ), FileMode.Open, FileAccess.Read, FileShare.Read ) )
					using ( var sr = new StreamReader( fs ) )
					using ( var jr = new JsonTextReader( sr ) )
					{
						config = json.Deserialize<RandomizerConfig>( jr );
					}
				}
				catch ( Exception ex )
				{
					Console.WriteLine( $"Error reading configuration file: {ex.Message}" );
					return;
				}
			}

			try
			{
				int fileCount = Directory.GetFiles( Path.Combine( RomPath, "RomFS", "a" ), "*", SearchOption.AllDirectories ).Length;
				GameConfig game = new GameConfig( fileCount );
				IRandomizer randomizer = Randomizer.GetRandomizer( game, config );

				await game.Initialize( RomPath, lang );

				if ( !string.IsNullOrEmpty( OutputDir ) )
					game.OutputPathOverride = OutputDir;

				await randomizer.RandomizeAll( null, CancellationToken.None );
			}
			catch ( Exception ex )
			{
				Console.WriteLine( $"An error occurred while randomizing the game:\n\n\t{ex.Message}" );
			}
		}

		private static void WriteDefaultConfig()
		{
			var jsonSettings = new JsonSerializerSettings { Formatting = Formatting.Indented };
			var json = JsonSerializer.Create( jsonSettings );

			using ( var fs = new FileStream( "Config.Default.json", FileMode.Create, FileAccess.Write, FileShare.None ) )
			using ( var sw = new StreamWriter( fs ) )
			{
				json.Serialize( sw, RandomizerConfig.Default );
			}
		}

		private static void PrintHelp()
		{
			OptionSet.WriteOptionDescriptions( Console.Out );

			Console.WriteLine( "\nThe ROM path must point to a directory containing an extracted 3DS Pokemon ROM, containing all the game files including ExeFS and RomFS" );
			Console.WriteLine( "If no output directory is specified, the randomization will be peformed in-place.\n" +
							   "THIS MEANS THE ORIGINAL GAME FILES WILL BE OVERWRITTEN.\n" +
							   "Otherwise, any modified files will be saved to the specified directory with the original folder structure so they can\n" +
							   "be used as a patch for tools such as Luma3DS" );

			Console.WriteLine( "\nIf specified, the game language must be one of the following:" );

			foreach ( var lang in Enum.GetNames( typeof( Language ) ) )
				Console.WriteLine( $"\t{lang}" );
		}
	}
}