using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.CTR;
using NUnit.Framework;

namespace CtrDotNet.Pokemon.Tests.XY
{
	[ TestFixture ]
	public class CodeBinTests
	{
		[ Test ]
		public async Task Decompress()
		{
			const string inPath  = @"D:\Users\Arcanox\Documents\3DS\Pokemon X\Unpacked\Vanilla\ExeFS\.code.bin";
			const string outPath = @"D:\Users\Arcanox\Documents\3DS\Pokemon X\Unpacked\Vanilla\ExeFS\code.decomp.bin";

			using ( var inStream = new FileStream( inPath, FileMode.Open, FileAccess.Read ) )
			using ( var tempStream = new MemoryStream() )
			using ( var outStream = new FileStream( outPath, FileMode.Create, FileAccess.Write ) )
			{
				await inStream.CopyToAsync( tempStream );

				var rData = tempStream.ToArray().Reverse().ToArray();

				using ( var temp2Stream = new MemoryStream( rData ) )
				{
					await Lzss.Decompress( temp2Stream, temp2Stream.Length, outStream );
				}
			}
		}
	}
}