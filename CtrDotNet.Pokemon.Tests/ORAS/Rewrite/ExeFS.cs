using System;
using System.IO;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.ExeFS;
using CtrDotNet.Pokemon.Structures.ExeFS.Common;
using NUnit.Framework;

namespace CtrDotNet.Pokemon.Tests.ORAS.Rewrite
{
	[ TestFixture ]
	public class ExeFS
	{
		public ExeFS()
		{
			string exeFsPath = Path.Combine( ORASConfig.OutputPath, "ExeFS" );

			if ( !Directory.Exists( exeFsPath ) )
				Directory.CreateDirectory( exeFsPath );
		}

		private async Task TestCodeBinStructure( Func<CodeBin, Task> saveAction )
		{
			var codeBin = await ORASConfig.GameConfig.GetCodeBin();
			var newFileName = Path.Combine( ORASConfig.OutputPath, "ExeFS", "code.bin" );
			var origFileName = Path.Combine( ORASConfig.OutputPath, "ExeFS", "code.orig.bin" );
			var origData = codeBin.Data;

			if ( File.Exists( origFileName ) )
				File.Delete( origFileName );

			await codeBin.SaveFileTo( ORASConfig.OutputPath );

			File.Move( newFileName,
					   origFileName );

			await saveAction( codeBin );

			await codeBin.SaveFileTo( ORASConfig.OutputPath );

			var newData = codeBin.Data;

			using ( var md5 = MD5.Create() )
			{
				byte[] hashOriginal = md5.ComputeHash( origData );
				byte[] hashNew = md5.ComputeHash( newData );

				Assert.AreEqual( hashOriginal, hashNew, "Hash for rewritten code.bin did not match original" );
			}
		}

		[ Test ]
		public async Task RewriteCodeBin()
		{
			await this.TestCodeBinStructure( async codeBin => {
				TmsHms tmsHms = await ORASConfig.GameConfig.GetTmsHms();
				ORASConfig.GameConfig.SaveTmsHms( tmsHms, codeBin );
			} );
		}
	}
}