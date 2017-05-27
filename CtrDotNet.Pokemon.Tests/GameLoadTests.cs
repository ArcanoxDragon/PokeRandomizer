using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Dynamic;
using CtrDotNet.Pokemon.GameData;
using CtrDotNet.Pokemon.Structures.ExeFS.Common;
using CtrDotNet.Pokemon.Structures.RomFS.PokemonInfo;
using NUnit.Framework;

namespace CtrDotNet.Pokemon.Tests
{
	[ TestFixture ]
	public class GameLoadTests
	{
		private GameConfig config;

		[ OneTimeSetUp ]
		public async Task SetUp()
		{
			Assert.True( Enum.TryParse<GameVersion>( Properties.Settings.Default.GameType, out var gameVersion ), "Could not parse game version in settings" );

			string romPath = Path.GetFullPath( Properties.Settings.Default.RomPath );
			string romFsPath = Path.Combine( romPath, "RomFS" );
			string exeFsPath = Path.Combine( romPath, "ExeFS" );

			Assert.True( Directory.Exists( romPath ), "ROM path does not exist" );
			Assert.True( Directory.Exists( romFsPath ), "ROM path does not contain a RomFS folder" );
			Assert.True( Directory.Exists( exeFsPath ), "ROM path does not contain an ExeFS folder" );

			this.config = new GameConfig( gameVersion );
			await this.config.Initialize( romFsPath, exeFsPath, Language.English );
		}

		[ Test ]
		public async Task TmHm()
		{
			TmsHms tmsHms = await this.config.LoadTmsHms();
			ushort dragonClawId = tmsHms.TmIds[ 1 ]; // TM02 - Dragon Claw (array is 0-indexed)

			Assert.AreEqual( Moves.DragonClaw, (Moves) dragonClawId, $"TM02 should be Dragon Claw but it is {( (Moves) dragonClawId ).GetDisplayName( this.config )}" );
		}

		[ Test ]
		public async Task Bulbasaur()
		{
			PokemonInfoTable pokeInfo = await this.config.LoadPokemonInfo();
			TmsHms tmsHms = await this.config.LoadTmsHms();
			PokemonInfo nullInfo = pokeInfo[ 0 ];
			PokemonInfo bulbasaurInfo = pokeInfo[ Species.Bulbasaur ];

			Assert.AreNotEqual( nullInfo, bulbasaurInfo, "Species info for Bulbasaur is the same as Null/Egg" );

			Assert.AreEqual( 6.9, bulbasaurInfo.WeightKg, "Species weight for Bulbasaur doesn't match the Pokedex!" );
			Assert.AreEqual( 0.7, bulbasaurInfo.HeightM, "Species height for Bulbasaur doesn't match the Pokedex!" );

			TestContext.Out.WriteLine( "Bulbasaur can learn the following TMs:" );

			foreach ( var (_, tm) in bulbasaurInfo.TMHM
												  .Select( ( b, i ) => (b, i) )
												  .Where( t => t.Item1 ) )
			{
				string type = tm >= tmsHms.TmIds.Length ? "HM" : "TM";
				int num = ( type == "HM" ) ? ( tm - 100 + 1 ) : ( tm + 1 );
				TestContext.Out.WriteLine( $"\t{type} {num}: {tmsHms.GetMove( tm ).GetDisplayName( this.config )}" );
			}
		}
	}
}