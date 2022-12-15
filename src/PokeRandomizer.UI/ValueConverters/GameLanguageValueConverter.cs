using System;
using System.Globalization;
using System.Windows.Data;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.UI.ValueConverters
{
	[ValueConversion(typeof(Language), typeof(string))]
	public class GameLanguageValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!targetType.IsAssignableFrom(typeof(string)))
				throw new NotSupportedException("Can only convert to the String type");

			if (value == null)
				return "None";

			try
			{
				var language = (Language) value;
				return language.GetDisplayName();
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException($"Value must be of type Language (was: {value})");
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotSupportedException();
	}
}