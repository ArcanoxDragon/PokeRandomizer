using System;
using System.IO;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen7
{
	public class Pokemon : BaseDataStructure
	{
		public const int Size = 32;

		public Pokemon( GameVersion gameVersion ) : base( gameVersion ) { }

		public byte Flag0 { get; set; }
		public byte Gender { get; set; }
		public byte Ability { get; set; }
		public byte Nature { get; set; }
		public byte EvHp { get; set; }
		public byte EvAttack { get; set; }
		public byte EvDefense { get; set; }
		public byte EvSpecialAttack { get; set; }
		public byte EvSpecialDefense { get; set; }
		public byte EvSpeed { get; set; }
		public uint Ivs { get; set; }
		public ushort IvHp { get; set; }
		public ushort IvAttack { get; set; }
		public ushort IvDefense { get; set; }
		public ushort IvSpecialAttack { get; set; }
		public ushort IvSpecialDefense { get; set; }
		public ushort IvSpeed { get; set; }
		public bool Shiny { get; set; }
		public ushort UnusedC { get; set; } // 12-13
		public byte Level { get; set; }
		public byte UnusedF { get; set; } // 15
		public ushort Species { get; set; }
		public byte Form { get; set; }
		public ushort Item { get; set; }
		public ushort Unused16 { get; set; }
		public ushort Move1 { get; set; }
		public ushort Move2 { get; set; }
		public ushort Move3 { get; set; }
		public ushort Move4 { get; set; }

		// ReSharper disable once InconsistentNaming
		public ushort[] IVs
		{
			get => new[] { this.IvHp, this.IvAttack, this.IvDefense, this.IvSpecialAttack, this.IvSpecialDefense, this.IvSpeed };
			set
			{
				if ( value?.Length != 6 )
					return;

				this.IvHp = value[ 0 ];
				this.IvAttack = value[ 1 ];
				this.IvDefense = value[ 2 ];
				this.IvSpecialAttack = value[ 3 ];
				this.IvSpecialDefense = value[ 4 ];
				this.IvSpeed = value[ 5 ];
			}
		}

		public byte[] EVs
		{
			get => new[] { this.EvHp, this.EvAttack, this.EvDefense, this.EvSpecialAttack, this.EvSpecialDefense, this.EvSpeed };
			set
			{
				if ( value?.Length != 6 )
					return;
				this.EvHp = value[ 0 ];
				this.EvAttack = value[ 1 ];
				this.EvDefense = value[ 2 ];
				this.EvSpecialAttack = value[ 3 ];
				this.EvSpecialDefense = value[ 4 ];
				this.EvSpeed = value[ 5 ];
			}
		}

		public ushort[] Moves
		{
			get => new[] { this.Move1, this.Move2, this.Move3, this.Move4 };
			set
			{
				if ( value?.Length != 4 )
					return;
				this.Move1 = value[ 0 ];
				this.Move2 = value[ 1 ];
				this.Move3 = value[ 2 ];
				this.Move4 = value[ 3 ];
			}
		}

		public override void Read( byte[] data )
		{
			if ( data.Length != 32 )
				throw new ArgumentException( "Invalid Pokemon!" );

			base.Read( data );
		}

		protected override void ReadData( BinaryReader br )
		{
			uint flag0 = br.ReadByte();
			this.Flag0 = (byte) flag0;
			this.Gender = (byte) flag0.GetBitfield( 2, 0 );
			this.Ability = (byte) flag0.GetBitfield( 2, 4 );

			this.Nature = br.ReadByte();
			this.EvHp = br.ReadByte();
			this.EvAttack = br.ReadByte();
			this.EvDefense = br.ReadByte();
			this.EvSpecialAttack = br.ReadByte();
			this.EvSpecialDefense = br.ReadByte();
			this.EvSpeed = br.ReadByte();

			uint ivs = br.ReadUInt32();
			this.Ivs = ivs;
			this.IvHp /*            */ = (ushort) ivs.GetBitfield( 5, 00 );
			this.IvAttack /*        */ = (ushort) ivs.GetBitfield( 5, 05 );
			this.IvDefense /*       */ = (ushort) ivs.GetBitfield( 5, 10 );
			this.IvSpecialAttack /* */ = (ushort) ivs.GetBitfield( 5, 15 );
			this.IvSpecialDefense /**/ = (ushort) ivs.GetBitfield( 5, 20 );
			this.IvSpeed /*         */ = (ushort) ivs.GetBitfield( 5, 25 );
			this.Shiny /*           */ = (ushort) ivs.GetBitfield( 1, 30 ) > 0;

			this.UnusedC = br.ReadUInt16();
			this.Level = br.ReadByte();
			this.UnusedF = br.ReadByte();
			this.Species = br.ReadUInt16();
			this.Form = br.ReadByte();
			this.Item = br.ReadUInt16();
			this.Unused16 = br.ReadUInt16();
			this.Move1 = br.ReadUInt16();
			this.Move2 = br.ReadUInt16();
			this.Move3 = br.ReadUInt16();
			this.Move4 = br.ReadUInt16();
		}

		protected override void WriteData( BinaryWriter bw )
		{
			uint flag0 = this.Flag0;
			flag0 = flag0.SetBitfield( this.Gender, 2, 0 );
			flag0 = flag0.SetBitfield( this.Ability, 2, 4 );
			bw.Write( (byte) flag0 );

			bw.Write( this.Nature );
			bw.Write( this.EvHp );
			bw.Write( this.EvAttack );
			bw.Write( this.EvDefense );
			bw.Write( this.EvSpecialAttack );
			bw.Write( this.EvSpecialDefense );
			bw.Write( this.EvSpeed );

			uint ivs = this.Ivs;
			ivs.SetBitfield( this.IvHp /*            */, 5, 00 );
			ivs.SetBitfield( this.IvAttack /*        */, 5, 05 );
			ivs.SetBitfield( this.IvDefense /*       */, 5, 10 );
			ivs.SetBitfield( this.IvSpecialAttack /* */, 5, 15 );
			ivs.SetBitfield( this.IvSpecialDefense /**/, 5, 20 );
			ivs.SetBitfield( this.IvSpeed /*         */, 5, 25 );
			ivs.SetBitfield( this.Shiny ? 1 : 0 /*   */, 1, 30 );

			bw.Write( this.UnusedC );
			bw.Write( this.Level );
			bw.Write( this.UnusedF );
			bw.Write( this.Species );
			bw.Write( this.Form );
			bw.Write( this.Item );
			bw.Write( this.Unused16 );
			bw.Write( this.Move1 );
			bw.Write( this.Move2 );
			bw.Write( this.Move3 );
			bw.Write( this.Move4 );
		}
	}
}