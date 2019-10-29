using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using pkNX.Structures;
using PokeRandomizer.Common;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Reference;
using PokeRandomizer.Config;
using PokeRandomizer.Utility;
using GameVersion = PokeRandomizer.Common.Game.GameVersion;

namespace PokeRandomizer.Tests.XY
{
	[ TestFixture ]
	public class ItemRandomizerTests
	{
		public static IRandomizer Randomizer { get; set; }
		public static GameConfig  Game       { get; set; }

		private string romOutputDir;
		private Random masterRandom;

		public void SetUpOutputDirectory()
		{
			this.romOutputDir = Path.Combine( TestContext.CurrentContext.TestDirectory, "ItemRandomize" );

			if ( !Directory.Exists( this.romOutputDir ) )
				Directory.CreateDirectory( this.romOutputDir );
		}

		[ OneTimeSetUp ]
		public async Task LoadRandomizer()
		{
			this.SetUpOutputDirectory();

			Game = new GameConfig( GameVersion.XY );

			string romPath   = Path.GetFullPath( Settings.RomPathXy );
			string romFsPath = Path.Combine( romPath, "RomFS" );
			string exeFsPath = Path.Combine( romPath, "ExeFS" );

			Assert.True( Directory.Exists( romPath ), "ROM path does not exist" );
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

			Randomizer              = PokeRandomizer.Common.Randomizer.GetRandomizer( Game, randConfig );
			Game.OutputPathOverride = this.romOutputDir;

			Assert.NotNull( Randomizer );

			this.masterRandom = new Random( Randomizer.RandomSeed );
		}

		private Random GetNewTaskRandom() => new Random( this.masterRandom.Next() );

		[ Test ]
		public async Task TestItemRandomizer()
		{
			var random          = this.GetNewTaskRandom();
			var itemNames       = await Game.GetTextFile( TextNames.ItemNames );
			var amxGarc         = await Game.GetGarc( GarcNames.AmxFiles, true );
			var fldItemFile     = await amxGarc.GetFile( (int) AmxNames.FldItem );
			var fldItemAmx      = new pkNX.Structures.Amx( fldItemFile );
			var itemDataPayload = fldItemAmx.DataPayload.Skip( 9 ).ToArray();

			Assert.AreEqual( 0, itemDataPayload.Length % 3, "Item data payload length incorrect" );

			var legalItems = Legal.Pouch_Items_XY
								  .Concat( Legal.Pouch_Medicine_XY )
								  .Concat( Legal.Pouch_TMHM_XY )
								  .Concat( Legal.Pouch_Berry_XY )
								  .ToList();

			for ( var i = 0; i < 206; i++ )
			{
				var itemId      = itemDataPayload[ i * 3 ];
				var quantity    = itemDataPayload[ i * 3 + 1 ];
				var scriptId    = itemDataPayload[ i * 3 + 2 ];
				var itemName    = itemNames.Lines[ (int) itemId ];
				var newItemId   = legalItems.GetRandom( random );
				var newItemName = itemNames.Lines[ newItemId ];

				TestContext.Progress.WriteLine( $"Script {scriptId:X4} has item {itemName} x{quantity}, changing to {newItemName}" );

				itemDataPayload[ i * 3 ] = newItemId;
			}

			var dataStart = fldItemAmx.CodeLength / sizeof( uint );
			var instructions = fldItemAmx.DecompressedInstructions
										 // Original code
										 .Take( dataStart )
										 // Original data header
										 .Concat( fldItemAmx.DataPayload.Take( 9 ) )
										 // Our own data payload
										 .Concat( itemDataPayload )
										 .ToArray();

			var newCompressedBytes = PawnUtil.CompressScript( instructions );
			var newData            = fldItemAmx.Data.Take( fldItemAmx.Header.COD ).Concat( newCompressedBytes ).ToArray();

			// Write new size
			BitConverter.GetBytes( newData.Length ).CopyTo( newData, 0 );

			File.WriteAllBytes( "old.bin", fldItemAmx.Data );
			File.WriteAllBytes( "new.bin", newData );

			await amxGarc.SetFile( (int) AmxNames.FldItem, newData );
			await Game.SaveFile( amxGarc );
		}
	}
}