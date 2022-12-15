using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.CTR.Cro;
using NUnit.Framework;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Garc;
using PokeRandomizer.Common.Reference;

namespace PokeRandomizer.Tests.Common
{
	public abstract class ScriptSearchTestBase
	{
		protected abstract int      MaxGarcIndex { get; }
		protected abstract string[] SkipGarcs    { get; }
		protected abstract uint[]   SearchValues { get; }
		protected abstract byte[]   SearchBytes  { get; }

		protected GameConfig Game { get; set; }

		protected async Task DoFindGarcScripts(int min = 000, int max = -1)
		{
			await CleanAndMakeDir("Garc");
			await CleanAndMakeDir("Script");

			await ForEachGarc(async (gRef, garc, garcIdx) => {
				var garcData = await garc.Write();
				var garcOutPath = Path.Combine("Garc", $"{gRef.RomFsPath}.bin".Replace('\\', '_'));

				await File.WriteAllBytesAsync(garcOutPath, garcData);

				await ForEachGarcFile(garc, async (file, fileIdx) => {
					var fileOutPath = Path.Combine("Garc", $"{gRef.RomFsPath}.{fileIdx}.bin".Replace('\\', '_'));

					await File.WriteAllBytesAsync(fileOutPath, file);

					if (DoSearchValues(file, SearchValues.Select(BitConverter.GetBytes), out var matches, out _))
					{
						await TestContext.Progress.WriteLineAsync($"\t!!!!!!!!!!!!! FOUND SCRIPT MAGIC: {gRef.RomFsPath}:{fileIdx} @ 0x{matches[0]:X} !!!!!!!!!!!!!");

						if (matches[0] < 4)
							// Need 4 bytes before magic; if not, it's not a real script
							return;

						var outFileName = $"{gRef.RomFsPath}.{fileIdx}.script.bin".Replace('\\', '_');
						var outFilePath = Path.Combine("Script", outFileName);

						await File.WriteAllBytesAsync(outFilePath, file.Skip(matches[0] - 4).ToArray());

						var outScriptPath = Path.Combine("Script", outFileName.Replace(".bin", ".txt"));

						await DisassembleScript(outFilePath, outScriptPath);
					}
				});
			}, min, max);
		}

		protected async Task ForEachGarc(Func<GarcReference, ReferencedGarc, int, Task> action, int min = 000, int max = -1)
		{
			if (max < 0)
				max = MaxGarcIndex;

			for (var garcIdx = min; garcIdx <= max; garcIdx++)
			{
				var gRef = new GarcReference(garcIdx, default);

				if (SkipGarcs.Contains(gRef.RomFsPath))
				{
					await TestContext.Progress.WriteLineAsync($"Skipping GARC {gRef.RomFsPath}");
					continue;
				}

				await TestContext.Progress.WriteLineAsync($"Processing GARC {gRef.RomFsPath}...");

				var garc = await Game.GetGarc(gRef, true);

				await action(gRef, garc, garcIdx);
			}
		}

		protected async Task ForEachGarcFile(ReferencedGarc garc, Func<byte[], int, Task> action, int min = 000, int max = -1)
		{
			if (max < 0)
				max = MaxGarcIndex;

			for (var fileIdx = 0; fileIdx < garc.Garc.FileCount; fileIdx++)
			{
				var file = await garc.GetFile(fileIdx);

				await action(file, fileIdx);
			}
		}

		protected async Task ForEachGarcFile(Func<GarcReference, byte[], int, int, Task> action, int min = 000, int max = -1)
		{
			if (max < 0)
				max = MaxGarcIndex;

			await ForEachGarc(async (gRef, garc, garcIdx) => {
				await ForEachGarcFile(garc, (file, fileIdx) => action(gRef, file, garcIdx, fileIdx));
			});
		}

		protected async Task ForEachCro(Func<CroNames, CroFile, Task> action)
		{
			foreach (var croName in Enum.GetValues(typeof(CroNames)).Cast<CroNames>())
			{
				await TestContext.Progress.WriteLineAsync($"Processing CRO file {croName}...");
				CroFile cro;

				try
				{
					cro = await Game.GetCroFile(croName);
				}
				catch (FileNotFoundException)
				{
					continue;
				}

				await action(croName, cro);
			}
		}

		protected async Task CleanAndMakeDir(string dir)
		{
			if (Directory.Exists(dir))
				Directory.Delete(dir, true);

			await Task.Delay(750);

			Directory.CreateDirectory(dir);

			await Task.Delay(750);
		}

		protected async Task DisassembleScript(string binaryPath, string scriptPath)
		{
			try
			{
				var scriptBinaryData = await File.ReadAllBytesAsync(binaryPath);
				var amx = new pkNX.Structures.Amx(scriptBinaryData);

				if (!amx.DataChunk.Any() && !amx.ParseScript.Any())
				{
					// Not a script
					try { File.Delete(binaryPath); }
					catch
					{
						/**/
					}

					return;
				}

				await using var scriptTextStream = new FileStream(scriptPath, FileMode.Create, FileAccess.Write);
				await using var writer = new StreamWriter(scriptTextStream);

				Task Line(string line = "") => writer.WriteLineAsync(line);

				// Write summary header
				await Line("/**");
				await Line(" * Script Information: ");

				foreach (var line in amx.SummaryLines)
					await Line($" * {line}");

				await Line(" */" + Environment.NewLine);

				if (amx.Publics != null)
				{
					// Write publics
					await Line("/** Publics */" + Environment.NewLine);

					foreach (var @public in amx.Publics.Where(p => p != null))
						await Line($"0x{@public.Address:X8}: {@public.Name}");

					await Line();
				}

				if (amx.Natives != null)
				{
					// Write natives
					await Line("/** Natives */" + Environment.NewLine);

					foreach (var native in amx.Natives.Where(p => p != null))
						await Line($"0x{native.Address:X8}: {native.Name}");

					await Line();
				}

				if (amx.Libraries != null)
				{
					// Write libraries
					await Line("/** Libraries */" + Environment.NewLine);

					foreach (var library in amx.Libraries.Where(p => p != null))
						await Line($"0x{library.Address:X8}: {library.Name}");

					await Line();
				}

				if (amx.PublicVars != null)
				{
					// Write public vars
					await Line("/** PublicVars */" + Environment.NewLine);

					foreach (var publicVar in amx.PublicVars.Where(p => p != null))
						await Line($"0x{publicVar.Address:X8}: {publicVar.Name}");

					await Line();
				}

				// Write data section
				await Line("/** Data Section */" + Environment.NewLine);

				foreach (var line in amx.DataChunk)
					await Line(line);

				await Line();

				// Write code section
				await Line("/** Code Section */" + Environment.NewLine);

				foreach (var line in amx.ParseScript)
					await Line(line);

				// Newline at end of file
				await Line();
			}
			catch (Exception ex)
			{
				await TestContext.Error.WriteLineAsync($"Could not process script file: {binaryPath}");
				TestContext.Error.WriteLine(ex);
			}
		}

		protected static bool DoSearchValues(byte[] data, IEnumerable<byte[]> values, out int[] matches, out int[] strides, bool checkStride = false)
		{
			matches = values.SelectMany(val => ArraySearch(data, val))
							.OrderBy(v => v)
							.ToArray();

			if (!matches.Any())
			{
				strides = Array.Empty<int>();
				return false;
			}

			strides = new int[matches.Length - 1];

			if (matches.Length == 1 || !checkStride)
			{
				return true;
			}

			var stride = matches[1] - matches[0];
			var strideConsistent = true;

			for (var m = 1; m < matches.Length; m++)
			{
				var thisStride = matches[m] - matches[m - 1];

				strides[m - 1] = thisStride;

				//if ( thisStride < 0 )
				if (thisStride != stride)
				{
					strideConsistent = false;
					break;
				}
			}

			if (strideConsistent)
			{
				return true;
			}

			return false;
		}

		protected static IEnumerable<int> ArraySearch(byte[] source, byte[] target, bool allowOverlap = false)
		{
			if (source.Length == 0 || target.Length == 0)
				yield break;

			for (var i = 0; i <= source.Length - target.Length; i++)
			{
				if (source[i] == target[0])
				{
					var match = true;

					for (var j = 0; j < target.Length; j++)
					{
						if (source[i + j] != target[j])
						{
							match = false;
						}
					}

					if (match)
						yield return i;

					if (!allowOverlap)
						i += target.Length - 1;
				}
			}
		}
	}
}