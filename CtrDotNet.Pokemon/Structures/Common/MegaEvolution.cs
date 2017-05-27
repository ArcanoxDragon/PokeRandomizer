using System.IO;

namespace CtrDotNet.Pokemon.Structures.Common
{
	public class MegaEvolutions : IDataStructure
	{
		public ushort[] Form { get; set; }
		public ushort[] Method { get; set; }
		public ushort[] Argument { get; set; }
		public ushort[] Unused6 { get; set; }

		public MegaEvolutions( byte[] data )
		{
			if ( data.Length < 0x10 || data.Length % 8 != 0 )
				return;

			this.Read( data );
		}

		public void Read( byte[] data )
		{
			this.Form = new ushort[ data.Length / 8 ];
			this.Method = new ushort[ data.Length / 8 ];
			this.Argument = new ushort[ data.Length / 8 ];
			this.Unused6 = new ushort[ data.Length / 8 ];

			using ( var ms = new MemoryStream( data ) )
			using ( var br = new BinaryReader( ms ) )
			{
				for ( int i = 0; i < this.Form.Length; i++ )
				{
					this.Form[ i ] = br.ReadUInt16();
					this.Method[ i ] = br.ReadUInt16();
					this.Argument[ i ] = br.ReadUInt16();
					this.Unused6[ i ] = br.ReadUInt16();
				}
			}
		}

		public byte[] Write()
		{
			using ( MemoryStream ms = new MemoryStream() )
			using ( BinaryWriter bw = new BinaryWriter( ms ) )
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

				return ms.ToArray();
			}
		}
	}
}