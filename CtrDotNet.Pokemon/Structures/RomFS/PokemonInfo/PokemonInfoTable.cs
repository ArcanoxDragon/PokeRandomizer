using System;
using System.IO;
using System.Linq;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Structures.RomFS.PokemonInfo
{
	public class PokemonInfoTable : BaseDataStructure
	{
		private readonly int entrySize;

		public PokemonInfoTable( GameVersion gameVersion ) : base( gameVersion )
		{
			switch ( gameVersion )
			{
				case GameVersion.XY:
					this.entrySize = PokemonInfoXY.Size;
					break;
				case GameVersion.ORASDemo:
				case GameVersion.ORAS:
					this.entrySize = PokemonInfoORAS.Size;
					break;
				case GameVersion.SunMoonDemo:
				case GameVersion.SunMoon:
					this.entrySize = PokemonInfoSM.Size;
					break;
			}
		}

		protected override void ReadData( BinaryReader br )
		{
			if ( br.BaseStream.Length == 0 )
			{
				this.Table = new PokemonInfo[ 0 ];
				return;
			}

			this.Table = new PokemonInfo[ br.BaseStream.Length / this.entrySize ];

			switch ( this.GameVersion )
			{
				case GameVersion.XY:
					this.Table.Fill( () => new PokemonInfoXY() );
					break;
				case GameVersion.ORASDemo:
				case GameVersion.ORAS:
					this.Table.Fill( () => new PokemonInfoORAS() );
					break;
				case GameVersion.SunMoonDemo:
				case GameVersion.SunMoon:
					this.Table.Fill( () => new PokemonInfoSM() );
					break;
			}

			foreach ( PokemonInfo info in this.Table )
			{
				byte[] subData = new byte[ this.entrySize ];
				br.Read( subData, 0, this.entrySize );
				info.Read( subData );
			}
		}

		protected override void WriteData( BinaryWriter bw )
		{
			if ( this.Table == null )
				return;

			foreach ( byte[] data in this.Table.Select( info => info.Write() ) )
			{
				bw.Write( data, 0, data.Length );
			}
		}

		public PokemonInfo[] Table { get; private set; }

		public PokemonInfo this[ int index ]
		{
			get => index >= 0 && index < this.Table.Length ? this.Table[ index ] : this.Table[ 0 ];
			set
			{
				if ( index < this.Table.Length )
					return;
				this.Table[ index ] = value;
			}
		}

		public PokemonInfo this[ BaseSpeciesType species ] => this[ species.Id ];

		public int[] GetAbilities( int species, int forme )
		{
			if ( species >= this.Table.Length )
			{
				species = 0;
				Console.WriteLine( "Requested out of bounds SpeciesID" );
			}
			return this[ this.GetFormeIndex( species, forme ) ].Abilities;
		}

		public int GetFormeIndex( int species, int forme )
		{
			if ( species >= this.Table.Length )
			{
				species = 0;
				Console.WriteLine( "Requested out of bounds SpeciesID" );
			}
			return this[ species ].FormeIndex( species, forme );
		}

		public PokemonInfo GetFormeEntry( int species, int forme )
		{
			return this[ this.GetFormeIndex( species, forme ) ];
		}

		public string[][] GetFormList( string[] species, int maxSpecies )
		{
			string[][] formList = new string[ maxSpecies + 1 ][];
			for ( int i = 0; i <= maxSpecies; i++ )
			{
				int formCount = this[ i ].FormeCount;
				formList[ i ] = new string[ formCount ];
				if ( formCount <= 0 )
					continue;
				formList[ i ][ 0 ] = species[ i ];
				for ( int j = 1; j < formCount; j++ )
				{
					formList[ i ][ j ] = $"{species[ i ]} " + j;
				}
			}

			return formList;
		}

		public string[] GetPersonalEntryList( string[][] altForms, string[] species, int maxSpecies, out int[] baseForm, out int[] formVal )
		{
			string[] result = new string[ this.Table.Length ];
			baseForm = new int[ result.Length ];
			formVal = new int[ result.Length ];
			for ( int i = 0; i <= maxSpecies; i++ )
			{
				result[ i ] = species[ i ];
				if ( altForms[ i ].Length == 0 )
					continue;
				int altformpointer = this[ i ].FormStatsIndex;
				if ( altformpointer <= 0 )
					continue;
				for ( int j = 1; j < altForms[ i ].Length; j++ )
				{
					int ptr = altformpointer + j - 1;
					baseForm[ ptr ] = i;
					formVal[ ptr ] = j;
					result[ ptr ] = altForms[ i ][ j ];
				}
			}
			return result;
		}

		public int[] GetSpeciesForm( int personalEntry )
		{
			int maxSpecies = this.GameVersion.GetGeneration().GetInfo().SpeciesCount;

			if ( personalEntry < maxSpecies )
				return new[] { personalEntry, 0 };

			for ( int i = 0; i < maxSpecies; i++ )
			{
				int formCount = this[ i ].FormeCount - 1; // Mons with no alt forms have a FormCount of 1.
				var altformpointer = this[ i ].FormStatsIndex;
				if ( altformpointer <= 0 )
					continue;
				for ( int j = 0; j < formCount; j++ )
					if ( altformpointer + j == personalEntry )
						return new[] { i, j };
			}

			return new[] { -1, -1 };
		}
	}
}