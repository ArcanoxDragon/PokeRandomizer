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
		private readonly string path;

		public ExeFS()
		{
			this.path = Path.Combine( TestContext.CurrentContext.TestDirectory, "Output", "ExeFS" );

			if ( !Directory.Exists( this.path ) )
				Directory.CreateDirectory( this.path );
		}

		[ Test ]
		public async Task RewriteCodeBin()
		{
			CodeBin origCodeBin = new CodeBin( Path.Combine( this.path, "code.orig.bin" ) )
			{
				Data = (byte[]) ORASConfig.GameConfig.CodeBin.Data.Clone()
			};

			await origCodeBin.Save();

			CodeBin newCodeBin = new CodeBin( Path.Combine( this.path, "code.bin" ) ) {
				Data = (byte[]) ORASConfig.GameConfig.CodeBin.Data.Clone()
			};

			TmsHms tmsHms = ORASConfig.GameConfig.GetTmsHms();
			newCodeBin.WriteStructure( tmsHms );

			await newCodeBin.Save();

			using ( var md5 = MD5.Create() )
			using ( var strOriginal = File.OpenRead( ORASConfig.GameConfig.CodeBin.Path ) )
			using ( var strNew = File.OpenRead( newCodeBin.Path ) )
			{
				byte[] hashOriginal = md5.ComputeHash( strOriginal );
				byte[] hashNew = md5.ComputeHash( strNew );

				Assert.AreEqual( hashOriginal, hashNew, "Hash for rewritten code.bin did not match original" );
			}
		}
	}
}