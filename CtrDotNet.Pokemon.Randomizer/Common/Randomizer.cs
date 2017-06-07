using System;
using System.Linq;
using System.Reflection;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Randomization.Config;
using CtrDotNet.Pokemon.Randomization.Reflection;

namespace CtrDotNet.Pokemon.Randomization.Common
{
	public static class Randomizer
	{
		public static IRandomizer GetRandomizer( GameConfig game, RandomizerConfig config )
		{
			var randomizers = from asm in AppDomain.CurrentDomain.GetAssemblies()
							  from type in asm.GetTypes()
							  where typeof( BaseRandomizer ).IsAssignableFrom( type )
							  let attrs = type.GetCustomAttributes<HandlesGameAttribute>()
							  where attrs.Any()
							  select (type, attrs);

			foreach ( var (type, attrs) in randomizers )
			{
				if ( attrs.Any( attr => attr.GameVersion == game.Version ) )
				{
					var instance = Activator.CreateInstance( type ) as BaseRandomizer;

					if ( instance == null )
						throw new ApplicationException( $"Could not create instance of {type.Name} as a BaseRandomizer" );

					instance.Initialize( game, config );

					return instance;
				}
			}

			throw new NotSupportedException( $"No loaded IRandomizer is able to support the game version {game.Version}" );
		}
	}
}