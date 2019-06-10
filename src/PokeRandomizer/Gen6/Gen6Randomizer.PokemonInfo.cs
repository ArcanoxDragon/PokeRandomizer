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
		public override async Task RandomizePokemonInfo( ProgressNotifier progressNotifier, CancellationToken token )
		{
			var config = this.ValidateAndGetConfig().PokemonInfo;

			if ( !config.RandomizeTypes && !config.RandomizeAbilities )
				return;

			progressNotifier?.NotifyUpdate( ProgressUpdate.StatusOnly( "Randomizing Pokémon personal information..." ) );

			var pokeInfoTable     = await this.Game.GetPokemonInfo();
			var avilableAbilities = Abilities.AllAbilities.ToList();
			var availableTypes    = PokemonTypes.AllPokemonTypes.ToList();
			var pokeNames         = ( await this.Game.GetTextFile( TextNames.SpeciesNames ) ).Lines;

			if ( !config.AllowWonderGuard )
				avilableAbilities = avilableAbilities.Where( a => a.Id != Abilities.WonderGuard.Id ).ToList();

			// Randomize abilities for each entry in the Info table (which includes all forms)
			for ( var i = 0; i < pokeInfoTable.Table.Length; i++ )
			{
				var name         = pokeNames[ pokeInfoTable.GetSpeciesForEntry( i ) ];
				var thisStatus   = $"Randomizing Pokémon personal information...\n{name}";
				var thisProgress = i / (double) pokeInfoTable.Table.Length;

				progressNotifier?.NotifyUpdate( ProgressUpdate.Update( thisStatus, thisProgress ) );

				var pokeInfo = pokeInfoTable[ i ];

				if ( config.RandomizeAbilities )
				{
					progressNotifier?.NotifyUpdate( ProgressUpdate.Update( $"{thisStatus}\nRandomizing abilities...", thisProgress ) );

					var abilities = pokeInfo.Abilities;

					for ( var a = 0; a < abilities.Length; a++ )
						abilities[ a ] = (byte) avilableAbilities.GetRandom( this.Random ).Id;

					pokeInfo.Abilities = abilities;
				}

				if ( config.RandomizeTypes )
				{
					progressNotifier?.NotifyUpdate( ProgressUpdate.Update( $"{thisStatus}\nRandomizing types...", thisProgress ) );

					var types = pokeInfo.Types;

					if ( types.Length > 0 ) // Just to be safe
					{
						if ( config.RandomizePrimaryTypes )
						{
							types[ 0 ] = (byte) availableTypes.GetRandom( this.Random ).Id;
						}

						if ( config.RandomizeSecondaryTypes && types.Length > 1 )
						{
							types[ 1 ] = (byte) availableTypes.Where( type => type.Id != types[ 0 ] )
															  .ToList()
															  .GetRandom( this.Random )
															  .Id;
						}
					}

					pokeInfo.Types = types;
				}

				pokeInfoTable[ i ] = pokeInfo;
			}

			await this.Game.SavePokemonInfo( pokeInfoTable );
		}
	}
}