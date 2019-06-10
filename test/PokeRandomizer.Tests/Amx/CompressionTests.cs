using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using pkNX.Structures;
using PokeRandomizer.Common;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Reference;
using PokeRandomizer.Config;
using GameVersion = PokeRandomizer.Common.Game.GameVersion;

namespace PokeRandomizer.Tests.Amx
{
	[ TestFixture ]
	public class CompressionTests
	{
		public static IRandomizer Randomizer { get; set; }
		public static GameConfig  Game       { get; set; }

		[ OneTimeSetUp ]
		public async Task LoadRandomizer()
		{
			Game = new GameConfig( GameVersion.XY );

			string romPath   = Path.GetFullPath( Settings.RomPathXy );
			string romFsPath = Path.Combine( romPath, "RomFS" );
			string exeFsPath = Path.Combine( romPath, "ExeFS" );

			Assert.True( Directory.Exists( romPath ),   "ROM path does not exist" );
			Assert.True( Directory.Exists( romFsPath ), "ROM path does not contain a RomFS folder" );
			Assert.True( Directory.Exists( exeFsPath ), "ROM path does not contain an ExeFS folder" );

			await Game.Initialize( romPath, Language.English );

			RandomizerConfig randConfig = new RandomizerConfig {
				EggMoves = { },
				Encounters = {
					LevelMultiplier = 1.25m,
					TypeThemedAreas = true
				},
				Learnsets = {
					AtLeast4Moves   = true,
					RandomizeLevels = true
				},
				OverworldItems = { },
				PokemonInfo    = { },
				Starters = {
					StartersOnly = false
				},
				Trainers = {
					FriendKeepsStarter = true,
					LevelMultiplier    = 1.3m,
					TypeThemed         = true
				}
			};

			Randomizer = PokeRandomizer.Common.Randomizer.GetRandomizer( Game, randConfig );
		}

		[ Test ]
		public void TestAlgorithm()
		{
			//var bytes  = new byte[] { 0x8E, 0xA5, 0xA0, 0x81, 0x45 };
			//var bytes = new byte[] { 0xF4, 0x40 };
			var bytes  = new byte[] { 0x8F, 0xA2, 0xA0, 0x81, 0x45 };
			var value  = PawnUtil.QuickDecompress2( bytes, 1 )[ 0 ];
			var bytes2 = PawnUtil.CompressScript( new[] { value } );

			TestContext.Progress.WriteLine( string.Join( " ", bytes.Select( b => $"{b:X2}" ) ) );
			TestContext.Progress.WriteLine( value.ToString( "X8" ) );
			TestContext.Progress.WriteLine( string.Join( " ", bytes2.Select( b => $"{b:X2}" ) ) );

			Assert.AreEqual( bytes, bytes2 );
		}

		public static IEnumerable<int> RecompressionFiles = Enum.GetValues( typeof( AmxNames ) ).Cast<int>();

		[ TestCaseSource( nameof(RecompressionFiles) ) ]
		public async Task TestRecompression( int file )
		{
			var name    = (AmxNames) file;
			var amxGarc = await Game.GetGarc( GarcNames.AmxFiles );

			if ( file >= amxGarc.Garc.FileCount )
				Assert.Inconclusive( $"Nonexistent file: {file}" );

			if ( !Directory.Exists( "Recompression" ) )
				Directory.CreateDirectory( "Recompression" );

			var amxFile = await amxGarc.GetFile( file );
			var amx     = new pkNX.Structures.Amx( amxFile );

			var origData     = amx.Data;
			var decompressed = amx.DecompressedInstructions;
			var recompressed = PawnUtil.CompressScript( decompressed );
			var newData = amx.Data
							 .Take( amx.Header.COD )
							 .Concat( recompressed )
							 .ToArray();

			await File.WriteAllBytesAsync( $"Recompression\\{name}_old.bin", origData );
			await File.WriteAllBytesAsync( $"Recompression\\{name}_new.bin", newData );

			Assert.AreEqual( origData, newData, $"File {file} failed" );
		}
	}
}