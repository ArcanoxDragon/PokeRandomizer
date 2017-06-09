using System;
using System.Globalization;
using System.Windows.Data;

namespace CtrDotNet.Pokemon.Randomization.UI.ValueConverters
{
	[ ValueConversion( typeof( bool ), typeof( bool ) ) ]
	public class InvertValueConverter : IValueConverter
	{
		public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
		{
			if ( targetType != typeof( bool ) )
				throw new NotSupportedException( $"Cannot convert to type: {targetType.Name}" );

			bool? from = value as bool?;

			if ( from == null )
				throw new ArgumentNullException( nameof(value), "Cannot convert a null value" );

			return !from;
		}

		public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
			=> throw new NotSupportedException();
	}
}