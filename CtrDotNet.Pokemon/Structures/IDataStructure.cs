namespace CtrDotNet.Pokemon.Structures
{
	public interface IDataStructure
	{
		void Read( byte[] data );
		byte[] Write();
	}
}