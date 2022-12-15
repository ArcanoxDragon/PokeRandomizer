using System;
using System.Linq;

namespace PokeRandomizer.Common.Structures.RomFS.PokemonInfo
{
	// TODO: Convert this whole thing to BaseDataStructure

	public class PokemonInfoXY : PokemonInfo
	{
		public const int Size = 0x40;

		public override void Read(byte[] data)
		{
			if (data.Length != Size)
				return;
			Data = data;

			// Unpack TMHM & Tutors
			TmHm = GetBits(Data.Skip(0x28).Take(0x10).ToArray());
			TypeTutors = GetBits(Data.Skip(0x38).Take(0x4).ToArray());
			// 0x3C-0x40 unknown
		}

		public override byte[] Write()
		{
			SetBits(TmHm).CopyTo(Data, 0x28);
			SetBits(TypeTutors).CopyTo(Data, 0x38);
			return Data;
		}

		public override int HP
		{
			get => Data[0x00];
			set => Data[0x00] = (byte) value;
		}

		public override int Attack
		{
			get => Data[0x01];
			set => Data[0x01] = (byte) value;
		}

		public override int Defense
		{
			get => Data[0x02];
			set => Data[0x02] = (byte) value;
		}

		public override int Speed
		{
			get => Data[0x03];
			set => Data[0x03] = (byte) value;
		}

		public override int SpecialAttack
		{
			get => Data[0x04];
			set => Data[0x04] = (byte) value;
		}

		public override int SpecialDefense
		{
			get => Data[0x05];
			set => Data[0x05] = (byte) value;
		}

		public override byte[] Types
		{
			get => new[] { Data[0x06], Data[0x07] };
			set
			{
				if (value?.Length != 2)
					return;

				Data[0x06] = value[0];
				Data[0x07] = value[1];
			}
		}

		public override int CatchRate
		{
			get => Data[0x08];
			set => Data[0x08] = (byte) value;
		}

		public override int EvoStage
		{
			get => Data[0x09];
			set => Data[0x09] = (byte) value;
		}

		private int EvYield
		{
			get => BitConverter.ToUInt16(Data, 0x0A);
			set => BitConverter.GetBytes((ushort) value).CopyTo(Data, 0x0A);
		}

		public override int EvHP
		{
			get => EvYield >> 0 & 0x3;
			set => EvYield = ( EvYield & ~( 0x3 << 0 ) ) | ( value & 0x3 ) << 0;
		}

		public override int EvAttack
		{
			get => EvYield >> 2 & 0x3;
			set => EvYield = ( EvYield & ~( 0x3 << 2 ) ) | ( value & 0x3 ) << 2;
		}

		public override int EvDefense
		{
			get => EvYield >> 4 & 0x3;
			set => EvYield = ( EvYield & ~( 0x3 << 4 ) ) | ( value & 0x3 ) << 4;
		}

		public override int EvSpeed
		{
			get => EvYield >> 6 & 0x3;
			set => EvYield = ( EvYield & ~( 0x3 << 6 ) ) | ( value & 0x3 ) << 6;
		}

		public override int EvSpecialAttack
		{
			get => EvYield >> 8 & 0x3;
			set => EvYield = ( EvYield & ~( 0x3 << 8 ) ) | ( value & 0x3 ) << 8;
		}

		public override int EvSpecialDefense
		{
			get => EvYield >> 10 & 0x3;
			set => EvYield = ( EvYield & ~( 0x3 << 10 ) ) | ( value & 0x3 ) << 10;
		}

		public override short[] Items
		{
			get => new[] { BitConverter.ToInt16(Data, 0xC), BitConverter.ToInt16(Data, 0xE), BitConverter.ToInt16(Data, 0x10) };
			set
			{
				if (value?.Length != 3)
					return;
				BitConverter.GetBytes(value[0]).CopyTo(Data, 0xC);
				BitConverter.GetBytes(value[1]).CopyTo(Data, 0xE);
				BitConverter.GetBytes(value[2]).CopyTo(Data, 0x10);
			}
		}

		public override int Gender
		{
			get => Data[0x12];
			set => Data[0x12] = (byte) value;
		}

		public override int HatchCycles
		{
			get => Data[0x13];
			set => Data[0x13] = (byte) value;
		}

		public override int BaseFriendship
		{
			get => Data[0x14];
			set => Data[0x14] = (byte) value;
		}

		public override int ExpGrowth
		{
			get => Data[0x15];
			set => Data[0x15] = (byte) value;
		}

		public override byte[] EggGroups
		{
			get => new[] { Data[0x16], Data[0x17] };
			set
			{
				if (value?.Length != 2)
					return;
				Data[0x16] = value[0];
				Data[0x17] = value[1];
			}
		}

		public override byte[] Abilities
		{
			get => new[] { Data[0x18], Data[0x19], Data[0x1A] };
			set
			{
				if (value?.Length != 3)
					return;

				Data[0x18] = value[0];
				Data[0x19] = value[1];
				Data[0x1A] = value[2];
			}
		}

		public override int EscapeRate
		{
			get => Data[0x1B];
			set => Data[0x1B] = (byte) value;
		}

		protected internal override int FormStatsIndex
		{
			get => BitConverter.ToUInt16(Data, 0x1C);
			set => BitConverter.GetBytes((ushort) value).CopyTo(Data, 0x1C);
		}

		public override int FormeSprite
		{
			get => BitConverter.ToUInt16(Data, 0x1E);
			set => BitConverter.GetBytes((ushort) value).CopyTo(Data, 0x1E);
		}

		public override int FormeCount
		{
			get => Data[0x20];
			set => Data[0x20] = (byte) value;
		}

		public override int Color
		{
			get => Data[0x21];
			set => Data[0x21] = (byte) value;
		}

		public override int BaseExp
		{
			get => BitConverter.ToUInt16(Data, 0x22);
			set => BitConverter.GetBytes((ushort) value).CopyTo(Data, 0x22);
		}

		public override int Height
		{
			get => BitConverter.ToUInt16(Data, 0x24);
			set => BitConverter.GetBytes((ushort) value).CopyTo(Data, 0x24);
		}

		public override int Weight
		{
			get => BitConverter.ToUInt16(Data, 0x26);
			set => BitConverter.GetBytes((ushort) value).CopyTo(Data, 0x26);
		}
	}
}