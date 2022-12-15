using IO = System.IO;
using System.IO;
using System.Threading.Tasks;
using CtrDotNet.Utility;

namespace CtrDotNet.CTR.Garc
{
	public class GarcFile : IGarcFile
	{
		#region Static

		public static async Task<GarcFile> FromFile(string path, bool useLz = false)
		{
			using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				byte[] buffer = new byte[fs.Length];
				await fs.ReadAsync(buffer, 0, buffer.Length);
				GarcFile file = new GarcFile(path, useLz);
				file.Read(buffer);
				return file;
			}
		}

		#endregion

		internal GarcFile(string p, bool useLz = false)
		{
			if (useLz)
				GarcData = new LzGarc();
			else
				GarcData = new MemGarc();

			Path = p;
		}

		public int      FileCount => GarcData.FileCount;
		public BaseGarc GarcData  { get; }
		public string   Path      { get; }

		public void Read(byte[] data) => GarcData.Read(data);

		public Task<byte[][]> GetFiles() => GarcData.GetFiles();

		public async Task<byte[]> GetFile(int file) => ( await GetFiles() )[file];

		public Task SetFiles(byte[][] files) => GarcData.SetFiles(files);

		public Task SetFile(int file, byte[] data) => GarcData.SetFile(file, data);

		public Task<byte[]> Write() => GarcData.Write();

		public Task SaveFile() => SaveFileTo(PathUtil.GetPathBase(Path, "RomFS"));

		public async Task SaveFileTo(string path)
		{
			string filename = IO.Path.GetFileName(Path);

			await GarcData.SaveFile();

			using (var fs = new FileStream(IO.Path.Combine(path, filename), FileMode.Create, FileAccess.Write, FileShare.None))
				await fs.WriteAsync(GarcData.Data, 0, GarcData.Data.Length);
		}
	}
}