﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Reference;
using PokeRandomizer.Tests.Common;
using GameVersion = PokeRandomizer.Common.Game.GameVersion;
using TextFile = PokeRandomizer.Common.Structures.RomFS.Common.TextFile;

namespace PokeRandomizer.Tests.XY
{
	[TestFixture]
	public class XyScriptSearchTests : ScriptSearchTestBase
	{
		protected override uint[] SearchValues => new uint[] {
			0x0A0AF1E0, // Code (32-bit)
			0x0A0AF1E1, // Code (64-bit)
			0x0A0AF1E2, // Code (16-bit)
			0x0A0AF1EF, // Debug
		};

		protected override byte[] SearchBytes => new byte[] { 0x5A, 0x4F, 0x04, 0x00, 0x18, 0x00, };

		protected override string[] SkipGarcs => new[] {
			@"a\0\0\7", // Really really massive (1+GB) file
		};

		protected override int MaxGarcIndex => 270;

		[OneTimeSetUp]
		public async Task SetUpGame()
		{
			Game = new GameConfig(GameVersion.XY);

			await Game.Initialize(Settings.RomPathXy, Language.English);
		}

		[Test]
		public async Task FindGarcScripts()
		{
			await DoFindGarcScripts();
		}

		[Test]
		public async Task SearchGarcs()
		{
			await CleanAndMakeDir("Garc");
			await CleanAndMakeDir("Found");

			await ForEachGarc(async (gRef, garc, garcIdx) => {
				var garcData = await garc.Write();
				var garcOutPath = Path.Combine("Garc", $"{gRef.RomFsPath}.bin".Replace('\\', '_'));

				await File.WriteAllBytesAsync(garcOutPath, garcData);

				if (DoSearchValues(garcData, new List<byte[]> { SearchBytes }, out var matches, out _))
				{
					await TestContext.Progress.WriteLineAsync($"\t!!!!!!!!!!!!! FOUND PATTERN: {gRef.RomFsPath} @ 0x{matches[0]:X} !!!!!!!!!!!!!");

					var outFileName = $"{gRef.RomFsPath}.found.bin".Replace('\\', '_');
					var outFilePath = Path.Combine("Found", outFileName);

					await File.WriteAllBytesAsync(outFilePath, garcData);
				}

				await ForEachGarcFile(garc, async (file, fileIdx) => {
					var fileOutPath = Path.Combine("Garc", $"{gRef.RomFsPath}.{fileIdx}.bin".Replace('\\', '_'));

					await File.WriteAllBytesAsync(fileOutPath, file);

					if (DoSearchValues(file, new List<byte[]> { SearchBytes }, out matches, out _))
					{
						await TestContext.Progress.WriteLineAsync($"\t!!!!!!!!!!!!! FOUND PATTERN: {gRef.RomFsPath}:{fileIdx} @ 0x{matches[0]:X} !!!!!!!!!!!!!");

						var outFileName = $"{gRef.RomFsPath}.{fileIdx}.found.bin".Replace('\\', '_');
						var outFilePath = Path.Combine("Found", outFileName);

						await File.WriteAllBytesAsync(outFilePath, file);
					}
				});
			});
		}

		[Test]
		public async Task FindGarcText()
		{
			await CleanAndMakeDir("Garc");
			await CleanAndMakeDir("Text");

			await ForEachGarc(async (gRef, garc, _) => {
				var garcData = await garc.Write();
				var garcOutPath = Path.Combine("Garc", $"{gRef.RomFsPath}.bin".Replace('\\', '_'));

				await File.WriteAllBytesAsync(garcOutPath, garcData);

				await ForEachGarcFile(garc, async (file, fileIdx) => {
					var fileOutPath = Path.Combine("Garc", $"{gRef.RomFsPath}.{fileIdx}.bin".Replace('\\', '_'));

					await File.WriteAllBytesAsync(fileOutPath, file);

					try
					{
						var textFile = new TextFile(Game.Version);

						textFile.Read(file);

						await TestContext.Progress.WriteLineAsync($"Found valid text file: {gRef.RomFsPath}:{fileIdx}");

						var outFileName = $"{gRef.RomFsPath}.{fileIdx}.txt".Replace('\\', '_');
						var outFilePath = Path.Combine("Text", outFileName);

						await File.WriteAllLinesAsync(outFilePath, textFile.Lines);
					}
					catch
					{
						// Ignored
					}
				});
			});
		}

		[Test]
		public async Task FindScripts2()
		{
			var searchBytesAmxL = "amx"u8.ToArray();
			var searchBytesAmxU = "AMX"u8.ToArray();
			var searchBytesFldItem = "fld_item"u8.ToArray();

#pragma warning disable 1998
			await ForEachCro(async (name, file) => {
#pragma warning restore 1998
				if (DoSearchValues(file.Data, new List<byte[]> { searchBytesAmxL, searchBytesAmxU }, out var matches, out _))
				{
					var addresses = string.Join(", ", matches.Select(m => $"{m:X4}"));

					await TestContext.Progress.WriteLineAsync($"Found \"amx\" in file {name}: {addresses}");
				}

				if (DoSearchValues(file.Data, new List<byte[]> { searchBytesFldItem }, out matches, out _))
				{
					var addresses = string.Join(", ", matches.Select(m => $"0x{m:X6}"));

					await TestContext.Progress.WriteLineAsync($"Found \"fld_item\" in file {name}: {addresses}");
				}

				if (DoSearchValues(file.Data, SearchValues.Select(BitConverter.GetBytes), out matches, out _))
				{
					var addresses = string.Join(", ", matches.Select(m => $"0x{m:X6}"));

					await TestContext.Progress.WriteLineAsync($"Found AMX header in file {name}: {addresses}");
				}
			});
		}

		[Test]
		public async Task TestCro()
		{
			foreach (var croName in Enum.GetValues(typeof(CroNames)).Cast<CroNames>())
			{
				try
				{
					await TestContext.Progress.WriteLineAsync($"Searching CRO {croName}...");

					var cro = await Game.GetCroFile(croName);
					var code = await cro.GetCodeSection();
					var data = await cro.GetDataSection();

					if (DoSearchValues(code, SearchValues.Select(BitConverter.GetBytes), out var matches, out var strides))
					{
						await TestContext.Progress.WriteLineAsync($"\t!!!!!!!!!!!!! FOUND MATCH: {croName}.code @ 0x{matches[0]:X} !!!!!!!!!!!!!");
						await TestContext.Progress.WriteLineAsync($"\tStrides: {string.Join(",", strides)}");

						await File.WriteAllBytesAsync($"{croName}.code.bin".Replace('\\', '_'), code);
					}

					if (DoSearchValues(data, SearchValues.Select(BitConverter.GetBytes), out matches, out strides))
					{
						await TestContext.Progress.WriteLineAsync($"\t!!!!!!!!!!!!! FOUND MATCH: {croName}.data @ 0x{matches[0]:X} !!!!!!!!!!!!!");
						await TestContext.Progress.WriteLineAsync($"\tStrides: {string.Join(",", strides)}");

						await File.WriteAllBytesAsync($"{croName}.data.bin".Replace('\\', '_'), data);
					}
				}
				catch (Exception ex)
				{
					await TestContext.Progress.WriteLineAsync($"Error for {croName}: {ex.Message}");
					// Ignored
				}
			}
		}

		[Test]
		public async Task TestCodeBin()
		{
			var codeBin = await Game.GetCodeBin();

			if (DoSearchValues(codeBin.Data, SearchValues.Select(BitConverter.GetBytes), out var matches, out var strides))
			{
				await TestContext.Progress.WriteLineAsync($"\t!!!!!!!!!!!!! FOUND MATCH: .code.bin @ 0x{matches[0]:X} !!!!!!!!!!!!!");
				await TestContext.Progress.WriteLineAsync($"\tStrides: {string.Join(",", strides)}");

				await File.WriteAllBytesAsync(".code.bin".Replace('\\', '_'), codeBin.Data);
			}
		}
	}
}