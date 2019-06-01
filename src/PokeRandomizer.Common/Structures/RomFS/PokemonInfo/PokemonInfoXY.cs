using System;
using System.Linq;

namespace PokeRandomizer.Common.Structures.RomFS.PokemonInfo
{
	// TODO: Convert this whole thing to BaseDataStructure

	public class PokemonInfoXY : PokemonInfo
	{
		public const int Size = 0x40;

		public override void Read( byte[] data )
		{
			if ( data.Length != Size )
				return;
			this.Data = data;

			// Unpack TMHM & Tutors
			this.TmHm = PokemonInfo.GetBits( this.Data.Skip( 0x28 ).Take( 0x10 ).ToArray() );
			this.TypeTutors = PokemonInfo.GetBits( this.Data.Skip( 0x38 ).Take( 0x4 ).ToArray() );
			// 0x3C-0x40 unknown
		}

		public override byte[] Write()
		{
			PokemonInfo.SetBits( this.TmHm ).CopyTo( this.Data, 0x28 );
			PokemonInfo.SetBits( this.TypeTutors ).CopyTo( this.Data, 0x38 );
			return this.Data;
		}

		public override int HP
		{
			get => this.Data[ 0x00 ];
			set => this.Data[ 0x00 ] = (byte) value;
		}

		public override int Attack
		{
			get => this.Data[ 0x01 ];
			set => this.Data[ 0x01 ] = (byte) value;
		}

		public override int Defense
		{
			get => this.Data[ 0x02 ];
			set => this.Data[ 0x02 ] = (byte) value;
		}

		public override int Speed
		{
			get => this.Data[ 0x03 ];
			set => this.Data[ 0x03 ] = (byte) value;
		}

		public override int SpecialAttack
		{
			get => this.Data[ 0x04 ];
			set => this.Data[ 0x04 ] = (byte) value;
		}

		public override int SpecialDefense
		{
			get => this.Data[ 0x05 ];
			set => this.Data[ 0x05 ] = (byte) value;
		}

		public override byte[] Types
		{
			get => new[] { this.Data[ 0x06 ], this.Data[ 0x07 ] };
			set
			{
				if ( value?.Length != 2 )
					return;

				this.Data[ 0x06 ] = value[ 0 ];
				this.Data[ 0x07 ] = value[ 1 ];
			}
		}

		public override int CatchRate
		{
			get => this.Data[ 0x08 ];
			set => this.Data[ 0x08 ] = (byte) value;
		}

		public override int EvoStage
		{
			get => this.Data[ 0x09 ];
			set => this.Data[ 0x09 ] = (byte) value;
		}

		private int EvYield
		{
			get => BitConverter.ToUInt16( this.Data, 0x0A );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.Data, 0x0A );
		}

		public override int EvHP
		{
			get => this.EvYield >> 0 & 0x3;
			set => this.EvYield = ( this.EvYield & ~( 0x3 << 0 ) ) | ( value & 0x3 ) << 0;
		}

		public override int EvAttack
		{
			get => this.EvYield >> 2 & 0x3;
			set => this.EvYield = ( this.EvYield & ~( 0x3 << 2 ) ) | ( value & 0x3 ) << 2;
		}

		public override int EvDefense
		{
			get => this.EvYield >> 4 & 0x3;
			set => this.EvYield = ( this.EvYield & ~( 0x3 << 4 ) ) | ( value & 0x3 ) << 4;
		}

		public override int EvSpeed
		{
			get => this.EvYield >> 6 & 0x3;
			set => this.EvYield = ( this.EvYield & ~( 0x3 << 6 ) ) | ( value & 0x3 ) << 6;
		}

		public override int EvSpecialAttack
		{
			get => this.EvYield >> 8 & 0x3;
			set => this.EvYield = ( this.EvYield & ~( 0x3 << 8 ) ) | ( value & 0x3 ) << 8;
		}

		public override int EvSpecialDefense
		{
			get => this.EvYield >> 10 & 0x3;
			set => this.EvYield = ( this.EvYield & ~( 0x3 << 10 ) ) | ( value & 0x3 ) << 10;
		}

		public override short[] Items
		{
			get => new[] { BitConverter.ToInt16( this.Data, 0xC ), BitConverter.ToInt16( this.Data, 0xE ), BitConverter.ToInt16( this.Data, 0x10 ) };
			set
			{
				if ( value?.Length != 3 )
					return;
				BitConverter.GetBytes( value[ 0 ] ).CopyTo( this.Data, 0xC );
				BitConverter.GetBytes( value[ 1 ] ).CopyTo( this.Data, 0xE );
				BitConverter.GetBytes( value[ 2 ] ).CopyTo( this.Data, 0x10 );
			}
		}

		public override int Gender
		{
			get => this.Data[ 0x12 ];
			set => this.Data[ 0x12 ] = (byte) value;
		}

		public override int HatchCycles
		{
			get => this.Data[ 0x13 ];
			set => this.Data[ 0x13 ] = (byte) value;
		}

		public override int BaseFriendship
		{
			get => this.Data[ 0x14 ];
			set => this.Data[ 0x14 ] = (byte) value;
		}

		public override int ExpGrowth
		{
			get => this.Data[ 0x15 ];
			set => this.Data[ 0x15 ] = (byte) value;
		}

		public override byte[] EggGroups
		{
			get => new[] { this.Data[ 0x16 ], this.Data[ 0x17 ] };
			set
			{
				if ( value?.Length != 2 )
					return;
				this.Data[ 0x16 ] = value[ 0 ];
				this.Data[ 0x17 ] = value[ 1 ];
			}
		}

		public override byte[] Abilities
		{
			get => new [] { this.Data[ 0x18 ], this.Data[ 0x19 ], this.Data[ 0x1A ] };
			set
			{
				if ( value?.Length != 3 )
					return;

				this.Data[ 0x18 ] = value[ 0 ];
				this.Data[ 0x19 ] = value[ 1 ];
				this.Data[ 0x1A ] = value[ 2 ];
			}
		}

		public override int EscapeRate
		{
			get => this.Data[ 0x1B ];
			set => this.Data[ 0x1B ] = (byte) value;
		}

		protected internal override int FormStatsIndex
		{
			get => BitConverter.ToUInt16( this.Data, 0x1C );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.Data, 0x1C );
		}

		public override int FormeSprite
		{
			get => BitConverter.ToUInt16( this.Data, 0x1E );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.Data, 0x1E );
		}

		public override int FormeCount
		{
			get => this.Data[ 0x20 ];
			set => this.Data[ 0x20 ] = (byte) value;
		}

		public override int Color
		{
			get => this.Data[ 0x21 ];
			set => this.Data[ 0x21 ] = (byte) value;
		}

		public override int BaseExp
		{
			get => BitConverter.ToUInt16( this.Data, 0x22 );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.Data, 0x22 );
		}

		public override int Height
		{
			get => BitConverter.ToUInt16( this.Data, 0x24 );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.Data, 0x24 );
		}

		public override int Weight
		{
			get => BitConverter.ToUInt16( this.Data, 0x26 );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.Data, 0x26 );
		}
	}
}