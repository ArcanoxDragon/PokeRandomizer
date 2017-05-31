using System.IO;

namespace CtrDotNet.Pokemon.Reference
{
	public class GarcReference
	{
		internal GarcReference( int file, GarcNames name, bool lv = false, int offset = 0 )
		{
			this.Name = name;
			this.FileNumber = file;
			this.HasLanguageVariant = lv;
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