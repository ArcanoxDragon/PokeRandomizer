namespace CtrDotNet.Pokemon.Structures.RomFS.Common
{
	public interface IDataStructure
	{
		void Read( byte[] data );
		byte[] Write();
	}
}