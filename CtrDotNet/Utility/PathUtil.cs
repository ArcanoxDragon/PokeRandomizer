using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CtrDotNet.Utility
{
	public static class PathUtil
	{
		/// <summary>
		/// Returns the base part of the path up until the search string
		/// </summary>
		/// <param name="path">The path to search</param>
		/// <param name="search">A portion of the path to search for to determine the end of the common root</param>
		public static string GetPathBase( string path, string search )
		{
			int searchIndex = path.IndexOf( search, StringComparison.OrdinalIgnoreCase );

			if ( searchIndex < 0 )
				throw new InvalidOperationException( $"Path segment not found: {search}" );

			return path.Substring( 0, searchIndex );
		}
	}
}