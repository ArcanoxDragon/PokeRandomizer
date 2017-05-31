using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Game;
using CtrDotNet.Pokemon.Randomizer.Config;

namespace CtrDotNet.Pokemon.Randomizer.Common
{
	public abstract class BaseRandomizer : IRandomizer
	{
		protected readonly Random rand;

		protected BaseRandomizer( GameConfig game, RandomizerConfig randomizerConfig )
		{
			this.rand = new Random();

			this.Game = game;
			this.RandomizerConfig = randomizerConfig;
		}

		public GameConfig Game { get; }
		public RandomizerConfig RandomizerConfig { get; }

		public abstract Task RandomizeStarters();

		#region Helpers

		public SpeciesType GetRandomSpecies( Func<IEnumerable<SpeciesType>, IEnumerable<SpeciesType>> selector = null, IEnumerable<SpeciesType> excluded = null )
		{
			selector = selector ?? ( s => s );
			excluded = excluded ?? Enumerable.Empty<SpeciesType>();

			IEnumerable<SpeciesType> available = Species.AllSpecies
														.Where( st => st.Id <= this.Game.Version.GetInfo().SpeciesCount )
														.Except( excluded );

			IList<SpeciesType> chooseFrom = selector( available ).ToList();

			if ( chooseFrom.Count <= 0 )
				throw new InvalidDataException( "No species available matching the given constraints" );

			return chooseFrom.Skip( this.rand.Next( chooseFrom.Count ) ).First();
		}

		#endregion
	}
}