using System;
using System.IO;
using CtrDotNet.Pokemon.Structures.RomFS.Common;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen6
{
	public class Maison
	{
		public class Trainer : IDataStructure
		{
			public ushort Class { get; set; }
			public ushort Count { get; set; }
			public ushort[] Choices { get; set; }

			public Trainer() { }

			public Trainer( byte[] data )
			{
				this.Read( data );
			}

			public void Read( byte[] data )
			{
				this.Class = BitConverter.ToUInt16( data, 0 );
				this.Count = BitConverter.ToUInt16( data, 2 );
				this.Choices = new ushort[ this.Count ];
				for ( int i = 0; i < this.Count; i++ )
					this.Choices[ i ] = BitConverter.ToUInt16( data, 4 + 2 * i );
			}

			public byte[] Write()
			{
				using ( var ms = new MemoryStream() )
				using ( var bw = new BinaryWriter( ms ) )
				{
					bw.Write( this.Class );
					bw.Write( this.Count );
					foreach ( ushort choice in this.Choices )
						bw.Write( choice );
					return ms.ToArray();
				}
			}
		}

		public class Pokemon : IDataStructure
		{
			private readonly ushort[] moves = new ushort[ 4 ];
			private readonly bool[] eVs = new bool[ 6 ];
			private byte ev;

			public ushort Species { get; set; }
			public byte Nature { get; set; }
			public ushort Item { get; set; }
			public ushort Form { get; set; }

			public int Move1
			{
				get => this.moves[ 0 ];
				set => this.moves[ 0 ] = (ushort) value;
			}

			public int Move2
			{
				get => this.moves[ 1 ];
				set => this.moves[ 1 ] = (ushort) value;
			}

			public int Move3
			{
				get => this.moves[ 2 ];
				set => this.moves[ 2 ] = (ushort) value;
			}

			public int Move4
			{
				get => this.moves[ 3 ];
				set => this.moves[ 3 ] = (ushort) value;
			}

			public bool Hp
			{
				get => this.eVs[ 0 ];
				set => this.eVs[ 0 ] = value;
			}

			public bool Atk
			{
				get => this.eVs[ 1 ];
				set => this.eVs[ 1 ] = value;
			}

			public bool Def
			{
				get => this.eVs[ 2 ];
				set => this.eVs[ 2 ] = value;
			}

			public bool Spe
			{
				get => this.eVs[ 3 ];
				set => this.eVs[ 3 ] = value;
			}

			public bool Spa
			{
				get => this.eVs[ 4 ];
				set => this.eVs[ 4 ] = value;
			}

			public bool Spd
			{
				get => this.eVs[ 5 ];
				set => this.eVs[ 5 ] = value;
			}

			public Pokemon( byte[] data )
			{
				this.Read( data );
			}

			public void Read( byte[] data )
			{
				this.Species = BitConverter.ToUInt16( data, 0 );
				for ( int i = 0; i < 4; i++ )
					this.moves[ i ] = BitConverter.ToUInt16( data, 2 + 2 * i );
				this.ev = data[ 0xA ];
				for ( int i = 0; i < 6; i++ )
					this.eVs[ i ] = ( ( this.ev >> i ) & 1 ) == 1;
				this.Nature = data[ 0xB ];
				this.Item = BitConverter.ToUInt16( data, 0xC );
				this.Form = BitConverter.ToUInt16( data, 0xE );
			}

			public byte[] Write()
			{
				using ( var ms = new MemoryStream() )
				using ( var bw = new BinaryWriter( ms ) )
				{
					bw.Write( this.Species );

					foreach ( ushort move in this.moves )
						bw.Write( move );

					int ev = this.ev & 0xC0;

					for ( int i = 0; i < this.eVs.Length; i++ )
						ev |= this.eVs[ i ] ? 1 << i : 0;

					bw.Write( (byte) ev );
					bw.Write( this.Nature );
					bw.Write( this.Item );
					bw.Write( this.Form );

					return ms.ToArray();
				}
			}
		}
	}
}