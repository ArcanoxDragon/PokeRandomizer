using CtrDotNet.Pokemon.GameData;

namespace CtrDotNet.Pokemon.Dynamic
{
	public static class EnumExtensions
	{
		private static string GetString( GameConfig game, TextName text, int id )
		{
			string[] strings = game.GameTextStrings[ game.GetGameText( text ).Index ];

			if ( id < 0 || id >= strings.Length )
				return "<Invalid>";

			return strings[ id ];
		}

		public static string GetDisplayName( this Species species, GameConfig game ) => GetString( game, TextName.SpeciesNames, (int) species );
		public static string GetDisplayName( this Moves move, GameConfig game ) => GetString( game, TextName.MoveNames, (int) move );
		public static string GetDisplayName( this Items item, GameConfig game ) => GetString( game, TextName.ItemNames, (int) item );
		public static string GetDisplayName( this Abilities ability, GameConfig game ) => GetString( game, TextName.AbilityNames, (int) ability );
	}
}