using System;
using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Structures.ExeFS.Common
{
	public abstract class BaseExeFsStructure : BaseDataStructure
	{
		protected BaseExeFsStructure( GameVersion gameVersion ) : base( gameVersion ) { }

		public abstract byte[] Signature { get; }
		public abstract int Length { get; }
		public virtual bool IncludeSignature => true;

		public override void Read( byte[] data )
		{
			if ( data.Length != this.Length )
				throw new ArgumentOutOfRangeException( nameof(data), $"Wrong data length. Expected {this.Length} bytes, but got {data.Length}." );

			base.Read( data );
		}
	}
}