using System.Linq;

namespace CtrDotNet.Pokemon.Structures.RomFS.PokemonInfo
{
	public class PokemonInfoORAS : PokemonInfoXY
	{
		public new const int Size = 0x50;

		public override void Read( byte[] data )
		{
			if ( data.Length != Size )
				return;
			this.Data = data;

			// Unpack TMHM & Tutors
			this.TmHm = PokemonInfo.GetBits( this.Data.Skip( 0x28 ).Take( 0x10 ).ToArray() );
			this.TypeTutors = PokemonInfo.GetBits( this.Data.Skip( 0x38 ).Take( 0x4 ).ToArray() );

			// 0x3C-0x40 unknown
			this.SpecialTutors = new[] {
				PokemonInfo.GetBits( this.Data.Skip( 0x40 ).Take( 0x04 ).ToArray() ),
				PokemonInfo.GetBits( this.Data.Skip( 0x44 ).Take( 0x04 ).ToArray() ),
				PokemonInfo.GetBits( this.Data.Skip( 0x48 ).Take( 0x04 ).ToArray() ),
				PokemonInfo.GetBits( this.Data.Skip( 0x4C ).Take( 0x04 ).ToArray() ),
			};
		}

		public override byte[] Write()
		{
			PokemonInfo.SetBits( this.TmHm ).CopyTo( this.Data, 0x28 );
			PokemonInfo.SetBits( this.TypeTutors ).CopyTo( this.Data, 0x38 );
			PokemonInfo.SetBits( this.SpecialTutors[ 0 ] ).CopyTo( this.Data, 0x40 );
			PokemonInfo.SetBits( this.SpecialTutors[ 1 ] ).CopyTo( this.Data, 0x44 );
			PokemonInfo.SetBits( this.SpecialTutors[ 2 ] ).CopyTo( this.Data, 0x48 );
			PokemonInfo.SetBits( this.SpecialTutors[ 3 ] ).CopyTo( this.Data, 0x4C );
			return this.Data;
		}
	}
}