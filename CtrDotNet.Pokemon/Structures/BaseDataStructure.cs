using System.IO;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Structures
{
	public abstract class BaseDataStructure : IDataStructure
	{
		protected BaseDataStructure( GameVersion gameVersion )
		{
			this.GameVersion = gameVersion;
		}

		public GameVersion GameVersion { get; }

		public virtual void Read( byte[] data )
		{
			data.WithReader( this.ReadData );
		}

		public virtual byte[] Write()
		{
			using ( var ms = new MemoryStream() )
			using ( var bw = new BinaryWriter( ms ) )
			{
				this.WriteData( bw );
				return ms.ToArray();
			}
		}

		protected virtual void ReadData( BinaryReader br ) { }
		protected virtual void WriteData( BinaryWriter bw ) { }
	}
}