using System;
using System.Linq;
using CtrDotNet.Pokemon.Data;
using CtrDotNet.Pokemon.Utility;

namespace CtrDotNet.Pokemon.Randomization.Gen6.ORAS
{
	public partial class OrasRandomizer
	{
		private static readonly string[] FriendNames = {
			"Brendan",
			"May"
		};

		public override int MainStarterGen => 3;

		public override (int Slot, int Evolution) GetStarterIndexAndEvolution( int speciesId )
		{
			if ( speciesId.In( Species.Treecko.Id, Species.Sceptile.Id ) )
				return (0, speciesId - Species.Treecko.Id );
			if ( speciesId.In( Species.Torchic.Id, Species.Blaziken.Id ) )
				return (1, speciesId - Species.Torchic.Id);
			if ( speciesId.In( Species.Mudkip.Id, Species.Swampert.Id ) )
				return (2, speciesId - Species.Mudkip.Id);

			return (-1, -1);
		}

		public override bool IsTrainerFriend( string trainerName )
			=> FriendNames.Contains( trainerName, StringComparer.OrdinalIgnoreCase );
	}
}