using System.IO;
using System.Threading.Tasks;
using CtrDotNet.CTR.Garc;
using CtrDotNet.Utility;
using PokeRandomizer.Common.Reference;

namespace PokeRandomizer.Common.Garc
{
	public class ReferencedGarc : IGarcFile
	{
		public ReferencedGarc(GarcFile garc, GarcReference reference)
		{
			Garc = garc;
			Reference = reference;
		}

		public GarcFile      Garc      { get; }
		public GarcReference Reference { get; }

		public Task<byte[][]> GetFiles() => Garc.GetFiles();
		public Task<byte[]> GetFile(int file) => Garc.GetFile(file);
		public Task SetFiles(byte[][] files) => Garc.SetFiles(files);
		public Task SetFile(int file, byte[] data) => Garc.SetFile(file, data);
		public Task<byte[]> Write() => Garc.Write();
		public Task SaveFile() => SaveFileTo(PathUtil.GetPathBase(Garc.Path, Reference.RomFsPath));

		public Task SaveFileTo(string path)
		{
			string dirName = Path.GetDirectoryName(Reference.RomFsPath);
			string outPath = Path.Combine(path, "RomFS", dirName);

			if (!Directory.Exists(outPath))
				Directory.CreateDirectory(outPath);

			return Garc.SaveFileTo(outPath);
		}
	}
}