using System;
using System.Globalization;
using System.Windows.Data;

namespace GarcBrowser.ValueConverters
{
    [ ValueConversion( typeof( bool? ), typeof( string ) ) ]
    public class CompressedValueConverter : IValueConverter
    {
        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value == null )
                return "Unknown";

            return ( value is bool b && b ) ? "Yes" : "No";
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
            => throw new NotSupportedException();
    }
}