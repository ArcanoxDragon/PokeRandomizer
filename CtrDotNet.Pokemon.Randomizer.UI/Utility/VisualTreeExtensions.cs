using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace CtrDotNet.Pokemon.Randomization.UI.Utility
{
	public static class VisualTreeExtensions
	{
		public static IEnumerable<T> GetVisualChildren<T>( this DependencyObject obj ) where T : DependencyObject
		{
			if ( obj == null )
				yield break;

			for ( int i = 0; i < VisualTreeHelper.GetChildrenCount( obj ); i++ )
			{
				var child = VisualTreeHelper.GetChild( obj, i );

				if ( child != null )
				{
					if ( child is T )
						yield return (T) child;

					foreach ( var subChild in child.GetVisualChildren<T>() )
						yield return subChild;
				}
			}
		}
	}
}