using System.Threading.Tasks;

namespace CtrDotNet.CTR
{
    public interface IWritableFile
    {
	    Task<byte[]> Write();
	    Task SaveFile();
	    Task SaveFileTo( string path );
    }
}
