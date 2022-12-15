using System.IO;
using System.Threading.Tasks;

namespace CtrDotNet.Utility
{
	static class StreamUtil
	{
		private static readonly byte[] SingleByteBuffer = new byte[1];

		public static async Task<byte> ReadByteAsync(this Stream stream)
		{
			await stream.ReadAsync(SingleByteBuffer, 0, 1);
			return SingleByteBuffer[0];
		}

		public static async Task WriteByteAsync(this Stream stream, byte val)
		{
			SingleByteBuffer[0] = val;
			await stream.WriteAsync(SingleByteBuffer, 0, 1);
		}
	}
}