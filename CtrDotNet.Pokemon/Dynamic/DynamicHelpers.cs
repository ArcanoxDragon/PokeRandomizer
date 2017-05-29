using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CtrDotNet.Pokemon.Dynamic
{
	public class DynamicEnumHelper
	{
		private struct DataItem
		{
			public int Id;
			public string Name;
			public string SafeName;
		}

		private readonly string pathRoot;

		public DynamicEnumHelper( string pathRoot )
		{
			this.pathRoot = pathRoot;
		}

		public string EmitEnum( string singleName, string pluralName )
		{
			var sb = new StringBuilder();
			var lineRegex = new Regex( @"^(\d+)=(.+)$" );
			var enumItems = File.ReadAllLines( Path.Combine( this.pathRoot, $"../Data/{pluralName}.db" ) );
			var added = new List<string>();
			var items = new List<DataItem>();

			foreach ( var line in enumItems )
			{
				var match = lineRegex.Match( line );
				var id = int.Parse( match.Groups[ 1 ].Value );
				var name = match.Groups[ 2 ].Value;
				string final;

				var val = name.Replace( '♀', 'F' );
				val = val.Replace( '♂', 'M' );
				val = Regex.Replace( val, @"[^a-zA-Z0-9]+", "" );

				if ( Regex.IsMatch( val, @"^\d" ) )
					val = "_" + val;

				if ( string.IsNullOrWhiteSpace( val ) )
				{
					items.Add( new DataItem { Id = -1 } );
					continue;
				}

				final = val;

				if ( added.Contains( final ) )
					final += "_" + ( added.Count( s => s == val ) + 1 );

				items.Add( new DataItem {
					Id = id,
					Name = name,
					SafeName = final
				} );
				added.Add( val );
			}

			sb.AppendLine( $"public sealed class {singleName} : Base{singleName} {{" );
			sb.AppendLine( $"    public {singleName}( int id, string name ) : base( id, name ) {{ }}" );
			sb.AppendLine( $"    public static explicit operator {singleName}( int id ) => {pluralName}.GetValue( id );" );
			sb.AppendLine( $"    public static explicit operator int( {singleName} val ) => val.Id;" );
			sb.AppendLine( $"}}" );

			sb.AppendLine( $"public static class {pluralName} {{" );

			foreach ( var item in items.Where( item => item.Id >= 0 ) )
			{
				sb.AppendLine( $"public static {singleName} {item.SafeName} = new {singleName}( {item.Id}, \"{item.Name}\" );" );
			}

			sb.AppendLine( $"public static {singleName} GetValue( int id ) => staticValues[ id ];" );
			sb.AppendLine( $"private static {singleName}[] staticValues = {{ {string.Join( ",\n\t", items.Select( i => i.Id >= 0 ? i.SafeName : "null" ) )} }};" );
			sb.AppendLine( $"}}" );

			return sb.ToString();
		}
	}
}