using System.Threading.Tasks;

namespace CtrDotNet.CTR.Garc
{
	public interface IGarcFile
	{
		Task<byte[][]> GetFiles();
		Task<byte[]> GetFile( int file );
		Task SetFiles( byte[][] files );
		Task SetFile( int file, byte[] data );
		Task Save();
	}
}