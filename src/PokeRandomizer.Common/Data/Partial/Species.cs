using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace PokeRandomizer.Common.Data
{
	public static partial class Species
	{
		public static IEnumerable<SpeciesType> ValidSpecies => Data.Species.AllSpecies.Where( s => s != Data.Species.Egg );
	}
}