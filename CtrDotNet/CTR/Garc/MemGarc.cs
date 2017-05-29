using System;

namespace CtrDotNet.CTR.Garc
{
	public class MemGarc : BaseGarc
	{
		public MemGarc( byte[] data ) : base( data ) { }

		public void WriteFile( byte[] data, int file, int subfile = 0 )
		{
			var entry = this.Def.Fatb.Entries[ file ];
			var subEntry = entry.SubEntries[ subfile ];
			if ( !subEntry.Exists )
				throw new ArgumentException( "SubFile does not exist." );
			var offset = subEntry.Start + this.Def.DataOffset;
			Array.Copy( data, 0, this.Data, offset, data.Length );
		}
	}
}