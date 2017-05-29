using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.CTR;
using CtrDotNet.CTR.Garc;
using CtrDotNet.Pokemon.ExeFS;
using CtrDotNet.Pokemon.Garc;
using CtrDotNet.Pokemon.Reference;
using CtrDotNet.Pokemon.Structures.ExeFS.Common;
using CtrDotNet.Pokemon.Structures.RomFS.Common;
using CtrDotNet.Pokemon.Structures.RomFS.PokemonInfo;
using Gen6 = CtrDotNet.Pokemon.Structures.RomFS.Gen6;
using Gen7 = CtrDotNet.Pokemon.Structures.RomFS.Gen7;

namespace CtrDotNet.Pokemon.Game
{
	public class GameConfig
	{
		private const int FilecountXy = 271;
		private const int FilecountOrasDemo = 301;
		private const int FilecountOras = 299;
		private const int FilecountSmDemo = 239;
		private const int FilecountSm = 311; // only a guess for now

		public GameConfig( int fileCount )
		{
			GameVersion game = GameVersion.Invalid;

			switch ( fileCount )
			{
				case FilecountXy:
					game = GameVersion.XY;
					break;
				case FilecountOrasDemo:
					game = GameVersion.ORASDemo;
					break;
				case FilecountOras:
					game = GameVersion.ORAS;
					break;
				case FilecountSmDemo:
					game = GameVersion.SunMoonDemo;
					break;
				case FilecountSm:
					game = GameVersion.SunMoon;
					break;
			}

			if ( game == GameVersion.Invalid )
				throw new InvalidOperationException( $"Invalid game version. No game known with {fileCount} files." );

			this.Version = game;
		}

		public GameConfig( GameVersion game )
		{
			this.Version = game;
		}

		#region Properties

		public GameVersion Version { get; }
		public CodeBin CodeBin { get; private set; }
		public GarcReference[] Files { get; private set; }
		public TextVariableCode[] Variables { get; private set; }
		public TextReference[] GameText { get; private set; }
		public string[][] GameTextStrings { get; private set; }
		public IEnumerable<Learnset> Learnsets { get; private set; }
		public Move[] Moves { get; private set; }
		public string RomFS { get; private set; }
		public string ExeFS { get; private set; }
		public Language Language { get; set; }

		#region Files

		//public async Task<ReferencedGarc<MemGarc>> GetGarcPersonal() => this.garcPersonal ?? ( this.garcPersonal = await this.GetGarcData<MemGarc>( "personal" ) );
		//public async Task<ReferencedGarc<MemGarc>> GetGarcLearnsets() => this.garcLearnsets ?? ( this.garcLearnsets = await this.GetGarcData<MemGarc>( "levelup" ) );
		//public async Task<ReferencedGarc<MemGarc>> GetGarcMoves() => this.garcMoves ?? ( this.garcMoves = await this.GetGarcData<MemGarc>( "move" ) );
		//public async Task<ReferencedGarc<MemGarc>> GetGarcGameText() => this.garcGameText ?? ( this.garcGameText = await this.GetGarcData<MemGarc>( "gametext" ) );

		#endregion

		#endregion

		#region Initialization

		private void GetGameData( GameVersion game )
		{
			switch ( game )
			{
				case GameVersion.XY:
					this.Files = ReferenceLists.Garc.Xy;
					this.Variables = ReferenceLists.Variables.Xy;
					this.GameText = ReferenceLists.Text.Xy;
					break;

				case GameVersion.ORASDemo:
				case GameVersion.ORAS:
					this.Files = ReferenceLists.Garc.Oras;
					this.Variables = ReferenceLists.Variables.Oras;
					this.GameText = ReferenceLists.Text.Oras;
					break;

				case GameVersion.SunMoonDemo:
					this.Files = ReferenceLists.Garc.SunMoonDemo;
					this.Variables = ReferenceLists.Variables.SunMoon;
					this.GameText = ReferenceLists.Text.SunMoonDemo;
					break;
				case GameVersion.SunMoon:
					this.Files = ReferenceLists.Garc.Sun;
					if ( new FileInfo( Path.Combine( this.RomFS, this.GetGarcFileName( GarcNames.EncounterData ) ) ).Length == 0 )
						this.Files = ReferenceLists.Garc.Moon;
					this.Variables = ReferenceLists.Variables.SunMoon;
					this.GameText = ReferenceLists.Text.SunMoon;
					break;
			}
		}

		public async Task Initialize( string romFSpath, string exeFSpath, Language lang )
		{
			this.RomFS = romFSpath;
			this.ExeFS = exeFSpath;
			this.Language = lang;

			this.GetGameData( this.Version );

			await this.GetCodeBin();
		}

		private async Task InitializeMoves()
		{
			var garcMoves = await this.GetGarcData( GarcNames.Moves );
			byte[][] files = await garcMoves.GetFiles();

			switch ( this.Version.GetGeneration() )
			{
				case GameGeneration.Generation6:
					if ( this.Version.IsXY() )
						this.Moves = files.Select( file => new Move( file ) ).ToArray();
					if ( this.Version.IsORAS() )
						this.Moves = Mini.UnpackMini( files[ 0 ], "WD" ).Select( file => new Move( file ) ).ToArray();
					break;
				case GameGeneration.Generation7:
					this.Moves = Mini.UnpackMini( files[ 0 ], "WD" ).Select( file => new Move( file ) ).ToArray();
					break;
			}
		}

		#endregion

		#region Game Data

		#region Pokemon Species Info

		public async Task<PokemonInfoTable> GetPokemonInfo()
		{
			PokemonInfoTable table = new PokemonInfoTable( this.Version );
			var garcPersonal = await this.GetGarcData( GarcNames.PokemonInfo );
			byte[] data = await garcPersonal.GetFile( garcPersonal.Garc.FileCount - 1 );
			table.Read( data );
			return table;
		}

		public async Task SavePokemonInfo( PokemonInfoTable table )
		{
			this.AssertVersion( table.GameVersion );

			var garcPersonal = await this.GetGarcData( GarcNames.PokemonInfo );
			byte[][] files = await garcPersonal.GetFiles();

			files[ garcPersonal.Garc.FileCount - 1 ] = table.Write();

			await garcPersonal.SetFiles( files );
			await garcPersonal.Save();
		}

		#endregion

		#region Learnsets

		public async Task<IEnumerable<Learnset>> GetLearnsets()
		{
			var garcLearnsets = await this.GetGarcData( GarcNames.Learnsets );
			byte[][] files = await garcLearnsets.GetFiles();
			return files.Select( f => {
				Learnset l;

				if ( this.Version.IsGen6() )
					l = new Gen6.Learnset( this.Version );
				else
					l = new Gen7.Learnset( this.Version );

				l.Read( f );
				return l;
			} );
		}

		public async Task SaveLearnsets( IEnumerable<Learnset> learnsets )
		{
			var garcLearnsets = await this.GetGarcData( GarcNames.Learnsets );
			IList<Learnset> list = learnsets.ToList();

			foreach ( Learnset l in list )
				this.AssertVersion( l.GameVersion );

			byte[][] files = list.Select( l => l.Write() ).ToArray();

			await garcLearnsets.SetFiles( files );
			await garcLearnsets.Save();
		}

		#endregion

		#region Game Text

		public async Task<TextFile[]> GetGameText()
		{
			var garcGameText = await this.GetGarcData( GarcNames.GameText );
			byte[][] files = await garcGameText.GetFiles();

			return files.Select( file => {
				TextFile tf = new TextFile( this.Version, this.Variables );
				tf.Read( file );
				return tf;
			} ).ToArray();
		}

		public async Task<TextFile> GetGameText( int textFile )
		{
			var garcGameText = await this.GetGarcData( GarcNames.GameText );
			byte[] file = await garcGameText.GetFile( textFile );

			TextFile tf = new TextFile( this.Version, this.Variables );
			tf.Read( file );
			return tf;
		}

		public async Task SaveGameText( IEnumerable<TextFile> textFiles )
		{
			var garcGameText = await this.GetGarcData( GarcNames.GameText );
			byte[][] files = textFiles.Select( tf => tf.Write() ).ToArray();

			await garcGameText.SetFiles( files );
			await garcGameText.Save();
		}

		public async Task SaveGameText( int fileNum, TextFile textFile )
		{
			var garcGameText = await this.GetGarcData( GarcNames.GameText );

			await garcGameText.SetFile( fileNum, textFile.Write() );
			await garcGameText.Save();
		}

		#endregion

		#region TMs/HMs

		public TmsHms GetTmsHms()
		{
			TmsHms tmsHms = new TmsHms( this.Version );
			tmsHms.ReadFromCodeBin( this.CodeBin );
			return tmsHms;
		}

		public async Task SaveTmsHms( TmsHms tmsHms )
		{
			this.AssertVersion( tmsHms.GameVersion );

			this.CodeBin.WriteStructure( tmsHms );

			await this.CodeBin.Save();
		}

		#endregion

		#endregion

		#region code.bin

		private async Task GetCodeBin()
		{
			string path = Directory.GetFiles( this.ExeFS, "*code.bin" ).FirstOrDefault();

			if ( path == null )
				throw new FileNotFoundException( "Could not find code binary file" );

			this.CodeBin = new CodeBin( path );
			await this.CodeBin.Load();
		}

		#endregion

		#region Garc

		public string GetGarcFileName( GarcNames garcName )
		{
			var garcRef = this.GetGarcReference( garcName );

			if ( garcRef.HasLanguageVariant )
				garcRef = garcRef.GetRelativeGarc( (int) this.Language );

			return garcRef.RomFsPath;
		}

		public GarcReference GetGarcReference( GarcNames garcName ) => this.Files.FirstOrDefault( f => f.Name == garcName );

		private string GetGarcPath( GarcNames garcName )
		{
			var gr = this.GetGarcReference( garcName );
			gr = gr.HasLanguageVariant ? gr.GetRelativeGarc( (int) this.Language ) : gr;
			string subloc = gr.RomFsPath;
			return Path.Combine( this.RomFS, subloc );
		}

		private async Task<T> GetGarc<T>( GarcNames garcName ) where T : BaseGarc, new()
		{
			string path = this.GetGarcPath( garcName );
			using ( var fs = new FileStream( path, FileMode.Open ) )
			{
				byte[] buffer = new byte[ fs.Length ];
				await fs.ReadAsync( buffer, 0, buffer.Length );
				T garc = new T();
				garc.Read( buffer );
				return garc;
			}
		}

		public async Task<ReferencedGarc<T>> GetGarcByReference<T>( GarcReference gr ) where T : BaseGarc, new()
		{
			GarcFile<T> file = new GarcFile<T>( await this.GetGarc<T>( gr.Name ), this.GetGarcPath( gr.Name ) );
			return new ReferencedGarc<T>( file, gr );
		}

		public async Task<ReferencedGarc<T>> GetGarcData<T>( GarcNames garcName, bool skipRelative = false ) where T : BaseGarc, new()
		{
			var gr = this.GetGarcReference( garcName );
			if ( gr.HasLanguageVariant && !skipRelative )
				gr = gr.GetRelativeGarc( (int) this.Language );
			return await this.GetGarcByReference<T>( gr );
		}

		public Task<ReferencedGarc<MemGarc>> GetGarcData( GarcNames garcName, bool skipRelative = false ) => this.GetGarcData<MemGarc>( garcName, skipRelative );

		#endregion

		private void AssertVersion( GameVersion check )
		{
			if ( check != this.Version )
				throw new ArgumentException( $"Version mismatch. Expected version {this.Version} but got {check}." );
		}

		public TextVariableCode GetVariableCode( string name ) => this.Variables.FirstOrDefault( v => v.Name == name );

		public TextVariableCode GetVariableName( int value ) => this.Variables.FirstOrDefault( v => v.Code == value );

		public TextReference GetGameText( TextNames name ) => this.GameText.FirstOrDefault( f => f.Name == name );

		public bool IsRebuildable( int fileCount )
		{
			switch ( fileCount )
			{
				case FilecountXy:
					return this.Version == GameVersion.XY;
				case FilecountOras:
					return this.Version == GameVersion.ORAS;
				case FilecountOrasDemo:
					return this.Version == GameVersion.ORASDemo;
				case FilecountSmDemo:
					return this.Version == GameVersion.SunMoonDemo;
				case FilecountSm:
					return this.Version == GameVersion.SunMoon;
			}
			return false;
		}
	}
}