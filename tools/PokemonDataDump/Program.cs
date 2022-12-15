using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Reference;

namespace PokemonDataDump
{
	class Program
	{
		private static readonly Regex BlankLineRegex = new(@"\[~ (\d+)\]");

		private static string     OutPath;
		private static GameConfig Config;

		private static void Main()
		{
			Initialize().Wait();
		}

		private static async Task Initialize()
		{
			try
			{
				if (!Enum.TryParse<GameVersion>(Settings.GameType, out var gameType))
				{
					Console.WriteLine("Could not parse game type");
					return;
				}

				OutPath = Path.Combine(Directory.GetCurrentDirectory(), Settings.OutputPath);

				if (!Directory.Exists(OutPath))
					Directory.CreateDirectory(OutPath);

				string romPath = Path.GetFullPath(Settings.RomPath);

				Config = new GameConfig(gameType);
				await Config.Initialize(romPath, Language.English);

				await Task.WhenAll(DumpSpeciesNames(),
								   DumpAbilityNames(),
								   DumpItemNames(),
								   DumpMoveNames(),
								   DumpTypeNames());
			}
			catch (Exception e)
			{
				Console.WriteLine("Error dumping data:");
				Console.WriteLine(e);

				while (Console.KeyAvailable)
					Console.ReadKey(true);

				Console.ReadKey(true);
			}
		}

		private static async Task DumpStringTable(TextNames tableName, string fileName)
		{
			var textFile = await Config.GetTextFile(tableName);

			if (textFile == null)
			{
				Console.WriteLine($"Could not find string table for text file {tableName}");
				return;
			}

			var strings = textFile.Lines;
			string filePath = Path.Combine(OutPath, $"{fileName}.db");

			File.Delete(filePath);

			await using var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None);
			await using var sw = new StreamWriter(fs);
			var lines = strings.Select((s, i) => $"{i}={s}")
							   .Where(s => !BlankLineRegex.IsMatch(s));

			foreach (var line in lines)
				await sw.WriteLineAsync(line);
		}

		private static Task DumpSpeciesNames() => DumpStringTable(TextNames.SpeciesNames, "Species");
		private static Task DumpAbilityNames() => DumpStringTable(TextNames.AbilityNames, "Abilities");
		private static Task DumpItemNames() => DumpStringTable(TextNames.ItemNames, "Items");
		private static Task DumpMoveNames() => DumpStringTable(TextNames.MoveNames, "Moves");
		private static Task DumpTypeNames() => DumpStringTable(TextNames.Types, "PokemonTypes");
	}
}