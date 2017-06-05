using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Randomization.Utility;

namespace CtrDotNet.Pokemon.Randomization.Gen6
{
	public partial class Gen6Randomizer
	{
		public override async Task RandomizeLearnsets()
		{
			var config = this.RandomizerConfig.Learnsets;
			var learnsets = ( await this.Game.GetLearnsets() ).ToList();
			var speciesInfo = ( await this.Game.GetPokemonInfo() ).ToList();
			var moves = ( await this.Game.GetMoves() ).ToList();

			foreach ( var (learnset, i) in learnsets.Select( ( l, i ) => (l, i) ) )
			{
				var species = speciesInfo[ i ];
				var chooseFrom = moves.ToList();
				bool preferSameType = config.FavorSameType && this.rand.Next( 2 ) == 0;

				if ( preferSameType )
					chooseFrom = chooseFrom.Where( m => species.Types.Any( t => t == (int) m.Type ) ).ToList();

				for ( int m = 0; m < learnset.Moves.Length; m++ )
				{
					var move = chooseFrom.GetRandom( this.rand );
					learnset.Moves[ m ] = moves.IndexOf( move );
				}

				if ( config.RandomizeLevels )
					learnset.Levels = learnset.Levels
											  .Select( _ => this.rand.Next( 5, 100 ) )
											  .OrderBy( l => l )
											  .ToArray();
			}

			await this.Game.SaveLearnsets( learnsets );
		}
	}
}