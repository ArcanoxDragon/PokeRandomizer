using System;
using System.IO;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Structures.CRO.Gen6.Starters
{
	public class StartersField : BaseStarters
	{
		#region Static

		public const int CodeSectionOffsetORAS = 0xF8EEC;
		public const int CodeSectionOffsetXY = 0xF7EDC;
		public const int EntrySizeXY = 0x18;
		public const int EntrySizeORAS = 0x24;

		#endregion

		public StartersField( GameVersion gameVersion ) : base( gameVersion ) { }

		private int CodeSectionOffset => this.GameVersion.IsORAS() ? CodeSectionOffsetORAS : CodeSectionOffsetXY;
		private int EntrySize => this.GameVersion.IsORAS() ? EntrySizeORAS : EntrySizeXY;

		private int GetOffsetForGen( int gen )
		{
			switch ( gen )
			{
				case 1:
					return 3;
				case 2:
					return 28;
				case 4:
					return 31;
				case 5:
					return 34;
				default:
					return 0;
			}
		}

		/// <param name="br">Reader for the .code section of the DllPoke3Select file</param>
		public override void ReadData( BinaryReader br )
		{
			this.Generations.ForEach( ( gen, i ) => {
				int entryGroupOffset = this.GetOffsetForGen( gen );

				for ( int entry = 0; entry < StartersPerGen; entry++ )
				{
					int entryOffset = entryGroupOffset + entry;

					br.BaseStream.Seek( this.CodeSectionOffset + ( entryOffset * this.EntrySize ), SeekOrigin.Begin );

					this.StarterSpecies[ gen - 1 ][ entry ] = br.ReadUInt16();
				}
			} );
		}

		/// <param name="bw">Writer for the .code section of the DllPoke3Select file</param>
		public override void WriteData( BinaryWriter bw )
		{
			this.Generations.ForEach( ( gen, i ) => {
				int entryGroupOffset = this.GetOffsetForGen( gen );

				for ( int entry = 0; entry < StartersPerGen; entry++ )
				{
					int entryOffset = entryGroupOffset + entry;

					bw.BaseStream.Seek( this.CodeSectionOffset + ( entryOffset * this.EntrySize ), SeekOrigin.Begin );

					bw.Write( this.StarterSpecies[ gen - 1 ][ entry ] );
				}
			} );
		}
	}
};