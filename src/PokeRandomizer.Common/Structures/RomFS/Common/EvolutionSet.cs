using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.RomFS.Common
{
	public class EvolutionMethod
	{
		public int Method { get; set; }
		public int Species { get; set; }
		public int Argument { get; set; }
		public int Form { get; set; } = -1;
		public int Level { get; set; }
	}

	public abstract class EvolutionSet : BaseDataStructure
	{
		#region Static

		public static EvolutionSet New( GameVersion version )
		{
			switch ( version.GetGeneration() )
			{
				case GameGeneration.Generation7:
					return new Gen7.EvolutionSet( version );
				default:
					return new Gen6.EvolutionSet( version );
			}
		}

		#endregion

		public EvolutionMethod[] PossibleEvolutions { get; protected set; }
		public int Size => this.EntrySize * this.EntryCount;

		protected abstract int EntryCount { get; }
		protected abstract int EntrySize { get; }

		protected EvolutionSet( GameVersion gameVersion ) : base( gameVersion ) { }
	}
}