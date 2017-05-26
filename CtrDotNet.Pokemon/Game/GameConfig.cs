using System;
using System.IO;
using System.Linq;
using CtrDotNet.CTR;

namespace CtrDotNet.Pokemon.Game
{
	public class GameConfig
	{
		private const int FilecountXy = 271;
		private const int FilecountOrasdemo = 301;
		private const int FilecountOras = 299;
		private const int FilecountSmdemo = 239;
		private const int FilecountSm = 311; // only a guess for now

		public GameVersion Version { get; } = GameVersion.Invalid;
		public GarcReference[] Files { get; private set; }
		public TextVariableCode[] Variables { get; private set; }
		public TextReference[] GameText { get; private set; }

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

		public void Initialize( string romFSpath, string exeFSpath, int lang )
		{
			this.RomFS = romFSpath;
			this.ExeFS = exeFSpath;
			this.Language = lang;
			this.GetGameData( this.Version );
			this.InitializeAll();
		}

		public void InitializeAll()
		{
			this.InitializePersonal();
			this.InitializeLearnset();
			this.InitializeGameText();
			this.InitializeMoves();
		}

		public void InitializePersonal()
		{
			this.GarcPersonal = this.GetGarcData( "personal" );
			this.Personal = new PersonalTable( this.GarcPersonal.GetFile( this.GarcPersonal.FileCount - 1 ), this.Version );
		}

		public void InitializeLearnset()
		{
			this.GarcLearnsets = this.GetGarcData( "levelup" );
			switch ( this.Generation )
			{
				case 6:
					this.Learnsets = this.GarcLearnsets.Files.Select( file => new Learnset6( file ) ).ToArray();
					break;
				case 7:
					this.Learnsets = this.GarcLearnsets.Files.Select( file => new Learnset7( file ) ).ToArray();
					break;
			}
		}

		public void InitializeGameText()
		{
			this.GarcGameText = this.GetGarcData( "gametext" );
			this.GameTextStrings = this.GarcGameText.Files.Select( file => new TextFile( file ).Lines ).ToArray();
		}

		public void InitializeMoves()
		{
			this.GarcMoves = this.GetGarcData( "move" );
			switch ( this.Generation )
			{
				case 6:
					if ( this.Xy )
						this.Moves = this.GarcMoves.Files.Select( file => new Move( file ) ).ToArray();
					if ( this.Oras )
						this.Moves = mini.unpackMini( this.GarcMoves.GetFile( 0 ), "WD" ).Select( file => new Move( file ) ).ToArray();
					break;
				case 7:
					this.Moves = mini.unpackMini( this.GarcMoves.GetFile( 0 ), "WD" ).Select( file => new Move( file ) ).ToArray();
					break;
			}
		}

		public LZGarcFile GetlzGarcData( string file )
		{
			var gr = this.GetGarcReference( file );
			gr = gr.LanguageVariant
					 ? gr.GetRelativeGarc( this.Language, gr.Name )
					 : gr;
			return new LZGarcFile( this.GetlzGarc( file ), gr, this.GetGarcPath( file ) );
		}

		public GarcFile GetGarcData( string file, bool skipRelative = false )
		{
			var gr = this.GetGarcReference( file );
			if ( gr.LanguageVariant && !skipRelative )
				gr = gr.GetRelativeGarc( this.Language, gr.Name );
			return this.GetGarcByReference( gr );
		}

		public GarcFile GetGarcByReference( GarcReference gr )
		{
			return new GarcFile( this.GetMemGarc( gr.Name ), gr, this.GetGarcPath( gr.Name ) );
		}

		private string GetGarcPath( string file )
		{
			var gr = this.GetGarcReference( file );
			gr = gr.LanguageVariant
					 ? gr.GetRelativeGarc( this.Language, gr.Name )
					 : gr;
			string subloc = gr.Reference;
			return Path.Combine( this.RomFS, subloc );
		}

		private Garc.MemGarc GetMemGarc( string file )
		{
			return new Garc.MemGarc( File.ReadAllBytes( this.GetGarcPath( file ) ) );
		}

		private Garc.LzGarc GetlzGarc( string file )
		{
			return new Garc.LzGarc( File.ReadAllBytes( this.GetGarcPath( file ) ) );
		}

		public string RomFS, ExeFS;

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
			if ( garc.LanguageVariant )
				garc = garc.GetRelativeGarc( this.Language );

			return garc.Reference;
		}

		public int Language { get; set; }

		public GarcFile GarcPersonal, GarcLearnsets, GarcMoves, GarcGameText;
		public PersonalTable Personal { get; private set; }
		public Learnset[] Learnsets { get; private set; }
		public string[][] GameTextStrings { get; private set; }
		public Move[] Moves { get; private set; }

		public bool Xy => this.Version == GameVersion.XY;
		public bool Oras => this.Version == GameVersion.ORAS || this.Version == GameVersion.ORASDemo;
		public bool Sm => this.Version == GameVersion.SunMoon || this.Version == GameVersion.SunMoonDemo;

		public int MaxSpeciesID => this.Xy || this.Oras
									   ? 721
									   : 802;

		public int GarcVersion => this.Xy || this.Oras
									  ? GARC.VER_4
									  : GARC.VER_6;

		public int Generation
		{
			get
			{
				if ( this.Xy || this.Oras )
					return 6;
				if ( this.Sm )
					return 7;
				return -1;
			}
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