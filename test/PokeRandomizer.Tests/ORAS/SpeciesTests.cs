using System.Linq;
using System.Threading.Tasks;
using NUnit.Framework;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Data.Extensions;
using PokeRandomizer.Common.Structures.ExeFS.Common;
using PokeRandomizer.Common.Structures.RomFS.PokemonInfo;

namespace PokeRandomizer.Tests.ORAS
{
	[TestFixture]
	public class SpeciesTests
	{
		[Test]
		public async Task Bulbasaur()
		{
			PokemonInfoTable pokeInfo = await ORASConfig.GameConfig.GetPokemonInfo();
			TmsHms tmsHms = await ORASConfig.GameConfig.GetTmsHms();
			PokemonInfo nullInfo = pokeInfo[0];
			PokemonInfo bulbasaurInfo = pokeInfo[Species.Bulbasaur];

			Assert.AreNotEqual(nullInfo, bulbasaurInfo, "Species info for Bulbasaur is the same as Null/Egg");

			Assert.AreEqual(6.9, bulbasaurInfo.WeightKg, "Species weight for Bulbasaur doesn't match the Pokedex!");
			Assert.AreEqual(0.7, bulbasaurInfo.HeightM, "Species height for Bulbasaur doesn't match the Pokedex!");
			Assert.AreEqual(PokemonTypes.Grass, PokemonTypes.GetValueFrom(bulbasaurInfo.Types[0]), "Bulbasaur's primary type should be Grass!");
			Assert.AreEqual(PokemonTypes.Poison, PokemonTypes.GetValueFrom(bulbasaurInfo.Types[1]), "Bulbasaur's secondary type should be Poison!");

			await TestContext.Out.WriteLineAsync("Bulbasaur can learn the following TMs:");

			foreach (var (_, tm) in bulbasaurInfo.TmHm
												 .Select((b, i) => ( b, i ))
												 .Where(t => t.Item1))
			{
				string type = tm >= tmsHms.TmIds.Length ? "HM" : "TM";
				int num = ( type == "HM" ) ? ( tm - 100 + 1 ) : ( tm + 1 );
				await TestContext.Out.WriteLineAsync($"\t{type} {num}: {tmsHms.GetMove(tm).Name}");
			}
		}
	}
}