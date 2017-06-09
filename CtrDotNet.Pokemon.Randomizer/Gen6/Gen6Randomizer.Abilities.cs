using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Randomization.Progress;
using CtrDotNet.Pokemon.Randomization.Utility;
using CtrDotNet.Pokemon.Reference;

namespace CtrDotNet.Pokemon.Randomization.Gen6
{
	public partial class Gen6Randomizer
	{
		public override async Task RandomizeAbilities( ProgressNotifier progressNotifier, CancellationToken token )
		{
			progressNotifier?.NotifyUpdate( ProgressUpdate.StatusOnly( "Randomizing Pokémon abilities..." ) );

			var config = this.ValidateAndGetConfig().Abilities;
			var pokeInfoTable = await this.Game.GetPokemonInfo();
			var chooseFrom = Abilities.AllAbilities.Skip( 1 ).ToList();
			var pokeNames = ( await this.Game.GetTextFile( TextNames.SpeciesNames ) ).Lines;

			if ( !config.AllowWonderGuard )
				chooseFrom = chooseFrom.Where( a => a.Id != Abilities.WonderGuard.Id ).ToList();

			// Randomize abilities for each entry in the Info table (which includes all forms)
			for ( int i = 0; i < pokeInfoTable.Table.Length; i++ )
			{
				string name = pokeNames[ pokeInfoTable.GetSpeciesForEntry( i ) ];

				progressNotifier?.NotifyUpdate( ProgressUpdate.Update( $"Randomizing Pokémon abilities...\n{name}", i / (double) pokeInfoTable.Table.Length ) );

				var pokeInfo = pokeInfoTable[ i ];
				var abilities = pokeInfo.Abilities;

				for ( int a = 0; a < pokeInfo.Abilities.Length; a++ )
					abilities[ a ] = (byte) chooseFrom.GetRandom( this.rand ).Id;

				pokeInfo.Abilities = abilities;
				pokeInfoTable[ i ] = pokeInfo;
			}

			await this.Game.SavePokemonInfo( pokeInfoTable );
		}
	}
}