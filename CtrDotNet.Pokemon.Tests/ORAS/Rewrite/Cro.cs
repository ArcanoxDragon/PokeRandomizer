using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Cro;
using CtrDotNet.Pokemon.Reference;
using CtrDotNet.Pokemon.Structures.CRO.Gen6.Starters;
using NUnit.Framework;

namespace CtrDotNet.Pokemon.Tests.ORAS.Rewrite
{
	[ TestFixture ]
	public class Cro
	{
		private readonly string path;

		public Cro()
		{
			this.path = Path.Combine( TestContext.CurrentContext.TestDirectory, "Output", "RomFS" );

			if ( !Directory.Exists( this.path ) )
				Directory.CreateDirectory( this.path );
		}

		private async Task TestCroFile( CroFile cro, Func<Task> saveAction, bool failOnBadHash = true )
		{
			var fname = Path.GetFileName( cro.Path );
			var outPath = Path.Combine( this.path, fname );
			var origPath = Path.Combine( this.path, $"{Path.GetFileNameWithoutExtension( outPath )}.orig.cro" );
			byte[] origData = await cro.Write();

			await saveAction();

			byte[] newData = await cro.Write();

			using ( var md5 = MD5.Create() )
			using ( var origFs = new FileStream( origPath, FileMode.Create, FileAccess.Write, FileShare.None ) )
			using ( var newFs = new FileStream( outPath, FileMode.Create, FileAccess.Write, FileShare.None ) )
			{
				await origFs.WriteAsync( origData, 0, origData.Length );
				await newFs.WriteAsync( newData, 0, newData.Length );

				byte[] hashOriginal = md5.ComputeHash( origData );
				byte[] hashNew = md5.ComputeHash( newData );

				try
				{
					Assert.AreEqual( hashOriginal, hashNew, $"Hash for rewritten {fname} did not match original" );
				}
				catch ( AssertionException ex )
				{
					if ( failOnBadHash )
						throw;

					throw new InconclusiveException( ex.Message, ex );
				}
			}
		}

		[ Test ]
		public async Task RewriteStarters()
		{
			CroFile dllField = await ORASConfig.GameConfig.GetCroFile( CroNames.Field );
			CroFile dllPoke3Select = await ORASConfig.GameConfig.GetCroFile( CroNames.Poke3Select );

			await this.TestCroFile( dllField, () => this.TestCroFile( dllPoke3Select, async () => {
				Starters starters = new Starters( ORASConfig.GameConfig.Version );

				await starters.Read( dllField, dllPoke3Select );
				await starters.Write( dllField, dllPoke3Select );
			} ) );
		}
	}
}