using System;
using System.Globalization;
using System.Windows.Data;

namespace GarcBrowser.ValueConverters
{
    [ ValueConversion( typeof( long ), typeof( string ) ) ]
    public class FileSizeConverter : IValueConverter
    {
        private static readonly string[] Units = { "B", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };

        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( value == null )
                return null;

            double size = (long) value;
            int unit = 0;

            while ( size >= 1024 )
            {
                size /= 1024;
                ++unit;
            }

            return $"{size:0.#} {Units[ unit ]}";
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
            => throw new NotSupportedException();
    }
}