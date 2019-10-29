using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Reference;
using PokeRandomizer.Progress;
using PokeRandomizer.Utility;

namespace PokeRandomizer.Gen6
{
	public partial class Gen6Randomizer
	{
		public override async Task RandomizeEggMoves( Random taskRandom, ProgressNotifier progressNotifier, CancellationToken token )
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
				var name               = pokeNames[ speciesInfo.GetSpeciesForEntry( i ) ];
				var species            = speciesInfo[ i ];
				var eggMoves           = eggMovesList[ i ];
				var chooseFrom         = moves.ToList(); // Clone list
				var chooseFromSameType = chooseFrom.Where( mv => species.HasType( PokemonTypes.GetValueFrom( mv.Type ) ) ).ToList();
				var preferSameType     = config.FavorSameType && taskRandom.NextDouble() < (double) config.SameTypePercentage;

				ushort PickRandomMove()
				{
					var move   = ( preferSameType ? chooseFromSameType : chooseFrom ).GetRandom( taskRandom );
					var moveId = (ushort) moves.IndexOf( move );

					// We have to make sure the "-----" move is not picked because it breaks the game. Ideally
					// we would just exclude it from the list of considered moves, but to ensure seed-compatiblity
					// with the previous version of the randomizer, we need to keep the list of moves exactly
					// the same when we pull a random one. We then replace any "-----" choices with the first real
					// move.
					if ( moveId == 0 )
						moveId++;

					return moveId;
				}

				if ( eggMoves.Empty || eggMoves.Count == 0 )
					continue;

				await this.LogAsync( $"{name}:" );
				progressNotifier?.NotifyUpdate( ProgressUpdate.Update( $"Randomizing egg moves...\n{name}", i / (double) eggMovesList.Length ) );

				for ( var m = 0; m < eggMoves.Count; m++ )
				{
					eggMoves.Moves[ m ] = PickRandomMove();
					await this.LogAsync( $" - {moveNames[ eggMoves.Moves[ m ] ]}" );
				}

				await this.LogAsync();
				eggMovesList[ i ] = eggMoves;
			}

			await this.Game.SaveEggMoves( eggMovesList );
			await this.LogAsync( $"======== Finished Egg Move randomization ========{Environment.NewLine}" );
		}
	}
}