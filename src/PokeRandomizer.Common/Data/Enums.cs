// ReSharper disable All

using System.Collections.Generic;
using System.Linq;

namespace PokeRandomizer.Common.Data {
    
    public sealed class Ability : BaseAbility {
        public Ability( int id, string name ) : base( id, name ) { }
        
        public static explicit operator Ability( int id ) => Abilities.GetValueFrom( id );
        public static explicit operator int( Ability val ) => val.Id;
    }
    
    public static partial class Abilities {
        public static readonly Ability Stench = new Ability( 1, "Stench" );
        public static readonly Ability Drizzle = new Ability( 2, "Drizzle" );
        public static readonly Ability SpeedBoost = new Ability( 3, "Speed Boost" );
        public static readonly Ability BattleArmor = new Ability( 4, "Battle Armor" );
        public static readonly Ability Sturdy = new Ability( 5, "Sturdy" );
        public static readonly Ability Damp = new Ability( 6, "Damp" );
        public static readonly Ability Limber = new Ability( 7, "Limber" );
        public static readonly Ability SandVeil = new Ability( 8, "Sand Veil" );
        public static readonly Ability Static = new Ability( 9, "Static" );
        public static readonly Ability VoltAbsorb = new Ability( 10, "Volt Absorb" );
        public static readonly Ability WaterAbsorb = new Ability( 11, "Water Absorb" );
        public static readonly Ability Oblivious = new Ability( 12, "Oblivious" );
        public static readonly Ability CloudNine = new Ability( 13, "Cloud Nine" );
        public static readonly Ability CompoundEyes = new Ability( 14, "Compound Eyes" );
        public static readonly Ability Insomnia = new Ability( 15, "Insomnia" );
        public static readonly Ability ColorChange = new Ability( 16, "Color Change" );
        public static readonly Ability Immunity = new Ability( 17, "Immunity" );
        public static readonly Ability FlashFire = new Ability( 18, "Flash Fire" );
        public static readonly Ability ShieldDust = new Ability( 19, "Shield Dust" );
        public static readonly Ability OwnTempo = new Ability( 20, "Own Tempo" );
        public static readonly Ability SuctionCups = new Ability( 21, "Suction Cups" );
        public static readonly Ability Intimidate = new Ability( 22, "Intimidate" );
        public static readonly Ability ShadowTag = new Ability( 23, "Shadow Tag" );
        public static readonly Ability RoughSkin = new Ability( 24, "Rough Skin" );
        public static readonly Ability WonderGuard = new Ability( 25, "Wonder Guard" );
        public static readonly Ability Levitate = new Ability( 26, "Levitate" );
        public static readonly Ability EffectSpore = new Ability( 27, "Effect Spore" );
        public static readonly Ability Synchronize = new Ability( 28, "Synchronize" );
        public static readonly Ability ClearBody = new Ability( 29, "Clear Body" );
        public static readonly Ability NaturalCure = new Ability( 30, "Natural Cure" );
        public static readonly Ability LightningRod = new Ability( 31, "Lightning Rod" );
        public static readonly Ability SereneGrace = new Ability( 32, "Serene Grace" );
        public static readonly Ability SwiftSwim = new Ability( 33, "Swift Swim" );
        public static readonly Ability Chlorophyll = new Ability( 34, "Chlorophyll" );
        public static readonly Ability Illuminate = new Ability( 35, "Illuminate" );
        public static readonly Ability Trace = new Ability( 36, "Trace" );
        public static readonly Ability HugePower = new Ability( 37, "Huge Power" );
        public static readonly Ability PoisonPoint = new Ability( 38, "Poison Point" );
        public static readonly Ability InnerFocus = new Ability( 39, "Inner Focus" );
        public static readonly Ability MagmaArmor = new Ability( 40, "Magma Armor" );
        public static readonly Ability WaterVeil = new Ability( 41, "Water Veil" );
        public static readonly Ability MagnetPull = new Ability( 42, "Magnet Pull" );
        public static readonly Ability Soundproof = new Ability( 43, "Soundproof" );
        public static readonly Ability RainDish = new Ability( 44, "Rain Dish" );
        public static readonly Ability SandStream = new Ability( 45, "Sand Stream" );
        public static readonly Ability Pressure = new Ability( 46, "Pressure" );
        public static readonly Ability ThickFat = new Ability( 47, "Thick Fat" );
        public static readonly Ability EarlyBird = new Ability( 48, "Early Bird" );
        public static readonly Ability FlameBody = new Ability( 49, "Flame Body" );
        public static readonly Ability RunAway = new Ability( 50, "Run Away" );
        public static readonly Ability KeenEye = new Ability( 51, "Keen Eye" );
        public static readonly Ability HyperCutter = new Ability( 52, "Hyper Cutter" );
        public static readonly Ability Pickup = new Ability( 53, "Pickup" );
        public static readonly Ability Truant = new Ability( 54, "Truant" );
        public static readonly Ability Hustle = new Ability( 55, "Hustle" );
        public static readonly Ability CuteCharm = new Ability( 56, "Cute Charm" );
        public static readonly Ability Plus = new Ability( 57, "Plus" );
        public static readonly Ability Minus = new Ability( 58, "Minus" );
        public static readonly Ability Forecast = new Ability( 59, "Forecast" );
        public static readonly Ability StickyHold = new Ability( 60, "Sticky Hold" );
        public static readonly Ability ShedSkin = new Ability( 61, "Shed Skin" );
        public static readonly Ability Guts = new Ability( 62, "Guts" );
        public static readonly Ability MarvelScale = new Ability( 63, "Marvel Scale" );
        public static readonly Ability LiquidOoze = new Ability( 64, "Liquid Ooze" );
        public static readonly Ability Overgrow = new Ability( 65, "Overgrow" );
        public static readonly Ability Blaze = new Ability( 66, "Blaze" );
        public static readonly Ability Torrent = new Ability( 67, "Torrent" );
        public static readonly Ability Swarm = new Ability( 68, "Swarm" );
        public static readonly Ability RockHead = new Ability( 69, "Rock Head" );
        public static readonly Ability Drought = new Ability( 70, "Drought" );
        public static readonly Ability ArenaTrap = new Ability( 71, "Arena Trap" );
        public static readonly Ability VitalSpirit = new Ability( 72, "Vital Spirit" );
        public static readonly Ability WhiteSmoke = new Ability( 73, "White Smoke" );
        public static readonly Ability PurePower = new Ability( 74, "Pure Power" );
        public static readonly Ability ShellArmor = new Ability( 75, "Shell Armor" );
        public static readonly Ability AirLock = new Ability( 76, "Air Lock" );
        public static readonly Ability TangledFeet = new Ability( 77, "Tangled Feet" );
        public static readonly Ability MotorDrive = new Ability( 78, "Motor Drive" );
        public static readonly Ability Rivalry = new Ability( 79, "Rivalry" );
        public static readonly Ability Steadfast = new Ability( 80, "Steadfast" );
        public static readonly Ability SnowCloak = new Ability( 81, "Snow Cloak" );
        public static readonly Ability Gluttony = new Ability( 82, "Gluttony" );
        public static readonly Ability AngerPoint = new Ability( 83, "Anger Point" );
        public static readonly Ability Unburden = new Ability( 84, "Unburden" );
        public static readonly Ability Heatproof = new Ability( 85, "Heatproof" );
        public static readonly Ability Simple = new Ability( 86, "Simple" );
        public static readonly Ability DrySkin = new Ability( 87, "Dry Skin" );
        public static readonly Ability Download = new Ability( 88, "Download" );
        public static readonly Ability IronFist = new Ability( 89, "Iron Fist" );
        public static readonly Ability PoisonHeal = new Ability( 90, "Poison Heal" );
        public static readonly Ability Adaptability = new Ability( 91, "Adaptability" );
        public static readonly Ability SkillLink = new Ability( 92, "Skill Link" );
        public static readonly Ability Hydration = new Ability( 93, "Hydration" );
        public static readonly Ability SolarPower = new Ability( 94, "Solar Power" );
        public static readonly Ability QuickFeet = new Ability( 95, "Quick Feet" );
        public static readonly Ability Normalize = new Ability( 96, "Normalize" );
        public static readonly Ability Sniper = new Ability( 97, "Sniper" );
        public static readonly Ability MagicGuard = new Ability( 98, "Magic Guard" );
        public static readonly Ability NoGuard = new Ability( 99, "No Guard" );
        public static readonly Ability Stall = new Ability( 100, "Stall" );
        public static readonly Ability Technician = new Ability( 101, "Technician" );
        public static readonly Ability LeafGuard = new Ability( 102, "Leaf Guard" );
        public static readonly Ability Klutz = new Ability( 103, "Klutz" );
        public static readonly Ability MoldBreaker = new Ability( 104, "Mold Breaker" );
        public static readonly Ability SuperLuck = new Ability( 105, "Super Luck" );
        public static readonly Ability Aftermath = new Ability( 106, "Aftermath" );
        public static readonly Ability Anticipation = new Ability( 107, "Anticipation" );
        public static readonly Ability Forewarn = new Ability( 108, "Forewarn" );
        public static readonly Ability Unaware = new Ability( 109, "Unaware" );
        public static readonly Ability TintedLens = new Ability( 110, "Tinted Lens" );
        public static readonly Ability Filter = new Ability( 111, "Filter" );
        public static readonly Ability SlowStart = new Ability( 112, "Slow Start" );
        public static readonly Ability Scrappy = new Ability( 113, "Scrappy" );
        public static readonly Ability StormDrain = new Ability( 114, "Storm Drain" );
        public static readonly Ability IceBody = new Ability( 115, "Ice Body" );
        public static readonly Ability SolidRock = new Ability( 116, "Solid Rock" );
        public static readonly Ability SnowWarning = new Ability( 117, "Snow Warning" );
        public static readonly Ability HoneyGather = new Ability( 118, "Honey Gather" );
        public static readonly Ability Frisk = new Ability( 119, "Frisk" );
        public static readonly Ability Reckless = new Ability( 120, "Reckless" );
        public static readonly Ability Multitype = new Ability( 121, "Multitype" );
        public static readonly Ability FlowerGift = new Ability( 122, "Flower Gift" );
        public static readonly Ability BadDreams = new Ability( 123, "Bad Dreams" );
        public static readonly Ability Pickpocket = new Ability( 124, "Pickpocket" );
        public static readonly Ability SheerForce = new Ability( 125, "Sheer Force" );
        public static readonly Ability Contrary = new Ability( 126, "Contrary" );
        public static readonly Ability Unnerve = new Ability( 127, "Unnerve" );
        public static readonly Ability Defiant = new Ability( 128, "Defiant" );
        public static readonly Ability Defeatist = new Ability( 129, "Defeatist" );
        public static readonly Ability CursedBody = new Ability( 130, "Cursed Body" );
        public static readonly Ability Healer = new Ability( 131, "Healer" );
        public static readonly Ability FriendGuard = new Ability( 132, "Friend Guard" );
        public static readonly Ability WeakArmor = new Ability( 133, "Weak Armor" );
        public static readonly Ability HeavyMetal = new Ability( 134, "Heavy Metal" );
        public static readonly Ability LightMetal = new Ability( 135, "Light Metal" );
        public static readonly Ability Multiscale = new Ability( 136, "Multiscale" );
        public static readonly Ability ToxicBoost = new Ability( 137, "Toxic Boost" );
        public static readonly Ability FlareBoost = new Ability( 138, "Flare Boost" );
        public static readonly Ability Harvest = new Ability( 139, "Harvest" );
        public static readonly Ability Telepathy = new Ability( 140, "Telepathy" );
        public static readonly Ability Moody = new Ability( 141, "Moody" );
        public static readonly Ability Overcoat = new Ability( 142, "Overcoat" );
        public static readonly Ability PoisonTouch = new Ability( 143, "Poison Touch" );
        public static readonly Ability Regenerator = new Ability( 144, "Regenerator" );
        public static readonly Ability BigPecks = new Ability( 145, "Big Pecks" );
        public static readonly Ability SandRush = new Ability( 146, "Sand Rush" );
        public static readonly Ability WonderSkin = new Ability( 147, "Wonder Skin" );
        public static readonly Ability Analytic = new Ability( 148, "Analytic" );
        public static readonly Ability Illusion = new Ability( 149, "Illusion" );
        public static readonly Ability Imposter = new Ability( 150, "Imposter" );
        public static readonly Ability Infiltrator = new Ability( 151, "Infiltrator" );
        public static readonly Ability Mummy = new Ability( 152, "Mummy" );
        public static readonly Ability Moxie = new Ability( 153, "Moxie" );
        public static readonly Ability Justified = new Ability( 154, "Justified" );
        public static readonly Ability Rattled = new Ability( 155, "Rattled" );
        public static readonly Ability MagicBounce = new Ability( 156, "Magic Bounce" );
        public static readonly Ability SapSipper = new Ability( 157, "Sap Sipper" );
        public static readonly Ability Prankster = new Ability( 158, "Prankster" );
        public static readonly Ability SandForce = new Ability( 159, "Sand Force" );
        public static readonly Ability IronBarbs = new Ability( 160, "Iron Barbs" );
        public static readonly Ability ZenMode = new Ability( 161, "Zen Mode" );
        public static readonly Ability VictoryStar = new Ability( 162, "Victory Star" );
        public static readonly Ability Turboblaze = new Ability( 163, "Turboblaze" );
        public static readonly Ability Teravolt = new Ability( 164, "Teravolt" );
        public static readonly Ability AromaVeil = new Ability( 165, "Aroma Veil" );
        public static readonly Ability FlowerVeil = new Ability( 166, "Flower Veil" );
        public static readonly Ability CheekPouch = new Ability( 167, "Cheek Pouch" );
        public static readonly Ability Protean = new Ability( 168, "Protean" );
        public static readonly Ability FurCoat = new Ability( 169, "Fur Coat" );
        public static readonly Ability Magician = new Ability( 170, "Magician" );
        public static readonly Ability Bulletproof = new Ability( 171, "Bulletproof" );
        public static readonly Ability Competitive = new Ability( 172, "Competitive" );
        public static readonly Ability StrongJaw = new Ability( 173, "Strong Jaw" );
        public static readonly Ability Refrigerate = new Ability( 174, "Refrigerate" );
        public static readonly Ability SweetVeil = new Ability( 175, "Sweet Veil" );
        public static readonly Ability StanceChange = new Ability( 176, "Stance Change" );
        public static readonly Ability GaleWings = new Ability( 177, "Gale Wings" );
        public static readonly Ability MegaLauncher = new Ability( 178, "Mega Launcher" );
        public static readonly Ability GrassPelt = new Ability( 179, "Grass Pelt" );
        public static readonly Ability Symbiosis = new Ability( 180, "Symbiosis" );
        public static readonly Ability ToughClaws = new Ability( 181, "Tough Claws" );
        public static readonly Ability Pixilate = new Ability( 182, "Pixilate" );
        public static readonly Ability Gooey = new Ability( 183, "Gooey" );
        public static readonly Ability Aerilate = new Ability( 184, "Aerilate" );
        public static readonly Ability ParentalBond = new Ability( 185, "Parental Bond" );
        public static readonly Ability DarkAura = new Ability( 186, "Dark Aura" );
        public static readonly Ability FairyAura = new Ability( 187, "Fairy Aura" );
        public static readonly Ability AuraBreak = new Ability( 188, "Aura Break" );
        public static readonly Ability PrimordialSea = new Ability( 189, "Primordial Sea" );
        public static readonly Ability DesolateLand = new Ability( 190, "Desolate Land" );
        public static readonly Ability DeltaStream = new Ability( 191, "Delta Stream" );
        
        private static readonly Ability[] staticValues = {
            Stench,
            Drizzle,
            SpeedBoost,
            BattleArmor,
            Sturdy,
            Damp,
            Limber,
            SandVeil,
            Static,
            VoltAbsorb,
            WaterAbsorb,
            Oblivious,
            CloudNine,
            CompoundEyes,
            Insomnia,
            ColorChange,
            Immunity,
            FlashFire,
            ShieldDust,
            OwnTempo,
            SuctionCups,
            Intimidate,
            ShadowTag,
            RoughSkin,
            WonderGuard,
            Levitate,
            EffectSpore,
            Synchronize,
            ClearBody,
            NaturalCure,
            LightningRod,
            SereneGrace,
            SwiftSwim,
            Chlorophyll,
            Illuminate,
            Trace,
            HugePower,
            PoisonPoint,
            InnerFocus,
            MagmaArmor,
            WaterVeil,
            MagnetPull,
            Soundproof,
            RainDish,
            SandStream,
            Pressure,
            ThickFat,
            EarlyBird,
            FlameBody,
            RunAway,
            KeenEye,
            HyperCutter,
            Pickup,
            Truant,
            Hustle,
            CuteCharm,
            Plus,
            Minus,
            Forecast,
            StickyHold,
            ShedSkin,
            Guts,
            MarvelScale,
            LiquidOoze,
            Overgrow,
            Blaze,
            Torrent,
            Swarm,
            RockHead,
            Drought,
            ArenaTrap,
            VitalSpirit,
            WhiteSmoke,
            PurePower,
            ShellArmor,
            AirLock,
            TangledFeet,
            MotorDrive,
            Rivalry,
            Steadfast,
            SnowCloak,
            Gluttony,
            AngerPoint,
            Unburden,
            Heatproof,
            Simple,
            DrySkin,
            Download,
            IronFist,
            PoisonHeal,
            Adaptability,
            SkillLink,
            Hydration,
            SolarPower,
            QuickFeet,
            Normalize,
            Sniper,
            MagicGuard,
            NoGuard,
            Stall,
            Technician,
            LeafGuard,
            Klutz,
            MoldBreaker,
            SuperLuck,
            Aftermath,
            Anticipation,
            Forewarn,
            Unaware,
            TintedLens,
            Filter,
            SlowStart,
            Scrappy,
            StormDrain,
            IceBody,
            SolidRock,
            SnowWarning,
            HoneyGather,
            Frisk,
            Reckless,
            Multitype,
            FlowerGift,
            BadDreams,
            Pickpocket,
            SheerForce,
            Contrary,
            Unnerve,
            Defiant,
            Defeatist,
            CursedBody,
            Healer,
            FriendGuard,
            WeakArmor,
            HeavyMetal,
            LightMetal,
            Multiscale,
            ToxicBoost,
            FlareBoost,
            Harvest,
            Telepathy,
            Moody,
            Overcoat,
            PoisonTouch,
            Regenerator,
            BigPecks,
            SandRush,
            WonderSkin,
            Analytic,
            Illusion,
            Imposter,
            Infiltrator,
            Mummy,
            Moxie,
            Justified,
            Rattled,
            MagicBounce,
            SapSipper,
            Prankster,
            SandForce,
            IronBarbs,
            ZenMode,
            VictoryStar,
            Turboblaze,
            Teravolt,
            AromaVeil,
            FlowerVeil,
            CheekPouch,
            Protean,
            FurCoat,
            Magician,
            Bulletproof,
            Competitive,
            StrongJaw,
            Refrigerate,
            SweetVeil,
            StanceChange,
            GaleWings,
            MegaLauncher,
            GrassPelt,
            Symbiosis,
            ToughClaws,
            Pixilate,
            Gooey,
            Aerilate,
            ParentalBond,
            DarkAura,
            FairyAura,
            AuraBreak,
            PrimordialSea,
            DesolateLand,
            DeltaStream,
        };
        
        public static Ability GetValueFrom( int id ) => staticValues.SingleOrDefault( v => v.Id == id );
        public static IEnumerable<Ability> AllAbilities => staticValues.AsEnumerable();
    }
    
    public sealed class Item : BaseItem {
        public Item( int id, string name ) : base( id, name ) { }
        
        public static explicit operator Item( int id ) => Items.GetValueFrom( id );
        public static explicit operator int( Item val ) => val.Id;
    }
    
    public static partial class Items {
        public static readonly Item MasterBall = new Item( 1, "Master Ball" );
        public static readonly Item UltraBall = new Item( 2, "Ultra Ball" );
        public static readonly Item GreatBall = new Item( 3, "Great Ball" );
        public static readonly Item PokBall = new Item( 4, "Poké Ball" );
        public static readonly Item SafariBall = new Item( 5, "Safari Ball" );
        public static readonly Item NetBall = new Item( 6, "Net Ball" );
        public static readonly Item DiveBall = new Item( 7, "Dive Ball" );
        public static readonly Item NestBall = new Item( 8, "Nest Ball" );
        public static readonly Item RepeatBall = new Item( 9, "Repeat Ball" );
        public static readonly Item TimerBall = new Item( 10, "Timer Ball" );
        public static readonly Item LuxuryBall = new Item( 11, "Luxury Ball" );
        public static readonly Item PremierBall = new Item( 12, "Premier Ball" );
        public static readonly Item DuskBall = new Item( 13, "Dusk Ball" );
        public static readonly Item HealBall = new Item( 14, "Heal Ball" );
        public static readonly Item QuickBall = new Item( 15, "Quick Ball" );
        public static readonly Item CherishBall = new Item( 16, "Cherish Ball" );
        public static readonly Item Potion = new Item( 17, "Potion" );
        public static readonly Item Antidote = new Item( 18, "Antidote" );
        public static readonly Item BurnHeal = new Item( 19, "Burn Heal" );
        public static readonly Item IceHeal = new Item( 20, "Ice Heal" );
        public static readonly Item Awakening = new Item( 21, "Awakening" );
        public static readonly Item ParalyzeHeal = new Item( 22, "Paralyze Heal" );
        public static readonly Item FullRestore = new Item( 23, "Full Restore" );
        public static readonly Item MaxPotion = new Item( 24, "Max Potion" );
        public static readonly Item HyperPotion = new Item( 25, "Hyper Potion" );
        public static readonly Item SuperPotion = new Item( 26, "Super Potion" );
        public static readonly Item FullHeal = new Item( 27, "Full Heal" );
        public static readonly Item Revive = new Item( 28, "Revive" );
        public static readonly Item MaxRevive = new Item( 29, "Max Revive" );
        public static readonly Item FreshWater = new Item( 30, "Fresh Water" );
        public static readonly Item SodaPop = new Item( 31, "Soda Pop" );
        public static readonly Item Lemonade = new Item( 32, "Lemonade" );
        public static readonly Item MoomooMilk = new Item( 33, "Moomoo Milk" );
        public static readonly Item EnergyPowder = new Item( 34, "Energy Powder" );
        public static readonly Item EnergyRoot = new Item( 35, "Energy Root" );
        public static readonly Item HealPowder = new Item( 36, "Heal Powder" );
        public static readonly Item RevivalHerb = new Item( 37, "Revival Herb" );
        public static readonly Item Ether = new Item( 38, "Ether" );
        public static readonly Item MaxEther = new Item( 39, "Max Ether" );
        public static readonly Item Elixir = new Item( 40, "Elixir" );
        public static readonly Item MaxElixir = new Item( 41, "Max Elixir" );
        public static readonly Item LavaCookie = new Item( 42, "Lava Cookie" );
        public static readonly Item BerryJuice = new Item( 43, "Berry Juice" );
        public static readonly Item SacredAsh = new Item( 44, "Sacred Ash" );
        public static readonly Item HPUp = new Item( 45, "HP Up" );
        public static readonly Item Protein = new Item( 46, "Protein" );
        public static readonly Item Iron = new Item( 47, "Iron" );
        public static readonly Item Carbos = new Item( 48, "Carbos" );
        public static readonly Item Calcium = new Item( 49, "Calcium" );
        public static readonly Item RareCandy = new Item( 50, "Rare Candy" );
        public static readonly Item PPUp = new Item( 51, "PP Up" );
        public static readonly Item Zinc = new Item( 52, "Zinc" );
        public static readonly Item PPMax = new Item( 53, "PP Max" );
        public static readonly Item OldGateau = new Item( 54, "Old Gateau" );
        public static readonly Item GuardSpec = new Item( 55, "Guard Spec." );
        public static readonly Item DireHit = new Item( 56, "Dire Hit" );
        public static readonly Item XAttack = new Item( 57, "X Attack" );
        public static readonly Item XDefense = new Item( 58, "X Defense" );
        public static readonly Item XSpeed = new Item( 59, "X Speed" );
        public static readonly Item XAccuracy = new Item( 60, "X Accuracy" );
        public static readonly Item XSpAtk = new Item( 61, "X Sp. Atk" );
        public static readonly Item XSpDef = new Item( 62, "X Sp. Def" );
        public static readonly Item PokDoll = new Item( 63, "Poké Doll" );
        public static readonly Item FluffyTail = new Item( 64, "Fluffy Tail" );
        public static readonly Item BlueFlute = new Item( 65, "Blue Flute" );
        public static readonly Item YellowFlute = new Item( 66, "Yellow Flute" );
        public static readonly Item RedFlute = new Item( 67, "Red Flute" );
        public static readonly Item BlackFlute = new Item( 68, "Black Flute" );
        public static readonly Item WhiteFlute = new Item( 69, "White Flute" );
        public static readonly Item ShoalSalt = new Item( 70, "Shoal Salt" );
        public static readonly Item ShoalShell = new Item( 71, "Shoal Shell" );
        public static readonly Item RedShard = new Item( 72, "Red Shard" );
        public static readonly Item BlueShard = new Item( 73, "Blue Shard" );
        public static readonly Item YellowShard = new Item( 74, "Yellow Shard" );
        public static readonly Item GreenShard = new Item( 75, "Green Shard" );
        public static readonly Item SuperRepel = new Item( 76, "Super Repel" );
        public static readonly Item MaxRepel = new Item( 77, "Max Repel" );
        public static readonly Item EscapeRope = new Item( 78, "Escape Rope" );
        public static readonly Item Repel = new Item( 79, "Repel" );
        public static readonly Item SunStone = new Item( 80, "Sun Stone" );
        public static readonly Item MoonStone = new Item( 81, "Moon Stone" );
        public static readonly Item FireStone = new Item( 82, "Fire Stone" );
        public static readonly Item ThunderStone = new Item( 83, "Thunder Stone" );
        public static readonly Item WaterStone = new Item( 84, "Water Stone" );
        public static readonly Item LeafStone = new Item( 85, "Leaf Stone" );
        public static readonly Item TinyMushroom = new Item( 86, "Tiny Mushroom" );
        public static readonly Item BigMushroom = new Item( 87, "Big Mushroom" );
        public static readonly Item Pearl = new Item( 88, "Pearl" );
        public static readonly Item BigPearl = new Item( 89, "Big Pearl" );
        public static readonly Item Stardust = new Item( 90, "Stardust" );
        public static readonly Item StarPiece = new Item( 91, "Star Piece" );
        public static readonly Item Nugget = new Item( 92, "Nugget" );
        public static readonly Item HeartScale = new Item( 93, "Heart Scale" );
        public static readonly Item Honey = new Item( 94, "Honey" );
        public static readonly Item GrowthMulch = new Item( 95, "Growth Mulch" );
        public static readonly Item DampMulch = new Item( 96, "Damp Mulch" );
        public static readonly Item StableMulch = new Item( 97, "Stable Mulch" );
        public static readonly Item GooeyMulch = new Item( 98, "Gooey Mulch" );
        public static readonly Item RootFossil = new Item( 99, "Root Fossil" );
        public static readonly Item ClawFossil = new Item( 100, "Claw Fossil" );
        public static readonly Item HelixFossil = new Item( 101, "Helix Fossil" );
        public static readonly Item DomeFossil = new Item( 102, "Dome Fossil" );
        public static readonly Item OldAmber = new Item( 103, "Old Amber" );
        public static readonly Item ArmorFossil = new Item( 104, "Armor Fossil" );
        public static readonly Item SkullFossil = new Item( 105, "Skull Fossil" );
        public static readonly Item RareBone = new Item( 106, "Rare Bone" );
        public static readonly Item ShinyStone = new Item( 107, "Shiny Stone" );
        public static readonly Item DuskStone = new Item( 108, "Dusk Stone" );
        public static readonly Item DawnStone = new Item( 109, "Dawn Stone" );
        public static readonly Item OvalStone = new Item( 110, "Oval Stone" );
        public static readonly Item OddKeystone = new Item( 111, "Odd Keystone" );
        public static readonly Item GriseousOrb = new Item( 112, "Griseous Orb" );
        public static readonly Item DouseDrive = new Item( 116, "Douse Drive" );
        public static readonly Item ShockDrive = new Item( 117, "Shock Drive" );
        public static readonly Item BurnDrive = new Item( 118, "Burn Drive" );
        public static readonly Item ChillDrive = new Item( 119, "Chill Drive" );
        public static readonly Item SweetHeart = new Item( 134, "Sweet Heart" );
        public static readonly Item AdamantOrb = new Item( 135, "Adamant Orb" );
        public static readonly Item LustrousOrb = new Item( 136, "Lustrous Orb" );
        public static readonly Item GreetMail = new Item( 137, "Greet Mail" );
        public static readonly Item FavoredMail = new Item( 138, "Favored Mail" );
        public static readonly Item RSVPMail = new Item( 139, "RSVP Mail" );
        public static readonly Item ThanksMail = new Item( 140, "Thanks Mail" );
        public static readonly Item InquiryMail = new Item( 141, "Inquiry Mail" );
        public static readonly Item LikeMail = new Item( 142, "Like Mail" );
        public static readonly Item ReplyMail = new Item( 143, "Reply Mail" );
        public static readonly Item BridgeMailS = new Item( 144, "Bridge Mail S" );
        public static readonly Item BridgeMailD = new Item( 145, "Bridge Mail D" );
        public static readonly Item BridgeMailT = new Item( 146, "Bridge Mail T" );
        public static readonly Item BridgeMailV = new Item( 147, "Bridge Mail V" );
        public static readonly Item BridgeMailM = new Item( 148, "Bridge Mail M" );
        public static readonly Item CheriBerry = new Item( 149, "Cheri Berry" );
        public static readonly Item ChestoBerry = new Item( 150, "Chesto Berry" );
        public static readonly Item PechaBerry = new Item( 151, "Pecha Berry" );
        public static readonly Item RawstBerry = new Item( 152, "Rawst Berry" );
        public static readonly Item AspearBerry = new Item( 153, "Aspear Berry" );
        public static readonly Item LeppaBerry = new Item( 154, "Leppa Berry" );
        public static readonly Item OranBerry = new Item( 155, "Oran Berry" );
        public static readonly Item PersimBerry = new Item( 156, "Persim Berry" );
        public static readonly Item LumBerry = new Item( 157, "Lum Berry" );
        public static readonly Item SitrusBerry = new Item( 158, "Sitrus Berry" );
        public static readonly Item FigyBerry = new Item( 159, "Figy Berry" );
        public static readonly Item WikiBerry = new Item( 160, "Wiki Berry" );
        public static readonly Item MagoBerry = new Item( 161, "Mago Berry" );
        public static readonly Item AguavBerry = new Item( 162, "Aguav Berry" );
        public static readonly Item IapapaBerry = new Item( 163, "Iapapa Berry" );
        public static readonly Item RazzBerry = new Item( 164, "Razz Berry" );
        public static readonly Item BlukBerry = new Item( 165, "Bluk Berry" );
        public static readonly Item NanabBerry = new Item( 166, "Nanab Berry" );
        public static readonly Item WepearBerry = new Item( 167, "Wepear Berry" );
        public static readonly Item PinapBerry = new Item( 168, "Pinap Berry" );
        public static readonly Item PomegBerry = new Item( 169, "Pomeg Berry" );
        public static readonly Item KelpsyBerry = new Item( 170, "Kelpsy Berry" );
        public static readonly Item QualotBerry = new Item( 171, "Qualot Berry" );
        public static readonly Item HondewBerry = new Item( 172, "Hondew Berry" );
        public static readonly Item GrepaBerry = new Item( 173, "Grepa Berry" );
        public static readonly Item TamatoBerry = new Item( 174, "Tamato Berry" );
        public static readonly Item CornnBerry = new Item( 175, "Cornn Berry" );
        public static readonly Item MagostBerry = new Item( 176, "Magost Berry" );
        public static readonly Item RabutaBerry = new Item( 177, "Rabuta Berry" );
        public static readonly Item NomelBerry = new Item( 178, "Nomel Berry" );
        public static readonly Item SpelonBerry = new Item( 179, "Spelon Berry" );
        public static readonly Item PamtreBerry = new Item( 180, "Pamtre Berry" );
        public static readonly Item WatmelBerry = new Item( 181, "Watmel Berry" );
        public static readonly Item DurinBerry = new Item( 182, "Durin Berry" );
        public static readonly Item BelueBerry = new Item( 183, "Belue Berry" );
        public static readonly Item OccaBerry = new Item( 184, "Occa Berry" );
        public static readonly Item PasshoBerry = new Item( 185, "Passho Berry" );
        public static readonly Item WacanBerry = new Item( 186, "Wacan Berry" );
        public static readonly Item RindoBerry = new Item( 187, "Rindo Berry" );
        public static readonly Item YacheBerry = new Item( 188, "Yache Berry" );
        public static readonly Item ChopleBerry = new Item( 189, "Chople Berry" );
        public static readonly Item KebiaBerry = new Item( 190, "Kebia Berry" );
        public static readonly Item ShucaBerry = new Item( 191, "Shuca Berry" );
        public static readonly Item CobaBerry = new Item( 192, "Coba Berry" );
        public static readonly Item PayapaBerry = new Item( 193, "Payapa Berry" );
        public static readonly Item TangaBerry = new Item( 194, "Tanga Berry" );
        public static readonly Item ChartiBerry = new Item( 195, "Charti Berry" );
        public static readonly Item KasibBerry = new Item( 196, "Kasib Berry" );
        public static readonly Item HabanBerry = new Item( 197, "Haban Berry" );
        public static readonly Item ColburBerry = new Item( 198, "Colbur Berry" );
        public static readonly Item BabiriBerry = new Item( 199, "Babiri Berry" );
        public static readonly Item ChilanBerry = new Item( 200, "Chilan Berry" );
        public static readonly Item LiechiBerry = new Item( 201, "Liechi Berry" );
        public static readonly Item GanlonBerry = new Item( 202, "Ganlon Berry" );
        public static readonly Item SalacBerry = new Item( 203, "Salac Berry" );
        public static readonly Item PetayaBerry = new Item( 204, "Petaya Berry" );
        public static readonly Item ApicotBerry = new Item( 205, "Apicot Berry" );
        public static readonly Item LansatBerry = new Item( 206, "Lansat Berry" );
        public static readonly Item StarfBerry = new Item( 207, "Starf Berry" );
        public static readonly Item EnigmaBerry = new Item( 208, "Enigma Berry" );
        public static readonly Item MicleBerry = new Item( 209, "Micle Berry" );
        public static readonly Item CustapBerry = new Item( 210, "Custap Berry" );
        public static readonly Item JabocaBerry = new Item( 211, "Jaboca Berry" );
        public static readonly Item RowapBerry = new Item( 212, "Rowap Berry" );
        public static readonly Item BrightPowder = new Item( 213, "Bright Powder" );
        public static readonly Item WhiteHerb = new Item( 214, "White Herb" );
        public static readonly Item MachoBrace = new Item( 215, "Macho Brace" );
        public static readonly Item ExpShare = new Item( 216, "Exp. Share" );
        public static readonly Item QuickClaw = new Item( 217, "Quick Claw" );
        public static readonly Item SootheBell = new Item( 218, "Soothe Bell" );
        public static readonly Item MentalHerb = new Item( 219, "Mental Herb" );
        public static readonly Item ChoiceBand = new Item( 220, "Choice Band" );
        public static readonly Item KingsRock = new Item( 221, "King’s Rock" );
        public static readonly Item SilverPowder = new Item( 222, "Silver Powder" );
        public static readonly Item AmuletCoin = new Item( 223, "Amulet Coin" );
        public static readonly Item CleanseTag = new Item( 224, "Cleanse Tag" );
        public static readonly Item SoulDew = new Item( 225, "Soul Dew" );
        public static readonly Item DeepSeaTooth = new Item( 226, "Deep Sea Tooth" );
        public static readonly Item DeepSeaScale = new Item( 227, "Deep Sea Scale" );
        public static readonly Item SmokeBall = new Item( 228, "Smoke Ball" );
        public static readonly Item Everstone = new Item( 229, "Everstone" );
        public static readonly Item FocusBand = new Item( 230, "Focus Band" );
        public static readonly Item LuckyEgg = new Item( 231, "Lucky Egg" );
        public static readonly Item ScopeLens = new Item( 232, "Scope Lens" );
        public static readonly Item MetalCoat = new Item( 233, "Metal Coat" );
        public static readonly Item Leftovers = new Item( 234, "Leftovers" );
        public static readonly Item DragonScale = new Item( 235, "Dragon Scale" );
        public static readonly Item LightBall = new Item( 236, "Light Ball" );
        public static readonly Item SoftSand = new Item( 237, "Soft Sand" );
        public static readonly Item HardStone = new Item( 238, "Hard Stone" );
        public static readonly Item MiracleSeed = new Item( 239, "Miracle Seed" );
        public static readonly Item BlackGlasses = new Item( 240, "Black Glasses" );
        public static readonly Item BlackBelt = new Item( 241, "Black Belt" );
        public static readonly Item Magnet = new Item( 242, "Magnet" );
        public static readonly Item MysticWater = new Item( 243, "Mystic Water" );
        public static readonly Item SharpBeak = new Item( 244, "Sharp Beak" );
        public static readonly Item PoisonBarb = new Item( 245, "Poison Barb" );
        public static readonly Item NeverMeltIce = new Item( 246, "Never-Melt Ice" );
        public static readonly Item SpellTag = new Item( 247, "Spell Tag" );
        public static readonly Item TwistedSpoon = new Item( 248, "Twisted Spoon" );
        public static readonly Item Charcoal = new Item( 249, "Charcoal" );
        public static readonly Item DragonFang = new Item( 250, "Dragon Fang" );
        public static readonly Item SilkScarf = new Item( 251, "Silk Scarf" );
        public static readonly Item UpGrade = new Item( 252, "Up-Grade" );
        public static readonly Item ShellBell = new Item( 253, "Shell Bell" );
        public static readonly Item SeaIncense = new Item( 254, "Sea Incense" );
        public static readonly Item LaxIncense = new Item( 255, "Lax Incense" );
        public static readonly Item LuckyPunch = new Item( 256, "Lucky Punch" );
        public static readonly Item MetalPowder = new Item( 257, "Metal Powder" );
        public static readonly Item ThickClub = new Item( 258, "Thick Club" );
        public static readonly Item Stick = new Item( 259, "Stick" );
        public static readonly Item RedScarf = new Item( 260, "Red Scarf" );
        public static readonly Item BlueScarf = new Item( 261, "Blue Scarf" );
        public static readonly Item PinkScarf = new Item( 262, "Pink Scarf" );
        public static readonly Item GreenScarf = new Item( 263, "Green Scarf" );
        public static readonly Item YellowScarf = new Item( 264, "Yellow Scarf" );
        public static readonly Item WideLens = new Item( 265, "Wide Lens" );
        public static readonly Item MuscleBand = new Item( 266, "Muscle Band" );
        public static readonly Item WiseGlasses = new Item( 267, "Wise Glasses" );
        public static readonly Item ExpertBelt = new Item( 268, "Expert Belt" );
        public static readonly Item LightClay = new Item( 269, "Light Clay" );
        public static readonly Item LifeOrb = new Item( 270, "Life Orb" );
        public static readonly Item PowerHerb = new Item( 271, "Power Herb" );
        public static readonly Item ToxicOrb = new Item( 272, "Toxic Orb" );
        public static readonly Item FlameOrb = new Item( 273, "Flame Orb" );
        public static readonly Item QuickPowder = new Item( 274, "Quick Powder" );
        public static readonly Item FocusSash = new Item( 275, "Focus Sash" );
        public static readonly Item ZoomLens = new Item( 276, "Zoom Lens" );
        public static readonly Item Metronome = new Item( 277, "Metronome" );
        public static readonly Item IronBall = new Item( 278, "Iron Ball" );
        public static readonly Item LaggingTail = new Item( 279, "Lagging Tail" );
        public static readonly Item DestinyKnot = new Item( 280, "Destiny Knot" );
        public static readonly Item BlackSludge = new Item( 281, "Black Sludge" );
        public static readonly Item IcyRock = new Item( 282, "Icy Rock" );
        public static readonly Item SmoothRock = new Item( 283, "Smooth Rock" );
        public static readonly Item HeatRock = new Item( 284, "Heat Rock" );
        public static readonly Item DampRock = new Item( 285, "Damp Rock" );
        public static readonly Item GripClaw = new Item( 286, "Grip Claw" );
        public static readonly Item ChoiceScarf = new Item( 287, "Choice Scarf" );
        public static readonly Item StickyBarb = new Item( 288, "Sticky Barb" );
        public static readonly Item PowerBracer = new Item( 289, "Power Bracer" );
        public static readonly Item PowerBelt = new Item( 290, "Power Belt" );
        public static readonly Item PowerLens = new Item( 291, "Power Lens" );
        public static readonly Item PowerBand = new Item( 292, "Power Band" );
        public static readonly Item PowerAnklet = new Item( 293, "Power Anklet" );
        public static readonly Item PowerWeight = new Item( 294, "Power Weight" );
        public static readonly Item ShedShell = new Item( 295, "Shed Shell" );
        public static readonly Item BigRoot = new Item( 296, "Big Root" );
        public static readonly Item ChoiceSpecs = new Item( 297, "Choice Specs" );
        public static readonly Item FlamePlate = new Item( 298, "Flame Plate" );
        public static readonly Item SplashPlate = new Item( 299, "Splash Plate" );
        public static readonly Item ZapPlate = new Item( 300, "Zap Plate" );
        public static readonly Item MeadowPlate = new Item( 301, "Meadow Plate" );
        public static readonly Item IciclePlate = new Item( 302, "Icicle Plate" );
        public static readonly Item FistPlate = new Item( 303, "Fist Plate" );
        public static readonly Item ToxicPlate = new Item( 304, "Toxic Plate" );
        public static readonly Item EarthPlate = new Item( 305, "Earth Plate" );
        public static readonly Item SkyPlate = new Item( 306, "Sky Plate" );
        public static readonly Item MindPlate = new Item( 307, "Mind Plate" );
        public static readonly Item InsectPlate = new Item( 308, "Insect Plate" );
        public static readonly Item StonePlate = new Item( 309, "Stone Plate" );
        public static readonly Item SpookyPlate = new Item( 310, "Spooky Plate" );
        public static readonly Item DracoPlate = new Item( 311, "Draco Plate" );
        public static readonly Item DreadPlate = new Item( 312, "Dread Plate" );
        public static readonly Item IronPlate = new Item( 313, "Iron Plate" );
        public static readonly Item OddIncense = new Item( 314, "Odd Incense" );
        public static readonly Item RockIncense = new Item( 315, "Rock Incense" );
        public static readonly Item FullIncense = new Item( 316, "Full Incense" );
        public static readonly Item WaveIncense = new Item( 317, "Wave Incense" );
        public static readonly Item RoseIncense = new Item( 318, "Rose Incense" );
        public static readonly Item LuckIncense = new Item( 319, "Luck Incense" );
        public static readonly Item PureIncense = new Item( 320, "Pure Incense" );
        public static readonly Item Protector = new Item( 321, "Protector" );
        public static readonly Item Electirizer = new Item( 322, "Electirizer" );
        public static readonly Item Magmarizer = new Item( 323, "Magmarizer" );
        public static readonly Item DubiousDisc = new Item( 324, "Dubious Disc" );
        public static readonly Item ReaperCloth = new Item( 325, "Reaper Cloth" );
        public static readonly Item RazorClaw = new Item( 326, "Razor Claw" );
        public static readonly Item RazorFang = new Item( 327, "Razor Fang" );
        public static readonly Item TM01 = new Item( 328, "TM01" );
        public static readonly Item TM02 = new Item( 329, "TM02" );
        public static readonly Item TM03 = new Item( 330, "TM03" );
        public static readonly Item TM04 = new Item( 331, "TM04" );
        public static readonly Item TM05 = new Item( 332, "TM05" );
        public static readonly Item TM06 = new Item( 333, "TM06" );
        public static readonly Item TM07 = new Item( 334, "TM07" );
        public static readonly Item TM08 = new Item( 335, "TM08" );
        public static readonly Item TM09 = new Item( 336, "TM09" );
        public static readonly Item TM10 = new Item( 337, "TM10" );
        public static readonly Item TM11 = new Item( 338, "TM11" );
        public static readonly Item TM12 = new Item( 339, "TM12" );
        public static readonly Item TM13 = new Item( 340, "TM13" );
        public static readonly Item TM14 = new Item( 341, "TM14" );
        public static readonly Item TM15 = new Item( 342, "TM15" );
        public static readonly Item TM16 = new Item( 343, "TM16" );
        public static readonly Item TM17 = new Item( 344, "TM17" );
        public static readonly Item TM18 = new Item( 345, "TM18" );
        public static readonly Item TM19 = new Item( 346, "TM19" );
        public static readonly Item TM20 = new Item( 347, "TM20" );
        public static readonly Item TM21 = new Item( 348, "TM21" );
        public static readonly Item TM22 = new Item( 349, "TM22" );
        public static readonly Item TM23 = new Item( 350, "TM23" );
        public static readonly Item TM24 = new Item( 351, "TM24" );
        public static readonly Item TM25 = new Item( 352, "TM25" );
        public static readonly Item TM26 = new Item( 353, "TM26" );
        public static readonly Item TM27 = new Item( 354, "TM27" );
        public static readonly Item TM28 = new Item( 355, "TM28" );
        public static readonly Item TM29 = new Item( 356, "TM29" );
        public static readonly Item TM30 = new Item( 357, "TM30" );
        public static readonly Item TM31 = new Item( 358, "TM31" );
        public static readonly Item TM32 = new Item( 359, "TM32" );
        public static readonly Item TM33 = new Item( 360, "TM33" );
        public static readonly Item TM34 = new Item( 361, "TM34" );
        public static readonly Item TM35 = new Item( 362, "TM35" );
        public static readonly Item TM36 = new Item( 363, "TM36" );
        public static readonly Item TM37 = new Item( 364, "TM37" );
        public static readonly Item TM38 = new Item( 365, "TM38" );
        public static readonly Item TM39 = new Item( 366, "TM39" );
        public static readonly Item TM40 = new Item( 367, "TM40" );
        public static readonly Item TM41 = new Item( 368, "TM41" );
        public static readonly Item TM42 = new Item( 369, "TM42" );
        public static readonly Item TM43 = new Item( 370, "TM43" );
        public static readonly Item TM44 = new Item( 371, "TM44" );
        public static readonly Item TM45 = new Item( 372, "TM45" );
        public static readonly Item TM46 = new Item( 373, "TM46" );
        public static readonly Item TM47 = new Item( 374, "TM47" );
        public static readonly Item TM48 = new Item( 375, "TM48" );
        public static readonly Item TM49 = new Item( 376, "TM49" );
        public static readonly Item TM50 = new Item( 377, "TM50" );
        public static readonly Item TM51 = new Item( 378, "TM51" );
        public static readonly Item TM52 = new Item( 379, "TM52" );
        public static readonly Item TM53 = new Item( 380, "TM53" );
        public static readonly Item TM54 = new Item( 381, "TM54" );
        public static readonly Item TM55 = new Item( 382, "TM55" );
        public static readonly Item TM56 = new Item( 383, "TM56" );
        public static readonly Item TM57 = new Item( 384, "TM57" );
        public static readonly Item TM58 = new Item( 385, "TM58" );
        public static readonly Item TM59 = new Item( 386, "TM59" );
        public static readonly Item TM60 = new Item( 387, "TM60" );
        public static readonly Item TM61 = new Item( 388, "TM61" );
        public static readonly Item TM62 = new Item( 389, "TM62" );
        public static readonly Item TM63 = new Item( 390, "TM63" );
        public static readonly Item TM64 = new Item( 391, "TM64" );
        public static readonly Item TM65 = new Item( 392, "TM65" );
        public static readonly Item TM66 = new Item( 393, "TM66" );
        public static readonly Item TM67 = new Item( 394, "TM67" );
        public static readonly Item TM68 = new Item( 395, "TM68" );
        public static readonly Item TM69 = new Item( 396, "TM69" );
        public static readonly Item TM70 = new Item( 397, "TM70" );
        public static readonly Item TM71 = new Item( 398, "TM71" );
        public static readonly Item TM72 = new Item( 399, "TM72" );
        public static readonly Item TM73 = new Item( 400, "TM73" );
        public static readonly Item TM74 = new Item( 401, "TM74" );
        public static readonly Item TM75 = new Item( 402, "TM75" );
        public static readonly Item TM76 = new Item( 403, "TM76" );
        public static readonly Item TM77 = new Item( 404, "TM77" );
        public static readonly Item TM78 = new Item( 405, "TM78" );
        public static readonly Item TM79 = new Item( 406, "TM79" );
        public static readonly Item TM80 = new Item( 407, "TM80" );
        public static readonly Item TM81 = new Item( 408, "TM81" );
        public static readonly Item TM82 = new Item( 409, "TM82" );
        public static readonly Item TM83 = new Item( 410, "TM83" );
        public static readonly Item TM84 = new Item( 411, "TM84" );
        public static readonly Item TM85 = new Item( 412, "TM85" );
        public static readonly Item TM86 = new Item( 413, "TM86" );
        public static readonly Item TM87 = new Item( 414, "TM87" );
        public static readonly Item TM88 = new Item( 415, "TM88" );
        public static readonly Item TM89 = new Item( 416, "TM89" );
        public static readonly Item TM90 = new Item( 417, "TM90" );
        public static readonly Item TM91 = new Item( 418, "TM91" );
        public static readonly Item TM92 = new Item( 419, "TM92" );
        public static readonly Item HM01 = new Item( 420, "HM01" );
        public static readonly Item HM02 = new Item( 421, "HM02" );
        public static readonly Item HM03 = new Item( 422, "HM03" );
        public static readonly Item HM04 = new Item( 423, "HM04" );
        public static readonly Item HM05 = new Item( 424, "HM05" );
        public static readonly Item HM06 = new Item( 425, "HM06" );
        public static readonly Item ExplorerKit = new Item( 428, "Explorer Kit" );
        public static readonly Item LootSack = new Item( 429, "Loot Sack" );
        public static readonly Item RuleBook = new Item( 430, "Rule Book" );
        public static readonly Item PokRadar = new Item( 431, "Poké Radar" );
        public static readonly Item PointCard = new Item( 432, "Point Card" );
        public static readonly Item Journal = new Item( 433, "Journal" );
        public static readonly Item SealCase = new Item( 434, "Seal Case" );
        public static readonly Item FashionCase = new Item( 435, "Fashion Case" );
        public static readonly Item SealBag = new Item( 436, "Seal Bag" );
        public static readonly Item PalPad = new Item( 437, "Pal Pad" );
        public static readonly Item WorksKey = new Item( 438, "Works Key" );
        public static readonly Item OldCharm = new Item( 439, "Old Charm" );
        public static readonly Item GalacticKey = new Item( 440, "Galactic Key" );
        public static readonly Item RedChain = new Item( 441, "Red Chain" );
        public static readonly Item TownMap = new Item( 442, "Town Map" );
        public static readonly Item VsSeeker = new Item( 443, "Vs. Seeker" );
        public static readonly Item CoinCase = new Item( 444, "Coin Case" );
        public static readonly Item OldRod = new Item( 445, "Old Rod" );
        public static readonly Item GoodRod = new Item( 446, "Good Rod" );
        public static readonly Item SuperRod = new Item( 447, "Super Rod" );
        public static readonly Item Sprayduck = new Item( 448, "Sprayduck" );
        public static readonly Item PoffinCase = new Item( 449, "Poffin Case" );
        public static readonly Item Bike = new Item( 450, "Bike" );
        public static readonly Item SuiteKey = new Item( 451, "Suite Key" );
        public static readonly Item OaksLetter = new Item( 452, "Oak’s Letter" );
        public static readonly Item LunarWing = new Item( 453, "Lunar Wing" );
        public static readonly Item MemberCard = new Item( 454, "Member Card" );
        public static readonly Item AzureFlute = new Item( 455, "Azure Flute" );
        public static readonly Item SSTicket = new Item( 456, "S.S. Ticket" );
        public static readonly Item ContestPass = new Item( 457, "Contest Pass" );
        public static readonly Item MagmaStone = new Item( 458, "Magma Stone" );
        public static readonly Item Parcel = new Item( 459, "Parcel" );
        public static readonly Item Coupon1 = new Item( 460, "Coupon 1" );
        public static readonly Item Coupon2 = new Item( 461, "Coupon 2" );
        public static readonly Item Coupon3 = new Item( 462, "Coupon 3" );
        public static readonly Item StorageKey = new Item( 463, "Storage Key" );
        public static readonly Item SecretPotion = new Item( 464, "Secret Potion" );
        public static readonly Item VsRecorder = new Item( 465, "Vs. Recorder" );
        public static readonly Item Gracidea = new Item( 466, "Gracidea" );
        public static readonly Item SecretKey = new Item( 467, "Secret Key" );
        public static readonly Item ApricornBox = new Item( 468, "Apricorn Box" );
        public static readonly Item UnownReport = new Item( 469, "Unown Report" );
        public static readonly Item BerryPots = new Item( 470, "Berry Pots" );
        public static readonly Item DowsingMachine = new Item( 471, "Dowsing Machine" );
        public static readonly Item BlueCard = new Item( 472, "Blue Card" );
        public static readonly Item SlowpokeTail = new Item( 473, "Slowpoke Tail" );
        public static readonly Item ClearBell = new Item( 474, "Clear Bell" );
        public static readonly Item CardKey = new Item( 475, "Card Key" );
        public static readonly Item BasementKey = new Item( 476, "Basement Key" );
        public static readonly Item SquirtBottle = new Item( 477, "Squirt Bottle" );
        public static readonly Item RedScale = new Item( 478, "Red Scale" );
        public static readonly Item LostItem = new Item( 479, "Lost Item" );
        public static readonly Item Pass = new Item( 480, "Pass" );
        public static readonly Item MachinePart = new Item( 481, "Machine Part" );
        public static readonly Item SilverWing = new Item( 482, "Silver Wing" );
        public static readonly Item RainbowWing = new Item( 483, "Rainbow Wing" );
        public static readonly Item MysteryEgg = new Item( 484, "Mystery Egg" );
        public static readonly Item RedApricorn = new Item( 485, "Red Apricorn" );
        public static readonly Item BlueApricorn = new Item( 486, "Blue Apricorn" );
        public static readonly Item YellowApricorn = new Item( 487, "Yellow Apricorn" );
        public static readonly Item GreenApricorn = new Item( 488, "Green Apricorn" );
        public static readonly Item PinkApricorn = new Item( 489, "Pink Apricorn" );
        public static readonly Item WhiteApricorn = new Item( 490, "White Apricorn" );
        public static readonly Item BlackApricorn = new Item( 491, "Black Apricorn" );
        public static readonly Item FastBall = new Item( 492, "Fast Ball" );
        public static readonly Item LevelBall = new Item( 493, "Level Ball" );
        public static readonly Item LureBall = new Item( 494, "Lure Ball" );
        public static readonly Item HeavyBall = new Item( 495, "Heavy Ball" );
        public static readonly Item LoveBall = new Item( 496, "Love Ball" );
        public static readonly Item FriendBall = new Item( 497, "Friend Ball" );
        public static readonly Item MoonBall = new Item( 498, "Moon Ball" );
        public static readonly Item SportBall = new Item( 499, "Sport Ball" );
        public static readonly Item ParkBall = new Item( 500, "Park Ball" );
        public static readonly Item PhotoAlbum = new Item( 501, "Photo Album" );
        public static readonly Item GBSounds = new Item( 502, "GB Sounds" );
        public static readonly Item TidalBell = new Item( 503, "Tidal Bell" );
        public static readonly Item RageCandyBar = new Item( 504, "Rage Candy Bar" );
        public static readonly Item DataCard01 = new Item( 505, "Data Card 01" );
        public static readonly Item DataCard02 = new Item( 506, "Data Card 02" );
        public static readonly Item DataCard03 = new Item( 507, "Data Card 03" );
        public static readonly Item DataCard04 = new Item( 508, "Data Card 04" );
        public static readonly Item DataCard05 = new Item( 509, "Data Card 05" );
        public static readonly Item DataCard06 = new Item( 510, "Data Card 06" );
        public static readonly Item DataCard07 = new Item( 511, "Data Card 07" );
        public static readonly Item DataCard08 = new Item( 512, "Data Card 08" );
        public static readonly Item DataCard09 = new Item( 513, "Data Card 09" );
        public static readonly Item DataCard10 = new Item( 514, "Data Card 10" );
        public static readonly Item DataCard11 = new Item( 515, "Data Card 11" );
        public static readonly Item DataCard12 = new Item( 516, "Data Card 12" );
        public static readonly Item DataCard13 = new Item( 517, "Data Card 13" );
        public static readonly Item DataCard14 = new Item( 518, "Data Card 14" );
        public static readonly Item DataCard15 = new Item( 519, "Data Card 15" );
        public static readonly Item DataCard16 = new Item( 520, "Data Card 16" );
        public static readonly Item DataCard17 = new Item( 521, "Data Card 17" );
        public static readonly Item DataCard18 = new Item( 522, "Data Card 18" );
        public static readonly Item DataCard19 = new Item( 523, "Data Card 19" );
        public static readonly Item DataCard20 = new Item( 524, "Data Card 20" );
        public static readonly Item DataCard21 = new Item( 525, "Data Card 21" );
        public static readonly Item DataCard22 = new Item( 526, "Data Card 22" );
        public static readonly Item DataCard23 = new Item( 527, "Data Card 23" );
        public static readonly Item DataCard24 = new Item( 528, "Data Card 24" );
        public static readonly Item DataCard25 = new Item( 529, "Data Card 25" );
        public static readonly Item DataCard26 = new Item( 530, "Data Card 26" );
        public static readonly Item DataCard27 = new Item( 531, "Data Card 27" );
        public static readonly Item JadeOrb = new Item( 532, "Jade Orb" );
        public static readonly Item LockCapsule = new Item( 533, "Lock Capsule" );
        public static readonly Item RedOrb = new Item( 534, "Red Orb" );
        public static readonly Item BlueOrb = new Item( 535, "Blue Orb" );
        public static readonly Item EnigmaStone = new Item( 536, "Enigma Stone" );
        public static readonly Item PrismScale = new Item( 537, "Prism Scale" );
        public static readonly Item Eviolite = new Item( 538, "Eviolite" );
        public static readonly Item FloatStone = new Item( 539, "Float Stone" );
        public static readonly Item RockyHelmet = new Item( 540, "Rocky Helmet" );
        public static readonly Item AirBalloon = new Item( 541, "Air Balloon" );
        public static readonly Item RedCard = new Item( 542, "Red Card" );
        public static readonly Item RingTarget = new Item( 543, "Ring Target" );
        public static readonly Item BindingBand = new Item( 544, "Binding Band" );
        public static readonly Item AbsorbBulb = new Item( 545, "Absorb Bulb" );
        public static readonly Item CellBattery = new Item( 546, "Cell Battery" );
        public static readonly Item EjectButton = new Item( 547, "Eject Button" );
        public static readonly Item FireGem = new Item( 548, "Fire Gem" );
        public static readonly Item WaterGem = new Item( 549, "Water Gem" );
        public static readonly Item ElectricGem = new Item( 550, "Electric Gem" );
        public static readonly Item GrassGem = new Item( 551, "Grass Gem" );
        public static readonly Item IceGem = new Item( 552, "Ice Gem" );
        public static readonly Item FightingGem = new Item( 553, "Fighting Gem" );
        public static readonly Item PoisonGem = new Item( 554, "Poison Gem" );
        public static readonly Item GroundGem = new Item( 555, "Ground Gem" );
        public static readonly Item FlyingGem = new Item( 556, "Flying Gem" );
        public static readonly Item PsychicGem = new Item( 557, "Psychic Gem" );
        public static readonly Item BugGem = new Item( 558, "Bug Gem" );
        public static readonly Item RockGem = new Item( 559, "Rock Gem" );
        public static readonly Item GhostGem = new Item( 560, "Ghost Gem" );
        public static readonly Item DragonGem = new Item( 561, "Dragon Gem" );
        public static readonly Item DarkGem = new Item( 562, "Dark Gem" );
        public static readonly Item SteelGem = new Item( 563, "Steel Gem" );
        public static readonly Item NormalGem = new Item( 564, "Normal Gem" );
        public static readonly Item HealthWing = new Item( 565, "Health Wing" );
        public static readonly Item MuscleWing = new Item( 566, "Muscle Wing" );
        public static readonly Item ResistWing = new Item( 567, "Resist Wing" );
        public static readonly Item GeniusWing = new Item( 568, "Genius Wing" );
        public static readonly Item CleverWing = new Item( 569, "Clever Wing" );
        public static readonly Item SwiftWing = new Item( 570, "Swift Wing" );
        public static readonly Item PrettyWing = new Item( 571, "Pretty Wing" );
        public static readonly Item CoverFossil = new Item( 572, "Cover Fossil" );
        public static readonly Item PlumeFossil = new Item( 573, "Plume Fossil" );
        public static readonly Item LibertyPass = new Item( 574, "Liberty Pass" );
        public static readonly Item PassOrb = new Item( 575, "Pass Orb" );
        public static readonly Item DreamBall = new Item( 576, "Dream Ball" );
        public static readonly Item PokToy = new Item( 577, "Poké Toy" );
        public static readonly Item PropCase = new Item( 578, "Prop Case" );
        public static readonly Item DragonSkull = new Item( 579, "Dragon Skull" );
        public static readonly Item BalmMushroom = new Item( 580, "Balm Mushroom" );
        public static readonly Item BigNugget = new Item( 581, "Big Nugget" );
        public static readonly Item PearlString = new Item( 582, "Pearl String" );
        public static readonly Item CometShard = new Item( 583, "Comet Shard" );
        public static readonly Item RelicCopper = new Item( 584, "Relic Copper" );
        public static readonly Item RelicSilver = new Item( 585, "Relic Silver" );
        public static readonly Item RelicGold = new Item( 586, "Relic Gold" );
        public static readonly Item RelicVase = new Item( 587, "Relic Vase" );
        public static readonly Item RelicBand = new Item( 588, "Relic Band" );
        public static readonly Item RelicStatue = new Item( 589, "Relic Statue" );
        public static readonly Item RelicCrown = new Item( 590, "Relic Crown" );
        public static readonly Item Casteliacone = new Item( 591, "Casteliacone" );
        public static readonly Item DireHit2 = new Item( 592, "Dire Hit 2" );
        public static readonly Item XSpeed2 = new Item( 593, "X Speed 2" );
        public static readonly Item XSpAtk2 = new Item( 594, "X Sp. Atk 2" );
        public static readonly Item XSpDef2 = new Item( 595, "X Sp. Def 2" );
        public static readonly Item XDefense2 = new Item( 596, "X Defense 2" );
        public static readonly Item XAttack2 = new Item( 597, "X Attack 2" );
        public static readonly Item XAccuracy2 = new Item( 598, "X Accuracy 2" );
        public static readonly Item XSpeed3 = new Item( 599, "X Speed 3" );
        public static readonly Item XSpAtk3 = new Item( 600, "X Sp. Atk 3" );
        public static readonly Item XSpDef3 = new Item( 601, "X Sp. Def 3" );
        public static readonly Item XDefense3 = new Item( 602, "X Defense 3" );
        public static readonly Item XAttack3 = new Item( 603, "X Attack 3" );
        public static readonly Item XAccuracy3 = new Item( 604, "X Accuracy 3" );
        public static readonly Item XSpeed6 = new Item( 605, "X Speed 6" );
        public static readonly Item XSpAtk6 = new Item( 606, "X Sp. Atk 6" );
        public static readonly Item XSpDef6 = new Item( 607, "X Sp. Def 6" );
        public static readonly Item XDefense6 = new Item( 608, "X Defense 6" );
        public static readonly Item XAttack6 = new Item( 609, "X Attack 6" );
        public static readonly Item XAccuracy6 = new Item( 610, "X Accuracy 6" );
        public static readonly Item AbilityUrge = new Item( 611, "Ability Urge" );
        public static readonly Item ItemDrop = new Item( 612, "Item Drop" );
        public static readonly Item ItemUrge = new Item( 613, "Item Urge" );
        public static readonly Item ResetUrge = new Item( 614, "Reset Urge" );
        public static readonly Item DireHit3 = new Item( 615, "Dire Hit 3" );
        public static readonly Item LightStone = new Item( 616, "Light Stone" );
        public static readonly Item DarkStone = new Item( 617, "Dark Stone" );
        public static readonly Item TM93 = new Item( 618, "TM93" );
        public static readonly Item TM94 = new Item( 619, "TM94" );
        public static readonly Item TM95 = new Item( 620, "TM95" );
        public static readonly Item Xtransceiver = new Item( 621, "Xtransceiver" );
        public static readonly Item Gram1 = new Item( 623, "Gram 1" );
        public static readonly Item Gram2 = new Item( 624, "Gram 2" );
        public static readonly Item Gram3 = new Item( 625, "Gram 3" );
        public static readonly Item Xtransceiver_2 = new Item( 626, "Xtransceiver" );
        public static readonly Item MedalBox = new Item( 627, "Medal Box" );
        public static readonly Item DNASplicers = new Item( 628, "DNA Splicers" );
        public static readonly Item DNASplicers_2 = new Item( 629, "DNA Splicers" );
        public static readonly Item Permit = new Item( 630, "Permit" );
        public static readonly Item OvalCharm = new Item( 631, "Oval Charm" );
        public static readonly Item ShinyCharm = new Item( 632, "Shiny Charm" );
        public static readonly Item PlasmaCard = new Item( 633, "Plasma Card" );
        public static readonly Item GrubbyHanky = new Item( 634, "Grubby Hanky" );
        public static readonly Item ColressMachine = new Item( 635, "Colress Machine" );
        public static readonly Item DroppedItem = new Item( 636, "Dropped Item" );
        public static readonly Item DroppedItem_2 = new Item( 637, "Dropped Item" );
        public static readonly Item RevealGlass = new Item( 638, "Reveal Glass" );
        public static readonly Item WeaknessPolicy = new Item( 639, "Weakness Policy" );
        public static readonly Item AssaultVest = new Item( 640, "Assault Vest" );
        public static readonly Item HoloCaster = new Item( 641, "Holo Caster" );
        public static readonly Item ProfsLetter = new Item( 642, "Prof’s Letter" );
        public static readonly Item RollerSkates = new Item( 643, "Roller Skates" );
        public static readonly Item PixiePlate = new Item( 644, "Pixie Plate" );
        public static readonly Item AbilityCapsule = new Item( 645, "Ability Capsule" );
        public static readonly Item WhippedDream = new Item( 646, "Whipped Dream" );
        public static readonly Item Sachet = new Item( 647, "Sachet" );
        public static readonly Item LuminousMoss = new Item( 648, "Luminous Moss" );
        public static readonly Item Snowball = new Item( 649, "Snowball" );
        public static readonly Item SafetyGoggles = new Item( 650, "Safety Goggles" );
        public static readonly Item PokFlute = new Item( 651, "Poké Flute" );
        public static readonly Item RichMulch = new Item( 652, "Rich Mulch" );
        public static readonly Item SurpriseMulch = new Item( 653, "Surprise Mulch" );
        public static readonly Item BoostMulch = new Item( 654, "Boost Mulch" );
        public static readonly Item AmazeMulch = new Item( 655, "Amaze Mulch" );
        public static readonly Item Gengarite = new Item( 656, "Gengarite" );
        public static readonly Item Gardevoirite = new Item( 657, "Gardevoirite" );
        public static readonly Item Ampharosite = new Item( 658, "Ampharosite" );
        public static readonly Item Venusaurite = new Item( 659, "Venusaurite" );
        public static readonly Item CharizarditeX = new Item( 660, "Charizardite X" );
        public static readonly Item Blastoisinite = new Item( 661, "Blastoisinite" );
        public static readonly Item MewtwoniteX = new Item( 662, "Mewtwonite X" );
        public static readonly Item MewtwoniteY = new Item( 663, "Mewtwonite Y" );
        public static readonly Item Blazikenite = new Item( 664, "Blazikenite" );
        public static readonly Item Medichamite = new Item( 665, "Medichamite" );
        public static readonly Item Houndoominite = new Item( 666, "Houndoominite" );
        public static readonly Item Aggronite = new Item( 667, "Aggronite" );
        public static readonly Item Banettite = new Item( 668, "Banettite" );
        public static readonly Item Tyranitarite = new Item( 669, "Tyranitarite" );
        public static readonly Item Scizorite = new Item( 670, "Scizorite" );
        public static readonly Item Pinsirite = new Item( 671, "Pinsirite" );
        public static readonly Item Aerodactylite = new Item( 672, "Aerodactylite" );
        public static readonly Item Lucarionite = new Item( 673, "Lucarionite" );
        public static readonly Item Abomasite = new Item( 674, "Abomasite" );
        public static readonly Item Kangaskhanite = new Item( 675, "Kangaskhanite" );
        public static readonly Item Gyaradosite = new Item( 676, "Gyaradosite" );
        public static readonly Item Absolite = new Item( 677, "Absolite" );
        public static readonly Item CharizarditeY = new Item( 678, "Charizardite Y" );
        public static readonly Item Alakazite = new Item( 679, "Alakazite" );
        public static readonly Item Heracronite = new Item( 680, "Heracronite" );
        public static readonly Item Mawilite = new Item( 681, "Mawilite" );
        public static readonly Item Manectite = new Item( 682, "Manectite" );
        public static readonly Item Garchompite = new Item( 683, "Garchompite" );
        public static readonly Item Latiasite = new Item( 684, "Latiasite" );
        public static readonly Item Latiosite = new Item( 685, "Latiosite" );
        public static readonly Item RoseliBerry = new Item( 686, "Roseli Berry" );
        public static readonly Item KeeBerry = new Item( 687, "Kee Berry" );
        public static readonly Item MarangaBerry = new Item( 688, "Maranga Berry" );
        public static readonly Item Sprinklotad = new Item( 689, "Sprinklotad" );
        public static readonly Item TM96 = new Item( 690, "TM96" );
        public static readonly Item TM97 = new Item( 691, "TM97" );
        public static readonly Item TM98 = new Item( 692, "TM98" );
        public static readonly Item TM99 = new Item( 693, "TM99" );
        public static readonly Item TM100 = new Item( 694, "TM100" );
        public static readonly Item PowerPlantPass = new Item( 695, "Power Plant Pass" );
        public static readonly Item MegaRing = new Item( 696, "Mega Ring" );
        public static readonly Item IntriguingStone = new Item( 697, "Intriguing Stone" );
        public static readonly Item CommonStone = new Item( 698, "Common Stone" );
        public static readonly Item DiscountCoupon = new Item( 699, "Discount Coupon" );
        public static readonly Item ElevatorKey = new Item( 700, "Elevator Key" );
        public static readonly Item TMVPass = new Item( 701, "TMV Pass" );
        public static readonly Item HonorofKalos = new Item( 702, "Honor of Kalos" );
        public static readonly Item AdventureRules = new Item( 703, "Adventure Rules" );
        public static readonly Item StrangeSouvenir = new Item( 704, "Strange Souvenir" );
        public static readonly Item LensCase = new Item( 705, "Lens Case" );
        public static readonly Item TravelTrunk = new Item( 706, "Travel Trunk" );
        public static readonly Item TravelTrunk_2 = new Item( 707, "Travel Trunk" );
        public static readonly Item LumioseGalette = new Item( 708, "Lumiose Galette" );
        public static readonly Item ShalourSable = new Item( 709, "Shalour Sable" );
        public static readonly Item JawFossil = new Item( 710, "Jaw Fossil" );
        public static readonly Item SailFossil = new Item( 711, "Sail Fossil" );
        public static readonly Item LookerTicket = new Item( 712, "Looker Ticket" );
        public static readonly Item Bike_2 = new Item( 713, "Bike" );
        public static readonly Item HoloCaster_2 = new Item( 714, "Holo Caster" );
        public static readonly Item FairyGem = new Item( 715, "Fairy Gem" );
        public static readonly Item MegaCharm = new Item( 716, "Mega Charm" );
        public static readonly Item MegaGlove = new Item( 717, "Mega Glove" );
        public static readonly Item MachBike = new Item( 718, "Mach Bike" );
        public static readonly Item AcroBike = new Item( 719, "Acro Bike" );
        public static readonly Item WailmerPail = new Item( 720, "Wailmer Pail" );
        public static readonly Item DevonParts = new Item( 721, "Devon Parts" );
        public static readonly Item SootSack = new Item( 722, "Soot Sack" );
        public static readonly Item BasementKey_2 = new Item( 723, "Basement Key" );
        public static readonly Item PokblockKit = new Item( 724, "Pokéblock Kit" );
        public static readonly Item Letter = new Item( 725, "Letter" );
        public static readonly Item EonTicket = new Item( 726, "Eon Ticket" );
        public static readonly Item Scanner = new Item( 727, "Scanner" );
        public static readonly Item GoGoggles = new Item( 728, "Go-Goggles" );
        public static readonly Item Meteorite = new Item( 729, "Meteorite" );
        public static readonly Item KeytoRoom1 = new Item( 730, "Key to Room 1" );
        public static readonly Item KeytoRoom2 = new Item( 731, "Key to Room 2" );
        public static readonly Item KeytoRoom4 = new Item( 732, "Key to Room 4" );
        public static readonly Item KeytoRoom6 = new Item( 733, "Key to Room 6" );
        public static readonly Item StorageKey_2 = new Item( 734, "Storage Key" );
        public static readonly Item DevonScope = new Item( 735, "Devon Scope" );
        public static readonly Item SSTicket_2 = new Item( 736, "S.S. Ticket" );
        public static readonly Item HM07 = new Item( 737, "HM07" );
        public static readonly Item DevonScubaGear = new Item( 738, "Devon Scuba Gear" );
        public static readonly Item ContestCostume = new Item( 739, "Contest Costume" );
        public static readonly Item ContestCostume_2 = new Item( 740, "Contest Costume" );
        public static readonly Item MagmaSuit = new Item( 741, "Magma Suit" );
        public static readonly Item AquaSuit = new Item( 742, "Aqua Suit" );
        public static readonly Item PairofTickets = new Item( 743, "Pair of Tickets" );
        public static readonly Item MegaBracelet = new Item( 744, "Mega Bracelet" );
        public static readonly Item MegaPendant = new Item( 745, "Mega Pendant" );
        public static readonly Item MegaGlasses = new Item( 746, "Mega Glasses" );
        public static readonly Item MegaAnchor = new Item( 747, "Mega Anchor" );
        public static readonly Item MegaStickpin = new Item( 748, "Mega Stickpin" );
        public static readonly Item MegaTiara = new Item( 749, "Mega Tiara" );
        public static readonly Item MegaAnklet = new Item( 750, "Mega Anklet" );
        public static readonly Item Meteorite_2 = new Item( 751, "Meteorite" );
        public static readonly Item Swampertite = new Item( 752, "Swampertite" );
        public static readonly Item Sceptilite = new Item( 753, "Sceptilite" );
        public static readonly Item Sablenite = new Item( 754, "Sablenite" );
        public static readonly Item Altarianite = new Item( 755, "Altarianite" );
        public static readonly Item Galladite = new Item( 756, "Galladite" );
        public static readonly Item Audinite = new Item( 757, "Audinite" );
        public static readonly Item Metagrossite = new Item( 758, "Metagrossite" );
        public static readonly Item Sharpedonite = new Item( 759, "Sharpedonite" );
        public static readonly Item Slowbronite = new Item( 760, "Slowbronite" );
        public static readonly Item Steelixite = new Item( 761, "Steelixite" );
        public static readonly Item Pidgeotite = new Item( 762, "Pidgeotite" );
        public static readonly Item Glalitite = new Item( 763, "Glalitite" );
        public static readonly Item Diancite = new Item( 764, "Diancite" );
        public static readonly Item PrisonBottle = new Item( 765, "Prison Bottle" );
        public static readonly Item MegaCuff = new Item( 766, "Mega Cuff" );
        public static readonly Item Cameruptite = new Item( 767, "Cameruptite" );
        public static readonly Item Lopunnite = new Item( 768, "Lopunnite" );
        public static readonly Item Salamencite = new Item( 769, "Salamencite" );
        public static readonly Item Beedrillite = new Item( 770, "Beedrillite" );
        public static readonly Item Meteorite_3 = new Item( 771, "Meteorite" );
        public static readonly Item Meteorite_4 = new Item( 772, "Meteorite" );
        public static readonly Item KeyStone = new Item( 773, "Key Stone" );
        public static readonly Item MeteoriteShard = new Item( 774, "Meteorite Shard" );
        public static readonly Item EonFlute = new Item( 775, "Eon Flute" );
        
        private static readonly Item[] staticValues = {
            MasterBall,
            UltraBall,
            GreatBall,
            PokBall,
            SafariBall,
            NetBall,
            DiveBall,
            NestBall,
            RepeatBall,
            TimerBall,
            LuxuryBall,
            PremierBall,
            DuskBall,
            HealBall,
            QuickBall,
            CherishBall,
            Potion,
            Antidote,
            BurnHeal,
            IceHeal,
            Awakening,
            ParalyzeHeal,
            FullRestore,
            MaxPotion,
            HyperPotion,
            SuperPotion,
            FullHeal,
            Revive,
            MaxRevive,
            FreshWater,
            SodaPop,
            Lemonade,
            MoomooMilk,
            EnergyPowder,
            EnergyRoot,
            HealPowder,
            RevivalHerb,
            Ether,
            MaxEther,
            Elixir,
            MaxElixir,
            LavaCookie,
            BerryJuice,
            SacredAsh,
            HPUp,
            Protein,
            Iron,
            Carbos,
            Calcium,
            RareCandy,
            PPUp,
            Zinc,
            PPMax,
            OldGateau,
            GuardSpec,
            DireHit,
            XAttack,
            XDefense,
            XSpeed,
            XAccuracy,
            XSpAtk,
            XSpDef,
            PokDoll,
            FluffyTail,
            BlueFlute,
            YellowFlute,
            RedFlute,
            BlackFlute,
            WhiteFlute,
            ShoalSalt,
            ShoalShell,
            RedShard,
            BlueShard,
            YellowShard,
            GreenShard,
            SuperRepel,
            MaxRepel,
            EscapeRope,
            Repel,
            SunStone,
            MoonStone,
            FireStone,
            ThunderStone,
            WaterStone,
            LeafStone,
            TinyMushroom,
            BigMushroom,
            Pearl,
            BigPearl,
            Stardust,
            StarPiece,
            Nugget,
            HeartScale,
            Honey,
            GrowthMulch,
            DampMulch,
            StableMulch,
            GooeyMulch,
            RootFossil,
            ClawFossil,
            HelixFossil,
            DomeFossil,
            OldAmber,
            ArmorFossil,
            SkullFossil,
            RareBone,
            ShinyStone,
            DuskStone,
            DawnStone,
            OvalStone,
            OddKeystone,
            GriseousOrb,
            DouseDrive,
            ShockDrive,
            BurnDrive,
            ChillDrive,
            SweetHeart,
            AdamantOrb,
            LustrousOrb,
            GreetMail,
            FavoredMail,
            RSVPMail,
            ThanksMail,
            InquiryMail,
            LikeMail,
            ReplyMail,
            BridgeMailS,
            BridgeMailD,
            BridgeMailT,
            BridgeMailV,
            BridgeMailM,
            CheriBerry,
            ChestoBerry,
            PechaBerry,
            RawstBerry,
            AspearBerry,
            LeppaBerry,
            OranBerry,
            PersimBerry,
            LumBerry,
            SitrusBerry,
            FigyBerry,
            WikiBerry,
            MagoBerry,
            AguavBerry,
            IapapaBerry,
            RazzBerry,
            BlukBerry,
            NanabBerry,
            WepearBerry,
            PinapBerry,
            PomegBerry,
            KelpsyBerry,
            QualotBerry,
            HondewBerry,
            GrepaBerry,
            TamatoBerry,
            CornnBerry,
            MagostBerry,
            RabutaBerry,
            NomelBerry,
            SpelonBerry,
            PamtreBerry,
            WatmelBerry,
            DurinBerry,
            BelueBerry,
            OccaBerry,
            PasshoBerry,
            WacanBerry,
            RindoBerry,
            YacheBerry,
            ChopleBerry,
            KebiaBerry,
            ShucaBerry,
            CobaBerry,
            PayapaBerry,
            TangaBerry,
            ChartiBerry,
            KasibBerry,
            HabanBerry,
            ColburBerry,
            BabiriBerry,
            ChilanBerry,
            LiechiBerry,
            GanlonBerry,
            SalacBerry,
            PetayaBerry,
            ApicotBerry,
            LansatBerry,
            StarfBerry,
            EnigmaBerry,
            MicleBerry,
            CustapBerry,
            JabocaBerry,
            RowapBerry,
            BrightPowder,
            WhiteHerb,
            MachoBrace,
            ExpShare,
            QuickClaw,
            SootheBell,
            MentalHerb,
            ChoiceBand,
            KingsRock,
            SilverPowder,
            AmuletCoin,
            CleanseTag,
            SoulDew,
            DeepSeaTooth,
            DeepSeaScale,
            SmokeBall,
            Everstone,
            FocusBand,
            LuckyEgg,
            ScopeLens,
            MetalCoat,
            Leftovers,
            DragonScale,
            LightBall,
            SoftSand,
            HardStone,
            MiracleSeed,
            BlackGlasses,
            BlackBelt,
            Magnet,
            MysticWater,
            SharpBeak,
            PoisonBarb,
            NeverMeltIce,
            SpellTag,
            TwistedSpoon,
            Charcoal,
            DragonFang,
            SilkScarf,
            UpGrade,
            ShellBell,
            SeaIncense,
            LaxIncense,
            LuckyPunch,
            MetalPowder,
            ThickClub,
            Stick,
            RedScarf,
            BlueScarf,
            PinkScarf,
            GreenScarf,
            YellowScarf,
            WideLens,
            MuscleBand,
            WiseGlasses,
            ExpertBelt,
            LightClay,
            LifeOrb,
            PowerHerb,
            ToxicOrb,
            FlameOrb,
            QuickPowder,
            FocusSash,
            ZoomLens,
            Metronome,
            IronBall,
            LaggingTail,
            DestinyKnot,
            BlackSludge,
            IcyRock,
            SmoothRock,
            HeatRock,
            DampRock,
            GripClaw,
            ChoiceScarf,
            StickyBarb,
            PowerBracer,
            PowerBelt,
            PowerLens,
            PowerBand,
            PowerAnklet,
            PowerWeight,
            ShedShell,
            BigRoot,
            ChoiceSpecs,
            FlamePlate,
            SplashPlate,
            ZapPlate,
            MeadowPlate,
            IciclePlate,
            FistPlate,
            ToxicPlate,
            EarthPlate,
            SkyPlate,
            MindPlate,
            InsectPlate,
            StonePlate,
            SpookyPlate,
            DracoPlate,
            DreadPlate,
            IronPlate,
            OddIncense,
            RockIncense,
            FullIncense,
            WaveIncense,
            RoseIncense,
            LuckIncense,
            PureIncense,
            Protector,
            Electirizer,
            Magmarizer,
            DubiousDisc,
            ReaperCloth,
            RazorClaw,
            RazorFang,
            TM01,
            TM02,
            TM03,
            TM04,
            TM05,
            TM06,
            TM07,
            TM08,
            TM09,
            TM10,
            TM11,
            TM12,
            TM13,
            TM14,
            TM15,
            TM16,
            TM17,
            TM18,
            TM19,
            TM20,
            TM21,
            TM22,
            TM23,
            TM24,
            TM25,
            TM26,
            TM27,
            TM28,
            TM29,
            TM30,
            TM31,
            TM32,
            TM33,
            TM34,
            TM35,
            TM36,
            TM37,
            TM38,
            TM39,
            TM40,
            TM41,
            TM42,
            TM43,
            TM44,
            TM45,
            TM46,
            TM47,
            TM48,
            TM49,
            TM50,
            TM51,
            TM52,
            TM53,
            TM54,
            TM55,
            TM56,
            TM57,
            TM58,
            TM59,
            TM60,
            TM61,
            TM62,
            TM63,
            TM64,
            TM65,
            TM66,
            TM67,
            TM68,
            TM69,
            TM70,
            TM71,
            TM72,
            TM73,
            TM74,
            TM75,
            TM76,
            TM77,
            TM78,
            TM79,
            TM80,
            TM81,
            TM82,
            TM83,
            TM84,
            TM85,
            TM86,
            TM87,
            TM88,
            TM89,
            TM90,
            TM91,
            TM92,
            HM01,
            HM02,
            HM03,
            HM04,
            HM05,
            HM06,
            ExplorerKit,
            LootSack,
            RuleBook,
            PokRadar,
            PointCard,
            Journal,
            SealCase,
            FashionCase,
            SealBag,
            PalPad,
            WorksKey,
            OldCharm,
            GalacticKey,
            RedChain,
            TownMap,
            VsSeeker,
            CoinCase,
            OldRod,
            GoodRod,
            SuperRod,
            Sprayduck,
            PoffinCase,
            Bike,
            SuiteKey,
            OaksLetter,
            LunarWing,
            MemberCard,
            AzureFlute,
            SSTicket,
            ContestPass,
            MagmaStone,
            Parcel,
            Coupon1,
            Coupon2,
            Coupon3,
            StorageKey,
            SecretPotion,
            VsRecorder,
            Gracidea,
            SecretKey,
            ApricornBox,
            UnownReport,
            BerryPots,
            DowsingMachine,
            BlueCard,
            SlowpokeTail,
            ClearBell,
            CardKey,
            BasementKey,
            SquirtBottle,
            RedScale,
            LostItem,
            Pass,
            MachinePart,
            SilverWing,
            RainbowWing,
            MysteryEgg,
            RedApricorn,
            BlueApricorn,
            YellowApricorn,
            GreenApricorn,
            PinkApricorn,
            WhiteApricorn,
            BlackApricorn,
            FastBall,
            LevelBall,
            LureBall,
            HeavyBall,
            LoveBall,
            FriendBall,
            MoonBall,
            SportBall,
            ParkBall,
            PhotoAlbum,
            GBSounds,
            TidalBell,
            RageCandyBar,
            DataCard01,
            DataCard02,
            DataCard03,
            DataCard04,
            DataCard05,
            DataCard06,
            DataCard07,
            DataCard08,
            DataCard09,
            DataCard10,
            DataCard11,
            DataCard12,
            DataCard13,
            DataCard14,
            DataCard15,
            DataCard16,
            DataCard17,
            DataCard18,
            DataCard19,
            DataCard20,
            DataCard21,
            DataCard22,
            DataCard23,
            DataCard24,
            DataCard25,
            DataCard26,
            DataCard27,
            JadeOrb,
            LockCapsule,
            RedOrb,
            BlueOrb,
            EnigmaStone,
            PrismScale,
            Eviolite,
            FloatStone,
            RockyHelmet,
            AirBalloon,
            RedCard,
            RingTarget,
            BindingBand,
            AbsorbBulb,
            CellBattery,
            EjectButton,
            FireGem,
            WaterGem,
            ElectricGem,
            GrassGem,
            IceGem,
            FightingGem,
            PoisonGem,
            GroundGem,
            FlyingGem,
            PsychicGem,
            BugGem,
            RockGem,
            GhostGem,
            DragonGem,
            DarkGem,
            SteelGem,
            NormalGem,
            HealthWing,
            MuscleWing,
            ResistWing,
            GeniusWing,
            CleverWing,
            SwiftWing,
            PrettyWing,
            CoverFossil,
            PlumeFossil,
            LibertyPass,
            PassOrb,
            DreamBall,
            PokToy,
            PropCase,
            DragonSkull,
            BalmMushroom,
            BigNugget,
            PearlString,
            CometShard,
            RelicCopper,
            RelicSilver,
            RelicGold,
            RelicVase,
            RelicBand,
            RelicStatue,
            RelicCrown,
            Casteliacone,
            DireHit2,
            XSpeed2,
            XSpAtk2,
            XSpDef2,
            XDefense2,
            XAttack2,
            XAccuracy2,
            XSpeed3,
            XSpAtk3,
            XSpDef3,
            XDefense3,
            XAttack3,
            XAccuracy3,
            XSpeed6,
            XSpAtk6,
            XSpDef6,
            XDefense6,
            XAttack6,
            XAccuracy6,
            AbilityUrge,
            ItemDrop,
            ItemUrge,
            ResetUrge,
            DireHit3,
            LightStone,
            DarkStone,
            TM93,
            TM94,
            TM95,
            Xtransceiver,
            Gram1,
            Gram2,
            Gram3,
            Xtransceiver_2,
            MedalBox,
            DNASplicers,
            DNASplicers_2,
            Permit,
            OvalCharm,
            ShinyCharm,
            PlasmaCard,
            GrubbyHanky,
            ColressMachine,
            DroppedItem,
            DroppedItem_2,
            RevealGlass,
            WeaknessPolicy,
            AssaultVest,
            HoloCaster,
            ProfsLetter,
            RollerSkates,
            PixiePlate,
            AbilityCapsule,
            WhippedDream,
            Sachet,
            LuminousMoss,
            Snowball,
            SafetyGoggles,
            PokFlute,
            RichMulch,
            SurpriseMulch,
            BoostMulch,
            AmazeMulch,
            Gengarite,
            Gardevoirite,
            Ampharosite,
            Venusaurite,
            CharizarditeX,
            Blastoisinite,
            MewtwoniteX,
            MewtwoniteY,
            Blazikenite,
            Medichamite,
            Houndoominite,
            Aggronite,
            Banettite,
            Tyranitarite,
            Scizorite,
            Pinsirite,
            Aerodactylite,
            Lucarionite,
            Abomasite,
            Kangaskhanite,
            Gyaradosite,
            Absolite,
            CharizarditeY,
            Alakazite,
            Heracronite,
            Mawilite,
            Manectite,
            Garchompite,
            Latiasite,
            Latiosite,
            RoseliBerry,
            KeeBerry,
            MarangaBerry,
            Sprinklotad,
            TM96,
            TM97,
            TM98,
            TM99,
            TM100,
            PowerPlantPass,
            MegaRing,
            IntriguingStone,
            CommonStone,
            DiscountCoupon,
            ElevatorKey,
            TMVPass,
            HonorofKalos,
            AdventureRules,
            StrangeSouvenir,
            LensCase,
            TravelTrunk,
            TravelTrunk_2,
            LumioseGalette,
            ShalourSable,
            JawFossil,
            SailFossil,
            LookerTicket,
            Bike_2,
            HoloCaster_2,
            FairyGem,
            MegaCharm,
            MegaGlove,
            MachBike,
            AcroBike,
            WailmerPail,
            DevonParts,
            SootSack,
            BasementKey_2,
            PokblockKit,
            Letter,
            EonTicket,
            Scanner,
            GoGoggles,
            Meteorite,
            KeytoRoom1,
            KeytoRoom2,
            KeytoRoom4,
            KeytoRoom6,
            StorageKey_2,
            DevonScope,
            SSTicket_2,
            HM07,
            DevonScubaGear,
            ContestCostume,
            ContestCostume_2,
            MagmaSuit,
            AquaSuit,
            PairofTickets,
            MegaBracelet,
            MegaPendant,
            MegaGlasses,
            MegaAnchor,
            MegaStickpin,
            MegaTiara,
            MegaAnklet,
            Meteorite_2,
            Swampertite,
            Sceptilite,
            Sablenite,
            Altarianite,
            Galladite,
            Audinite,
            Metagrossite,
            Sharpedonite,
            Slowbronite,
            Steelixite,
            Pidgeotite,
            Glalitite,
            Diancite,
            PrisonBottle,
            MegaCuff,
            Cameruptite,
            Lopunnite,
            Salamencite,
            Beedrillite,
            Meteorite_3,
            Meteorite_4,
            KeyStone,
            MeteoriteShard,
            EonFlute,
        };
        
        public static Item GetValueFrom( int id ) => staticValues.SingleOrDefault( v => v.Id == id );
        public static IEnumerable<Item> AllItems => staticValues.AsEnumerable();
    }
    
    public sealed class Move : BaseMove {
        public Move( int id, string name ) : base( id, name ) { }
        
        public static explicit operator Move( int id ) => Moves.GetValueFrom( id );
        public static explicit operator int( Move val ) => val.Id;
    }
    
    public static partial class Moves {
        public static readonly Move Pound = new Move( 1, "Pound" );
        public static readonly Move KarateChop = new Move( 2, "Karate Chop" );
        public static readonly Move DoubleSlap = new Move( 3, "Double Slap" );
        public static readonly Move CometPunch = new Move( 4, "Comet Punch" );
        public static readonly Move MegaPunch = new Move( 5, "Mega Punch" );
        public static readonly Move PayDay = new Move( 6, "Pay Day" );
        public static readonly Move FirePunch = new Move( 7, "Fire Punch" );
        public static readonly Move IcePunch = new Move( 8, "Ice Punch" );
        public static readonly Move ThunderPunch = new Move( 9, "Thunder Punch" );
        public static readonly Move Scratch = new Move( 10, "Scratch" );
        public static readonly Move ViceGrip = new Move( 11, "Vice Grip" );
        public static readonly Move Guillotine = new Move( 12, "Guillotine" );
        public static readonly Move RazorWind = new Move( 13, "Razor Wind" );
        public static readonly Move SwordsDance = new Move( 14, "Swords Dance" );
        public static readonly Move Cut = new Move( 15, "Cut" );
        public static readonly Move Gust = new Move( 16, "Gust" );
        public static readonly Move WingAttack = new Move( 17, "Wing Attack" );
        public static readonly Move Whirlwind = new Move( 18, "Whirlwind" );
        public static readonly Move Fly = new Move( 19, "Fly" );
        public static readonly Move Bind = new Move( 20, "Bind" );
        public static readonly Move Slam = new Move( 21, "Slam" );
        public static readonly Move VineWhip = new Move( 22, "Vine Whip" );
        public static readonly Move Stomp = new Move( 23, "Stomp" );
        public static readonly Move DoubleKick = new Move( 24, "Double Kick" );
        public static readonly Move MegaKick = new Move( 25, "Mega Kick" );
        public static readonly Move JumpKick = new Move( 26, "Jump Kick" );
        public static readonly Move RollingKick = new Move( 27, "Rolling Kick" );
        public static readonly Move SandAttack = new Move( 28, "Sand Attack" );
        public static readonly Move Headbutt = new Move( 29, "Headbutt" );
        public static readonly Move HornAttack = new Move( 30, "Horn Attack" );
        public static readonly Move FuryAttack = new Move( 31, "Fury Attack" );
        public static readonly Move HornDrill = new Move( 32, "Horn Drill" );
        public static readonly Move Tackle = new Move( 33, "Tackle" );
        public static readonly Move BodySlam = new Move( 34, "Body Slam" );
        public static readonly Move Wrap = new Move( 35, "Wrap" );
        public static readonly Move TakeDown = new Move( 36, "Take Down" );
        public static readonly Move Thrash = new Move( 37, "Thrash" );
        public static readonly Move DoubleEdge = new Move( 38, "Double-Edge" );
        public static readonly Move TailWhip = new Move( 39, "Tail Whip" );
        public static readonly Move PoisonSting = new Move( 40, "Poison Sting" );
        public static readonly Move Twineedle = new Move( 41, "Twineedle" );
        public static readonly Move PinMissile = new Move( 42, "Pin Missile" );
        public static readonly Move Leer = new Move( 43, "Leer" );
        public static readonly Move Bite = new Move( 44, "Bite" );
        public static readonly Move Growl = new Move( 45, "Growl" );
        public static readonly Move Roar = new Move( 46, "Roar" );
        public static readonly Move Sing = new Move( 47, "Sing" );
        public static readonly Move Supersonic = new Move( 48, "Supersonic" );
        public static readonly Move SonicBoom = new Move( 49, "Sonic Boom" );
        public static readonly Move Disable = new Move( 50, "Disable" );
        public static readonly Move Acid = new Move( 51, "Acid" );
        public static readonly Move Ember = new Move( 52, "Ember" );
        public static readonly Move Flamethrower = new Move( 53, "Flamethrower" );
        public static readonly Move Mist = new Move( 54, "Mist" );
        public static readonly Move WaterGun = new Move( 55, "Water Gun" );
        public static readonly Move HydroPump = new Move( 56, "Hydro Pump" );
        public static readonly Move Surf = new Move( 57, "Surf" );
        public static readonly Move IceBeam = new Move( 58, "Ice Beam" );
        public static readonly Move Blizzard = new Move( 59, "Blizzard" );
        public static readonly Move Psybeam = new Move( 60, "Psybeam" );
        public static readonly Move BubbleBeam = new Move( 61, "Bubble Beam" );
        public static readonly Move AuroraBeam = new Move( 62, "Aurora Beam" );
        public static readonly Move HyperBeam = new Move( 63, "Hyper Beam" );
        public static readonly Move Peck = new Move( 64, "Peck" );
        public static readonly Move DrillPeck = new Move( 65, "Drill Peck" );
        public static readonly Move Submission = new Move( 66, "Submission" );
        public static readonly Move LowKick = new Move( 67, "Low Kick" );
        public static readonly Move Counter = new Move( 68, "Counter" );
        public static readonly Move SeismicToss = new Move( 69, "Seismic Toss" );
        public static readonly Move Strength = new Move( 70, "Strength" );
        public static readonly Move Absorb = new Move( 71, "Absorb" );
        public static readonly Move MegaDrain = new Move( 72, "Mega Drain" );
        public static readonly Move LeechSeed = new Move( 73, "Leech Seed" );
        public static readonly Move Growth = new Move( 74, "Growth" );
        public static readonly Move RazorLeaf = new Move( 75, "Razor Leaf" );
        public static readonly Move SolarBeam = new Move( 76, "Solar Beam" );
        public static readonly Move PoisonPowder = new Move( 77, "Poison Powder" );
        public static readonly Move StunSpore = new Move( 78, "Stun Spore" );
        public static readonly Move SleepPowder = new Move( 79, "Sleep Powder" );
        public static readonly Move PetalDance = new Move( 80, "Petal Dance" );
        public static readonly Move StringShot = new Move( 81, "String Shot" );
        public static readonly Move DragonRage = new Move( 82, "Dragon Rage" );
        public static readonly Move FireSpin = new Move( 83, "Fire Spin" );
        public static readonly Move ThunderShock = new Move( 84, "Thunder Shock" );
        public static readonly Move Thunderbolt = new Move( 85, "Thunderbolt" );
        public static readonly Move ThunderWave = new Move( 86, "Thunder Wave" );
        public static readonly Move Thunder = new Move( 87, "Thunder" );
        public static readonly Move RockThrow = new Move( 88, "Rock Throw" );
        public static readonly Move Earthquake = new Move( 89, "Earthquake" );
        public static readonly Move Fissure = new Move( 90, "Fissure" );
        public static readonly Move Dig = new Move( 91, "Dig" );
        public static readonly Move Toxic = new Move( 92, "Toxic" );
        public static readonly Move Confusion = new Move( 93, "Confusion" );
        public static readonly Move Psychic = new Move( 94, "Psychic" );
        public static readonly Move Hypnosis = new Move( 95, "Hypnosis" );
        public static readonly Move Meditate = new Move( 96, "Meditate" );
        public static readonly Move Agility = new Move( 97, "Agility" );
        public static readonly Move QuickAttack = new Move( 98, "Quick Attack" );
        public static readonly Move Rage = new Move( 99, "Rage" );
        public static readonly Move Teleport = new Move( 100, "Teleport" );
        public static readonly Move NightShade = new Move( 101, "Night Shade" );
        public static readonly Move Mimic = new Move( 102, "Mimic" );
        public static readonly Move Screech = new Move( 103, "Screech" );
        public static readonly Move DoubleTeam = new Move( 104, "Double Team" );
        public static readonly Move Recover = new Move( 105, "Recover" );
        public static readonly Move Harden = new Move( 106, "Harden" );
        public static readonly Move Minimize = new Move( 107, "Minimize" );
        public static readonly Move Smokescreen = new Move( 108, "Smokescreen" );
        public static readonly Move ConfuseRay = new Move( 109, "Confuse Ray" );
        public static readonly Move Withdraw = new Move( 110, "Withdraw" );
        public static readonly Move DefenseCurl = new Move( 111, "Defense Curl" );
        public static readonly Move Barrier = new Move( 112, "Barrier" );
        public static readonly Move LightScreen = new Move( 113, "Light Screen" );
        public static readonly Move Haze = new Move( 114, "Haze" );
        public static readonly Move Reflect = new Move( 115, "Reflect" );
        public static readonly Move FocusEnergy = new Move( 116, "Focus Energy" );
        public static readonly Move Bide = new Move( 117, "Bide" );
        public static readonly Move Metronome = new Move( 118, "Metronome" );
        public static readonly Move MirrorMove = new Move( 119, "Mirror Move" );
        public static readonly Move SelfDestruct = new Move( 120, "Self-Destruct" );
        public static readonly Move EggBomb = new Move( 121, "Egg Bomb" );
        public static readonly Move Lick = new Move( 122, "Lick" );
        public static readonly Move Smog = new Move( 123, "Smog" );
        public static readonly Move Sludge = new Move( 124, "Sludge" );
        public static readonly Move BoneClub = new Move( 125, "Bone Club" );
        public static readonly Move FireBlast = new Move( 126, "Fire Blast" );
        public static readonly Move Waterfall = new Move( 127, "Waterfall" );
        public static readonly Move Clamp = new Move( 128, "Clamp" );
        public static readonly Move Swift = new Move( 129, "Swift" );
        public static readonly Move SkullBash = new Move( 130, "Skull Bash" );
        public static readonly Move SpikeCannon = new Move( 131, "Spike Cannon" );
        public static readonly Move Constrict = new Move( 132, "Constrict" );
        public static readonly Move Amnesia = new Move( 133, "Amnesia" );
        public static readonly Move Kinesis = new Move( 134, "Kinesis" );
        public static readonly Move SoftBoiled = new Move( 135, "Soft-Boiled" );
        public static readonly Move HighJumpKick = new Move( 136, "High Jump Kick" );
        public static readonly Move Glare = new Move( 137, "Glare" );
        public static readonly Move DreamEater = new Move( 138, "Dream Eater" );
        public static readonly Move PoisonGas = new Move( 139, "Poison Gas" );
        public static readonly Move Barrage = new Move( 140, "Barrage" );
        public static readonly Move LeechLife = new Move( 141, "Leech Life" );
        public static readonly Move LovelyKiss = new Move( 142, "Lovely Kiss" );
        public static readonly Move SkyAttack = new Move( 143, "Sky Attack" );
        public static readonly Move Transform = new Move( 144, "Transform" );
        public static readonly Move Bubble = new Move( 145, "Bubble" );
        public static readonly Move DizzyPunch = new Move( 146, "Dizzy Punch" );
        public static readonly Move Spore = new Move( 147, "Spore" );
        public static readonly Move Flash = new Move( 148, "Flash" );
        public static readonly Move Psywave = new Move( 149, "Psywave" );
        public static readonly Move Splash = new Move( 150, "Splash" );
        public static readonly Move AcidArmor = new Move( 151, "Acid Armor" );
        public static readonly Move Crabhammer = new Move( 152, "Crabhammer" );
        public static readonly Move Explosion = new Move( 153, "Explosion" );
        public static readonly Move FurySwipes = new Move( 154, "Fury Swipes" );
        public static readonly Move Bonemerang = new Move( 155, "Bonemerang" );
        public static readonly Move Rest = new Move( 156, "Rest" );
        public static readonly Move RockSlide = new Move( 157, "Rock Slide" );
        public static readonly Move HyperFang = new Move( 158, "Hyper Fang" );
        public static readonly Move Sharpen = new Move( 159, "Sharpen" );
        public static readonly Move Conversion = new Move( 160, "Conversion" );
        public static readonly Move TriAttack = new Move( 161, "Tri Attack" );
        public static readonly Move SuperFang = new Move( 162, "Super Fang" );
        public static readonly Move Slash = new Move( 163, "Slash" );
        public static readonly Move Substitute = new Move( 164, "Substitute" );
        public static readonly Move Struggle = new Move( 165, "Struggle" );
        public static readonly Move Sketch = new Move( 166, "Sketch" );
        public static readonly Move TripleKick = new Move( 167, "Triple Kick" );
        public static readonly Move Thief = new Move( 168, "Thief" );
        public static readonly Move SpiderWeb = new Move( 169, "Spider Web" );
        public static readonly Move MindReader = new Move( 170, "Mind Reader" );
        public static readonly Move Nightmare = new Move( 171, "Nightmare" );
        public static readonly Move FlameWheel = new Move( 172, "Flame Wheel" );
        public static readonly Move Snore = new Move( 173, "Snore" );
        public static readonly Move Curse = new Move( 174, "Curse" );
        public static readonly Move Flail = new Move( 175, "Flail" );
        public static readonly Move Conversion2 = new Move( 176, "Conversion 2" );
        public static readonly Move Aeroblast = new Move( 177, "Aeroblast" );
        public static readonly Move CottonSpore = new Move( 178, "Cotton Spore" );
        public static readonly Move Reversal = new Move( 179, "Reversal" );
        public static readonly Move Spite = new Move( 180, "Spite" );
        public static readonly Move PowderSnow = new Move( 181, "Powder Snow" );
        public static readonly Move Protect = new Move( 182, "Protect" );
        public static readonly Move MachPunch = new Move( 183, "Mach Punch" );
        public static readonly Move ScaryFace = new Move( 184, "Scary Face" );
        public static readonly Move FeintAttack = new Move( 185, "Feint Attack" );
        public static readonly Move SweetKiss = new Move( 186, "Sweet Kiss" );
        public static readonly Move BellyDrum = new Move( 187, "Belly Drum" );
        public static readonly Move SludgeBomb = new Move( 188, "Sludge Bomb" );
        public static readonly Move MudSlap = new Move( 189, "Mud-Slap" );
        public static readonly Move Octazooka = new Move( 190, "Octazooka" );
        public static readonly Move Spikes = new Move( 191, "Spikes" );
        public static readonly Move ZapCannon = new Move( 192, "Zap Cannon" );
        public static readonly Move Foresight = new Move( 193, "Foresight" );
        public static readonly Move DestinyBond = new Move( 194, "Destiny Bond" );
        public static readonly Move PerishSong = new Move( 195, "Perish Song" );
        public static readonly Move IcyWind = new Move( 196, "Icy Wind" );
        public static readonly Move Detect = new Move( 197, "Detect" );
        public static readonly Move BoneRush = new Move( 198, "Bone Rush" );
        public static readonly Move LockOn = new Move( 199, "Lock-On" );
        public static readonly Move Outrage = new Move( 200, "Outrage" );
        public static readonly Move Sandstorm = new Move( 201, "Sandstorm" );
        public static readonly Move GigaDrain = new Move( 202, "Giga Drain" );
        public static readonly Move Endure = new Move( 203, "Endure" );
        public static readonly Move Charm = new Move( 204, "Charm" );
        public static readonly Move Rollout = new Move( 205, "Rollout" );
        public static readonly Move FalseSwipe = new Move( 206, "False Swipe" );
        public static readonly Move Swagger = new Move( 207, "Swagger" );
        public static readonly Move MilkDrink = new Move( 208, "Milk Drink" );
        public static readonly Move Spark = new Move( 209, "Spark" );
        public static readonly Move FuryCutter = new Move( 210, "Fury Cutter" );
        public static readonly Move SteelWing = new Move( 211, "Steel Wing" );
        public static readonly Move MeanLook = new Move( 212, "Mean Look" );
        public static readonly Move Attract = new Move( 213, "Attract" );
        public static readonly Move SleepTalk = new Move( 214, "Sleep Talk" );
        public static readonly Move HealBell = new Move( 215, "Heal Bell" );
        public static readonly Move Return = new Move( 216, "Return" );
        public static readonly Move Present = new Move( 217, "Present" );
        public static readonly Move Frustration = new Move( 218, "Frustration" );
        public static readonly Move Safeguard = new Move( 219, "Safeguard" );
        public static readonly Move PainSplit = new Move( 220, "Pain Split" );
        public static readonly Move SacredFire = new Move( 221, "Sacred Fire" );
        public static readonly Move Magnitude = new Move( 222, "Magnitude" );
        public static readonly Move DynamicPunch = new Move( 223, "Dynamic Punch" );
        public static readonly Move Megahorn = new Move( 224, "Megahorn" );
        public static readonly Move DragonBreath = new Move( 225, "Dragon Breath" );
        public static readonly Move BatonPass = new Move( 226, "Baton Pass" );
        public static readonly Move Encore = new Move( 227, "Encore" );
        public static readonly Move Pursuit = new Move( 228, "Pursuit" );
        public static readonly Move RapidSpin = new Move( 229, "Rapid Spin" );
        public static readonly Move SweetScent = new Move( 230, "Sweet Scent" );
        public static readonly Move IronTail = new Move( 231, "Iron Tail" );
        public static readonly Move MetalClaw = new Move( 232, "Metal Claw" );
        public static readonly Move VitalThrow = new Move( 233, "Vital Throw" );
        public static readonly Move MorningSun = new Move( 234, "Morning Sun" );
        public static readonly Move Synthesis = new Move( 235, "Synthesis" );
        public static readonly Move Moonlight = new Move( 236, "Moonlight" );
        public static readonly Move HiddenPower = new Move( 237, "Hidden Power" );
        public static readonly Move CrossChop = new Move( 238, "Cross Chop" );
        public static readonly Move Twister = new Move( 239, "Twister" );
        public static readonly Move RainDance = new Move( 240, "Rain Dance" );
        public static readonly Move SunnyDay = new Move( 241, "Sunny Day" );
        public static readonly Move Crunch = new Move( 242, "Crunch" );
        public static readonly Move MirrorCoat = new Move( 243, "Mirror Coat" );
        public static readonly Move PsychUp = new Move( 244, "Psych Up" );
        public static readonly Move ExtremeSpeed = new Move( 245, "Extreme Speed" );
        public static readonly Move AncientPower = new Move( 246, "Ancient Power" );
        public static readonly Move ShadowBall = new Move( 247, "Shadow Ball" );
        public static readonly Move FutureSight = new Move( 248, "Future Sight" );
        public static readonly Move RockSmash = new Move( 249, "Rock Smash" );
        public static readonly Move Whirlpool = new Move( 250, "Whirlpool" );
        public static readonly Move BeatUp = new Move( 251, "Beat Up" );
        public static readonly Move FakeOut = new Move( 252, "Fake Out" );
        public static readonly Move Uproar = new Move( 253, "Uproar" );
        public static readonly Move Stockpile = new Move( 254, "Stockpile" );
        public static readonly Move SpitUp = new Move( 255, "Spit Up" );
        public static readonly Move Swallow = new Move( 256, "Swallow" );
        public static readonly Move HeatWave = new Move( 257, "Heat Wave" );
        public static readonly Move Hail = new Move( 258, "Hail" );
        public static readonly Move Torment = new Move( 259, "Torment" );
        public static readonly Move Flatter = new Move( 260, "Flatter" );
        public static readonly Move WillOWisp = new Move( 261, "Will-O-Wisp" );
        public static readonly Move Memento = new Move( 262, "Memento" );
        public static readonly Move Facade = new Move( 263, "Facade" );
        public static readonly Move FocusPunch = new Move( 264, "Focus Punch" );
        public static readonly Move SmellingSalts = new Move( 265, "Smelling Salts" );
        public static readonly Move FollowMe = new Move( 266, "Follow Me" );
        public static readonly Move NaturePower = new Move( 267, "Nature Power" );
        public static readonly Move Charge = new Move( 268, "Charge" );
        public static readonly Move Taunt = new Move( 269, "Taunt" );
        public static readonly Move HelpingHand = new Move( 270, "Helping Hand" );
        public static readonly Move Trick = new Move( 271, "Trick" );
        public static readonly Move RolePlay = new Move( 272, "Role Play" );
        public static readonly Move Wish = new Move( 273, "Wish" );
        public static readonly Move Assist = new Move( 274, "Assist" );
        public static readonly Move Ingrain = new Move( 275, "Ingrain" );
        public static readonly Move Superpower = new Move( 276, "Superpower" );
        public static readonly Move MagicCoat = new Move( 277, "Magic Coat" );
        public static readonly Move Recycle = new Move( 278, "Recycle" );
        public static readonly Move Revenge = new Move( 279, "Revenge" );
        public static readonly Move BrickBreak = new Move( 280, "Brick Break" );
        public static readonly Move Yawn = new Move( 281, "Yawn" );
        public static readonly Move KnockOff = new Move( 282, "Knock Off" );
        public static readonly Move Endeavor = new Move( 283, "Endeavor" );
        public static readonly Move Eruption = new Move( 284, "Eruption" );
        public static readonly Move SkillSwap = new Move( 285, "Skill Swap" );
        public static readonly Move Imprison = new Move( 286, "Imprison" );
        public static readonly Move Refresh = new Move( 287, "Refresh" );
        public static readonly Move Grudge = new Move( 288, "Grudge" );
        public static readonly Move Snatch = new Move( 289, "Snatch" );
        public static readonly Move SecretPower = new Move( 290, "Secret Power" );
        public static readonly Move Dive = new Move( 291, "Dive" );
        public static readonly Move ArmThrust = new Move( 292, "Arm Thrust" );
        public static readonly Move Camouflage = new Move( 293, "Camouflage" );
        public static readonly Move TailGlow = new Move( 294, "Tail Glow" );
        public static readonly Move LusterPurge = new Move( 295, "Luster Purge" );
        public static readonly Move MistBall = new Move( 296, "Mist Ball" );
        public static readonly Move FeatherDance = new Move( 297, "Feather Dance" );
        public static readonly Move TeeterDance = new Move( 298, "Teeter Dance" );
        public static readonly Move BlazeKick = new Move( 299, "Blaze Kick" );
        public static readonly Move MudSport = new Move( 300, "Mud Sport" );
        public static readonly Move IceBall = new Move( 301, "Ice Ball" );
        public static readonly Move NeedleArm = new Move( 302, "Needle Arm" );
        public static readonly Move SlackOff = new Move( 303, "Slack Off" );
        public static readonly Move HyperVoice = new Move( 304, "Hyper Voice" );
        public static readonly Move PoisonFang = new Move( 305, "Poison Fang" );
        public static readonly Move CrushClaw = new Move( 306, "Crush Claw" );
        public static readonly Move BlastBurn = new Move( 307, "Blast Burn" );
        public static readonly Move HydroCannon = new Move( 308, "Hydro Cannon" );
        public static readonly Move MeteorMash = new Move( 309, "Meteor Mash" );
        public static readonly Move Astonish = new Move( 310, "Astonish" );
        public static readonly Move WeatherBall = new Move( 311, "Weather Ball" );
        public static readonly Move Aromatherapy = new Move( 312, "Aromatherapy" );
        public static readonly Move FakeTears = new Move( 313, "Fake Tears" );
        public static readonly Move AirCutter = new Move( 314, "Air Cutter" );
        public static readonly Move Overheat = new Move( 315, "Overheat" );
        public static readonly Move OdorSleuth = new Move( 316, "Odor Sleuth" );
        public static readonly Move RockTomb = new Move( 317, "Rock Tomb" );
        public static readonly Move SilverWind = new Move( 318, "Silver Wind" );
        public static readonly Move MetalSound = new Move( 319, "Metal Sound" );
        public static readonly Move GrassWhistle = new Move( 320, "Grass Whistle" );
        public static readonly Move Tickle = new Move( 321, "Tickle" );
        public static readonly Move CosmicPower = new Move( 322, "Cosmic Power" );
        public static readonly Move WaterSpout = new Move( 323, "Water Spout" );
        public static readonly Move SignalBeam = new Move( 324, "Signal Beam" );
        public static readonly Move ShadowPunch = new Move( 325, "Shadow Punch" );
        public static readonly Move Extrasensory = new Move( 326, "Extrasensory" );
        public static readonly Move SkyUppercut = new Move( 327, "Sky Uppercut" );
        public static readonly Move SandTomb = new Move( 328, "Sand Tomb" );
        public static readonly Move SheerCold = new Move( 329, "Sheer Cold" );
        public static readonly Move MuddyWater = new Move( 330, "Muddy Water" );
        public static readonly Move BulletSeed = new Move( 331, "Bullet Seed" );
        public static readonly Move AerialAce = new Move( 332, "Aerial Ace" );
        public static readonly Move IcicleSpear = new Move( 333, "Icicle Spear" );
        public static readonly Move IronDefense = new Move( 334, "Iron Defense" );
        public static readonly Move Block = new Move( 335, "Block" );
        public static readonly Move Howl = new Move( 336, "Howl" );
        public static readonly Move DragonClaw = new Move( 337, "Dragon Claw" );
        public static readonly Move FrenzyPlant = new Move( 338, "Frenzy Plant" );
        public static readonly Move BulkUp = new Move( 339, "Bulk Up" );
        public static readonly Move Bounce = new Move( 340, "Bounce" );
        public static readonly Move MudShot = new Move( 341, "Mud Shot" );
        public static readonly Move PoisonTail = new Move( 342, "Poison Tail" );
        public static readonly Move Covet = new Move( 343, "Covet" );
        public static readonly Move VoltTackle = new Move( 344, "Volt Tackle" );
        public static readonly Move MagicalLeaf = new Move( 345, "Magical Leaf" );
        public static readonly Move WaterSport = new Move( 346, "Water Sport" );
        public static readonly Move CalmMind = new Move( 347, "Calm Mind" );
        public static readonly Move LeafBlade = new Move( 348, "Leaf Blade" );
        public static readonly Move DragonDance = new Move( 349, "Dragon Dance" );
        public static readonly Move RockBlast = new Move( 350, "Rock Blast" );
        public static readonly Move ShockWave = new Move( 351, "Shock Wave" );
        public static readonly Move WaterPulse = new Move( 352, "Water Pulse" );
        public static readonly Move DoomDesire = new Move( 353, "Doom Desire" );
        public static readonly Move PsychoBoost = new Move( 354, "Psycho Boost" );
        public static readonly Move Roost = new Move( 355, "Roost" );
        public static readonly Move Gravity = new Move( 356, "Gravity" );
        public static readonly Move MiracleEye = new Move( 357, "Miracle Eye" );
        public static readonly Move WakeUpSlap = new Move( 358, "Wake-Up Slap" );
        public static readonly Move HammerArm = new Move( 359, "Hammer Arm" );
        public static readonly Move GyroBall = new Move( 360, "Gyro Ball" );
        public static readonly Move HealingWish = new Move( 361, "Healing Wish" );
        public static readonly Move Brine = new Move( 362, "Brine" );
        public static readonly Move NaturalGift = new Move( 363, "Natural Gift" );
        public static readonly Move Feint = new Move( 364, "Feint" );
        public static readonly Move Pluck = new Move( 365, "Pluck" );
        public static readonly Move Tailwind = new Move( 366, "Tailwind" );
        public static readonly Move Acupressure = new Move( 367, "Acupressure" );
        public static readonly Move MetalBurst = new Move( 368, "Metal Burst" );
        public static readonly Move Uturn = new Move( 369, "U-turn" );
        public static readonly Move CloseCombat = new Move( 370, "Close Combat" );
        public static readonly Move Payback = new Move( 371, "Payback" );
        public static readonly Move Assurance = new Move( 372, "Assurance" );
        public static readonly Move Embargo = new Move( 373, "Embargo" );
        public static readonly Move Fling = new Move( 374, "Fling" );
        public static readonly Move PsychoShift = new Move( 375, "Psycho Shift" );
        public static readonly Move TrumpCard = new Move( 376, "Trump Card" );
        public static readonly Move HealBlock = new Move( 377, "Heal Block" );
        public static readonly Move WringOut = new Move( 378, "Wring Out" );
        public static readonly Move PowerTrick = new Move( 379, "Power Trick" );
        public static readonly Move GastroAcid = new Move( 380, "Gastro Acid" );
        public static readonly Move LuckyChant = new Move( 381, "Lucky Chant" );
        public static readonly Move MeFirst = new Move( 382, "Me First" );
        public static readonly Move Copycat = new Move( 383, "Copycat" );
        public static readonly Move PowerSwap = new Move( 384, "Power Swap" );
        public static readonly Move GuardSwap = new Move( 385, "Guard Swap" );
        public static readonly Move Punishment = new Move( 386, "Punishment" );
        public static readonly Move LastResort = new Move( 387, "Last Resort" );
        public static readonly Move WorrySeed = new Move( 388, "Worry Seed" );
        public static readonly Move SuckerPunch = new Move( 389, "Sucker Punch" );
        public static readonly Move ToxicSpikes = new Move( 390, "Toxic Spikes" );
        public static readonly Move HeartSwap = new Move( 391, "Heart Swap" );
        public static readonly Move AquaRing = new Move( 392, "Aqua Ring" );
        public static readonly Move MagnetRise = new Move( 393, "Magnet Rise" );
        public static readonly Move FlareBlitz = new Move( 394, "Flare Blitz" );
        public static readonly Move ForcePalm = new Move( 395, "Force Palm" );
        public static readonly Move AuraSphere = new Move( 396, "Aura Sphere" );
        public static readonly Move RockPolish = new Move( 397, "Rock Polish" );
        public static readonly Move PoisonJab = new Move( 398, "Poison Jab" );
        public static readonly Move DarkPulse = new Move( 399, "Dark Pulse" );
        public static readonly Move NightSlash = new Move( 400, "Night Slash" );
        public static readonly Move AquaTail = new Move( 401, "Aqua Tail" );
        public static readonly Move SeedBomb = new Move( 402, "Seed Bomb" );
        public static readonly Move AirSlash = new Move( 403, "Air Slash" );
        public static readonly Move XScissor = new Move( 404, "X-Scissor" );
        public static readonly Move BugBuzz = new Move( 405, "Bug Buzz" );
        public static readonly Move DragonPulse = new Move( 406, "Dragon Pulse" );
        public static readonly Move DragonRush = new Move( 407, "Dragon Rush" );
        public static readonly Move PowerGem = new Move( 408, "Power Gem" );
        public static readonly Move DrainPunch = new Move( 409, "Drain Punch" );
        public static readonly Move VacuumWave = new Move( 410, "Vacuum Wave" );
        public static readonly Move FocusBlast = new Move( 411, "Focus Blast" );
        public static readonly Move EnergyBall = new Move( 412, "Energy Ball" );
        public static readonly Move BraveBird = new Move( 413, "Brave Bird" );
        public static readonly Move EarthPower = new Move( 414, "Earth Power" );
        public static readonly Move Switcheroo = new Move( 415, "Switcheroo" );
        public static readonly Move GigaImpact = new Move( 416, "Giga Impact" );
        public static readonly Move NastyPlot = new Move( 417, "Nasty Plot" );
        public static readonly Move BulletPunch = new Move( 418, "Bullet Punch" );
        public static readonly Move Avalanche = new Move( 419, "Avalanche" );
        public static readonly Move IceShard = new Move( 420, "Ice Shard" );
        public static readonly Move ShadowClaw = new Move( 421, "Shadow Claw" );
        public static readonly Move ThunderFang = new Move( 422, "Thunder Fang" );
        public static readonly Move IceFang = new Move( 423, "Ice Fang" );
        public static readonly Move FireFang = new Move( 424, "Fire Fang" );
        public static readonly Move ShadowSneak = new Move( 425, "Shadow Sneak" );
        public static readonly Move MudBomb = new Move( 426, "Mud Bomb" );
        public static readonly Move PsychoCut = new Move( 427, "Psycho Cut" );
        public static readonly Move ZenHeadbutt = new Move( 428, "Zen Headbutt" );
        public static readonly Move MirrorShot = new Move( 429, "Mirror Shot" );
        public static readonly Move FlashCannon = new Move( 430, "Flash Cannon" );
        public static readonly Move RockClimb = new Move( 431, "Rock Climb" );
        public static readonly Move Defog = new Move( 432, "Defog" );
        public static readonly Move TrickRoom = new Move( 433, "Trick Room" );
        public static readonly Move DracoMeteor = new Move( 434, "Draco Meteor" );
        public static readonly Move Discharge = new Move( 435, "Discharge" );
        public static readonly Move LavaPlume = new Move( 436, "Lava Plume" );
        public static readonly Move LeafStorm = new Move( 437, "Leaf Storm" );
        public static readonly Move PowerWhip = new Move( 438, "Power Whip" );
        public static readonly Move RockWrecker = new Move( 439, "Rock Wrecker" );
        public static readonly Move CrossPoison = new Move( 440, "Cross Poison" );
        public static readonly Move GunkShot = new Move( 441, "Gunk Shot" );
        public static readonly Move IronHead = new Move( 442, "Iron Head" );
        public static readonly Move MagnetBomb = new Move( 443, "Magnet Bomb" );
        public static readonly Move StoneEdge = new Move( 444, "Stone Edge" );
        public static readonly Move Captivate = new Move( 445, "Captivate" );
        public static readonly Move StealthRock = new Move( 446, "Stealth Rock" );
        public static readonly Move GrassKnot = new Move( 447, "Grass Knot" );
        public static readonly Move Chatter = new Move( 448, "Chatter" );
        public static readonly Move Judgment = new Move( 449, "Judgment" );
        public static readonly Move BugBite = new Move( 450, "Bug Bite" );
        public static readonly Move ChargeBeam = new Move( 451, "Charge Beam" );
        public static readonly Move WoodHammer = new Move( 452, "Wood Hammer" );
        public static readonly Move AquaJet = new Move( 453, "Aqua Jet" );
        public static readonly Move AttackOrder = new Move( 454, "Attack Order" );
        public static readonly Move DefendOrder = new Move( 455, "Defend Order" );
        public static readonly Move HealOrder = new Move( 456, "Heal Order" );
        public static readonly Move HeadSmash = new Move( 457, "Head Smash" );
        public static readonly Move DoubleHit = new Move( 458, "Double Hit" );
        public static readonly Move RoarofTime = new Move( 459, "Roar of Time" );
        public static readonly Move SpacialRend = new Move( 460, "Spacial Rend" );
        public static readonly Move LunarDance = new Move( 461, "Lunar Dance" );
        public static readonly Move CrushGrip = new Move( 462, "Crush Grip" );
        public static readonly Move MagmaStorm = new Move( 463, "Magma Storm" );
        public static readonly Move DarkVoid = new Move( 464, "Dark Void" );
        public static readonly Move SeedFlare = new Move( 465, "Seed Flare" );
        public static readonly Move OminousWind = new Move( 466, "Ominous Wind" );
        public static readonly Move ShadowForce = new Move( 467, "Shadow Force" );
        public static readonly Move HoneClaws = new Move( 468, "Hone Claws" );
        public static readonly Move WideGuard = new Move( 469, "Wide Guard" );
        public static readonly Move GuardSplit = new Move( 470, "Guard Split" );
        public static readonly Move PowerSplit = new Move( 471, "Power Split" );
        public static readonly Move WonderRoom = new Move( 472, "Wonder Room" );
        public static readonly Move Psyshock = new Move( 473, "Psyshock" );
        public static readonly Move Venoshock = new Move( 474, "Venoshock" );
        public static readonly Move Autotomize = new Move( 475, "Autotomize" );
        public static readonly Move RagePowder = new Move( 476, "Rage Powder" );
        public static readonly Move Telekinesis = new Move( 477, "Telekinesis" );
        public static readonly Move MagicRoom = new Move( 478, "Magic Room" );
        public static readonly Move SmackDown = new Move( 479, "Smack Down" );
        public static readonly Move StormThrow = new Move( 480, "Storm Throw" );
        public static readonly Move FlameBurst = new Move( 481, "Flame Burst" );
        public static readonly Move SludgeWave = new Move( 482, "Sludge Wave" );
        public static readonly Move QuiverDance = new Move( 483, "Quiver Dance" );
        public static readonly Move HeavySlam = new Move( 484, "Heavy Slam" );
        public static readonly Move Synchronoise = new Move( 485, "Synchronoise" );
        public static readonly Move ElectroBall = new Move( 486, "Electro Ball" );
        public static readonly Move Soak = new Move( 487, "Soak" );
        public static readonly Move FlameCharge = new Move( 488, "Flame Charge" );
        public static readonly Move Coil = new Move( 489, "Coil" );
        public static readonly Move LowSweep = new Move( 490, "Low Sweep" );
        public static readonly Move AcidSpray = new Move( 491, "Acid Spray" );
        public static readonly Move FoulPlay = new Move( 492, "Foul Play" );
        public static readonly Move SimpleBeam = new Move( 493, "Simple Beam" );
        public static readonly Move Entrainment = new Move( 494, "Entrainment" );
        public static readonly Move AfterYou = new Move( 495, "After You" );
        public static readonly Move Round = new Move( 496, "Round" );
        public static readonly Move EchoedVoice = new Move( 497, "Echoed Voice" );
        public static readonly Move ChipAway = new Move( 498, "Chip Away" );
        public static readonly Move ClearSmog = new Move( 499, "Clear Smog" );
        public static readonly Move StoredPower = new Move( 500, "Stored Power" );
        public static readonly Move QuickGuard = new Move( 501, "Quick Guard" );
        public static readonly Move AllySwitch = new Move( 502, "Ally Switch" );
        public static readonly Move Scald = new Move( 503, "Scald" );
        public static readonly Move ShellSmash = new Move( 504, "Shell Smash" );
        public static readonly Move HealPulse = new Move( 505, "Heal Pulse" );
        public static readonly Move Hex = new Move( 506, "Hex" );
        public static readonly Move SkyDrop = new Move( 507, "Sky Drop" );
        public static readonly Move ShiftGear = new Move( 508, "Shift Gear" );
        public static readonly Move CircleThrow = new Move( 509, "Circle Throw" );
        public static readonly Move Incinerate = new Move( 510, "Incinerate" );
        public static readonly Move Quash = new Move( 511, "Quash" );
        public static readonly Move Acrobatics = new Move( 512, "Acrobatics" );
        public static readonly Move ReflectType = new Move( 513, "Reflect Type" );
        public static readonly Move Retaliate = new Move( 514, "Retaliate" );
        public static readonly Move FinalGambit = new Move( 515, "Final Gambit" );
        public static readonly Move Bestow = new Move( 516, "Bestow" );
        public static readonly Move Inferno = new Move( 517, "Inferno" );
        public static readonly Move WaterPledge = new Move( 518, "Water Pledge" );
        public static readonly Move FirePledge = new Move( 519, "Fire Pledge" );
        public static readonly Move GrassPledge = new Move( 520, "Grass Pledge" );
        public static readonly Move VoltSwitch = new Move( 521, "Volt Switch" );
        public static readonly Move StruggleBug = new Move( 522, "Struggle Bug" );
        public static readonly Move Bulldoze = new Move( 523, "Bulldoze" );
        public static readonly Move FrostBreath = new Move( 524, "Frost Breath" );
        public static readonly Move DragonTail = new Move( 525, "Dragon Tail" );
        public static readonly Move WorkUp = new Move( 526, "Work Up" );
        public static readonly Move Electroweb = new Move( 527, "Electroweb" );
        public static readonly Move WildCharge = new Move( 528, "Wild Charge" );
        public static readonly Move DrillRun = new Move( 529, "Drill Run" );
        public static readonly Move DualChop = new Move( 530, "Dual Chop" );
        public static readonly Move HeartStamp = new Move( 531, "Heart Stamp" );
        public static readonly Move HornLeech = new Move( 532, "Horn Leech" );
        public static readonly Move SacredSword = new Move( 533, "Sacred Sword" );
        public static readonly Move RazorShell = new Move( 534, "Razor Shell" );
        public static readonly Move HeatCrash = new Move( 535, "Heat Crash" );
        public static readonly Move LeafTornado = new Move( 536, "Leaf Tornado" );
        public static readonly Move Steamroller = new Move( 537, "Steamroller" );
        public static readonly Move CottonGuard = new Move( 538, "Cotton Guard" );
        public static readonly Move NightDaze = new Move( 539, "Night Daze" );
        public static readonly Move Psystrike = new Move( 540, "Psystrike" );
        public static readonly Move TailSlap = new Move( 541, "Tail Slap" );
        public static readonly Move Hurricane = new Move( 542, "Hurricane" );
        public static readonly Move HeadCharge = new Move( 543, "Head Charge" );
        public static readonly Move GearGrind = new Move( 544, "Gear Grind" );
        public static readonly Move SearingShot = new Move( 545, "Searing Shot" );
        public static readonly Move TechnoBlast = new Move( 546, "Techno Blast" );
        public static readonly Move RelicSong = new Move( 547, "Relic Song" );
        public static readonly Move SecretSword = new Move( 548, "Secret Sword" );
        public static readonly Move Glaciate = new Move( 549, "Glaciate" );
        public static readonly Move BoltStrike = new Move( 550, "Bolt Strike" );
        public static readonly Move BlueFlare = new Move( 551, "Blue Flare" );
        public static readonly Move FieryDance = new Move( 552, "Fiery Dance" );
        public static readonly Move FreezeShock = new Move( 553, "Freeze Shock" );
        public static readonly Move IceBurn = new Move( 554, "Ice Burn" );
        public static readonly Move Snarl = new Move( 555, "Snarl" );
        public static readonly Move IcicleCrash = new Move( 556, "Icicle Crash" );
        public static readonly Move Vcreate = new Move( 557, "V-create" );
        public static readonly Move FusionFlare = new Move( 558, "Fusion Flare" );
        public static readonly Move FusionBolt = new Move( 559, "Fusion Bolt" );
        public static readonly Move FlyingPress = new Move( 560, "Flying Press" );
        public static readonly Move MatBlock = new Move( 561, "Mat Block" );
        public static readonly Move Belch = new Move( 562, "Belch" );
        public static readonly Move Rototiller = new Move( 563, "Rototiller" );
        public static readonly Move StickyWeb = new Move( 564, "Sticky Web" );
        public static readonly Move FellStinger = new Move( 565, "Fell Stinger" );
        public static readonly Move PhantomForce = new Move( 566, "Phantom Force" );
        public static readonly Move TrickorTreat = new Move( 567, "Trick-or-Treat" );
        public static readonly Move NobleRoar = new Move( 568, "Noble Roar" );
        public static readonly Move IonDeluge = new Move( 569, "Ion Deluge" );
        public static readonly Move ParabolicCharge = new Move( 570, "Parabolic Charge" );
        public static readonly Move ForestsCurse = new Move( 571, "Forest’s Curse" );
        public static readonly Move PetalBlizzard = new Move( 572, "Petal Blizzard" );
        public static readonly Move FreezeDry = new Move( 573, "Freeze-Dry" );
        public static readonly Move DisarmingVoice = new Move( 574, "Disarming Voice" );
        public static readonly Move PartingShot = new Move( 575, "Parting Shot" );
        public static readonly Move TopsyTurvy = new Move( 576, "Topsy-Turvy" );
        public static readonly Move DrainingKiss = new Move( 577, "Draining Kiss" );
        public static readonly Move CraftyShield = new Move( 578, "Crafty Shield" );
        public static readonly Move FlowerShield = new Move( 579, "Flower Shield" );
        public static readonly Move GrassyTerrain = new Move( 580, "Grassy Terrain" );
        public static readonly Move MistyTerrain = new Move( 581, "Misty Terrain" );
        public static readonly Move Electrify = new Move( 582, "Electrify" );
        public static readonly Move PlayRough = new Move( 583, "Play Rough" );
        public static readonly Move FairyWind = new Move( 584, "Fairy Wind" );
        public static readonly Move Moonblast = new Move( 585, "Moonblast" );
        public static readonly Move Boomburst = new Move( 586, "Boomburst" );
        public static readonly Move FairyLock = new Move( 587, "Fairy Lock" );
        public static readonly Move KingsShield = new Move( 588, "King’s Shield" );
        public static readonly Move PlayNice = new Move( 589, "Play Nice" );
        public static readonly Move Confide = new Move( 590, "Confide" );
        public static readonly Move DiamondStorm = new Move( 591, "Diamond Storm" );
        public static readonly Move SteamEruption = new Move( 592, "Steam Eruption" );
        public static readonly Move HyperspaceHole = new Move( 593, "Hyperspace Hole" );
        public static readonly Move WaterShuriken = new Move( 594, "Water Shuriken" );
        public static readonly Move MysticalFire = new Move( 595, "Mystical Fire" );
        public static readonly Move SpikyShield = new Move( 596, "Spiky Shield" );
        public static readonly Move AromaticMist = new Move( 597, "Aromatic Mist" );
        public static readonly Move EerieImpulse = new Move( 598, "Eerie Impulse" );
        public static readonly Move VenomDrench = new Move( 599, "Venom Drench" );
        public static readonly Move Powder = new Move( 600, "Powder" );
        public static readonly Move Geomancy = new Move( 601, "Geomancy" );
        public static readonly Move MagneticFlux = new Move( 602, "Magnetic Flux" );
        public static readonly Move HappyHour = new Move( 603, "Happy Hour" );
        public static readonly Move ElectricTerrain = new Move( 604, "Electric Terrain" );
        public static readonly Move DazzlingGleam = new Move( 605, "Dazzling Gleam" );
        public static readonly Move Celebrate = new Move( 606, "Celebrate" );
        public static readonly Move HoldHands = new Move( 607, "Hold Hands" );
        public static readonly Move BabyDollEyes = new Move( 608, "Baby-Doll Eyes" );
        public static readonly Move Nuzzle = new Move( 609, "Nuzzle" );
        public static readonly Move HoldBack = new Move( 610, "Hold Back" );
        public static readonly Move Infestation = new Move( 611, "Infestation" );
        public static readonly Move PowerUpPunch = new Move( 612, "Power-Up Punch" );
        public static readonly Move OblivionWing = new Move( 613, "Oblivion Wing" );
        public static readonly Move ThousandArrows = new Move( 614, "Thousand Arrows" );
        public static readonly Move ThousandWaves = new Move( 615, "Thousand Waves" );
        public static readonly Move LandsWrath = new Move( 616, "Land’s Wrath" );
        public static readonly Move LightofRuin = new Move( 617, "Light of Ruin" );
        public static readonly Move OriginPulse = new Move( 618, "Origin Pulse" );
        public static readonly Move PrecipiceBlades = new Move( 619, "Precipice Blades" );
        public static readonly Move DragonAscent = new Move( 620, "Dragon Ascent" );
        public static readonly Move HyperspaceFury = new Move( 621, "Hyperspace Fury" );
        
        private static readonly Move[] staticValues = {
            Pound,
            KarateChop,
            DoubleSlap,
            CometPunch,
            MegaPunch,
            PayDay,
            FirePunch,
            IcePunch,
            ThunderPunch,
            Scratch,
            ViceGrip,
            Guillotine,
            RazorWind,
            SwordsDance,
            Cut,
            Gust,
            WingAttack,
            Whirlwind,
            Fly,
            Bind,
            Slam,
            VineWhip,
            Stomp,
            DoubleKick,
            MegaKick,
            JumpKick,
            RollingKick,
            SandAttack,
            Headbutt,
            HornAttack,
            FuryAttack,
            HornDrill,
            Tackle,
            BodySlam,
            Wrap,
            TakeDown,
            Thrash,
            DoubleEdge,
            TailWhip,
            PoisonSting,
            Twineedle,
            PinMissile,
            Leer,
            Bite,
            Growl,
            Roar,
            Sing,
            Supersonic,
            SonicBoom,
            Disable,
            Acid,
            Ember,
            Flamethrower,
            Mist,
            WaterGun,
            HydroPump,
            Surf,
            IceBeam,
            Blizzard,
            Psybeam,
            BubbleBeam,
            AuroraBeam,
            HyperBeam,
            Peck,
            DrillPeck,
            Submission,
            LowKick,
            Counter,
            SeismicToss,
            Strength,
            Absorb,
            MegaDrain,
            LeechSeed,
            Growth,
            RazorLeaf,
            SolarBeam,
            PoisonPowder,
            StunSpore,
            SleepPowder,
            PetalDance,
            StringShot,
            DragonRage,
            FireSpin,
            ThunderShock,
            Thunderbolt,
            ThunderWave,
            Thunder,
            RockThrow,
            Earthquake,
            Fissure,
            Dig,
            Toxic,
            Confusion,
            Psychic,
            Hypnosis,
            Meditate,
            Agility,
            QuickAttack,
            Rage,
            Teleport,
            NightShade,
            Mimic,
            Screech,
            DoubleTeam,
            Recover,
            Harden,
            Minimize,
            Smokescreen,
            ConfuseRay,
            Withdraw,
            DefenseCurl,
            Barrier,
            LightScreen,
            Haze,
            Reflect,
            FocusEnergy,
            Bide,
            Metronome,
            MirrorMove,
            SelfDestruct,
            EggBomb,
            Lick,
            Smog,
            Sludge,
            BoneClub,
            FireBlast,
            Waterfall,
            Clamp,
            Swift,
            SkullBash,
            SpikeCannon,
            Constrict,
            Amnesia,
            Kinesis,
            SoftBoiled,
            HighJumpKick,
            Glare,
            DreamEater,
            PoisonGas,
            Barrage,
            LeechLife,
            LovelyKiss,
            SkyAttack,
            Transform,
            Bubble,
            DizzyPunch,
            Spore,
            Flash,
            Psywave,
            Splash,
            AcidArmor,
            Crabhammer,
            Explosion,
            FurySwipes,
            Bonemerang,
            Rest,
            RockSlide,
            HyperFang,
            Sharpen,
            Conversion,
            TriAttack,
            SuperFang,
            Slash,
            Substitute,
            Struggle,
            Sketch,
            TripleKick,
            Thief,
            SpiderWeb,
            MindReader,
            Nightmare,
            FlameWheel,
            Snore,
            Curse,
            Flail,
            Conversion2,
            Aeroblast,
            CottonSpore,
            Reversal,
            Spite,
            PowderSnow,
            Protect,
            MachPunch,
            ScaryFace,
            FeintAttack,
            SweetKiss,
            BellyDrum,
            SludgeBomb,
            MudSlap,
            Octazooka,
            Spikes,
            ZapCannon,
            Foresight,
            DestinyBond,
            PerishSong,
            IcyWind,
            Detect,
            BoneRush,
            LockOn,
            Outrage,
            Sandstorm,
            GigaDrain,
            Endure,
            Charm,
            Rollout,
            FalseSwipe,
            Swagger,
            MilkDrink,
            Spark,
            FuryCutter,
            SteelWing,
            MeanLook,
            Attract,
            SleepTalk,
            HealBell,
            Return,
            Present,
            Frustration,
            Safeguard,
            PainSplit,
            SacredFire,
            Magnitude,
            DynamicPunch,
            Megahorn,
            DragonBreath,
            BatonPass,
            Encore,
            Pursuit,
            RapidSpin,
            SweetScent,
            IronTail,
            MetalClaw,
            VitalThrow,
            MorningSun,
            Synthesis,
            Moonlight,
            HiddenPower,
            CrossChop,
            Twister,
            RainDance,
            SunnyDay,
            Crunch,
            MirrorCoat,
            PsychUp,
            ExtremeSpeed,
            AncientPower,
            ShadowBall,
            FutureSight,
            RockSmash,
            Whirlpool,
            BeatUp,
            FakeOut,
            Uproar,
            Stockpile,
            SpitUp,
            Swallow,
            HeatWave,
            Hail,
            Torment,
            Flatter,
            WillOWisp,
            Memento,
            Facade,
            FocusPunch,
            SmellingSalts,
            FollowMe,
            NaturePower,
            Charge,
            Taunt,
            HelpingHand,
            Trick,
            RolePlay,
            Wish,
            Assist,
            Ingrain,
            Superpower,
            MagicCoat,
            Recycle,
            Revenge,
            BrickBreak,
            Yawn,
            KnockOff,
            Endeavor,
            Eruption,
            SkillSwap,
            Imprison,
            Refresh,
            Grudge,
            Snatch,
            SecretPower,
            Dive,
            ArmThrust,
            Camouflage,
            TailGlow,
            LusterPurge,
            MistBall,
            FeatherDance,
            TeeterDance,
            BlazeKick,
            MudSport,
            IceBall,
            NeedleArm,
            SlackOff,
            HyperVoice,
            PoisonFang,
            CrushClaw,
            BlastBurn,
            HydroCannon,
            MeteorMash,
            Astonish,
            WeatherBall,
            Aromatherapy,
            FakeTears,
            AirCutter,
            Overheat,
            OdorSleuth,
            RockTomb,
            SilverWind,
            MetalSound,
            GrassWhistle,
            Tickle,
            CosmicPower,
            WaterSpout,
            SignalBeam,
            ShadowPunch,
            Extrasensory,
            SkyUppercut,
            SandTomb,
            SheerCold,
            MuddyWater,
            BulletSeed,
            AerialAce,
            IcicleSpear,
            IronDefense,
            Block,
            Howl,
            DragonClaw,
            FrenzyPlant,
            BulkUp,
            Bounce,
            MudShot,
            PoisonTail,
            Covet,
            VoltTackle,
            MagicalLeaf,
            WaterSport,
            CalmMind,
            LeafBlade,
            DragonDance,
            RockBlast,
            ShockWave,
            WaterPulse,
            DoomDesire,
            PsychoBoost,
            Roost,
            Gravity,
            MiracleEye,
            WakeUpSlap,
            HammerArm,
            GyroBall,
            HealingWish,
            Brine,
            NaturalGift,
            Feint,
            Pluck,
            Tailwind,
            Acupressure,
            MetalBurst,
            Uturn,
            CloseCombat,
            Payback,
            Assurance,
            Embargo,
            Fling,
            PsychoShift,
            TrumpCard,
            HealBlock,
            WringOut,
            PowerTrick,
            GastroAcid,
            LuckyChant,
            MeFirst,
            Copycat,
            PowerSwap,
            GuardSwap,
            Punishment,
            LastResort,
            WorrySeed,
            SuckerPunch,
            ToxicSpikes,
            HeartSwap,
            AquaRing,
            MagnetRise,
            FlareBlitz,
            ForcePalm,
            AuraSphere,
            RockPolish,
            PoisonJab,
            DarkPulse,
            NightSlash,
            AquaTail,
            SeedBomb,
            AirSlash,
            XScissor,
            BugBuzz,
            DragonPulse,
            DragonRush,
            PowerGem,
            DrainPunch,
            VacuumWave,
            FocusBlast,
            EnergyBall,
            BraveBird,
            EarthPower,
            Switcheroo,
            GigaImpact,
            NastyPlot,
            BulletPunch,
            Avalanche,
            IceShard,
            ShadowClaw,
            ThunderFang,
            IceFang,
            FireFang,
            ShadowSneak,
            MudBomb,
            PsychoCut,
            ZenHeadbutt,
            MirrorShot,
            FlashCannon,
            RockClimb,
            Defog,
            TrickRoom,
            DracoMeteor,
            Discharge,
            LavaPlume,
            LeafStorm,
            PowerWhip,
            RockWrecker,
            CrossPoison,
            GunkShot,
            IronHead,
            MagnetBomb,
            StoneEdge,
            Captivate,
            StealthRock,
            GrassKnot,
            Chatter,
            Judgment,
            BugBite,
            ChargeBeam,
            WoodHammer,
            AquaJet,
            AttackOrder,
            DefendOrder,
            HealOrder,
            HeadSmash,
            DoubleHit,
            RoarofTime,
            SpacialRend,
            LunarDance,
            CrushGrip,
            MagmaStorm,
            DarkVoid,
            SeedFlare,
            OminousWind,
            ShadowForce,
            HoneClaws,
            WideGuard,
            GuardSplit,
            PowerSplit,
            WonderRoom,
            Psyshock,
            Venoshock,
            Autotomize,
            RagePowder,
            Telekinesis,
            MagicRoom,
            SmackDown,
            StormThrow,
            FlameBurst,
            SludgeWave,
            QuiverDance,
            HeavySlam,
            Synchronoise,
            ElectroBall,
            Soak,
            FlameCharge,
            Coil,
            LowSweep,
            AcidSpray,
            FoulPlay,
            SimpleBeam,
            Entrainment,
            AfterYou,
            Round,
            EchoedVoice,
            ChipAway,
            ClearSmog,
            StoredPower,
            QuickGuard,
            AllySwitch,
            Scald,
            ShellSmash,
            HealPulse,
            Hex,
            SkyDrop,
            ShiftGear,
            CircleThrow,
            Incinerate,
            Quash,
            Acrobatics,
            ReflectType,
            Retaliate,
            FinalGambit,
            Bestow,
            Inferno,
            WaterPledge,
            FirePledge,
            GrassPledge,
            VoltSwitch,
            StruggleBug,
            Bulldoze,
            FrostBreath,
            DragonTail,
            WorkUp,
            Electroweb,
            WildCharge,
            DrillRun,
            DualChop,
            HeartStamp,
            HornLeech,
            SacredSword,
            RazorShell,
            HeatCrash,
            LeafTornado,
            Steamroller,
            CottonGuard,
            NightDaze,
            Psystrike,
            TailSlap,
            Hurricane,
            HeadCharge,
            GearGrind,
            SearingShot,
            TechnoBlast,
            RelicSong,
            SecretSword,
            Glaciate,
            BoltStrike,
            BlueFlare,
            FieryDance,
            FreezeShock,
            IceBurn,
            Snarl,
            IcicleCrash,
            Vcreate,
            FusionFlare,
            FusionBolt,
            FlyingPress,
            MatBlock,
            Belch,
            Rototiller,
            StickyWeb,
            FellStinger,
            PhantomForce,
            TrickorTreat,
            NobleRoar,
            IonDeluge,
            ParabolicCharge,
            ForestsCurse,
            PetalBlizzard,
            FreezeDry,
            DisarmingVoice,
            PartingShot,
            TopsyTurvy,
            DrainingKiss,
            CraftyShield,
            FlowerShield,
            GrassyTerrain,
            MistyTerrain,
            Electrify,
            PlayRough,
            FairyWind,
            Moonblast,
            Boomburst,
            FairyLock,
            KingsShield,
            PlayNice,
            Confide,
            DiamondStorm,
            SteamEruption,
            HyperspaceHole,
            WaterShuriken,
            MysticalFire,
            SpikyShield,
            AromaticMist,
            EerieImpulse,
            VenomDrench,
            Powder,
            Geomancy,
            MagneticFlux,
            HappyHour,
            ElectricTerrain,
            DazzlingGleam,
            Celebrate,
            HoldHands,
            BabyDollEyes,
            Nuzzle,
            HoldBack,
            Infestation,
            PowerUpPunch,
            OblivionWing,
            ThousandArrows,
            ThousandWaves,
            LandsWrath,
            LightofRuin,
            OriginPulse,
            PrecipiceBlades,
            DragonAscent,
            HyperspaceFury,
        };
        
        public static Move GetValueFrom( int id ) => staticValues.SingleOrDefault( v => v.Id == id );
        public static IEnumerable<Move> AllMoves => staticValues.AsEnumerable();
    }
    
    public sealed class PokemonType : BasePokemonType {
        public PokemonType( int id, string name ) : base( id, name ) { }
        
        public static explicit operator PokemonType( int id ) => PokemonTypes.GetValueFrom( id );
        public static explicit operator int( PokemonType val ) => val.Id;
    }
    
    public static partial class PokemonTypes {
        public static readonly PokemonType Normal = new PokemonType( 0, "Normal" );
        public static readonly PokemonType Fighting = new PokemonType( 1, "Fighting" );
        public static readonly PokemonType Flying = new PokemonType( 2, "Flying" );
        public static readonly PokemonType Poison = new PokemonType( 3, "Poison" );
        public static readonly PokemonType Ground = new PokemonType( 4, "Ground" );
        public static readonly PokemonType Rock = new PokemonType( 5, "Rock" );
        public static readonly PokemonType Bug = new PokemonType( 6, "Bug" );
        public static readonly PokemonType Ghost = new PokemonType( 7, "Ghost" );
        public static readonly PokemonType Steel = new PokemonType( 8, "Steel" );
        public static readonly PokemonType Fire = new PokemonType( 9, "Fire" );
        public static readonly PokemonType Water = new PokemonType( 10, "Water" );
        public static readonly PokemonType Grass = new PokemonType( 11, "Grass" );
        public static readonly PokemonType Electric = new PokemonType( 12, "Electric" );
        public static readonly PokemonType Psychic = new PokemonType( 13, "Psychic" );
        public static readonly PokemonType Ice = new PokemonType( 14, "Ice" );
        public static readonly PokemonType Dragon = new PokemonType( 15, "Dragon" );
        public static readonly PokemonType Dark = new PokemonType( 16, "Dark" );
        public static readonly PokemonType Fairy = new PokemonType( 17, "Fairy" );
        
        private static readonly PokemonType[] staticValues = {
            Normal,
            Fighting,
            Flying,
            Poison,
            Ground,
            Rock,
            Bug,
            Ghost,
            Steel,
            Fire,
            Water,
            Grass,
            Electric,
            Psychic,
            Ice,
            Dragon,
            Dark,
            Fairy,
        };
        
        public static PokemonType GetValueFrom( int id ) => staticValues.SingleOrDefault( v => v.Id == id );
        public static IEnumerable<PokemonType> AllPokemonTypes => staticValues.AsEnumerable();
    }
    
    public sealed class SpeciesType : BaseSpeciesType {
        public SpeciesType( int id, string name ) : base( id, name ) { }
        
        public static explicit operator SpeciesType( int id ) => Species.GetValueFrom( id );
        public static explicit operator int( SpeciesType val ) => val.Id;
    }
    
    public static partial class Species {
        public static readonly SpeciesType Egg = new SpeciesType( 0, "Egg" );
        public static readonly SpeciesType Bulbasaur = new SpeciesType( 1, "Bulbasaur" );
        public static readonly SpeciesType Ivysaur = new SpeciesType( 2, "Ivysaur" );
        public static readonly SpeciesType Venusaur = new SpeciesType( 3, "Venusaur" );
        public static readonly SpeciesType Charmander = new SpeciesType( 4, "Charmander" );
        public static readonly SpeciesType Charmeleon = new SpeciesType( 5, "Charmeleon" );
        public static readonly SpeciesType Charizard = new SpeciesType( 6, "Charizard" );
        public static readonly SpeciesType Squirtle = new SpeciesType( 7, "Squirtle" );
        public static readonly SpeciesType Wartortle = new SpeciesType( 8, "Wartortle" );
        public static readonly SpeciesType Blastoise = new SpeciesType( 9, "Blastoise" );
        public static readonly SpeciesType Caterpie = new SpeciesType( 10, "Caterpie" );
        public static readonly SpeciesType Metapod = new SpeciesType( 11, "Metapod" );
        public static readonly SpeciesType Butterfree = new SpeciesType( 12, "Butterfree" );
        public static readonly SpeciesType Weedle = new SpeciesType( 13, "Weedle" );
        public static readonly SpeciesType Kakuna = new SpeciesType( 14, "Kakuna" );
        public static readonly SpeciesType Beedrill = new SpeciesType( 15, "Beedrill" );
        public static readonly SpeciesType Pidgey = new SpeciesType( 16, "Pidgey" );
        public static readonly SpeciesType Pidgeotto = new SpeciesType( 17, "Pidgeotto" );
        public static readonly SpeciesType Pidgeot = new SpeciesType( 18, "Pidgeot" );
        public static readonly SpeciesType Rattata = new SpeciesType( 19, "Rattata" );
        public static readonly SpeciesType Raticate = new SpeciesType( 20, "Raticate" );
        public static readonly SpeciesType Spearow = new SpeciesType( 21, "Spearow" );
        public static readonly SpeciesType Fearow = new SpeciesType( 22, "Fearow" );
        public static readonly SpeciesType Ekans = new SpeciesType( 23, "Ekans" );
        public static readonly SpeciesType Arbok = new SpeciesType( 24, "Arbok" );
        public static readonly SpeciesType Pikachu = new SpeciesType( 25, "Pikachu" );
        public static readonly SpeciesType Raichu = new SpeciesType( 26, "Raichu" );
        public static readonly SpeciesType Sandshrew = new SpeciesType( 27, "Sandshrew" );
        public static readonly SpeciesType Sandslash = new SpeciesType( 28, "Sandslash" );
        public static readonly SpeciesType NidoranF = new SpeciesType( 29, "Nidoran♀" );
        public static readonly SpeciesType Nidorina = new SpeciesType( 30, "Nidorina" );
        public static readonly SpeciesType Nidoqueen = new SpeciesType( 31, "Nidoqueen" );
        public static readonly SpeciesType NidoranM = new SpeciesType( 32, "Nidoran♂" );
        public static readonly SpeciesType Nidorino = new SpeciesType( 33, "Nidorino" );
        public static readonly SpeciesType Nidoking = new SpeciesType( 34, "Nidoking" );
        public static readonly SpeciesType Clefairy = new SpeciesType( 35, "Clefairy" );
        public static readonly SpeciesType Clefable = new SpeciesType( 36, "Clefable" );
        public static readonly SpeciesType Vulpix = new SpeciesType( 37, "Vulpix" );
        public static readonly SpeciesType Ninetales = new SpeciesType( 38, "Ninetales" );
        public static readonly SpeciesType Jigglypuff = new SpeciesType( 39, "Jigglypuff" );
        public static readonly SpeciesType Wigglytuff = new SpeciesType( 40, "Wigglytuff" );
        public static readonly SpeciesType Zubat = new SpeciesType( 41, "Zubat" );
        public static readonly SpeciesType Golbat = new SpeciesType( 42, "Golbat" );
        public static readonly SpeciesType Oddish = new SpeciesType( 43, "Oddish" );
        public static readonly SpeciesType Gloom = new SpeciesType( 44, "Gloom" );
        public static readonly SpeciesType Vileplume = new SpeciesType( 45, "Vileplume" );
        public static readonly SpeciesType Paras = new SpeciesType( 46, "Paras" );
        public static readonly SpeciesType Parasect = new SpeciesType( 47, "Parasect" );
        public static readonly SpeciesType Venonat = new SpeciesType( 48, "Venonat" );
        public static readonly SpeciesType Venomoth = new SpeciesType( 49, "Venomoth" );
        public static readonly SpeciesType Diglett = new SpeciesType( 50, "Diglett" );
        public static readonly SpeciesType Dugtrio = new SpeciesType( 51, "Dugtrio" );
        public static readonly SpeciesType Meowth = new SpeciesType( 52, "Meowth" );
        public static readonly SpeciesType Persian = new SpeciesType( 53, "Persian" );
        public static readonly SpeciesType Psyduck = new SpeciesType( 54, "Psyduck" );
        public static readonly SpeciesType Golduck = new SpeciesType( 55, "Golduck" );
        public static readonly SpeciesType Mankey = new SpeciesType( 56, "Mankey" );
        public static readonly SpeciesType Primeape = new SpeciesType( 57, "Primeape" );
        public static readonly SpeciesType Growlithe = new SpeciesType( 58, "Growlithe" );
        public static readonly SpeciesType Arcanine = new SpeciesType( 59, "Arcanine" );
        public static readonly SpeciesType Poliwag = new SpeciesType( 60, "Poliwag" );
        public static readonly SpeciesType Poliwhirl = new SpeciesType( 61, "Poliwhirl" );
        public static readonly SpeciesType Poliwrath = new SpeciesType( 62, "Poliwrath" );
        public static readonly SpeciesType Abra = new SpeciesType( 63, "Abra" );
        public static readonly SpeciesType Kadabra = new SpeciesType( 64, "Kadabra" );
        public static readonly SpeciesType Alakazam = new SpeciesType( 65, "Alakazam" );
        public static readonly SpeciesType Machop = new SpeciesType( 66, "Machop" );
        public static readonly SpeciesType Machoke = new SpeciesType( 67, "Machoke" );
        public static readonly SpeciesType Machamp = new SpeciesType( 68, "Machamp" );
        public static readonly SpeciesType Bellsprout = new SpeciesType( 69, "Bellsprout" );
        public static readonly SpeciesType Weepinbell = new SpeciesType( 70, "Weepinbell" );
        public static readonly SpeciesType Victreebel = new SpeciesType( 71, "Victreebel" );
        public static readonly SpeciesType Tentacool = new SpeciesType( 72, "Tentacool" );
        public static readonly SpeciesType Tentacruel = new SpeciesType( 73, "Tentacruel" );
        public static readonly SpeciesType Geodude = new SpeciesType( 74, "Geodude" );
        public static readonly SpeciesType Graveler = new SpeciesType( 75, "Graveler" );
        public static readonly SpeciesType Golem = new SpeciesType( 76, "Golem" );
        public static readonly SpeciesType Ponyta = new SpeciesType( 77, "Ponyta" );
        public static readonly SpeciesType Rapidash = new SpeciesType( 78, "Rapidash" );
        public static readonly SpeciesType Slowpoke = new SpeciesType( 79, "Slowpoke" );
        public static readonly SpeciesType Slowbro = new SpeciesType( 80, "Slowbro" );
        public static readonly SpeciesType Magnemite = new SpeciesType( 81, "Magnemite" );
        public static readonly SpeciesType Magneton = new SpeciesType( 82, "Magneton" );
        public static readonly SpeciesType Farfetchd = new SpeciesType( 83, "Farfetch’d" );
        public static readonly SpeciesType Doduo = new SpeciesType( 84, "Doduo" );
        public static readonly SpeciesType Dodrio = new SpeciesType( 85, "Dodrio" );
        public static readonly SpeciesType Seel = new SpeciesType( 86, "Seel" );
        public static readonly SpeciesType Dewgong = new SpeciesType( 87, "Dewgong" );
        public static readonly SpeciesType Grimer = new SpeciesType( 88, "Grimer" );
        public static readonly SpeciesType Muk = new SpeciesType( 89, "Muk" );
        public static readonly SpeciesType Shellder = new SpeciesType( 90, "Shellder" );
        public static readonly SpeciesType Cloyster = new SpeciesType( 91, "Cloyster" );
        public static readonly SpeciesType Gastly = new SpeciesType( 92, "Gastly" );
        public static readonly SpeciesType Haunter = new SpeciesType( 93, "Haunter" );
        public static readonly SpeciesType Gengar = new SpeciesType( 94, "Gengar" );
        public static readonly SpeciesType Onix = new SpeciesType( 95, "Onix" );
        public static readonly SpeciesType Drowzee = new SpeciesType( 96, "Drowzee" );
        public static readonly SpeciesType Hypno = new SpeciesType( 97, "Hypno" );
        public static readonly SpeciesType Krabby = new SpeciesType( 98, "Krabby" );
        public static readonly SpeciesType Kingler = new SpeciesType( 99, "Kingler" );
        public static readonly SpeciesType Voltorb = new SpeciesType( 100, "Voltorb" );
        public static readonly SpeciesType Electrode = new SpeciesType( 101, "Electrode" );
        public static readonly SpeciesType Exeggcute = new SpeciesType( 102, "Exeggcute" );
        public static readonly SpeciesType Exeggutor = new SpeciesType( 103, "Exeggutor" );
        public static readonly SpeciesType Cubone = new SpeciesType( 104, "Cubone" );
        public static readonly SpeciesType Marowak = new SpeciesType( 105, "Marowak" );
        public static readonly SpeciesType Hitmonlee = new SpeciesType( 106, "Hitmonlee" );
        public static readonly SpeciesType Hitmonchan = new SpeciesType( 107, "Hitmonchan" );
        public static readonly SpeciesType Lickitung = new SpeciesType( 108, "Lickitung" );
        public static readonly SpeciesType Koffing = new SpeciesType( 109, "Koffing" );
        public static readonly SpeciesType Weezing = new SpeciesType( 110, "Weezing" );
        public static readonly SpeciesType Rhyhorn = new SpeciesType( 111, "Rhyhorn" );
        public static readonly SpeciesType Rhydon = new SpeciesType( 112, "Rhydon" );
        public static readonly SpeciesType Chansey = new SpeciesType( 113, "Chansey" );
        public static readonly SpeciesType Tangela = new SpeciesType( 114, "Tangela" );
        public static readonly SpeciesType Kangaskhan = new SpeciesType( 115, "Kangaskhan" );
        public static readonly SpeciesType Horsea = new SpeciesType( 116, "Horsea" );
        public static readonly SpeciesType Seadra = new SpeciesType( 117, "Seadra" );
        public static readonly SpeciesType Goldeen = new SpeciesType( 118, "Goldeen" );
        public static readonly SpeciesType Seaking = new SpeciesType( 119, "Seaking" );
        public static readonly SpeciesType Staryu = new SpeciesType( 120, "Staryu" );
        public static readonly SpeciesType Starmie = new SpeciesType( 121, "Starmie" );
        public static readonly SpeciesType MrMime = new SpeciesType( 122, "Mr. Mime" );
        public static readonly SpeciesType Scyther = new SpeciesType( 123, "Scyther" );
        public static readonly SpeciesType Jynx = new SpeciesType( 124, "Jynx" );
        public static readonly SpeciesType Electabuzz = new SpeciesType( 125, "Electabuzz" );
        public static readonly SpeciesType Magmar = new SpeciesType( 126, "Magmar" );
        public static readonly SpeciesType Pinsir = new SpeciesType( 127, "Pinsir" );
        public static readonly SpeciesType Tauros = new SpeciesType( 128, "Tauros" );
        public static readonly SpeciesType Magikarp = new SpeciesType( 129, "Magikarp" );
        public static readonly SpeciesType Gyarados = new SpeciesType( 130, "Gyarados" );
        public static readonly SpeciesType Lapras = new SpeciesType( 131, "Lapras" );
        public static readonly SpeciesType Ditto = new SpeciesType( 132, "Ditto" );
        public static readonly SpeciesType Eevee = new SpeciesType( 133, "Eevee" );
        public static readonly SpeciesType Vaporeon = new SpeciesType( 134, "Vaporeon" );
        public static readonly SpeciesType Jolteon = new SpeciesType( 135, "Jolteon" );
        public static readonly SpeciesType Flareon = new SpeciesType( 136, "Flareon" );
        public static readonly SpeciesType Porygon = new SpeciesType( 137, "Porygon" );
        public static readonly SpeciesType Omanyte = new SpeciesType( 138, "Omanyte" );
        public static readonly SpeciesType Omastar = new SpeciesType( 139, "Omastar" );
        public static readonly SpeciesType Kabuto = new SpeciesType( 140, "Kabuto" );
        public static readonly SpeciesType Kabutops = new SpeciesType( 141, "Kabutops" );
        public static readonly SpeciesType Aerodactyl = new SpeciesType( 142, "Aerodactyl" );
        public static readonly SpeciesType Snorlax = new SpeciesType( 143, "Snorlax" );
        public static readonly SpeciesType Articuno = new SpeciesType( 144, "Articuno" );
        public static readonly SpeciesType Zapdos = new SpeciesType( 145, "Zapdos" );
        public static readonly SpeciesType Moltres = new SpeciesType( 146, "Moltres" );
        public static readonly SpeciesType Dratini = new SpeciesType( 147, "Dratini" );
        public static readonly SpeciesType Dragonair = new SpeciesType( 148, "Dragonair" );
        public static readonly SpeciesType Dragonite = new SpeciesType( 149, "Dragonite" );
        public static readonly SpeciesType Mewtwo = new SpeciesType( 150, "Mewtwo" );
        public static readonly SpeciesType Mew = new SpeciesType( 151, "Mew" );
        public static readonly SpeciesType Chikorita = new SpeciesType( 152, "Chikorita" );
        public static readonly SpeciesType Bayleef = new SpeciesType( 153, "Bayleef" );
        public static readonly SpeciesType Meganium = new SpeciesType( 154, "Meganium" );
        public static readonly SpeciesType Cyndaquil = new SpeciesType( 155, "Cyndaquil" );
        public static readonly SpeciesType Quilava = new SpeciesType( 156, "Quilava" );
        public static readonly SpeciesType Typhlosion = new SpeciesType( 157, "Typhlosion" );
        public static readonly SpeciesType Totodile = new SpeciesType( 158, "Totodile" );
        public static readonly SpeciesType Croconaw = new SpeciesType( 159, "Croconaw" );
        public static readonly SpeciesType Feraligatr = new SpeciesType( 160, "Feraligatr" );
        public static readonly SpeciesType Sentret = new SpeciesType( 161, "Sentret" );
        public static readonly SpeciesType Furret = new SpeciesType( 162, "Furret" );
        public static readonly SpeciesType Hoothoot = new SpeciesType( 163, "Hoothoot" );
        public static readonly SpeciesType Noctowl = new SpeciesType( 164, "Noctowl" );
        public static readonly SpeciesType Ledyba = new SpeciesType( 165, "Ledyba" );
        public static readonly SpeciesType Ledian = new SpeciesType( 166, "Ledian" );
        public static readonly SpeciesType Spinarak = new SpeciesType( 167, "Spinarak" );
        public static readonly SpeciesType Ariados = new SpeciesType( 168, "Ariados" );
        public static readonly SpeciesType Crobat = new SpeciesType( 169, "Crobat" );
        public static readonly SpeciesType Chinchou = new SpeciesType( 170, "Chinchou" );
        public static readonly SpeciesType Lanturn = new SpeciesType( 171, "Lanturn" );
        public static readonly SpeciesType Pichu = new SpeciesType( 172, "Pichu" );
        public static readonly SpeciesType Cleffa = new SpeciesType( 173, "Cleffa" );
        public static readonly SpeciesType Igglybuff = new SpeciesType( 174, "Igglybuff" );
        public static readonly SpeciesType Togepi = new SpeciesType( 175, "Togepi" );
        public static readonly SpeciesType Togetic = new SpeciesType( 176, "Togetic" );
        public static readonly SpeciesType Natu = new SpeciesType( 177, "Natu" );
        public static readonly SpeciesType Xatu = new SpeciesType( 178, "Xatu" );
        public static readonly SpeciesType Mareep = new SpeciesType( 179, "Mareep" );
        public static readonly SpeciesType Flaaffy = new SpeciesType( 180, "Flaaffy" );
        public static readonly SpeciesType Ampharos = new SpeciesType( 181, "Ampharos" );
        public static readonly SpeciesType Bellossom = new SpeciesType( 182, "Bellossom" );
        public static readonly SpeciesType Marill = new SpeciesType( 183, "Marill" );
        public static readonly SpeciesType Azumarill = new SpeciesType( 184, "Azumarill" );
        public static readonly SpeciesType Sudowoodo = new SpeciesType( 185, "Sudowoodo" );
        public static readonly SpeciesType Politoed = new SpeciesType( 186, "Politoed" );
        public static readonly SpeciesType Hoppip = new SpeciesType( 187, "Hoppip" );
        public static readonly SpeciesType Skiploom = new SpeciesType( 188, "Skiploom" );
        public static readonly SpeciesType Jumpluff = new SpeciesType( 189, "Jumpluff" );
        public static readonly SpeciesType Aipom = new SpeciesType( 190, "Aipom" );
        public static readonly SpeciesType Sunkern = new SpeciesType( 191, "Sunkern" );
        public static readonly SpeciesType Sunflora = new SpeciesType( 192, "Sunflora" );
        public static readonly SpeciesType Yanma = new SpeciesType( 193, "Yanma" );
        public static readonly SpeciesType Wooper = new SpeciesType( 194, "Wooper" );
        public static readonly SpeciesType Quagsire = new SpeciesType( 195, "Quagsire" );
        public static readonly SpeciesType Espeon = new SpeciesType( 196, "Espeon" );
        public static readonly SpeciesType Umbreon = new SpeciesType( 197, "Umbreon" );
        public static readonly SpeciesType Murkrow = new SpeciesType( 198, "Murkrow" );
        public static readonly SpeciesType Slowking = new SpeciesType( 199, "Slowking" );
        public static readonly SpeciesType Misdreavus = new SpeciesType( 200, "Misdreavus" );
        public static readonly SpeciesType Unown = new SpeciesType( 201, "Unown" );
        public static readonly SpeciesType Wobbuffet = new SpeciesType( 202, "Wobbuffet" );
        public static readonly SpeciesType Girafarig = new SpeciesType( 203, "Girafarig" );
        public static readonly SpeciesType Pineco = new SpeciesType( 204, "Pineco" );
        public static readonly SpeciesType Forretress = new SpeciesType( 205, "Forretress" );
        public static readonly SpeciesType Dunsparce = new SpeciesType( 206, "Dunsparce" );
        public static readonly SpeciesType Gligar = new SpeciesType( 207, "Gligar" );
        public static readonly SpeciesType Steelix = new SpeciesType( 208, "Steelix" );
        public static readonly SpeciesType Snubbull = new SpeciesType( 209, "Snubbull" );
        public static readonly SpeciesType Granbull = new SpeciesType( 210, "Granbull" );
        public static readonly SpeciesType Qwilfish = new SpeciesType( 211, "Qwilfish" );
        public static readonly SpeciesType Scizor = new SpeciesType( 212, "Scizor" );
        public static readonly SpeciesType Shuckle = new SpeciesType( 213, "Shuckle" );
        public static readonly SpeciesType Heracross = new SpeciesType( 214, "Heracross" );
        public static readonly SpeciesType Sneasel = new SpeciesType( 215, "Sneasel" );
        public static readonly SpeciesType Teddiursa = new SpeciesType( 216, "Teddiursa" );
        public static readonly SpeciesType Ursaring = new SpeciesType( 217, "Ursaring" );
        public static readonly SpeciesType Slugma = new SpeciesType( 218, "Slugma" );
        public static readonly SpeciesType Magcargo = new SpeciesType( 219, "Magcargo" );
        public static readonly SpeciesType Swinub = new SpeciesType( 220, "Swinub" );
        public static readonly SpeciesType Piloswine = new SpeciesType( 221, "Piloswine" );
        public static readonly SpeciesType Corsola = new SpeciesType( 222, "Corsola" );
        public static readonly SpeciesType Remoraid = new SpeciesType( 223, "Remoraid" );
        public static readonly SpeciesType Octillery = new SpeciesType( 224, "Octillery" );
        public static readonly SpeciesType Delibird = new SpeciesType( 225, "Delibird" );
        public static readonly SpeciesType Mantine = new SpeciesType( 226, "Mantine" );
        public static readonly SpeciesType Skarmory = new SpeciesType( 227, "Skarmory" );
        public static readonly SpeciesType Houndour = new SpeciesType( 228, "Houndour" );
        public static readonly SpeciesType Houndoom = new SpeciesType( 229, "Houndoom" );
        public static readonly SpeciesType Kingdra = new SpeciesType( 230, "Kingdra" );
        public static readonly SpeciesType Phanpy = new SpeciesType( 231, "Phanpy" );
        public static readonly SpeciesType Donphan = new SpeciesType( 232, "Donphan" );
        public static readonly SpeciesType Porygon2 = new SpeciesType( 233, "Porygon2" );
        public static readonly SpeciesType Stantler = new SpeciesType( 234, "Stantler" );
        public static readonly SpeciesType Smeargle = new SpeciesType( 235, "Smeargle" );
        public static readonly SpeciesType Tyrogue = new SpeciesType( 236, "Tyrogue" );
        public static readonly SpeciesType Hitmontop = new SpeciesType( 237, "Hitmontop" );
        public static readonly SpeciesType Smoochum = new SpeciesType( 238, "Smoochum" );
        public static readonly SpeciesType Elekid = new SpeciesType( 239, "Elekid" );
        public static readonly SpeciesType Magby = new SpeciesType( 240, "Magby" );
        public static readonly SpeciesType Miltank = new SpeciesType( 241, "Miltank" );
        public static readonly SpeciesType Blissey = new SpeciesType( 242, "Blissey" );
        public static readonly SpeciesType Raikou = new SpeciesType( 243, "Raikou" );
        public static readonly SpeciesType Entei = new SpeciesType( 244, "Entei" );
        public static readonly SpeciesType Suicune = new SpeciesType( 245, "Suicune" );
        public static readonly SpeciesType Larvitar = new SpeciesType( 246, "Larvitar" );
        public static readonly SpeciesType Pupitar = new SpeciesType( 247, "Pupitar" );
        public static readonly SpeciesType Tyranitar = new SpeciesType( 248, "Tyranitar" );
        public static readonly SpeciesType Lugia = new SpeciesType( 249, "Lugia" );
        public static readonly SpeciesType HoOh = new SpeciesType( 250, "Ho-Oh" );
        public static readonly SpeciesType Celebi = new SpeciesType( 251, "Celebi" );
        public static readonly SpeciesType Treecko = new SpeciesType( 252, "Treecko" );
        public static readonly SpeciesType Grovyle = new SpeciesType( 253, "Grovyle" );
        public static readonly SpeciesType Sceptile = new SpeciesType( 254, "Sceptile" );
        public static readonly SpeciesType Torchic = new SpeciesType( 255, "Torchic" );
        public static readonly SpeciesType Combusken = new SpeciesType( 256, "Combusken" );
        public static readonly SpeciesType Blaziken = new SpeciesType( 257, "Blaziken" );
        public static readonly SpeciesType Mudkip = new SpeciesType( 258, "Mudkip" );
        public static readonly SpeciesType Marshtomp = new SpeciesType( 259, "Marshtomp" );
        public static readonly SpeciesType Swampert = new SpeciesType( 260, "Swampert" );
        public static readonly SpeciesType Poochyena = new SpeciesType( 261, "Poochyena" );
        public static readonly SpeciesType Mightyena = new SpeciesType( 262, "Mightyena" );
        public static readonly SpeciesType Zigzagoon = new SpeciesType( 263, "Zigzagoon" );
        public static readonly SpeciesType Linoone = new SpeciesType( 264, "Linoone" );
        public static readonly SpeciesType Wurmple = new SpeciesType( 265, "Wurmple" );
        public static readonly SpeciesType Silcoon = new SpeciesType( 266, "Silcoon" );
        public static readonly SpeciesType Beautifly = new SpeciesType( 267, "Beautifly" );
        public static readonly SpeciesType Cascoon = new SpeciesType( 268, "Cascoon" );
        public static readonly SpeciesType Dustox = new SpeciesType( 269, "Dustox" );
        public static readonly SpeciesType Lotad = new SpeciesType( 270, "Lotad" );
        public static readonly SpeciesType Lombre = new SpeciesType( 271, "Lombre" );
        public static readonly SpeciesType Ludicolo = new SpeciesType( 272, "Ludicolo" );
        public static readonly SpeciesType Seedot = new SpeciesType( 273, "Seedot" );
        public static readonly SpeciesType Nuzleaf = new SpeciesType( 274, "Nuzleaf" );
        public static readonly SpeciesType Shiftry = new SpeciesType( 275, "Shiftry" );
        public static readonly SpeciesType Taillow = new SpeciesType( 276, "Taillow" );
        public static readonly SpeciesType Swellow = new SpeciesType( 277, "Swellow" );
        public static readonly SpeciesType Wingull = new SpeciesType( 278, "Wingull" );
        public static readonly SpeciesType Pelipper = new SpeciesType( 279, "Pelipper" );
        public static readonly SpeciesType Ralts = new SpeciesType( 280, "Ralts" );
        public static readonly SpeciesType Kirlia = new SpeciesType( 281, "Kirlia" );
        public static readonly SpeciesType Gardevoir = new SpeciesType( 282, "Gardevoir" );
        public static readonly SpeciesType Surskit = new SpeciesType( 283, "Surskit" );
        public static readonly SpeciesType Masquerain = new SpeciesType( 284, "Masquerain" );
        public static readonly SpeciesType Shroomish = new SpeciesType( 285, "Shroomish" );
        public static readonly SpeciesType Breloom = new SpeciesType( 286, "Breloom" );
        public static readonly SpeciesType Slakoth = new SpeciesType( 287, "Slakoth" );
        public static readonly SpeciesType Vigoroth = new SpeciesType( 288, "Vigoroth" );
        public static readonly SpeciesType Slaking = new SpeciesType( 289, "Slaking" );
        public static readonly SpeciesType Nincada = new SpeciesType( 290, "Nincada" );
        public static readonly SpeciesType Ninjask = new SpeciesType( 291, "Ninjask" );
        public static readonly SpeciesType Shedinja = new SpeciesType( 292, "Shedinja" );
        public static readonly SpeciesType Whismur = new SpeciesType( 293, "Whismur" );
        public static readonly SpeciesType Loudred = new SpeciesType( 294, "Loudred" );
        public static readonly SpeciesType Exploud = new SpeciesType( 295, "Exploud" );
        public static readonly SpeciesType Makuhita = new SpeciesType( 296, "Makuhita" );
        public static readonly SpeciesType Hariyama = new SpeciesType( 297, "Hariyama" );
        public static readonly SpeciesType Azurill = new SpeciesType( 298, "Azurill" );
        public static readonly SpeciesType Nosepass = new SpeciesType( 299, "Nosepass" );
        public static readonly SpeciesType Skitty = new SpeciesType( 300, "Skitty" );
        public static readonly SpeciesType Delcatty = new SpeciesType( 301, "Delcatty" );
        public static readonly SpeciesType Sableye = new SpeciesType( 302, "Sableye" );
        public static readonly SpeciesType Mawile = new SpeciesType( 303, "Mawile" );
        public static readonly SpeciesType Aron = new SpeciesType( 304, "Aron" );
        public static readonly SpeciesType Lairon = new SpeciesType( 305, "Lairon" );
        public static readonly SpeciesType Aggron = new SpeciesType( 306, "Aggron" );
        public static readonly SpeciesType Meditite = new SpeciesType( 307, "Meditite" );
        public static readonly SpeciesType Medicham = new SpeciesType( 308, "Medicham" );
        public static readonly SpeciesType Electrike = new SpeciesType( 309, "Electrike" );
        public static readonly SpeciesType Manectric = new SpeciesType( 310, "Manectric" );
        public static readonly SpeciesType Plusle = new SpeciesType( 311, "Plusle" );
        public static readonly SpeciesType Minun = new SpeciesType( 312, "Minun" );
        public static readonly SpeciesType Volbeat = new SpeciesType( 313, "Volbeat" );
        public static readonly SpeciesType Illumise = new SpeciesType( 314, "Illumise" );
        public static readonly SpeciesType Roselia = new SpeciesType( 315, "Roselia" );
        public static readonly SpeciesType Gulpin = new SpeciesType( 316, "Gulpin" );
        public static readonly SpeciesType Swalot = new SpeciesType( 317, "Swalot" );
        public static readonly SpeciesType Carvanha = new SpeciesType( 318, "Carvanha" );
        public static readonly SpeciesType Sharpedo = new SpeciesType( 319, "Sharpedo" );
        public static readonly SpeciesType Wailmer = new SpeciesType( 320, "Wailmer" );
        public static readonly SpeciesType Wailord = new SpeciesType( 321, "Wailord" );
        public static readonly SpeciesType Numel = new SpeciesType( 322, "Numel" );
        public static readonly SpeciesType Camerupt = new SpeciesType( 323, "Camerupt" );
        public static readonly SpeciesType Torkoal = new SpeciesType( 324, "Torkoal" );
        public static readonly SpeciesType Spoink = new SpeciesType( 325, "Spoink" );
        public static readonly SpeciesType Grumpig = new SpeciesType( 326, "Grumpig" );
        public static readonly SpeciesType Spinda = new SpeciesType( 327, "Spinda" );
        public static readonly SpeciesType Trapinch = new SpeciesType( 328, "Trapinch" );
        public static readonly SpeciesType Vibrava = new SpeciesType( 329, "Vibrava" );
        public static readonly SpeciesType Flygon = new SpeciesType( 330, "Flygon" );
        public static readonly SpeciesType Cacnea = new SpeciesType( 331, "Cacnea" );
        public static readonly SpeciesType Cacturne = new SpeciesType( 332, "Cacturne" );
        public static readonly SpeciesType Swablu = new SpeciesType( 333, "Swablu" );
        public static readonly SpeciesType Altaria = new SpeciesType( 334, "Altaria" );
        public static readonly SpeciesType Zangoose = new SpeciesType( 335, "Zangoose" );
        public static readonly SpeciesType Seviper = new SpeciesType( 336, "Seviper" );
        public static readonly SpeciesType Lunatone = new SpeciesType( 337, "Lunatone" );
        public static readonly SpeciesType Solrock = new SpeciesType( 338, "Solrock" );
        public static readonly SpeciesType Barboach = new SpeciesType( 339, "Barboach" );
        public static readonly SpeciesType Whiscash = new SpeciesType( 340, "Whiscash" );
        public static readonly SpeciesType Corphish = new SpeciesType( 341, "Corphish" );
        public static readonly SpeciesType Crawdaunt = new SpeciesType( 342, "Crawdaunt" );
        public static readonly SpeciesType Baltoy = new SpeciesType( 343, "Baltoy" );
        public static readonly SpeciesType Claydol = new SpeciesType( 344, "Claydol" );
        public static readonly SpeciesType Lileep = new SpeciesType( 345, "Lileep" );
        public static readonly SpeciesType Cradily = new SpeciesType( 346, "Cradily" );
        public static readonly SpeciesType Anorith = new SpeciesType( 347, "Anorith" );
        public static readonly SpeciesType Armaldo = new SpeciesType( 348, "Armaldo" );
        public static readonly SpeciesType Feebas = new SpeciesType( 349, "Feebas" );
        public static readonly SpeciesType Milotic = new SpeciesType( 350, "Milotic" );
        public static readonly SpeciesType Castform = new SpeciesType( 351, "Castform" );
        public static readonly SpeciesType Kecleon = new SpeciesType( 352, "Kecleon" );
        public static readonly SpeciesType Shuppet = new SpeciesType( 353, "Shuppet" );
        public static readonly SpeciesType Banette = new SpeciesType( 354, "Banette" );
        public static readonly SpeciesType Duskull = new SpeciesType( 355, "Duskull" );
        public static readonly SpeciesType Dusclops = new SpeciesType( 356, "Dusclops" );
        public static readonly SpeciesType Tropius = new SpeciesType( 357, "Tropius" );
        public static readonly SpeciesType Chimecho = new SpeciesType( 358, "Chimecho" );
        public static readonly SpeciesType Absol = new SpeciesType( 359, "Absol" );
        public static readonly SpeciesType Wynaut = new SpeciesType( 360, "Wynaut" );
        public static readonly SpeciesType Snorunt = new SpeciesType( 361, "Snorunt" );
        public static readonly SpeciesType Glalie = new SpeciesType( 362, "Glalie" );
        public static readonly SpeciesType Spheal = new SpeciesType( 363, "Spheal" );
        public static readonly SpeciesType Sealeo = new SpeciesType( 364, "Sealeo" );
        public static readonly SpeciesType Walrein = new SpeciesType( 365, "Walrein" );
        public static readonly SpeciesType Clamperl = new SpeciesType( 366, "Clamperl" );
        public static readonly SpeciesType Huntail = new SpeciesType( 367, "Huntail" );
        public static readonly SpeciesType Gorebyss = new SpeciesType( 368, "Gorebyss" );
        public static readonly SpeciesType Relicanth = new SpeciesType( 369, "Relicanth" );
        public static readonly SpeciesType Luvdisc = new SpeciesType( 370, "Luvdisc" );
        public static readonly SpeciesType Bagon = new SpeciesType( 371, "Bagon" );
        public static readonly SpeciesType Shelgon = new SpeciesType( 372, "Shelgon" );
        public static readonly SpeciesType Salamence = new SpeciesType( 373, "Salamence" );
        public static readonly SpeciesType Beldum = new SpeciesType( 374, "Beldum" );
        public static readonly SpeciesType Metang = new SpeciesType( 375, "Metang" );
        public static readonly SpeciesType Metagross = new SpeciesType( 376, "Metagross" );
        public static readonly SpeciesType Regirock = new SpeciesType( 377, "Regirock" );
        public static readonly SpeciesType Regice = new SpeciesType( 378, "Regice" );
        public static readonly SpeciesType Registeel = new SpeciesType( 379, "Registeel" );
        public static readonly SpeciesType Latias = new SpeciesType( 380, "Latias" );
        public static readonly SpeciesType Latios = new SpeciesType( 381, "Latios" );
        public static readonly SpeciesType Kyogre = new SpeciesType( 382, "Kyogre" );
        public static readonly SpeciesType Groudon = new SpeciesType( 383, "Groudon" );
        public static readonly SpeciesType Rayquaza = new SpeciesType( 384, "Rayquaza" );
        public static readonly SpeciesType Jirachi = new SpeciesType( 385, "Jirachi" );
        public static readonly SpeciesType Deoxys = new SpeciesType( 386, "Deoxys" );
        public static readonly SpeciesType Turtwig = new SpeciesType( 387, "Turtwig" );
        public static readonly SpeciesType Grotle = new SpeciesType( 388, "Grotle" );
        public static readonly SpeciesType Torterra = new SpeciesType( 389, "Torterra" );
        public static readonly SpeciesType Chimchar = new SpeciesType( 390, "Chimchar" );
        public static readonly SpeciesType Monferno = new SpeciesType( 391, "Monferno" );
        public static readonly SpeciesType Infernape = new SpeciesType( 392, "Infernape" );
        public static readonly SpeciesType Piplup = new SpeciesType( 393, "Piplup" );
        public static readonly SpeciesType Prinplup = new SpeciesType( 394, "Prinplup" );
        public static readonly SpeciesType Empoleon = new SpeciesType( 395, "Empoleon" );
        public static readonly SpeciesType Starly = new SpeciesType( 396, "Starly" );
        public static readonly SpeciesType Staravia = new SpeciesType( 397, "Staravia" );
        public static readonly SpeciesType Staraptor = new SpeciesType( 398, "Staraptor" );
        public static readonly SpeciesType Bidoof = new SpeciesType( 399, "Bidoof" );
        public static readonly SpeciesType Bibarel = new SpeciesType( 400, "Bibarel" );
        public static readonly SpeciesType Kricketot = new SpeciesType( 401, "Kricketot" );
        public static readonly SpeciesType Kricketune = new SpeciesType( 402, "Kricketune" );
        public static readonly SpeciesType Shinx = new SpeciesType( 403, "Shinx" );
        public static readonly SpeciesType Luxio = new SpeciesType( 404, "Luxio" );
        public static readonly SpeciesType Luxray = new SpeciesType( 405, "Luxray" );
        public static readonly SpeciesType Budew = new SpeciesType( 406, "Budew" );
        public static readonly SpeciesType Roserade = new SpeciesType( 407, "Roserade" );
        public static readonly SpeciesType Cranidos = new SpeciesType( 408, "Cranidos" );
        public static readonly SpeciesType Rampardos = new SpeciesType( 409, "Rampardos" );
        public static readonly SpeciesType Shieldon = new SpeciesType( 410, "Shieldon" );
        public static readonly SpeciesType Bastiodon = new SpeciesType( 411, "Bastiodon" );
        public static readonly SpeciesType Burmy = new SpeciesType( 412, "Burmy" );
        public static readonly SpeciesType Wormadam = new SpeciesType( 413, "Wormadam" );
        public static readonly SpeciesType Mothim = new SpeciesType( 414, "Mothim" );
        public static readonly SpeciesType Combee = new SpeciesType( 415, "Combee" );
        public static readonly SpeciesType Vespiquen = new SpeciesType( 416, "Vespiquen" );
        public static readonly SpeciesType Pachirisu = new SpeciesType( 417, "Pachirisu" );
        public static readonly SpeciesType Buizel = new SpeciesType( 418, "Buizel" );
        public static readonly SpeciesType Floatzel = new SpeciesType( 419, "Floatzel" );
        public static readonly SpeciesType Cherubi = new SpeciesType( 420, "Cherubi" );
        public static readonly SpeciesType Cherrim = new SpeciesType( 421, "Cherrim" );
        public static readonly SpeciesType Shellos = new SpeciesType( 422, "Shellos" );
        public static readonly SpeciesType Gastrodon = new SpeciesType( 423, "Gastrodon" );
        public static readonly SpeciesType Ambipom = new SpeciesType( 424, "Ambipom" );
        public static readonly SpeciesType Drifloon = new SpeciesType( 425, "Drifloon" );
        public static readonly SpeciesType Drifblim = new SpeciesType( 426, "Drifblim" );
        public static readonly SpeciesType Buneary = new SpeciesType( 427, "Buneary" );
        public static readonly SpeciesType Lopunny = new SpeciesType( 428, "Lopunny" );
        public static readonly SpeciesType Mismagius = new SpeciesType( 429, "Mismagius" );
        public static readonly SpeciesType Honchkrow = new SpeciesType( 430, "Honchkrow" );
        public static readonly SpeciesType Glameow = new SpeciesType( 431, "Glameow" );
        public static readonly SpeciesType Purugly = new SpeciesType( 432, "Purugly" );
        public static readonly SpeciesType Chingling = new SpeciesType( 433, "Chingling" );
        public static readonly SpeciesType Stunky = new SpeciesType( 434, "Stunky" );
        public static readonly SpeciesType Skuntank = new SpeciesType( 435, "Skuntank" );
        public static readonly SpeciesType Bronzor = new SpeciesType( 436, "Bronzor" );
        public static readonly SpeciesType Bronzong = new SpeciesType( 437, "Bronzong" );
        public static readonly SpeciesType Bonsly = new SpeciesType( 438, "Bonsly" );
        public static readonly SpeciesType MimeJr = new SpeciesType( 439, "Mime Jr." );
        public static readonly SpeciesType Happiny = new SpeciesType( 440, "Happiny" );
        public static readonly SpeciesType Chatot = new SpeciesType( 441, "Chatot" );
        public static readonly SpeciesType Spiritomb = new SpeciesType( 442, "Spiritomb" );
        public static readonly SpeciesType Gible = new SpeciesType( 443, "Gible" );
        public static readonly SpeciesType Gabite = new SpeciesType( 444, "Gabite" );
        public static readonly SpeciesType Garchomp = new SpeciesType( 445, "Garchomp" );
        public static readonly SpeciesType Munchlax = new SpeciesType( 446, "Munchlax" );
        public static readonly SpeciesType Riolu = new SpeciesType( 447, "Riolu" );
        public static readonly SpeciesType Lucario = new SpeciesType( 448, "Lucario" );
        public static readonly SpeciesType Hippopotas = new SpeciesType( 449, "Hippopotas" );
        public static readonly SpeciesType Hippowdon = new SpeciesType( 450, "Hippowdon" );
        public static readonly SpeciesType Skorupi = new SpeciesType( 451, "Skorupi" );
        public static readonly SpeciesType Drapion = new SpeciesType( 452, "Drapion" );
        public static readonly SpeciesType Croagunk = new SpeciesType( 453, "Croagunk" );
        public static readonly SpeciesType Toxicroak = new SpeciesType( 454, "Toxicroak" );
        public static readonly SpeciesType Carnivine = new SpeciesType( 455, "Carnivine" );
        public static readonly SpeciesType Finneon = new SpeciesType( 456, "Finneon" );
        public static readonly SpeciesType Lumineon = new SpeciesType( 457, "Lumineon" );
        public static readonly SpeciesType Mantyke = new SpeciesType( 458, "Mantyke" );
        public static readonly SpeciesType Snover = new SpeciesType( 459, "Snover" );
        public static readonly SpeciesType Abomasnow = new SpeciesType( 460, "Abomasnow" );
        public static readonly SpeciesType Weavile = new SpeciesType( 461, "Weavile" );
        public static readonly SpeciesType Magnezone = new SpeciesType( 462, "Magnezone" );
        public static readonly SpeciesType Lickilicky = new SpeciesType( 463, "Lickilicky" );
        public static readonly SpeciesType Rhyperior = new SpeciesType( 464, "Rhyperior" );
        public static readonly SpeciesType Tangrowth = new SpeciesType( 465, "Tangrowth" );
        public static readonly SpeciesType Electivire = new SpeciesType( 466, "Electivire" );
        public static readonly SpeciesType Magmortar = new SpeciesType( 467, "Magmortar" );
        public static readonly SpeciesType Togekiss = new SpeciesType( 468, "Togekiss" );
        public static readonly SpeciesType Yanmega = new SpeciesType( 469, "Yanmega" );
        public static readonly SpeciesType Leafeon = new SpeciesType( 470, "Leafeon" );
        public static readonly SpeciesType Glaceon = new SpeciesType( 471, "Glaceon" );
        public static readonly SpeciesType Gliscor = new SpeciesType( 472, "Gliscor" );
        public static readonly SpeciesType Mamoswine = new SpeciesType( 473, "Mamoswine" );
        public static readonly SpeciesType PorygonZ = new SpeciesType( 474, "Porygon-Z" );
        public static readonly SpeciesType Gallade = new SpeciesType( 475, "Gallade" );
        public static readonly SpeciesType Probopass = new SpeciesType( 476, "Probopass" );
        public static readonly SpeciesType Dusknoir = new SpeciesType( 477, "Dusknoir" );
        public static readonly SpeciesType Froslass = new SpeciesType( 478, "Froslass" );
        public static readonly SpeciesType Rotom = new SpeciesType( 479, "Rotom" );
        public static readonly SpeciesType Uxie = new SpeciesType( 480, "Uxie" );
        public static readonly SpeciesType Mesprit = new SpeciesType( 481, "Mesprit" );
        public static readonly SpeciesType Azelf = new SpeciesType( 482, "Azelf" );
        public static readonly SpeciesType Dialga = new SpeciesType( 483, "Dialga" );
        public static readonly SpeciesType Palkia = new SpeciesType( 484, "Palkia" );
        public static readonly SpeciesType Heatran = new SpeciesType( 485, "Heatran" );
        public static readonly SpeciesType Regigigas = new SpeciesType( 486, "Regigigas" );
        public static readonly SpeciesType Giratina = new SpeciesType( 487, "Giratina" );
        public static readonly SpeciesType Cresselia = new SpeciesType( 488, "Cresselia" );
        public static readonly SpeciesType Phione = new SpeciesType( 489, "Phione" );
        public static readonly SpeciesType Manaphy = new SpeciesType( 490, "Manaphy" );
        public static readonly SpeciesType Darkrai = new SpeciesType( 491, "Darkrai" );
        public static readonly SpeciesType Shaymin = new SpeciesType( 492, "Shaymin" );
        public static readonly SpeciesType Arceus = new SpeciesType( 493, "Arceus" );
        public static readonly SpeciesType Victini = new SpeciesType( 494, "Victini" );
        public static readonly SpeciesType Snivy = new SpeciesType( 495, "Snivy" );
        public static readonly SpeciesType Servine = new SpeciesType( 496, "Servine" );
        public static readonly SpeciesType Serperior = new SpeciesType( 497, "Serperior" );
        public static readonly SpeciesType Tepig = new SpeciesType( 498, "Tepig" );
        public static readonly SpeciesType Pignite = new SpeciesType( 499, "Pignite" );
        public static readonly SpeciesType Emboar = new SpeciesType( 500, "Emboar" );
        public static readonly SpeciesType Oshawott = new SpeciesType( 501, "Oshawott" );
        public static readonly SpeciesType Dewott = new SpeciesType( 502, "Dewott" );
        public static readonly SpeciesType Samurott = new SpeciesType( 503, "Samurott" );
        public static readonly SpeciesType Patrat = new SpeciesType( 504, "Patrat" );
        public static readonly SpeciesType Watchog = new SpeciesType( 505, "Watchog" );
        public static readonly SpeciesType Lillipup = new SpeciesType( 506, "Lillipup" );
        public static readonly SpeciesType Herdier = new SpeciesType( 507, "Herdier" );
        public static readonly SpeciesType Stoutland = new SpeciesType( 508, "Stoutland" );
        public static readonly SpeciesType Purrloin = new SpeciesType( 509, "Purrloin" );
        public static readonly SpeciesType Liepard = new SpeciesType( 510, "Liepard" );
        public static readonly SpeciesType Pansage = new SpeciesType( 511, "Pansage" );
        public static readonly SpeciesType Simisage = new SpeciesType( 512, "Simisage" );
        public static readonly SpeciesType Pansear = new SpeciesType( 513, "Pansear" );
        public static readonly SpeciesType Simisear = new SpeciesType( 514, "Simisear" );
        public static readonly SpeciesType Panpour = new SpeciesType( 515, "Panpour" );
        public static readonly SpeciesType Simipour = new SpeciesType( 516, "Simipour" );
        public static readonly SpeciesType Munna = new SpeciesType( 517, "Munna" );
        public static readonly SpeciesType Musharna = new SpeciesType( 518, "Musharna" );
        public static readonly SpeciesType Pidove = new SpeciesType( 519, "Pidove" );
        public static readonly SpeciesType Tranquill = new SpeciesType( 520, "Tranquill" );
        public static readonly SpeciesType Unfezant = new SpeciesType( 521, "Unfezant" );
        public static readonly SpeciesType Blitzle = new SpeciesType( 522, "Blitzle" );
        public static readonly SpeciesType Zebstrika = new SpeciesType( 523, "Zebstrika" );
        public static readonly SpeciesType Roggenrola = new SpeciesType( 524, "Roggenrola" );
        public static readonly SpeciesType Boldore = new SpeciesType( 525, "Boldore" );
        public static readonly SpeciesType Gigalith = new SpeciesType( 526, "Gigalith" );
        public static readonly SpeciesType Woobat = new SpeciesType( 527, "Woobat" );
        public static readonly SpeciesType Swoobat = new SpeciesType( 528, "Swoobat" );
        public static readonly SpeciesType Drilbur = new SpeciesType( 529, "Drilbur" );
        public static readonly SpeciesType Excadrill = new SpeciesType( 530, "Excadrill" );
        public static readonly SpeciesType Audino = new SpeciesType( 531, "Audino" );
        public static readonly SpeciesType Timburr = new SpeciesType( 532, "Timburr" );
        public static readonly SpeciesType Gurdurr = new SpeciesType( 533, "Gurdurr" );
        public static readonly SpeciesType Conkeldurr = new SpeciesType( 534, "Conkeldurr" );
        public static readonly SpeciesType Tympole = new SpeciesType( 535, "Tympole" );
        public static readonly SpeciesType Palpitoad = new SpeciesType( 536, "Palpitoad" );
        public static readonly SpeciesType Seismitoad = new SpeciesType( 537, "Seismitoad" );
        public static readonly SpeciesType Throh = new SpeciesType( 538, "Throh" );
        public static readonly SpeciesType Sawk = new SpeciesType( 539, "Sawk" );
        public static readonly SpeciesType Sewaddle = new SpeciesType( 540, "Sewaddle" );
        public static readonly SpeciesType Swadloon = new SpeciesType( 541, "Swadloon" );
        public static readonly SpeciesType Leavanny = new SpeciesType( 542, "Leavanny" );
        public static readonly SpeciesType Venipede = new SpeciesType( 543, "Venipede" );
        public static readonly SpeciesType Whirlipede = new SpeciesType( 544, "Whirlipede" );
        public static readonly SpeciesType Scolipede = new SpeciesType( 545, "Scolipede" );
        public static readonly SpeciesType Cottonee = new SpeciesType( 546, "Cottonee" );
        public static readonly SpeciesType Whimsicott = new SpeciesType( 547, "Whimsicott" );
        public static readonly SpeciesType Petilil = new SpeciesType( 548, "Petilil" );
        public static readonly SpeciesType Lilligant = new SpeciesType( 549, "Lilligant" );
        public static readonly SpeciesType Basculin = new SpeciesType( 550, "Basculin" );
        public static readonly SpeciesType Sandile = new SpeciesType( 551, "Sandile" );
        public static readonly SpeciesType Krokorok = new SpeciesType( 552, "Krokorok" );
        public static readonly SpeciesType Krookodile = new SpeciesType( 553, "Krookodile" );
        public static readonly SpeciesType Darumaka = new SpeciesType( 554, "Darumaka" );
        public static readonly SpeciesType Darmanitan = new SpeciesType( 555, "Darmanitan" );
        public static readonly SpeciesType Maractus = new SpeciesType( 556, "Maractus" );
        public static readonly SpeciesType Dwebble = new SpeciesType( 557, "Dwebble" );
        public static readonly SpeciesType Crustle = new SpeciesType( 558, "Crustle" );
        public static readonly SpeciesType Scraggy = new SpeciesType( 559, "Scraggy" );
        public static readonly SpeciesType Scrafty = new SpeciesType( 560, "Scrafty" );
        public static readonly SpeciesType Sigilyph = new SpeciesType( 561, "Sigilyph" );
        public static readonly SpeciesType Yamask = new SpeciesType( 562, "Yamask" );
        public static readonly SpeciesType Cofagrigus = new SpeciesType( 563, "Cofagrigus" );
        public static readonly SpeciesType Tirtouga = new SpeciesType( 564, "Tirtouga" );
        public static readonly SpeciesType Carracosta = new SpeciesType( 565, "Carracosta" );
        public static readonly SpeciesType Archen = new SpeciesType( 566, "Archen" );
        public static readonly SpeciesType Archeops = new SpeciesType( 567, "Archeops" );
        public static readonly SpeciesType Trubbish = new SpeciesType( 568, "Trubbish" );
        public static readonly SpeciesType Garbodor = new SpeciesType( 569, "Garbodor" );
        public static readonly SpeciesType Zorua = new SpeciesType( 570, "Zorua" );
        public static readonly SpeciesType Zoroark = new SpeciesType( 571, "Zoroark" );
        public static readonly SpeciesType Minccino = new SpeciesType( 572, "Minccino" );
        public static readonly SpeciesType Cinccino = new SpeciesType( 573, "Cinccino" );
        public static readonly SpeciesType Gothita = new SpeciesType( 574, "Gothita" );
        public static readonly SpeciesType Gothorita = new SpeciesType( 575, "Gothorita" );
        public static readonly SpeciesType Gothitelle = new SpeciesType( 576, "Gothitelle" );
        public static readonly SpeciesType Solosis = new SpeciesType( 577, "Solosis" );
        public static readonly SpeciesType Duosion = new SpeciesType( 578, "Duosion" );
        public static readonly SpeciesType Reuniclus = new SpeciesType( 579, "Reuniclus" );
        public static readonly SpeciesType Ducklett = new SpeciesType( 580, "Ducklett" );
        public static readonly SpeciesType Swanna = new SpeciesType( 581, "Swanna" );
        public static readonly SpeciesType Vanillite = new SpeciesType( 582, "Vanillite" );
        public static readonly SpeciesType Vanillish = new SpeciesType( 583, "Vanillish" );
        public static readonly SpeciesType Vanilluxe = new SpeciesType( 584, "Vanilluxe" );
        public static readonly SpeciesType Deerling = new SpeciesType( 585, "Deerling" );
        public static readonly SpeciesType Sawsbuck = new SpeciesType( 586, "Sawsbuck" );
        public static readonly SpeciesType Emolga = new SpeciesType( 587, "Emolga" );
        public static readonly SpeciesType Karrablast = new SpeciesType( 588, "Karrablast" );
        public static readonly SpeciesType Escavalier = new SpeciesType( 589, "Escavalier" );
        public static readonly SpeciesType Foongus = new SpeciesType( 590, "Foongus" );
        public static readonly SpeciesType Amoonguss = new SpeciesType( 591, "Amoonguss" );
        public static readonly SpeciesType Frillish = new SpeciesType( 592, "Frillish" );
        public static readonly SpeciesType Jellicent = new SpeciesType( 593, "Jellicent" );
        public static readonly SpeciesType Alomomola = new SpeciesType( 594, "Alomomola" );
        public static readonly SpeciesType Joltik = new SpeciesType( 595, "Joltik" );
        public static readonly SpeciesType Galvantula = new SpeciesType( 596, "Galvantula" );
        public static readonly SpeciesType Ferroseed = new SpeciesType( 597, "Ferroseed" );
        public static readonly SpeciesType Ferrothorn = new SpeciesType( 598, "Ferrothorn" );
        public static readonly SpeciesType Klink = new SpeciesType( 599, "Klink" );
        public static readonly SpeciesType Klang = new SpeciesType( 600, "Klang" );
        public static readonly SpeciesType Klinklang = new SpeciesType( 601, "Klinklang" );
        public static readonly SpeciesType Tynamo = new SpeciesType( 602, "Tynamo" );
        public static readonly SpeciesType Eelektrik = new SpeciesType( 603, "Eelektrik" );
        public static readonly SpeciesType Eelektross = new SpeciesType( 604, "Eelektross" );
        public static readonly SpeciesType Elgyem = new SpeciesType( 605, "Elgyem" );
        public static readonly SpeciesType Beheeyem = new SpeciesType( 606, "Beheeyem" );
        public static readonly SpeciesType Litwick = new SpeciesType( 607, "Litwick" );
        public static readonly SpeciesType Lampent = new SpeciesType( 608, "Lampent" );
        public static readonly SpeciesType Chandelure = new SpeciesType( 609, "Chandelure" );
        public static readonly SpeciesType Axew = new SpeciesType( 610, "Axew" );
        public static readonly SpeciesType Fraxure = new SpeciesType( 611, "Fraxure" );
        public static readonly SpeciesType Haxorus = new SpeciesType( 612, "Haxorus" );
        public static readonly SpeciesType Cubchoo = new SpeciesType( 613, "Cubchoo" );
        public static readonly SpeciesType Beartic = new SpeciesType( 614, "Beartic" );
        public static readonly SpeciesType Cryogonal = new SpeciesType( 615, "Cryogonal" );
        public static readonly SpeciesType Shelmet = new SpeciesType( 616, "Shelmet" );
        public static readonly SpeciesType Accelgor = new SpeciesType( 617, "Accelgor" );
        public static readonly SpeciesType Stunfisk = new SpeciesType( 618, "Stunfisk" );
        public static readonly SpeciesType Mienfoo = new SpeciesType( 619, "Mienfoo" );
        public static readonly SpeciesType Mienshao = new SpeciesType( 620, "Mienshao" );
        public static readonly SpeciesType Druddigon = new SpeciesType( 621, "Druddigon" );
        public static readonly SpeciesType Golett = new SpeciesType( 622, "Golett" );
        public static readonly SpeciesType Golurk = new SpeciesType( 623, "Golurk" );
        public static readonly SpeciesType Pawniard = new SpeciesType( 624, "Pawniard" );
        public static readonly SpeciesType Bisharp = new SpeciesType( 625, "Bisharp" );
        public static readonly SpeciesType Bouffalant = new SpeciesType( 626, "Bouffalant" );
        public static readonly SpeciesType Rufflet = new SpeciesType( 627, "Rufflet" );
        public static readonly SpeciesType Braviary = new SpeciesType( 628, "Braviary" );
        public static readonly SpeciesType Vullaby = new SpeciesType( 629, "Vullaby" );
        public static readonly SpeciesType Mandibuzz = new SpeciesType( 630, "Mandibuzz" );
        public static readonly SpeciesType Heatmor = new SpeciesType( 631, "Heatmor" );
        public static readonly SpeciesType Durant = new SpeciesType( 632, "Durant" );
        public static readonly SpeciesType Deino = new SpeciesType( 633, "Deino" );
        public static readonly SpeciesType Zweilous = new SpeciesType( 634, "Zweilous" );
        public static readonly SpeciesType Hydreigon = new SpeciesType( 635, "Hydreigon" );
        public static readonly SpeciesType Larvesta = new SpeciesType( 636, "Larvesta" );
        public static readonly SpeciesType Volcarona = new SpeciesType( 637, "Volcarona" );
        public static readonly SpeciesType Cobalion = new SpeciesType( 638, "Cobalion" );
        public static readonly SpeciesType Terrakion = new SpeciesType( 639, "Terrakion" );
        public static readonly SpeciesType Virizion = new SpeciesType( 640, "Virizion" );
        public static readonly SpeciesType Tornadus = new SpeciesType( 641, "Tornadus" );
        public static readonly SpeciesType Thundurus = new SpeciesType( 642, "Thundurus" );
        public static readonly SpeciesType Reshiram = new SpeciesType( 643, "Reshiram" );
        public static readonly SpeciesType Zekrom = new SpeciesType( 644, "Zekrom" );
        public static readonly SpeciesType Landorus = new SpeciesType( 645, "Landorus" );
        public static readonly SpeciesType Kyurem = new SpeciesType( 646, "Kyurem" );
        public static readonly SpeciesType Keldeo = new SpeciesType( 647, "Keldeo" );
        public static readonly SpeciesType Meloetta = new SpeciesType( 648, "Meloetta" );
        public static readonly SpeciesType Genesect = new SpeciesType( 649, "Genesect" );
        public static readonly SpeciesType Chespin = new SpeciesType( 650, "Chespin" );
        public static readonly SpeciesType Quilladin = new SpeciesType( 651, "Quilladin" );
        public static readonly SpeciesType Chesnaught = new SpeciesType( 652, "Chesnaught" );
        public static readonly SpeciesType Fennekin = new SpeciesType( 653, "Fennekin" );
        public static readonly SpeciesType Braixen = new SpeciesType( 654, "Braixen" );
        public static readonly SpeciesType Delphox = new SpeciesType( 655, "Delphox" );
        public static readonly SpeciesType Froakie = new SpeciesType( 656, "Froakie" );
        public static readonly SpeciesType Frogadier = new SpeciesType( 657, "Frogadier" );
        public static readonly SpeciesType Greninja = new SpeciesType( 658, "Greninja" );
        public static readonly SpeciesType Bunnelby = new SpeciesType( 659, "Bunnelby" );
        public static readonly SpeciesType Diggersby = new SpeciesType( 660, "Diggersby" );
        public static readonly SpeciesType Fletchling = new SpeciesType( 661, "Fletchling" );
        public static readonly SpeciesType Fletchinder = new SpeciesType( 662, "Fletchinder" );
        public static readonly SpeciesType Talonflame = new SpeciesType( 663, "Talonflame" );
        public static readonly SpeciesType Scatterbug = new SpeciesType( 664, "Scatterbug" );
        public static readonly SpeciesType Spewpa = new SpeciesType( 665, "Spewpa" );
        public static readonly SpeciesType Vivillon = new SpeciesType( 666, "Vivillon" );
        public static readonly SpeciesType Litleo = new SpeciesType( 667, "Litleo" );
        public static readonly SpeciesType Pyroar = new SpeciesType( 668, "Pyroar" );
        public static readonly SpeciesType Flabb = new SpeciesType( 669, "Flabébé" );
        public static readonly SpeciesType Floette = new SpeciesType( 670, "Floette" );
        public static readonly SpeciesType Florges = new SpeciesType( 671, "Florges" );
        public static readonly SpeciesType Skiddo = new SpeciesType( 672, "Skiddo" );
        public static readonly SpeciesType Gogoat = new SpeciesType( 673, "Gogoat" );
        public static readonly SpeciesType Pancham = new SpeciesType( 674, "Pancham" );
        public static readonly SpeciesType Pangoro = new SpeciesType( 675, "Pangoro" );
        public static readonly SpeciesType Furfrou = new SpeciesType( 676, "Furfrou" );
        public static readonly SpeciesType Espurr = new SpeciesType( 677, "Espurr" );
        public static readonly SpeciesType Meowstic = new SpeciesType( 678, "Meowstic" );
        public static readonly SpeciesType Honedge = new SpeciesType( 679, "Honedge" );
        public static readonly SpeciesType Doublade = new SpeciesType( 680, "Doublade" );
        public static readonly SpeciesType Aegislash = new SpeciesType( 681, "Aegislash" );
        public static readonly SpeciesType Spritzee = new SpeciesType( 682, "Spritzee" );
        public static readonly SpeciesType Aromatisse = new SpeciesType( 683, "Aromatisse" );
        public static readonly SpeciesType Swirlix = new SpeciesType( 684, "Swirlix" );
        public static readonly SpeciesType Slurpuff = new SpeciesType( 685, "Slurpuff" );
        public static readonly SpeciesType Inkay = new SpeciesType( 686, "Inkay" );
        public static readonly SpeciesType Malamar = new SpeciesType( 687, "Malamar" );
        public static readonly SpeciesType Binacle = new SpeciesType( 688, "Binacle" );
        public static readonly SpeciesType Barbaracle = new SpeciesType( 689, "Barbaracle" );
        public static readonly SpeciesType Skrelp = new SpeciesType( 690, "Skrelp" );
        public static readonly SpeciesType Dragalge = new SpeciesType( 691, "Dragalge" );
        public static readonly SpeciesType Clauncher = new SpeciesType( 692, "Clauncher" );
        public static readonly SpeciesType Clawitzer = new SpeciesType( 693, "Clawitzer" );
        public static readonly SpeciesType Helioptile = new SpeciesType( 694, "Helioptile" );
        public static readonly SpeciesType Heliolisk = new SpeciesType( 695, "Heliolisk" );
        public static readonly SpeciesType Tyrunt = new SpeciesType( 696, "Tyrunt" );
        public static readonly SpeciesType Tyrantrum = new SpeciesType( 697, "Tyrantrum" );
        public static readonly SpeciesType Amaura = new SpeciesType( 698, "Amaura" );
        public static readonly SpeciesType Aurorus = new SpeciesType( 699, "Aurorus" );
        public static readonly SpeciesType Sylveon = new SpeciesType( 700, "Sylveon" );
        public static readonly SpeciesType Hawlucha = new SpeciesType( 701, "Hawlucha" );
        public static readonly SpeciesType Dedenne = new SpeciesType( 702, "Dedenne" );
        public static readonly SpeciesType Carbink = new SpeciesType( 703, "Carbink" );
        public static readonly SpeciesType Goomy = new SpeciesType( 704, "Goomy" );
        public static readonly SpeciesType Sliggoo = new SpeciesType( 705, "Sliggoo" );
        public static readonly SpeciesType Goodra = new SpeciesType( 706, "Goodra" );
        public static readonly SpeciesType Klefki = new SpeciesType( 707, "Klefki" );
        public static readonly SpeciesType Phantump = new SpeciesType( 708, "Phantump" );
        public static readonly SpeciesType Trevenant = new SpeciesType( 709, "Trevenant" );
        public static readonly SpeciesType Pumpkaboo = new SpeciesType( 710, "Pumpkaboo" );
        public static readonly SpeciesType Gourgeist = new SpeciesType( 711, "Gourgeist" );
        public static readonly SpeciesType Bergmite = new SpeciesType( 712, "Bergmite" );
        public static readonly SpeciesType Avalugg = new SpeciesType( 713, "Avalugg" );
        public static readonly SpeciesType Noibat = new SpeciesType( 714, "Noibat" );
        public static readonly SpeciesType Noivern = new SpeciesType( 715, "Noivern" );
        public static readonly SpeciesType Xerneas = new SpeciesType( 716, "Xerneas" );
        public static readonly SpeciesType Yveltal = new SpeciesType( 717, "Yveltal" );
        public static readonly SpeciesType Zygarde = new SpeciesType( 718, "Zygarde" );
        public static readonly SpeciesType Diancie = new SpeciesType( 719, "Diancie" );
        public static readonly SpeciesType Hoopa = new SpeciesType( 720, "Hoopa" );
        public static readonly SpeciesType Volcanion = new SpeciesType( 721, "Volcanion" );
        
        private static readonly SpeciesType[] staticValues = {
            Egg,
            Bulbasaur,
            Ivysaur,
            Venusaur,
            Charmander,
            Charmeleon,
            Charizard,
            Squirtle,
            Wartortle,
            Blastoise,
            Caterpie,
            Metapod,
            Butterfree,
            Weedle,
            Kakuna,
            Beedrill,
            Pidgey,
            Pidgeotto,
            Pidgeot,
            Rattata,
            Raticate,
            Spearow,
            Fearow,
            Ekans,
            Arbok,
            Pikachu,
            Raichu,
            Sandshrew,
            Sandslash,
            NidoranF,
            Nidorina,
            Nidoqueen,
            NidoranM,
            Nidorino,
            Nidoking,
            Clefairy,
            Clefable,
            Vulpix,
            Ninetales,
            Jigglypuff,
            Wigglytuff,
            Zubat,
            Golbat,
            Oddish,
            Gloom,
            Vileplume,
            Paras,
            Parasect,
            Venonat,
            Venomoth,
            Diglett,
            Dugtrio,
            Meowth,
            Persian,
            Psyduck,
            Golduck,
            Mankey,
            Primeape,
            Growlithe,
            Arcanine,
            Poliwag,
            Poliwhirl,
            Poliwrath,
            Abra,
            Kadabra,
            Alakazam,
            Machop,
            Machoke,
            Machamp,
            Bellsprout,
            Weepinbell,
            Victreebel,
            Tentacool,
            Tentacruel,
            Geodude,
            Graveler,
            Golem,
            Ponyta,
            Rapidash,
            Slowpoke,
            Slowbro,
            Magnemite,
            Magneton,
            Farfetchd,
            Doduo,
            Dodrio,
            Seel,
            Dewgong,
            Grimer,
            Muk,
            Shellder,
            Cloyster,
            Gastly,
            Haunter,
            Gengar,
            Onix,
            Drowzee,
            Hypno,
            Krabby,
            Kingler,
            Voltorb,
            Electrode,
            Exeggcute,
            Exeggutor,
            Cubone,
            Marowak,
            Hitmonlee,
            Hitmonchan,
            Lickitung,
            Koffing,
            Weezing,
            Rhyhorn,
            Rhydon,
            Chansey,
            Tangela,
            Kangaskhan,
            Horsea,
            Seadra,
            Goldeen,
            Seaking,
            Staryu,
            Starmie,
            MrMime,
            Scyther,
            Jynx,
            Electabuzz,
            Magmar,
            Pinsir,
            Tauros,
            Magikarp,
            Gyarados,
            Lapras,
            Ditto,
            Eevee,
            Vaporeon,
            Jolteon,
            Flareon,
            Porygon,
            Omanyte,
            Omastar,
            Kabuto,
            Kabutops,
            Aerodactyl,
            Snorlax,
            Articuno,
            Zapdos,
            Moltres,
            Dratini,
            Dragonair,
            Dragonite,
            Mewtwo,
            Mew,
            Chikorita,
            Bayleef,
            Meganium,
            Cyndaquil,
            Quilava,
            Typhlosion,
            Totodile,
            Croconaw,
            Feraligatr,
            Sentret,
            Furret,
            Hoothoot,
            Noctowl,
            Ledyba,
            Ledian,
            Spinarak,
            Ariados,
            Crobat,
            Chinchou,
            Lanturn,
            Pichu,
            Cleffa,
            Igglybuff,
            Togepi,
            Togetic,
            Natu,
            Xatu,
            Mareep,
            Flaaffy,
            Ampharos,
            Bellossom,
            Marill,
            Azumarill,
            Sudowoodo,
            Politoed,
            Hoppip,
            Skiploom,
            Jumpluff,
            Aipom,
            Sunkern,
            Sunflora,
            Yanma,
            Wooper,
            Quagsire,
            Espeon,
            Umbreon,
            Murkrow,
            Slowking,
            Misdreavus,
            Unown,
            Wobbuffet,
            Girafarig,
            Pineco,
            Forretress,
            Dunsparce,
            Gligar,
            Steelix,
            Snubbull,
            Granbull,
            Qwilfish,
            Scizor,
            Shuckle,
            Heracross,
            Sneasel,
            Teddiursa,
            Ursaring,
            Slugma,
            Magcargo,
            Swinub,
            Piloswine,
            Corsola,
            Remoraid,
            Octillery,
            Delibird,
            Mantine,
            Skarmory,
            Houndour,
            Houndoom,
            Kingdra,
            Phanpy,
            Donphan,
            Porygon2,
            Stantler,
            Smeargle,
            Tyrogue,
            Hitmontop,
            Smoochum,
            Elekid,
            Magby,
            Miltank,
            Blissey,
            Raikou,
            Entei,
            Suicune,
            Larvitar,
            Pupitar,
            Tyranitar,
            Lugia,
            HoOh,
            Celebi,
            Treecko,
            Grovyle,
            Sceptile,
            Torchic,
            Combusken,
            Blaziken,
            Mudkip,
            Marshtomp,
            Swampert,
            Poochyena,
            Mightyena,
            Zigzagoon,
            Linoone,
            Wurmple,
            Silcoon,
            Beautifly,
            Cascoon,
            Dustox,
            Lotad,
            Lombre,
            Ludicolo,
            Seedot,
            Nuzleaf,
            Shiftry,
            Taillow,
            Swellow,
            Wingull,
            Pelipper,
            Ralts,
            Kirlia,
            Gardevoir,
            Surskit,
            Masquerain,
            Shroomish,
            Breloom,
            Slakoth,
            Vigoroth,
            Slaking,
            Nincada,
            Ninjask,
            Shedinja,
            Whismur,
            Loudred,
            Exploud,
            Makuhita,
            Hariyama,
            Azurill,
            Nosepass,
            Skitty,
            Delcatty,
            Sableye,
            Mawile,
            Aron,
            Lairon,
            Aggron,
            Meditite,
            Medicham,
            Electrike,
            Manectric,
            Plusle,
            Minun,
            Volbeat,
            Illumise,
            Roselia,
            Gulpin,
            Swalot,
            Carvanha,
            Sharpedo,
            Wailmer,
            Wailord,
            Numel,
            Camerupt,
            Torkoal,
            Spoink,
            Grumpig,
            Spinda,
            Trapinch,
            Vibrava,
            Flygon,
            Cacnea,
            Cacturne,
            Swablu,
            Altaria,
            Zangoose,
            Seviper,
            Lunatone,
            Solrock,
            Barboach,
            Whiscash,
            Corphish,
            Crawdaunt,
            Baltoy,
            Claydol,
            Lileep,
            Cradily,
            Anorith,
            Armaldo,
            Feebas,
            Milotic,
            Castform,
            Kecleon,
            Shuppet,
            Banette,
            Duskull,
            Dusclops,
            Tropius,
            Chimecho,
            Absol,
            Wynaut,
            Snorunt,
            Glalie,
            Spheal,
            Sealeo,
            Walrein,
            Clamperl,
            Huntail,
            Gorebyss,
            Relicanth,
            Luvdisc,
            Bagon,
            Shelgon,
            Salamence,
            Beldum,
            Metang,
            Metagross,
            Regirock,
            Regice,
            Registeel,
            Latias,
            Latios,
            Kyogre,
            Groudon,
            Rayquaza,
            Jirachi,
            Deoxys,
            Turtwig,
            Grotle,
            Torterra,
            Chimchar,
            Monferno,
            Infernape,
            Piplup,
            Prinplup,
            Empoleon,
            Starly,
            Staravia,
            Staraptor,
            Bidoof,
            Bibarel,
            Kricketot,
            Kricketune,
            Shinx,
            Luxio,
            Luxray,
            Budew,
            Roserade,
            Cranidos,
            Rampardos,
            Shieldon,
            Bastiodon,
            Burmy,
            Wormadam,
            Mothim,
            Combee,
            Vespiquen,
            Pachirisu,
            Buizel,
            Floatzel,
            Cherubi,
            Cherrim,
            Shellos,
            Gastrodon,
            Ambipom,
            Drifloon,
            Drifblim,
            Buneary,
            Lopunny,
            Mismagius,
            Honchkrow,
            Glameow,
            Purugly,
            Chingling,
            Stunky,
            Skuntank,
            Bronzor,
            Bronzong,
            Bonsly,
            MimeJr,
            Happiny,
            Chatot,
            Spiritomb,
            Gible,
            Gabite,
            Garchomp,
            Munchlax,
            Riolu,
            Lucario,
            Hippopotas,
            Hippowdon,
            Skorupi,
            Drapion,
            Croagunk,
            Toxicroak,
            Carnivine,
            Finneon,
            Lumineon,
            Mantyke,
            Snover,
            Abomasnow,
            Weavile,
            Magnezone,
            Lickilicky,
            Rhyperior,
            Tangrowth,
            Electivire,
            Magmortar,
            Togekiss,
            Yanmega,
            Leafeon,
            Glaceon,
            Gliscor,
            Mamoswine,
            PorygonZ,
            Gallade,
            Probopass,
            Dusknoir,
            Froslass,
            Rotom,
            Uxie,
            Mesprit,
            Azelf,
            Dialga,
            Palkia,
            Heatran,
            Regigigas,
            Giratina,
            Cresselia,
            Phione,
            Manaphy,
            Darkrai,
            Shaymin,
            Arceus,
            Victini,
            Snivy,
            Servine,
            Serperior,
            Tepig,
            Pignite,
            Emboar,
            Oshawott,
            Dewott,
            Samurott,
            Patrat,
            Watchog,
            Lillipup,
            Herdier,
            Stoutland,
            Purrloin,
            Liepard,
            Pansage,
            Simisage,
            Pansear,
            Simisear,
            Panpour,
            Simipour,
            Munna,
            Musharna,
            Pidove,
            Tranquill,
            Unfezant,
            Blitzle,
            Zebstrika,
            Roggenrola,
            Boldore,
            Gigalith,
            Woobat,
            Swoobat,
            Drilbur,
            Excadrill,
            Audino,
            Timburr,
            Gurdurr,
            Conkeldurr,
            Tympole,
            Palpitoad,
            Seismitoad,
            Throh,
            Sawk,
            Sewaddle,
            Swadloon,
            Leavanny,
            Venipede,
            Whirlipede,
            Scolipede,
            Cottonee,
            Whimsicott,
            Petilil,
            Lilligant,
            Basculin,
            Sandile,
            Krokorok,
            Krookodile,
            Darumaka,
            Darmanitan,
            Maractus,
            Dwebble,
            Crustle,
            Scraggy,
            Scrafty,
            Sigilyph,
            Yamask,
            Cofagrigus,
            Tirtouga,
            Carracosta,
            Archen,
            Archeops,
            Trubbish,
            Garbodor,
            Zorua,
            Zoroark,
            Minccino,
            Cinccino,
            Gothita,
            Gothorita,
            Gothitelle,
            Solosis,
            Duosion,
            Reuniclus,
            Ducklett,
            Swanna,
            Vanillite,
            Vanillish,
            Vanilluxe,
            Deerling,
            Sawsbuck,
            Emolga,
            Karrablast,
            Escavalier,
            Foongus,
            Amoonguss,
            Frillish,
            Jellicent,
            Alomomola,
            Joltik,
            Galvantula,
            Ferroseed,
            Ferrothorn,
            Klink,
            Klang,
            Klinklang,
            Tynamo,
            Eelektrik,
            Eelektross,
            Elgyem,
            Beheeyem,
            Litwick,
            Lampent,
            Chandelure,
            Axew,
            Fraxure,
            Haxorus,
            Cubchoo,
            Beartic,
            Cryogonal,
            Shelmet,
            Accelgor,
            Stunfisk,
            Mienfoo,
            Mienshao,
            Druddigon,
            Golett,
            Golurk,
            Pawniard,
            Bisharp,
            Bouffalant,
            Rufflet,
            Braviary,
            Vullaby,
            Mandibuzz,
            Heatmor,
            Durant,
            Deino,
            Zweilous,
            Hydreigon,
            Larvesta,
            Volcarona,
            Cobalion,
            Terrakion,
            Virizion,
            Tornadus,
            Thundurus,
            Reshiram,
            Zekrom,
            Landorus,
            Kyurem,
            Keldeo,
            Meloetta,
            Genesect,
            Chespin,
            Quilladin,
            Chesnaught,
            Fennekin,
            Braixen,
            Delphox,
            Froakie,
            Frogadier,
            Greninja,
            Bunnelby,
            Diggersby,
            Fletchling,
            Fletchinder,
            Talonflame,
            Scatterbug,
            Spewpa,
            Vivillon,
            Litleo,
            Pyroar,
            Flabb,
            Floette,
            Florges,
            Skiddo,
            Gogoat,
            Pancham,
            Pangoro,
            Furfrou,
            Espurr,
            Meowstic,
            Honedge,
            Doublade,
            Aegislash,
            Spritzee,
            Aromatisse,
            Swirlix,
            Slurpuff,
            Inkay,
            Malamar,
            Binacle,
            Barbaracle,
            Skrelp,
            Dragalge,
            Clauncher,
            Clawitzer,
            Helioptile,
            Heliolisk,
            Tyrunt,
            Tyrantrum,
            Amaura,
            Aurorus,
            Sylveon,
            Hawlucha,
            Dedenne,
            Carbink,
            Goomy,
            Sliggoo,
            Goodra,
            Klefki,
            Phantump,
            Trevenant,
            Pumpkaboo,
            Gourgeist,
            Bergmite,
            Avalugg,
            Noibat,
            Noivern,
            Xerneas,
            Yveltal,
            Zygarde,
            Diancie,
            Hoopa,
            Volcanion,
        };
        
        public static SpeciesType GetValueFrom( int id ) => staticValues.SingleOrDefault( v => v.Id == id );
        public static IEnumerable<SpeciesType> AllSpecies => staticValues.AsEnumerable();
    }
}
