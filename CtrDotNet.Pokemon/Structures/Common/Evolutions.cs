namespace CtrDotNet.Pokemon.Structures.Common
{
	public class EvolutionMethod
	{
		public int Method { get; set; }
		public int Species { get; set; }
		public int Argument { get; set; }
		public int Form { get; set; } = -1;
		public int Level { get; set; }
	}

	public abstract class BaseEvolutionSet : IDataStructure
	{
		public EvolutionMethod[] PossibleEvolutions { get; protected set; }
		public int Size => this.EntrySize * this.EntryCount;

		protected abstract int EntryCount { get; }
		protected abstract int EntrySize { get; }

		public abstract void Read( byte[] data );
		public abstract byte[] Write();
	}
}