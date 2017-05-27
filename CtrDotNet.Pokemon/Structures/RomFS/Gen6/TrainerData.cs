using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen6
{
	public class TrainerData
	{
		public bool IsORAS { get; set; }
		public int Format { get; set; }
		public int Class { get; set; }
		public bool Item { get; set; }
		public bool Moves { get; set; }
		public byte BattleType { get; set; }
		public byte NumPokemon { get; set; }
		public byte AI { get; set; }
		public ushort[] Items { get; set; } = new ushort[ 4 ];
		public byte Unused1 { get; set; }
		public byte Unused2 { get; set; }
		public byte Unused3 { get; set; }
		public ushort UnusedORAS { get; set; }
		public bool Healer { get; set; }
		public byte Money { get; set; }
		public ushort Prize { get; set; }
		public Pokemon[] Team { get; set; }

		public TrainerData( byte[] trData, IReadOnlyCollection<byte> trPoke, bool oras )
		{
			using ( BinaryReader br = new BinaryReader( new MemoryStream( trData ) ) )
			{
				this.IsORAS = oras;
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

				// Fetch Team
				this.Team = new Pokemon[ this.NumPokemon ];
				byte[][] teamData = new byte[ this.NumPokemon ][];
				int dataLen = trPoke.Count / this.NumPokemon;
				for ( int i = 0; i < teamData.Length; i++ )
					teamData[ i ] = trPoke.Skip( i * dataLen ).Take( dataLen ).ToArray();
				for ( int i = 0; i < this.NumPokemon; i++ )
					this.Team[ i ] = new Pokemon( teamData[ i ], this.Item, this.Moves );
			}
		}

		public byte[] Write()
		{
			using ( MemoryStream ms = new MemoryStream() )
			using ( BinaryWriter bw = new BinaryWriter( ms ) )
			{
				this.Format = Convert.ToByte( this.Moves ) + ( Convert.ToByte( this.Item ) << 1 );
				if ( this.IsORAS )
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

				return ms.ToArray();
			}
		}

		public byte[] WriteTeam()
		{
			return this.Team.Aggregate( new byte[ 0 ], ( i, pkm ) => i.Concat( pkm.Write( this.Item, this.Moves ) ).ToArray() );
		}

		public class Pokemon
		{
			public byte Vs { get; set; }
			public byte Pid { get; set; }
			public ushort Level { get; set; }
			public ushort Species { get; set; }
			public ushort Form { get; set; }
			public int Ability { get; set; }
			public int Gender { get; set; }
			public int UBit { get; set; }
			public ushort Item { get; set; }
			public ushort[] Moves { get; set; } = new ushort[ 4 ];

			public Pokemon( byte[] data, bool hasItem, bool hasMoves )
			{
				using ( var ms = new MemoryStream( data ) )
				using ( var br = new BinaryReader( ms ) )
				{
					this.Vs = br.ReadByte();
					this.Pid = br.ReadByte();
					this.Level = br.ReadUInt16();
					this.Species = br.ReadUInt16();
					this.Form = br.ReadUInt16();

					this.Ability = this.Pid >> 4;
					this.Gender = this.Pid & 3;
					this.UBit = ( this.Pid >> 3 ) & 1;

					if ( hasItem )
						this.Item = br.ReadUInt16();

					if ( hasMoves )
						for ( int i = 0; i < 4; i++ )
							this.Moves[ i ] = br.ReadUInt16();
				}
			}

			public IEnumerable<byte> Write( bool hasItem, bool hasMoves )
			{
				using ( MemoryStream ms = new MemoryStream() )
				using ( BinaryWriter bw = new BinaryWriter( ms ) )
				{
					this.Pid = (byte) ( ( ( this.Ability & 0b1111 ) << 4 ) |
										( ( this.UBit & 0b1 ) << 3 ) |
										( this.Gender & 0b111 ) );

					bw.Write( this.Vs );
					bw.Write( this.Pid );
					bw.Write( this.Level );
					bw.Write( this.Species );
					bw.Write( this.Form );

					if ( hasItem )
						bw.Write( this.Item );

					if ( hasMoves )
						foreach ( ushort move in this.Moves )
							bw.Write( move );

					return ms.ToArray();
				}
			}
		}
	}
}