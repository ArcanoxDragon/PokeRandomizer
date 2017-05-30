using System.IO;
using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Structures.RomFS.Common
{
	public class MegaEvolutions : BaseDataStructure
	{
		public MegaEvolutions( GameVersion gameVersion ) : base( gameVersion ) { }

		public ushort[] Form { get; set; }
		public ushort[] Method { get; set; }
		public ushort[] Argument { get; set; }
		public ushort[] Unused6 { get; set; }

		public override void Read( byte[] data )
		{
			if ( data.Length < 0x10 || data.Length % 8 != 0 )
				throw new InvalidDataException( $"Invalid length for Mega Evolution structure: {data.Length}" );

			base.Read( data );
		}

		protected override void ReadData( BinaryReader br )
		{
			this.Form = new ushort[ br.BaseStream.Length / 8 ];
			this.Method = new ushort[ br.BaseStream.Length / 8 ];
			this.Argument = new ushort[ br.BaseStream.Length / 8 ];
			this.Unused6 = new ushort[ br.BaseStream.Length / 8 ];

			for ( int i = 0; i < this.Form.Length; i++ )
			{
				this.Form[ i ] = br.ReadUInt16();
				this.Method[ i ] = br.ReadUInt16();
				this.Argument[ i ] = br.ReadUInt16();
				this.Unused6[ i ] = br.ReadUInt16();
			}
		}

		protected override void WriteData( BinaryWriter bw )
		{
			for ( int i = 0; i < this.Form.Length; i++ )
			{
				if ( this.Method[ i ] == 0 ) // No method to evolve, clear information.
				{
					this.Form[ i ] = this.Argument[ i ] = 0;
				}

				bw.Write( this.Form[ i ] );
				bw.Write( this.Method[ i ] );
				bw.Write( this.Argument[ i ] );
				bw.Write( this.Unused6[ i ] );
			}
		}
	}
}