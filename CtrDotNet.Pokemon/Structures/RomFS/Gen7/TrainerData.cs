using System;
using System.Collections.Generic;
using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Structures.RomFS.Gen7
{
	public class TrainerData
	{
		private readonly byte[] trdata;
		public readonly List<Pokemon> Pokemon = new List<Pokemon>();

		public int ID { get; set; }
		public string Name { get; set; }

		// TODO: refactor this class to use BaseDataStructure
		public TrainerData( GameVersion gameVersion, byte[] data = null, byte[] pokeData = null )
		{
			data = data ?? new byte[ 0x14 ];
			pokeData = pokeData ?? new byte[ 0x20 ];

			this.trdata = (byte[]) data.Clone();

			for ( int i = 0; i < this.NumPokemon; i++ )
			{
				byte[] poke = new byte[ 0x20 ];
				Array.Copy( pokeData, i * 0x20, poke, 0, 0x20 );
				Pokemon pkm = new Pokemon( gameVersion );
				pkm.Read( poke );
				this.Pokemon.Add( pkm );
			}
		}

		public byte TrainerClass
		{
			get => this.trdata[ 0 ];
			set => this.trdata[ 0 ] = value;
		}

		public int NumPokemon
		{
			get => this.trdata[ 3 ];
			set => this.trdata[ 3 ] = (byte) ( value % 7 );
		}

		public int Item1
		{
			get => BitConverter.ToUInt16( this.trdata, 0x04 );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.trdata, 0x04 );
		}

		public int Item2
		{
			get => BitConverter.ToUInt16( this.trdata, 0x06 );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.trdata, 0x06 );
		}

		public int Item3
		{
			get => BitConverter.ToUInt16( this.trdata, 0x08 );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.trdata, 0x08 );
		}

		public int Item4
		{
			get => BitConverter.ToUInt16( this.trdata, 0x0A );
			set => BitConverter.GetBytes( (ushort) value ).CopyTo( this.trdata, 0x0A );
		}

		public int AI
		{
			get => this.trdata[ 0x0C ];
			set => this.trdata[ 0x0C ] = (byte) value;
		}

		public bool Flag
		{
			get => this.trdata[ 0x0D ] == 1;
			set => this.trdata[ 0x0D ] = (byte) ( value ? 1 : 0 );
		}

		public int Money
		{
			get => this.trdata[ 0x11 ];
			set => this.trdata[ 0x11 ] = (byte) value;
		}

		public void Write( out byte[] tr, out byte[] pk )
		{
			tr = this.trdata;
			byte[] dat = new byte[ Gen7.Pokemon.Size * this.NumPokemon ];

			for ( int i = 0; i < this.NumPokemon; i++ )
				this.Pokemon[ i ].Write().CopyTo( dat, Gen7.Pokemon.Size * i );

			pk = dat;
		}
	}
}