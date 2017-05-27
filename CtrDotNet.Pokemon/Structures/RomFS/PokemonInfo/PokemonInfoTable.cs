using System;
using System.Linq;
using CtrDotNet.Pokemon.Dynamic;
using CtrDotNet.Pokemon.GameData;
using CtrDotNet.Pokemon.Structures.RomFS.Common;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Structures.RomFS.PokemonInfo
{
	public class PokemonInfoTable : IDataStructure
	{
		private readonly GameVersion gameVersion;
		private readonly int entrySize;

		public PokemonInfoTable( GameVersion gameVersion )
		{
			this.gameVersion = gameVersion;

			switch ( this.gameVersion )
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

		public void Read( byte[] data )
		{
			if ( this.entrySize == 0 )
			{
				this.Table = null;
			}
			else
			{
				byte[][] entries = data.Partition( this.entrySize );
				PokemonInfo[] d = new PokemonInfo[ data.Length / this.entrySize ];

				switch ( this.gameVersion )
				{
					case GameVersion.XY:
						d.Fill( () => new PokemonInfoXY() );
						break;
					case GameVersion.ORASDemo:
					case GameVersion.ORAS:
						d.Fill( () => new PokemonInfoORAS() );
						break;
					case GameVersion.SunMoonDemo:
					case GameVersion.SunMoon:
						d.Fill( () => new PokemonInfoSM() );
						break;
				}

				for ( int i = 0; i < d.Length; i++ )
					d[ i ].Read( entries[ i ] );

				this.Table = d;
			}
		}

		public byte[] Write()
		{
			if ( this.Table == null )
				return new byte[ 0 ];

			byte[][] entries = new byte[ this.Table.Length ][];

			for ( int i = 0; i < this.Table.Length; i++ )
				entries[ i ] = this.Table[ i ].Write();

			return entries.SelectMany( e => e ).ToArray();
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

		public PokemonInfo this[ Species species ] => this[ (int) species ];

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
			int maxSpecies = this.gameVersion.GetGeneration().GetInfo().SpeciesCount;

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