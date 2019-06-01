using System.Collections.Generic;
using System.IO;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.ExeFS.Common
{
	public class TmsHms : BaseExeFsStructure
	{
		private const int TmHmIdSize = 2;

		public TmsHms( GameVersion gameVersion ) : base( gameVersion )
		{
			const int numTms = 100;
			int numHms = gameVersion.IsGen6()
							 ? ( gameVersion.IsORAS() ? 7 : 5 )
							 : 0;

			this.TmIds = new ushort[ numTms + 1 ];
			this.HmIds = new ushort[ numHms + 1 ];
		}

		public override byte[] Signature => this.GameVersion.IsGen6()
												? new byte[] { 0xD4, 0x00, 0xAE, 0x02, 0xAF, 0x02, 0xB0, 0x02 }
												: new byte[] { 0x03, 0x40, 0x03, 0x41, 0x03, 0x42, 0x03, 0x43, 0x03 };

		public override int Length => ( this.TmIds.Length + this.HmIds.Length ) * TmHmIdSize;
		public override bool IncludeSignature => false;

		public ushort[] TmIds { get; private set; }
		public ushort[] HmIds { get; private set; }

		protected override void ReadData( BinaryReader br )
		{
			List<ushort> tmIds = new List<ushort>();
			List<ushort> hmIds = new List<ushort>();

			// Add first 92 TMs
			for ( int i = 0; i < 92; i++ )
				tmIds.Add( br.ReadUInt16() );

			if ( this.GameVersion.IsGen6() )
				// Add first 5 HMs
				for ( int i = 0; i < 5; i++ )
					hmIds.Add( br.ReadUInt16() );

			if ( this.GameVersion.IsORAS() )
				// Add another HM
				hmIds.Add( br.ReadUInt16() );

			// Add 8 more TMs
			for ( int i = 0; i < 8; i++ )
				tmIds.Add( br.ReadUInt16() );

			if ( this.GameVersion.IsORAS() )
				// Add the last HM
				hmIds.Add( br.ReadUInt16() );

			this.TmIds = tmIds.ToArray();
			this.HmIds = hmIds.ToArray();
		}

		protected override void WriteData( BinaryWriter bw )
		{
			// Write first 92 TMs
			for ( int i = 0; i < 92; i++ )
				bw.Write( this.TmIds[ i ] );

			if ( this.GameVersion.IsGen6() )
				// Write first 5 HMs
				for ( int i = 0; i < 5; i++ )
					bw.Write( this.HmIds[ i ] );

			if ( this.GameVersion.IsORAS() )
				// Add 6th HM
				bw.Write( this.HmIds[ 5 ] );

			// Add rest of TMs
			for ( int i = 92; i < this.TmIds.Length; i++ )
				bw.Write( this.TmIds[ i ] );

			if ( this.GameVersion.IsORAS() )
				// Add the last HM
				bw.Write( this.HmIds[ 6 ] );
		}
	}
}