using System;
using System.Linq;
using System.Threading.Tasks;
using PokeRandomizer.Common.Data;
using PokeRandomizer.Common.Structures.RomFS.Gen6;
using PokeRandomizer.Common.Utility;

namespace PokeRandomizer.Gen6.ORAS
{
	public partial class OrasRandomizer
	{
		#region Starters

		public override int MainStarterGen => 3;

		public override (int Slot, int Evolution) GetStarterIndexAndEvolution(int speciesId)
		{
			if (speciesId.InRange(Species.Treecko.Id, Species.Sceptile.Id))
				return ( 0, speciesId - Species.Treecko.Id );
			if (speciesId.InRange(Species.Torchic.Id, Species.Blaziken.Id))
				return ( 1, speciesId - Species.Torchic.Id );
			if (speciesId.InRange(Species.Mudkip.Id, Species.Swampert.Id))
				return ( 2, speciesId - Species.Mudkip.Id );

			return ( -1, -1 );
		}

		#endregion

		#region Trainers

		private static readonly string[] FriendNames = { "Brendan", "May" };

		public override bool IsTrainerFriend(string trainerName)
			=> FriendNames.Contains(trainerName, StringComparer.OrdinalIgnoreCase);

		public override int GetGymId(string trainerName, int trainerId) => trainerName switch {
			// Rustboro
			"Josh"    => 0, // Youngster
			"Tommy"   => 0, // Youngster
			"Georgia" => 0, // Schoolkid
			"Roxanne" => 0, // Leader

			// Dewford
			"Laura"  => 1, // Battle Girl
			"Hideki" => 1, // Black Belt
			"Tessa"  => 1, // Battle Girl
			"Brawly" => 1, // Leader

			// Mauville
			"Kirk"    => 2, // Guitarist
			"Ben"     => 2, // Youngster
			"Vivian"  => 2, // Battle Girl
			"Shawn"   => 2, // Guitarist
			"Wattson" => 2, // Leader

			// Lavaridge
			"Cole"      => 3, // Kindler
			"Hiromichi" => 3, // Ninja Boy
			"Axle"      => 3, // Kindler
			"Sadie"     => 3, // Battle Girl
			"Shoji"     => 3, // Ninja Boy
			"Zane"      => 3, // Ace Trainer
			"Andy"      => 3, // Kindler
			"Flannery"  => 3, // Leader

			// Petalburg
			"Randall" => 4, // Ace Trainer
			"Mary"    => 4, // Ace Trainer
			"Parker"  => 4, // Ace Trainer
			"Lori"    => 4, // Ace Trainer
			"George"  => 4, // Ace Trainer
			"Jody"    => 4, // Ace Trainer
			"Berke"   => 4, // Ace Trainer
			"Norman"  => 4, // Leader

			// Fortree
			"Jared"   => 5, // Bird Keeper
			"Kylee"   => 5, // Picnicker
			"Terrell" => 5, // Camper
			"Will"    => 5, // Bird Keeper
			"Bran"    => 5, // Bird Keeper
			"Winona"  => 5, // Leader

			// Mossdeep
			"Preston"     => 6, // 
			"Joshua"      => 6, // 
			"Fritz"       => 6, // 
			"Kindra"      => 6, // 
			"Patricia"    => 6, // 
			"Virgil"      => 6, // 
			"Liza & Tate" => 6, // Leaders

			// Sootopolis
			"Marissa"                       => 7, // 1F Poké Fan
			"Crissy"                        => 7, // 1F Lass
			"Olivia"                        => 7, // BF Beauty
			"Brianna"                       => 7, // BF Lady
			"Connie"                        => 7, // BF Beauty
			"Bridget"                       => 7, // BF Beauty
			"Tiffany"                       => 7, // BF Beauty
			"Andrea"                        => 7, // BF Lass
			"Wallace" when trainerId == 572 => 7, // 1F Leader (pre-E4)

			_ => -1
		};

		public override Task<bool> HandleTrainerSpecificLogicAsync(Random taskRandom, string trainerName, TrainerData trainer) => Task.FromResult(false);

		#endregion
	}
}