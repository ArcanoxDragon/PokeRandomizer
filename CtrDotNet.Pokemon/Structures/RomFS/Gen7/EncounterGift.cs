using System.IO;
using CtrDotNet.Pokemon.Structures.RomFS.Common;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen7
{
	public class EncounterGift : Common.EncounterStatic, IDataStructure
	{
		public EncounterGift( byte[] data )
		{
			this.Read( data );
		}

		public override ushort Species { get; set; }
		public byte Form { get; set; }
		public byte Level { get; set; }
		public byte Gender { get; set; }
		public byte[] Unused1 { get; private set; }
		public override int HeldItem { get; set; }

		public void Read( byte[] data )
		{
			using ( var ms = new MemoryStream( data ) )
			using ( var br = new BinaryReader( ms ) )
			{
				this.Species = br.ReadUInt16();
				this.Form = br.ReadByte();
				this.Level = br.ReadByte();
				this.Gender = br.ReadByte();
				this.Unused1 = br.ReadBytes( 3 );
				this.HeldItem = br.ReadUInt16();
			}
		}

		public byte[] Write()
		{
			using ( var ms = new MemoryStream() )
			using ( var bw = new BinaryWriter( ms ) )
			{
				bw.Write( this.Species );
				bw.Write( this.Form );
				bw.Write( this.Level );
				bw.Write( this.Gender );
				bw.Write( this.Unused1, 0, 3 );
				bw.Write( (ushort) this.HeldItem );

				return ms.ToArray();
			}
		}
	}
}