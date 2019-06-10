using System;
using System.IO;
using System.Linq;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Utility;

namespace PokeRandomizer.Common.Structures.RomFS.Gen6
{
	public class TrainerData : BaseDataStructure
	{
		public const int BattleTypeSingle = 0;
		public const int BattleTypeDouble = 1;

		public TrainerData( GameVersion gameVersion ) : base( gameVersion ) { }

		public bool      PartialEntry { get; private set; }
		public int       Format       { get; set; }
		public int       Class        { get; set; }
		public bool      Item         { get; set; }
		public bool      Moves        { get; set; }
		public byte      BattleType   { get; set; }
		public byte      NumPokemon   { get; set; }
		public byte      AI           { get; set; }
		public ushort[]  Items        { get; private set; }
		public byte      Unused1      { get; set; }
		public byte      Unused2      { get; set; }
		public byte      Unused3      { get; set; }
		public ushort    UnusedORAS   { get; set; }
		public bool      Healer       { get; set; }
		public byte      Money        { get; set; }
		public ushort    Prize        { get; set; }
		public Pokemon[] Team         { get; set; }

		public void ReadTeam( byte[] trainerPokeData )
		{
			// Fetch Team
			this.Team = new Pokemon[ this.NumPokemon ];

			if ( this.NumPokemon == 0 )
				return;

			byte[][] teamData = trainerPokeData.ToArray().Partition( trainerPokeData.Length / this.NumPokemon );

			for ( int i = 0; i < this.NumPokemon; i++ )
			{
				Pokemon pkm = new Pokemon( this.GameVersion, this.Item, this.Moves );
				pkm.Read( teamData[ i ] );
				this.Team[ i ] = pkm;
			}
		}

		public byte[] WriteTeam() => this.Team.Length == 0
										 ? new byte[ 6 ]
										 : this.Team.Aggregate( new byte[ 0 ].AsEnumerable(), ( data, pkm ) => {
											 pkm.HasItem  = this.Item;
											 pkm.HasMoves = this.Moves;
											 return data.Concat( pkm.Write() );
										 } ).ToArray();

		protected override void ReadData( BinaryReader br )
		{
			var oras = this.GameVersion.IsORAS();

			if ( oras )
				this.PartialEntry = br.BaseStream.Length < 24;
			else
				this.PartialEntry = br.BaseStream.Length < 20;

			this.Items = new ushort[ 4 ];

			if ( oras )
			{
				this.Format     = br.ReadUInt16();
				this.Class      = br.ReadUInt16();
				this.UnusedORAS = br.ReadUInt16();
			}
			else
			{
				this.Format = br.ReadByte();
				this.Class  = br.ReadByte();
			}

			this.Item       = ( ( this.Format >> 1 ) & 1 ) == 1;
			this.Moves      = ( this.Format & 1 ) == 1;
			this.BattleType = br.ReadByte();
			this.NumPokemon = br.ReadByte();

			for ( int i = 0; i < 4; i++ )
				this.Items[ i ] = br.ReadUInt16();

			if ( this.PartialEntry )
				return;

			this.AI      = br.ReadByte();
			this.Unused1 = br.ReadByte();
			this.Unused2 = br.ReadByte();
			this.Unused3 = br.ReadByte();
			this.Healer  = br.ReadByte() != 0;
			this.Money   = br.ReadByte();
			this.Prize   = br.ReadUInt16();
		}

		protected override void WriteData( BinaryWriter bw )
		{
			this.Format = Convert.ToByte( this.Moves ) + ( Convert.ToByte( this.Item ) << 1 );

			if ( this.GameVersion.IsORAS() )
			{
				bw.Write( (ushort) this.Format );
				bw.Write( (ushort) this.Class );
				bw.Write( this.UnusedORAS );
			}
			else
			{
				bw.Write( (byte) this.Format );
				bw.Write( (byte) this.Class );
			}

			bw.Write( this.BattleType );
			bw.Write( this.NumPokemon );
			this.Items.ForEach( bw.Write );

			if ( this.PartialEntry )
				return;

			bw.Write( this.AI );
			bw.Write( this.Unused1 );
			bw.Write( this.Unused2 );
			bw.Write( this.Unused3 );
			bw.Write( Convert.ToByte( this.Healer ) );
			bw.Write( this.Money );
			bw.Write( this.Prize );
		}

		public class Pokemon : BaseDataStructure
		{
			public Pokemon( GameVersion gameVersion, bool hasItem, bool hasMoves ) : base( gameVersion )
			{
				this.HasItem  = hasItem;
				this.HasMoves = hasMoves;
			}

			public bool HasItem  { get; set; }
			public bool HasMoves { get; set; }

			// ReSharper disable once InconsistentNaming
			public byte IVs { get; set; }

			public byte     Pid     { get; set; }
			public ushort   Level   { get; set; }
			public ushort   Species { get; set; }
			public ushort   Form    { get; set; }
			public int      Ability { get; set; }
			public int      Gender  { get; set; }
			public int      UBit    { get; set; }
			public ushort   Item    { get; set; }
			public ushort[] Moves   { get; set; }

			protected override void ReadData( BinaryReader br )
			{
				this.Moves = new ushort[ 4 ];

				this.IVs     = br.ReadByte();
				this.Pid     = br.ReadByte();
				this.Level   = br.ReadUInt16();
				this.Species = br.ReadUInt16();
				this.Form    = br.ReadUInt16();

				this.Ability = this.Pid >> 4;
				this.Gender  = this.Pid & 3;
				this.UBit    = ( this.Pid >> 3 ) & 1;

				if ( this.HasItem )
					this.Item = br.ReadUInt16();

				if ( this.HasMoves )
					for ( int i = 0; i < 4; i++ )
						this.Moves[ i ] = br.ReadUInt16();
			}

			protected override void WriteData( BinaryWriter bw )
			{
				this.Pid = (byte) ( ( ( this.Ability & 0b1111 ) << 4 ) |
									( ( this.UBit & 0b1 ) << 3 ) |
									( this.Gender & 0b11 ) );

				bw.Write( this.IVs );
				bw.Write( this.Pid );
				bw.Write( this.Level );
				bw.Write( this.Species );
				bw.Write( this.Form );

				if ( this.HasItem )
					bw.Write( this.Item );

				if ( this.HasMoves )
					foreach ( ushort move in this.Moves )
						bw.Write( move );
			}
		}
	}
}