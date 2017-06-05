using System;
using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Randomization.Reflection
{
	[ AttributeUsage( AttributeTargets.Class, AllowMultiple = true ) ]
	public class HandlesGameAttribute : Attribute
	{
		public HandlesGameAttribute( GameVersion gameVersion )
		{
			this.GameVersion = gameVersion;
		}

		public GameVersion GameVersion { get; }
	}
}