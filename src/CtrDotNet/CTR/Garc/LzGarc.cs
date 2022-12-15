using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CtrDotNet.CTR.Garc
{
	public class LzGarc : BaseGarc
	{
		#region Static

		private sealed class Entry
		{
			public bool   Accessed      { get; set; }
			public bool   Saved         { get; set; }
			public byte[] Data          { get; set; }
			public bool   WasCompressed { get; set; }

			public async Task Read(byte[] data)
			{
				Data = data;
				Accessed = true;

				if (data.Length == 0)
					return;

				if (data[0] != 0x11)
					return;

				try
				{
					using (var inputStream = new MemoryStream(data))
					using (var outputStream = new MemoryStream())
					{
						await Lzss.Decompress(inputStream, data.Length, outputStream);

						Data = outputStream.ToArray();
						WasCompressed = true;
					}
				}
				catch
				{
					// ignored
				}
			}

			public async Task<byte[]> Save()
			{
				if (!WasCompressed)
					return Data;

				byte[] data;

				try
				{
					using (MemoryStream inputStream = new MemoryStream(Data))
					using (MemoryStream outputStream = new MemoryStream())
					{
						await Lzss.Compress(inputStream, Data.Length, outputStream, original: true);

						data = outputStream.ToArray();
					}
				}
				catch
				{
					data = Array.Empty<byte>();
				}

				return data;
			}
		}

		#endregion

		private Entry[] storage;

		internal LzGarc() { }

		public override bool? IsFileCompressed(int file) => this.storage[file]?.WasCompressed;

		public override async Task Read(byte[] data)
		{
			await base.Read(data);
			this.storage = new Entry[FileCount];

			for (int i = 0; i < FileCount; i++)
			{
				this.storage[i] = new Entry();
				await this.storage[i].Read(await base.GetFile(i, -1));
			}
		}

		public override Task<byte[]> GetFile(int fileIndex, int subfileIndex)
			=> Task.FromResult(this.storage[fileIndex].Data);

		public override Task<byte[][]> GetFiles()
			=> Task.WhenAll(Enumerable.Range(0, FileCount).Select(i => GetFile(i)));

		public override async Task SetFiles(byte[][] files)
		{
			if (files.Length != FileCount)
				throw new NotSupportedException("Cannot change number of entries");

			for (int i = 0; i < files.Length; i++)
			{
				if (this.storage[i] == null)
					this.storage[i] = new Entry();

				this.storage[i].Data = files[i];
			}

			await base.SetFiles(await Task.WhenAll(this.storage.Select(e => e.Save())));
		}

		public override async Task SetFile(int file, byte[] data)
		{
			if (file < 0 || file >= FileCount)
				throw new ArgumentOutOfRangeException(nameof(file), "File index not valid");

			if (this.storage[file] == null)
				await GetFile(file);

			// ReSharper disable once PossibleNullReferenceException
			this.storage[file].Data = data;

			await base.SetFile(file, await this.storage[file].Save());
		}

		public override async Task SaveFile()
		{
			byte[][] data = await Task.WhenAll(this.storage.Select((s, i) => s.Save()));

			var memGarc = await GarcUtil.PackGarc(data, Def.Version, (int) Def.ContentPadToNearest);
			Def = memGarc.Def;
			Data = memGarc.Data;
		}
	}
}