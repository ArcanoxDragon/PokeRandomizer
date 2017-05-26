using System;
using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Structures.PersonalInfo
{
	public class PersonalTable
	{
		#region Static

		private static byte[][] SplitBytes( byte[] data, int size )
		{
			byte[][] r = new byte[ data.Length / size ][];
			for ( int i = 0; i < data.Length; i += size )
			{
				r[ i / size ] = new byte[ size ];
				Array.Copy( data, i, r[ i / size ], 0, size );
			}
			return r;
		}

		#endregion

		public PersonalTable( byte[] data, GameVersion format )
		{
			this.Format = format;

			int size = 0;

			switch ( format )
			{
				case GameVersion.XY:
					size = PersonalInfoXY.Size;
					break;
				case GameVersion.ORASDemo:
				case GameVersion.ORAS:
					size = PersonalInfoORAS.Size;
					break;
				case GameVersion.SunMoonDemo:
				case GameVersion.SunMoon:
					size = PersonalInfoSM.SIZE;
					break;
			}

			if ( size == 0 )
			{
				this.Table = null;
			}
			else
			{
				byte[][] entries = PersonalTable.SplitBytes( data, size );
				PersonalInfo[] d = new PersonalInfo[ data.Length / size ];

				switch ( format )
				{
					case GameVersion.XY:
						for ( int i = 0; i < d.Length; i++ )
							d[ i ] = new PersonalInfoXY( entries[ i ] );
						break;
					case GameVersion.ORASDemo:
					case GameVersion.ORAS:
						for ( int i = 0; i < d.Length; i++ )
							d[ i ] = new PersonalInfoORAS( entries[ i ] );
						break;
					case GameVersion.SunMoonDemo:
					case GameVersion.SunMoon:
						for ( int i = 0; i < d.Length; i++ )
							d[ i ] = new PersonalInfoSM( entries[ i ] );
						break;
				}

				this.Table = d;
			}
		}

		public PersonalInfo[] Table { get; }
		public GameVersion Format { get; }

		public PersonalInfo this[ int index ]
		{
			get => index < this.Table.Length ? this.Table[ index ] : this.Table[ 0 ];
			set
			{
				if ( index < this.Table.Length )
					return;
				this.Table[ index ] = value;
			}
		}

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

		public PersonalInfo GetFormeEntry( int species, int forme )
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
			if ( personalEntry < Main.Config.MaxSpeciesID )
				return new[] { personalEntry, 0 };

			for ( int i = 0; i < Main.Config.MaxSpeciesID; i++ )
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