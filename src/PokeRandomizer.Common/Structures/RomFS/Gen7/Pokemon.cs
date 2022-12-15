using System;
using System.IO;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Utility;

namespace PokeRandomizer.Common.Structures.RomFS.Gen7
{
	public class Pokemon : BaseDataStructure
	{
		public const int Size = 32;

		public Pokemon(GameVersion gameVersion) : base(gameVersion) { }

		public byte   Flag0            { get; set; }
		public byte   Gender           { get; set; }
		public byte   Ability          { get; set; }
		public byte   Nature           { get; set; }
		public byte   EvHp             { get; set; }
		public byte   EvAttack         { get; set; }
		public byte   EvDefense        { get; set; }
		public byte   EvSpecialAttack  { get; set; }
		public byte   EvSpecialDefense { get; set; }
		public byte   EvSpeed          { get; set; }
		public uint   Ivs              { get; set; }
		public ushort IvHp             { get; set; }
		public ushort IvAttack         { get; set; }
		public ushort IvDefense        { get; set; }
		public ushort IvSpecialAttack  { get; set; }
		public ushort IvSpecialDefense { get; set; }
		public ushort IvSpeed          { get; set; }
		public bool   Shiny            { get; set; }
		public ushort UnusedC          { get; set; } // 12-13
		public byte   Level            { get; set; }
		public byte   UnusedF          { get; set; } // 15
		public ushort Species          { get; set; }
		public byte   Form             { get; set; }
		public ushort Item             { get; set; }
		public ushort Unused16         { get; set; }
		public ushort Move1            { get; set; }
		public ushort Move2            { get; set; }
		public ushort Move3            { get; set; }
		public ushort Move4            { get; set; }

		// ReSharper disable once InconsistentNaming
		public ushort[] IVs
		{
			get => new[] { IvHp, IvAttack, IvDefense, IvSpecialAttack, IvSpecialDefense, IvSpeed };
			set
			{
				if (value?.Length != 6)
					return;

				IvHp = value[0];
				IvAttack = value[1];
				IvDefense = value[2];
				IvSpecialAttack = value[3];
				IvSpecialDefense = value[4];
				IvSpeed = value[5];
			}
		}

		public byte[] EVs
		{
			get => new[] { EvHp, EvAttack, EvDefense, EvSpecialAttack, EvSpecialDefense, EvSpeed };
			set
			{
				if (value?.Length != 6)
					return;
				EvHp = value[0];
				EvAttack = value[1];
				EvDefense = value[2];
				EvSpecialAttack = value[3];
				EvSpecialDefense = value[4];
				EvSpeed = value[5];
			}
		}

		public ushort[] Moves
		{
			get => new[] { Move1, Move2, Move3, Move4 };
			set
			{
				if (value?.Length != 4)
					return;
				Move1 = value[0];
				Move2 = value[1];
				Move3 = value[2];
				Move4 = value[3];
			}
		}

		public override void Read(byte[] data)
		{
			if (data.Length != 32)
				throw new ArgumentException("Invalid Pokemon!");

			base.Read(data);
		}

		protected override void ReadData(BinaryReader br)
		{
			uint flag0 = br.ReadByte();
			Flag0 = (byte) flag0;
			Gender = (byte) flag0.GetBitfield(2, 0);
			Ability = (byte) flag0.GetBitfield(2, 4);

			Nature = br.ReadByte();
			EvHp = br.ReadByte();
			EvAttack = br.ReadByte();
			EvDefense = br.ReadByte();
			EvSpecialAttack = br.ReadByte();
			EvSpecialDefense = br.ReadByte();
			EvSpeed = br.ReadByte();

			uint ivs = br.ReadUInt32();
			Ivs = ivs;
			IvHp /*            */ = (ushort) ivs.GetBitfield(5, 00);
			IvAttack /*        */ = (ushort) ivs.GetBitfield(5, 05);
			IvDefense /*       */ = (ushort) ivs.GetBitfield(5, 10);
			IvSpecialAttack /* */ = (ushort) ivs.GetBitfield(5, 15);
			IvSpecialDefense /**/ = (ushort) ivs.GetBitfield(5, 20);
			IvSpeed /*         */ = (ushort) ivs.GetBitfield(5, 25);
			Shiny /*           */ = (ushort) ivs.GetBitfield(1, 30) > 0;

			UnusedC = br.ReadUInt16();
			Level = br.ReadByte();
			UnusedF = br.ReadByte();
			Species = br.ReadUInt16();
			Form = br.ReadByte();
			Item = br.ReadUInt16();
			Unused16 = br.ReadUInt16();
			Move1 = br.ReadUInt16();
			Move2 = br.ReadUInt16();
			Move3 = br.ReadUInt16();
			Move4 = br.ReadUInt16();
		}

		protected override void WriteData(BinaryWriter bw)
		{
			uint flag0 = Flag0;
			flag0 = flag0.SetBitfield(Gender, 2, 0);
			flag0 = flag0.SetBitfield(Ability, 2, 4);
			bw.Write((byte) flag0);

			bw.Write(Nature);
			bw.Write(EvHp);
			bw.Write(EvAttack);
			bw.Write(EvDefense);
			bw.Write(EvSpecialAttack);
			bw.Write(EvSpecialDefense);
			bw.Write(EvSpeed);

			uint ivs = Ivs;
			ivs.SetBitfield(IvHp /*            */, 5, 00);
			ivs.SetBitfield(IvAttack /*        */, 5, 05);
			ivs.SetBitfield(IvDefense /*       */, 5, 10);
			ivs.SetBitfield(IvSpecialAttack /* */, 5, 15);
			ivs.SetBitfield(IvSpecialDefense /**/, 5, 20);
			ivs.SetBitfield(IvSpeed /*         */, 5, 25);
			ivs.SetBitfield(Shiny ? 1 : 0 /*   */, 1, 30);

			bw.Write(UnusedC);
			bw.Write(Level);
			bw.Write(UnusedF);
			bw.Write(Species);
			bw.Write(Form);
			bw.Write(Item);
			bw.Write(Unused16);
			bw.Write(Move1);
			bw.Write(Move2);
			bw.Write(Move3);
			bw.Write(Move4);
		}
	}
}