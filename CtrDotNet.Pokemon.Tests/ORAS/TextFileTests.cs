using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Reference;
using CtrDotNet.Pokemon.Structures.RomFS.Common;
using CtrDotNet.Pokemon.Utility;
using NUnit.Framework;
using NUnit.Framework.Internal;

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
					TestContext.Progress.WriteLine( $"\tPadding for line {i}: {padding}" );
					TestContext.Progress.WriteLine( $"\tLength: {lineInfo.Length}" );
					TestContext.Progress.WriteLine( $"\tLength % 4: {lineInfo.Length % 4}" );
				}

				string tfhString = TextFileHelper.GetLineString( tf0, TextFileHelper.CryptLineData( lineData, key ) );

				Assert.AreEqual( tf0.Lines[ i ], tfhString, "Directly decoded string not equal" );

				byte[] tfhData = TextFileHelper.CryptLineData( TextFileHelper.GetLineData( tf0, tf0.Lines[ i ] ), key );

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
			byte[] textData = tf0Data.Skip( 0x10 /* SectionDataOffset */ + 0x4 /* Section length */ + ( 8 * tf0.Lines.Count /* LineInfo[] size */ ) ).ToArray();

			byte[][] tfhData = TextFileHelper.EncryptLines( tf0, tf0.Lines.ToArray() );
			byte[][] actual = new byte[ tf0.Lines.Count ][];

			Assert.AreEqual( tf0.Lines.Count, tfhData.Length, "Number of data blobs returned does not equal number of lines" );

			for ( int l = 0; l < tfhData.Length; l++ )
			{
				byte[] testData = tfhData[ l ];
				TextFile.LineInfo lineInfo = tf0.LineInfos[ l ];
				actual[ l ] = tf0Data.Skip( (int) lineInfo.Offset + 0x10 ).Take( lineInfo.Length * 2 ).ToArray();

				if ( testData.Length == actual[ l ].Length + 2
					 && testData[ testData.Length - 1 ] == 0
					 && testData[ testData.Length - 2 ] == 0 )
					testData = testData.Take( testData.Length - 2 ).ToArray(); // don't compare the padding thing

				Assert.AreEqual( actual[ l ], testData, $"Blob for line {l} does not match original file" );
			}

			string[] tfhText = TextFileHelper.DecryptLines( tf0, actual );

			Assert.AreEqual( tf0.Lines.Count, tfhText.Length, "Number of strings returned does not equal number of lines" );

			for ( int l = 0; l < tfhText.Length; l++ )
			{
				Assert.AreEqual( tf0.Lines[ l ], tfhText[ l ], $"Line {l} does not match original file" );
			}
		}
	}
}