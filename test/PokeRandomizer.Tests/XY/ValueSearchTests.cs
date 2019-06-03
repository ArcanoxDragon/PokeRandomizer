using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CtrDotNet.CTR.Cro;
using NUnit.Framework;
using pkNX.Structures;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Garc;
using PokeRandomizer.Common.Reference;
using GameVersion = PokeRandomizer.Common.Game.GameVersion;
using TextFile = PokeRandomizer.Common.Structures.RomFS.Common.TextFile;

namespace PokeRandomizer.Tests.XY
{
	[ TestFixture ]
	public class ValueSearchTests
	{
		private static readonly uint[] SearchValues = {
			0x0A0AF1E0, // Code (32-bit)
			0x0A0AF1E1, // Code (64-bit)
			0x0A0AF1E2, // Code (16-bit)
			0x0A0AF1EF, // Debug
		};

		private static readonly byte[] SearchBytes = {
			0x5A,
			0x4F,
			0x04,
			0x00,
			0x18,
			0x00,
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

		private async Task ForEachGarc( Func<GarcReference, ReferencedGarc, int, Task> action, int min = 000, int max = 270 )
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

		private async Task ForEachGarcFile( ReferencedGarc garc, Func<byte[], int, Task> action, int min = 000, int max = 270 )
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

		private async Task ForEachCro( Func<CroNames, CroFile, Task> action )
		{
			foreach ( var croName in Enum.GetValues( typeof( CroNames ) ).Cast<CroNames>() )
			{
				TestContext.Progress.WriteLine( $"Processing CRO file {croName}..." );
				CroFile cro;

				try
				{
					cro = await this.Game.GetCroFile( croName );
				}
				catch ( FileNotFoundException )
				{
					continue;
				}

				await action( croName, cro );
			}
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

				await File.WriteAllBytesAsync( garcOutPath, garcData );

				await this.ForEachGarcFile( garc, async ( file, fileIdx ) => {
					var fileOutPath = Path.Combine( "Garc", $"{gRef.RomFsPath}.{fileIdx}.bin".Replace( '\\', '_' ) );

					await File.WriteAllBytesAsync( fileOutPath, file );

					if ( DoSearchValues( file, SearchValues.Select( BitConverter.GetBytes ), out var matches, out _ ) )
					{
						TestContext.Progress.WriteLine( $"\t!!!!!!!!!!!!! FOUND SCRIPT MAGIC: {gRef.RomFsPath}:{fileIdx} @ 0x{matches[ 0 ]:X} !!!!!!!!!!!!!" );

						if ( matches[ 0 ] < 4 )
							// Need 4 bytes before magic; if not, it's not a real script
							return;

						var outFileName = $"{gRef.RomFsPath}.{fileIdx}.script.bin".Replace( '\\', '_' );
						var outFilePath = Path.Combine( "Script", outFileName );

						await File.WriteAllBytesAsync( outFilePath, file.Skip( matches[ 0 ] - 4 ).ToArray() );

						var outScriptPath = Path.Combine( "Script", outFileName.Replace( ".bin", ".txt" ) );

						await this.DisassembleScript( outFilePath, outScriptPath );
					}
				} );
			} );
		}

		[ Test ]
		public async Task SearchGarcs()
		{
			this.CleanAndMakeDir( "Garc" );
			this.CleanAndMakeDir( "Found" );

			await this.ForEachGarc( async ( gRef, garc, garcIdx ) => {
				var garcData    = await garc.Write();
				var garcOutPath = Path.Combine( "Garc", $"{gRef.RomFsPath}.bin".Replace( '\\', '_' ) );

				await File.WriteAllBytesAsync( garcOutPath, garcData );

				if ( DoSearchValues( garcData, new List<byte[]>{ SearchBytes }, out var matches, out _ ) )
				{
					TestContext.Progress.WriteLine( $"\t!!!!!!!!!!!!! FOUND PATTERN: {gRef.RomFsPath} @ 0x{matches[ 0 ]:X} !!!!!!!!!!!!!" );

					var outFileName = $"{gRef.RomFsPath}.found.bin".Replace( '\\', '_' );
					var outFilePath = Path.Combine( "Found", outFileName );

					await File.WriteAllBytesAsync( outFilePath, garcData );
				}

				await this.ForEachGarcFile( garc, async ( file, fileIdx ) => {
					var fileOutPath = Path.Combine( "Garc", $"{gRef.RomFsPath}.{fileIdx}.bin".Replace( '\\', '_' ) );

					await File.WriteAllBytesAsync( fileOutPath, file );

					if ( DoSearchValues( file, new List<byte[]>{ SearchBytes }, out matches, out _ ) )
					{
						TestContext.Progress.WriteLine( $"\t!!!!!!!!!!!!! FOUND PATTERN: {gRef.RomFsPath}:{fileIdx} @ 0x{matches[ 0 ]:X} !!!!!!!!!!!!!" );

						var outFileName = $"{gRef.RomFsPath}.{fileIdx}.found.bin".Replace( '\\', '_' );
						var outFilePath = Path.Combine( "Found", outFileName );

						await File.WriteAllBytesAsync( outFilePath, file );
					}
				} );
			} );
		}

		[ Test ]
		public async Task FindGarcText()
		{
			this.CleanAndMakeDir( "Garc" );
			this.CleanAndMakeDir( "Text" );

			await this.ForEachGarc( async ( gRef, garc, garcIdx ) => {
				var garcData    = await garc.Write();
				var garcOutPath = Path.Combine( "Garc", $"{gRef.RomFsPath}.bin".Replace( '\\', '_' ) );

				await File.WriteAllBytesAsync( garcOutPath, garcData );

				await this.ForEachGarcFile( garc, async ( file, fileIdx ) => {
					var fileOutPath = Path.Combine( "Garc", $"{gRef.RomFsPath}.{fileIdx}.bin".Replace( '\\', '_' ) );

					await File.WriteAllBytesAsync( fileOutPath, file );

					try
					{
						var textFile = new TextFile( this.Game.Version );

						textFile.Read( file );

						TestContext.Progress.WriteLine( $"Found valid text file: {gRef.RomFsPath}:{fileIdx}" );

						var outFileName = $"{gRef.RomFsPath}.{fileIdx}.txt".Replace( '\\', '_' );
						var outFilePath = Path.Combine( "Text", outFileName );

						await File.WriteAllLinesAsync( outFilePath, textFile.Lines );
					}
					catch
					{
						// Ignored
					}
				} );
			} );
		}

		[ Test ]
		public async Task FindScripts2()
		{
			var searchBytesAmxL    = Encoding.ASCII.GetBytes( "amx" );
			var searchBytesAmxU    = Encoding.ASCII.GetBytes( "AMX" );
			var searchBytesFldItem = Encoding.ASCII.GetBytes( "fld_item" );

			await this.ForEachCro( async ( name, file ) => {
				if ( DoSearchValues( file.Data, new List<byte[]> { searchBytesAmxL, searchBytesAmxU }, out var matches, out _ ) )
				{
					var addresses = string.Join( ", ", matches.Select( m => $"{m:X4}" ) );

					TestContext.Progress.WriteLine( $"Found \"amx\" in file {name}: {addresses}" );
				}

				if ( DoSearchValues( file.Data, new List<byte[]> { searchBytesFldItem }, out matches, out _ ) )
				{
					var addresses = string.Join( ", ", matches.Select( m => $"0x{m:X6}" ) );

					TestContext.Progress.WriteLine( $"Found \"fld_item\" in file {name}: {addresses}" );
				}

				if ( DoSearchValues( file.Data, SearchValues.Select( BitConverter.GetBytes ), out matches, out _ ) )
				{
					var addresses = string.Join( ", ", matches.Select( m => $"0x{m:X6}" ) );

					TestContext.Progress.WriteLine( $"Found AMX header in file {name}: {addresses}" );
				}
			} );
		}

		/*[ Test ]
		public async Task PrintItemNPCScripts()
		{
			await this.ForEachGarcFile( async ( gRef, file, garcIdx, fileIdx ) => {
				//
			}, 012, 012 );
		}*/

		private async Task DisassembleScript( string binaryPath, string scriptPath )
		{
			try
			{
				var scriptBinaryData = await File.ReadAllBytesAsync( binaryPath );
				var amx              = new Amx( scriptBinaryData );

				if ( !amx.DataChunk.Any() && !amx.ParseScript.Any() )
				{
					// Not a script
					try { File.Delete( binaryPath ); }
					catch
					{
						/**/
					}

					return;
				}

				using var scriptTextStream = new FileStream( scriptPath, FileMode.Create, FileAccess.Write );
				using var writer           = new StreamWriter( scriptTextStream );

				Task Line( string line = "" ) => writer.WriteLineAsync( line );

				// Write summary header
				await Line( "/**" );
				await Line( " * Script Information: " );

				foreach ( var line in amx.SummaryLines )
					await Line( $" * {line}" );

				await Line( " */" + Environment.NewLine );

				if ( amx.Publics != null )
				{
					// Write publics
					await Line( "/** Publics */" + Environment.NewLine );

					foreach ( var @public in amx.Publics.Where( p => p != null ) )
						await Line( $"0x{@public.Address:X8}: {@public.Name}" );

					await Line();
				}

				if ( amx.Natives != null )
				{
					// Write natives
					await Line( "/** Natives */" + Environment.NewLine );

					foreach ( var native in amx.Natives.Where( p => p != null ) )
						await Line( $"0x{native.Address:X8}: {native.Name}" );

					await Line();
				}

				if ( amx.Libraries != null )
				{
					// Write libraries
					await Line( "/** Libraries */" + Environment.NewLine );

					foreach ( var library in amx.Libraries.Where( p => p != null ) )
						await Line( $"0x{library.Address:X8}: {library.Name}" );

					await Line();
				}

				if ( amx.PublicVars != null )
				{
					// Write public vars
					await Line( "/** PublicVars */" + Environment.NewLine );

					foreach ( var publicVar in amx.PublicVars.Where( p => p != null ) )
						await Line( $"0x{publicVar.Address:X8}: {publicVar.Name}" );

					await Line();
				}

				// Write data section
				await Line( "/** Data Section */" + Environment.NewLine );

				foreach ( var line in amx.DataChunk )
					await Line( line );

				await Line();

				// Write code section
				await Line( "/** Code Section */" + Environment.NewLine );

				foreach ( var line in amx.ParseScript )
					await Line( line );

				// Newline at end of file
				await Line();
			}
			catch ( Exception ex )
			{
				TestContext.Error.WriteLine( $"Could not process script file: {binaryPath}" );
				TestContext.Error.WriteLine( ex );
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

					if ( ValueSearchTests.DoSearchValues( code, SearchValues.Select( BitConverter.GetBytes ), out var matches, out var strides ) )
					{
						TestContext.Progress.WriteLine( $"\t!!!!!!!!!!!!! FOUND MATCH: {croName}.code @ 0x{matches[ 0 ]:X} !!!!!!!!!!!!!" );
						TestContext.Progress.WriteLine( $"\tStrides: {string.Join( ",", strides )}" );

						File.WriteAllBytes( $"{croName}.code.bin".Replace( '\\', '_' ), code );
					}

					if ( ValueSearchTests.DoSearchValues( data, SearchValues.Select( BitConverter.GetBytes ), out matches, out strides ) )
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

			if ( ValueSearchTests.DoSearchValues( codeBin.Data, SearchValues.Select( BitConverter.GetBytes ), out var matches, out var strides ) )
			{
				TestContext.Progress.WriteLine( $"\t!!!!!!!!!!!!! FOUND MATCH: .code.bin @ 0x{matches[ 0 ]:X} !!!!!!!!!!!!!" );
				TestContext.Progress.WriteLine( $"\tStrides: {string.Join( ",", strides )}" );

				File.WriteAllBytes( ".code.bin".Replace( '\\', '_' ), codeBin.Data );
			}
		}

		private static bool DoSearchValues( byte[] data, IEnumerable<byte[]> values, out int[] matches, out int[] strides, bool checkStride = false )
		{
			matches = values.SelectMany( val => ValueSearchTests.ArraySearch( data, val ) )
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