using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Reference;
using CtrDotNet.Pokemon.Structures.RomFS.Common;
using CtrDotNet.Pokemon.Utility;
using NUnit.Framework;

namespace CtrDotNet.Pokemon.Tests.ORAS
{
	[ TestFixture ]
	public class TextFileTests
	{
		[ Test ]
		public async Task TestEachLine()
		{
			var garcGameText = await ORASConfig.GameConfig.GetGarcData( GarcNames.GameText );
			byte[] tf0Data = await garcGameText.GetFile( 0 );
			TextFile tf0 = await ORASConfig.GameConfig.GetGameText( 0 );
			byte[] textData = tf0Data.Skip( 0x10 /* SectionDataOffset */ ).ToArray();
			ushort key = TextFileHelper.KeyBase;

			for ( int i = 0; i < tf0.Lines.Count; i++ )
			{
				TestContext.Progress.WriteLine( $"Testing line {i}..." );
				TextFile.LineInfo lineInfo = tf0.LineInfos[ i ];
				byte[] lineData = textData.Skip( (int) lineInfo.Offset ).Take( lineInfo.Length * 2 ).ToArray();

				if ( i < tf0.Lines.Count - 1 )
				{
					TextFile.LineInfo next = tf0.LineInfos[ i + 1 ];
					long padding = next.Offset - ( lineInfo.Offset + lineInfo.Length * 2 );
					TestContext.Progress.WriteLine( $"               Length: {lineInfo.Length * 2}" );
					TestContext.Progress.WriteLine( $"              Padding: {padding}" );
					TestContext.Progress.WriteLine( $"                 Flag: {lineInfo.Flag}" );
					TestContext.Progress.WriteLine( $"    Length w/ padding: {lineInfo.Length * 2 + padding}" );
					TestContext.Progress.WriteLine( $"         Char Len % 2: {lineInfo.Length % 2}" );
					TestContext.Progress.WriteLine( $"       Len w/ pad % 2: {( lineInfo.Length + ( padding / 2 ) ) % 2}" );
				}

				string tfhString = TextFileHelper.GetLineString( tf0, TextFileHelper.CryptLineData( lineData, key ) );

				Assert.AreEqual( tf0.Lines[ i ], tfhString, "Directly decoded string not equal" );

				var newData = TextFileHelper.GetLineData( tf0, tf0.Lines[ i ] );
				byte[] tfhData = TextFileHelper.CryptLineData( newData, key );

				if ( lineData.Length == tfhData.Length + 2
					 && lineData[ lineData.Length - 1 ] == 0
					 && lineData[ lineData.Length - 2 ] == 0 )
					tfhData = tfhData.Concat( new byte[] { 0, 0 } ).ToArray(); // add padding

				Assert.AreEqual( lineData, tfhData, "Directly encoded string not equal" );

				key += TextFileHelper.KeyAdvance;
			}
		}

		[ Test ]
		public async Task TestAllLines()
		{
			var garcGameText = await ORASConfig.GameConfig.GetGarcData( GarcNames.GameText );
			byte[] tf0Data = await garcGameText.GetFile( 0 );
			TextFile tf0 = await ORASConfig.GameConfig.GetGameText( 0 );
			byte[][] encData = TextFileHelper.EncryptLines( tf0, tf0.Lines ).Select( el => el.Item2 ).ToArray();
			byte[][] oriData = new byte[ tf0.Lines.Count ][];

			Assert.AreEqual( tf0.Lines.Count, encData.Length, "Number of data blobs returned does not equal number of lines" );

			for ( int l = 0; l < encData.Length; l++ )
			{
				byte[] testData = encData[ l ];
				TextFile.LineInfo lineInfo = tf0.LineInfos[ l ];
				oriData[ l ] = tf0Data.Skip( (int) lineInfo.Offset + 0x10 ).Take( lineInfo.Length * 2 ).ToArray();

				if ( testData.Length == oriData[ l ].Length + 2
					 && testData[ testData.Length - 1 ] == 0
					 && testData[ testData.Length - 2 ] == 0 )
					testData = testData.Take( testData.Length - 2 ).ToArray(); // don't compare the padding thing

				Assert.AreEqual( oriData[ l ], testData, $"Blob for line {l} does not match original file" );
			}

			string[] tfhText = TextFileHelper.DecryptLines( tf0, oriData );

			Assert.AreEqual( tf0.Lines.Count, tfhText.Length, "Number of strings returned does not equal number of lines" );

			for ( int l = 0; l < tfhText.Length; l++ )
			{
				Assert.AreEqual( tf0.Lines[ l ], tfhText[ l ], $"Line {l} does not match original file" );
			}
		}
	}
}