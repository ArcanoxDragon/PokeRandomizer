using System;
using System.Windows;

namespace PokeRandomizer.UI.DependencyProperties {
	public partial class Properties
	{
		public static readonly DependencyProperty HintTextProperty
			= DependencyProperty.RegisterAttached( "HintText",
												   typeof( string ),
												   typeof( Properties ) );

		public static void SetHintText( UIElement element, string value ) => element.SetValue( HintTextProperty, value );

		public static string GetHintText( UIElement element )
		{
			object objValue = element.GetValue( HintTextProperty );

			if ( !( objValue is string ) )
				throw new InvalidOperationException( "Property value for property HintText was not a string" );

			return (string) objValue;
		}
	}
}