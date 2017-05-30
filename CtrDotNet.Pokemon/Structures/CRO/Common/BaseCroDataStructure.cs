using System.IO;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Structures.CRO.Common
{
	public abstract class BaseCroDataStructure : BaseDataStructure
	{
		protected BaseCroDataStructure( GameVersion gameVersion ) : base( gameVersion ) { }

		protected abstract int EntryCount { get; }
		protected abstract int EntrySize { get; }
		protected virtual int EntryOffset => 0;

		protected virtual void ReadEntry( int entry, BinaryReader br ) { }
		protected virtual void WriteEntry( int entry, BinaryWriter bw ) { }

		protected override void ReadData( BinaryReader br )
		{
			br.BaseStream.Seek( this.EntryOffset, SeekOrigin.Begin );

			for ( int i = 0; i < this.EntryCount; i++ )
			{
				byte[] entry = new byte[ this.EntrySize ];
				br.Read( entry, 0, entry.Length );

				int entryIndex = i;
				entry.WithReader( er => this.ReadEntry( entryIndex, er ) );
			}
		}

		protected override void WriteData( BinaryWriter bw )
		{
			bw.BaseStream.Seek( this.EntryOffset, SeekOrigin.Begin );

			for ( int i = 0; i < this.EntryCount; i++ )
			{
				using ( var ms = new MemoryStream() )
				using ( var ew = new BinaryWriter( ms ) )
				{
					this.WriteEntry( i, ew );
					byte[] entry = ms.ToArray();

					if ( entry.Length != this.EntrySize )
						throw new InvalidDataException( $"Entry size mismatch. Expected {this.EntrySize} but got {entry.Length}." );

					bw.Write( entry, 0, entry.Length );
				}
			}
		}
	}
}