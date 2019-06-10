using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Utility;

namespace PokeRandomizer.Common.Structures.RomFS.PokemonInfo
{
	public class PokemonInfoTable : BaseDataStructure, IEnumerable<PokemonInfo>
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

		#region BaseDataStructure implementation

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

		#endregion

		#region IEnumerable implementation

		public IEnumerator<PokemonInfo> GetEnumerator() => this.Table.AsEnumerable().GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => this.Table.GetEnumerator();

		#endregion

		#region Querying helpers

		public PokemonInfo[] Table { get; private set; }

		public PokemonInfo this[ int index ]
		{
			get => index >= 0 && index < this.Table.Length
					   ? this.Table[ index ]
					   : this.Table[ 0 ];
			set
			{
				if ( index < this.Table.Length )
					return;
				this.Table[ index ] = value;
			}
		}

		public PokemonInfo this[ BaseSpeciesType species ] => this[ species.Id ];

		#endregion

		#region Ability helpers

		public byte[] GetAbilities( BaseSpeciesType species, int forme )
			=> this[ this.GetFormIndex( species, forme ) ].Abilities;

		#endregion

		#region Form helpers

		public ushort GetSpeciesForEntry( int entryId )
		{
			if ( entryId <= this.GameVersion.GetInfo().SpeciesCount )
				return (ushort) entryId;

			var entry = this.Table.FirstOrDefault( e => e.FormeCount > 1 &&
														entryId >= e.FormStatsIndex &&
														entryId < e.FormStatsIndex + e.FormeCount - 1 );

			if ( entry == null )
				return 0;

			return (ushort) Array.IndexOf( this.Table, entry );
		}

		public int GetFormIndex( BaseSpeciesType species, int form )
		{
			return this[ species ].FormIndex( form );
		}

		public PokemonInfo GetFormEntry( BaseSpeciesType species, int form )
		{
			return this[ this.GetFormIndex( species, form ) ];
		}

		public IEnumerable<int> GetAllFormIndices( BaseSpeciesType species )
		{
			var info = this[ species ];

			if ( info.FormeCount <= 1 )
				return new int[ 0 ];

			return Enumerable.Range( 0, info.FormeCount - 1 )
							 .Select( i => this.GetFormIndex( species, i ) );
		}

		public IEnumerable<PokemonInfo> GetAllForms( BaseSpeciesType species )
			=> this.GetAllFormIndices( species ).Select( index => this[ index ] );

		#endregion
	}
}