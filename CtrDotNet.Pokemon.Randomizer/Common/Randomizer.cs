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
		public static IRandomizer GetRandomizer( GameConfig game, RandomizerConfig config, int? seed = null )
		{
			var randomizers = from asm in AppDomain.CurrentDomain.GetAssemblies()
							  from type in asm.GetTypes()
							  where typeof( BaseRandomizer ).IsAssignableFrom( type )
							  let attrs = type.GetCustomAttributes<HandlesGameAttribute>()
							  where attrs.Any()
							  select ( type, attrs );

			foreach ( var (type, attrs) in randomizers )
			{
				if ( attrs.Any( attr => attr.GameVersion == game.Version ) )
				{
					object objInstance;

					if ( seed == null )
					{
						objInstance = Activator.CreateInstance( type );
					}
					else
					{
						objInstance = Activator.CreateInstance( type, seed );
					}

					if ( !( objInstance is BaseRandomizer instance ) )
						throw new ApplicationException( $"Could not create instance of {type.Name} as a BaseRandomizer" );

					instance.Initialize( game, config );

					return instance;
				}
			}

			throw new NotSupportedException( $"There is no loaded randomizer which supports the game version {game.Version}" );
		}
	}
}