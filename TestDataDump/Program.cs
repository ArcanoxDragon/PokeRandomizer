using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using CtrDotNet.Pokemon.GameData;

namespace TestDataDump
{
	class Program
	{
		private static readonly Regex BlankLineRegex = new Regex( @"\[~ (\d+)\]" );

		private static string OutPath;
		private static GameConfig Config;

		private static void Main()
		{
			Initialize();
		}

		private static async void Initialize()
		{
			if ( !Enum.TryParse<GameVersion>( Properties.Settings.Default.GameType, out var gameType ) )
			{
				Console.WriteLine( "Could not parse game type" );
				return;
			}

			OutPath = Path.Combine(
				Path.GetDirectoryName( Process.GetCurrentProcess().MainModule.FileName ),
				Properties.Settings.Default.OutputPath
			);

			if ( !Directory.Exists( OutPath ) )
				Directory.CreateDirectory( OutPath );

			string romPath = Path.GetFullPath( Properties.Settings.Default.RomPath );
			string romFsPath = Path.Combine( romPath, "RomFS" );
			string exeFsPath = Path.Combine( romPath, "ExeFS" );

			Config = new GameConfig( gameType );
			await Config.Initialize( romFsPath, exeFsPath, Language.English );

			DumpSpeciesNames();
			DumpAbilityNames();
			DumpItemNames();
			DumpMoveNames();
		}

		private static void DumpStringTable( TextName tableName, string fileName )
		{
			TextReference textRef = Config.GameText.FirstOrDefault( tr => tr.Name == tableName );

			if ( textRef == null )
			{
				Console.WriteLine( $"Could not find string table for text reference {tableName}" );
				return;
			}

			string[] strings = Config.GameTextStrings[ textRef.Index ];
			string filePath = Path.Combine( OutPath, $"{fileName}.db" );

			File.Delete( filePath );
			File.AppendAllLines( filePath, strings.Select( ( s, i ) => $"{i}={s}" )
												  .Where( s => !BlankLineRegex.IsMatch( s ) ) );
		}

		private static void DumpSpeciesNames() => DumpStringTable( TextName.SpeciesNames, "Species" );
		private static void DumpAbilityNames() => DumpStringTable( TextName.AbilityNames, "Abilities" );
		private static void DumpItemNames() => DumpStringTable( TextName.ItemNames, "Items" );
		private static void DumpMoveNames() => DumpStringTable( TextName.MoveNames, "Moves" );
	}
}