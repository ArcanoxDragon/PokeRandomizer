using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CtrDotNet.CTR.Garc;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Reference;
using CtrDotNet.Pokemon.Structures.RomFS.Common;
using CtrDotNet.Pokemon.Tests.Utility;
using NUnit.Framework;

namespace CtrDotNet.Pokemon.Tests.ORAS.Rewrite
{
	public partial class RomFS
	{
		[ Test, Order( 1 ) ]
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

			const int pbWidth = 50;
			double progress;
			int lastIntProgress = 0;

			TestContext.Progress.WriteLine( $"Testing {gameText.Length} text files..." );
			TestContext.Progress.WriteLine( $"S{"-".Repeat( pbWidth - 2 )}E" );

			void UpdateProgress()
			{
				int intProgress = (int) Math.Ceiling( progress * pbWidth );
				while ( lastIntProgress < intProgress )
				{
					TestContext.Progress.Write( "#" );
					lastIntProgress++;
				}
			}

			// Skip the last file (for ORAS) because it's weird and nobody likes it
			await this.TestGarcStructure( garcGameText, async () => {
				for ( int i = 0; i < gameText.Length; i++ )
				{
					try
					{
						int file = i;
						byte[] data = gameText[ file ].Write();

						TextFile test = new TextFile( ORASConfig.GameConfig.Version, ORASConfig.GameConfig.Variables );
						test.Read( data );
						var path = Path.Combine( this.path, "GameText", $"TextFile-{file}.txt" );
						Encoding testEncoding = Encoding.GetEncoding( "UTF-8", new EncoderReplacementFallback( "?" ), new DecoderExceptionFallback() );
						File.WriteAllLines( path, test.Lines, testEncoding );

						Assert.AreEqual( gameText[ file ].Lines, test.Lines, $"Strings did not match for file {file}" );

						await garcGameText.SetFile( file, data );
					}
					catch ( InconclusiveException )
					{
						// ignore
					}
					finally
					{
						progress = i / (double) gameText.Length;
						UpdateProgress();
					}
				}

				TestContext.Progress.WriteLine();
				TestContext.Out.WriteLine();
			}, false ); // ignore hash mismatch, because the game text files in the game are inconsistent with their own format
		}

		[ Test, Order( 2 ) ]
		[ SuppressMessage( "ReSharper", "AccessToDisposedClosure" ) ]
		public async Task DiffGameText()
		{
			using ( var fs = new FileStream( Path.Combine( this.path, "GameText.diff.txt" ), FileMode.Create, FileAccess.Write, FileShare.None ) )
			using ( var sw = new StreamWriter( fs ) )
			{
				async Task WriteLine( string line = "" )
				{
					TestContext.Progress.WriteLine( line );
					await sw.WriteLineAsync( line );
				}

				var cfg = ORASConfig.GameConfig;
				var garcRef = ORASConfig.GameConfig.GetGarcReference( GarcNames.GameText );
				var origPath = Path.Combine( this.path, $"{garcRef.RomFsPath}.orig" );
				var newPath = Path.Combine( this.path, garcRef.RomFsPath );
				var origGarc = await GarcFile.FromFile( origPath );
				var newGarc = await GarcFile.FromFile( newPath );

				Assert.AreEqual( origGarc.FileCount, newGarc.FileCount, "Re-written GameText GARC has different file count than original" );

				for ( int file = 0; file < origGarc.FileCount; file++ )
				{
					await WriteLine( $"File #{file} {"-".Repeat( 50 )}" );

					TextFile origFile = new TextFile( cfg.Version, cfg.Variables );
					TextFile newFile = new TextFile( cfg.Version, cfg.Variables );

					origFile.Read( await origGarc.GetFile( file ) );
					newFile.Read( await newGarc.GetFile( file ) );

					Assert.AreEqual( origFile.LineCount, newFile.LineCount, $"File {file} has different number of lines than original" );

					for ( int line = 0; line < origFile.LineCount; line++ )
					{
						TextFile.LineInfo origInfo = origFile.LineInfos[ line ];
						TextFile.LineInfo newInfo = newFile.LineInfos[ line ];
						string origLine = origFile.Lines[ line ];
						string newLine = newFile.Lines[ line ];
						bool dLength = origInfo.Length != newInfo.Length;
						bool dOffset = origInfo.Offset != newInfo.Offset;
						bool dFlag = origInfo.Flag != newInfo.Flag;
						bool dLine = origLine != newLine;
						bool diff = dLength || dOffset || dFlag || dLine;

						if ( diff )
						{
							await WriteLine( $"[DIFF] line #{line}" );

							if ( dLine )
							{
								await WriteLine( $"  Line Difference" );
								await WriteLine( $"    Original: {origLine}" );
								await WriteLine( $"         New: {newLine}" );
							}

							if ( dLength )
							{
								await WriteLine( $"  Length Difference" );
								await WriteLine( $"    Original: {origInfo.Length}" );
								await WriteLine( $"         New: {newInfo.Length}" );
							}

							if ( dOffset )
							{
								await WriteLine( $"  Offset Difference" );
								await WriteLine( $"    Original: {origInfo.Offset}" );
								await WriteLine( $"         New: {newInfo.Offset}" );
							}

							if ( dFlag )
							{
								await WriteLine( $"  Flag Difference" );
								await WriteLine( $"    Original: {origInfo.Flag}" );
								await WriteLine( $"         New: {newInfo.Flag}" );
							}
						}
						else
						{
							// don't bother spamming test output with this
							await sw.WriteLineAsync( $"[SAME] line #{line}" );
						}
					}
				}
			}
		}
	}
}