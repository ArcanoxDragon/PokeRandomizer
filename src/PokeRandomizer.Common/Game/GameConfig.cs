using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.CTR;
using CtrDotNet.CTR.Cro;
using CtrDotNet.CTR.Garc;
using pkNX.Structures;
using PokeRandomizer.Common.ExeFS;
using PokeRandomizer.Common.Garc;
using PokeRandomizer.Common.Reference;
using PokeRandomizer.Common.Structures.CRO.Gen6.Starters;
using PokeRandomizer.Common.Structures.ExeFS.Common;
using PokeRandomizer.Common.Structures.RomFS.Common;
using PokeRandomizer.Common.Structures.RomFS.Gen6;
using PokeRandomizer.Common.Structures.RomFS.PokemonInfo;
using Assertions = PokeRandomizer.Common.Utility.Assertions;
using EggMoves = PokeRandomizer.Common.Structures.RomFS.Common.EggMoves;
using EvolutionSet = PokeRandomizer.Common.Structures.RomFS.Common.EvolutionSet;
using Item = PokeRandomizer.Common.Structures.RomFS.Gen6.Item;
using Learnset = PokeRandomizer.Common.Structures.RomFS.Common.Learnset;
using Move = PokeRandomizer.Common.Structures.RomFS.Common.Move;
using TextFile = PokeRandomizer.Common.Structures.RomFS.Common.TextFile;
using TextVariableCode = PokeRandomizer.Common.Reference.TextVariableCode;
using TrainerData = PokeRandomizer.Common.Structures.RomFS.Gen6.TrainerData;

namespace PokeRandomizer.Common.Game
{
	public class GameConfig
	{
		private const int FileCountXy       = 271;
		private const int FileCountOrasDemo = 301;
		private const int FileCountOras     = 299;
		private const int FileCountSmDemo   = 239;
		private const int FileCountSm       = 311; // only a guess for now

		#region Constructors

		public GameConfig( int fileCount )
		{
			var game = GameVersion.Invalid;

			switch ( fileCount )
			{
				case FileCountXy:
					game = GameVersion.XY;
					break;
				case FileCountOrasDemo:
					game = GameVersion.ORASDemo;
					break;
				case FileCountOras:
					game = GameVersion.ORAS;
					break;
				case FileCountSmDemo:
					game = GameVersion.SunMoonDemo;
					break;
				case FileCountSm:
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

		#endregion

		#region Properties

		public GameVersion        Version             { get; }
		public GarcReference[]    GarcFiles           { get; private set; }
		public AmxReference[]     AmxFiles            { get; private set; }
		public TextVariableCode[] Variables           { get; private set; }
		public TextReference[]    TextFiles           { get; private set; }
		public string             RomPath             { get; private set; }
		public string             RomFS               => Path.Combine( this.RomPath, "RomFS" );
		public string             ExeFS               => Path.Combine( this.RomPath, "ExeFS" );
		public Language           Language            { get; set; }
		public string             OutputPathOverride  { get; set; }
		public bool               IsOverridingOutPath => !string.IsNullOrEmpty( this.OutputPathOverride );

		#endregion

		#region Initialization

		private void GetGameData( GameVersion version )
		{
			switch ( version )
			{
				case GameVersion.XY:
					this.GarcFiles = ReferenceLists.Garc.Xy;
					this.Variables = ReferenceLists.Variables.Xy;
					this.TextFiles = ReferenceLists.Text.Xy;
					this.AmxFiles  = ReferenceLists.Amx.Xy;
					break;

				case GameVersion.ORASDemo:
				case GameVersion.ORAS:
					this.GarcFiles = ReferenceLists.Garc.Oras;
					this.Variables = ReferenceLists.Variables.Oras;
					this.TextFiles = ReferenceLists.Text.Oras;
					this.AmxFiles  = ReferenceLists.Amx.Oras;
					break;

				case GameVersion.SunMoonDemo:
					this.GarcFiles = ReferenceLists.Garc.SunMoonDemo;
					this.Variables = ReferenceLists.Variables.SunMoon;
					this.TextFiles = ReferenceLists.Text.SunMoonDemo;
					break;
				case GameVersion.SunMoon:
					this.GarcFiles = ReferenceLists.Garc.Sun;
					if ( new FileInfo( Path.Combine( this.RomFS, this.GetGarcFileName( GarcNames.EncounterData ) ) ).Length == 0 )
						this.GarcFiles = ReferenceLists.Garc.Moon;
					this.Variables = ReferenceLists.Variables.SunMoon;
					this.TextFiles = ReferenceLists.Text.SunMoon;
					break;
				default:
					throw new ArgumentOutOfRangeException( nameof(version), version, null );
			}
		}

		public async Task Initialize( string romPath, Language lang )
		{
			this.RomPath  = romPath;
			this.Language = lang;

			this.GetGameData( this.Version );

			await this.GetCodeBin();
		}

		#endregion

		#region Game Data

		public Task SaveFile( IWritableFile file ) => this.IsOverridingOutPath
														  ? file.SaveFileTo( this.OutputPathOverride )
														  : file.SaveFile();

		#region Egg Moves

		public async Task<EggMoves[]> GetEggMoves( bool edited = false )
		{
			var garcEggMoves = await this.GetGarc( GarcNames.EggMoves, edited: edited );
			var files        = await garcEggMoves.GetFiles();
			var array        = new EggMoves[ files.Length ];

			for ( var i = 0; i < files.Length; i++ )
			{
				var file = files[ i ];

				array[ i ] = EggMoves.New( this.Version );
				array[ i ].Read( file );
			}

			return array;
		}

		internal async Task SaveEggMoves( IEnumerable<EggMoves> eggMoves, ReferencedGarc garcEggMoves )
		{
			var files = eggMoves.Select( em => em.Write() ).ToArray();

			await garcEggMoves.SetFiles( files );
		}

		public async Task SaveEggMoves( IEnumerable<EggMoves> eggMoves )
		{
			var garcEggMoves = await this.GetGarc( GarcNames.EggMoves );
			await this.SaveEggMoves( eggMoves, garcEggMoves );
			await this.SaveFile( garcEggMoves );
		}

		#endregion

		#region Encounter Data

		private int ZoneDataPositionFromEnd => this.Version == GameVersion.XY ? 1 : 2;

		public async Task<IEnumerable<EncounterWild>> GetEncounterData( bool edited = false )
		{
			var encounterGarc     = await this.GetGarc( GarcNames.EncounterData, useLz: true, edited: edited );
			var zoneDataFileIndex = encounterGarc.Garc.FileCount - this.ZoneDataPositionFromEnd;

			// All files up until the zone data file are the encounter data
			var encounterBuffers = ( await encounterGarc.GetFiles() ).Take( zoneDataFileIndex ).ToArray();
			var zoneDataBuffer   = await encounterGarc.GetFile( zoneDataFileIndex );
			var zoneData         = new ZoneData( this.Version );

			zoneData.Read( zoneDataBuffer );

			if ( zoneData.Entries.Length > encounterBuffers.Length )
				throw new InvalidDataException( $"Zone data and encounter data mismatch. Zone data had {zoneData.Entries.Length} entries, but encounter data only had {encounterBuffers.Length}" );

			return encounterBuffers.Zip( zoneData.Entries, ( b, e ) => {
				var encounter = EncounterWild.New( this.Version, e.ZoneId );
				encounter.Read( b );
				return encounter;
			} );
		}

		internal async Task SaveEncounterData( IEnumerable<EncounterWild> encounters, ReferencedGarc garcEncounters )
		{
			var array             = encounters.ToArray();
			var files             = await garcEncounters.GetFiles();
			var zoneDataFileIndex = garcEncounters.Garc.FileCount - this.ZoneDataPositionFromEnd;

			Assertions.AssertLength( zoneDataFileIndex, array, exact: true );

			for ( var i = 0; i < array.Length; i++ )
			{
				if ( !array[ i ].HasEntries )
					continue;

				var encounterBuffer = array[ i ].Write();

				if ( this.Version.IsORAS() ) // Have to write encounter zone data to DexNav database too
				{
					// Last file is dexNavData
					const int Offset       = 0xE;
					var       dexNavData   = files[ files.Length - 1 ];
					var       entryPointer = BitConverter.ToInt32( dexNavData, ( i + 1 ) * sizeof( int ) ) + Offset;

					var dataPointer = BitConverter.ToInt32( encounterBuffer, EncounterWild.DataOffset );
					dataPointer += array[ i ].DataStart;
					var dataLength = array[ i ].GetAllEntries().Count() * EncounterWild.Entry.Size;

					Array.Copy( encounterBuffer, dataPointer, dexNavData, entryPointer, dataLength );
				}

				files[ i ] = encounterBuffer;
			}

			await garcEncounters.SetFiles( files );
		}

		public async Task SaveEncounterData( IEnumerable<EncounterWild> encounters )
		{
			var garcEncounters = await this.GetGarc( GarcNames.EncounterData, useLz: true );
			await this.SaveEncounterData( encounters, garcEncounters );
			await this.SaveFile( garcEncounters );
		}

		#endregion

		#region Evolutions

		public async Task<IEnumerable<EvolutionSet>> GetEvolutions( bool edited = false )
		{
			var garcEvolutions = await this.GetGarc( GarcNames.Evolutions, edited: edited );
			var files          = await garcEvolutions.GetFiles();

			return files.Select( f => {
				var evoSet = EvolutionSet.New( this.Version );
				evoSet.Read( f );
				return evoSet;
			} );
		}

		internal async Task SaveEvolutions( IEnumerable<EvolutionSet> evolutions, ReferencedGarc garcEvolutions )
		{
			var files = evolutions.Select( e => e.Write() ).ToArray();
			await garcEvolutions.SetFiles( files );
		}

		public async Task SaveEvolutions( IEnumerable<EvolutionSet> evolutions )
		{
			var garcEvolutions = await this.GetGarc( GarcNames.Evolutions );
			await this.SaveEvolutions( evolutions, garcEvolutions );
			await this.SaveFile( garcEvolutions );
		}

		#endregion

		#region Game Text

		public async Task<TextFile[]> GetTextFiles( bool edited = false )
		{
			var garcGameText = await this.GetGarc( GarcNames.GameText, edited: edited );
			var files        = await garcGameText.GetFiles();

			return files.Select( file => {
				var tf = new TextFile( this.Version, this.Variables );
				tf.Read( file );
				return tf;
			} ).ToArray();
		}

		public async Task<TextFile> GetTextFile( int textFile, bool edited = false, Language languageOverride = Language.None )
		{
			var garcGameText = await this.GetGarc( GarcNames.GameText, edited: edited, languageOverride: languageOverride );
			var file         = await garcGameText.GetFile( textFile );

			var tf = new TextFile( this.Version, this.Variables );
			tf.Read( file );
			return tf;
		}

		public Task<TextFile> GetTextFile( TextNames name, bool edited = false, Language languageOverride = Language.None )
		{
			var reference = this.GetTextReference( name );

			return reference == null ? null : this.GetTextFile( reference.Index, edited, languageOverride );
		}

		internal async Task SaveTextFiles( IEnumerable<TextFile> textFiles, ReferencedGarc garcGameText )
		{
			var files = textFiles.Select( tf => tf.Write() ).ToArray();

			await garcGameText.SetFiles( files );
		}

		public async Task SaveTextFiles( IEnumerable<TextFile> textFiles )
		{
			var garcGameText = await this.GetGarc( GarcNames.GameText );
			await this.SaveTextFiles( textFiles, garcGameText );
			await this.SaveFile( garcGameText );
		}

		internal async Task SaveTextFile( int fileNum, TextFile textFile, ReferencedGarc garcGameText )
		{
			await garcGameText.SetFile( fileNum, textFile.Write() );
		}

		public async Task SaveTextFile( int fileNum, TextFile textFile )
		{
			var garcGameText = await this.GetGarc( GarcNames.GameText );
			await this.SaveTextFile( fileNum, textFile, garcGameText );
			await this.SaveFile( garcGameText );
		}

		#endregion

		#region Items

		public async Task<IEnumerable<Item>> GetItems( bool edited = false )
		{
			var garcItems = await this.GetGarc( GarcNames.Items, edited: edited );
			var files     = await garcItems.GetFiles();

			return files.Select( f => {
				var i = new Structures.RomFS.Gen6.Item( this.Version );

				i.Read( f );

				return i;
			} );
		}

		internal async Task SaveItems( IEnumerable<Item> items, ReferencedGarc garcItems )
		{
			var list = items.ToList();

			foreach ( var i in list )
				Assertions.AssertVersion( this.Version, i.GameVersion );

			var files = list.Select( l => l.Write() ).ToArray();

			await garcItems.SetFiles( files );
		}

		public async Task SaveItems( IEnumerable<Item> items )
		{
			var garcItems = await this.GetGarc( GarcNames.Items );
			await this.SaveItems( items, garcItems );
			await this.SaveFile( garcItems );
		}

		#endregion

		#region Learnsets

		public async Task<IEnumerable<Learnset>> GetLearnsets( bool edited = false )
		{
			var garcLearnsets = await this.GetGarc( GarcNames.Learnsets, edited: edited );
			var files         = await garcLearnsets.GetFiles();
			return files.Select( f => {
				var l = Learnset.New( this.Version );

				l.Read( f );

				return l;
			} );
		}

		internal async Task SaveLearnsets( IEnumerable<Learnset> learnsets, ReferencedGarc garcLearnsets )
		{
			var list = learnsets.ToList();

			foreach ( var l in list )
				Assertions.AssertVersion( this.Version, l.GameVersion );

			var files = list.Select( l => l.Write() ).ToArray();

			await garcLearnsets.SetFiles( files );
		}

		public async Task SaveLearnsets( IEnumerable<Learnset> learnsets )
		{
			var garcLearnsets = await this.GetGarc( GarcNames.Learnsets );
			await this.SaveLearnsets( learnsets, garcLearnsets );
			await this.SaveFile( garcLearnsets );
		}

		#endregion

		#region Moves

		public async Task<IEnumerable<Move>> GetMoves( bool edited = false )
		{
			var           garcMoves = await this.GetGarc( GarcNames.Moves, edited: edited );
			IList<byte[]> files     = await garcMoves.GetFiles();

			if ( this.Version.IsORAS() || this.Version.IsGen7() )
			{
				files = Mini.UnpackMini( files[ 0 ], "WD" );
			}

			return files.Select( file => {
				var m = new Move( this.Version );
				m.Read( file );
				return m;
			} );
		}

		internal async Task SaveMoves( IEnumerable<Move> moves, ReferencedGarc garcMoves )
		{
			var list = moves.ToList();

			foreach ( var move in list )
				Assertions.AssertVersion( this.Version, move.GameVersion );

			if ( this.Version.IsORAS() || this.Version.IsGen7() )
			{
				var file = Mini.PackMini( list.Select( m => m.Write() ).ToArray(), "WD" );
				await garcMoves.SetFile( 0, file );
			}
			else
			{
				await garcMoves.SetFiles( list.Select( m => m.Write() ).ToArray() );
			}
		}

		public async Task SaveMoves( IEnumerable<Move> moves )
		{
			var garcMoves = await this.GetGarc( GarcNames.Moves );
			await this.SaveMoves( moves, garcMoves );
			await this.SaveFile( garcMoves );
		}

		#endregion

		#region Overworld Items

		public async Task<OverworldItems> GetOverworldItems( bool edited = false )
		{
			var itemsAmxFile = await this.GetAmxFile( AmxNames.FldItem, edited );
			var items        = new OverworldItems( this.Version );
			items.Read( itemsAmxFile );
			return items;
		}

		public async Task SaveOverworldItems( OverworldItems items )
		{
			var data = items.Write();

			await this.SaveAmxFile( AmxNames.FldItem, data );
		}

		#endregion

		#region Pokemon Species Info

		public async Task<PokemonInfoTable> GetPokemonInfo( bool edited = false )
		{
			var table        = new PokemonInfoTable( this.Version );
			var garcPersonal = await this.GetGarc( GarcNames.PokemonInfo, edited: edited );
			var data         = await garcPersonal.GetFile( garcPersonal.Garc.FileCount - 1 );
			table.Read( data );
			return table;
		}

		internal async Task SavePokemonInfo( PokemonInfoTable table, ReferencedGarc garcPokeInfo )
		{
			Assertions.AssertVersion( this.Version, table.GameVersion );

			var files = await garcPokeInfo.GetFiles();

			files[ garcPokeInfo.Garc.FileCount - 1 ] = table.Write();

			await garcPokeInfo.SetFiles( files );
		}

		public async Task SavePokemonInfo( PokemonInfoTable table )
		{
			var garcPokeInfo = await this.GetGarc( GarcNames.PokemonInfo );
			await this.SavePokemonInfo( table, garcPokeInfo );
			await this.SaveFile( garcPokeInfo );
		}

		#endregion

		#region Starters

		public async Task<Starters> GetStarters( bool edited = false )
		{
			var dllField      = await this.GetCroFile( CroNames.Field,       edited );
			var dllPokeSelect = await this.GetCroFile( CroNames.Poke3Select, edited );
			var starters      = new Starters( this.Version );

			await starters.Read( dllField, dllPokeSelect );

			return starters;
		}

		internal Task SaveStarters( Starters starters, CroFile dllField, CroFile dllPokeSelect )
			=> starters.Write( dllField, dllPokeSelect );

		public async Task SaveStarters( Starters starters )
		{
			var dllField      = await this.GetCroFile( CroNames.Field );
			var dllPokeSelect = await this.GetCroFile( CroNames.Poke3Select );

			await this.SaveStarters( starters, dllField, dllPokeSelect );

			await this.SaveFile( dllField );
			await this.SaveFile( dllPokeSelect );
		}

		#endregion

		#region TMs/HMs

		public async Task<TmsHms> GetTmsHms( bool edited = false )
		{
			var codeBin = await this.GetCodeBin( edited );
			var tmsHms  = new TmsHms( this.Version );
			tmsHms.ReadFromCodeBin( codeBin );
			return tmsHms;
		}

		internal void SaveTmsHms( TmsHms tmsHms, CodeBin codeBin )
		{
			Assertions.AssertVersion( this.Version, tmsHms.GameVersion );

			codeBin.WriteStructure( tmsHms );
		}

		public async Task SaveTmsHms( TmsHms tmsHms )
		{
			var codeBin = await this.GetCodeBin();
			this.SaveTmsHms( tmsHms, codeBin );
			await this.SaveFile( codeBin );
		}

		#endregion

		#region Trainer Data

		public async Task<IEnumerable<TrainerData>> GetTrainerData( bool edited = false )
		{
			var garcTrainerData = await this.GetGarc( GarcNames.TrainerData,    edited: edited );
			var garcTrainerPoke = await this.GetGarc( GarcNames.TrainerPokemon, edited: edited );

			if ( garcTrainerData.Garc.FileCount != garcTrainerPoke.Garc.FileCount )
				throw new InvalidDataException( $"Trainer data and trainer team databases did not have the same number of files. " +
												$"Trainer data had {garcTrainerData.Garc.FileCount}, and trainer team had {garcTrainerPoke.Garc.FileCount}" );

			return await Task.WhenAll( Enumerable.Range( 0, garcTrainerData.Garc.FileCount ).Select( async i => {
				var files    = await Task.WhenAll( garcTrainerData.GetFile( i ), garcTrainerPoke.GetFile( i ) );
				var dataFile = files[ 0 ];
				var pokeFile = files[ 1 ];

				var data = new TrainerData( this.Version );
				data.Read( dataFile );
				data.ReadTeam( pokeFile );

				return data;
			} ) );
		}

		internal async Task SaveTrainerData( IEnumerable<TrainerData> trainerData, ReferencedGarc garcTrainerData, ReferencedGarc garcTrainerPoke )
		{
			var list         = trainerData.ToList();
			var trainerFiles = new byte[ list.Count ][];
			var pokemonFiles = new byte[ list.Count ][];

			for ( var i = 0; i < list.Count; i++ )
			{
				trainerFiles[ i ] = list[ i ].Write();
				pokemonFiles[ i ] = list[ i ].WriteTeam();
			}

			await Task.WhenAll( garcTrainerData.SetFiles( trainerFiles ), garcTrainerPoke.SetFiles( pokemonFiles ) );
		}

		public async Task SaveTrainerData( IEnumerable<TrainerData> trainerData )
		{
			var garcTrainerData = await this.GetGarc( GarcNames.TrainerData );
			var garcTrainerPoke = await this.GetGarc( GarcNames.TrainerPokemon );

			await this.SaveTrainerData( trainerData, garcTrainerData, garcTrainerPoke );

			await Task.WhenAll( this.SaveFile( garcTrainerData ), this.SaveFile( garcTrainerPoke ) );
		}

		#endregion

		#endregion

		#region CRO

		public Task<CroFile> GetCroFile( CroNames name, bool edited = false )
		{
			var filename = $"Dll{name}.cro";
			var path     = Path.Combine( this.RomFS, filename );

			if ( edited && this.IsOverridingOutPath )
			{
				var editedPath = Path.Combine( this.OutputPathOverride, "RomFS", filename );

				if ( File.Exists( editedPath ) )
					path = editedPath;
			}

			if ( !File.Exists( path ) )
				throw new FileNotFoundException( $"CRO file not found: {name}" );

			return CroFile.FromFile( path );
		}

		#endregion

		#region code.bin

		public async Task<CodeBin> GetCodeBin( bool edited = false )
		{
			var path = this.ExeFS;

			if ( edited && this.IsOverridingOutPath )
			{
				var editedPath = Path.Combine( this.OutputPathOverride, "ExeFS" );

				if ( Directory.GetFiles( editedPath, "*code.bin" ).Length > 0 )
					path = editedPath;
			}

			var filename = Directory.GetFiles( path, "*code.bin" ).FirstOrDefault();

			if ( filename == null )
				throw new FileNotFoundException( "Could not find code binary file" );

			var codeBin = new CodeBin( filename );

			await codeBin.Load();

			return codeBin;
		}

		#endregion

		#region Garc

		public GarcReference GetGarcReference( GarcNames garcName, Language languageOverride = Language.None )
		{
			var garcRef = this.GarcFiles.FirstOrDefault( f => f.Name == garcName );

			if ( garcRef == null )
				throw new FileNotFoundException( $"GARC file not found: {garcName}" );

			if ( garcRef.HasLanguageVariant )
				garcRef = garcRef.GetRelativeGarc( (int) ( languageOverride == Language.None ? this.Language : languageOverride ) );

			return garcRef;
		}

		public string GetGarcFileName( GarcNames garcName )
			=> this.GetGarcReference( garcName ).RomFsPath;

		private string GetGarcPath( GarcReference garcReference, bool edited = false )
		{
			var path = this.RomFS;

			if ( edited && this.IsOverridingOutPath )
			{
				var editedPath = Path.Combine( this.OutputPathOverride, "RomFS" );

				if ( File.Exists( Path.Combine( editedPath, garcReference.RomFsPath ) ) )
					path = editedPath;
			}

			return Path.Combine( path, garcReference.RomFsPath );
		}

		public async Task<ReferencedGarc> GetGarc( GarcReference gr, bool useLz = false, bool edited = false )
		{
			var path = this.GetGarcPath( gr, edited );
			var file = await GarcFile.FromFile( path, useLz );
			return new ReferencedGarc( file, gr );
		}

		public async Task<ReferencedGarc> GetGarc( GarcNames garcName, bool useLz = false, bool edited = false, Language languageOverride = Language.None )
		{
			var gr = this.GetGarcReference( garcName, languageOverride );
			return await this.GetGarc( gr, useLz, edited );
		}

		#endregion

		#region Scripts

		public async Task<byte[]> GetAmxFile( AmxNames amxName, bool edited = false )
		{
			var garc   = await this.GetGarc( GarcNames.AmxFiles, edited: edited );
			var amxRef = this.AmxFiles.SingleOrDefault( amx => amx.Name == amxName );

			if ( amxRef == null )
				throw new NotSupportedException( $"The game version \"{this.Version}\" does not support the AMX file \"{amxName}\"" );

			return await garc.GetFile( amxRef.FileNumber );
		}

		public async Task<Amx> GetAmxScript( AmxNames amxName, bool edited = false )
		{
			var amxFile = await this.GetAmxFile( amxName, edited );

			return new Amx( amxFile );
		}

		public async Task SaveAmxFile( AmxNames amxName, byte[] fileData )
		{
			var garc   = await this.GetGarc( GarcNames.AmxFiles );
			var amxRef = this.AmxFiles.SingleOrDefault( amx => amx.Name == amxName );

			if ( amxRef == null )
				throw new NotSupportedException( $"The game version \"{this.Version}\" does not support the AMX file \"{amxName}\"" );

			await garc.SetFile( amxRef.FileNumber, fileData );
			await this.SaveFile( garc );
		}

		public Task SaveAmxScript( AmxNames amxName, Amx amxScript ) => this.SaveAmxFile( amxName, amxScript.Data );

		#endregion

		#region Misc Helpers

		public TextVariableCode GetVariableCode( string name ) => this.Variables.FirstOrDefault( v => v.Name == name );

		public TextVariableCode GetVariableName( int value ) => this.Variables.FirstOrDefault( v => v.Code == value );

		public TextReference GetTextReference( TextNames name ) => this.TextFiles.FirstOrDefault( f => f.Name == name );

		public bool IsRebuildable( int fileCount )
		{
			switch ( fileCount )
			{
				case FileCountXy:
					return this.Version == GameVersion.XY;
				case FileCountOras:
					return this.Version == GameVersion.ORAS;
				case FileCountOrasDemo:
					return this.Version == GameVersion.ORASDemo;
				case FileCountSmDemo:
					return this.Version == GameVersion.SunMoonDemo;
				case FileCountSm:
					return this.Version == GameVersion.SunMoon;
			}

			return false;
		}

		#endregion
	}
}