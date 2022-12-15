namespace PokeRandomizer.Common.Structures
{
	public interface IDataStructure
	{
		void Read(byte[] data);
		byte[] Write();
	}
}