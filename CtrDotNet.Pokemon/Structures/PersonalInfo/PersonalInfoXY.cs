using System;
using System.Linq;

namespace CtrDotNet.Pokemon.Structures.PersonalInfo
{
	public class PersonalInfoXY : PersonalInfo
	{
		public const int Size = 0x40;

		protected PersonalInfoXY() { } // For ORAS

		public PersonalInfoXY( byte[] data )
		{
			if ( data.Length != Size )
				return;
			this.data = data;

			// Unpack TMHM & Tutors
			this.TMHM = PersonalInfo.GetBits( this.data.Skip( 0x28 ).Take( 0x10 ).ToArray() );
			this.TypeTutors = PersonalInfo.GetBits( this.data.Skip( 0x38 ).Take( 0x4 ).ToArray() );
			// 0x3C-0x40 unknown
		}

		public override byte[] Write()
		{
			PersonalInfo.SetBits( this.TMHM ).CopyTo( this.data, 0x28 );
			PersonalInfo.SetBits( this.TypeTutors ).CopyTo( this.data, 0x38 );
			return this.data;
		}

		public override int HP
		{
			get => this.data[ 0x00 ];
			set => this.data[ 0x00 ] = (byte) value;
		}

		public override int Attack
		{
			get => this.data[ 0x01 ];
			set => this.data[ 0x01 ] = (byte) value;
		}

		public override int Defense
		{
			get => this.data[ 0x02 ];
			set => this.data[ 0x02 ] = (byte) value;
		}

		public override int Speed
		{
			get => this.data[ 0x03 ];
			set => this.data[ 0x03 ] = (byte) value;
		}

		public override int SpecialAttack
		{
			get => this.data[ 0x04 ];
			set => this.data[ 0x04 ] = (byte) value;
		}

		public override int SpecialDefense
		{
			get => this.data[ 0x05 ];
			set => this.data[ 0x05 ] = (byte) value;
		}

		public override int[] Types
		{
			get => new int[] { this.data[ 0x06 ], this.data[ 0x07 ] };
			set
			{
				if ( value?.Length != 2 )
					return;
				this.data[ 0x06 ] = (byte) value[ 0 ];
				this.data[ 0x07 ] = (byte) value[ 1 ];
			}
		}

		public override int CatchRate
		{
			get => this.data[ 0x08 ];
			set => this.data[ 0x08 ] = (byte) value;
		}

		public override int EvoStage
		{
			get => this.data[ 0x09 ];
			set => this.data[ 0x09 ] = (byte) value;
		}

		private int EvYield
		{
			get => BitConverter.ToUInt16( this.data, 0x0A );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.data, 0x0A );
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

		public override int[] Items
		{
			get => new int[] { BitConverter.ToInt16( this.data, 0xC ), BitConverter.ToInt16( this.data, 0xE ), BitConverter.ToInt16( this.data, 0x10 ) };
			set
			{
				if ( value?.Length != 3 )
					return;
				BitConverter.GetBytes( (short) value[ 0 ] ).CopyTo( this.data, 0xC );
				BitConverter.GetBytes( (short) value[ 1 ] ).CopyTo( this.data, 0xE );
				BitConverter.GetBytes( (short) value[ 2 ] ).CopyTo( this.data, 0x10 );
			}
		}

		public override int Gender
		{
			get => this.data[ 0x12 ];
			set => this.data[ 0x12 ] = (byte) value;
		}

		public override int HatchCycles
		{
			get => this.data[ 0x13 ];
			set => this.data[ 0x13 ] = (byte) value;
		}

		public override int BaseFriendship
		{
			get => this.data[ 0x14 ];
			set => this.data[ 0x14 ] = (byte) value;
		}

		public override int ExpGrowth
		{
			get => this.data[ 0x15 ];
			set => this.data[ 0x15 ] = (byte) value;
		}

		public override int[] EggGroups
		{
			get => new int[] { this.data[ 0x16 ], this.data[ 0x17 ] };
			set
			{
				if ( value?.Length != 2 )
					return;
				this.data[ 0x16 ] = (byte) value[ 0 ];
				this.data[ 0x17 ] = (byte) value[ 1 ];
			}
		}

		public override int[] Abilities
		{
			get => new int[] { this.data[ 0x18 ], this.data[ 0x19 ], this.data[ 0x1A ] };
			set
			{
				if ( value?.Length != 3 )
					return;
				this.data[ 0x18 ] = (byte) value[ 0 ];
				this.data[ 0x19 ] = (byte) value[ 1 ];
				this.data[ 0x1A ] = (byte) value[ 2 ];
			}
		}

		public override int EscapeRate
		{
			get => this.data[ 0x1B ];
			set => this.data[ 0x1B ] = (byte) value;
		}

		protected internal override int FormStatsIndex
		{
			get => BitConverter.ToUInt16( this.data, 0x1C );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.data, 0x1C );
		}

		public override int FormeSprite
		{
			get => BitConverter.ToUInt16( this.data, 0x1E );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.data, 0x1E );
		}

		public override int FormeCount
		{
			get => this.data[ 0x20 ];
			set => this.data[ 0x20 ] = (byte) value;
		}

		public override int Color
		{
			get => this.data[ 0x21 ];
			set => this.data[ 0x21 ] = (byte) value;
		}

		public override int BaseExp
		{
			get => BitConverter.ToUInt16( this.data, 0x22 );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.data, 0x22 );
		}

		public override int Height
		{
			get => BitConverter.ToUInt16( this.data, 0x24 );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.data, 0x24 );
		}

		public override int Weight
		{
			get => BitConverter.ToUInt16( this.data, 0x26 );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.data, 0x26 );
		}
	}
}