using System.IO;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.RomFS.Gen6
{
	public class Item : BaseDataStructure
	{
		#region Static

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

		#endregion

		public Item( GameVersion gameVersion ) : base( gameVersion ) { }

		public ushort Price { get; set; }
		public int BuyPrice { get; set; }
		public int SellPrice { get; set; }
		public byte HeldEffect { get; set; }
		public byte HeldArgument { get; set; }
		public byte NaturalGiftEffect { get; set; }
		public byte FlingEffect { get; set; }
		public byte FlingPower { get; set; }
		public byte NaturalGiftPower { get; set; }
		public byte NaturalGiftType { get; set; }
		public byte U8Flags { get; set; }
		public byte KeyFlags { get; set; }
		public byte UseEffect { get; set; }
		public byte BattleType { get; set; } // Battle Type
		public byte UnusedC { get; set; } // 0 or 1
		public byte Classification { get; set; } // Classification (0-3 Battle, 4 Balls, 5 Mail)
		public byte Consumable { get; set; }
		public byte SortIndex { get; set; }
		public byte CureInflict { get; set; } // Bitflags
		public byte FieldEffect { get; set; } // Revive 1, Sacred Ash 3, Rare Candy 5, EvoStone 8
		public int BoostAttack { get; set; }
		public int BoostDefense { get; set; }
		public int BoostSpeed { get; set; }
		public int BoostSpecialAttack { get; set; }
		public int BoostSpecialDefense { get; set; }
		public int BoostAccuracy { get; set; }
		public int BoostCrit { get; set; }
		public int BoostPp { get; set; }
		public ushort FunctionFlags { get; set; }
		public byte Evhp { get; set; }
		public byte EvAttack { get; set; }
		public byte EvDefense { get; set; }
		public byte EvSpeed { get; set; }
		public byte EvSpecialAttack { get; set; }
		public byte EvSpecialDefense { get; set; }
		public Heal Healing { get; set; }
		public byte PpGain { get; set; }
		public byte Friendship1 { get; set; }
		public byte Friendship2 { get; set; }
		public byte Friendship3 { get; set; }
		public byte Unused23 { get; set; }
		public byte Unused24 { get; set; }

		protected override void ReadData( BinaryReader br )
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

		protected override void WriteData( BinaryWriter bw )
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
		}
	}
}