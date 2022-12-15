using System;
using System.Globalization;
using System.Windows.Data;

namespace PokeRandomizer.UI.ValueConverters
{
	public class PercentageValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType != typeof(double) &&
			    targetType != typeof(float) &&
			    targetType != typeof(decimal) &&
			    targetType != typeof(int) &&
			    targetType != typeof(long) &&
			    targetType != typeof(short) &&
			    targetType != typeof(byte) &&
			    targetType != typeof(double?) &&
			    targetType != typeof(float?) &&
			    targetType != typeof(decimal?) &&
			    targetType != typeof(int?) &&
			    targetType != typeof(long?) &&
			    targetType != typeof(short?) &&
			    targetType != typeof(byte?) &&
			    targetType != typeof(object))
				throw new NotSupportedException($"Converting to type {targetType.Name} is not supported");

			switch (value)
			{
				case decimal d:
					return d * 100;
				case double d:
					return d * 100;
				case float f:
					return f * 100;
				default:
					return value;
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (targetType != typeof(double) &&
			    targetType != typeof(float) &&
			    targetType != typeof(decimal) &&
			    targetType != typeof(int) &&
			    targetType != typeof(long) &&
			    targetType != typeof(short) &&
			    targetType != typeof(byte) &&
			    targetType != typeof(double?) &&
			    targetType != typeof(float?) &&
			    targetType != typeof(decimal?) &&
			    targetType != typeof(int?) &&
			    targetType != typeof(long?) &&
			    targetType != typeof(short?) &&
			    targetType != typeof(byte?) &&
			    targetType != typeof(object))
				throw new NotSupportedException($"Converting to type {targetType.Name} is not supported");

			switch (value)
			{
				case int i:
					return i / 100.0;
				case long l:
					return l / 100.0;
				case short s:
					return s / 100.0;
				case byte b:
					return b / 100.0;
				default:
					return value;
			}
		}
	}
}