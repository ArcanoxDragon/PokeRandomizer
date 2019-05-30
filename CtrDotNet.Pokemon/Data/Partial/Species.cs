using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace CtrDotNet.Pokemon.Data
{
	public static partial class Species
	{
		public static IEnumerable<SpeciesType> ValidSpecies => AllSpecies.Where( s => s != Species.Egg );
	}
}