using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Reference;
using PokeRandomizer.Progress;
using PokeRandomizer.Utility;
using Move = PokeRandomizer.Common.Structures.RomFS.Common.Move;

namespace PokeRandomizer.Gen6
{
	public partial class Gen6Randomizer
	{
		public override async Task RandomizeLearnsets( ProgressNotifier progressNotifier, CancellationToken token )
		{
			var config = this.ValidateAndGetConfig().Learnsets;

			if ( !config.RandomizeLearnsets )
				return;

			await this.LogAsync( $"======== Beginning Learnset randomization ========{Environment.NewLine}" );
			progressNotifier?.NotifyUpdate( ProgressUpdate.StatusOnly( "Randomizing learnsets..." ) );

			var learnsets         = ( await this.Game.GetLearnsets() ).ToList();
			var speciesInfo       = await this.Game.GetPokemonInfo( edited: true );
			var moves             = ( await this.Game.GetMoves() ).ToList();
			var pokeNames         = ( await this.Game.GetTextFile( TextNames.SpeciesNames ) ).Lines;
			var moveNames         = ( await this.Game.GetTextFile( TextNames.MoveNames ) ).Lines;
			var maxMoveNameLength = moveNames.Max( n => n.Length );

			for ( var i = 0; i < learnsets.Count; i++ )
			{
				var name = pokeNames[ speciesInfo.GetSpeciesForEntry( i ) ];

				await this.LogAsync( $"{name}:" );
				progressNotifier?.NotifyUpdate( ProgressUpdate.Update( $"Randomizing learnsets...\n{name}", i / (double) learnsets.Count ) );

				var learnset           = learnsets[ i ];
				var species            = speciesInfo[ i ];
				var chooseFrom         = moves.Skip( 1 ).ToList(); // Always skip the first move (it's the Null move and breaks the game)
				var chooseFromSameType = chooseFrom.Where( mv => species.HasType( PokemonTypes.GetValueFrom( mv.Type ) ) ).ToList();

				ushort PickRandomMove()
				{
					var preferSameType = config.FavorSameType && this.Random.NextDouble() < (double) config.SameTypePercentage;
					var move           = ( preferSameType ? chooseFromSameType : chooseFrom ).GetRandom( this.Random );

					return (ushort) moves.IndexOf( move );
				}

				for ( var m = 0; m < learnset.Moves.Length; m++ )
				{
					learnset.Moves[ m ] = PickRandomMove();
				}

				if ( config.RandomizeLevels )
					learnset.Levels = learnset.Levels
											  .Select( _ => (ushort) this.Random.Next( 1, MathUtil.Clamp( config.LearnAllMovesBy, 10, 100 ) ) )
											  .OrderBy( l => l )
											  .ToArray();

				if ( learnset.Levels.Length > 0 )
				{
					// Make sure there's always at least one move on the Pokemon
					learnset.Levels[ 0 ] = 1;

					// Pick a random STAB/Normal attack move
					var firstMoveChoose = moves.Where( m => m.Type == PokemonTypes.Normal.Id || species.HasType( PokemonTypes.GetValueFrom( m.Type ) ) )
											   .Where( m => m.Category == Move.CategoryPhysical || m.Category == Move.CategorySpecial )
											   .ToList();
					var move = firstMoveChoose.GetRandom( this.Random );

					learnset.Moves[ 0 ] = (ushort) moves.IndexOf( move );

					if ( config.AtLeast4Moves )
					{
						if ( learnset.Levels.Length < 4 )
							learnset.Levels = new ushort[ 4 ];
						if ( learnset.Moves.Length < 4 )
							learnset.Moves = new ushort[ 4 ];

						// Make sure every Pokemon has at least 4 moves at level 1
						for ( var m = 1; m < 4; m++ )
						{
							learnset.Levels[ m ] = 1;
							learnset.Moves[ m ]  = PickRandomMove();
						}
					}

					// Go down the list and make sure there are no duplicates
					for ( var m = learnset.Moves.Length - 1; m >= 0; m-- )
					{
						while ( Array.IndexOf( learnset.Moves, learnset.Moves[ m ], 0, m ) >= 0 )
						{
							learnset.Moves[ m ] = PickRandomMove();
						}
					}
				}

				for ( var m = 0; m < learnset.Moves.Length; m++ )
				{
					await this.LogAsync( $"  - {moveNames[ learnset.Moves[ m ] ].PadLeft( maxMoveNameLength )} @ Lv. {learnset.Levels[ m ]}" );
				}

				await this.LogAsync();
				learnsets[ i ] = learnset;
			}

			await this.Game.SaveLearnsets( learnsets );
			await this.LogAsync( $"======== Finished Learnset randomization ========{Environment.NewLine}" );
		}
	}
}