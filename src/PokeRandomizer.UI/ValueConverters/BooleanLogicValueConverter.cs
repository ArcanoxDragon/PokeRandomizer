using System;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace PokeRandomizer.UI.ValueConverters
{
	public class BooleanLogicValueConverter : IMultiValueConverter
	{
		public enum LogicalOperation
		{
			And,
			Or,
		}

		public LogicalOperation Operation { get; set; }

		public object Convert( object[] values, Type targetType, object parameter, CultureInfo culture )
		{
			if ( targetType != typeof( bool ) && targetType != typeof( object ) )
				throw new ArgumentException( "Target type must be a boolean", nameof(targetType) );

			var bools = values.Select( v => v is bool b && b );

			switch ( this.Operation )
			{
				case LogicalOperation.And:
					return bools.Aggregate( true, ( a, b ) => a && b );
				case LogicalOperation.Or:
					return bools.Aggregate( false, ( a, b ) => a || b );
				default:
					throw new NotSupportedException();
			}
		}

		public object[] ConvertBack( object value, Type[] targetTypes, object parameter, CultureInfo culture )
		{
			throw new NotSupportedException();
		}
	}
}