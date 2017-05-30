using System.IO;
using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen6
{
	public class EncounterStatic : Common.EncounterStatic
	{
		public EncounterStatic( GameVersion gameVersion ) : base( gameVersion ) { }

		public override short HeldItem { get; set; }
		public override ushort Species { get; set; }
		public byte Form { get; set; }
		public byte Level { get; set; }
		public int Gender { get; set; }
		public int Ability { get; set; }
		public bool ShinyLock { get; set; }
		public bool IVLower { get; set; }
		public bool IVUpper { get; set; }

		protected override void ReadData( BinaryReader br )
		{
			this.Species = br.ReadUInt16();
			this.Form = br.ReadByte();
			this.Level = br.ReadByte();

			short heldItem = br.ReadInt16();
			if ( heldItem <= 0 )
				heldItem = 0;
			this.HeldItem = heldItem;

			byte flag6 = br.ReadByte();
			this.Gender = ( flag6 & 0b1100 ) >> 2;
			this.Ability = ( flag6 & 0b1110000 ) >> 4;
			this.ShinyLock = ( flag6 & 0b10 ) > 0;

			byte flag7 = br.ReadByte();
			this.IVLower = ( flag7 & 0b01 ) > 0;
			this.IVUpper = ( flag7 & 0b10 ) > 0;
		}

		protected override void WriteData( BinaryWriter bw )
		{
			bw.Write( this.Species );
			bw.Write( this.Form );
			bw.Write( this.Level );
			bw.Write( (short) ( this.HeldItem == 0 ? -1 : this.HeldItem ) );

			int flag6 = 0;
			flag6 |= ( this.Gender & 0b11 ) << 2;
			flag6 |= ( this.Ability & 0b111 ) << 4;
			flag6 |= this.ShinyLock ? 0b10 : 0;

			int flag7 = 0;
			flag7 |= this.IVUpper ? 0b10 : 0;
			flag7 |= this.IVLower ? 0b01 : 0;

			bw.Write( (byte) flag6 );
			bw.Write( (byte) flag7 );
		}
	}
}