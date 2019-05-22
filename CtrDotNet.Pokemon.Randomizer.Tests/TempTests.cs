using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Randomization.Config;
using CtrDotNet.Pokemon.Randomization.Progress;
using CtrDotNet.Pokemon.Reference;
using CtrDotNet.Utility.Extensions;
using Newtonsoft.Json;
using NUnit.Framework;

namespace CtrDotNet.Pokemon.Randomizer.Tests
{
	[ TestFixture ]
	public class TempTests
	{
		private const string GameDirectory   = @"D:\Users\Arcanox\Documents\3DS\Pokemon X\Unpacked\Vanilla";
		private const string OutputDirectory = @"D:\Users\Arcanox\Documents\3DS\Pokemon X\Luma Patches\TestAll";

		protected async Task<(GameConfig, RandomizerConfig)> GetGameAndConfigAsync()
		{
			if ( !Directory.Exists( OutputDirectory ) )
				Directory.CreateDirectory( OutputDirectory );

			var configJson = File.ReadAllText( @"D:\Users\Arcanox\Documents\3DS\Pokemon X\Random Preferred.json" );
			Assert.That( configJson, Is.Not.Null.And.Not.Empty );

			var config = JsonConvert.DeserializeObject<RandomizerConfig>( configJson );
			Assert.That( config, Is.Not.Null );

			var garcPath  = Path.Combine( GameDirectory, "RomFS", "a" );
			var garcCount = Directory.GetFiles( garcPath, "*", SearchOption.AllDirectories ).Length;
			var game      = new GameConfig( garcCount ) { OutputPathOverride = OutputDirectory };

			await game.Initialize( GameDirectory, Language.English );

			return ( game, config );
		}

		[ Test ]
		public async Task TestRandomizer()
		{
			var (game, config) = await this.GetGameAndConfigAsync();
			var randomizer = Randomization.Common.Randomizer.GetRandomizer( game, config );
			var progress   = new ProgressNotifier();

			await randomizer.RandomizeAll( progress, CancellationToken.None );
		}

		public async Task<IList<string>> GetOriginalNullEncounters()
		{
			var (game, _) = await this.GetGameAndConfigAsync();
			var encounters = await game.GetEncounterData( false );
			var whitelist  = new List<string>();

			foreach ( var (iEnc, encounter) in encounters.Pairs() )
			{
				foreach ( var (iEnt, entry) in encounter.GetAllEntries().Pairs() )
				{
					if ( entry == null || entry.Species == 0 )
						whitelist.Add( $"{iEnc}:{iEnt}" );
				}
			}

			return whitelist;
		}

		[ Test ]
		public async Task TestEditedEncounters()
		{
			var (game, config) = await this.GetGameAndConfigAsync();
			var whitelist  = await this.GetOriginalNullEncounters();
			var randomizer = Randomization.Common.Randomizer.GetRandomizer( game, config, 407643609 );
			var progress = new ProgressNotifier();

			//await randomizer.RandomizeEncounters( progress, CancellationToken.None );

			var encounters = await game.GetEncounterData( false );
			var zoneNames = ( await game.GetTextFile( TextNames.EncounterZoneNames ) ).Lines;

			foreach ( var (iEnc, encounter) in encounters.Pairs() )
			{
				var name = zoneNames[ encounter.ZoneId ];
				
				foreach ( var (iEnt, entry) in encounter.GetAllEntries().Pairs() )
				{
					var key = $"{iEnc}:{iEnt}";

					if ( whitelist.Contains( key ) )
						continue;

					Assert.That( entry,         Is.Not.Null, $"Encounter entry {key} was null" );
					Assert.That( entry.Species, Is.Not.Zero, $"Encounter entry {key} had a species of \"0\"" );
				}
			}
		}
	}
}