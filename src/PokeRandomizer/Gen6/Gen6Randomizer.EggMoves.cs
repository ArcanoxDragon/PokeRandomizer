using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PokeRandomizer.Common.Reference;
using PokeRandomizer.Progress;
using PokeRandomizer.Utility;

namespace PokeRandomizer.Gen6
{
	public partial class Gen6Randomizer
	{
		public override async Task RandomizeEggMoves( ProgressNotifier progressNotifier, CancellationToken token )
		{
			var config = this.ValidateAndGetConfig().EggMoves;

			if ( !config.RandomizeEggMoves )
				return;

			await this.LogAsync( $"======== Beginning Egg Move randomization ========{Environment.NewLine}" );
			progressNotifier?.NotifyUpdate( ProgressUpdate.StatusOnly( "Randomizing egg moves..." ) );

			var eggMovesList = await this.Game.GetEggMoves();
			var speciesInfo  = await this.Game.GetPokemonInfo( edited: true );
			var moves        = ( await this.Game.GetMoves() ).ToList();
			var pokeNames    = ( await this.Game.GetTextFile( TextNames.SpeciesNames ) ).Lines;
			var moveNames    = ( await this.Game.GetTextFile( TextNames.MoveNames ) ).Lines;

			for ( var i = 0; i < eggMovesList.Length; i++ )
			{
				var name           = pokeNames[ speciesInfo.GetSpeciesForEntry( i ) ];
				var species        = speciesInfo[ i ];
				var eggMoves       = eggMovesList[ i ];
				var chooseFrom     = moves.ToList();
				var preferSameType = config.FavorSameType && this.Random.NextDouble() < (double) config.SameTypePercentage;

				if ( eggMoves.Empty || eggMoves.Count == 0 )
					continue;

				await this.LogAsync( $"{name}:" );
				progressNotifier?.NotifyUpdate( ProgressUpdate.Update( $"Randomizing egg moves...\n{name}", i / (double) eggMovesList.Length ) );

				if ( preferSameType )
					chooseFrom = chooseFrom.Where( m => species.Types.Any( t => t == m.Type ) ).ToList();

				for ( var m = 0; m < eggMoves.Count; m++ )
				{
					var move      = chooseFrom.GetRandom( this.Random );
					var moveIndex = (ushort) moves.IndexOf( move );

					await this.LogAsync( $" - {moveNames[ moveIndex ]}" );
					eggMoves.Moves[ m ] = moveIndex;
				}

				await this.LogAsync();
				eggMovesList[ i ] = eggMoves;
			}

			await this.Game.SaveEggMoves( eggMovesList );
			await this.LogAsync( $"======== Finished Egg Move randomization ========{Environment.NewLine}" );
		}
	}
}