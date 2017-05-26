using System.IO;
using System.Linq;

namespace CtrDotNet.Pokemon.Game
{
    public class GarcReference
    {
        public readonly int FileNumber;
        public readonly string Name;
        private int A => (this.FileNumber / 100) % 10;
        private int B => (this.FileNumber / 10) % 10;
        private int C => (this.FileNumber / 1) % 10;
        public readonly bool LanguageVariant;
        public string Reference => Path.Combine("a", this.A.ToString(), this.B.ToString(), this.C.ToString());

        private GarcReference(int file, string name, bool lv = false)
        {
            this.Name = name;
            this.FileNumber = file;
            this.LanguageVariant = lv;
        }
        public GarcReference GetRelativeGarc(int offset, string name = "")
        {
            return new GarcReference(this.FileNumber + offset, name);
        }

        public static readonly GarcReference[] GarcReferenceXy =
        {
            new GarcReference(005, "movesprite"),
            new GarcReference(012, "encdata"),
            new GarcReference(038, "trdata"),
            new GarcReference(039, "trclass"),
            new GarcReference(040, "trpoke"),
            new GarcReference(041, "mapGR"),
            new GarcReference(042, "mapMatrix"),
            new GarcReference(104, "wallpaper"),
            new GarcReference(165, "titlescreen"),
            new GarcReference(203, "maisonpkN"),
            new GarcReference(204, "maisontrN"),
            new GarcReference(205, "maisonpkS"),
            new GarcReference(206, "maisontrS"),
            new GarcReference(212, "move"),
            new GarcReference(213, "eggmove"),
            new GarcReference(214, "levelup"),
            new GarcReference(215, "evolution"),
            new GarcReference(216, "megaevo"),
            new GarcReference(218, "personal"),
            new GarcReference(220, "item"),

            // Varied
            new GarcReference(072, "gametext", true),
            new GarcReference(080, "storytext", true),
        };
        public static readonly GarcReference[] GarcReferenceAo =
        {
            new GarcReference(013, "encdata"),
            new GarcReference(036, "trdata"),
            new GarcReference(037, "trclass"),
            new GarcReference(038, "trpoke"),
            new GarcReference(039, "mapGR"),
            new GarcReference(040, "mapMatrix"),
            new GarcReference(103, "wallpaper"),
            new GarcReference(152, "titlescreen"),
            new GarcReference(182, "maisonpkN"),
            new GarcReference(183, "maisontrN"),
            new GarcReference(184, "maisonpkS"),
            new GarcReference(185, "maisontrS"),
            new GarcReference(189, "move"),
            new GarcReference(190, "eggmove"),
            new GarcReference(191, "levelup"),
            new GarcReference(192, "evolution"),
            new GarcReference(193, "megaevo"),
            new GarcReference(195, "personal"),
            new GarcReference(197, "item"),
                
            // Varied
            new GarcReference(071, "gametext", true),
            new GarcReference(079, "storytext", true),
        };
        public static readonly GarcReference[] GarcReferenceSmdemo =
        {
            new GarcReference(011, "move"),
            new GarcReference(012, "eggmove"),
            new GarcReference(013, "levelup"),
            new GarcReference(014, "evolution"),
            new GarcReference(015, "megaevo"),
            new GarcReference(017, "personal"),
            new GarcReference(019, "item"),

            new GarcReference(076, "zonedata"),
            new GarcReference(081, "encdata"),

            new GarcReference(101, "trclass"),
            new GarcReference(102, "trdata"),
            new GarcReference(103, "trpoke"),
                
            // Varied
            new GarcReference(030, "gametext", true),
            new GarcReference(040, "storytext", true),
        };
        private static readonly GarcReference[] GarcReferenceSm =
        {
            new GarcReference(011, "move"),
            new GarcReference(012, "eggmove"),
            new GarcReference(013, "levelup"),
            new GarcReference(014, "evolution"),
            new GarcReference(015, "megaevo"),
            new GarcReference(017, "personal"),
            new GarcReference(019, "item"),

            new GarcReference(077, "zonedata"),
            new GarcReference(091, "worlddata"),

            new GarcReference(104, "trclass"),
            new GarcReference(105, "trdata"),
            new GarcReference(106, "trpoke"),

            new GarcReference(155, "encounterstatic"),

            new GarcReference(267, "pickup"),

            new GarcReference(277, "maisonpkN"),
            new GarcReference(278, "maisontrN"),
            new GarcReference(279, "maisonpkS"),
            new GarcReference(280, "maisontrS"),
                
            // Varied
            new GarcReference(030, "gametext", true),
            new GarcReference(040, "storytext", true),
        };

        public static readonly GarcReference[] GarcReferenceSn = GarcReferenceSm.Concat(
            new[] {
              new GarcReference(082, "encdata"),
            }).ToArray();
        public static readonly GarcReference[] GarcReferenceMN = GarcReferenceSm.Concat(
            new[] {
              new GarcReference(083, "encdata"),
            }).ToArray();
    }
}
