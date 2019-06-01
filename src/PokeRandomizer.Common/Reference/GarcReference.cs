using System.IO;

namespace PokeRandomizer.Common.Reference
{
	public class GarcReference
	{
		public GarcReference( int file, GarcNames name, bool hasLangVariant = false, int offset = 0 )
		{
			this.Name = name;
			this.FileNumber = file;
			this.HasLanguageVariant = hasLangVariant;
			this.Offset = offset;
		}

		public int FileNumber { get; }
		public int Offset { get; }
		public GarcNames Name { get; }
		public bool HasLanguageVariant { get; }
		private int A => ( this.FileNumber / 100 ) % 10;
		private int B => ( this.FileNumber / 10 ) % 10;
		private int C => ( this.FileNumber / 1 ) % 10;
		public string RomFsPath => Path.Combine( "a", this.A.ToString(), this.B.ToString(), this.C.ToString() );

		public GarcReference GetRelativeGarc( int offset )
		{
			return new GarcReference( ( this.FileNumber - this.Offset ) + offset, this.Name, this.HasLanguageVariant, offset );
		}
	}
}