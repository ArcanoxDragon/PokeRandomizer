using System;
using System.Globalization;
using System.Windows.Data;
using PokeRandomizer.Common.Game;

namespace PokeRandomizer.UI.ValueConverters
{
	[ValueConversion(typeof(GameVersion), typeof(string))]
	public class GameVersionValueConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (!targetType.IsAssignableFrom(typeof(string)))
				throw new NotSupportedException("Can only convert to the String type");

			if (value == null)
				return "None";

			try
			{
				var version = (GameVersion) value;
				return version.GetDisplayName();
			}
			catch (InvalidCastException)
			{
				throw new ArgumentException($"Value must be of type GameVersion (was: {value})");
			}
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
			=> throw new NotSupportedException();
	}
}