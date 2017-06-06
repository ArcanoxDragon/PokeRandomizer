using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Randomization.Utility;

namespace CtrDotNet.Pokemon.Randomization.Gen6
{
	public partial class Gen6Randomizer
	{
		public override async Task RandomizeEggMoves()
		{
			var config = this.RandomizerConfig.EggMoves;
			var eggMovesList = await this.Game.GetEggMoves();
			var speciesInfo = ( await this.Game.GetPokemonInfo( edited: true ) ).ToList();
			var moves = ( await this.Game.GetMoves() ).ToList();

			for ( var i = 0; i < eggMovesList.Length; i++ )
			{
				var species = speciesInfo[ i ];
				var eggMoves = eggMovesList[ i ];
				var chooseFrom = moves.ToList();
				bool preferSameType = config.FavorSameType && this.rand.Next( 2 ) == 0;

				if ( eggMoves.Empty || eggMoves.Count == 0 )
					continue;

				if ( preferSameType )
					chooseFrom = chooseFrom.Where( m => species.Types.Any( t => t == m.Type ) ).ToList();

				for ( int m = 0; m < eggMoves.Count; m++ )
				{
					var move = chooseFrom.GetRandom( this.rand );
					eggMoves.Moves[ m ] = (ushort) moves.IndexOf( move );
				}

				eggMovesList[ i ] = eggMoves;
			}

			await this.Game.SaveEggMoves( eggMovesList );
		}
	}
}