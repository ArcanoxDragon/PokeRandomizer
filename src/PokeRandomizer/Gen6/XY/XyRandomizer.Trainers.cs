using System;
using System.Linq;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Utility;

namespace PokeRandomizer.Gen6.XY
{
	public partial class XyRandomizer
	{
		#region Starters

		public override int MainStarterGen => 6;

		public override (int Slot, int Evolution) GetStarterIndexAndEvolution( int speciesId )
		{
			if ( speciesId.In( Species.Chespin.Id, Species.Chesnaught.Id ) )
				return ( 0, speciesId - Species.Chespin.Id );
			if ( speciesId.In( Species.Fennekin.Id, Species.Delphox.Id ) )
				return ( 1, speciesId - Species.Fennekin.Id );
			if ( speciesId.In( Species.Froakie.Id, Species.Greninja.Id ) )
				return ( 2, speciesId - Species.Froakie.Id );

			return ( -1, -1 );
		}

		#endregion

		#region Trainers

		private static readonly string[] FriendNames = {
			"Shauna",
			"Serena",
			"Calem",
		};

		public override bool IsTrainerFriend( string trainerName )
			=> FriendNames.Contains( trainerName, StringComparer.OrdinalIgnoreCase );

		public override int GetGymId( string trainerName, int trainerId )
		{
			switch ( trainerName )
			{
				// Santalune
				case "David":     // Youngster
				case "Zachary":   // Youngster
				case "Charlotte": // Lass
				case "Viola":     // Leader
					return 0;
				// Cyllage
				case "Didier":  // Rising Star
				case "Manon":   // Rising Star
				case "Craig":   // Hiker
				case "Bernard": // Hiker
				case "Grant":   // Leader
					return 1;
				// Shalour
				case "Shun":    // Roller Skater
				case "Dash":    // Roller Skater
				case "Rolanda": // Roller Skater
				case "Kate":    // Roller Skater
				case "Korrina": // Leader
					return 2;
				// Coumarine
				case "Chaise":  // Pokémon Ranger
				case "Brooke":  // Pokémon Ranger
				case "Maurice": // Pokémon Ranger
				case "Twiggy":  // Pokémon Ranger
				case "Ramos":   // Leader
					return 3;
				// Lumiose
				case "Arno":     // 2F Schoolboy
				case "Sherlock": // 2F Schoolboy
				case "Finnian":  // 2F Schoolboy
				case "Estel":    // 3F Rising Star
				case "Nelly":    // 3F Rising Star
				case "Helene":   // 3F Rising Star
				case "Mathis":   // 4F Ace Trainer
				case "Maxim":    // 4F Ace Trainer
				case "Rico":     // 4F Ace Trainer
				case "Abigail":  // 5F Poké Fan
				case "Lydie":    // 5F Poké Fan
				case "Tara":     // 5F Poké Fan
				case "Clemont":  // 6F Leader
					return 4;
				// Laverre
				case "Kali":      // Furisode Girl
				case "Linnea":    // Furisode Girl
				case "Blossom":   // Furisode Girl
				case "Katherine": // Furisode Girl
				case "Valerie":   // Leader
					return 5;
				// Anistar
				case "Paschal": // Psychic
				case "Harry":   // Psychic
				case "Arthur":  // Psychic
				case "Arachna": // Hex Maniac
				case "Melanie": // Hex Maniac
				case "Olympia": // Leader
					return 6;
				// Showbelle
				case "Imelda":  // Ace Trainer
				case "Viktor":  // Ace Trainer
				case "Shannon": // Ace Trainer
				case "Theo":    // Ace Trainer
				case "Wulfric": // Leader
					return 7;
				default:
					return -1;
			}
		}

		#endregion
	}
}