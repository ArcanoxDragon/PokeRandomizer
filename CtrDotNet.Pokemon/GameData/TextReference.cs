namespace CtrDotNet.Pokemon.GameData
{
    public enum TextName
    {
        AbilityNames,
        MoveNames,
        MoveFlavor,

        ItemNames,
        ItemFlavor,

        SpeciesNames,
        Types,
        Natures,
        Forms,

        TrainerNames,
        TrainerClasses,
        TrainerText,
        Metlist000000,
        OPowerFlavor,
        MaisonTrainerNames,
        SuperTrainerNames,
        BattleRoyalNames,
        BattleTreeNames,
    }
    public class TextReference
    {
        public readonly int Index;
        public readonly TextName Name;
        private TextReference(int index, TextName name)
        {
            this.Index = index;
            this.Name = name;
        }

        public static readonly TextReference[] GameTextXy =
        {
            new TextReference(005, TextName.Forms),
            new TextReference(013, TextName.MoveNames),
            new TextReference(015, TextName.MoveFlavor),
            new TextReference(017, TextName.Types),
            new TextReference(020, TextName.TrainerClasses),
            new TextReference(021, TextName.TrainerNames),
            new TextReference(022, TextName.TrainerText),
            new TextReference(034, TextName.AbilityNames),
            new TextReference(047, TextName.Natures),
            new TextReference(072, TextName.Metlist000000),
            new TextReference(080, TextName.SpeciesNames),
            new TextReference(096, TextName.ItemNames),
            new TextReference(099, TextName.ItemFlavor),
            new TextReference(130, TextName.MaisonTrainerNames),
            new TextReference(131, TextName.SuperTrainerNames),
            new TextReference(141, TextName.OPowerFlavor),
        };
        public static readonly TextReference[] GameTextAo =
        {
            new TextReference(005, TextName.Forms),
            new TextReference(014, TextName.MoveNames),
            new TextReference(016, TextName.MoveFlavor),
            new TextReference(018, TextName.Types),
            new TextReference(021, TextName.TrainerClasses),
            new TextReference(022, TextName.TrainerNames),
            new TextReference(023, TextName.TrainerText),
            new TextReference(037, TextName.AbilityNames),
            new TextReference(051, TextName.Natures),
            new TextReference(090, TextName.Metlist000000),
            new TextReference(098, TextName.SpeciesNames),
            new TextReference(114, TextName.ItemNames),
            new TextReference(117, TextName.ItemFlavor),
            new TextReference(153, TextName.MaisonTrainerNames),
            new TextReference(154, TextName.SuperTrainerNames),
            new TextReference(165, TextName.OPowerFlavor),
        };
        public static readonly TextReference[] GameTextSmdemo =
        {
            new TextReference(020, TextName.ItemFlavor),
            new TextReference(021, TextName.ItemNames),
            new TextReference(026, TextName.SpeciesNames),
            new TextReference(030, TextName.Metlist000000),
            new TextReference(044, TextName.Forms),
            new TextReference(044, TextName.Natures),
            new TextReference(046, TextName.AbilityNames),
            new TextReference(049, TextName.TrainerText),
            new TextReference(050, TextName.TrainerNames),
            new TextReference(051, TextName.TrainerClasses),
            new TextReference(052, TextName.Types),
            new TextReference(054, TextName.MoveFlavor),
            new TextReference(055, TextName.MoveNames),
        };
        public static readonly TextReference[] GameTextSm =
        {
            new TextReference(035, TextName.ItemFlavor),
            new TextReference(036, TextName.ItemNames),
            new TextReference(055, TextName.SpeciesNames),
            new TextReference(067, TextName.Metlist000000),
            new TextReference(086, TextName.BattleRoyalNames),
            new TextReference(087, TextName.Natures),
            new TextReference(096, TextName.AbilityNames),
            new TextReference(099, TextName.BattleTreeNames),
            new TextReference(104, TextName.TrainerText),
            new TextReference(105, TextName.TrainerNames),
            new TextReference(106, TextName.TrainerClasses),
            new TextReference(107, TextName.Types),
            new TextReference(112, TextName.MoveFlavor), 
            new TextReference(113, TextName.MoveNames),
            new TextReference(114, TextName.Forms)
        };
    }
}
