using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CtrDotNet.CTR.Garc;
using CtrDotNet.Pokemon.Garc;
using CtrDotNet.Pokemon.Reference;
using CtrDotNet.Pokemon.Structures.RomFS.Common;
using NUnit.Framework;

namespace CtrDotNet.Pokemon.Tests.ORAS.Rewrite
{
	[ TestFixture ]
	public class RomFS
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

		private async Task TestGarcStructure<T>( ReferencedGarc<T> garc, Func<Task> saveAction, bool failOnBadHash = true ) where T : BaseGarc
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
			var garcPersonal = await ORASConfig.GameConfig.GetGarcData( GarcNames.PokemonInfo );

			await this.TestGarcStructure( garcPersonal, async () => {
				var pokeInfo = await ORASConfig.GameConfig.GetPokemonInfo();
				byte[][] files = await garcPersonal.GetFiles();

				files[ garcPersonal.Garc.FileCount - 1 ] = pokeInfo.Write();

				await garcPersonal.SetFiles( files );
			} );
		}

		[ Test ]
		public async Task RewriteGameText()
		{
			var garcGameText = await ORASConfig.GameConfig.GetGarcData( GarcNames.GameText );
			var gameText = await ORASConfig.GameConfig.GetGameText();
			var outDir = Path.Combine( this.path, "GameText" );

			if ( !Directory.Exists( outDir ) )
				Directory.CreateDirectory( outDir );

			for ( int i = 0; i < gameText.Length; i++ )
			{
				var path = Path.Combine( outDir, $"TextFile-{i}.orig.txt" );
				File.WriteAllLines( path, gameText[ i ].Lines );
			}

			InconclusiveException ex = null;

			for ( int i = 0; i < gameText.Length; i++ )
			{
				try
				{
					TestContext.Progress.WriteLine( $"Testing text file #{i}" );
					int file = i;
					await this.TestGarcStructure( garcGameText, async () => {
						byte[] data = gameText[ file ].Write();

						TextFile test = new TextFile( ORASConfig.GameConfig.Version, ORASConfig.GameConfig.Variables );
						test.Read( data );
						var path = Path.Combine( this.path, "GameText", $"TextFile-{file}.txt" );
						Encoding testEncoding = Encoding.GetEncoding( "UTF-8", new EncoderReplacementFallback( "?" ), new DecoderExceptionFallback() );
						File.WriteAllLines( path, test.Lines, testEncoding );

						Assert.AreEqual( gameText[ file ].Lines, test.Lines, $"Strings did not match for file {file}" );

						await garcGameText.SetFile( file, data );
					}, false );
				}
				catch ( InconclusiveException e )
				{
					ex = e;
				}
			}

			if ( ex != null )
				throw ex;
		}
	}
}