using System;
using System.Windows;
using PokeRandomizer.Config;

namespace PokeRandomizer.UI.Pages
{
	public partial class RandomizerPage
	{
		public static readonly DependencyProperty ConfigProperty
			= DependencyProperty.Register( "Config",
										   typeof( RandomizerConfig ),
										   typeof( RandomizerPage ) );

		public static void SetConfig( RandomizerPage page, RandomizerConfig value ) => page.SetValue( ConfigProperty, value );

		public static RandomizerConfig GetConfig( RandomizerPage page )
		{
			object objValue = page.GetValue( ConfigProperty );

			if ( !( objValue is RandomizerConfig ) )
				throw new InvalidOperationException( "Property value for property Config was not of type RandomizerConfig" );

			return (RandomizerConfig) objValue;
		}
	}
}