using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Reference;
using CtrDotNet.Utility.Extensions;
using NUnit.Framework;

namespace CtrDotNet.Pokemon.Tests.XY
{
	[ TestFixture ]
	public class ValueSearchTests
	{
		private static readonly int[] SearchValues = {
			0x0A0AF1E0 // Code
			//0x0A0AF1EF // Debug
		};

		private static readonly string[] SkipGarcs = {
			@"a\0\0\7", // Really really massive (1+GB) file
		};

		private GameConfig Game { get; set; }

		[ OneTimeSetUp ]
		public async Task SetUpGame()
		{
			this.Game = new GameConfig( GameVersion.XY );

			await this.Game.Initialize( @"D:\Users\Arcanox\Documents\3DS\Pokemon X\Unpacked\Vanilla", Language.English );
		}

		private async Task ForEachGarc( Func<GarcReference, Garc.ReferencedGarc, int, Task> action, int min = 000, int max = 270 )
		{
			for ( var garcIdx = min; garcIdx <= max; garcIdx++ )
			{
				var gRef = new GarcReference( garcIdx, default );

				if ( SkipGarcs.Contains( gRef.RomFsPath ) )
				{
					TestContext.Progress.WriteLine( $"Skipping GARC {gRef.RomFsPath}" );
					continue;
				}

				TestContext.Progress.WriteLine( $"Processing GARC {gRef.RomFsPath}..." );

				var garc = await this.Game.GetGarc( gRef, true );

				await action( gRef, garc, garcIdx );
			}
		}

		private async Task ForEachGarcFile( Garc.ReferencedGarc garc, Func<byte[], int, Task> action, int min = 000, int max = 270 )
		{
			for ( var fileIdx = 0; fileIdx < garc.Garc.FileCount; fileIdx++ )
			{
				var file = await garc.GetFile( fileIdx );

				await action( file, fileIdx );
			}
		}

		private async Task ForEachGarcFile( Func<GarcReference, byte[], int, int, Task> action, int min = 000, int max = 270 )
		{
			await this.ForEachGarc( async ( gRef, garc, garcIdx ) => {
				await this.ForEachGarcFile( garc, ( file, fileIdx ) => action( gRef, file, garcIdx, fileIdx ) );
			} );
		}

		private void CleanAndMakeDir( string dir )
		{
			if ( Directory.Exists( dir ) )
				Directory.Delete( dir, true );
			Directory.CreateDirectory( dir );
		}

		[ Test ]
		public async Task FindGarcScripts()
		{
			this.CleanAndMakeDir( "Garc" );
			this.CleanAndMakeDir( "Script" );

			await this.ForEachGarc( async ( gRef, garc, garcIdx ) => {
				var garcData    = await garc.Write();
				var garcOutPath = Path.Combine( "Garc", $"{gRef.RomFsPath}.bin".Replace( '\\', '_' ) );

				File.WriteAllBytes( garcOutPath, garcData );

				await this.ForEachGarcFile( garc, async ( file, fileIdx ) => {
					var fileOutPath = Path.Combine( "Garc", $"{gRef.RomFsPath}.{fileIdx}.bin".Replace( '\\', '_' ) );

					File.WriteAllBytes( fileOutPath, file );

					if ( garcIdx == 012 && fileIdx == 1 )
						Debugger.Break();

					if ( DoSearchValues( file, SearchValues.Select( BitConverter.GetBytes ), out var matches, out _ ) )
					{
						foreach ( var (i, match) in matches.Pairs() )
						{
							TestContext.Progress.WriteLine( $"\t!!!!!!!!!!!!! FOUND SCRIPT MAGIC: {gRef.RomFsPath}:{fileIdx} @ 0x{match:X} !!!!!!!!!!!!!" );

							if ( match < 4 )
							{
								// Probably not an actual script file; skip the whole file
								TestContext.Error.WriteLine( $"File {gRef.RomFsPath}.{fileIdx} had invalid match @ 0x{match:X}" );
								return;
							}

							// 4-byte length, then 4-byte magic
							var length        = BitConverter.ToInt32( file, match - 4 );
							var roundedLength = (int) Math.Ceiling( length / 4.0 ) * 4;

							if ( match - 4 + roundedLength > file.Length )
							{
								if ( match - 4 + length <= file.Length )
								{
									// Some files don't round up to the 4-byte boundary if the script ends at the end of the file
									roundedLength = length;
								}
								else
								{
									// Probably not an actual script file; skip the whole file
									TestContext.Error.WriteLine( $"File {gRef.RomFsPath}.{fileIdx} had invalid length 0x{length:X}" );
									return;
								}
							}

							var alignedData = file.Skip( match - 4 ).Take( roundedLength ).ToArray();
							var outFileName = $"{gRef.RomFsPath}.{fileIdx}.script.{i}.bin".Replace( '\\', '_' );
							var outFilePath = Path.Combine( "Script", outFileName );

							File.WriteAllBytes( outFilePath, alignedData );

							var outScriptPath = Path.Combine( "Script", outFileName.Replace( ".bin", ".txt" ) );

							await this.WriteScript( outFilePath, outScriptPath );
						}
					}
				} );
			}, 000, 031 );
		}

		/*[ Test ]
		public async Task PrintItemNPCScripts()
		{
			await this.ForEachGarcFile( async ( gRef, file, garcIdx, fileIdx ) => {
				//
			}, 012, 012 );
		}*/

		private async Task WriteScript( string binaryPath, string scriptPath )
		{
			var startInfo = new ProcessStartInfo {
				FileName               = "poketools.exe",
				UseShellExecute        = false,
				CreateNoWindow         = true,
				Arguments              = binaryPath,
				RedirectStandardOutput = true,
				RedirectStandardError  = true,
			};
			var process = Process.Start( startInfo );

			if ( process == null )
				throw new Exception( "Could not start poketools.exe" );

			var standardOutput = "";
			var standardError  = "";

			process.OutputDataReceived += ( sender, args ) => standardOutput += args.Data + Environment.NewLine;
			process.ErrorDataReceived  += ( sender, args ) => standardError  += args.Data + Environment.NewLine;

			process.BeginOutputReadLine();
			process.BeginErrorReadLine();

			if ( !process.WaitForExit( 5000 ) )
			{
				TestContext.Error.WriteLine( $"poketools.exe timed out for script file \"{binaryPath}\"" );
				return;
			}

			if ( standardError.Contains( "No blocks read!" ) )
				return;

			using ( var scriptStream = new FileStream( scriptPath, FileMode.Create, FileAccess.Write ) )
			using ( var scriptWriter = new StreamWriter( scriptStream ) )
			{
				await scriptWriter.WriteAsync( standardOutput );
				await scriptWriter.FlushAsync();
			}
		}

		[ Test ]
		public async Task TestCro()
		{
			foreach ( var croName in Enum.GetValues( typeof( CroNames ) ).Cast<CroNames>() )
			{
				try
				{
					TestContext.Progress.WriteLine( $"Searching CRO {croName}..." );

					var cro  = await this.Game.GetCroFile( croName );
					var code = await cro.GetCodeSection();
					var data = await cro.GetDataSection();

					if ( DoSearchValues( code, SearchValues.Select( BitConverter.GetBytes ), out var matches, out var strides ) )
					{
						TestContext.Progress.WriteLine( $"\t!!!!!!!!!!!!! FOUND MATCH: {croName}.code @ 0x{matches[ 0 ]:X} !!!!!!!!!!!!!" );
						TestContext.Progress.WriteLine( $"\tStrides: {string.Join( ",", strides )}" );

						File.WriteAllBytes( $"{croName}.code.bin".Replace( '\\', '_' ), code );
					}

					if ( DoSearchValues( data, SearchValues.Select( BitConverter.GetBytes ), out matches, out strides ) )
					{
						TestContext.Progress.WriteLine( $"\t!!!!!!!!!!!!! FOUND MATCH: {croName}.data @ 0x{matches[ 0 ]:X} !!!!!!!!!!!!!" );
						TestContext.Progress.WriteLine( $"\tStrides: {string.Join( ",", strides )}" );

						File.WriteAllBytes( $"{croName}.data.bin".Replace( '\\', '_' ), data );
					}
				}
				catch ( Exception ex )
				{
					TestContext.Progress.WriteLine( $"Error for {croName}: {ex.Message}" );
					// Ignored
				}
			}
		}

		[ Test ]
		public async Task TestCodeBin()
		{
			var codeBin = await this.Game.GetCodeBin();

			if ( DoSearchValues( codeBin.Data, SearchValues.Select( BitConverter.GetBytes ), out var matches, out var strides ) )
			{
				TestContext.Progress.WriteLine( $"\t!!!!!!!!!!!!! FOUND MATCH: .code.bin @ 0x{matches[ 0 ]:X} !!!!!!!!!!!!!" );
				TestContext.Progress.WriteLine( $"\tStrides: {string.Join( ",", strides )}" );

				File.WriteAllBytes( ".code.bin".Replace( '\\', '_' ), codeBin.Data );
			}
		}

		private static bool DoSearchValues( byte[] data, IEnumerable<byte[]> values, out int[] matches, out int[] strides, bool checkStride = false )
		{
			matches = values.SelectMany( val => ArraySearch( data, val ) )
							.OrderBy( v => v )
							.ToArray();

			if ( !matches.Any() )
			{
				strides = new int[ 0 ];
				return false;
			}

			strides = new int[ matches.Length - 1 ];

			if ( matches.Length == 1 || !checkStride )
			{
				return true;
			}

			var stride           = matches[ 1 ] - matches[ 0 ];
			var strideConsistent = true;

			for ( var m = 1; m < matches.Length; m++ )
			{
				var thisStride = matches[ m ] - matches[ m - 1 ];

				strides[ m - 1 ] = thisStride;

				//if ( thisStride < 0 )
				if ( thisStride != stride )
				{
					strideConsistent = false;
					break;
				}
			}

			if ( strideConsistent )
			{
				return true;
			}

			return false;
		}

		private static IEnumerable<int> ArraySearch( byte[] source, byte[] target, bool allowOverlap = false )
		{
			if ( source.Length == 0 || target.Length == 0 )
				yield break;

			for ( var i = 0; i <= source.Length - target.Length; i++ )
			{
				if ( source[ i ] == target[ 0 ] )
				{
					var match = true;

					for ( var j = 0; j < target.Length; j++ )
					{
						if ( source[ i + j ] != target[ j ] )
						{
							match = false;
						}
					}

					if ( match )
						yield return i;

					if ( !allowOverlap )
						i += target.Length - 1;
				}
			}
		}
	}
}