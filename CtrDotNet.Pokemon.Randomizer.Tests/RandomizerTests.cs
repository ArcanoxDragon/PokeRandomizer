using System.IO;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Randomization.Common;
using CtrDotNet.Pokemon.Randomization.Config;
using NUnit.Framework;

namespace CtrDotNet.Pokemon.Randomizer.Tests
{
	[ TestFixture ]
	public class RandomizerTests
	{
		private string romOutputDir;

		public static IRandomizer Randomizer { get; set; }
		public static GameConfig Game { get; set; }

		public void SetUpOutputDirectory()
		{
			this.romOutputDir = Path.Combine( TestContext.CurrentContext.TestDirectory, "RomOutput" );

			if ( !Directory.Exists( this.romOutputDir ) )
				Directory.CreateDirectory( this.romOutputDir );
		}

		[ OneTimeSetUp ]
		public async Task TestLoadRandomizer()
		{
			this.SetUpOutputDirectory();

			Randomizer = Randomization.Common.Randomizer.GetRandomizerFor( GameVersion.ORAS );
			Game = new GameConfig( GameVersion.ORAS );

			string romPath = Path.GetFullPath( Properties.Settings.Default.RomPathORAS );
			string romFsPath = Path.Combine( romPath, "RomFS" );
			string exeFsPath = Path.Combine( romPath, "ExeFS" );

			Assert.True( Directory.Exists( romPath ), "ROM path does not exist" );
			Assert.True( Directory.Exists( romFsPath ), "ROM path does not contain a RomFS folder" );
			Assert.True( Directory.Exists( exeFsPath ), "ROM path does not contain an ExeFS folder" );

			await Game.Initialize( romFsPath, exeFsPath, Language.English );

			RandomizerConfig randConfig = new RandomizerConfig {
				Starters = {
					StartersOnly = false
				},
				Encounters = {
					LevelMultiplier = 1.25m,
					TypeThemedAreas = true
				},
				Learnsets = {
					RandomizeLevels = true
				}
			};

			Randomizer.Initialize( Game, randConfig );

			Game.OutputPathOverride = this.romOutputDir;
		}

		[ Test ]
		public async Task RandomizeStarters()
		{
			await Randomizer.RandomizeStarters();
		}

		[ Test ]
		public async Task RandomizeEncounters()
		{
			await Randomizer.RandomizeEncounters();
		}

		[ Test ]
		public async Task RandomizeLearnsets()
		{
			await Randomizer.RandomizeLearnsets();
		}
	}
}