using System.IO;

namespace CtrDotNet.Pokemon.Reference
{
	public class GarcReference
	{
		public readonly int FileNumber;
		public readonly string Name;
		private int A => ( this.FileNumber / 100 ) % 10;
		private int B => ( this.FileNumber / 10 ) % 10;
		private int C => ( this.FileNumber / 1 ) % 10;
		public readonly bool HasLanguageVariant;
		public string Reference => Path.Combine( "a", this.A.ToString(), this.B.ToString(), this.C.ToString() );

		internal GarcReference( int file, string name, bool lv = false )
		{
			this.Name = name;
			this.FileNumber = file;
			this.HasLanguageVariant = lv;
		}

		public GarcReference GetRelativeGarc( int offset, string name = "" )
		{
			return new GarcReference( this.FileNumber + offset, name );
		}
	}
}