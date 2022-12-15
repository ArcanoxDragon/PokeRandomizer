using System;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.Common.Structures.ExeFS.Common
{
	public abstract class BaseExeFsStructure : BaseDataStructure
	{
		protected BaseExeFsStructure(GameVersion gameVersion) : base(gameVersion) { }

		public abstract byte[] Signature        { get; }
		public abstract int    Length           { get; }
		public virtual  bool   IncludeSignature => true;

		public override void Read(byte[] data)
		{
			if (data.Length != Length)
				throw new ArgumentOutOfRangeException(nameof(data), $"Wrong data length. Expected {Length} bytes, but got {data.Length}.");

			base.Read(data);
		}
	}
}