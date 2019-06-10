using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CtrDotNet.Utility.Extensions;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Game;
using PokeRandomizer.Common.Reference;
using PokeRandomizer.Progress;
using PokeRandomizer.Utility;
using Species = PokeRandomizer.Common.Data.Partial.Species;

namespace PokeRandomizer.Gen6
{
	public partial class Gen6Randomizer
	{
		public override async Task RandomizeTrainers( ProgressNotifier progressNotifier, CancellationToken token )
		{
			var config = this.ValidateAndGetConfig().Trainers;

			if ( !config.RandomizeTrainers )
				return;

			progressNotifier?.NotifyUpdate( ProgressUpdate.StatusOnly( "Randomizing trainer teams..." ) );

			var trainers              = ( await this.Game.GetTrainerData() ).ToList();
			var trainerNames          = await this.Game.GetTextFile( TextNames.TrainerNames, languageOverride: Language.English );
			var localizedTrainerNames = await this.Game.GetTextFile( TextNames.TrainerNames );
			var evolutions            = ( await this.Game.GetEvolutions() ).ToList();
			var availableSpecies      = Species.ValidSpecies.ToList();
			var availableTypes        = PokemonTypes.AllPokemonTypes.ToList();

			// Get already-edited versions of the info so that we get the shuffled version
			var starters  = await this.Game.GetStarters( edited: true );
			var pokeInfo  = await this.Game.GetPokemonInfo( edited: true );
			var learnsets = ( await this.Game.GetLearnsets( edited: true ) ).ToList();

			// Decide a type for each gym for use later on if the user has chosen to type-theme entire gyms
			var gymTypes = Enumerable.Range( 0, 8 ).Select( gymId => availableTypes.GetRandom( this.Random ) ).ToArray();

			ushort GetEvolutionOf( ushort speciesId, int evoIndex )
			{
				if ( evoIndex == 0 )
					return speciesId;

				var evoSet   = evolutions[ speciesId ];
				var possible = evoSet.PossibleEvolutions.Where( e => e.Species > 0 ).ToArray();

				if ( possible.Length == 0 )
					return speciesId;

				var evo = possible.GetRandom( this.Random );

				return GetEvolutionOf( (ushort) evo.Species, evoIndex - 1 );
			}

			var friendStarterGender  = new[] { -1, -1, -1 };
			var friendStarterAbility = new[] { -1, -1, -1 };

			foreach ( var (i, trainer) in trainers.Pairs() )
			{
				var name          = i < trainerNames.LineCount ? trainerNames[ i ] : "UNKNOWN";
				var localizedName = i < localizedTrainerNames.LineCount ? localizedTrainerNames[ i ] : "UNKNOWN";

				progressNotifier?.NotifyUpdate( ProgressUpdate.Update( $"Randomizing trainer teams...\n{localizedName}", i / (double) trainers.Count ) );

				var isFriend   = this.IsTrainerFriend( name );
				var gymId      = this.GetGymId( name, i );
				var chooseFrom = availableSpecies;

				if ( config.TypeThemed )
				{
					PokemonType type;

					if ( config.TypeThemedGyms && gymId >= 0 )
						type = gymTypes[ gymId ];
					else
						type = availableTypes.GetRandom( this.Random );

					chooseFrom = chooseFrom.Where( s => pokeInfo[ s.Id ].HasType( type ) ).ToList();
				}

				// If there's no item specified for the trainer, randomize the move list instead for more variety
				// Otherwise, the game will pick moves at random
				if ( !trainer.Item )
					trainer.Moves = true;

				foreach ( var pokemon in trainer.Team )
				{
					if ( config.FriendKeepsStarter && isFriend && this.IsStarter( pokemon.Species ) )
					{
						// Figure out which battle scenario we're in (one per starter per story-battle with friend)
						// Then, get the starter from the slot this scenario represents and figure out
						// if it needs to be evolved at all
						var (starterSlot, starterEvo) = this.GetStarterIndexAndEvolution( pokemon.Species );
						var newStarterBaseSpecies = starters.StarterSpecies[ this.MainStarterGen - 1 ][ starterSlot ];
						var newStarter            = GetEvolutionOf( newStarterBaseSpecies, starterEvo );
						var info                  = pokeInfo[ newStarter ];

						pokemon.Species = newStarter;

						if ( friendStarterGender[ starterSlot ] < 0 )
							friendStarterGender[ starterSlot ] = info.GetRandomGender();

						if ( friendStarterAbility[ starterSlot ] < 0 )
							friendStarterAbility[ starterSlot ] = info.Abilities.GetRandom( this.Random );

						pokemon.Gender  = friendStarterGender[ starterSlot ];
						pokemon.Ability = friendStarterAbility[ starterSlot ];
					}
					else
					{
						// Just pick a random species, making sure it conforms to any specified criteria
						pokemon.Species = (ushort) this.GetRandomSpecies( chooseFrom ).Id;

						var info = pokeInfo[ pokemon.Species ];

						pokemon.Gender  = info.GetRandomGender();
						pokemon.Ability = info.Abilities.GetRandom( this.Random );
					}

					pokemon.Level = (ushort) MathUtil.Clamp( (int) ( pokemon.Level * config.LevelMultiplier ), 2, 100 );

					// Fill the Pokemon's available moves with random choices from its learnset (up to its current level)
					var moveset = learnsets[ pokemon.Species ].GetPossibleMoves( pokemon.Level ).ToList();

					if ( trainer.Moves )
					{
						pokemon.HasItem  = false;
						pokemon.HasMoves = true;
						pokemon.Moves    = new ushort[] { 0, 0, 0, 0 };
						for ( var m = 0; m < Math.Min( moveset.Count, 4 ); m++ )
							pokemon.Moves[ m ] = moveset.Except( pokemon.Moves ).ToList().GetRandom( this.Random );
					}
				}
			}

			await this.Game.SaveTrainerData( trainers );
		}

		#region Starter Methods

		public abstract int MainStarterGen { get; }
		public abstract (int Slot, int Evolution) GetStarterIndexAndEvolution( int speciesId );
		public bool IsStarter( int speciesId ) => this.GetStarterIndexAndEvolution( speciesId ).Slot >= 0;

		#endregion

		#region Trainer Methods

		public abstract bool IsTrainerFriend( string trainerName );
		public abstract int GetGymId( string trainerName, int trainerId );

		#endregion
	}
}