using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace PokeRandomizer.Common.Data
{
	public static partial class Species
	{
		public static IEnumerable<SpeciesType> ValidSpecies => AllSpecies.Where(s => s != Egg);
	}
}