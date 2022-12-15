using System.Linq;

namespace PokeRandomizer.Common.Reference
{
	public static class ReferenceLists
	{
		public static class Garc
		{
			public static readonly GarcReference[] Xy = {
				new(005, GarcNames.MoveSprites),
				new(012, GarcNames.EncounterData),
				new(031, GarcNames.AmxFiles),
				new(038, GarcNames.TrainerData),
				new(039, GarcNames.TrainerClasses),
				new(040, GarcNames.TrainerPokemon),
				new(041, GarcNames.MapGrid),
				new(042, GarcNames.MapMatrix),
				new(082, GarcNames.Scripts),
				new(104, GarcNames.Wallpapers),
				new(165, GarcNames.TitleScreen),
				new(203, GarcNames.MaisonPokemonN),
				new(204, GarcNames.MaisonTrainerN),
				new(205, GarcNames.MaisonPokemonS),
				new(206, GarcNames.MaisonTrainerS),
				new(212, GarcNames.Moves),
				new(213, GarcNames.EggMoves),
				new(214, GarcNames.Learnsets),
				new(215, GarcNames.Evolutions),
				new(216, GarcNames.MegaEvolutions),
				new(218, GarcNames.PokemonInfo),
				new(220, GarcNames.Items),

				// Varied
				new(072, GarcNames.GameText, true),
				new(080, GarcNames.StoryText, true),
			};

			public static readonly GarcReference[] Oras = {
				new(013, GarcNames.EncounterData),
				new(029, GarcNames.AmxFiles),
				new(036, GarcNames.TrainerData),
				new(037, GarcNames.TrainerClasses),
				new(038, GarcNames.TrainerPokemon),
				new(039, GarcNames.MapGrid),
				new(040, GarcNames.MapMatrix),
				new(103, GarcNames.Wallpapers),
				new(152, GarcNames.TitleScreen),
				new(182, GarcNames.MaisonPokemonN),
				new(183, GarcNames.MaisonTrainerN),
				new(184, GarcNames.MaisonPokemonS),
				new(185, GarcNames.MaisonTrainerS),
				new(189, GarcNames.Moves),
				new(190, GarcNames.EggMoves),
				new(191, GarcNames.Learnsets),
				new(192, GarcNames.Evolutions),
				new(193, GarcNames.MegaEvolutions),
				new(195, GarcNames.PokemonInfo),
				new(196, GarcNames.EggSpecies),
				new(197, GarcNames.Items),

				// Varied
				new(071, GarcNames.GameText, true),
				new(079, GarcNames.StoryText, true),
			};

			public static readonly GarcReference[] SunMoonDemo = {
				new(011, GarcNames.Moves),
				new(012, GarcNames.EggMoves),
				new(013, GarcNames.Learnsets),
				new(014, GarcNames.Evolutions),
				new(015, GarcNames.MegaEvolutions),
				new(017, GarcNames.PokemonInfo),
				new(019, GarcNames.Items),

				new(076, GarcNames.ZoneData),
				new(081, GarcNames.EncounterData),

				new(101, GarcNames.TrainerClasses),
				new(102, GarcNames.TrainerData),
				new(103, GarcNames.TrainerPokemon),

				// Varied
				new(030, GarcNames.GameText, true),
				new(040, GarcNames.StoryText, true),
			};

			private static readonly GarcReference[] SunMoon = {
				new(011, GarcNames.Moves),
				new(012, GarcNames.EggMoves),
				new(013, GarcNames.Learnsets),
				new(014, GarcNames.Evolutions),
				new(015, GarcNames.MegaEvolutions),
				new(017, GarcNames.PokemonInfo),
				new(019, GarcNames.Items),

				new(077, GarcNames.ZoneData),
				new(091, GarcNames.WorldData),

				new(104, GarcNames.TrainerClasses),
				new(105, GarcNames.TrainerData),
				new(106, GarcNames.TrainerPokemon),

				new(155, GarcNames.StaticEncounters),

				new(267, GarcNames.ItemPickup),

				new(277, GarcNames.MaisonPokemonN),
				new(278, GarcNames.MaisonTrainerN),
				new(279, GarcNames.MaisonPokemonS),
				new(280, GarcNames.MaisonTrainerS),

				// Varied
				new(030, GarcNames.GameText, true),
				new(040, GarcNames.StoryText, true),
			};

			public static readonly GarcReference[] Sun = SunMoon.Concat(new[] {
				new GarcReference(082, GarcNames.EncounterData),
			}).ToArray();

			public static readonly GarcReference[] Moon = SunMoon.Concat(new[] {
				new GarcReference(083, GarcNames.EncounterData),
			}).ToArray();
		}

		public static class Text
		{
			public static readonly TextReference[] Xy = {
				new(005, TextNames.Forms),
				new(013, TextNames.MoveNames),
				new(015, TextNames.MoveFlavor),
				new(017, TextNames.Types),
				new(020, TextNames.TrainerClasses),
				new(021, TextNames.TrainerNames),
				new(022, TextNames.TrainerText),
				new(034, TextNames.AbilityNames),
				new(047, TextNames.Natures),
				new(072, TextNames.EncounterZoneNames),
				new(080, TextNames.SpeciesNames),
				new(096, TextNames.ItemNames),
				new(099, TextNames.ItemFlavor),
				new(130, TextNames.MaisonTrainerNames),
				new(131, TextNames.SuperTrainerNames),
				new(141, TextNames.OPowerFlavor),
			};

			public static readonly TextReference[] Oras = {
				new(005, TextNames.Forms),
				new(014, TextNames.MoveNames),
				new(016, TextNames.MoveFlavor),
				new(018, TextNames.Types),
				new(021, TextNames.TrainerClasses),
				new(022, TextNames.TrainerNames),
				new(023, TextNames.TrainerText),
				new(037, TextNames.AbilityNames),
				new(051, TextNames.Natures),
				new(090, TextNames.EncounterZoneNames),
				new(098, TextNames.SpeciesNames),
				new(114, TextNames.ItemNames),
				new(117, TextNames.ItemFlavor),
				new(153, TextNames.MaisonTrainerNames),
				new(154, TextNames.SuperTrainerNames),
				new(165, TextNames.OPowerFlavor),
			};

			public static readonly TextReference[] SunMoonDemo = {
				new(020, TextNames.ItemFlavor),
				new(021, TextNames.ItemNames),
				new(026, TextNames.SpeciesNames),
				new(030, TextNames.EncounterZoneNames),
				new(044, TextNames.Forms),
				new(044, TextNames.Natures),
				new(046, TextNames.AbilityNames),
				new(049, TextNames.TrainerText),
				new(050, TextNames.TrainerNames),
				new(051, TextNames.TrainerClasses),
				new(052, TextNames.Types),
				new(054, TextNames.MoveFlavor),
				new(055, TextNames.MoveNames),
			};

			public static readonly TextReference[] SunMoon = {
				new(035, TextNames.ItemFlavor),
				new(036, TextNames.ItemNames),
				new(055, TextNames.SpeciesNames),
				new(067, TextNames.EncounterZoneNames),
				new(086, TextNames.BattleRoyalNames),
				new(087, TextNames.Natures),
				new(096, TextNames.AbilityNames),
				new(099, TextNames.BattleTreeNames),
				new(104, TextNames.TrainerText),
				new(105, TextNames.TrainerNames),
				new(106, TextNames.TrainerClasses),
				new(107, TextNames.Types),
				new(112, TextNames.MoveFlavor),
				new(113, TextNames.MoveNames),
				new(114, TextNames.Forms)
			};
		}

		public static class Variables
		{
			public static readonly TextVariableCode[] Xy = {
				new(0xFF00, "COLOR"),
				new(0x0100, "TRNAME"),
				new(0x0101, "PKNAME"),
				new(0x0102, "PKNICK"),
				new(0x0103, "TYPE"),
				new(0x0105, "LOCATION"),
				new(0x0106, "ABILITY"),
				new(0x0107, "MOVE"),
				new(0x0108, "ITEM1"),
				new(0x0109, "ITEM2"),
				new(0x010A, "sTRBAG"),
				new(0x010B, "BOX"),
				new(0x010D, "EVSTAT"),
				new(0x0110, "OPOWER"),
				new(0x0127, "RIBBON"),
				new(0x0134, "MIINAME"),
				new(0x013E, "WEATHER"),
				new(0x0189, "TRNICK"),
				new(0x018A, "1stchrTR"),
				new(0x018B, "SHOUTOUT"),
				new(0x018E, "BERRY"),
				new(0x018F, "REMFEEL"),
				new(0x0190, "REMQUAL"),
				new(0x0191, "WEBSITE"),
				new(0x019C, "CHOICECOS"),
				new(0x01A1, "GSYNCID"),
				new(0x0192, "PRVIDSAY"),
				new(0x0193, "BTLTEST"),
				new(0x0195, "GENLOC"),
				new(0x0199, "CHOICEFOOD"),
				new(0x019A, "HOTELITEM"),
				new(0x019B, "TAXISTOP"),
				new(0x019F, "MAISTITLE"),
				new(0x1000, "ITEMPLUR0"),
				new(0x1001, "ITEMPLUR1"),
				new(0x1100, "GENDBR"),
				new(0x1101, "NUMBRNCH"),
				new(0x1302, "iCOLOR2"),
				new(0x1303, "iCOLOR3"),
				new(0x0200, "NUM1"),
				new(0x0201, "NUM2"),
				new(0x0202, "NUM3"),
				new(0x0203, "NUM4"),
				new(0x0204, "NUM5"),
				new(0x0205, "NUM6"),
				new(0x0206, "NUM7"),
				new(0x0207, "NUM8"),
				new(0x0208, "NUM9"),
			};

			public static readonly TextVariableCode[] Oras = {
				new(0xFF00, "COLOR"),
				new(0x0100, "TRNAME"),
				new(0x0101, "PKNAME"),
				new(0x0102, "PKNICK"),
				new(0x0103, "TYPE"),
				new(0x0105, "LOCATION"),
				new(0x0106, "ABILITY"),
				new(0x0107, "MOVE"),
				new(0x0108, "ITEM1"),
				new(0x0109, "ITEM2"),
				new(0x010A, "sTRBAG"),
				new(0x010B, "BOX"),
				new(0x010D, "EVSTAT"),
				new(0x0110, "OPOWER"),
				new(0x0127, "RIBBON"),
				new(0x0134, "MIINAME"),
				new(0x013E, "WEATHER"),
				new(0x0189, "TRNICK"),
				new(0x018A, "1stchrTR"),
				new(0x018B, "SHOUTOUT"),
				new(0x018E, "BERRY"),
				new(0x018F, "REMFEEL"),
				new(0x0190, "REMQUAL"),
				new(0x0191, "WEBSITE"),
				new(0x019C, "CHOICECOS"),
				new(0x01A1, "GSYNCID"),
				new(0x0192, "PRVIDSAY"),
				new(0x0193, "BTLTEST"),
				new(0x0195, "GENLOC"),
				new(0x0199, "CHOICEFOOD"),
				new(0x019A, "HOTELITEM"),
				new(0x019B, "TAXISTOP"),
				new(0x019F, "MAISTITLE"),
				new(0x1000, "ITEMPLUR0"),
				new(0x1001, "ITEMPLUR1"),
				new(0x1100, "GENDBR"),
				new(0x1101, "NUMBRNCH"),
				new(0x1302, "iCOLOR2"),
				new(0x1303, "iCOLOR3"),
				new(0x0200, "NUM1"),
				new(0x0201, "NUM2"),
				new(0x0202, "NUM3"),
				new(0x0203, "NUM4"),
				new(0x0204, "NUM5"),
				new(0x0205, "NUM6"),
				new(0x0206, "NUM7"),
				new(0x0207, "NUM8"),
				new(0x0208, "NUM9"),
			};

			public static readonly TextVariableCode[] SunMoon = {
				new(0xFF00, "COLOR"),
				new(0x0100, "TRNAME"),
				new(0x0101, "PKNAME"),
				new(0x0102, "PKNICK"),
				new(0x0103, "TYPE"),
				new(0x0105, "LOCATION"),
				new(0x0106, "ABILITY"),
				new(0x0107, "MOVE"),
				new(0x0108, "ITEM1"),
				new(0x0109, "ITEM2"),
				new(0x010A, "sTRBAG"),
				new(0x010B, "BOX"),
				new(0x010D, "EVSTAT"),
				new(0x0110, "OPOWER"),
				new(0x0127, "RIBBON"),
				new(0x0134, "MIINAME"),
				new(0x013E, "WEATHER"),
				new(0x0189, "TRNICK"),
				new(0x018A, "1stchrTR"),
				new(0x018B, "SHOUTOUT"),
				new(0x018E, "BERRY"),
				new(0x018F, "REMFEEL"),
				new(0x0190, "REMQUAL"),
				new(0x0191, "WEBSITE"),
				new(0x019C, "CHOICECOS"),
				new(0x01A1, "GSYNCID"),
				new(0x0192, "PRVIDSAY"),
				new(0x0193, "BTLTEST"),
				new(0x0195, "GENLOC"),
				new(0x0199, "CHOICEFOOD"),
				new(0x019A, "HOTELITEM"),
				new(0x019B, "TAXISTOP"),
				new(0x019F, "MAISTITLE"),
				new(0x1000, "ITEMPLUR0"),
				new(0x1001, "ITEMPLUR1"),
				new(0x1100, "GENDBR"),
				new(0x1101, "NUMBRNCH"),
				new(0x1302, "iCOLOR2"),
				new(0x1303, "iCOLOR3"),
				new(0x0200, "NUM1"),
				new(0x0201, "NUM2"),
				new(0x0202, "NUM3"),
				new(0x0203, "NUM4"),
				new(0x0204, "NUM5"),
				new(0x0205, "NUM6"),
				new(0x0206, "NUM7"),
				new(0x0207, "NUM8"),
				new(0x0208, "NUM9"),
			};
		}

		public static class Amx
		{
			public static readonly AmxReference[] Xy = {
				new(0x11, AmxNames.FldItem),
			};

			public static readonly AmxReference[] Oras = {
				new(0x27, AmxNames.FldItem),
			};
		}
	}
}