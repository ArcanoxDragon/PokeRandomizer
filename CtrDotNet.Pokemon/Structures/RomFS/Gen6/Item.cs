using System.IO;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen6
{
	public class Item
	{
		public ushort Price { get; }
		public int BuyPrice { get; }
		public int SellPrice { get; }
		public byte HeldEffect { get; }
		public byte HeldArgument { get; }
		public byte NaturalGiftEffect { get; }
		public byte FlingEffect { get; }
		public byte FlingPower { get; }
		public byte NaturalGiftPower { get; }
		public byte NaturalGiftType { get; }
		public byte U8Flags { get; }
		public byte KeyFlags { get; }
		public byte UseEffect { get; }
		public byte BattleType { get; } // Battle Type
		public byte UnusedC { get; } // 0 or 1
		public byte Classification { get; } // Classification (0-3 Battle, 4 Balls, 5 Mail)
		public byte Consumable { get; }
		public byte SortIndex { get; }
		public byte CureInflict { get; } // Bitflags
		public byte FieldEffect { get; } // Revive 1, Sacred Ash 3, Rare Candy 5, EvoStone 8
		public int BoostAttack { get; }
		public int BoostDefense { get; }
		public int BoostSpeed { get; }
		public int BoostSpecialAttack { get; }
		public int BoostSpecialDefense { get; }
		public int BoostAccuracy { get; }
		public int BoostCrit { get; }
		public int BoostPp { get; }
		public ushort FunctionFlags { get; }
		public byte Evhp { get; }
		public byte EvAttack { get; }
		public byte EvDefense { get; }
		public byte EvSpeed { get; }
		public byte EvSpecialAttack { get; }
		public byte EvSpecialDefense { get; }
		public Heal Healing { get; }
		public byte PpGain { get; }
		public byte Friendship1 { get; }
		public byte Friendship2 { get; }
		public byte Friendship3 { get; }
		public byte Unused23 { get; }
		public byte Unused24 { get; }

		public Item( byte[] data )
		{
			using ( BinaryReader br = new BinaryReader( new MemoryStream( data ) ) )
			{
				this.Price = br.ReadUInt16();
				this.BuyPrice = this.Price * 10;
				this.SellPrice = this.Price * 5;

				this.HeldEffect = br.ReadByte();
				this.HeldArgument = br.ReadByte();
				this.NaturalGiftEffect = br.ReadByte();
				this.FlingEffect = br.ReadByte();
				this.FlingPower = br.ReadByte();
				this.NaturalGiftPower = br.ReadByte();
				this.NaturalGiftType = br.ReadByte();
				this.U8Flags = (byte) ( this.NaturalGiftType >> 5 );
				this.NaturalGiftType &= 0x1F;
				this.KeyFlags = br.ReadByte();
				this.UseEffect = br.ReadByte();
				this.BattleType = br.ReadByte();
				this.UnusedC = br.ReadByte();
				this.Classification = br.ReadByte();
				this.Consumable = br.ReadByte();
				this.SortIndex = br.ReadByte();
				this.CureInflict = br.ReadByte();

				this.FieldEffect = br.ReadByte();
				this.BoostAttack = this.FieldEffect >> 4;
				this.FieldEffect &= 0xF;
				this.BoostDefense = br.ReadByte();
				this.BoostSpecialAttack = this.BoostDefense >> 4;
				this.BoostDefense &= 0xF;
				this.BoostSpecialDefense = br.ReadByte();
				this.BoostSpeed = this.BoostSpecialDefense >> 4;
				this.BoostSpecialDefense &= 0xF;
				this.BoostAccuracy = br.ReadByte();
				this.BoostCrit = ( this.BoostAccuracy >> 4 ) & 0x3;
				this.BoostPp = this.BoostAccuracy >> 6;
				this.BoostAccuracy &= 0xF;

				this.FunctionFlags = br.ReadUInt16();

				this.Evhp = br.ReadByte();
				this.EvAttack = br.ReadByte();
				this.EvDefense = br.ReadByte();
				this.EvSpeed = br.ReadByte();
				this.EvSpecialAttack = br.ReadByte();
				this.EvSpecialDefense = br.ReadByte();

				this.Healing = new Heal( br.ReadByte() );
				this.PpGain = br.ReadByte();
				this.Friendship1 = br.ReadByte();
				this.Friendship2 = br.ReadByte();
				this.Friendship3 = br.ReadByte();
				this.Unused23 = br.ReadByte();
				this.Unused24 = br.ReadByte();
			}
		}

		public byte[] Write()
		{
			using ( MemoryStream ms = new MemoryStream() )
			using ( BinaryWriter bw = new BinaryWriter( ms ) )
			{
				bw.Write( this.Price );
				bw.Write( this.HeldEffect );
				bw.Write( this.HeldArgument );
				bw.Write( this.NaturalGiftEffect );
				bw.Write( this.FlingEffect );
				bw.Write( this.FlingPower );
				bw.Write( this.NaturalGiftPower );
				bw.Write( (byte) ( this.NaturalGiftType | ( this.U8Flags << 5 ) ) );
				bw.Write( this.KeyFlags );
				bw.Write( this.UseEffect );
				bw.Write( this.BattleType );
				bw.Write( this.UnusedC );
				bw.Write( this.Classification );
				bw.Write( this.Consumable );
				bw.Write( this.SortIndex );
				bw.Write( this.CureInflict );

				bw.Write( (byte) ( this.FieldEffect | ( this.BoostAttack << 4 ) ) );
				bw.Write( (byte) ( this.BoostDefense | ( this.BoostSpecialAttack << 4 ) ) );
				bw.Write( (byte) ( this.BoostSpecialDefense | ( this.BoostSpeed << 4 ) ) );
				bw.Write( (byte) ( this.BoostAccuracy | ( this.BoostCrit << 4 ) | ( this.BoostPp << 6 ) ) );

				bw.Write( this.FunctionFlags );

				bw.Write( this.Evhp );
				bw.Write( this.EvAttack );
				bw.Write( this.EvDefense );
				bw.Write( this.EvSpeed );
				bw.Write( this.EvSpecialAttack );
				bw.Write( this.EvSpecialDefense );
				bw.Write( this.Healing.Write() );
				bw.Write( this.PpGain );
				bw.Write( this.Friendship1 );
				bw.Write( this.Friendship2 );
				bw.Write( this.Friendship3 );

				bw.Write( this.Unused23 );
				bw.Write( this.Unused24 );

				return ms.ToArray();
			}
		}

		public class Heal
		{
			public byte Val { get; }
			public bool Full { get; }
			public bool Half { get; }
			public bool Quarter { get; }
			public bool Value { get; }

			public Heal( byte val )
			{
				this.Val = val;
				this.Full = this.Val == 0b11111111;
				this.Half = this.Val == 0b11111110;
				this.Quarter = this.Val == 0b11111101;
				this.Value = this.Val < 0b11111101;
			}

			public byte Write()
			{
				if ( this.Value )
					return this.Val;
				if ( this.Full )
					return 0b11111111;
				if ( this.Half )
					return 0b11111110;
				return this.Quarter
						   ? (byte) 0b11111101
						   : this.Val;
			}
		}
	}
}