using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.Utility.Extensions;

namespace CtrDotNet.CTR.Garc
{
	public abstract class BaseGarc : IGarcFile
	{
		public byte[]  Data      { get; protected set; }
		public GarcDef Def       { get; protected set; }
		public int     FileCount => Def.Fato.EntryCount;

		public virtual Task Read(byte[] data)
		{
			Data = data;
			Def = GarcUtil.UnpackGarc(data);

			return Task.CompletedTask;
		}

		public virtual bool? IsFileCompressed(int file) => false;

		public virtual Task<byte[]> Write() => Task.FromResult(Data);

		public virtual Task<byte[][]> GetFiles()
			=> Task.WhenAll(Enumerable.Range(0, FileCount).Select(i => GetFile(i)));

		public virtual async Task SetFiles(byte[][] files)
		{
			if (files == null || files.Length != FileCount)
				throw new ArgumentException();

			var memGarc = await GarcUtil.PackGarc(files, Def.Version, (int) Def.ContentPadToNearest);
			Def = memGarc.Def;
			Data = memGarc.Data;
		}

		public virtual async Task<byte[]> GetFile(int file, int subfile)
		{
			var entry = Def.Fatb.Entries[file];
			var subfileIndex = subfile >= 0 ? subfile : entry.SubEntries.FindIndex(e => e.Exists);

			if (subfile < 0 && subfileIndex < 0)
				throw new ArgumentException("SubFile does not exist.");

			var subEntry = entry.SubEntries[subfileIndex];

			if (!subEntry.Exists)
				throw new ArgumentException("SubFile does not exist.");

			long offset = subEntry.Start + Def.DataOffset;
			byte[] data = new byte[subEntry.Length];

			using (var mainStr = new MemoryStream(Data))
			{
				mainStr.Seek(offset, SeekOrigin.Begin);
				await mainStr.ReadAsync(data, 0, data.Length);
			}

			return data;
		}

		public Task<byte[]> GetFile(int file) => GetFile(file, -1);

		public virtual async Task SetFile(int file, byte[] data)
		{
			byte[][] files = await GetFiles();
			files[file] = data;
			await SetFiles(files);
		}

		public virtual async Task SaveFile()
		{
			byte[][] data = new byte[FileCount][];

			for (int i = 0; i < data.Length; i++)
			{
				data[i] = await GetFile(i);
			}

			var memGarc = await GarcUtil.PackGarc(data, Def.Version, (int) Def.ContentPadToNearest);
			Def = memGarc.Def;
			Data = memGarc.Data;
		}

		public virtual Task SaveFileTo(string path) => SaveFile();
	}
}