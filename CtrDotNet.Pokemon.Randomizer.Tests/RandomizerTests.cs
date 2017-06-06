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
		public async Task LoadRandomizer()
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

			await Game.Initialize( romPath, Language.English );

			RandomizerConfig randConfig = new RandomizerConfig {
				Abilities = { },
				EggMoves = { },
				Encounters = {
					LevelMultiplier = 1.25m,
					TypeThemedAreas = true
				},
				Learnsets = {
					RandomizeLevels = true
				},
				Starters = {
					StartersOnly = false
				},
				Trainers = {
					FriendKeepsStarter = true,
					TypeThemed = true
				}
			};

			Randomizer.Initialize( Game, randConfig );

			Game.OutputPathOverride = this.romOutputDir;
		}

		[ Test, Order( 1 ) ]
		public async Task RandomizeAbilities()
		{
			await Randomizer.RandomizeAbilities();
		}

		[ Test, Order( 2 ) ]
		public async Task RandomizeEggMoves()
		{
			await Randomizer.RandomizeEggMoves();
		}

		[ Test, Order( 3 ) ]
		public async Task RandomizeEncounters()
		{
			await Randomizer.RandomizeEncounters();
		}

		[ Test, Order( 4 ) ]
		public async Task RandomizeLearnsets()
		{
			await Randomizer.RandomizeLearnsets();
		}

		[ Test, Order( 5 ) ]
		public async Task RandomizeStarters()
		{
			await Randomizer.RandomizeStarters();
		}

		[ Test, Order( 6 ) ]
		public async Task RandomizeTrainers()
		{
			await Randomizer.RandomizeTrainers();
		}
	}
}