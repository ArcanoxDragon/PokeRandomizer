using System;
using System.Globalization;
using System.Windows.Data;

namespace PokeRandomizer.UI.ValueConverters
{
	[ ValueConversion( typeof( bool ), typeof( bool ) ) ]
	public class InvertValueConverter : IValueConverter
	{
		public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			if ( !( value is bool from ) )
				throw new NotSupportedException( $"Value must be a boolean" );
			if ( targetType != typeof( bool ) && targetType != typeof( object ) )
				throw new NotSupportedException( $"Cannot convert to type: {targetType.Name}" );

			return !from;
		}

		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
			=> throw new NotSupportedException();
	}
}