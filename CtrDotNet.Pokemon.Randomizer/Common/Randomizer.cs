using System;
using System.Linq;
using System.Reflection;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Randomization.Reflection;

namespace CtrDotNet.Pokemon.Randomization.Common
{
	public static class Randomizer
	{
		public static IRandomizer GetRandomizerFor( GameVersion version )
		{
			var randomizers = from asm in AppDomain.CurrentDomain.GetAssemblies()
							  from type in asm.GetTypes()
							  where typeof( IRandomizer ).IsAssignableFrom( type )
							  let attrs = type.GetCustomAttributes<HandlesGameAttribute>()
							  where attrs.Any()
							  select (type, attrs);

			foreach ( var (type, attrs) in randomizers )
			{
				if ( attrs.Any( attr => attr.GameVersion == version ) )
				{
					var instance = Activator.CreateInstance( type ) as IRandomizer;

					if ( instance == null )
						throw new ApplicationException( $"Could not invoke instance of {type.Name} as an IRandomizer" );

					return instance;
				}
			}

			throw new NotSupportedException( $"No loaded IRandomizer is able to support the game version {version}" );
		}
	}
}