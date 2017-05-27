using System;
using System.IO;
using System.Linq;

namespace CtrDotNet.Pokemon.Game
{
	public static class GameBackup
	{
		private const string Bakpath = "backup";
		private const string Bakexefs = "exefs";
		private const string Baka = "a";
		private const string Bakdll = "dll";

		public static void BackupFiles( this GameConfig config, bool overwrite = false )
		{
			// Users may use pk3DS for multiple games, and even the same game but from different paths.
			// A simple way is to create a backup for each unique game, but... some carts may be pre-patched.
			// Just save the backup based on the folder name, as the user may move that parent folder.
			// Store a text file in each backup to keep track of its origin in case they rename the folder.

			if ( !Directory.Exists( Bakpath ) )
				Directory.CreateDirectory( Bakpath );

			var gamePath = new DirectoryInfo( config.RomFS ).Parent;
			string gameFolder = gamePath?.Name;
			string gameBackup = Path.Combine( Bakpath, gameFolder );
			if ( !Directory.Exists( gameBackup ) )
				Directory.CreateDirectory( gameBackup );

			string bakExefs = Path.Combine( gameBackup, Bakexefs );
			string bakA = Path.Combine( gameBackup, Baka );
			string bakDll = Path.Combine( gameBackup, Bakdll );
			if ( !Directory.Exists( bakExefs ) )
				Directory.CreateDirectory( bakExefs );
			if ( !Directory.Exists( bakA ) )
				Directory.CreateDirectory( bakA );
			if ( !Directory.Exists( bakDll ) )
				Directory.CreateDirectory( bakDll );

			// Backup files
			if ( config.ExeFS != null ) // exefs
				GameBackup.BackupExeFS( config, overwrite, bakExefs );
			if ( config.RomFS != null ) // a
				GameBackup.BackupGarc( config, overwrite, bakA );
			if ( config.RomFS != null ) // dll
				GameBackup.BackupDll( config, overwrite, bakDll );

			File.WriteAllText( Path.Combine( gameBackup, "bakinfo.txt" ), "Backup created from the following location:" + Environment.NewLine + gamePath?.FullName );
		}

		private static void BackupExeFS( GameConfig config, bool overwrite, string bakExefs )
		{
			var files = Directory.GetFiles( config.ExeFS );
			foreach ( var f in files )
			{
				string dest = Path.Combine( bakExefs, Path.GetFileName( f ) );
				if ( overwrite || !File.Exists( dest ) )
					File.Copy( f, dest );
			}
		}

		private static void BackupGarc( GameConfig config, bool overwrite, string bakA )
		{
			var files = config.Files.Select( file => file.Name );
			foreach ( var f in files )
			{
				string garc = config.GetGarcFileName( f );
				string name = f + $" ({garc.Replace( Path.DirectorySeparatorChar.ToString(), "" )})";
				string src = Path.Combine( config.RomFS, garc );
				string dest = Path.Combine( bakA, name );
				if ( overwrite || !File.Exists( dest ) )
					File.Copy( src, dest );
			}
		}

		private static void BackupDll( GameConfig config, bool overwrite, string bakDll )
		{
			string path = config.RomFS;
			string[] files = Directory.GetFiles( path );
			string[] crOs = files.Where( x => new FileInfo( x ).Name.Contains( "Dll" ) ).ToArray();
			string[] crSs = files.Where( x => new FileInfo( x ).Extension.Contains( "crs" ) ).ToArray();
			string[] crRs = Directory.Exists( Path.Combine( path, ".crr" ) )
								? Directory.GetFiles( Path.Combine( path, ".crr" ) )
								: new string[ 0 ];

			int count = crOs.Length + crSs.Length + crRs.Length;
			if ( count <= 0 )
				return;

			if ( !Directory.Exists( bakDll ) )
				Directory.CreateDirectory( bakDll );

			foreach ( string src in crOs.Concat( crSs ) )
			{
				string dest = Path.Combine( bakDll, Path.GetFileName( src ) );
				if ( overwrite || !File.Exists( dest ) )
					File.Copy( src, dest );
			}

			if ( crRs.Length <= 0 )
				return;

			// Separate folder for the .crr
			string crrbakpath = Path.Combine( bakDll, ".crr" );
			if ( !Directory.Exists( crrbakpath ) )
				Directory.CreateDirectory( crrbakpath );

			foreach ( string src in crRs )
			{
				string dest = Path.Combine( crrbakpath, Path.GetFileName( src ) );
				if ( overwrite || !File.Exists( dest ) )
					File.Copy( src, dest );
			}
		}

		public static string[] RestoreFiles( this GameConfig config )
		{
			// Do the same process as backing up, but copy files in the opposite direction.

			string gameFolder = new DirectoryInfo( config.RomFS ).Parent?.Name;
			string gameBackup = Path.Combine( Bakpath, gameFolder );
			if ( !Directory.Exists( gameBackup ) )
				return new[] { "Unable to find the backup folder for this game.", $"Expected:\n{gameBackup}" };

			string bakExefs = Path.Combine( gameBackup, Bakexefs );
			string bakA = Path.Combine( gameBackup, Baka );
			string bakDll = Path.Combine( gameBackup, Bakdll );

			int[] count = new int[ 3 ];

			// restore exefs
			if ( Directory.Exists( bakExefs ) )
				count[ 0 ] = GameBackup.RestoreExeFS( config, bakExefs );
			if ( Directory.Exists( bakA ) )
				count[ 1 ] = GameBackup.RestoreGarc( config, bakA );
			if ( Directory.Exists( bakDll ) )
				count[ 2 ] = GameBackup.RestoreDll( config, bakDll );

			string[] sources = { "ExeFS", "'a'", "CRO" };
			var info = count.Select( ( c, i ) => $"{sources[ i ]}: {c}" );
			var result = string.Join( Environment.NewLine, info );
			return new[] { result };
		}

		private static int RestoreExeFS( GameConfig config, string bakExefs )
		{
			int count = 0;
			var files = Directory.GetFiles( config.ExeFS );
			foreach ( var src in files )
			{
				string dest = Path.Combine( bakExefs, Path.GetFileName( src ) );
				if ( File.Exists( dest ) )
				{
					try
					{
						File.Copy( dest, src, overwrite: true );
						count++;
					}
					catch
					{
						Console.WriteLine( "Unable to overwrite backup: " + dest );
					}
				}
				else
					Console.WriteLine( "Unable to find backup: " + dest );
			}
			return count;
		}

		private static int RestoreGarc( GameConfig config, string bakA )
		{
			int count = 0;
			var files = config.Files.Select( file => file.Name );
			foreach ( var f in files )
			{
				string garc = config.GetGarcFileName( f );
				string name = f + $" ({garc.Replace( Path.DirectorySeparatorChar.ToString(), "" )})";
				string src = Path.Combine( config.RomFS, garc );
				string dest = Path.Combine( bakA, name );
				if ( File.Exists( dest ) )
				{
					try
					{
						File.Copy( dest, src, overwrite: true );
						count++;
					}
					catch
					{
						Console.WriteLine( "Unable to overwrite backup: " + dest );
					}
				}
				else
					Console.WriteLine( "Unable to find backup: " + dest );
			}
			return count;
		}

		private static int RestoreDll( GameConfig config, string bakDll )
		{
			int count = 0;

			string path = config.RomFS;
			string[] files = Directory.GetFiles( path );
			string[] crOs = files.Where( x => new FileInfo( x ).Name.Contains( "Dll" ) ).ToArray();
			string[] crSs = files.Where( x => new FileInfo( x ).Extension.Contains( "crs" ) ).ToArray();
			string[] crRs = Directory.Exists( Path.Combine( path, ".crr" ) )
								? Directory.GetFiles( Path.Combine( path, ".crr" ) )
								: new string[ 0 ];

			if ( crOs.Length + crSs.Length + crRs.Length <= 0 )
				return 0;

			foreach ( string src in crOs.Concat( crSs ) )
			{
				string dest = Path.Combine( bakDll, Path.GetFileName( src ) );
				if ( File.Exists( dest ) )
				{
					try
					{
						File.Copy( dest, src, overwrite: true );
						count++;
					}
					catch
					{
						Console.WriteLine( "Unable to overwrite backup: " + dest );
					}
				}
				else
					Console.WriteLine( "Unable to find backup: " + dest );
			}

			if ( crRs.Length <= 0 )
				return count;

			// Separate folder for the .crr
			string crrbakpath = Path.Combine( bakDll, ".crr" );
			foreach ( string src in crRs )
			{
				string dest = Path.Combine( crrbakpath, Path.GetFileName( src ) );
				if ( File.Exists( dest ) )
				{
					File.Copy( dest, src );
					count++;
				}
				else
					Console.WriteLine( "Unable to find backup: " + dest );
			}
			return count;
		}
	}
}