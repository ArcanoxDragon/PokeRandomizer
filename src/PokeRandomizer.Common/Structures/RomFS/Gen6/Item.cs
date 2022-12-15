using System.IO;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.RomFS.Gen6
{
	public class Item : BaseDataStructure
	{
		#region Static

		public class Heal
		{
			public byte Val     { get; }
			public bool Full    { get; }
			public bool Half    { get; }
			public bool Quarter { get; }
			public bool Value   { get; }

			public Heal(byte val)
			{
				Val = val;
				Full = Val == 0b11111111;
				Half = Val == 0b11111110;
				Quarter = Val == 0b11111101;
				Value = Val < 0b11111101;
			}

			public byte Write()
			{
				if (Value)
					return Val;
				if (Full)
					return 0b11111111;
				if (Half)
					return 0b11111110;
				return Quarter
						   ? (byte) 0b11111101
						   : Val;
			}
		}

		#endregion

		public Item(GameVersion gameVersion) : base(gameVersion) { }

		public ushort Price               { get; set; }
		public int    BuyPrice            { get; set; }
		public int    SellPrice           { get; set; }
		public byte   HeldEffect          { get; set; }
		public byte   HeldArgument        { get; set; }
		public byte   NaturalGiftEffect   { get; set; }
		public byte   FlingEffect         { get; set; }
		public byte   FlingPower          { get; set; }
		public byte   NaturalGiftPower    { get; set; }
		public byte   NaturalGiftType     { get; set; }
		public byte   U8Flags             { get; set; }
		public byte   KeyFlags            { get; set; }
		public byte   UseEffect           { get; set; }
		public byte   BattleType          { get; set; } // Battle Type
		public byte   UnusedC             { get; set; } // 0 or 1
		public byte   Classification      { get; set; } // Classification (0-3 Battle, 4 Balls, 5 Mail)
		public byte   Consumable          { get; set; }
		public byte   SortIndex           { get; set; }
		public byte   CureInflict         { get; set; } // Bitflags
		public byte   FieldEffect         { get; set; } // Revive 1, Sacred Ash 3, Rare Candy 5, EvoStone 8
		public int    BoostAttack         { get; set; }
		public int    BoostDefense        { get; set; }
		public int    BoostSpeed          { get; set; }
		public int    BoostSpecialAttack  { get; set; }
		public int    BoostSpecialDefense { get; set; }
		public int    BoostAccuracy       { get; set; }
		public int    BoostCrit           { get; set; }
		public int    BoostPp             { get; set; }
		public ushort FunctionFlags       { get; set; }
		public byte   Evhp                { get; set; }
		public byte   EvAttack            { get; set; }
		public byte   EvDefense           { get; set; }
		public byte   EvSpeed             { get; set; }
		public byte   EvSpecialAttack     { get; set; }
		public byte   EvSpecialDefense    { get; set; }
		public Heal   Healing             { get; set; }
		public byte   PpGain              { get; set; }
		public byte   Friendship1         { get; set; }
		public byte   Friendship2         { get; set; }
		public byte   Friendship3         { get; set; }
		public byte   Unused23            { get; set; }
		public byte   Unused24            { get; set; }

		protected override void ReadData(BinaryReader br)
		{
			Price = br.ReadUInt16();
			BuyPrice = Price * 10;
			SellPrice = Price * 5;

			HeldEffect = br.ReadByte();
			HeldArgument = br.ReadByte();
			NaturalGiftEffect = br.ReadByte();
			FlingEffect = br.ReadByte();
			FlingPower = br.ReadByte();
			NaturalGiftPower = br.ReadByte();
			NaturalGiftType = br.ReadByte();
			U8Flags = (byte) ( NaturalGiftType >> 5 );
			NaturalGiftType &= 0x1F;
			KeyFlags = br.ReadByte();
			UseEffect = br.ReadByte();
			BattleType = br.ReadByte();
			UnusedC = br.ReadByte();
			Classification = br.ReadByte();
			Consumable = br.ReadByte();
			SortIndex = br.ReadByte();
			CureInflict = br.ReadByte();

			FieldEffect = br.ReadByte();
			BoostAttack = FieldEffect >> 4;
			FieldEffect &= 0xF;
			BoostDefense = br.ReadByte();
			BoostSpecialAttack = BoostDefense >> 4;
			BoostDefense &= 0xF;
			BoostSpecialDefense = br.ReadByte();
			BoostSpeed = BoostSpecialDefense >> 4;
			BoostSpecialDefense &= 0xF;
			BoostAccuracy = br.ReadByte();
			BoostCrit = ( BoostAccuracy >> 4 ) & 0x3;
			BoostPp = BoostAccuracy >> 6;
			BoostAccuracy &= 0xF;

			FunctionFlags = br.ReadUInt16();

			Evhp = br.ReadByte();
			EvAttack = br.ReadByte();
			EvDefense = br.ReadByte();
			EvSpeed = br.ReadByte();
			EvSpecialAttack = br.ReadByte();
			EvSpecialDefense = br.ReadByte();

			Healing = new Heal(br.ReadByte());
			PpGain = br.ReadByte();
			Friendship1 = br.ReadByte();
			Friendship2 = br.ReadByte();
			Friendship3 = br.ReadByte();
			Unused23 = br.ReadByte();
			Unused24 = br.ReadByte();
		}

		protected override void WriteData(BinaryWriter bw)
		{
			bw.Write(Price);
			bw.Write(HeldEffect);
			bw.Write(HeldArgument);
			bw.Write(NaturalGiftEffect);
			bw.Write(FlingEffect);
			bw.Write(FlingPower);
			bw.Write(NaturalGiftPower);
			bw.Write((byte) ( NaturalGiftType | ( U8Flags << 5 ) ));
			bw.Write(KeyFlags);
			bw.Write(UseEffect);
			bw.Write(BattleType);
			bw.Write(UnusedC);
			bw.Write(Classification);
			bw.Write(Consumable);
			bw.Write(SortIndex);
			bw.Write(CureInflict);

			bw.Write((byte) ( FieldEffect | ( BoostAttack << 4 ) ));
			bw.Write((byte) ( BoostDefense | ( BoostSpecialAttack << 4 ) ));
			bw.Write((byte) ( BoostSpecialDefense | ( BoostSpeed << 4 ) ));
			bw.Write((byte) ( BoostAccuracy | ( BoostCrit << 4 ) | ( BoostPp << 6 ) ));

			bw.Write(FunctionFlags);

			bw.Write(Evhp);
			bw.Write(EvAttack);
			bw.Write(EvDefense);
			bw.Write(EvSpeed);
			bw.Write(EvSpecialAttack);
			bw.Write(EvSpecialDefense);
			bw.Write(Healing.Write());
			bw.Write(PpGain);
			bw.Write(Friendship1);
			bw.Write(Friendship2);
			bw.Write(Friendship3);

			bw.Write(Unused23);
			bw.Write(Unused24);
		}
	}
}