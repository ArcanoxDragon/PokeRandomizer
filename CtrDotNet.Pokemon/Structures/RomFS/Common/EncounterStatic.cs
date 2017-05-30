using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Structures.RomFS.Common
{
	public abstract class EncounterStatic : BaseDataStructure
	{
		public abstract ushort Species { get; set; }
		public abstract short HeldItem { get; set; }

		protected EncounterStatic( GameVersion gameVersion ) : base( gameVersion ) { }
	}
}