using System;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen7
{
	public class EncounterTrade : Common.EncounterStatic
	{
		public const int Size = 0x34;

		private readonly byte[] data;

		public EncounterTrade( byte[] data )
		{
			this.data = data;
		}

		public override ushort Species
		{
			get => BitConverter.ToUInt16( this.data, 0x0 );
			set => BitConverter.GetBytes( value ).CopyTo( this.data, 0x0 );
		}

		public int Form
		{
			get => this.data[ 0x4 ];
			set => this.data[ 0x4 ] = (byte) value;
		}

		public int Level
		{
			get => this.data[ 0x5 ];
			set => this.data[ 0x5 ] = (byte) value;
		}

		// ReSharper disable once InconsistentNaming
		public int[] IVs
		{
			get => new int[] {
				(sbyte) this.data[ 0x6 ], (sbyte) this.data[ 0x7 ], (sbyte) this.data[ 0x8 ], (sbyte) this.data[ 0x9 ], (sbyte) this.data[ 0xA ], (sbyte) this.data[ 0xB ]
			};
			set
			{
				if ( value.Length != 6 )
					return;
				for ( int i = 0; i < 6; i++ )
					this.data[ i + 0x6 ] = (byte) Convert.ToSByte( value[ i ] );
			}
		}

		public int Ability
		{
			get => this.data[ 0xC ];
			set => this.data[ 0xC ] = (byte) value;
		}

		public int Nature
		{
			get => this.data[ 0xD ];
			set => this.data[ 0xD ] = (byte) value;
		}

		public int Gender
		{
			get => this.data[ 0xE ];
			set => this.data[ 0xE ] = (byte) value;
		}

		public int TID
		{
			get => BitConverter.ToUInt16( this.data, 0x10 );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.data, 0x10 );
		}

		public int SID
		{
			get => BitConverter.ToUInt16( this.data, 0x12 );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.data, 0x12 );
		}

		public uint ID
		{
			get => BitConverter.ToUInt32( this.data, 0x10 );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.data, 0x10 );
		}

		public override int HeldItem
		{
			get
			{
				int item = BitConverter.ToInt16( this.data, 0x14 );
				if ( item < 0 )
					item = 0;
				return item;
			}
			set
			{
				if ( value == 0 )
					value = -1;
				BitConverter.GetBytes( (short) value ).CopyTo( this.data, 0x14 );
			}
		}

		public int TrainerGender
		{
			get => this.data[ 0x1A ];
			set => this.data[ 0x1A ] = (byte) value;
		}

		public int TradeRequestSpecies
		{
			get => BitConverter.ToUInt16( this.data, 0x2C );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.data, 0x2C );
		}
	}
}