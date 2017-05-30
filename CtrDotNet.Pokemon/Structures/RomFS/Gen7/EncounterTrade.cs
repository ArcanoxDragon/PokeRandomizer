using System.IO;
using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen7
{
	public class EncounterTrade : Common.EncounterStatic
	{
		#region Static

		public const int Size = 0x34;

		#endregion

		public EncounterTrade( GameVersion gameVersion ) : base( gameVersion ) { }

		protected override void ReadData( BinaryReader br )
		{
			this.Species = br.ReadUInt16();
			this.Unused0 = br.ReadInt16();
			this.Form = br.ReadByte();
			this.Level = br.ReadByte();

			this.IVs = new sbyte[ 6 ];
			for ( int i = 0; i < this.IVs.Length; i++ )
				this.IVs[ i ] = br.ReadSByte();

			this.Ability = br.ReadByte();
			this.Nature = br.ReadByte();
			this.Gender = br.ReadByte();
			this.TID = br.ReadUInt16();
			this.SID = br.ReadUInt16();

			short heldItem = br.ReadInt16();
			if ( heldItem <= 0 )
				heldItem = 0;
			this.HeldItem = heldItem;

			this.Unused1 = br.ReadInt32();
			this.TrainerGender = br.ReadByte();

			this.Unused2 = new byte[ 18 ];
			br.Read( this.Unused2, 0, this.Unused2.Length );

			this.TradeRequestSpecies = br.ReadUInt16();
		}

		protected override void WriteData( BinaryWriter bw )
		{
			bw.Write( this.Unused0 );
			bw.Write( this.Form );
			bw.Write( this.Level );

			foreach ( sbyte iv in this.IVs )
				bw.Write( iv );

			bw.Write( this.Ability );
			bw.Write( this.Nature );
			bw.Write( this.Gender );
			bw.Write( this.TID );
			bw.Write( this.SID );

			bw.Write( this.HeldItem == 0 ? -1 : this.HeldItem );

			bw.Write( this.Unused1 );
			bw.Write( this.TrainerGender );

			bw.Write( this.Unused2, 0, this.Unused2.Length );

			bw.Write( this.TradeRequestSpecies );
		}

		public override ushort Species { get; set; }
		public short Unused0 { get; set; }
		public byte Form { get; set; }
		public byte Level { get; set; }

		// ReSharper disable once InconsistentNaming
		public sbyte[] IVs { get; set; }

		public byte Ability { get; set; }
		public byte Nature { get; set; }
		public byte Gender { get; set; }
		public ushort TID { get; set; }
		public ushort SID { get; set; }
		public override short HeldItem { get; set; }
		public int Unused1 { get; set; }
		public byte TrainerGender { get; set; }
		public byte[] Unused2 { get; set; }
		public ushort TradeRequestSpecies { get; set; }
	}
}