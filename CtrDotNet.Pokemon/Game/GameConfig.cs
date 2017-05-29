using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.CTR;
using CtrDotNet.CTR.Garc;
using CtrDotNet.Pokemon.ExeFS;
using CtrDotNet.Pokemon.Reference;
using CtrDotNet.Pokemon.Structures.ExeFS.Common;
using CtrDotNet.Pokemon.Structures.RomFS.Common;
using CtrDotNet.Pokemon.Structures.RomFS.PokemonInfo;

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

		public GameVersion Version { get; }
		public CodeBin CodeBin { get; private set; }
		public GarcReference[] Files { get; private set; }
		public GarcFile<MemGarc> GarcPersonal, GarcLearnsets, GarcMoves, GarcGameText;
		public TextVariableCode[] Variables { get; private set; }
		public TextReference[] GameText { get; private set; }
		public string[][] GameTextStrings { get; private set; }
		public IEnumerable<Learnset> Learnsets { get; private set; }
		public Move[] Moves { get; private set; }
		public string RomFS { get; private set; }
		public string ExeFS { get; private set; }
		public Language Language { get; set; }

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
					if ( new FileInfo( Path.Combine( this.RomFS, this.GetGarcFileName( "encdata" ) ) ).Length == 0 )
						this.Files = ReferenceLists.Garc.Moon;
					this.Variables = ReferenceLists.Variables.SunMoon;
					this.GameText = ReferenceLists.Text.SunMoon;
					break;
			}
		}

		#region Initialization

		public async Task Initialize( string romFSpath, string exeFSpath, Language lang )
		{
			this.RomFS = romFSpath;
			this.ExeFS = exeFSpath;
			this.Language = lang;

			this.GetGameData( this.Version );

			await this.GetCodeBin();
			await this.InitializeAll();
		}

		private async Task InitializeAll()
		{
			await Task.WhenAll( this.InitializeLearnset(),
								this.InitializeGameText(),
								this.InitializeMoves() );
		}

		private async Task InitializeLearnset()
		{
			this.GarcLearnsets = await this.GetMemGarcData( "levelup" );
			byte[][] files = await this.GarcLearnsets.GetFiles();

			switch ( this.Version.GetGeneration() )
			{
				case GameGeneration.Generation6:
					this.Learnsets = files.Select( file => new Structures.RomFS.Gen6.Learnset( file ) );
					break;
				case GameGeneration.Generation7:
					this.Learnsets = files.Select( file => new Structures.RomFS.Gen7.Learnset( file ) );
					break;
			}
		}

		private async Task InitializeGameText()
		{
			this.GarcGameText = await this.GetMemGarcData( "gametext" );
			byte[][] files = await this.GarcGameText.GetFiles();

			this.GameTextStrings = files.Select( file => new TextFile( file, this.Variables ).Lines.ToArray() ).ToArray();
		}

		private async Task InitializeMoves()
		{
			this.GarcMoves = await this.GetMemGarcData( "move" );
			byte[][] files = await this.GarcMoves.GetFiles();

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
			this.GarcPersonal = await this.GetMemGarcData( "personal" );
			PokemonInfoTable table = new PokemonInfoTable( this.Version );
			table.Read( await this.GarcPersonal.GetFile( this.GarcPersonal.FileCount - 1 ) );
			return table;
		}

		#endregion

		#region TMs/HMs

		public TmsHms GetTmsHms()
		{
			TmsHms tmsHms = new TmsHms( this.Version );
			tmsHms.Read( this.CodeBin.Data );
			return tmsHms;
		}

		public async Task SaveTmsHms( TmsHms tmsHms )
		{
			if ( tmsHms.GameVersion != this.Version )
				throw new ArgumentException( $"Version mismatch. Expected version {this.Version} but got {tmsHms.GameVersion}.", nameof(tmsHms) );

			tmsHms.Write( this.CodeBin.Data );

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

		public string GetGarcFileName( string garc )
		{
			var garcRef = this.GetGarcReference( garc );

			if ( garcRef.HasLanguageVariant )
				garcRef = garcRef.GetRelativeGarc( (int) this.Language );

			return garcRef.Reference;
		}

		public GarcReference GetGarcReference( string name ) => this.Files.FirstOrDefault( f => f.Name == name );

		private string GetGarcPath( string file )
		{
			var gr = this.GetGarcReference( file );
			gr = gr.HasLanguageVariant ? gr.GetRelativeGarc( (int) this.Language, gr.Name ) : gr;
			string subloc = gr.Reference;
			return Path.Combine( this.RomFS, subloc );
		}

		#region MemGarc

		private async Task<MemGarc> GetMemGarc( string file )
		{
			string path = this.GetGarcPath( file );
			using ( var fs = new FileStream( path, FileMode.Open ) )
			{
				byte[] buffer = new byte[ fs.Length ];
				await fs.ReadAsync( buffer, 0, buffer.Length );
				return new MemGarc( buffer );
			}
		}

		public async Task<GarcFile<MemGarc>> GetMemGarcByReference( GarcReference gr )
			=> new GarcFile<MemGarc>( await this.GetMemGarc( gr.Name ), this.GetGarcPath( gr.Name ) );

		public async Task<GarcFile<MemGarc>> GetMemGarcData( string file, bool skipRelative = false )
		{
			var gr = this.GetGarcReference( file );
			if ( gr.HasLanguageVariant && !skipRelative )
				gr = gr.GetRelativeGarc( (int) this.Language, gr.Name );
			return await this.GetMemGarcByReference( gr );
		}

		#endregion

		#region LzGarc

		private async Task<LzGarc> GetLzGarc( string file )
		{
			string path = this.GetGarcPath( file );
			using ( var fs = new FileStream( path, FileMode.Open ) )
			{
				byte[] buffer = new byte[ fs.Length ];
				await fs.ReadAsync( buffer, 0, buffer.Length );
				return new LzGarc( buffer );
			}
		}

		public async Task<GarcFile<LzGarc>> GetLzGarcByReference( GarcReference gr )
			=> new GarcFile<LzGarc>( await this.GetLzGarc( gr.Name ), this.GetGarcPath( gr.Name ) );

		public async Task<GarcFile<LzGarc>> GetLzGarcData( string file )
		{
			var gr = this.GetGarcReference( file );
			if ( gr.HasLanguageVariant )
				gr = gr.GetRelativeGarc( (int) this.Language, gr.Name );
			return await this.GetLzGarcByReference( gr );
		}

		#endregion

		#endregion

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