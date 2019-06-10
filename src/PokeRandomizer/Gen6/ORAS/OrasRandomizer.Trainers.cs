using System;
using System.Linq;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Utility;

namespace PokeRandomizer.Gen6.ORAS
{
	public partial class OrasRandomizer
	{
		#region Starters

		public override int MainStarterGen => 3;

		public override (int Slot, int Evolution) GetStarterIndexAndEvolution( int speciesId )
		{
			if ( speciesId.In( Species.Treecko.Id, Species.Sceptile.Id ) )
				return ( 0, speciesId - Species.Treecko.Id );
			if ( speciesId.In( Species.Torchic.Id, Species.Blaziken.Id ) )
				return ( 1, speciesId - Species.Torchic.Id );
			if ( speciesId.In( Species.Mudkip.Id, Species.Swampert.Id ) )
				return ( 2, speciesId - Species.Mudkip.Id );

			return ( -1, -1 );
		}

		#endregion

		#region Trainers

		private static readonly string[] FriendNames = {
			"Brendan",
			"May"
		};

		public override bool IsTrainerFriend( string trainerName )
			=> FriendNames.Contains( trainerName, StringComparer.OrdinalIgnoreCase );

		public override int GetGymId( string trainerName, int trainerId )
		{
			switch ( trainerName )
			{
				// Rustboro
				case "Josh":    // Youngster
				case "Tommy":   // Youngster
				case "Georgia": // Schoolkid
				case "Roxanne": // Leader
					return 0;
				// Dewford
				case "Laura":  // Battle Girl
				case "Hideki": // Black Belt
				case "Tessa":  // Battle Girl
				case "Brawly": // Leader
					return 1;
				// Mauville
				case "Kirk":    // Guitarist
				case "Ben":     // Youngster
				case "Vivian":  // Battle Girl
				case "Shawn":   // Guitarist
				case "Wattson": // Leader
					return 2;
				// Lavaridge
				case "Cole":      // Kindler
				case "Hiromichi": // Ninja Boy
				case "Axle":      // Kindler
				case "Sadie":     // Battle Girl
				case "Shoji":     // Ninja Boy
				case "Zane":      // Ace Trainer
				case "Andy":      // Kindler
				case "Flannery":  // Leader
					return 3;
				// Petalburg
				case "Randall": // Ace Trainer
				case "Mary":    // Ace Trainer
				case "Parker":  // Ace Trainer
				case "Lori":    // Ace Trainer
				case "George":  // Ace Trainer
				case "Jody":    // Ace Trainer
				case "Berke":   // Ace Trainer
				case "Norman":  // Leader
					return 4;
				// Fortree
				case "Jared":   // Bird Keeper
				case "Kylee":   // Picnicker
				case "Terrell": // Camper
				case "Will":    // Bird Keeper
				case "Bran":    // Bird Keeper
				case "Winona":  // Leader
					return 5;
				// Mossdeep
				case "Preston":     // 
				case "Joshua":      // 
				case "Fritz":       // 
				case "Kindra":      // 
				case "Patricia":    // 
				case "Virgil":      // 
				case "Liza & Tate": // Leaders
					return 6;
				// Sootopolis
				case "Marissa":                       // 1F Poké Fan
				case "Crissy":                        // 1F Lass
				case "Olivia":                        // BF Beauty
				case "Brianna":                       // BF Lady
				case "Connie":                        // BF Beauty
				case "Bridget":                       // BF Beauty
				case "Tiffany":                       // BF Beauty
				case "Andrea":                        // BF Lass
				case "Wallace" when trainerId == 572: // 1F Leader (pre-E4)
					return 7;
				default:
					return -1;
			}
		}

		#endregion
	}
}