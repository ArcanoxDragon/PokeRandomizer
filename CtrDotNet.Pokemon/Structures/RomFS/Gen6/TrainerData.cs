using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen6
{
	public class TrainerData : BaseDataStructure
	{
		public TrainerData( GameVersion gameVersion ) : base( gameVersion ) { }

		public int Format { get; set; }
		public int Class { get; set; }
		public bool Item { get; set; }
		public bool Moves { get; set; }
		public byte BattleType { get; set; }
		public byte NumPokemon { get; set; }
		public byte AI { get; set; }
		public ushort[] Items { get; set; }
		public byte Unused1 { get; set; }
		public byte Unused2 { get; set; }
		public byte Unused3 { get; set; }
		public ushort UnusedORAS { get; set; }
		public bool Healer { get; set; }
		public byte Money { get; set; }
		public ushort Prize { get; set; }
		public Pokemon[] Team { get; set; }

		public void ReadTeam( byte[] trainerPokeData )
		{
			// Fetch Team
			this.Team = new Pokemon[ this.NumPokemon ];
			byte[][] teamData = trainerPokeData.ToArray().Partition( trainerPokeData.Length / this.NumPokemon );

			for ( int i = 0; i < this.NumPokemon; i++ )
			{
				Pokemon pkm = new Pokemon( this.GameVersion, this.Item, this.Moves );
				pkm.Read( teamData[ i ] );
				this.Team[ i ] = pkm;
			}
		}

		public IEnumerable<byte> WriteTeam() => this.Team.Aggregate( new byte[ 0 ].AsEnumerable(), ( i, pkm ) => {
			pkm.HasItem = this.Item;
			pkm.HasMoves = this.Moves;
			return i.Concat( pkm.Write() );
		} );

		protected override void ReadData( BinaryReader br )
		{
			bool oras = this.GameVersion.IsORAS();
			this.Items = new ushort[ 4 ];

			this.Format = oras ? br.ReadUInt16() : br.ReadByte();
			this.Class = oras ? br.ReadUInt16() : br.ReadByte();

			if ( oras )
				this.UnusedORAS = br.ReadUInt16();

			this.Item = ( ( this.Format >> 1 ) & 1 ) == 1;
			this.Moves = ( this.Format & 1 ) == 1;
			this.BattleType = br.ReadByte();
			this.NumPokemon = br.ReadByte();

			for ( int i = 0; i < 4; i++ )
				this.Items[ i ] = br.ReadUInt16();

			this.AI = br.ReadByte();
			this.Unused1 = br.ReadByte();
			this.Unused2 = br.ReadByte();
			this.Unused3 = br.ReadByte();
			this.Healer = br.ReadByte() != 0;
			this.Money = br.ReadByte();
			this.Prize = br.ReadUInt16();
		}

		protected override void WriteData( BinaryWriter bw )
		{
			this.Format = Convert.ToByte( this.Moves ) + ( Convert.ToByte( this.Item ) << 1 );

			if ( this.GameVersion.IsORAS() )
			{
				bw.Write( (ushort) this.Format );
				bw.Write( (ushort) this.Class );
				bw.Write( (ushort) 0 );
			}
			else
			{
				bw.Write( (byte) this.Format );
				bw.Write( (byte) this.Class );
			}

			bw.Write( this.BattleType );
			bw.Write( this.NumPokemon );
			bw.Write( this.Items[ 0 ] );
			bw.Write( this.Items[ 1 ] );
			bw.Write( this.Items[ 2 ] );
			bw.Write( this.Items[ 3 ] );

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
				this.HasItem = hasItem;
				this.HasMoves = hasMoves;
			}

			public bool HasItem { get; set; }
			public bool HasMoves { get; set; }

			public byte Vs { get; set; }
			public byte Pid { get; set; }
			public ushort Level { get; set; }
			public ushort Species { get; set; }
			public ushort Form { get; set; }
			public int Ability { get; set; }
			public int Gender { get; set; }
			public int UBit { get; set; }
			public ushort Item { get; set; }
			public ushort[] Moves { get; set; }

			protected override void ReadData( BinaryReader br )
			{
				this.Moves = new ushort[ 4 ];

				this.Vs = br.ReadByte();
				this.Pid = br.ReadByte();
				this.Level = br.ReadUInt16();
				this.Species = br.ReadUInt16();
				this.Form = br.ReadUInt16();

				this.Ability = this.Pid >> 4;
				this.Gender = this.Pid & 3;
				this.UBit = ( this.Pid >> 3 ) & 1;

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
									( this.Gender & 0b111 ) );

				bw.Write( this.Vs );
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