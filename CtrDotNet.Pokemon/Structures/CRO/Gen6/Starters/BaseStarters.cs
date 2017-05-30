using System.Collections.Generic;
using System.IO;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Structures.CRO.Gen6.Starters
{
	public abstract class BaseStarters
	{
		#region Static

		public const int NumGens = 6;
		public const int StartersPerGen = 3;

		#endregion

		protected BaseStarters( GameVersion gameVersion )
		{
			this.GameVersion = gameVersion;
			this.StarterSpecies = new ushort[ NumGens ][];
			this.ClearStarters();
		}

		public GameVersion GameVersion { get; }
		public ushort[][] StarterSpecies { get; }

		public IEnumerable<int> Generations => this.GameVersion.IsORAS()
													  ? new[] { 3, 2, 4, 5 }
													  : new[] { 6, 1 };

		protected void ClearStarters() => this.StarterSpecies.Fill( () => new ushort[ StartersPerGen ] );

		public abstract void ReadData( BinaryReader br );
		public abstract void WriteData( BinaryWriter bw );
	}
}