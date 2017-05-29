using System;
using System.IO;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Game;
using NUnit.Framework;

namespace CtrDotNet.Pokemon.Tests.ORAS
{
	[ SetUpFixture ]
	public class ORASConfig
	{
		public static GameConfig GameConfig { get; private set; }

		[ OneTimeSetUp ]
		public async Task SetUp()
		{
			string romPath = Path.GetFullPath( Properties.Settings.Default.RomPathORAS );
			string romFsPath = Path.Combine( romPath, "RomFS" );
			string exeFsPath = Path.Combine( romPath, "ExeFS" );

			Assert.True( Directory.Exists( romPath ), "ROM path does not exist" );
			Assert.True( Directory.Exists( romFsPath ), "ROM path does not contain a RomFS folder" );
			Assert.True( Directory.Exists( exeFsPath ), "ROM path does not contain an ExeFS folder" );

			GameConfig = new GameConfig( GameVersion.ORAS );
			await GameConfig.Initialize( romFsPath, exeFsPath, Language.English );
		}
	}
}