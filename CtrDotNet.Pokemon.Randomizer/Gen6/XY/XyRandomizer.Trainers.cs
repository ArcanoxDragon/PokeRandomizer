using System;
using System.Linq;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Randomization.Gen6.XY
{
	public partial class XyRandomizer
	{
		private static readonly string[] FriendNames = {
			"Shauna",
			"Serena",
			"Calem",
		};

		public override int MainStarterGen => 6;

		public override (int Slot, int Evolution) GetStarterIndexAndEvolution( int speciesId )
		{
			if ( speciesId.In( Species.Chespin.Id, Species.Chesnaught.Id ) )
				return (0, speciesId - Species.Chespin.Id );
			if ( speciesId.In( Species.Fennekin.Id, Species.Delphox.Id ) )
				return (1, speciesId - Species.Fennekin.Id);
			if ( speciesId.In( Species.Froakie.Id, Species.Greninja.Id ) )
				return (2, speciesId - Species.Froakie.Id);

			return (-1, -1);
		}

		public override bool IsTrainerFriend( string trainerName )
			=> FriendNames.Contains( trainerName, StringComparer.OrdinalIgnoreCase );
	}
}