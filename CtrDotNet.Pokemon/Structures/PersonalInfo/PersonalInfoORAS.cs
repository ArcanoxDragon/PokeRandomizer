using System.Linq;

namespace CtrDotNet.Pokemon.Structures.PersonalInfo
{
	public class PersonalInfoORAS : PersonalInfoXY
	{
		public new const int Size = 0x50;

		public PersonalInfoORAS( byte[] data )
		{
			if ( data.Length != Size )
				return;
			this.data = data;

			// Unpack TMHM & Tutors
			this.TMHM = PersonalInfo.GetBits( this.data.Skip( 0x28 ).Take( 0x10 ).ToArray() );
			this.TypeTutors = PersonalInfo.GetBits( this.data.Skip( 0x38 ).Take( 0x4 ).ToArray() );

			// 0x3C-0x40 unknown
			this.SpecialTutors = new[] {
				PersonalInfo.GetBits( this.data.Skip( 0x40 ).Take( 0x04 ).ToArray() ),
				PersonalInfo.GetBits( this.data.Skip( 0x44 ).Take( 0x04 ).ToArray() ),
				PersonalInfo.GetBits( this.data.Skip( 0x48 ).Take( 0x04 ).ToArray() ),
				PersonalInfo.GetBits( this.data.Skip( 0x4C ).Take( 0x04 ).ToArray() ),
			};
		}

		public override byte[] Write()
		{
			PersonalInfo.SetBits( this.TMHM ).CopyTo( this.data, 0x28 );
			PersonalInfo.SetBits( this.TypeTutors ).CopyTo( this.data, 0x38 );
			PersonalInfo.SetBits( this.SpecialTutors[ 0 ] ).CopyTo( this.data, 0x40 );
			PersonalInfo.SetBits( this.SpecialTutors[ 1 ] ).CopyTo( this.data, 0x44 );
			PersonalInfo.SetBits( this.SpecialTutors[ 2 ] ).CopyTo( this.data, 0x48 );
			PersonalInfo.SetBits( this.SpecialTutors[ 3 ] ).CopyTo( this.data, 0x4C );
			return this.data;
		}
	}
}