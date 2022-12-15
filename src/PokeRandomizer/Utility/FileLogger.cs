using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PokeRandomizer.Utility
{
	public class FileLogger : ILogger, IDisposable
	{
		private readonly FileStream fileStream;

		public FileLogger(string fileName, Encoding encoding)
		{
			FileName = fileName;
			Encoding = encoding;

			this.fileStream = File.Open(FileName, FileMode.Create, FileAccess.Write, FileShare.Read);
		}

		public FileLogger(string fileName) : this(fileName, Encoding.UTF8) { }

		public string   FileName { get; }
		public Encoding Encoding { get; }

		public async Task WriteAsync(string text)
		{
			var bytes = Encoding.GetBytes(text);

			await this.fileStream.WriteAsync(bytes, 0, bytes.Length);
		}

		public Task WriteLineAsync(string text = "") => WriteAsync($"{text}{Environment.NewLine}");

		public Task FlushAsync() => this.fileStream.FlushAsync(CancellationToken.None);

		public void Dispose()
		{
			this.fileStream?.Dispose();
		}
	}
}