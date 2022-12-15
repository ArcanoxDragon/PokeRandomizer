using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace PokeRandomizer.UI.Utility
{
	public static class VisualTreeExtensions
	{
		public static IEnumerable<T> GetVisualChildren<T>(this DependencyObject obj) where T : DependencyObject
		{
			if (obj == null)
				yield break;

			for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
			{
				var child = VisualTreeHelper.GetChild(obj, i);

				if (child is T typedChild)
					yield return typedChild;

				foreach (var subChild in child.GetVisualChildren<T>())
					yield return subChild;
			}
		}
	}
}