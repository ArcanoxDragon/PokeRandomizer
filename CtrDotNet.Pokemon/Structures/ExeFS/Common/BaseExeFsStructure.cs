using System;
using System.IO;
using System.Linq;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Structures.ExeFS.Common
{
	public abstract class BaseExeFsStructure
	{
		protected BaseExeFsStructure( GameVersion gameVersion )
		{
			this.GameVersion = gameVersion;
		}

		public GameVersion GameVersion { get; }
		protected abstract byte[] Signature { get; }
		protected abstract int Length { get; }
		protected virtual bool IncludeSignature => true;

		public void Read( byte[] exeData )
		{
			var matches = exeData.Find( this.Signature, 0x400000 ).ToList();

			if ( !matches.Any() )
				throw new InvalidOperationException( "Could not find signature in binary file" );

			int offset = matches.First();

			if ( !this.IncludeSignature )
				offset += this.Signature.Length;

			byte[] data = exeData.Skip( offset ).Take( this.Length ).ToArray();

			using ( var ms = new MemoryStream( data ) )
			using ( var br = new BinaryReader( ms ) )
			{
				this.ReadData( br );
			}
		}

		public void Write( byte[] exeData )
		{
			var matches = exeData.Find( this.Signature, 0x400000 ).ToList();

			if ( !matches.Any() )
				throw new InvalidOperationException( "Could not find signature in binary file" );

			int offset = matches.First();

			if ( !this.IncludeSignature )
				offset += this.Signature.Length;

			byte[] data;

			using ( var ms = new MemoryStream() )
			using ( var bw = new BinaryWriter( ms ) )
			{
				this.WriteData( bw );
				data = ms.ToArray();
			}

			Array.Copy( data, 0, exeData, offset, data.Length );
		}

		protected abstract void ReadData( BinaryReader br );
		protected abstract void WriteData( BinaryWriter bw );
	}
}