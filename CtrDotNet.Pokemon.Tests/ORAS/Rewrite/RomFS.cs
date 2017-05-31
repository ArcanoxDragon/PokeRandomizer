using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CtrDotNet.CTR;
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
			this.path = Path.Combine( TestContext.CurrentContext.TestDirectory, "Output", "RomFS" );

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
			byte[] origData = await garc.Garc.GarcData.Save();

			await saveAction();

			byte[] newData = await garc.Garc.GarcData.Save();

			using ( var md5 = MD5.Create() )
			using ( var origFs = new FileStream( Path.Combine( outDir, $"{fname}.orig" ), FileMode.Create, FileAccess.Write, FileShare.None ) )
			using ( var newFs = new FileStream( outPath, FileMode.Create, FileAccess.Write, FileShare.None ) )
			{
				await origFs.WriteAsync( origData, 0, origData.Length );
				await newFs.WriteAsync( newData, 0, newData.Length );

				byte[] hashOriginal = md5.ComputeHash( origData );
				byte[] hashNew = md5.ComputeHash( newData );

				try
				{
					Assert.AreEqual( hashOriginal, hashNew, $"Hash for rewritten {garc.Reference.RomFsPath} did not match original" );
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
			var garcPersonal = await ORASConfig.GameConfig.GetGarc( GarcNames.PokemonInfo );

			await this.TestGarcStructure( garcPersonal, async () => {
				var pokeInfo = await ORASConfig.GameConfig.GetPokemonInfo();

				await garcPersonal.SetFile( garcPersonal.Garc.FileCount - 1, pokeInfo.Write() );
			} );
		}

		[ Test ]
		public async Task RewriteLearnsets()
		{
			var garcLearnsets = await ORASConfig.GameConfig.GetGarc( GarcNames.Learnsets );

			await this.TestGarcStructure( garcLearnsets, async () => {
				var learnsets = await ORASConfig.GameConfig.GetLearnsets();
				byte[][] files = learnsets.Select( l => l.Write() ).ToArray();

				await garcLearnsets.SetFiles( files );
			} );
		}

		[ Test ]
		public async Task RewriteMoves()
		{
			var garcMoves = await ORASConfig.GameConfig.GetGarc( GarcNames.Moves );

			await this.TestGarcStructure( garcMoves, async () => {
				var moves = await ORASConfig.GameConfig.GetMoves();

				byte[][] orig = Mini.UnpackMini( await garcMoves.GetFile( 0 ), "WD" );
				byte[][] files = moves.Select( l => l.Write() ).ToArray();

				for ( int i = 0; i < files.Length; i++ )
				{
					string moveName = Moves.GetValueFrom( i )?.Name ?? "NULL";
					Assert.AreEqual( orig[ i ], files[ i ], $"Rewritten data for move {i} ({moveName}) did not match" );
				}

				await garcMoves.SetFile( 0, Mini.PackMini( files, "WD" ) );
			} );
		}

		[ Test ]
		public async Task RewriteEncounters()
		{
			var garcEncounters = await ORASConfig.GameConfig.GetGarc( GarcNames.EncounterData, useLz: true );

			await this.TestGarcStructure( garcEncounters, async () => {
				var encounters = ( await ORASConfig.GameConfig.GetEncounterData() ).ToArray();
				byte[][] files = await garcEncounters.GetFiles();

				for ( int i = 0; i < encounters.Length; i++ )
				{
					byte[] encounterBuffer = encounters[ i ].Write();
					// Last file is decStorage
					const int offset = 0xE;
					byte[] decStorageData = files[ files.Length - 1 ];
					int entryPointer = BitConverter.ToInt32( decStorageData, ( i + 1 ) * sizeof( int ) ) + offset;

					Array.Copy( encounterBuffer, 0, decStorageData, entryPointer, encounterBuffer.Length - offset );

					files[ i ] = encounterBuffer;
				}

				await garcEncounters.SetFiles( files );
			} );
		}
	}
}