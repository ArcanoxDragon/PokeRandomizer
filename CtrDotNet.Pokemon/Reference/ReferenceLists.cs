using System.Linq;
using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Reference
{
	public static class ReferenceLists
	{
		public static class Garc
		{
			public static readonly GarcReference[] Xy = {
				new GarcReference( 005, GarcNames.MoveSprites ),
				new GarcReference( 012, GarcNames.EncounterData ),
				new GarcReference( 038, GarcNames.TrainerData ),
				new GarcReference( 039, GarcNames.TrainerClasses ),
				new GarcReference( 040, GarcNames.TrainerPokemon ),
				new GarcReference( 041, GarcNames.MapGrid ),
				new GarcReference( 042, GarcNames.MapMatrix ),
				new GarcReference( 104, GarcNames.Wallpapers ),
				new GarcReference( 165, GarcNames.TitleScreen ),
				new GarcReference( 203, GarcNames.MaisonPokemonN ),
				new GarcReference( 204, GarcNames.MaisonTrainerN ),
				new GarcReference( 205, GarcNames.MaisonPokemonS ),
				new GarcReference( 206, GarcNames.MaisonTrainerS ),
				new GarcReference( 212, GarcNames.Moves ),
				new GarcReference( 213, GarcNames.EggMoves ),
				new GarcReference( 214, GarcNames.Learnsets ),
				new GarcReference( 215, GarcNames.Evolutions ),
				new GarcReference( 216, GarcNames.MegaEvolutions ),
				new GarcReference( 218, GarcNames.PokemonInfo ),
				new GarcReference( 220, GarcNames.Items ),

				// Varied
				new GarcReference( 072, GarcNames.GameText, true ),
				new GarcReference( 080, GarcNames.StoryText, true ),
			};

			public static readonly GarcReference[] Oras = {
				new GarcReference( 013, GarcNames.EncounterData ),
				new GarcReference( 036, GarcNames.TrainerData ),
				new GarcReference( 037, GarcNames.TrainerClasses ),
				new GarcReference( 038, GarcNames.TrainerPokemon ),
				new GarcReference( 039, GarcNames.MapGrid ),
				new GarcReference( 040, GarcNames.MapMatrix ),
				new GarcReference( 103, GarcNames.Wallpapers ),
				new GarcReference( 152, GarcNames.TitleScreen ),
				new GarcReference( 182, GarcNames.MaisonPokemonN ),
				new GarcReference( 183, GarcNames.MaisonTrainerN ),
				new GarcReference( 184, GarcNames.MaisonPokemonS ),
				new GarcReference( 185, GarcNames.MaisonTrainerS ),
				new GarcReference( 189, GarcNames.Moves ),
				new GarcReference( 190, GarcNames.EggMoves ),
				new GarcReference( 191, GarcNames.Learnsets ),
				new GarcReference( 192, GarcNames.Evolutions ),
				new GarcReference( 193, GarcNames.MegaEvolutions ),
				new GarcReference( 195, GarcNames.PokemonInfo ),
				new GarcReference( 197, GarcNames.Items ),

				// Varied
				new GarcReference( 071, GarcNames.GameText, true ),
				new GarcReference( 079, GarcNames.StoryText, true ),
			};

			public static readonly GarcReference[] SunMoonDemo = {
				new GarcReference( 011, GarcNames.Moves ),
				new GarcReference( 012, GarcNames.EggMoves ),
				new GarcReference( 013, GarcNames.Learnsets ),
				new GarcReference( 014, GarcNames.Evolutions ),
				new GarcReference( 015, GarcNames.MegaEvolutions ),
				new GarcReference( 017, GarcNames.PokemonInfo ),
				new GarcReference( 019, GarcNames.Items ),

				new GarcReference( 076, GarcNames.ZoneData ),
				new GarcReference( 081, GarcNames.EncounterData ),

				new GarcReference( 101, GarcNames.TrainerClasses ),
				new GarcReference( 102, GarcNames.TrainerData ),
				new GarcReference( 103, GarcNames.TrainerPokemon ),

				// Varied
				new GarcReference( 030, GarcNames.GameText, true ),
				new GarcReference( 040, GarcNames.StoryText, true ),
			};

			private static readonly GarcReference[] SunMoon = {
				new GarcReference( 011, GarcNames.Moves ),
				new GarcReference( 012, GarcNames.EggMoves ),
				new GarcReference( 013, GarcNames.Learnsets ),
				new GarcReference( 014, GarcNames.Evolutions ),
				new GarcReference( 015, GarcNames.MegaEvolutions ),
				new GarcReference( 017, GarcNames.PokemonInfo ),
				new GarcReference( 019, GarcNames.Items ),

				new GarcReference( 077, GarcNames.ZoneData ),
				new GarcReference( 091, GarcNames.WorldData ),

				new GarcReference( 104, GarcNames.TrainerClasses ),
				new GarcReference( 105, GarcNames.TrainerData ),
				new GarcReference( 106, GarcNames.TrainerPokemon ),

				new GarcReference( 155, GarcNames.StaticEncounters ),

				new GarcReference( 267, GarcNames.ItemPickup ),

				new GarcReference( 277, GarcNames.MaisonPokemonN ),
				new GarcReference( 278, GarcNames.MaisonTrainerN ),
				new GarcReference( 279, GarcNames.MaisonPokemonS ),
				new GarcReference( 280, GarcNames.MaisonTrainerS ),

				// Varied
				new GarcReference( 030, GarcNames.GameText, true ),
				new GarcReference( 040, GarcNames.StoryText, true ),
			};

			public static readonly GarcReference[] Sun = SunMoon.Concat( new[] {
				new GarcReference( 082, GarcNames.EncounterData ),
			} ).ToArray();

			public static readonly GarcReference[] Moon = SunMoon.Concat( new[] {
				new GarcReference( 083, GarcNames.EncounterData ),
			} ).ToArray();
		}

		public static class Text
		{
			public static readonly TextReference[] Xy = {
				new TextReference( 005, TextNames.Forms ),
				new TextReference( 013, TextNames.MoveNames ),
				new TextReference( 015, TextNames.MoveFlavor ),
				new TextReference( 017, TextNames.Types ),
				new TextReference( 020, TextNames.TrainerClasses ),
				new TextReference( 021, TextNames.TrainerNames ),
				new TextReference( 022, TextNames.TrainerText ),
				new TextReference( 034, TextNames.AbilityNames ),
				new TextReference( 047, TextNames.Natures ),
				new TextReference( 072, TextNames.Metlist000000 ),
				new TextReference( 080, TextNames.SpeciesNames ),
				new TextReference( 096, TextNames.ItemNames ),
				new TextReference( 099, TextNames.ItemFlavor ),
				new TextReference( 130, TextNames.MaisonTrainerNames ),
				new TextReference( 131, TextNames.SuperTrainerNames ),
				new TextReference( 141, TextNames.OPowerFlavor ),
			};

			public static readonly TextReference[] Oras = {
				new TextReference( 005, TextNames.Forms ),
				new TextReference( 014, TextNames.MoveNames ),
				new TextReference( 016, TextNames.MoveFlavor ),
				new TextReference( 018, TextNames.Types ),
				new TextReference( 021, TextNames.TrainerClasses ),
				new TextReference( 022, TextNames.TrainerNames ),
				new TextReference( 023, TextNames.TrainerText ),
				new TextReference( 037, TextNames.AbilityNames ),
				new TextReference( 051, TextNames.Natures ),
				new TextReference( 090, TextNames.Metlist000000 ),
				new TextReference( 098, TextNames.SpeciesNames ),
				new TextReference( 114, TextNames.ItemNames ),
				new TextReference( 117, TextNames.ItemFlavor ),
				new TextReference( 153, TextNames.MaisonTrainerNames ),
				new TextReference( 154, TextNames.SuperTrainerNames ),
				new TextReference( 165, TextNames.OPowerFlavor ),
			};

			public static readonly TextReference[] SunMoonDemo = {
				new TextReference( 020, TextNames.ItemFlavor ),
				new TextReference( 021, TextNames.ItemNames ),
				new TextReference( 026, TextNames.SpeciesNames ),
				new TextReference( 030, TextNames.Metlist000000 ),
				new TextReference( 044, TextNames.Forms ),
				new TextReference( 044, TextNames.Natures ),
				new TextReference( 046, TextNames.AbilityNames ),
				new TextReference( 049, TextNames.TrainerText ),
				new TextReference( 050, TextNames.TrainerNames ),
				new TextReference( 051, TextNames.TrainerClasses ),
				new TextReference( 052, TextNames.Types ),
				new TextReference( 054, TextNames.MoveFlavor ),
				new TextReference( 055, TextNames.MoveNames ),
			};

			public static readonly TextReference[] SunMoon = {
				new TextReference( 035, TextNames.ItemFlavor ),
				new TextReference( 036, TextNames.ItemNames ),
				new TextReference( 055, TextNames.SpeciesNames ),
				new TextReference( 067, TextNames.Metlist000000 ),
				new TextReference( 086, TextNames.BattleRoyalNames ),
				new TextReference( 087, TextNames.Natures ),
				new TextReference( 096, TextNames.AbilityNames ),
				new TextReference( 099, TextNames.BattleTreeNames ),
				new TextReference( 104, TextNames.TrainerText ),
				new TextReference( 105, TextNames.TrainerNames ),
				new TextReference( 106, TextNames.TrainerClasses ),
				new TextReference( 107, TextNames.Types ),
				new TextReference( 112, TextNames.MoveFlavor ),
				new TextReference( 113, TextNames.MoveNames ),
				new TextReference( 114, TextNames.Forms )
			};
		}

		public static class Variables
		{
			public static readonly TextVariableCode[] Xy = {
				new TextVariableCode( 0xFF00, "COLOR" ),
				new TextVariableCode( 0x0100, "TRNAME" ),
				new TextVariableCode( 0x0101, "PKNAME" ),
				new TextVariableCode( 0x0102, "PKNICK" ),
				new TextVariableCode( 0x0103, "TYPE" ),
				new TextVariableCode( 0x0105, "LOCATION" ),
				new TextVariableCode( 0x0106, "ABILITY" ),
				new TextVariableCode( 0x0107, "MOVE" ),
				new TextVariableCode( 0x0108, "ITEM1" ),
				new TextVariableCode( 0x0109, "ITEM2" ),
				new TextVariableCode( 0x010A, "sTRBAG" ),
				new TextVariableCode( 0x010B, "BOX" ),
				new TextVariableCode( 0x010D, "EVSTAT" ),
				new TextVariableCode( 0x0110, "OPOWER" ),
				new TextVariableCode( 0x0127, "RIBBON" ),
				new TextVariableCode( 0x0134, "MIINAME" ),
				new TextVariableCode( 0x013E, "WEATHER" ),
				new TextVariableCode( 0x0189, "TRNICK" ),
				new TextVariableCode( 0x018A, "1stchrTR" ),
				new TextVariableCode( 0x018B, "SHOUTOUT" ),
				new TextVariableCode( 0x018E, "BERRY" ),
				new TextVariableCode( 0x018F, "REMFEEL" ),
				new TextVariableCode( 0x0190, "REMQUAL" ),
				new TextVariableCode( 0x0191, "WEBSITE" ),
				new TextVariableCode( 0x019C, "CHOICECOS" ),
				new TextVariableCode( 0x01A1, "GSYNCID" ),
				new TextVariableCode( 0x0192, "PRVIDSAY" ),
				new TextVariableCode( 0x0193, "BTLTEST" ),
				new TextVariableCode( 0x0195, "GENLOC" ),
				new TextVariableCode( 0x0199, "CHOICEFOOD" ),
				new TextVariableCode( 0x019A, "HOTELITEM" ),
				new TextVariableCode( 0x019B, "TAXISTOP" ),
				new TextVariableCode( 0x019F, "MAISTITLE" ),
				new TextVariableCode( 0x1000, "ITEMPLUR0" ),
				new TextVariableCode( 0x1001, "ITEMPLUR1" ),
				new TextVariableCode( 0x1100, "GENDBR" ),
				new TextVariableCode( 0x1101, "NUMBRNCH" ),
				new TextVariableCode( 0x1302, "iCOLOR2" ),
				new TextVariableCode( 0x1303, "iCOLOR3" ),
				new TextVariableCode( 0x0200, "NUM1" ),
				new TextVariableCode( 0x0201, "NUM2" ),
				new TextVariableCode( 0x0202, "NUM3" ),
				new TextVariableCode( 0x0203, "NUM4" ),
				new TextVariableCode( 0x0204, "NUM5" ),
				new TextVariableCode( 0x0205, "NUM6" ),
				new TextVariableCode( 0x0206, "NUM7" ),
				new TextVariableCode( 0x0207, "NUM8" ),
				new TextVariableCode( 0x0208, "NUM9" ),
			};

			public static readonly TextVariableCode[] Oras = {
				new TextVariableCode( 0xFF00, "COLOR" ),
				new TextVariableCode( 0x0100, "TRNAME" ),
				new TextVariableCode( 0x0101, "PKNAME" ),
				new TextVariableCode( 0x0102, "PKNICK" ),
				new TextVariableCode( 0x0103, "TYPE" ),
				new TextVariableCode( 0x0105, "LOCATION" ),
				new TextVariableCode( 0x0106, "ABILITY" ),
				new TextVariableCode( 0x0107, "MOVE" ),
				new TextVariableCode( 0x0108, "ITEM1" ),
				new TextVariableCode( 0x0109, "ITEM2" ),
				new TextVariableCode( 0x010A, "sTRBAG" ),
				new TextVariableCode( 0x010B, "BOX" ),
				new TextVariableCode( 0x010D, "EVSTAT" ),
				new TextVariableCode( 0x0110, "OPOWER" ),
				new TextVariableCode( 0x0127, "RIBBON" ),
				new TextVariableCode( 0x0134, "MIINAME" ),
				new TextVariableCode( 0x013E, "WEATHER" ),
				new TextVariableCode( 0x0189, "TRNICK" ),
				new TextVariableCode( 0x018A, "1stchrTR" ),
				new TextVariableCode( 0x018B, "SHOUTOUT" ),
				new TextVariableCode( 0x018E, "BERRY" ),
				new TextVariableCode( 0x018F, "REMFEEL" ),
				new TextVariableCode( 0x0190, "REMQUAL" ),
				new TextVariableCode( 0x0191, "WEBSITE" ),
				new TextVariableCode( 0x019C, "CHOICECOS" ),
				new TextVariableCode( 0x01A1, "GSYNCID" ),
				new TextVariableCode( 0x0192, "PRVIDSAY" ),
				new TextVariableCode( 0x0193, "BTLTEST" ),
				new TextVariableCode( 0x0195, "GENLOC" ),
				new TextVariableCode( 0x0199, "CHOICEFOOD" ),
				new TextVariableCode( 0x019A, "HOTELITEM" ),
				new TextVariableCode( 0x019B, "TAXISTOP" ),
				new TextVariableCode( 0x019F, "MAISTITLE" ),
				new TextVariableCode( 0x1000, "ITEMPLUR0" ),
				new TextVariableCode( 0x1001, "ITEMPLUR1" ),
				new TextVariableCode( 0x1100, "GENDBR" ),
				new TextVariableCode( 0x1101, "NUMBRNCH" ),
				new TextVariableCode( 0x1302, "iCOLOR2" ),
				new TextVariableCode( 0x1303, "iCOLOR3" ),
				new TextVariableCode( 0x0200, "NUM1" ),
				new TextVariableCode( 0x0201, "NUM2" ),
				new TextVariableCode( 0x0202, "NUM3" ),
				new TextVariableCode( 0x0203, "NUM4" ),
				new TextVariableCode( 0x0204, "NUM5" ),
				new TextVariableCode( 0x0205, "NUM6" ),
				new TextVariableCode( 0x0206, "NUM7" ),
				new TextVariableCode( 0x0207, "NUM8" ),
				new TextVariableCode( 0x0208, "NUM9" ),
			};

			public static readonly TextVariableCode[] SunMoon = {
				new TextVariableCode( 0xFF00, "COLOR" ),
				new TextVariableCode( 0x0100, "TRNAME" ),
				new TextVariableCode( 0x0101, "PKNAME" ),
				new TextVariableCode( 0x0102, "PKNICK" ),
				new TextVariableCode( 0x0103, "TYPE" ),
				new TextVariableCode( 0x0105, "LOCATION" ),
				new TextVariableCode( 0x0106, "ABILITY" ),
				new TextVariableCode( 0x0107, "MOVE" ),
				new TextVariableCode( 0x0108, "ITEM1" ),
				new TextVariableCode( 0x0109, "ITEM2" ),
				new TextVariableCode( 0x010A, "sTRBAG" ),
				new TextVariableCode( 0x010B, "BOX" ),
				new TextVariableCode( 0x010D, "EVSTAT" ),
				new TextVariableCode( 0x0110, "OPOWER" ),
				new TextVariableCode( 0x0127, "RIBBON" ),
				new TextVariableCode( 0x0134, "MIINAME" ),
				new TextVariableCode( 0x013E, "WEATHER" ),
				new TextVariableCode( 0x0189, "TRNICK" ),
				new TextVariableCode( 0x018A, "1stchrTR" ),
				new TextVariableCode( 0x018B, "SHOUTOUT" ),
				new TextVariableCode( 0x018E, "BERRY" ),
				new TextVariableCode( 0x018F, "REMFEEL" ),
				new TextVariableCode( 0x0190, "REMQUAL" ),
				new TextVariableCode( 0x0191, "WEBSITE" ),
				new TextVariableCode( 0x019C, "CHOICECOS" ),
				new TextVariableCode( 0x01A1, "GSYNCID" ),
				new TextVariableCode( 0x0192, "PRVIDSAY" ),
				new TextVariableCode( 0x0193, "BTLTEST" ),
				new TextVariableCode( 0x0195, "GENLOC" ),
				new TextVariableCode( 0x0199, "CHOICEFOOD" ),
				new TextVariableCode( 0x019A, "HOTELITEM" ),
				new TextVariableCode( 0x019B, "TAXISTOP" ),
				new TextVariableCode( 0x019F, "MAISTITLE" ),
				new TextVariableCode( 0x1000, "ITEMPLUR0" ),
				new TextVariableCode( 0x1001, "ITEMPLUR1" ),
				new TextVariableCode( 0x1100, "GENDBR" ),
				new TextVariableCode( 0x1101, "NUMBRNCH" ),
				new TextVariableCode( 0x1302, "iCOLOR2" ),
				new TextVariableCode( 0x1303, "iCOLOR3" ),
				new TextVariableCode( 0x0200, "NUM1" ),
				new TextVariableCode( 0x0201, "NUM2" ),
				new TextVariableCode( 0x0202, "NUM3" ),
				new TextVariableCode( 0x0203, "NUM4" ),
				new TextVariableCode( 0x0204, "NUM5" ),
				new TextVariableCode( 0x0205, "NUM6" ),
				new TextVariableCode( 0x0206, "NUM7" ),
				new TextVariableCode( 0x0207, "NUM8" ),
				new TextVariableCode( 0x0208, "NUM9" ),
			};
		}
	}
}