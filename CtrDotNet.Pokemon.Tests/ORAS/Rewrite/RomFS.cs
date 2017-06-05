using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CtrDotNet.CTR;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Garc;
using CtrDotNet.Pokemon.Reference;
using CtrDotNet.Pokemon.Structures.RomFS.Gen6;
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

				TestContext.Out.WriteLine( $"Old hash: {string.Join( "", hashOriginal.Select( d => d.ToString( "X2" ) ) )}" );
				TestContext.Out.WriteLine( $"New hash: {string.Join( "", hashNew.Select( d => d.ToString( "X2" ) ) )}" );

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
			byte[][] files = await garcEncounters.GetFiles();
			byte[][] origFiles = new byte[ files.Length ][];

			for ( int i = 0; i < files.Length; i++ )
				origFiles[ i ] = (byte[]) files[ i ].Clone();

			await this.TestGarcStructure( garcEncounters, async () => {
				var encounters = ( await ORASConfig.GameConfig.GetEncounterData() ).ToArray();

				for ( int i = 0; i < encounters.Length; i++ )
				{
					if ( !encounters[ i ].HasEntries )
						continue;

					byte[] encounterBuffer = encounters[ i ].Write();

					if ( true /* IsORAS */ )
					{
						// Last file is dexNavData
						const int Offset = 0xE;
						byte[] dexNavData = files[ files.Length - 1 ];
						int entryPointer = BitConverter.ToInt32( dexNavData, ( i + 1 ) * sizeof( int ) ) + Offset;

						int dataPointer = BitConverter.ToInt32( encounterBuffer, EncounterWild.DataOffset );
						dataPointer += encounters[ i ].DataStart;
						int dataLength = encounters[ i ].GetAllEntries().Count() * EncounterWild.Entry.Size;

						Array.Copy( encounterBuffer, dataPointer, dexNavData, entryPointer, dataLength );
					}

					files[ i ] = encounterBuffer;

					Assert.AreEqual( origFiles[ i ], files[ i ], $"Data for encounter zone {i} did not match" );
				}

				Assert.AreEqual( origFiles[ files.Length - 1 ], files[ files.Length - 1 ], "DecStorage data did not match" );

				await garcEncounters.SetFiles( files );
			} );
		}
	}
}