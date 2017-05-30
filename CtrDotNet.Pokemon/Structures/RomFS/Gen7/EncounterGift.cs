using System.IO;
using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen7
{
	public class EncounterGift : Common.EncounterStatic
	{
		public EncounterGift( GameVersion gameVersion ) : base( gameVersion ) { }

		public override ushort Species { get; set; }
		public byte Form { get; set; }
		public byte Level { get; set; }
		public byte Gender { get; set; }
		public byte[] Unused1 { get; private set; }
		public override short HeldItem { get; set; }

		protected override void ReadData( BinaryReader br )
		{
			this.Species = br.ReadUInt16();
			this.Form = br.ReadByte();
			this.Level = br.ReadByte();
			this.Gender = br.ReadByte();
			this.Unused1 = br.ReadBytes( 3 );
			this.HeldItem = (short) br.ReadUInt16();
		}

		protected override void WriteData( BinaryWriter bw )
		{
			bw.Write( this.Species );
			bw.Write( this.Form );
			bw.Write( this.Level );
			bw.Write( this.Gender );
			bw.Write( this.Unused1, 0, 3 );
			bw.Write( (ushort) this.HeldItem );
		}
	}
}