using System;
using System.Globalization;
using System.Windows.Data;

namespace PokeRandomizer.UI.ValueConverters
{
	[ ValueConversion( typeof( string ), typeof( bool ) ) ]
	public class StringNullOrEmptyValueConverter : IValueConverter
	{
		public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			if ( !( value is string str ) )
				throw new NotSupportedException( "Can only convert from strings" );

			if ( targetType != typeof( bool ) )
				throw new NotSupportedException( "Can only convert to booleans" );

			return string.IsNullOrEmpty( str );
		}

		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
		{
			throw new NotSupportedException();
		}
	}
}