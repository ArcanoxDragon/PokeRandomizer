using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Config;
using PokeRandomizer.Gen6;

namespace PokeRandomizer.Tests.RandomizerTests
{
	[ TestFixture ]
	public class Gen6RandomizerTests
	{
		private string romOutputDir;

		public static Gen6Randomizer Randomizer { get; set; }
		public static GameConfig     Game       { get; set; }

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

			Game = new GameConfig( GameVersion.ORAS );

			string romPath   = Path.GetFullPath( Settings.RomPathOras );
			string romFsPath = Path.Combine( romPath, "RomFS" );
			string exeFsPath = Path.Combine( romPath, "ExeFS" );

			Assert.True( Directory.Exists( romPath ),   "ROM path does not exist" );
			Assert.True( Directory.Exists( romFsPath ), "ROM path does not contain a RomFS folder" );
			Assert.True( Directory.Exists( exeFsPath ), "ROM path does not contain an ExeFS folder" );

			await Game.Initialize( romPath, Language.English );

			RandomizerConfig randConfig = new RandomizerConfig {
				EggMoves = {
					RandomizeEggMoves = true,
					FavorSameType     = true,
				},
				Encounters = {
					RandomizeEncounters = true,
					LevelMultiplier     = 1.25m,
					TypeThemedAreas     = true,
					AllowLegendaries    = true,
				},
				Learnsets = {
					RandomizeLearnsets = true,
					AtLeast4Moves      = true,
					RandomizeLevels    = true,
					FavorSameType      = true,
				},
				OverworldItems = {
					RandomizeOverworldItems = true,
					AllowMegaStones         = true,
					AllowTMs                = true,
					AllowMasterBalls        = true,
				},
				PokemonInfo = {
					RandomizeTypes          = true,
					RandomizePrimaryTypes   = true,
					RandomizeSecondaryTypes = true,
					RandomizeAbilities      = true,
				},
				Starters = {
					RandomizeStarters = true,
					StartersOnly      = false,
					AllowLegendaries  = true,
				},
				Trainers = {
					RandomizeTrainers  = true,
					FriendKeepsStarter = true,
					LevelMultiplier    = 1.3m,
					TypeThemed         = true,
					TypeThemedGyms     = true,
				}
			};

			Randomizer              = PokeRandomizer.Common.Randomizer.GetRandomizer( Game, randConfig ) as Gen6Randomizer;
			Game.OutputPathOverride = this.romOutputDir;

			Assert.NotNull( Randomizer );
		}

		[ Test, Order( 1 ) ]
		public async Task RandomizePokemonInfo()
		{
			await Randomizer.RandomizePokemonInfo( null, CancellationToken.None );
		}

		[ Test, Order( 2 ) ]
		public async Task RandomizeEggMoves()
		{
			await Randomizer.RandomizeEggMoves( null, CancellationToken.None );
		}

		[ Test, Order( 3 ) ]
		public async Task RandomizeEncounters()
		{
			await Randomizer.RandomizeEncounters( null, CancellationToken.None );
		}

		[ Test, Order( 4 ) ]
		public async Task RandomizeLearnsets()
		{
			await Randomizer.RandomizeLearnsets( null, CancellationToken.None );
		}

		[ Test, Order( 5 ) ]
		public async Task RandomizeOverworldItems()
		{
			await Randomizer.RandomizeOverworldItems( null, CancellationToken.None );
		}

		[ Test, Order( 6 ) ]
		public async Task RandomizeStarters()
		{
			await Randomizer.RandomizeStarters( null, CancellationToken.None );
		}

		[ Test, Order( 7 ) ]
		public async Task RandomizeTrainers()
		{
			await Randomizer.RandomizeTrainers( null, CancellationToken.None );
		}
	}
}