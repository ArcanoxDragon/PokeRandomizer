using System;
using System.Globalization;
using System.Windows.Data;

namespace GarcBrowser.ValueConverters
{
    [ ValueConversion( typeof( int ), typeof( string ) ) ]
    [ ValueConversion( typeof( long ), typeof( string ) ) ]
    public class HexConverter : IValueConverter
    {
        #region Implementation of IValueConverter

        public object Convert( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( targetType != typeof( string ) )
                throw new NotSupportedException();

            switch ( value )
            {
                case int i:
                    return $"0x{i:X8}";
                case long l:
                    return $"0x{l:X8}";
                default:
                    throw new NotSupportedException();
            }
        }

        public object ConvertBack( object value, Type targetType, object parameter, CultureInfo culture )
        {
            if ( !( value is string str ) )
                throw new NotSupportedException();

            if ( targetType == typeof( int ) )
                return int.Parse( str );
            if ( targetType == typeof( long ) )
                return long.Parse( str );

            throw new NotSupportedException();
        }

        #endregion
    }
}