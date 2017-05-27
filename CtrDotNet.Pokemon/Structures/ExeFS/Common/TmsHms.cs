using System.Collections.Generic;
using System.IO;
using CtrDotNet.Pokemon.Dynamic;
using CtrDotNet.Pokemon.GameData;

namespace CtrDotNet.Pokemon.Structures.ExeFS.Common
{
	public class TmsHms : BaseExeFsStructure
	{
		private const int TmHmIdSize = 2;

		public TmsHms( GameConfig gameConfig ) : base( gameConfig )
		{
			const int numTms = 100;
			int numHms = gameConfig.Version.IsORAS() ? 7 : 5;

			this.TmIds = new ushort[ numTms + 1 ];
			this.HmIds = new ushort[ numHms + 1 ];
		}

		protected override byte[] Signature => new byte[] { 0xD4, 0x00, 0xAE, 0x02, 0xAF, 0x02, 0xB0, 0x02 };
		protected override int Length => ( this.TmIds.Length + this.HmIds.Length ) * TmHmIdSize;
		protected override bool IncludeSignature => false;

		public ushort[] TmIds { get; private set; }
		public ushort[] HmIds { get; private set; }

		public Moves GetMove( int tmHm ) => tmHm >= this.TmIds.Length
												? (Moves) this.HmIds[ tmHm - this.TmIds.Length ]
												: (Moves) this.TmIds[ tmHm ];

		protected override void ReadData( BinaryReader br )
		{
			List<ushort> tmIds = new List<ushort>();
			List<ushort> hmIds = new List<ushort>();

			// Add first 92 TMs
			for ( int i = 0; i < 92; i++ )
				tmIds.Add( br.ReadUInt16() );

			// Add first 5 HMs
			for ( int i = 0; i < 5; i++ )
				hmIds.Add( br.ReadUInt16() );

			if ( this.GameConfig.Version.IsORAS() )
				// Add another HM
				hmIds.Add( br.ReadUInt16() );

			// Add 8 more TMs
			for ( int i = 0; i < 8; i++ )
				tmIds.Add( br.ReadUInt16() );

			if ( this.GameConfig.Version.IsORAS() )
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

			// Write first 5 HMs
			for ( int i = 0; i < 5; i++ )
				bw.Write( this.HmIds[ i ] );

			if ( this.GameConfig.Version.IsORAS() )
				// Add 6th HM
				bw.Write( this.HmIds[ 5 ] );

			// Add rest of TMs
			for ( int i = 92; i < this.TmIds.Length; i++ )
				bw.Write( this.TmIds[ i ] );

			if ( this.GameConfig.Version.IsORAS() )
				// Add the last HM
				bw.Write( this.HmIds[ 6 ] );
		}
	}
}