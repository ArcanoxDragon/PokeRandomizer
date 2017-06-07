using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Garc;
using CtrDotNet.Pokemon.Reference;
using NUnit.Framework;

namespace CtrDotNet.Pokemon.Tests.ORAS.Rewrite
{
	[ TestFixture ]
	public partial class RomFS
	{
		private readonly string path;

		public RomFS()
		{
			this.path = Path.Combine( ORASConfig.OutputPath, "RomFS" );

			if ( !Directory.Exists( this.path ) )
				Directory.CreateDirectory( this.path );
		}

		#region Helpers

		private (string, string) GetAndCreateGarcPath( ReferencedGarc garc )
		{
			var outPath = Path.Combine( this.path, garc.Reference.RomFsPath );
			var outDir = Path.GetDirectoryName( outPath );

			if ( !Directory.Exists( outDir ) )
				Directory.CreateDirectory( outDir );

			return (outPath, outDir);
		}

		private async Task TestGarcStructure( ReferencedGarc garc, Func<Task> saveAction, bool failOnBadHash = true )
		{
			var (outPath, outDir) = this.GetAndCreateGarcPath( garc );
			var fname = Path.GetFileName( outPath );
			await garc.Garc.GarcData.SaveFile();
			byte[] origData = (byte[]) ( await garc.Write() ).Clone();

			await saveAction();

			await garc.Garc.GarcData.SaveFile();
			byte[] newData = await garc.Write();

			using ( var md5 = MD5.Create() )
			using ( var origFs = new FileStream( Path.Combine( outDir, $"{fname}.orig" ), FileMode.Create, FileAccess.Write, FileShare.None ) )
			using ( var newFs = new FileStream( outPath, FileMode.Create, FileAccess.Write, FileShare.None ) )
			{
				await origFs.WriteAsync( origData, 0, origData.Length );
				await newFs.WriteAsync( newData, 0, newData.Length );

				byte[] hashOriginal = md5.ComputeHash( origData );
				byte[] hashNew = md5.ComputeHash( newData );

				TestContext.Out.WriteLine( $"GARC {garc.Reference.RomFsPath} ({garc.Reference.Name}):" );
				TestContext.Out.WriteLine( $"\tOld hash: {string.Join( "", hashOriginal.Select( d => d.ToString( "X2" ) ) )}" );
				TestContext.Out.WriteLine( $"\tNew hash: {string.Join( "", hashNew.Select( d => d.ToString( "X2" ) ) )}" );

				try
				{
					Assert.AreEqual( origData, newData, $"Data for rewritten {garc.Reference.RomFsPath} did not match original" );
				}
				catch ( AssertionException ex )
				{
					if ( failOnBadHash )
						throw;

					throw new InconclusiveException( ex.Message, ex );
				}
			}
		}

		#endregion

		[ Test ]
		public async Task RewritePokemonInfo()
		{
			var garcPokeInfo = await ORASConfig.GameConfig.GetGarc( GarcNames.PokemonInfo );

			await this.TestGarcStructure( garcPokeInfo, async () => {
				var pokeInfoTable = await ORASConfig.GameConfig.GetPokemonInfo();

				await ORASConfig.GameConfig.SavePokemonInfo( pokeInfoTable, garcPokeInfo );
			} );
		}

		[ Test ]
		public async Task RewriteLearnsets()
		{
			var garcLearnsets = await ORASConfig.GameConfig.GetGarc( GarcNames.Learnsets );

			await this.TestGarcStructure( garcLearnsets, async () => {
				var learnsets = await ORASConfig.GameConfig.GetLearnsets();

				await ORASConfig.GameConfig.SaveLearnsets( learnsets, garcLearnsets );
			} );
		}

		[ Test ]
		public async Task RewriteMoves()
		{
			var garcMoves = await ORASConfig.GameConfig.GetGarc( GarcNames.Moves );

			await this.TestGarcStructure( garcMoves, async () => {
				var moves = await ORASConfig.GameConfig.GetMoves();

				await ORASConfig.GameConfig.SaveMoves( moves, garcMoves );
			} );
		}

		[ Test ]
		public async Task RewriteEncounters()
		{
			var garcEncounters = await ORASConfig.GameConfig.GetGarc( GarcNames.EncounterData, useLz: true );

			await this.TestGarcStructure( garcEncounters, async () => {
				var encounters = await ORASConfig.GameConfig.GetEncounterData();

				await ORASConfig.GameConfig.SaveEncounterData( encounters, garcEncounters );
			} );
		}

		[ Test ]
		public async Task RewriteEggMoves()
		{
			var garcEggMoves = await ORASConfig.GameConfig.GetGarc( GarcNames.EggMoves );

			await this.TestGarcStructure( garcEggMoves, async () => {
				var eggMoves = await ORASConfig.GameConfig.GetEggMoves();

				byte[][] files = await garcEggMoves.GetFiles();
				for ( int i = 0; i < files.Length; i++ )
				{
					Assert.AreEqual( files[ i ], eggMoves[ i ].Write(), $"Data for egg move for species #{i} did not match" );
				}

				await ORASConfig.GameConfig.SaveEggMoves( eggMoves, garcEggMoves );
			} );
		}

		[ Test ]
		public async Task RewriteTrainers()
		{
			var garcTrainerData = await ORASConfig.GameConfig.GetGarc( GarcNames.TrainerData );
			var garcTrainerPoke = await ORASConfig.GameConfig.GetGarc( GarcNames.TrainerPokemon );

			await this.TestGarcStructure( garcTrainerData, () => this.TestGarcStructure( garcTrainerPoke, async () => {
				var trainers = ( await ORASConfig.GameConfig.GetTrainerData() ).ToList();
				byte[][] trainerFiles = await garcTrainerData.GetFiles();
				byte[][] pokemonFiles = await garcTrainerPoke.GetFiles();

				Assert.AreEqual( trainerFiles.Length, pokemonFiles.Length, "Trainer data and trainer team files do not have equal lengths" );

				for ( int i = 0; i < trainers.Count; i++ )
				{
					Assert.AreEqual( trainerFiles[ i ], trainers[ i ].Write(), $"Trainer data file for trainer #{i} did not match" );
					Assert.AreEqual( pokemonFiles[ i ], trainers[ i ].WriteTeam(), $"Trainer team file for trainer #{i} did not match" );
				}

				await ORASConfig.GameConfig.SaveTrainerData( trainers, garcTrainerData, garcTrainerPoke );
			} ) );
		}
	}
}