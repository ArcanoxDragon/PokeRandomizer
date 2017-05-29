using System.Linq;
using CtrDotNet.Pokemon.Game;

namespace CtrDotNet.Pokemon.Reference
{
	public static class ReferenceLists
	{
		public static class Garc
		{
			public static readonly GarcReference[] Xy = {
				new GarcReference( 005, "movesprite" ),
				new GarcReference( 012, "encdata" ),
				new GarcReference( 038, "trdata" ),
				new GarcReference( 039, "trclass" ),
				new GarcReference( 040, "trpoke" ),
				new GarcReference( 041, "mapGR" ),
				new GarcReference( 042, "mapMatrix" ),
				new GarcReference( 104, "wallpaper" ),
				new GarcReference( 165, "titlescreen" ),
				new GarcReference( 203, "maisonpkN" ),
				new GarcReference( 204, "maisontrN" ),
				new GarcReference( 205, "maisonpkS" ),
				new GarcReference( 206, "maisontrS" ),
				new GarcReference( 212, "move" ),
				new GarcReference( 213, "eggmove" ),
				new GarcReference( 214, "levelup" ),
				new GarcReference( 215, "evolution" ),
				new GarcReference( 216, "megaevo" ),
				new GarcReference( 218, "personal" ),
				new GarcReference( 220, "item" ),

				// Varied
				new GarcReference( 072, "gametext", true ),
				new GarcReference( 080, "storytext", true ),
			};

			public static readonly GarcReference[] Oras = {
				new GarcReference( 013, "encdata" ),
				new GarcReference( 036, "trdata" ),
				new GarcReference( 037, "trclass" ),
				new GarcReference( 038, "trpoke" ),
				new GarcReference( 039, "mapGR" ),
				new GarcReference( 040, "mapMatrix" ),
				new GarcReference( 103, "wallpaper" ),
				new GarcReference( 152, "titlescreen" ),
				new GarcReference( 182, "maisonpkN" ),
				new GarcReference( 183, "maisontrN" ),
				new GarcReference( 184, "maisonpkS" ),
				new GarcReference( 185, "maisontrS" ),
				new GarcReference( 189, "move" ),
				new GarcReference( 190, "eggmove" ),
				new GarcReference( 191, "levelup" ),
				new GarcReference( 192, "evolution" ),
				new GarcReference( 193, "megaevo" ),
				new GarcReference( 195, "personal" ),
				new GarcReference( 197, "item" ),

				// Varied
				new GarcReference( 071, "gametext", true ),
				new GarcReference( 079, "storytext", true ),
			};

			public static readonly GarcReference[] SunMoonDemo = {
				new GarcReference( 011, "move" ),
				new GarcReference( 012, "eggmove" ),
				new GarcReference( 013, "levelup" ),
				new GarcReference( 014, "evolution" ),
				new GarcReference( 015, "megaevo" ),
				new GarcReference( 017, "personal" ),
				new GarcReference( 019, "item" ),

				new GarcReference( 076, "zonedata" ),
				new GarcReference( 081, "encdata" ),

				new GarcReference( 101, "trclass" ),
				new GarcReference( 102, "trdata" ),
				new GarcReference( 103, "trpoke" ),

				// Varied
				new GarcReference( 030, "gametext", true ),
				new GarcReference( 040, "storytext", true ),
			};

			private static readonly GarcReference[] SunMoon = {
				new GarcReference( 011, "move" ),
				new GarcReference( 012, "eggmove" ),
				new GarcReference( 013, "levelup" ),
				new GarcReference( 014, "evolution" ),
				new GarcReference( 015, "megaevo" ),
				new GarcReference( 017, "personal" ),
				new GarcReference( 019, "item" ),

				new GarcReference( 077, "zonedata" ),
				new GarcReference( 091, "worlddata" ),

				new GarcReference( 104, "trclass" ),
				new GarcReference( 105, "trdata" ),
				new GarcReference( 106, "trpoke" ),

				new GarcReference( 155, "encounterstatic" ),

				new GarcReference( 267, "pickup" ),

				new GarcReference( 277, "maisonpkN" ),
				new GarcReference( 278, "maisontrN" ),
				new GarcReference( 279, "maisonpkS" ),
				new GarcReference( 280, "maisontrS" ),

				// Varied
				new GarcReference( 030, "gametext", true ),
				new GarcReference( 040, "storytext", true ),
			};

			public static readonly GarcReference[] Sun = SunMoon.Concat( new[] {
				new GarcReference( 082, "encdata" ),
			} ).ToArray();

			public static readonly GarcReference[] Moon = SunMoon.Concat( new[] {
				new GarcReference( 083, "encdata" ),
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