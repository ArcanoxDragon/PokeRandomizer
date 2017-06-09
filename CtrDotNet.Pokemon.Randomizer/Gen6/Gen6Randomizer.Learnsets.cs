using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Randomization.Progress;
using CtrDotNet.Pokemon.Randomization.Utility;
using CtrDotNet.Pokemon.Reference;
using Move = CtrDotNet.Pokemon.Structures.RomFS.Common.Move;

namespace CtrDotNet.Pokemon.Randomization.Gen6
{
	public partial class Gen6Randomizer
	{
		public override async Task RandomizeLearnsets( ProgressNotifier progressNotifier, CancellationToken token )
		{
			progressNotifier?.NotifyUpdate( ProgressUpdate.StatusOnly( "Randomizing learnsets..." ) );

			var config = this.ValidateAndGetConfig().Learnsets;
			var learnsets = ( await this.Game.GetLearnsets() ).ToList();
			var speciesInfo = await this.Game.GetPokemonInfo( edited: true );
			var moves = ( await this.Game.GetMoves() ).ToList();
			var pokeNames = ( await this.Game.GetTextFile( TextNames.SpeciesNames ) ).Lines;

			for ( int i = 0; i < learnsets.Count; i++ )
			{
				string name = pokeNames[ speciesInfo.GetSpeciesForEntry( i ) ];
				progressNotifier?.NotifyUpdate( ProgressUpdate.Update( $"Randomizing learnsets...\n{name}", i / (double) learnsets.Count ) );

				bool preferSameType;
				var learnset = learnsets[ i ];
				var species = speciesInfo[ i ];
				var chooseFrom = moves;
				var chooseFromSameType = chooseFrom.Where( mv => species.HasType( PokemonTypes.GetValueFrom( mv.Type ) ) ).ToList();
				Move move;

				for ( int m = 0; m < learnset.Moves.Length; m++ )
				{
					preferSameType = config.FavorSameType && this.rand.Next( 2 ) == 0;
					move = ( preferSameType ? chooseFromSameType : chooseFrom ).GetRandom( this.rand );
					learnset.Moves[ m ] = (ushort) moves.IndexOf( move );
				}

				if ( config.RandomizeLevels )
					learnset.Levels = learnset.Levels
											  .Select( _ => (ushort) this.rand.Next( 1, MathUtil.Clamp( config.LearnAllMovesBy, 10, 100 ) ) )
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

					move = firstMoveChoose.GetRandom( this.rand );
					learnset.Moves[ 0 ] = (ushort) moves.IndexOf( move );

					if ( config.AtLeast4Moves )
					{
						if ( learnset.Levels.Length < 4 )
							learnset.Levels = new ushort[ 4 ];
						if ( learnset.Moves.Length < 4 )
							learnset.Moves = new ushort[ 4 ];

						// Make sure every Pokemon has at least 4 moves at level 1
						for ( int m = 1; m < 4; m++ )
						{
							preferSameType = config.FavorSameType && this.rand.Next( 2 ) == 0;
							move = ( preferSameType ? chooseFromSameType : chooseFrom ).GetRandom( this.rand );
							learnset.Levels[ m ] = 1;
							learnset.Moves[ m ] = (ushort) moves.IndexOf( move );
						}
					}

					// Go down the list and make sure there are no duplicates
					for ( int m = learnset.Moves.Length - 1; m >= 0; m-- )
					{
						while ( Array.IndexOf( learnset.Moves, learnset.Moves[ m ], 0, m ) >= 0 )
						{
							preferSameType = config.FavorSameType && this.rand.Next( 2 ) == 0;
							move = ( preferSameType ? chooseFromSameType : chooseFrom ).GetRandom( this.rand );
							learnset.Moves[ m ] = (ushort) moves.IndexOf( move );
						}
					}
				}

				learnsets[ i ] = learnset;
			}

			await this.Game.SaveLearnsets( learnsets );
		}
	}
}