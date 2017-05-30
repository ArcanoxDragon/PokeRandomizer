using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Structures.RomFS.Common
{
	public class EvolutionMethod
	{
		public int Method { get; set; }
		public int Species { get; set; }
		public int Argument { get; set; }
		public int Form { get; set; } = -1;
		public int Level { get; set; }
	}

	public abstract class BaseEvolutionSet : BaseDataStructure
	{
		public EvolutionMethod[] PossibleEvolutions { get; protected set; }
		public int Size => this.EntrySize * this.EntryCount;

		protected abstract int EntryCount { get; }
		protected abstract int EntrySize { get; }

		protected BaseEvolutionSet( GameVersion gameVersion ) : base( gameVersion ) { }
	}
}