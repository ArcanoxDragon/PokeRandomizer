using System.IO;
using System.Threading.Tasks;
using NUnit.Framework;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Tests.ORAS
{
	[SetUpFixture]
	public class ORASConfig
	{
		public static GameConfig GameConfig { get; private set; }
		public static string     OutputPath { get; private set; }

		[OneTimeSetUp]
		public async Task SetUp()
		{
			string romPath = Path.GetFullPath(Settings.RomPathOras);
			string romFsPath = Path.Combine(romPath, "RomFS");
			string exeFsPath = Path.Combine(romPath, "ExeFS");

			Assert.True(Directory.Exists(romPath), "ROM path does not exist");
			Assert.True(Directory.Exists(romFsPath), "ROM path does not contain a RomFS folder");
			Assert.True(Directory.Exists(exeFsPath), "ROM path does not contain an ExeFS folder");

			OutputPath = Path.Combine(TestContext.CurrentContext.TestDirectory, "Output");
			GameConfig = new GameConfig(GameVersion.ORAS);

			if (Directory.Exists(OutputPath))
			{
				foreach (var file in Directory.EnumerateFiles(OutputPath, "*", SearchOption.AllDirectories))
					File.Delete(file);

				Directory.Delete(OutputPath, true);
			}

			await GameConfig.Initialize(romPath, Language.English);
		}
	}
}