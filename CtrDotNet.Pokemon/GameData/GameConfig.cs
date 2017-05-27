using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.CTR;
using CtrDotNet.Pokemon.GARC;
using CtrDotNet.Pokemon.Structures.ExeFS.Common;
using CtrDotNet.Pokemon.Structures.RomFS.Common;
using CtrDotNet.Pokemon.Structures.RomFS.PokemonInfo;

namespace CtrDotNet.Pokemon.GameData
{
	public class GameConfig
	{
		private const int FilecountXy = 271;
		private const int FilecountOrasdemo = 301;
		private const int FilecountOras = 299;
		private const int FilecountSmdemo = 239;
		private const int FilecountSm = 311; // only a guess for now

		public GameConfig( int fileCount )
		{
			GameVersion game = GameVersion.Invalid;

			switch ( fileCount )
			{
				case FilecountXy:
					game = GameVersion.XY;
					break;
				case FilecountOrasdemo:
					game = GameVersion.ORASDemo;
					break;
				case FilecountOras:
					game = GameVersion.ORAS;
					break;
				case FilecountSmdemo:
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
		public GarcReference[] Files { get; private set; }
		public GarcFile GarcPersonal, GarcLearnsets, GarcMoves, GarcGameText;
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
					this.Files = GarcReference.GarcReferenceXy;
					this.Variables = TextVariableCode.VariableCodesXy;
					this.GameText = TextReference.GameTextXy;
					break;

				case GameVersion.ORASDemo:
				case GameVersion.ORAS:
					this.Files = GarcReference.GarcReferenceAo;
					this.Variables = TextVariableCode.VariableCodesAo;
					this.GameText = TextReference.GameTextAo;
					break;

				case GameVersion.SunMoonDemo:
					this.Files = GarcReference.GarcReferenceSmdemo;
					this.Variables = TextVariableCode.VariableCodesSm;
					this.GameText = TextReference.GameTextSmdemo;
					break;
				case GameVersion.SunMoon:
					this.Files = GarcReference.GarcReferenceSn;
					if ( new FileInfo( Path.Combine( this.RomFS, this.GetGarcFileName( "encdata" ) ) ).Length == 0 )
						this.Files = GarcReference.GarcReferenceMN;
					this.Variables = TextVariableCode.VariableCodesSm;
					this.GameText = TextReference.GameTextSm;
					break;
			}
		}

		public async Task Initialize( string romFSpath, string exeFSpath, Language lang )
		{
			this.RomFS = romFSpath;
			this.ExeFS = exeFSpath;
			this.Language = lang;
			this.GetGameData( this.Version );

			await this.InitializeAll();
		}

		private async Task InitializeAll()
		{
			await Task.WhenAll( this.InitializeLearnset(),
								this.InitializeGameText(),
								this.InitializeMoves() );
		}

		public async Task<PokemonInfoTable> LoadPokemonInfo()
		{
			this.GarcPersonal = await this.GetGarcData( "personal" );
			PokemonInfoTable table = new PokemonInfoTable( this.Version );
			table.Read( this.GarcPersonal.GetFile( this.GarcPersonal.FileCount - 1 ) );
			return table;
		}

		public async Task<TmsHms> LoadTmsHms()
		{
			byte[] data = await this.GetCodeBin();
			TmsHms tmsHms = new TmsHms( this );
			tmsHms.Read( data );
			return tmsHms;
		}

		private async Task InitializeLearnset()
		{
			this.GarcLearnsets = await this.GetGarcData( "levelup" );

			switch ( this.Version.GetGeneration() )
			{
				case GameGeneration.Generation6:
					this.Learnsets = this.GarcLearnsets.Files.Select( file => new Structures.RomFS.Gen6.Learnset( file ) );
					break;
				case GameGeneration.Generation7:
					this.Learnsets = this.GarcLearnsets.Files.Select( file => new Structures.RomFS.Gen7.Learnset( file ) );
					break;
			}
		}

		private async Task InitializeGameText()
		{
			this.GarcGameText = await this.GetGarcData( "gametext" );
			this.GameTextStrings = this.GarcGameText.Files.Select( file => new TextFile( file, this.Variables ).Lines.ToArray() ).ToArray();
		}

		private async Task InitializeMoves()
		{
			this.GarcMoves = await this.GetGarcData( "move" );
			switch ( this.Version.GetGeneration() )
			{
				case GameGeneration.Generation6:
					if ( this.Version.IsXY() )
						this.Moves = this.GarcMoves.Files.Select( file => new Move( file ) ).ToArray();
					if ( this.Version.IsORAS() )
						this.Moves = Mini.UnpackMini( this.GarcMoves.GetFile( 0 ), "WD" ).Select( file => new Move( file ) ).ToArray();
					break;
				case GameGeneration.Generation7:
					this.Moves = Mini.UnpackMini( this.GarcMoves.GetFile( 0 ), "WD" ).Select( file => new Move( file ) ).ToArray();
					break;
			}
		}

		public async Task<byte[]> GetCodeBin()
		{
			string path = Directory.GetFiles( this.ExeFS, "*code.bin" ).FirstOrDefault();

			if ( path == null )
				throw new FileNotFoundException( "Could not find code binary file" );

			using ( var fs = new FileStream( path, FileMode.Open, FileAccess.Read ) )
			{
				byte[] buffer = new byte[ fs.Length ];
				await fs.ReadAsync( buffer, 0, buffer.Length );
				return buffer;
			}
		}

		public async Task<LZGarcFile> GetlzGarcData( string file )
		{
			var gr = this.GetGarcReference( file );
			gr = gr.HasLanguageVariant ? gr.GetRelativeGarc( (int) this.Language, gr.Name ) : gr;
			return new LZGarcFile( await this.GetLzGarc( file ), gr, this.GetGarcPath( file ) );
		}

		public async Task<GarcFile> GetGarcData( string file, bool skipRelative = false )
		{
			var gr = this.GetGarcReference( file );
			if ( gr.HasLanguageVariant && !skipRelative )
				gr = gr.GetRelativeGarc( (int) this.Language, gr.Name );
			return await this.GetGarcByReference( gr );
		}

		public async Task<GarcFile> GetGarcByReference( GarcReference gr )
		{
			return new GarcFile( await this.GetMemGarc( gr.Name ), gr, this.GetGarcPath( gr.Name ) );
		}

		private string GetGarcPath( string file )
		{
			var gr = this.GetGarcReference( file );
			gr = gr.HasLanguageVariant ? gr.GetRelativeGarc( (int) this.Language, gr.Name ) : gr;
			string subloc = gr.Reference;
			return Path.Combine( this.RomFS, subloc );
		}

		private async Task<Garc.MemGarc> GetMemGarc( string file )
		{
			string path = this.GetGarcPath( file );
			using ( var fs = new FileStream( path, FileMode.Open ) )
			{
				byte[] buffer = new byte[ fs.Length ];
				await fs.ReadAsync( buffer, 0, buffer.Length );
				return new Garc.MemGarc( buffer );
			}
		}

		private async Task<Garc.LzGarc> GetLzGarc( string file )
		{
			string path = this.GetGarcPath( file );
			using ( var fs = new FileStream( path, FileMode.Open ) )
			{
				byte[] buffer = new byte[ fs.Length ];
				await fs.ReadAsync( buffer, 0, buffer.Length );
				return new Garc.LzGarc( buffer );
			}
		}

		public GarcReference GetGarcReference( string name )
		{
			return this.Files.FirstOrDefault( f => f.Name == name );
		}

		public TextVariableCode GetVariableCode( string name )
		{
			return this.Variables.FirstOrDefault( v => v.Name == name );
		}

		public TextVariableCode GetVariableName( int value )
		{
			return this.Variables.FirstOrDefault( v => v.Code == value );
		}

		public TextReference GetGameText( TextName name )
		{
			return this.GameText.FirstOrDefault( f => f.Name == name );
		}

		public string GetGarcFileName( string requestedGarc )
		{
			var garc = this.GetGarcReference( requestedGarc );

			if ( garc.HasLanguageVariant )
				garc = garc.GetRelativeGarc( (int) this.Language );

			return garc.Reference;
		}

		public bool IsRebuildable( int fileCount )
		{
			switch ( fileCount )
			{
				case FilecountXy:
					return this.Version == GameVersion.XY;
				case FilecountOras:
					return this.Version == GameVersion.ORAS;
				case FilecountOrasdemo:
					return this.Version == GameVersion.ORASDemo;
				case FilecountSmdemo:
					return this.Version == GameVersion.SunMoonDemo;
				case FilecountSm:
					return this.Version == GameVersion.SunMoon;
			}
			return false;
		}
	}
}