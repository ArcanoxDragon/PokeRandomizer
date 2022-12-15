using System;
using System.Linq;

namespace PokeRandomizer.Common.Structures.RomFS.PokemonInfo
{
	public class PokemonInfoSM : PokemonInfoXY
	{
		public new const int Size = 0x54;

		public override void Read(byte[] data)
		{
			if (data.Length != Size)
				return;
			Data = data;

			TmHm = GetBits(Data.Skip(0x28).Take(0x10).ToArray());      // 36-39
			TypeTutors = GetBits(Data.Skip(0x38).Take(0x4).ToArray()); // 40
		}

		public override byte[] Write()
		{
			SetBits(TmHm).CopyTo(Data, 0x28);
			SetBits(TypeTutors).CopyTo(Data, 0x38);
			return Data;
		}

		// No accessing for 3C-4B

		public int SpecialZItem
		{
			get => BitConverter.ToUInt16(Data, 0x4C);
			set => BitConverter.GetBytes((ushort) value).CopyTo(Data, 0x4C);
		}

		public int SpecialZBaseMove
		{
			get => BitConverter.ToUInt16(Data, 0x4E);
			set => BitConverter.GetBytes((ushort) value).CopyTo(Data, 0x4E);
		}

		public int SpecialZzMove
		{
			get => BitConverter.ToUInt16(Data, 0x50);
			set => BitConverter.GetBytes((ushort) value).CopyTo(Data, 0x50);
		}

		public bool LocalVariant
		{
			get => Data[0x52] == 1;
			set => Data[0x52] = (byte) ( value ? 1 : 0 );
		}
	}
}