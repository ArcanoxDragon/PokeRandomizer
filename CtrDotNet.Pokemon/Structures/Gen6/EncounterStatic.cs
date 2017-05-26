using System.IO;
using CtrDotNet.Pokemon.Structures.Common;

namespace CtrDotNet.Pokemon.Structures.Gen6
{
	public class EncounterStatic : Structures.EncounterStatic, IDataStructure
	{
		private int heldItem;

		public EncounterStatic( byte[] data )
		{
			this.Read( data );
		}

		public override int HeldItem
		{
			get => this.heldItem < 0 ? 0 : this.heldItem;
			set => this.heldItem = value == 0 ? -1 : value;
		}

		public override ushort Species { get; set; }
		public byte Form { get; set; }
		public byte Level { get; set; }
		public int Gender { get; set; }
		public int Ability { get; set; }
		public bool ShinyLock { get; set; }
		public bool IVLower { get; set; }
		public bool IVUpper { get; set; }

		public void Read( byte[] data )
		{
			using ( var ms = new MemoryStream( data ) )
			using ( var br = new BinaryReader( ms ) )
			{
				this.Read( br );
			}
		}

		public byte[] Write()
		{
			byte[] data;

			using ( var ms = new MemoryStream() )
			using ( var bw = new BinaryWriter( ms ) )
			{
				this.Write( bw );

				data = ms.ToArray();
			}

			return data;
		}

		protected virtual void Read( BinaryReader r )
		{
			this.Species = r.ReadUInt16();
			this.Form = r.ReadByte();
			this.Level = r.ReadByte();
			this.HeldItem = r.ReadInt16();

			byte flag6 = r.ReadByte();
			this.Gender = ( flag6 & 0b1100 ) >> 2;
			this.Ability = ( flag6 & 0b1110000 ) >> 4;
			this.ShinyLock = ( flag6 & 0b10 ) > 0;

			byte flag7 = r.ReadByte();
			this.IVLower = ( flag7 & 0b01 ) > 0;
			this.IVUpper = ( flag7 & 0b10 ) > 0;
		}

		protected virtual void Write( BinaryWriter w )
		{
			w.Write( this.Species );
			w.Write( this.Form );
			w.Write( this.Level );
			w.Write( (short) this.HeldItem );

			int flag6 = 0;
			flag6 |= ( this.Gender & 0b11 ) << 2;
			flag6 |= ( this.Ability & 0b111 ) << 4;
			flag6 |= this.ShinyLock ? 0b10 : 0;

			int flag7 = 0;
			flag7 |= this.IVUpper ? 0b10 : 0;
			flag7 |= this.IVLower ? 0b01 : 0;

			w.Write( (byte) flag6 );
			w.Write( (byte) flag7 );
		}
	}
}