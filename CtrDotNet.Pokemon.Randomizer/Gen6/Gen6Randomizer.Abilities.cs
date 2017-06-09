using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Randomization.Utility;

namespace CtrDotNet.Pokemon.Randomization.Gen6
{
	public partial class Gen6Randomizer
	{
		public override async Task RandomizeAbilities()
		{
			var config = this.ValidateAndGetConfig().Abilities;
			var pokeInfoTable = await this.Game.GetPokemonInfo();
			var chooseFrom = Abilities.AllAbilities.Skip( 1 ).ToList();

			if ( !config.AllowWonderGuard )
				chooseFrom = chooseFrom.Where( a => a.Id != Abilities.WonderGuard.Id ).ToList();

			// Randomize abilities for each entry in the Info table (which includes all forms)
			for ( int i = 0; i < pokeInfoTable.Table.Length; i++ )
			{
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