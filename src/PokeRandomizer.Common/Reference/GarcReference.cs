using System.IO;

namespace PokeRandomizer.Common.Reference
{
	public class GarcReference
	{
		public GarcReference(int file, GarcNames name, bool hasLangVariant = false, int offset = 0)
		{
			Name = name;
			FileNumber = file;
			HasLanguageVariant = hasLangVariant;
			Offset = offset;
		}

		public  int       FileNumber         { get; }
		public  int       Offset             { get; }
		public  GarcNames Name               { get; }
		public  bool      HasLanguageVariant { get; }
		private int       A                  => ( FileNumber / 100 ) % 10;
		private int       B                  => ( FileNumber / 10 ) % 10;
		private int       C                  => ( FileNumber / 1 ) % 10;
		public  string    RomFsPath          => Path.Combine("a", A.ToString(), B.ToString(), C.ToString());

		public GarcReference GetRelativeGarc(int offset)
		{
			return new GarcReference(( FileNumber - Offset ) + offset, Name, HasLanguageVariant, offset);
		}
	}
}