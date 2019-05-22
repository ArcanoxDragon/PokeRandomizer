// ReSharper disable All

using System.Collections.Generic;
using System.Linq;

namespace CtrDotNet.Pokemon.Data
{
public sealed class Ability : BaseAbility {
    public Ability( int id, string name ) : base( id, name ) { }
    public static explicit operator Ability( int id ) => Abilities.GetValueFrom( id );
    public static explicit operator int( Ability val ) => val.Id;
}
public static partial class Abilities {
public static Ability Stench = new Ability( 1, "Stench" );
public static Ability Drizzle = new Ability( 2, "Drizzle" );
public static Ability SpeedBoost = new Ability( 3, "Speed Boost" );
public static Ability BattleArmor = new Ability( 4, "Battle Armor" );
public static Ability Sturdy = new Ability( 5, "Sturdy" );
public static Ability Damp = new Ability( 6, "Damp" );
public static Ability Limber = new Ability( 7, "Limber" );
public static Ability SandVeil = new Ability( 8, "Sand Veil" );
public static Ability Static = new Ability( 9, "Static" );
public static Ability VoltAbsorb = new Ability( 10, "Volt Absorb" );
public static Ability WaterAbsorb = new Ability( 11, "Water Absorb" );
public static Ability Oblivious = new Ability( 12, "Oblivious" );
public static Ability CloudNine = new Ability( 13, "Cloud Nine" );
public static Ability CompoundEyes = new Ability( 14, "Compound Eyes" );
public static Ability Insomnia = new Ability( 15, "Insomnia" );
public static Ability ColorChange = new Ability( 16, "Color Change" );
public static Ability Immunity = new Ability( 17, "Immunity" );
public static Ability FlashFire = new Ability( 18, "Flash Fire" );
public static Ability ShieldDust = new Ability( 19, "Shield Dust" );
public static Ability OwnTempo = new Ability( 20, "Own Tempo" );
public static Ability SuctionCups = new Ability( 21, "Suction Cups" );
public static Ability Intimidate = new Ability( 22, "Intimidate" );
public static Ability ShadowTag = new Ability( 23, "Shadow Tag" );
public static Ability RoughSkin = new Ability( 24, "Rough Skin" );
public static Ability WonderGuard = new Ability( 25, "Wonder Guard" );
public static Ability Levitate = new Ability( 26, "Levitate" );
public static Ability EffectSpore = new Ability( 27, "Effect Spore" );
public static Ability Synchronize = new Ability( 28, "Synchronize" );
public static Ability ClearBody = new Ability( 29, "Clear Body" );
public static Ability NaturalCure = new Ability( 30, "Natural Cure" );
public static Ability LightningRod = new Ability( 31, "Lightning Rod" );
public static Ability SereneGrace = new Ability( 32, "Serene Grace" );
public static Ability SwiftSwim = new Ability( 33, "Swift Swim" );
public static Ability Chlorophyll = new Ability( 34, "Chlorophyll" );
public static Ability Illuminate = new Ability( 35, "Illuminate" );
public static Ability Trace = new Ability( 36, "Trace" );
public static Ability HugePower = new Ability( 37, "Huge Power" );
public static Ability PoisonPoint = new Ability( 38, "Poison Point" );
public static Ability InnerFocus = new Ability( 39, "Inner Focus" );
public static Ability MagmaArmor = new Ability( 40, "Magma Armor" );
public static Ability WaterVeil = new Ability( 41, "Water Veil" );
public static Ability MagnetPull = new Ability( 42, "Magnet Pull" );
public static Ability Soundproof = new Ability( 43, "Soundproof" );
public static Ability RainDish = new Ability( 44, "Rain Dish" );
public static Ability SandStream = new Ability( 45, "Sand Stream" );
public static Ability Pressure = new Ability( 46, "Pressure" );
public static Ability ThickFat = new Ability( 47, "Thick Fat" );
public static Ability EarlyBird = new Ability( 48, "Early Bird" );
public static Ability FlameBody = new Ability( 49, "Flame Body" );
public static Ability RunAway = new Ability( 50, "Run Away" );
public static Ability KeenEye = new Ability( 51, "Keen Eye" );
public static Ability HyperCutter = new Ability( 52, "Hyper Cutter" );
public static Ability Pickup = new Ability( 53, "Pickup" );
public static Ability Truant = new Ability( 54, "Truant" );
public static Ability Hustle = new Ability( 55, "Hustle" );
public static Ability CuteCharm = new Ability( 56, "Cute Charm" );
public static Ability Plus = new Ability( 57, "Plus" );
public static Ability Minus = new Ability( 58, "Minus" );
public static Ability Forecast = new Ability( 59, "Forecast" );
public static Ability StickyHold = new Ability( 60, "Sticky Hold" );
public static Ability ShedSkin = new Ability( 61, "Shed Skin" );
public static Ability Guts = new Ability( 62, "Guts" );
public static Ability MarvelScale = new Ability( 63, "Marvel Scale" );
public static Ability LiquidOoze = new Ability( 64, "Liquid Ooze" );
public static Ability Overgrow = new Ability( 65, "Overgrow" );
public static Ability Blaze = new Ability( 66, "Blaze" );
public static Ability Torrent = new Ability( 67, "Torrent" );
public static Ability Swarm = new Ability( 68, "Swarm" );
public static Ability RockHead = new Ability( 69, "Rock Head" );
public static Ability Drought = new Ability( 70, "Drought" );
public static Ability ArenaTrap = new Ability( 71, "Arena Trap" );
public static Ability VitalSpirit = new Ability( 72, "Vital Spirit" );
public static Ability WhiteSmoke = new Ability( 73, "White Smoke" );
public static Ability PurePower = new Ability( 74, "Pure Power" );
public static Ability ShellArmor = new Ability( 75, "Shell Armor" );
public static Ability AirLock = new Ability( 76, "Air Lock" );
public static Ability TangledFeet = new Ability( 77, "Tangled Feet" );
public static Ability MotorDrive = new Ability( 78, "Motor Drive" );
public static Ability Rivalry = new Ability( 79, "Rivalry" );
public static Ability Steadfast = new Ability( 80, "Steadfast" );
public static Ability SnowCloak = new Ability( 81, "Snow Cloak" );
public static Ability Gluttony = new Ability( 82, "Gluttony" );
public static Ability AngerPoint = new Ability( 83, "Anger Point" );
public static Ability Unburden = new Ability( 84, "Unburden" );
public static Ability Heatproof = new Ability( 85, "Heatproof" );
public static Ability Simple = new Ability( 86, "Simple" );
public static Ability DrySkin = new Ability( 87, "Dry Skin" );
public static Ability Download = new Ability( 88, "Download" );
public static Ability IronFist = new Ability( 89, "Iron Fist" );
public static Ability PoisonHeal = new Ability( 90, "Poison Heal" );
public static Ability Adaptability = new Ability( 91, "Adaptability" );
public static Ability SkillLink = new Ability( 92, "Skill Link" );
public static Ability Hydration = new Ability( 93, "Hydration" );
public static Ability SolarPower = new Ability( 94, "Solar Power" );
public static Ability QuickFeet = new Ability( 95, "Quick Feet" );
public static Ability Normalize = new Ability( 96, "Normalize" );
public static Ability Sniper = new Ability( 97, "Sniper" );
public static Ability MagicGuard = new Ability( 98, "Magic Guard" );
public static Ability NoGuard = new Ability( 99, "No Guard" );
public static Ability Stall = new Ability( 100, "Stall" );
public static Ability Technician = new Ability( 101, "Technician" );
public static Ability LeafGuard = new Ability( 102, "Leaf Guard" );
public static Ability Klutz = new Ability( 103, "Klutz" );
public static Ability MoldBreaker = new Ability( 104, "Mold Breaker" );
public static Ability SuperLuck = new Ability( 105, "Super Luck" );
public static Ability Aftermath = new Ability( 106, "Aftermath" );
public static Ability Anticipation = new Ability( 107, "Anticipation" );
public static Ability Forewarn = new Ability( 108, "Forewarn" );
public static Ability Unaware = new Ability( 109, "Unaware" );
public static Ability TintedLens = new Ability( 110, "Tinted Lens" );
public static Ability Filter = new Ability( 111, "Filter" );
public static Ability SlowStart = new Ability( 112, "Slow Start" );
public static Ability Scrappy = new Ability( 113, "Scrappy" );
public static Ability StormDrain = new Ability( 114, "Storm Drain" );
public static Ability IceBody = new Ability( 115, "Ice Body" );
public static Ability SolidRock = new Ability( 116, "Solid Rock" );
public static Ability SnowWarning = new Ability( 117, "Snow Warning" );
public static Ability HoneyGather = new Ability( 118, "Honey Gather" );
public static Ability Frisk = new Ability( 119, "Frisk" );
public static Ability Reckless = new Ability( 120, "Reckless" );
public static Ability Multitype = new Ability( 121, "Multitype" );
public static Ability FlowerGift = new Ability( 122, "Flower Gift" );
public static Ability BadDreams = new Ability( 123, "Bad Dreams" );
public static Ability Pickpocket = new Ability( 124, "Pickpocket" );
public static Ability SheerForce = new Ability( 125, "Sheer Force" );
public static Ability Contrary = new Ability( 126, "Contrary" );
public static Ability Unnerve = new Ability( 127, "Unnerve" );
public static Ability Defiant = new Ability( 128, "Defiant" );
public static Ability Defeatist = new Ability( 129, "Defeatist" );
public static Ability CursedBody = new Ability( 130, "Cursed Body" );
public static Ability Healer = new Ability( 131, "Healer" );
public static Ability FriendGuard = new Ability( 132, "Friend Guard" );
public static Ability WeakArmor = new Ability( 133, "Weak Armor" );
public static Ability HeavyMetal = new Ability( 134, "Heavy Metal" );
public static Ability LightMetal = new Ability( 135, "Light Metal" );
public static Ability Multiscale = new Ability( 136, "Multiscale" );
public static Ability ToxicBoost = new Ability( 137, "Toxic Boost" );
public static Ability FlareBoost = new Ability( 138, "Flare Boost" );
public static Ability Harvest = new Ability( 139, "Harvest" );
public static Ability Telepathy = new Ability( 140, "Telepathy" );
public static Ability Moody = new Ability( 141, "Moody" );
public static Ability Overcoat = new Ability( 142, "Overcoat" );
public static Ability PoisonTouch = new Ability( 143, "Poison Touch" );
public static Ability Regenerator = new Ability( 144, "Regenerator" );
public static Ability BigPecks = new Ability( 145, "Big Pecks" );
public static Ability SandRush = new Ability( 146, "Sand Rush" );
public static Ability WonderSkin = new Ability( 147, "Wonder Skin" );
public static Ability Analytic = new Ability( 148, "Analytic" );
public static Ability Illusion = new Ability( 149, "Illusion" );
public static Ability Imposter = new Ability( 150, "Imposter" );
public static Ability Infiltrator = new Ability( 151, "Infiltrator" );
public static Ability Mummy = new Ability( 152, "Mummy" );
public static Ability Moxie = new Ability( 153, "Moxie" );
public static Ability Justified = new Ability( 154, "Justified" );
public static Ability Rattled = new Ability( 155, "Rattled" );
public static Ability MagicBounce = new Ability( 156, "Magic Bounce" );
public static Ability SapSipper = new Ability( 157, "Sap Sipper" );
public static Ability Prankster = new Ability( 158, "Prankster" );
public static Ability SandForce = new Ability( 159, "Sand Force" );
public static Ability IronBarbs = new Ability( 160, "Iron Barbs" );
public static Ability ZenMode = new Ability( 161, "Zen Mode" );
public static Ability VictoryStar = new Ability( 162, "Victory Star" );
public static Ability Turboblaze = new Ability( 163, "Turboblaze" );
public static Ability Teravolt = new Ability( 164, "Teravolt" );
public static Ability AromaVeil = new Ability( 165, "Aroma Veil" );
public static Ability FlowerVeil = new Ability( 166, "Flower Veil" );
public static Ability CheekPouch = new Ability( 167, "Cheek Pouch" );
public static Ability Protean = new Ability( 168, "Protean" );
public static Ability FurCoat = new Ability( 169, "Fur Coat" );
public static Ability Magician = new Ability( 170, "Magician" );
public static Ability Bulletproof = new Ability( 171, "Bulletproof" );
public static Ability Competitive = new Ability( 172, "Competitive" );
public static Ability StrongJaw = new Ability( 173, "Strong Jaw" );
public static Ability Refrigerate = new Ability( 174, "Refrigerate" );
public static Ability SweetVeil = new Ability( 175, "Sweet Veil" );
public static Ability StanceChange = new Ability( 176, "Stance Change" );
public static Ability GaleWings = new Ability( 177, "Gale Wings" );
public static Ability MegaLauncher = new Ability( 178, "Mega Launcher" );
public static Ability GrassPelt = new Ability( 179, "Grass Pelt" );
public static Ability Symbiosis = new Ability( 180, "Symbiosis" );
public static Ability ToughClaws = new Ability( 181, "Tough Claws" );
public static Ability Pixilate = new Ability( 182, "Pixilate" );
public static Ability Gooey = new Ability( 183, "Gooey" );
public static Ability Aerilate = new Ability( 184, "Aerilate" );
public static Ability ParentalBond = new Ability( 185, "Parental Bond" );
public static Ability DarkAura = new Ability( 186, "Dark Aura" );
public static Ability FairyAura = new Ability( 187, "Fairy Aura" );
public static Ability AuraBreak = new Ability( 188, "Aura Break" );
public static Ability PrimordialSea = new Ability( 189, "Primordial Sea" );
public static Ability DesolateLand = new Ability( 190, "Desolate Land" );
public static Ability DeltaStream = new Ability( 191, "Delta Stream" );
public static Ability GetValueFrom( int id ) => staticValues[ id ];
private static readonly Ability[] staticValues = { null,
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
	DeltaStream };
public static IEnumerable<Ability> AllAbilities => staticValues.AsEnumerable();
}

public sealed class Item : BaseItem {
    public Item( int id, string name ) : base( id, name ) { }
    public static explicit operator Item( int id ) => Items.GetValueFrom( id );
    public static explicit operator int( Item val ) => val.Id;
}
public static partial class Items {
public static Item MasterBall = new Item( 1, "Master Ball" );
public static Item UltraBall = new Item( 2, "Ultra Ball" );
public static Item GreatBall = new Item( 3, "Great Ball" );
public static Item PokBall = new Item( 4, "Poké Ball" );
public static Item SafariBall = new Item( 5, "Safari Ball" );
public static Item NetBall = new Item( 6, "Net Ball" );
public static Item DiveBall = new Item( 7, "Dive Ball" );
public static Item NestBall = new Item( 8, "Nest Ball" );
public static Item RepeatBall = new Item( 9, "Repeat Ball" );
public static Item TimerBall = new Item( 10, "Timer Ball" );
public static Item LuxuryBall = new Item( 11, "Luxury Ball" );
public static Item PremierBall = new Item( 12, "Premier Ball" );
public static Item DuskBall = new Item( 13, "Dusk Ball" );
public static Item HealBall = new Item( 14, "Heal Ball" );
public static Item QuickBall = new Item( 15, "Quick Ball" );
public static Item CherishBall = new Item( 16, "Cherish Ball" );
public static Item Potion = new Item( 17, "Potion" );
public static Item Antidote = new Item( 18, "Antidote" );
public static Item BurnHeal = new Item( 19, "Burn Heal" );
public static Item IceHeal = new Item( 20, "Ice Heal" );
public static Item Awakening = new Item( 21, "Awakening" );
public static Item ParalyzeHeal = new Item( 22, "Paralyze Heal" );
public static Item FullRestore = new Item( 23, "Full Restore" );
public static Item MaxPotion = new Item( 24, "Max Potion" );
public static Item HyperPotion = new Item( 25, "Hyper Potion" );
public static Item SuperPotion = new Item( 26, "Super Potion" );
public static Item FullHeal = new Item( 27, "Full Heal" );
public static Item Revive = new Item( 28, "Revive" );
public static Item MaxRevive = new Item( 29, "Max Revive" );
public static Item FreshWater = new Item( 30, "Fresh Water" );
public static Item SodaPop = new Item( 31, "Soda Pop" );
public static Item Lemonade = new Item( 32, "Lemonade" );
public static Item MoomooMilk = new Item( 33, "Moomoo Milk" );
public static Item EnergyPowder = new Item( 34, "Energy Powder" );
public static Item EnergyRoot = new Item( 35, "Energy Root" );
public static Item HealPowder = new Item( 36, "Heal Powder" );
public static Item RevivalHerb = new Item( 37, "Revival Herb" );
public static Item Ether = new Item( 38, "Ether" );
public static Item MaxEther = new Item( 39, "Max Ether" );
public static Item Elixir = new Item( 40, "Elixir" );
public static Item MaxElixir = new Item( 41, "Max Elixir" );
public static Item LavaCookie = new Item( 42, "Lava Cookie" );
public static Item BerryJuice = new Item( 43, "Berry Juice" );
public static Item SacredAsh = new Item( 44, "Sacred Ash" );
public static Item HPUp = new Item( 45, "HP Up" );
public static Item Protein = new Item( 46, "Protein" );
public static Item Iron = new Item( 47, "Iron" );
public static Item Carbos = new Item( 48, "Carbos" );
public static Item Calcium = new Item( 49, "Calcium" );
public static Item RareCandy = new Item( 50, "Rare Candy" );
public static Item PPUp = new Item( 51, "PP Up" );
public static Item Zinc = new Item( 52, "Zinc" );
public static Item PPMax = new Item( 53, "PP Max" );
public static Item OldGateau = new Item( 54, "Old Gateau" );
public static Item GuardSpec = new Item( 55, "Guard Spec." );
public static Item DireHit = new Item( 56, "Dire Hit" );
public static Item XAttack = new Item( 57, "X Attack" );
public static Item XDefense = new Item( 58, "X Defense" );
public static Item XSpeed = new Item( 59, "X Speed" );
public static Item XAccuracy = new Item( 60, "X Accuracy" );
public static Item XSpAtk = new Item( 61, "X Sp. Atk" );
public static Item XSpDef = new Item( 62, "X Sp. Def" );
public static Item PokDoll = new Item( 63, "Poké Doll" );
public static Item FluffyTail = new Item( 64, "Fluffy Tail" );
public static Item BlueFlute = new Item( 65, "Blue Flute" );
public static Item YellowFlute = new Item( 66, "Yellow Flute" );
public static Item RedFlute = new Item( 67, "Red Flute" );
public static Item BlackFlute = new Item( 68, "Black Flute" );
public static Item WhiteFlute = new Item( 69, "White Flute" );
public static Item ShoalSalt = new Item( 70, "Shoal Salt" );
public static Item ShoalShell = new Item( 71, "Shoal Shell" );
public static Item RedShard = new Item( 72, "Red Shard" );
public static Item BlueShard = new Item( 73, "Blue Shard" );
public static Item YellowShard = new Item( 74, "Yellow Shard" );
public static Item GreenShard = new Item( 75, "Green Shard" );
public static Item SuperRepel = new Item( 76, "Super Repel" );
public static Item MaxRepel = new Item( 77, "Max Repel" );
public static Item EscapeRope = new Item( 78, "Escape Rope" );
public static Item Repel = new Item( 79, "Repel" );
public static Item SunStone = new Item( 80, "Sun Stone" );
public static Item MoonStone = new Item( 81, "Moon Stone" );
public static Item FireStone = new Item( 82, "Fire Stone" );
public static Item ThunderStone = new Item( 83, "Thunder Stone" );
public static Item WaterStone = new Item( 84, "Water Stone" );
public static Item LeafStone = new Item( 85, "Leaf Stone" );
public static Item TinyMushroom = new Item( 86, "Tiny Mushroom" );
public static Item BigMushroom = new Item( 87, "Big Mushroom" );
public static Item Pearl = new Item( 88, "Pearl" );
public static Item BigPearl = new Item( 89, "Big Pearl" );
public static Item Stardust = new Item( 90, "Stardust" );
public static Item StarPiece = new Item( 91, "Star Piece" );
public static Item Nugget = new Item( 92, "Nugget" );
public static Item HeartScale = new Item( 93, "Heart Scale" );
public static Item Honey = new Item( 94, "Honey" );
public static Item GrowthMulch = new Item( 95, "Growth Mulch" );
public static Item DampMulch = new Item( 96, "Damp Mulch" );
public static Item StableMulch = new Item( 97, "Stable Mulch" );
public static Item GooeyMulch = new Item( 98, "Gooey Mulch" );
public static Item RootFossil = new Item( 99, "Root Fossil" );
public static Item ClawFossil = new Item( 100, "Claw Fossil" );
public static Item HelixFossil = new Item( 101, "Helix Fossil" );
public static Item DomeFossil = new Item( 102, "Dome Fossil" );
public static Item OldAmber = new Item( 103, "Old Amber" );
public static Item ArmorFossil = new Item( 104, "Armor Fossil" );
public static Item SkullFossil = new Item( 105, "Skull Fossil" );
public static Item RareBone = new Item( 106, "Rare Bone" );
public static Item ShinyStone = new Item( 107, "Shiny Stone" );
public static Item DuskStone = new Item( 108, "Dusk Stone" );
public static Item DawnStone = new Item( 109, "Dawn Stone" );
public static Item OvalStone = new Item( 110, "Oval Stone" );
public static Item OddKeystone = new Item( 111, "Odd Keystone" );
public static Item GriseousOrb = new Item( 112, "Griseous Orb" );
public static Item DouseDrive = new Item( 116, "Douse Drive" );
public static Item ShockDrive = new Item( 117, "Shock Drive" );
public static Item BurnDrive = new Item( 118, "Burn Drive" );
public static Item ChillDrive = new Item( 119, "Chill Drive" );
public static Item SweetHeart = new Item( 134, "Sweet Heart" );
public static Item AdamantOrb = new Item( 135, "Adamant Orb" );
public static Item LustrousOrb = new Item( 136, "Lustrous Orb" );
public static Item GreetMail = new Item( 137, "Greet Mail" );
public static Item FavoredMail = new Item( 138, "Favored Mail" );
public static Item RSVPMail = new Item( 139, "RSVP Mail" );
public static Item ThanksMail = new Item( 140, "Thanks Mail" );
public static Item InquiryMail = new Item( 141, "Inquiry Mail" );
public static Item LikeMail = new Item( 142, "Like Mail" );
public static Item ReplyMail = new Item( 143, "Reply Mail" );
public static Item BridgeMailS = new Item( 144, "Bridge Mail S" );
public static Item BridgeMailD = new Item( 145, "Bridge Mail D" );
public static Item BridgeMailT = new Item( 146, "Bridge Mail T" );
public static Item BridgeMailV = new Item( 147, "Bridge Mail V" );
public static Item BridgeMailM = new Item( 148, "Bridge Mail M" );
public static Item CheriBerry = new Item( 149, "Cheri Berry" );
public static Item ChestoBerry = new Item( 150, "Chesto Berry" );
public static Item PechaBerry = new Item( 151, "Pecha Berry" );
public static Item RawstBerry = new Item( 152, "Rawst Berry" );
public static Item AspearBerry = new Item( 153, "Aspear Berry" );
public static Item LeppaBerry = new Item( 154, "Leppa Berry" );
public static Item OranBerry = new Item( 155, "Oran Berry" );
public static Item PersimBerry = new Item( 156, "Persim Berry" );
public static Item LumBerry = new Item( 157, "Lum Berry" );
public static Item SitrusBerry = new Item( 158, "Sitrus Berry" );
public static Item FigyBerry = new Item( 159, "Figy Berry" );
public static Item WikiBerry = new Item( 160, "Wiki Berry" );
public static Item MagoBerry = new Item( 161, "Mago Berry" );
public static Item AguavBerry = new Item( 162, "Aguav Berry" );
public static Item IapapaBerry = new Item( 163, "Iapapa Berry" );
public static Item RazzBerry = new Item( 164, "Razz Berry" );
public static Item BlukBerry = new Item( 165, "Bluk Berry" );
public static Item NanabBerry = new Item( 166, "Nanab Berry" );
public static Item WepearBerry = new Item( 167, "Wepear Berry" );
public static Item PinapBerry = new Item( 168, "Pinap Berry" );
public static Item PomegBerry = new Item( 169, "Pomeg Berry" );
public static Item KelpsyBerry = new Item( 170, "Kelpsy Berry" );
public static Item QualotBerry = new Item( 171, "Qualot Berry" );
public static Item HondewBerry = new Item( 172, "Hondew Berry" );
public static Item GrepaBerry = new Item( 173, "Grepa Berry" );
public static Item TamatoBerry = new Item( 174, "Tamato Berry" );
public static Item CornnBerry = new Item( 175, "Cornn Berry" );
public static Item MagostBerry = new Item( 176, "Magost Berry" );
public static Item RabutaBerry = new Item( 177, "Rabuta Berry" );
public static Item NomelBerry = new Item( 178, "Nomel Berry" );
public static Item SpelonBerry = new Item( 179, "Spelon Berry" );
public static Item PamtreBerry = new Item( 180, "Pamtre Berry" );
public static Item WatmelBerry = new Item( 181, "Watmel Berry" );
public static Item DurinBerry = new Item( 182, "Durin Berry" );
public static Item BelueBerry = new Item( 183, "Belue Berry" );
public static Item OccaBerry = new Item( 184, "Occa Berry" );
public static Item PasshoBerry = new Item( 185, "Passho Berry" );
public static Item WacanBerry = new Item( 186, "Wacan Berry" );
public static Item RindoBerry = new Item( 187, "Rindo Berry" );
public static Item YacheBerry = new Item( 188, "Yache Berry" );
public static Item ChopleBerry = new Item( 189, "Chople Berry" );
public static Item KebiaBerry = new Item( 190, "Kebia Berry" );
public static Item ShucaBerry = new Item( 191, "Shuca Berry" );
public static Item CobaBerry = new Item( 192, "Coba Berry" );
public static Item PayapaBerry = new Item( 193, "Payapa Berry" );
public static Item TangaBerry = new Item( 194, "Tanga Berry" );
public static Item ChartiBerry = new Item( 195, "Charti Berry" );
public static Item KasibBerry = new Item( 196, "Kasib Berry" );
public static Item HabanBerry = new Item( 197, "Haban Berry" );
public static Item ColburBerry = new Item( 198, "Colbur Berry" );
public static Item BabiriBerry = new Item( 199, "Babiri Berry" );
public static Item ChilanBerry = new Item( 200, "Chilan Berry" );
public static Item LiechiBerry = new Item( 201, "Liechi Berry" );
public static Item GanlonBerry = new Item( 202, "Ganlon Berry" );
public static Item SalacBerry = new Item( 203, "Salac Berry" );
public static Item PetayaBerry = new Item( 204, "Petaya Berry" );
public static Item ApicotBerry = new Item( 205, "Apicot Berry" );
public static Item LansatBerry = new Item( 206, "Lansat Berry" );
public static Item StarfBerry = new Item( 207, "Starf Berry" );
public static Item EnigmaBerry = new Item( 208, "Enigma Berry" );
public static Item MicleBerry = new Item( 209, "Micle Berry" );
public static Item CustapBerry = new Item( 210, "Custap Berry" );
public static Item JabocaBerry = new Item( 211, "Jaboca Berry" );
public static Item RowapBerry = new Item( 212, "Rowap Berry" );
public static Item BrightPowder = new Item( 213, "Bright Powder" );
public static Item WhiteHerb = new Item( 214, "White Herb" );
public static Item MachoBrace = new Item( 215, "Macho Brace" );
public static Item ExpShare = new Item( 216, "Exp. Share" );
public static Item QuickClaw = new Item( 217, "Quick Claw" );
public static Item SootheBell = new Item( 218, "Soothe Bell" );
public static Item MentalHerb = new Item( 219, "Mental Herb" );
public static Item ChoiceBand = new Item( 220, "Choice Band" );
public static Item KingsRock = new Item( 221, "King’s Rock" );
public static Item SilverPowder = new Item( 222, "Silver Powder" );
public static Item AmuletCoin = new Item( 223, "Amulet Coin" );
public static Item CleanseTag = new Item( 224, "Cleanse Tag" );
public static Item SoulDew = new Item( 225, "Soul Dew" );
public static Item DeepSeaTooth = new Item( 226, "Deep Sea Tooth" );
public static Item DeepSeaScale = new Item( 227, "Deep Sea Scale" );
public static Item SmokeBall = new Item( 228, "Smoke Ball" );
public static Item Everstone = new Item( 229, "Everstone" );
public static Item FocusBand = new Item( 230, "Focus Band" );
public static Item LuckyEgg = new Item( 231, "Lucky Egg" );
public static Item ScopeLens = new Item( 232, "Scope Lens" );
public static Item MetalCoat = new Item( 233, "Metal Coat" );
public static Item Leftovers = new Item( 234, "Leftovers" );
public static Item DragonScale = new Item( 235, "Dragon Scale" );
public static Item LightBall = new Item( 236, "Light Ball" );
public static Item SoftSand = new Item( 237, "Soft Sand" );
public static Item HardStone = new Item( 238, "Hard Stone" );
public static Item MiracleSeed = new Item( 239, "Miracle Seed" );
public static Item BlackGlasses = new Item( 240, "Black Glasses" );
public static Item BlackBelt = new Item( 241, "Black Belt" );
public static Item Magnet = new Item( 242, "Magnet" );
public static Item MysticWater = new Item( 243, "Mystic Water" );
public static Item SharpBeak = new Item( 244, "Sharp Beak" );
public static Item PoisonBarb = new Item( 245, "Poison Barb" );
public static Item NeverMeltIce = new Item( 246, "Never-Melt Ice" );
public static Item SpellTag = new Item( 247, "Spell Tag" );
public static Item TwistedSpoon = new Item( 248, "Twisted Spoon" );
public static Item Charcoal = new Item( 249, "Charcoal" );
public static Item DragonFang = new Item( 250, "Dragon Fang" );
public static Item SilkScarf = new Item( 251, "Silk Scarf" );
public static Item UpGrade = new Item( 252, "Up-Grade" );
public static Item ShellBell = new Item( 253, "Shell Bell" );
public static Item SeaIncense = new Item( 254, "Sea Incense" );
public static Item LaxIncense = new Item( 255, "Lax Incense" );
public static Item LuckyPunch = new Item( 256, "Lucky Punch" );
public static Item MetalPowder = new Item( 257, "Metal Powder" );
public static Item ThickClub = new Item( 258, "Thick Club" );
public static Item Stick = new Item( 259, "Stick" );
public static Item RedScarf = new Item( 260, "Red Scarf" );
public static Item BlueScarf = new Item( 261, "Blue Scarf" );
public static Item PinkScarf = new Item( 262, "Pink Scarf" );
public static Item GreenScarf = new Item( 263, "Green Scarf" );
public static Item YellowScarf = new Item( 264, "Yellow Scarf" );
public static Item WideLens = new Item( 265, "Wide Lens" );
public static Item MuscleBand = new Item( 266, "Muscle Band" );
public static Item WiseGlasses = new Item( 267, "Wise Glasses" );
public static Item ExpertBelt = new Item( 268, "Expert Belt" );
public static Item LightClay = new Item( 269, "Light Clay" );
public static Item LifeOrb = new Item( 270, "Life Orb" );
public static Item PowerHerb = new Item( 271, "Power Herb" );
public static Item ToxicOrb = new Item( 272, "Toxic Orb" );
public static Item FlameOrb = new Item( 273, "Flame Orb" );
public static Item QuickPowder = new Item( 274, "Quick Powder" );
public static Item FocusSash = new Item( 275, "Focus Sash" );
public static Item ZoomLens = new Item( 276, "Zoom Lens" );
public static Item Metronome = new Item( 277, "Metronome" );
public static Item IronBall = new Item( 278, "Iron Ball" );
public static Item LaggingTail = new Item( 279, "Lagging Tail" );
public static Item DestinyKnot = new Item( 280, "Destiny Knot" );
public static Item BlackSludge = new Item( 281, "Black Sludge" );
public static Item IcyRock = new Item( 282, "Icy Rock" );
public static Item SmoothRock = new Item( 283, "Smooth Rock" );
public static Item HeatRock = new Item( 284, "Heat Rock" );
public static Item DampRock = new Item( 285, "Damp Rock" );
public static Item GripClaw = new Item( 286, "Grip Claw" );
public static Item ChoiceScarf = new Item( 287, "Choice Scarf" );
public static Item StickyBarb = new Item( 288, "Sticky Barb" );
public static Item PowerBracer = new Item( 289, "Power Bracer" );
public static Item PowerBelt = new Item( 290, "Power Belt" );
public static Item PowerLens = new Item( 291, "Power Lens" );
public static Item PowerBand = new Item( 292, "Power Band" );
public static Item PowerAnklet = new Item( 293, "Power Anklet" );
public static Item PowerWeight = new Item( 294, "Power Weight" );
public static Item ShedShell = new Item( 295, "Shed Shell" );
public static Item BigRoot = new Item( 296, "Big Root" );
public static Item ChoiceSpecs = new Item( 297, "Choice Specs" );
public static Item FlamePlate = new Item( 298, "Flame Plate" );
public static Item SplashPlate = new Item( 299, "Splash Plate" );
public static Item ZapPlate = new Item( 300, "Zap Plate" );
public static Item MeadowPlate = new Item( 301, "Meadow Plate" );
public static Item IciclePlate = new Item( 302, "Icicle Plate" );
public static Item FistPlate = new Item( 303, "Fist Plate" );
public static Item ToxicPlate = new Item( 304, "Toxic Plate" );
public static Item EarthPlate = new Item( 305, "Earth Plate" );
public static Item SkyPlate = new Item( 306, "Sky Plate" );
public static Item MindPlate = new Item( 307, "Mind Plate" );
public static Item InsectPlate = new Item( 308, "Insect Plate" );
public static Item StonePlate = new Item( 309, "Stone Plate" );
public static Item SpookyPlate = new Item( 310, "Spooky Plate" );
public static Item DracoPlate = new Item( 311, "Draco Plate" );
public static Item DreadPlate = new Item( 312, "Dread Plate" );
public static Item IronPlate = new Item( 313, "Iron Plate" );
public static Item OddIncense = new Item( 314, "Odd Incense" );
public static Item RockIncense = new Item( 315, "Rock Incense" );
public static Item FullIncense = new Item( 316, "Full Incense" );
public static Item WaveIncense = new Item( 317, "Wave Incense" );
public static Item RoseIncense = new Item( 318, "Rose Incense" );
public static Item LuckIncense = new Item( 319, "Luck Incense" );
public static Item PureIncense = new Item( 320, "Pure Incense" );
public static Item Protector = new Item( 321, "Protector" );
public static Item Electirizer = new Item( 322, "Electirizer" );
public static Item Magmarizer = new Item( 323, "Magmarizer" );
public static Item DubiousDisc = new Item( 324, "Dubious Disc" );
public static Item ReaperCloth = new Item( 325, "Reaper Cloth" );
public static Item RazorClaw = new Item( 326, "Razor Claw" );
public static Item RazorFang = new Item( 327, "Razor Fang" );
public static Item TM01 = new Item( 328, "TM01" );
public static Item TM02 = new Item( 329, "TM02" );
public static Item TM03 = new Item( 330, "TM03" );
public static Item TM04 = new Item( 331, "TM04" );
public static Item TM05 = new Item( 332, "TM05" );
public static Item TM06 = new Item( 333, "TM06" );
public static Item TM07 = new Item( 334, "TM07" );
public static Item TM08 = new Item( 335, "TM08" );
public static Item TM09 = new Item( 336, "TM09" );
public static Item TM10 = new Item( 337, "TM10" );
public static Item TM11 = new Item( 338, "TM11" );
public static Item TM12 = new Item( 339, "TM12" );
public static Item TM13 = new Item( 340, "TM13" );
public static Item TM14 = new Item( 341, "TM14" );
public static Item TM15 = new Item( 342, "TM15" );
public static Item TM16 = new Item( 343, "TM16" );
public static Item TM17 = new Item( 344, "TM17" );
public static Item TM18 = new Item( 345, "TM18" );
public static Item TM19 = new Item( 346, "TM19" );
public static Item TM20 = new Item( 347, "TM20" );
public static Item TM21 = new Item( 348, "TM21" );
public static Item TM22 = new Item( 349, "TM22" );
public static Item TM23 = new Item( 350, "TM23" );
public static Item TM24 = new Item( 351, "TM24" );
public static Item TM25 = new Item( 352, "TM25" );
public static Item TM26 = new Item( 353, "TM26" );
public static Item TM27 = new Item( 354, "TM27" );
public static Item TM28 = new Item( 355, "TM28" );
public static Item TM29 = new Item( 356, "TM29" );
public static Item TM30 = new Item( 357, "TM30" );
public static Item TM31 = new Item( 358, "TM31" );
public static Item TM32 = new Item( 359, "TM32" );
public static Item TM33 = new Item( 360, "TM33" );
public static Item TM34 = new Item( 361, "TM34" );
public static Item TM35 = new Item( 362, "TM35" );
public static Item TM36 = new Item( 363, "TM36" );
public static Item TM37 = new Item( 364, "TM37" );
public static Item TM38 = new Item( 365, "TM38" );
public static Item TM39 = new Item( 366, "TM39" );
public static Item TM40 = new Item( 367, "TM40" );
public static Item TM41 = new Item( 368, "TM41" );
public static Item TM42 = new Item( 369, "TM42" );
public static Item TM43 = new Item( 370, "TM43" );
public static Item TM44 = new Item( 371, "TM44" );
public static Item TM45 = new Item( 372, "TM45" );
public static Item TM46 = new Item( 373, "TM46" );
public static Item TM47 = new Item( 374, "TM47" );
public static Item TM48 = new Item( 375, "TM48" );
public static Item TM49 = new Item( 376, "TM49" );
public static Item TM50 = new Item( 377, "TM50" );
public static Item TM51 = new Item( 378, "TM51" );
public static Item TM52 = new Item( 379, "TM52" );
public static Item TM53 = new Item( 380, "TM53" );
public static Item TM54 = new Item( 381, "TM54" );
public static Item TM55 = new Item( 382, "TM55" );
public static Item TM56 = new Item( 383, "TM56" );
public static Item TM57 = new Item( 384, "TM57" );
public static Item TM58 = new Item( 385, "TM58" );
public static Item TM59 = new Item( 386, "TM59" );
public static Item TM60 = new Item( 387, "TM60" );
public static Item TM61 = new Item( 388, "TM61" );
public static Item TM62 = new Item( 389, "TM62" );
public static Item TM63 = new Item( 390, "TM63" );
public static Item TM64 = new Item( 391, "TM64" );
public static Item TM65 = new Item( 392, "TM65" );
public static Item TM66 = new Item( 393, "TM66" );
public static Item TM67 = new Item( 394, "TM67" );
public static Item TM68 = new Item( 395, "TM68" );
public static Item TM69 = new Item( 396, "TM69" );
public static Item TM70 = new Item( 397, "TM70" );
public static Item TM71 = new Item( 398, "TM71" );
public static Item TM72 = new Item( 399, "TM72" );
public static Item TM73 = new Item( 400, "TM73" );
public static Item TM74 = new Item( 401, "TM74" );
public static Item TM75 = new Item( 402, "TM75" );
public static Item TM76 = new Item( 403, "TM76" );
public static Item TM77 = new Item( 404, "TM77" );
public static Item TM78 = new Item( 405, "TM78" );
public static Item TM79 = new Item( 406, "TM79" );
public static Item TM80 = new Item( 407, "TM80" );
public static Item TM81 = new Item( 408, "TM81" );
public static Item TM82 = new Item( 409, "TM82" );
public static Item TM83 = new Item( 410, "TM83" );
public static Item TM84 = new Item( 411, "TM84" );
public static Item TM85 = new Item( 412, "TM85" );
public static Item TM86 = new Item( 413, "TM86" );
public static Item TM87 = new Item( 414, "TM87" );
public static Item TM88 = new Item( 415, "TM88" );
public static Item TM89 = new Item( 416, "TM89" );
public static Item TM90 = new Item( 417, "TM90" );
public static Item TM91 = new Item( 418, "TM91" );
public static Item TM92 = new Item( 419, "TM92" );
public static Item HM01 = new Item( 420, "HM01" );
public static Item HM02 = new Item( 421, "HM02" );
public static Item HM03 = new Item( 422, "HM03" );
public static Item HM04 = new Item( 423, "HM04" );
public static Item HM05 = new Item( 424, "HM05" );
public static Item HM06 = new Item( 425, "HM06" );
public static Item ExplorerKit = new Item( 428, "Explorer Kit" );
public static Item LootSack = new Item( 429, "Loot Sack" );
public static Item RuleBook = new Item( 430, "Rule Book" );
public static Item PokRadar = new Item( 431, "Poké Radar" );
public static Item PointCard = new Item( 432, "Point Card" );
public static Item Journal = new Item( 433, "Journal" );
public static Item SealCase = new Item( 434, "Seal Case" );
public static Item FashionCase = new Item( 435, "Fashion Case" );
public static Item SealBag = new Item( 436, "Seal Bag" );
public static Item PalPad = new Item( 437, "Pal Pad" );
public static Item WorksKey = new Item( 438, "Works Key" );
public static Item OldCharm = new Item( 439, "Old Charm" );
public static Item GalacticKey = new Item( 440, "Galactic Key" );
public static Item RedChain = new Item( 441, "Red Chain" );
public static Item TownMap = new Item( 442, "Town Map" );
public static Item VsSeeker = new Item( 443, "Vs. Seeker" );
public static Item CoinCase = new Item( 444, "Coin Case" );
public static Item OldRod = new Item( 445, "Old Rod" );
public static Item GoodRod = new Item( 446, "Good Rod" );
public static Item SuperRod = new Item( 447, "Super Rod" );
public static Item Sprayduck = new Item( 448, "Sprayduck" );
public static Item PoffinCase = new Item( 449, "Poffin Case" );
public static Item Bike = new Item( 450, "Bike" );
public static Item SuiteKey = new Item( 451, "Suite Key" );
public static Item OaksLetter = new Item( 452, "Oak’s Letter" );
public static Item LunarWing = new Item( 453, "Lunar Wing" );
public static Item MemberCard = new Item( 454, "Member Card" );
public static Item AzureFlute = new Item( 455, "Azure Flute" );
public static Item SSTicket = new Item( 456, "S.S. Ticket" );
public static Item ContestPass = new Item( 457, "Contest Pass" );
public static Item MagmaStone = new Item( 458, "Magma Stone" );
public static Item Parcel = new Item( 459, "Parcel" );
public static Item Coupon1 = new Item( 460, "Coupon 1" );
public static Item Coupon2 = new Item( 461, "Coupon 2" );
public static Item Coupon3 = new Item( 462, "Coupon 3" );
public static Item StorageKey = new Item( 463, "Storage Key" );
public static Item SecretPotion = new Item( 464, "Secret Potion" );
public static Item VsRecorder = new Item( 465, "Vs. Recorder" );
public static Item Gracidea = new Item( 466, "Gracidea" );
public static Item SecretKey = new Item( 467, "Secret Key" );
public static Item ApricornBox = new Item( 468, "Apricorn Box" );
public static Item UnownReport = new Item( 469, "Unown Report" );
public static Item BerryPots = new Item( 470, "Berry Pots" );
public static Item DowsingMachine = new Item( 471, "Dowsing Machine" );
public static Item BlueCard = new Item( 472, "Blue Card" );
public static Item SlowpokeTail = new Item( 473, "Slowpoke Tail" );
public static Item ClearBell = new Item( 474, "Clear Bell" );
public static Item CardKey = new Item( 475, "Card Key" );
public static Item BasementKey = new Item( 476, "Basement Key" );
public static Item SquirtBottle = new Item( 477, "Squirt Bottle" );
public static Item RedScale = new Item( 478, "Red Scale" );
public static Item LostItem = new Item( 479, "Lost Item" );
public static Item Pass = new Item( 480, "Pass" );
public static Item MachinePart = new Item( 481, "Machine Part" );
public static Item SilverWing = new Item( 482, "Silver Wing" );
public static Item RainbowWing = new Item( 483, "Rainbow Wing" );
public static Item MysteryEgg = new Item( 484, "Mystery Egg" );
public static Item RedApricorn = new Item( 485, "Red Apricorn" );
public static Item BlueApricorn = new Item( 486, "Blue Apricorn" );
public static Item YellowApricorn = new Item( 487, "Yellow Apricorn" );
public static Item GreenApricorn = new Item( 488, "Green Apricorn" );
public static Item PinkApricorn = new Item( 489, "Pink Apricorn" );
public static Item WhiteApricorn = new Item( 490, "White Apricorn" );
public static Item BlackApricorn = new Item( 491, "Black Apricorn" );
public static Item FastBall = new Item( 492, "Fast Ball" );
public static Item LevelBall = new Item( 493, "Level Ball" );
public static Item LureBall = new Item( 494, "Lure Ball" );
public static Item HeavyBall = new Item( 495, "Heavy Ball" );
public static Item LoveBall = new Item( 496, "Love Ball" );
public static Item FriendBall = new Item( 497, "Friend Ball" );
public static Item MoonBall = new Item( 498, "Moon Ball" );
public static Item SportBall = new Item( 499, "Sport Ball" );
public static Item ParkBall = new Item( 500, "Park Ball" );
public static Item PhotoAlbum = new Item( 501, "Photo Album" );
public static Item GBSounds = new Item( 502, "GB Sounds" );
public static Item TidalBell = new Item( 503, "Tidal Bell" );
public static Item RageCandyBar = new Item( 504, "Rage Candy Bar" );
public static Item DataCard01 = new Item( 505, "Data Card 01" );
public static Item DataCard02 = new Item( 506, "Data Card 02" );
public static Item DataCard03 = new Item( 507, "Data Card 03" );
public static Item DataCard04 = new Item( 508, "Data Card 04" );
public static Item DataCard05 = new Item( 509, "Data Card 05" );
public static Item DataCard06 = new Item( 510, "Data Card 06" );
public static Item DataCard07 = new Item( 511, "Data Card 07" );
public static Item DataCard08 = new Item( 512, "Data Card 08" );
public static Item DataCard09 = new Item( 513, "Data Card 09" );
public static Item DataCard10 = new Item( 514, "Data Card 10" );
public static Item DataCard11 = new Item( 515, "Data Card 11" );
public static Item DataCard12 = new Item( 516, "Data Card 12" );
public static Item DataCard13 = new Item( 517, "Data Card 13" );
public static Item DataCard14 = new Item( 518, "Data Card 14" );
public static Item DataCard15 = new Item( 519, "Data Card 15" );
public static Item DataCard16 = new Item( 520, "Data Card 16" );
public static Item DataCard17 = new Item( 521, "Data Card 17" );
public static Item DataCard18 = new Item( 522, "Data Card 18" );
public static Item DataCard19 = new Item( 523, "Data Card 19" );
public static Item DataCard20 = new Item( 524, "Data Card 20" );
public static Item DataCard21 = new Item( 525, "Data Card 21" );
public static Item DataCard22 = new Item( 526, "Data Card 22" );
public static Item DataCard23 = new Item( 527, "Data Card 23" );
public static Item DataCard24 = new Item( 528, "Data Card 24" );
public static Item DataCard25 = new Item( 529, "Data Card 25" );
public static Item DataCard26 = new Item( 530, "Data Card 26" );
public static Item DataCard27 = new Item( 531, "Data Card 27" );
public static Item JadeOrb = new Item( 532, "Jade Orb" );
public static Item LockCapsule = new Item( 533, "Lock Capsule" );
public static Item RedOrb = new Item( 534, "Red Orb" );
public static Item BlueOrb = new Item( 535, "Blue Orb" );
public static Item EnigmaStone = new Item( 536, "Enigma Stone" );
public static Item PrismScale = new Item( 537, "Prism Scale" );
public static Item Eviolite = new Item( 538, "Eviolite" );
public static Item FloatStone = new Item( 539, "Float Stone" );
public static Item RockyHelmet = new Item( 540, "Rocky Helmet" );
public static Item AirBalloon = new Item( 541, "Air Balloon" );
public static Item RedCard = new Item( 542, "Red Card" );
public static Item RingTarget = new Item( 543, "Ring Target" );
public static Item BindingBand = new Item( 544, "Binding Band" );
public static Item AbsorbBulb = new Item( 545, "Absorb Bulb" );
public static Item CellBattery = new Item( 546, "Cell Battery" );
public static Item EjectButton = new Item( 547, "Eject Button" );
public static Item FireGem = new Item( 548, "Fire Gem" );
public static Item WaterGem = new Item( 549, "Water Gem" );
public static Item ElectricGem = new Item( 550, "Electric Gem" );
public static Item GrassGem = new Item( 551, "Grass Gem" );
public static Item IceGem = new Item( 552, "Ice Gem" );
public static Item FightingGem = new Item( 553, "Fighting Gem" );
public static Item PoisonGem = new Item( 554, "Poison Gem" );
public static Item GroundGem = new Item( 555, "Ground Gem" );
public static Item FlyingGem = new Item( 556, "Flying Gem" );
public static Item PsychicGem = new Item( 557, "Psychic Gem" );
public static Item BugGem = new Item( 558, "Bug Gem" );
public static Item RockGem = new Item( 559, "Rock Gem" );
public static Item GhostGem = new Item( 560, "Ghost Gem" );
public static Item DragonGem = new Item( 561, "Dragon Gem" );
public static Item DarkGem = new Item( 562, "Dark Gem" );
public static Item SteelGem = new Item( 563, "Steel Gem" );
public static Item NormalGem = new Item( 564, "Normal Gem" );
public static Item HealthWing = new Item( 565, "Health Wing" );
public static Item MuscleWing = new Item( 566, "Muscle Wing" );
public static Item ResistWing = new Item( 567, "Resist Wing" );
public static Item GeniusWing = new Item( 568, "Genius Wing" );
public static Item CleverWing = new Item( 569, "Clever Wing" );
public static Item SwiftWing = new Item( 570, "Swift Wing" );
public static Item PrettyWing = new Item( 571, "Pretty Wing" );
public static Item CoverFossil = new Item( 572, "Cover Fossil" );
public static Item PlumeFossil = new Item( 573, "Plume Fossil" );
public static Item LibertyPass = new Item( 574, "Liberty Pass" );
public static Item PassOrb = new Item( 575, "Pass Orb" );
public static Item DreamBall = new Item( 576, "Dream Ball" );
public static Item PokToy = new Item( 577, "Poké Toy" );
public static Item PropCase = new Item( 578, "Prop Case" );
public static Item DragonSkull = new Item( 579, "Dragon Skull" );
public static Item BalmMushroom = new Item( 580, "Balm Mushroom" );
public static Item BigNugget = new Item( 581, "Big Nugget" );
public static Item PearlString = new Item( 582, "Pearl String" );
public static Item CometShard = new Item( 583, "Comet Shard" );
public static Item RelicCopper = new Item( 584, "Relic Copper" );
public static Item RelicSilver = new Item( 585, "Relic Silver" );
public static Item RelicGold = new Item( 586, "Relic Gold" );
public static Item RelicVase = new Item( 587, "Relic Vase" );
public static Item RelicBand = new Item( 588, "Relic Band" );
public static Item RelicStatue = new Item( 589, "Relic Statue" );
public static Item RelicCrown = new Item( 590, "Relic Crown" );
public static Item Casteliacone = new Item( 591, "Casteliacone" );
public static Item DireHit2 = new Item( 592, "Dire Hit 2" );
public static Item XSpeed2 = new Item( 593, "X Speed 2" );
public static Item XSpAtk2 = new Item( 594, "X Sp. Atk 2" );
public static Item XSpDef2 = new Item( 595, "X Sp. Def 2" );
public static Item XDefense2 = new Item( 596, "X Defense 2" );
public static Item XAttack2 = new Item( 597, "X Attack 2" );
public static Item XAccuracy2 = new Item( 598, "X Accuracy 2" );
public static Item XSpeed3 = new Item( 599, "X Speed 3" );
public static Item XSpAtk3 = new Item( 600, "X Sp. Atk 3" );
public static Item XSpDef3 = new Item( 601, "X Sp. Def 3" );
public static Item XDefense3 = new Item( 602, "X Defense 3" );
public static Item XAttack3 = new Item( 603, "X Attack 3" );
public static Item XAccuracy3 = new Item( 604, "X Accuracy 3" );
public static Item XSpeed6 = new Item( 605, "X Speed 6" );
public static Item XSpAtk6 = new Item( 606, "X Sp. Atk 6" );
public static Item XSpDef6 = new Item( 607, "X Sp. Def 6" );
public static Item XDefense6 = new Item( 608, "X Defense 6" );
public static Item XAttack6 = new Item( 609, "X Attack 6" );
public static Item XAccuracy6 = new Item( 610, "X Accuracy 6" );
public static Item AbilityUrge = new Item( 611, "Ability Urge" );
public static Item ItemDrop = new Item( 612, "Item Drop" );
public static Item ItemUrge = new Item( 613, "Item Urge" );
public static Item ResetUrge = new Item( 614, "Reset Urge" );
public static Item DireHit3 = new Item( 615, "Dire Hit 3" );
public static Item LightStone = new Item( 616, "Light Stone" );
public static Item DarkStone = new Item( 617, "Dark Stone" );
public static Item TM93 = new Item( 618, "TM93" );
public static Item TM94 = new Item( 619, "TM94" );
public static Item TM95 = new Item( 620, "TM95" );
public static Item Xtransceiver = new Item( 621, "Xtransceiver" );
public static Item Gram1 = new Item( 623, "Gram 1" );
public static Item Gram2 = new Item( 624, "Gram 2" );
public static Item Gram3 = new Item( 625, "Gram 3" );
public static Item Xtransceiver_2 = new Item( 626, "Xtransceiver" );
public static Item MedalBox = new Item( 627, "Medal Box" );
public static Item DNASplicers = new Item( 628, "DNA Splicers" );
public static Item DNASplicers_2 = new Item( 629, "DNA Splicers" );
public static Item Permit = new Item( 630, "Permit" );
public static Item OvalCharm = new Item( 631, "Oval Charm" );
public static Item ShinyCharm = new Item( 632, "Shiny Charm" );
public static Item PlasmaCard = new Item( 633, "Plasma Card" );
public static Item GrubbyHanky = new Item( 634, "Grubby Hanky" );
public static Item ColressMachine = new Item( 635, "Colress Machine" );
public static Item DroppedItem = new Item( 636, "Dropped Item" );
public static Item DroppedItem_2 = new Item( 637, "Dropped Item" );
public static Item RevealGlass = new Item( 638, "Reveal Glass" );
public static Item WeaknessPolicy = new Item( 639, "Weakness Policy" );
public static Item AssaultVest = new Item( 640, "Assault Vest" );
public static Item HoloCaster = new Item( 641, "Holo Caster" );
public static Item ProfsLetter = new Item( 642, "Prof’s Letter" );
public static Item RollerSkates = new Item( 643, "Roller Skates" );
public static Item PixiePlate = new Item( 644, "Pixie Plate" );
public static Item AbilityCapsule = new Item( 645, "Ability Capsule" );
public static Item WhippedDream = new Item( 646, "Whipped Dream" );
public static Item Sachet = new Item( 647, "Sachet" );
public static Item LuminousMoss = new Item( 648, "Luminous Moss" );
public static Item Snowball = new Item( 649, "Snowball" );
public static Item SafetyGoggles = new Item( 650, "Safety Goggles" );
public static Item PokFlute = new Item( 651, "Poké Flute" );
public static Item RichMulch = new Item( 652, "Rich Mulch" );
public static Item SurpriseMulch = new Item( 653, "Surprise Mulch" );
public static Item BoostMulch = new Item( 654, "Boost Mulch" );
public static Item AmazeMulch = new Item( 655, "Amaze Mulch" );
public static Item Gengarite = new Item( 656, "Gengarite" );
public static Item Gardevoirite = new Item( 657, "Gardevoirite" );
public static Item Ampharosite = new Item( 658, "Ampharosite" );
public static Item Venusaurite = new Item( 659, "Venusaurite" );
public static Item CharizarditeX = new Item( 660, "Charizardite X" );
public static Item Blastoisinite = new Item( 661, "Blastoisinite" );
public static Item MewtwoniteX = new Item( 662, "Mewtwonite X" );
public static Item MewtwoniteY = new Item( 663, "Mewtwonite Y" );
public static Item Blazikenite = new Item( 664, "Blazikenite" );
public static Item Medichamite = new Item( 665, "Medichamite" );
public static Item Houndoominite = new Item( 666, "Houndoominite" );
public static Item Aggronite = new Item( 667, "Aggronite" );
public static Item Banettite = new Item( 668, "Banettite" );
public static Item Tyranitarite = new Item( 669, "Tyranitarite" );
public static Item Scizorite = new Item( 670, "Scizorite" );
public static Item Pinsirite = new Item( 671, "Pinsirite" );
public static Item Aerodactylite = new Item( 672, "Aerodactylite" );
public static Item Lucarionite = new Item( 673, "Lucarionite" );
public static Item Abomasite = new Item( 674, "Abomasite" );
public static Item Kangaskhanite = new Item( 675, "Kangaskhanite" );
public static Item Gyaradosite = new Item( 676, "Gyaradosite" );
public static Item Absolite = new Item( 677, "Absolite" );
public static Item CharizarditeY = new Item( 678, "Charizardite Y" );
public static Item Alakazite = new Item( 679, "Alakazite" );
public static Item Heracronite = new Item( 680, "Heracronite" );
public static Item Mawilite = new Item( 681, "Mawilite" );
public static Item Manectite = new Item( 682, "Manectite" );
public static Item Garchompite = new Item( 683, "Garchompite" );
public static Item Latiasite = new Item( 684, "Latiasite" );
public static Item Latiosite = new Item( 685, "Latiosite" );
public static Item RoseliBerry = new Item( 686, "Roseli Berry" );
public static Item KeeBerry = new Item( 687, "Kee Berry" );
public static Item MarangaBerry = new Item( 688, "Maranga Berry" );
public static Item Sprinklotad = new Item( 689, "Sprinklotad" );
public static Item TM96 = new Item( 690, "TM96" );
public static Item TM97 = new Item( 691, "TM97" );
public static Item TM98 = new Item( 692, "TM98" );
public static Item TM99 = new Item( 693, "TM99" );
public static Item TM100 = new Item( 694, "TM100" );
public static Item PowerPlantPass = new Item( 695, "Power Plant Pass" );
public static Item MegaRing = new Item( 696, "Mega Ring" );
public static Item IntriguingStone = new Item( 697, "Intriguing Stone" );
public static Item CommonStone = new Item( 698, "Common Stone" );
public static Item DiscountCoupon = new Item( 699, "Discount Coupon" );
public static Item ElevatorKey = new Item( 700, "Elevator Key" );
public static Item TMVPass = new Item( 701, "TMV Pass" );
public static Item HonorofKalos = new Item( 702, "Honor of Kalos" );
public static Item AdventureRules = new Item( 703, "Adventure Rules" );
public static Item StrangeSouvenir = new Item( 704, "Strange Souvenir" );
public static Item LensCase = new Item( 705, "Lens Case" );
public static Item TravelTrunk = new Item( 706, "Travel Trunk" );
public static Item TravelTrunk_2 = new Item( 707, "Travel Trunk" );
public static Item LumioseGalette = new Item( 708, "Lumiose Galette" );
public static Item ShalourSable = new Item( 709, "Shalour Sable" );
public static Item JawFossil = new Item( 710, "Jaw Fossil" );
public static Item SailFossil = new Item( 711, "Sail Fossil" );
public static Item LookerTicket = new Item( 712, "Looker Ticket" );
public static Item Bike_2 = new Item( 713, "Bike" );
public static Item HoloCaster_2 = new Item( 714, "Holo Caster" );
public static Item FairyGem = new Item( 715, "Fairy Gem" );
public static Item MegaCharm = new Item( 716, "Mega Charm" );
public static Item MegaGlove = new Item( 717, "Mega Glove" );
public static Item MachBike = new Item( 718, "Mach Bike" );
public static Item AcroBike = new Item( 719, "Acro Bike" );
public static Item WailmerPail = new Item( 720, "Wailmer Pail" );
public static Item DevonParts = new Item( 721, "Devon Parts" );
public static Item SootSack = new Item( 722, "Soot Sack" );
public static Item BasementKey_2 = new Item( 723, "Basement Key" );
public static Item PokblockKit = new Item( 724, "Pokéblock Kit" );
public static Item Letter = new Item( 725, "Letter" );
public static Item EonTicket = new Item( 726, "Eon Ticket" );
public static Item Scanner = new Item( 727, "Scanner" );
public static Item GoGoggles = new Item( 728, "Go-Goggles" );
public static Item Meteorite = new Item( 729, "Meteorite" );
public static Item KeytoRoom1 = new Item( 730, "Key to Room 1" );
public static Item KeytoRoom2 = new Item( 731, "Key to Room 2" );
public static Item KeytoRoom4 = new Item( 732, "Key to Room 4" );
public static Item KeytoRoom6 = new Item( 733, "Key to Room 6" );
public static Item StorageKey_2 = new Item( 734, "Storage Key" );
public static Item DevonScope = new Item( 735, "Devon Scope" );
public static Item SSTicket_2 = new Item( 736, "S.S. Ticket" );
public static Item HM07 = new Item( 737, "HM07" );
public static Item DevonScubaGear = new Item( 738, "Devon Scuba Gear" );
public static Item ContestCostume = new Item( 739, "Contest Costume" );
public static Item ContestCostume_2 = new Item( 740, "Contest Costume" );
public static Item MagmaSuit = new Item( 741, "Magma Suit" );
public static Item AquaSuit = new Item( 742, "Aqua Suit" );
public static Item PairofTickets = new Item( 743, "Pair of Tickets" );
public static Item MegaBracelet = new Item( 744, "Mega Bracelet" );
public static Item MegaPendant = new Item( 745, "Mega Pendant" );
public static Item MegaGlasses = new Item( 746, "Mega Glasses" );
public static Item MegaAnchor = new Item( 747, "Mega Anchor" );
public static Item MegaStickpin = new Item( 748, "Mega Stickpin" );
public static Item MegaTiara = new Item( 749, "Mega Tiara" );
public static Item MegaAnklet = new Item( 750, "Mega Anklet" );
public static Item Meteorite_2 = new Item( 751, "Meteorite" );
public static Item Swampertite = new Item( 752, "Swampertite" );
public static Item Sceptilite = new Item( 753, "Sceptilite" );
public static Item Sablenite = new Item( 754, "Sablenite" );
public static Item Altarianite = new Item( 755, "Altarianite" );
public static Item Galladite = new Item( 756, "Galladite" );
public static Item Audinite = new Item( 757, "Audinite" );
public static Item Metagrossite = new Item( 758, "Metagrossite" );
public static Item Sharpedonite = new Item( 759, "Sharpedonite" );
public static Item Slowbronite = new Item( 760, "Slowbronite" );
public static Item Steelixite = new Item( 761, "Steelixite" );
public static Item Pidgeotite = new Item( 762, "Pidgeotite" );
public static Item Glalitite = new Item( 763, "Glalitite" );
public static Item Diancite = new Item( 764, "Diancite" );
public static Item PrisonBottle = new Item( 765, "Prison Bottle" );
public static Item MegaCuff = new Item( 766, "Mega Cuff" );
public static Item Cameruptite = new Item( 767, "Cameruptite" );
public static Item Lopunnite = new Item( 768, "Lopunnite" );
public static Item Salamencite = new Item( 769, "Salamencite" );
public static Item Beedrillite = new Item( 770, "Beedrillite" );
public static Item Meteorite_3 = new Item( 771, "Meteorite" );
public static Item Meteorite_4 = new Item( 772, "Meteorite" );
public static Item KeyStone = new Item( 773, "Key Stone" );
public static Item MeteoriteShard = new Item( 774, "Meteorite Shard" );
public static Item EonFlute = new Item( 775, "Eon Flute" );
public static Item GetValueFrom( int id ) => staticValues[ id ];
private static readonly Item[] staticValues = { null,
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
	null,
	null,
	null,
	DouseDrive,
	ShockDrive,
	BurnDrive,
	ChillDrive,
	null,
	null,
	null,
	null,
	null,
	null,
	null,
	null,
	null,
	null,
	null,
	null,
	null,
	null,
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
	null,
	null,
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
	null,
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
	EonFlute };
public static IEnumerable<Item> AllItems => staticValues.AsEnumerable();
}

public sealed class Move : BaseMove {
    public Move( int id, string name ) : base( id, name ) { }
    public static explicit operator Move( int id ) => Moves.GetValueFrom( id );
    public static explicit operator int( Move val ) => val.Id;
}
public static partial class Moves {
public static Move Pound = new Move( 1, "Pound" );
public static Move KarateChop = new Move( 2, "Karate Chop" );
public static Move DoubleSlap = new Move( 3, "Double Slap" );
public static Move CometPunch = new Move( 4, "Comet Punch" );
public static Move MegaPunch = new Move( 5, "Mega Punch" );
public static Move PayDay = new Move( 6, "Pay Day" );
public static Move FirePunch = new Move( 7, "Fire Punch" );
public static Move IcePunch = new Move( 8, "Ice Punch" );
public static Move ThunderPunch = new Move( 9, "Thunder Punch" );
public static Move Scratch = new Move( 10, "Scratch" );
public static Move ViceGrip = new Move( 11, "Vice Grip" );
public static Move Guillotine = new Move( 12, "Guillotine" );
public static Move RazorWind = new Move( 13, "Razor Wind" );
public static Move SwordsDance = new Move( 14, "Swords Dance" );
public static Move Cut = new Move( 15, "Cut" );
public static Move Gust = new Move( 16, "Gust" );
public static Move WingAttack = new Move( 17, "Wing Attack" );
public static Move Whirlwind = new Move( 18, "Whirlwind" );
public static Move Fly = new Move( 19, "Fly" );
public static Move Bind = new Move( 20, "Bind" );
public static Move Slam = new Move( 21, "Slam" );
public static Move VineWhip = new Move( 22, "Vine Whip" );
public static Move Stomp = new Move( 23, "Stomp" );
public static Move DoubleKick = new Move( 24, "Double Kick" );
public static Move MegaKick = new Move( 25, "Mega Kick" );
public static Move JumpKick = new Move( 26, "Jump Kick" );
public static Move RollingKick = new Move( 27, "Rolling Kick" );
public static Move SandAttack = new Move( 28, "Sand Attack" );
public static Move Headbutt = new Move( 29, "Headbutt" );
public static Move HornAttack = new Move( 30, "Horn Attack" );
public static Move FuryAttack = new Move( 31, "Fury Attack" );
public static Move HornDrill = new Move( 32, "Horn Drill" );
public static Move Tackle = new Move( 33, "Tackle" );
public static Move BodySlam = new Move( 34, "Body Slam" );
public static Move Wrap = new Move( 35, "Wrap" );
public static Move TakeDown = new Move( 36, "Take Down" );
public static Move Thrash = new Move( 37, "Thrash" );
public static Move DoubleEdge = new Move( 38, "Double-Edge" );
public static Move TailWhip = new Move( 39, "Tail Whip" );
public static Move PoisonSting = new Move( 40, "Poison Sting" );
public static Move Twineedle = new Move( 41, "Twineedle" );
public static Move PinMissile = new Move( 42, "Pin Missile" );
public static Move Leer = new Move( 43, "Leer" );
public static Move Bite = new Move( 44, "Bite" );
public static Move Growl = new Move( 45, "Growl" );
public static Move Roar = new Move( 46, "Roar" );
public static Move Sing = new Move( 47, "Sing" );
public static Move Supersonic = new Move( 48, "Supersonic" );
public static Move SonicBoom = new Move( 49, "Sonic Boom" );
public static Move Disable = new Move( 50, "Disable" );
public static Move Acid = new Move( 51, "Acid" );
public static Move Ember = new Move( 52, "Ember" );
public static Move Flamethrower = new Move( 53, "Flamethrower" );
public static Move Mist = new Move( 54, "Mist" );
public static Move WaterGun = new Move( 55, "Water Gun" );
public static Move HydroPump = new Move( 56, "Hydro Pump" );
public static Move Surf = new Move( 57, "Surf" );
public static Move IceBeam = new Move( 58, "Ice Beam" );
public static Move Blizzard = new Move( 59, "Blizzard" );
public static Move Psybeam = new Move( 60, "Psybeam" );
public static Move BubbleBeam = new Move( 61, "Bubble Beam" );
public static Move AuroraBeam = new Move( 62, "Aurora Beam" );
public static Move HyperBeam = new Move( 63, "Hyper Beam" );
public static Move Peck = new Move( 64, "Peck" );
public static Move DrillPeck = new Move( 65, "Drill Peck" );
public static Move Submission = new Move( 66, "Submission" );
public static Move LowKick = new Move( 67, "Low Kick" );
public static Move Counter = new Move( 68, "Counter" );
public static Move SeismicToss = new Move( 69, "Seismic Toss" );
public static Move Strength = new Move( 70, "Strength" );
public static Move Absorb = new Move( 71, "Absorb" );
public static Move MegaDrain = new Move( 72, "Mega Drain" );
public static Move LeechSeed = new Move( 73, "Leech Seed" );
public static Move Growth = new Move( 74, "Growth" );
public static Move RazorLeaf = new Move( 75, "Razor Leaf" );
public static Move SolarBeam = new Move( 76, "Solar Beam" );
public static Move PoisonPowder = new Move( 77, "Poison Powder" );
public static Move StunSpore = new Move( 78, "Stun Spore" );
public static Move SleepPowder = new Move( 79, "Sleep Powder" );
public static Move PetalDance = new Move( 80, "Petal Dance" );
public static Move StringShot = new Move( 81, "String Shot" );
public static Move DragonRage = new Move( 82, "Dragon Rage" );
public static Move FireSpin = new Move( 83, "Fire Spin" );
public static Move ThunderShock = new Move( 84, "Thunder Shock" );
public static Move Thunderbolt = new Move( 85, "Thunderbolt" );
public static Move ThunderWave = new Move( 86, "Thunder Wave" );
public static Move Thunder = new Move( 87, "Thunder" );
public static Move RockThrow = new Move( 88, "Rock Throw" );
public static Move Earthquake = new Move( 89, "Earthquake" );
public static Move Fissure = new Move( 90, "Fissure" );
public static Move Dig = new Move( 91, "Dig" );
public static Move Toxic = new Move( 92, "Toxic" );
public static Move Confusion = new Move( 93, "Confusion" );
public static Move Psychic = new Move( 94, "Psychic" );
public static Move Hypnosis = new Move( 95, "Hypnosis" );
public static Move Meditate = new Move( 96, "Meditate" );
public static Move Agility = new Move( 97, "Agility" );
public static Move QuickAttack = new Move( 98, "Quick Attack" );
public static Move Rage = new Move( 99, "Rage" );
public static Move Teleport = new Move( 100, "Teleport" );
public static Move NightShade = new Move( 101, "Night Shade" );
public static Move Mimic = new Move( 102, "Mimic" );
public static Move Screech = new Move( 103, "Screech" );
public static Move DoubleTeam = new Move( 104, "Double Team" );
public static Move Recover = new Move( 105, "Recover" );
public static Move Harden = new Move( 106, "Harden" );
public static Move Minimize = new Move( 107, "Minimize" );
public static Move Smokescreen = new Move( 108, "Smokescreen" );
public static Move ConfuseRay = new Move( 109, "Confuse Ray" );
public static Move Withdraw = new Move( 110, "Withdraw" );
public static Move DefenseCurl = new Move( 111, "Defense Curl" );
public static Move Barrier = new Move( 112, "Barrier" );
public static Move LightScreen = new Move( 113, "Light Screen" );
public static Move Haze = new Move( 114, "Haze" );
public static Move Reflect = new Move( 115, "Reflect" );
public static Move FocusEnergy = new Move( 116, "Focus Energy" );
public static Move Bide = new Move( 117, "Bide" );
public static Move Metronome = new Move( 118, "Metronome" );
public static Move MirrorMove = new Move( 119, "Mirror Move" );
public static Move SelfDestruct = new Move( 120, "Self-Destruct" );
public static Move EggBomb = new Move( 121, "Egg Bomb" );
public static Move Lick = new Move( 122, "Lick" );
public static Move Smog = new Move( 123, "Smog" );
public static Move Sludge = new Move( 124, "Sludge" );
public static Move BoneClub = new Move( 125, "Bone Club" );
public static Move FireBlast = new Move( 126, "Fire Blast" );
public static Move Waterfall = new Move( 127, "Waterfall" );
public static Move Clamp = new Move( 128, "Clamp" );
public static Move Swift = new Move( 129, "Swift" );
public static Move SkullBash = new Move( 130, "Skull Bash" );
public static Move SpikeCannon = new Move( 131, "Spike Cannon" );
public static Move Constrict = new Move( 132, "Constrict" );
public static Move Amnesia = new Move( 133, "Amnesia" );
public static Move Kinesis = new Move( 134, "Kinesis" );
public static Move SoftBoiled = new Move( 135, "Soft-Boiled" );
public static Move HighJumpKick = new Move( 136, "High Jump Kick" );
public static Move Glare = new Move( 137, "Glare" );
public static Move DreamEater = new Move( 138, "Dream Eater" );
public static Move PoisonGas = new Move( 139, "Poison Gas" );
public static Move Barrage = new Move( 140, "Barrage" );
public static Move LeechLife = new Move( 141, "Leech Life" );
public static Move LovelyKiss = new Move( 142, "Lovely Kiss" );
public static Move SkyAttack = new Move( 143, "Sky Attack" );
public static Move Transform = new Move( 144, "Transform" );
public static Move Bubble = new Move( 145, "Bubble" );
public static Move DizzyPunch = new Move( 146, "Dizzy Punch" );
public static Move Spore = new Move( 147, "Spore" );
public static Move Flash = new Move( 148, "Flash" );
public static Move Psywave = new Move( 149, "Psywave" );
public static Move Splash = new Move( 150, "Splash" );
public static Move AcidArmor = new Move( 151, "Acid Armor" );
public static Move Crabhammer = new Move( 152, "Crabhammer" );
public static Move Explosion = new Move( 153, "Explosion" );
public static Move FurySwipes = new Move( 154, "Fury Swipes" );
public static Move Bonemerang = new Move( 155, "Bonemerang" );
public static Move Rest = new Move( 156, "Rest" );
public static Move RockSlide = new Move( 157, "Rock Slide" );
public static Move HyperFang = new Move( 158, "Hyper Fang" );
public static Move Sharpen = new Move( 159, "Sharpen" );
public static Move Conversion = new Move( 160, "Conversion" );
public static Move TriAttack = new Move( 161, "Tri Attack" );
public static Move SuperFang = new Move( 162, "Super Fang" );
public static Move Slash = new Move( 163, "Slash" );
public static Move Substitute = new Move( 164, "Substitute" );
public static Move Struggle = new Move( 165, "Struggle" );
public static Move Sketch = new Move( 166, "Sketch" );
public static Move TripleKick = new Move( 167, "Triple Kick" );
public static Move Thief = new Move( 168, "Thief" );
public static Move SpiderWeb = new Move( 169, "Spider Web" );
public static Move MindReader = new Move( 170, "Mind Reader" );
public static Move Nightmare = new Move( 171, "Nightmare" );
public static Move FlameWheel = new Move( 172, "Flame Wheel" );
public static Move Snore = new Move( 173, "Snore" );
public static Move Curse = new Move( 174, "Curse" );
public static Move Flail = new Move( 175, "Flail" );
public static Move Conversion2 = new Move( 176, "Conversion 2" );
public static Move Aeroblast = new Move( 177, "Aeroblast" );
public static Move CottonSpore = new Move( 178, "Cotton Spore" );
public static Move Reversal = new Move( 179, "Reversal" );
public static Move Spite = new Move( 180, "Spite" );
public static Move PowderSnow = new Move( 181, "Powder Snow" );
public static Move Protect = new Move( 182, "Protect" );
public static Move MachPunch = new Move( 183, "Mach Punch" );
public static Move ScaryFace = new Move( 184, "Scary Face" );
public static Move FeintAttack = new Move( 185, "Feint Attack" );
public static Move SweetKiss = new Move( 186, "Sweet Kiss" );
public static Move BellyDrum = new Move( 187, "Belly Drum" );
public static Move SludgeBomb = new Move( 188, "Sludge Bomb" );
public static Move MudSlap = new Move( 189, "Mud-Slap" );
public static Move Octazooka = new Move( 190, "Octazooka" );
public static Move Spikes = new Move( 191, "Spikes" );
public static Move ZapCannon = new Move( 192, "Zap Cannon" );
public static Move Foresight = new Move( 193, "Foresight" );
public static Move DestinyBond = new Move( 194, "Destiny Bond" );
public static Move PerishSong = new Move( 195, "Perish Song" );
public static Move IcyWind = new Move( 196, "Icy Wind" );
public static Move Detect = new Move( 197, "Detect" );
public static Move BoneRush = new Move( 198, "Bone Rush" );
public static Move LockOn = new Move( 199, "Lock-On" );
public static Move Outrage = new Move( 200, "Outrage" );
public static Move Sandstorm = new Move( 201, "Sandstorm" );
public static Move GigaDrain = new Move( 202, "Giga Drain" );
public static Move Endure = new Move( 203, "Endure" );
public static Move Charm = new Move( 204, "Charm" );
public static Move Rollout = new Move( 205, "Rollout" );
public static Move FalseSwipe = new Move( 206, "False Swipe" );
public static Move Swagger = new Move( 207, "Swagger" );
public static Move MilkDrink = new Move( 208, "Milk Drink" );
public static Move Spark = new Move( 209, "Spark" );
public static Move FuryCutter = new Move( 210, "Fury Cutter" );
public static Move SteelWing = new Move( 211, "Steel Wing" );
public static Move MeanLook = new Move( 212, "Mean Look" );
public static Move Attract = new Move( 213, "Attract" );
public static Move SleepTalk = new Move( 214, "Sleep Talk" );
public static Move HealBell = new Move( 215, "Heal Bell" );
public static Move Return = new Move( 216, "Return" );
public static Move Present = new Move( 217, "Present" );
public static Move Frustration = new Move( 218, "Frustration" );
public static Move Safeguard = new Move( 219, "Safeguard" );
public static Move PainSplit = new Move( 220, "Pain Split" );
public static Move SacredFire = new Move( 221, "Sacred Fire" );
public static Move Magnitude = new Move( 222, "Magnitude" );
public static Move DynamicPunch = new Move( 223, "Dynamic Punch" );
public static Move Megahorn = new Move( 224, "Megahorn" );
public static Move DragonBreath = new Move( 225, "Dragon Breath" );
public static Move BatonPass = new Move( 226, "Baton Pass" );
public static Move Encore = new Move( 227, "Encore" );
public static Move Pursuit = new Move( 228, "Pursuit" );
public static Move RapidSpin = new Move( 229, "Rapid Spin" );
public static Move SweetScent = new Move( 230, "Sweet Scent" );
public static Move IronTail = new Move( 231, "Iron Tail" );
public static Move MetalClaw = new Move( 232, "Metal Claw" );
public static Move VitalThrow = new Move( 233, "Vital Throw" );
public static Move MorningSun = new Move( 234, "Morning Sun" );
public static Move Synthesis = new Move( 235, "Synthesis" );
public static Move Moonlight = new Move( 236, "Moonlight" );
public static Move HiddenPower = new Move( 237, "Hidden Power" );
public static Move CrossChop = new Move( 238, "Cross Chop" );
public static Move Twister = new Move( 239, "Twister" );
public static Move RainDance = new Move( 240, "Rain Dance" );
public static Move SunnyDay = new Move( 241, "Sunny Day" );
public static Move Crunch = new Move( 242, "Crunch" );
public static Move MirrorCoat = new Move( 243, "Mirror Coat" );
public static Move PsychUp = new Move( 244, "Psych Up" );
public static Move ExtremeSpeed = new Move( 245, "Extreme Speed" );
public static Move AncientPower = new Move( 246, "Ancient Power" );
public static Move ShadowBall = new Move( 247, "Shadow Ball" );
public static Move FutureSight = new Move( 248, "Future Sight" );
public static Move RockSmash = new Move( 249, "Rock Smash" );
public static Move Whirlpool = new Move( 250, "Whirlpool" );
public static Move BeatUp = new Move( 251, "Beat Up" );
public static Move FakeOut = new Move( 252, "Fake Out" );
public static Move Uproar = new Move( 253, "Uproar" );
public static Move Stockpile = new Move( 254, "Stockpile" );
public static Move SpitUp = new Move( 255, "Spit Up" );
public static Move Swallow = new Move( 256, "Swallow" );
public static Move HeatWave = new Move( 257, "Heat Wave" );
public static Move Hail = new Move( 258, "Hail" );
public static Move Torment = new Move( 259, "Torment" );
public static Move Flatter = new Move( 260, "Flatter" );
public static Move WillOWisp = new Move( 261, "Will-O-Wisp" );
public static Move Memento = new Move( 262, "Memento" );
public static Move Facade = new Move( 263, "Facade" );
public static Move FocusPunch = new Move( 264, "Focus Punch" );
public static Move SmellingSalts = new Move( 265, "Smelling Salts" );
public static Move FollowMe = new Move( 266, "Follow Me" );
public static Move NaturePower = new Move( 267, "Nature Power" );
public static Move Charge = new Move( 268, "Charge" );
public static Move Taunt = new Move( 269, "Taunt" );
public static Move HelpingHand = new Move( 270, "Helping Hand" );
public static Move Trick = new Move( 271, "Trick" );
public static Move RolePlay = new Move( 272, "Role Play" );
public static Move Wish = new Move( 273, "Wish" );
public static Move Assist = new Move( 274, "Assist" );
public static Move Ingrain = new Move( 275, "Ingrain" );
public static Move Superpower = new Move( 276, "Superpower" );
public static Move MagicCoat = new Move( 277, "Magic Coat" );
public static Move Recycle = new Move( 278, "Recycle" );
public static Move Revenge = new Move( 279, "Revenge" );
public static Move BrickBreak = new Move( 280, "Brick Break" );
public static Move Yawn = new Move( 281, "Yawn" );
public static Move KnockOff = new Move( 282, "Knock Off" );
public static Move Endeavor = new Move( 283, "Endeavor" );
public static Move Eruption = new Move( 284, "Eruption" );
public static Move SkillSwap = new Move( 285, "Skill Swap" );
public static Move Imprison = new Move( 286, "Imprison" );
public static Move Refresh = new Move( 287, "Refresh" );
public static Move Grudge = new Move( 288, "Grudge" );
public static Move Snatch = new Move( 289, "Snatch" );
public static Move SecretPower = new Move( 290, "Secret Power" );
public static Move Dive = new Move( 291, "Dive" );
public static Move ArmThrust = new Move( 292, "Arm Thrust" );
public static Move Camouflage = new Move( 293, "Camouflage" );
public static Move TailGlow = new Move( 294, "Tail Glow" );
public static Move LusterPurge = new Move( 295, "Luster Purge" );
public static Move MistBall = new Move( 296, "Mist Ball" );
public static Move FeatherDance = new Move( 297, "Feather Dance" );
public static Move TeeterDance = new Move( 298, "Teeter Dance" );
public static Move BlazeKick = new Move( 299, "Blaze Kick" );
public static Move MudSport = new Move( 300, "Mud Sport" );
public static Move IceBall = new Move( 301, "Ice Ball" );
public static Move NeedleArm = new Move( 302, "Needle Arm" );
public static Move SlackOff = new Move( 303, "Slack Off" );
public static Move HyperVoice = new Move( 304, "Hyper Voice" );
public static Move PoisonFang = new Move( 305, "Poison Fang" );
public static Move CrushClaw = new Move( 306, "Crush Claw" );
public static Move BlastBurn = new Move( 307, "Blast Burn" );
public static Move HydroCannon = new Move( 308, "Hydro Cannon" );
public static Move MeteorMash = new Move( 309, "Meteor Mash" );
public static Move Astonish = new Move( 310, "Astonish" );
public static Move WeatherBall = new Move( 311, "Weather Ball" );
public static Move Aromatherapy = new Move( 312, "Aromatherapy" );
public static Move FakeTears = new Move( 313, "Fake Tears" );
public static Move AirCutter = new Move( 314, "Air Cutter" );
public static Move Overheat = new Move( 315, "Overheat" );
public static Move OdorSleuth = new Move( 316, "Odor Sleuth" );
public static Move RockTomb = new Move( 317, "Rock Tomb" );
public static Move SilverWind = new Move( 318, "Silver Wind" );
public static Move MetalSound = new Move( 319, "Metal Sound" );
public static Move GrassWhistle = new Move( 320, "Grass Whistle" );
public static Move Tickle = new Move( 321, "Tickle" );
public static Move CosmicPower = new Move( 322, "Cosmic Power" );
public static Move WaterSpout = new Move( 323, "Water Spout" );
public static Move SignalBeam = new Move( 324, "Signal Beam" );
public static Move ShadowPunch = new Move( 325, "Shadow Punch" );
public static Move Extrasensory = new Move( 326, "Extrasensory" );
public static Move SkyUppercut = new Move( 327, "Sky Uppercut" );
public static Move SandTomb = new Move( 328, "Sand Tomb" );
public static Move SheerCold = new Move( 329, "Sheer Cold" );
public static Move MuddyWater = new Move( 330, "Muddy Water" );
public static Move BulletSeed = new Move( 331, "Bullet Seed" );
public static Move AerialAce = new Move( 332, "Aerial Ace" );
public static Move IcicleSpear = new Move( 333, "Icicle Spear" );
public static Move IronDefense = new Move( 334, "Iron Defense" );
public static Move Block = new Move( 335, "Block" );
public static Move Howl = new Move( 336, "Howl" );
public static Move DragonClaw = new Move( 337, "Dragon Claw" );
public static Move FrenzyPlant = new Move( 338, "Frenzy Plant" );
public static Move BulkUp = new Move( 339, "Bulk Up" );
public static Move Bounce = new Move( 340, "Bounce" );
public static Move MudShot = new Move( 341, "Mud Shot" );
public static Move PoisonTail = new Move( 342, "Poison Tail" );
public static Move Covet = new Move( 343, "Covet" );
public static Move VoltTackle = new Move( 344, "Volt Tackle" );
public static Move MagicalLeaf = new Move( 345, "Magical Leaf" );
public static Move WaterSport = new Move( 346, "Water Sport" );
public static Move CalmMind = new Move( 347, "Calm Mind" );
public static Move LeafBlade = new Move( 348, "Leaf Blade" );
public static Move DragonDance = new Move( 349, "Dragon Dance" );
public static Move RockBlast = new Move( 350, "Rock Blast" );
public static Move ShockWave = new Move( 351, "Shock Wave" );
public static Move WaterPulse = new Move( 352, "Water Pulse" );
public static Move DoomDesire = new Move( 353, "Doom Desire" );
public static Move PsychoBoost = new Move( 354, "Psycho Boost" );
public static Move Roost = new Move( 355, "Roost" );
public static Move Gravity = new Move( 356, "Gravity" );
public static Move MiracleEye = new Move( 357, "Miracle Eye" );
public static Move WakeUpSlap = new Move( 358, "Wake-Up Slap" );
public static Move HammerArm = new Move( 359, "Hammer Arm" );
public static Move GyroBall = new Move( 360, "Gyro Ball" );
public static Move HealingWish = new Move( 361, "Healing Wish" );
public static Move Brine = new Move( 362, "Brine" );
public static Move NaturalGift = new Move( 363, "Natural Gift" );
public static Move Feint = new Move( 364, "Feint" );
public static Move Pluck = new Move( 365, "Pluck" );
public static Move Tailwind = new Move( 366, "Tailwind" );
public static Move Acupressure = new Move( 367, "Acupressure" );
public static Move MetalBurst = new Move( 368, "Metal Burst" );
public static Move Uturn = new Move( 369, "U-turn" );
public static Move CloseCombat = new Move( 370, "Close Combat" );
public static Move Payback = new Move( 371, "Payback" );
public static Move Assurance = new Move( 372, "Assurance" );
public static Move Embargo = new Move( 373, "Embargo" );
public static Move Fling = new Move( 374, "Fling" );
public static Move PsychoShift = new Move( 375, "Psycho Shift" );
public static Move TrumpCard = new Move( 376, "Trump Card" );
public static Move HealBlock = new Move( 377, "Heal Block" );
public static Move WringOut = new Move( 378, "Wring Out" );
public static Move PowerTrick = new Move( 379, "Power Trick" );
public static Move GastroAcid = new Move( 380, "Gastro Acid" );
public static Move LuckyChant = new Move( 381, "Lucky Chant" );
public static Move MeFirst = new Move( 382, "Me First" );
public static Move Copycat = new Move( 383, "Copycat" );
public static Move PowerSwap = new Move( 384, "Power Swap" );
public static Move GuardSwap = new Move( 385, "Guard Swap" );
public static Move Punishment = new Move( 386, "Punishment" );
public static Move LastResort = new Move( 387, "Last Resort" );
public static Move WorrySeed = new Move( 388, "Worry Seed" );
public static Move SuckerPunch = new Move( 389, "Sucker Punch" );
public static Move ToxicSpikes = new Move( 390, "Toxic Spikes" );
public static Move HeartSwap = new Move( 391, "Heart Swap" );
public static Move AquaRing = new Move( 392, "Aqua Ring" );
public static Move MagnetRise = new Move( 393, "Magnet Rise" );
public static Move FlareBlitz = new Move( 394, "Flare Blitz" );
public static Move ForcePalm = new Move( 395, "Force Palm" );
public static Move AuraSphere = new Move( 396, "Aura Sphere" );
public static Move RockPolish = new Move( 397, "Rock Polish" );
public static Move PoisonJab = new Move( 398, "Poison Jab" );
public static Move DarkPulse = new Move( 399, "Dark Pulse" );
public static Move NightSlash = new Move( 400, "Night Slash" );
public static Move AquaTail = new Move( 401, "Aqua Tail" );
public static Move SeedBomb = new Move( 402, "Seed Bomb" );
public static Move AirSlash = new Move( 403, "Air Slash" );
public static Move XScissor = new Move( 404, "X-Scissor" );
public static Move BugBuzz = new Move( 405, "Bug Buzz" );
public static Move DragonPulse = new Move( 406, "Dragon Pulse" );
public static Move DragonRush = new Move( 407, "Dragon Rush" );
public static Move PowerGem = new Move( 408, "Power Gem" );
public static Move DrainPunch = new Move( 409, "Drain Punch" );
public static Move VacuumWave = new Move( 410, "Vacuum Wave" );
public static Move FocusBlast = new Move( 411, "Focus Blast" );
public static Move EnergyBall = new Move( 412, "Energy Ball" );
public static Move BraveBird = new Move( 413, "Brave Bird" );
public static Move EarthPower = new Move( 414, "Earth Power" );
public static Move Switcheroo = new Move( 415, "Switcheroo" );
public static Move GigaImpact = new Move( 416, "Giga Impact" );
public static Move NastyPlot = new Move( 417, "Nasty Plot" );
public static Move BulletPunch = new Move( 418, "Bullet Punch" );
public static Move Avalanche = new Move( 419, "Avalanche" );
public static Move IceShard = new Move( 420, "Ice Shard" );
public static Move ShadowClaw = new Move( 421, "Shadow Claw" );
public static Move ThunderFang = new Move( 422, "Thunder Fang" );
public static Move IceFang = new Move( 423, "Ice Fang" );
public static Move FireFang = new Move( 424, "Fire Fang" );
public static Move ShadowSneak = new Move( 425, "Shadow Sneak" );
public static Move MudBomb = new Move( 426, "Mud Bomb" );
public static Move PsychoCut = new Move( 427, "Psycho Cut" );
public static Move ZenHeadbutt = new Move( 428, "Zen Headbutt" );
public static Move MirrorShot = new Move( 429, "Mirror Shot" );
public static Move FlashCannon = new Move( 430, "Flash Cannon" );
public static Move RockClimb = new Move( 431, "Rock Climb" );
public static Move Defog = new Move( 432, "Defog" );
public static Move TrickRoom = new Move( 433, "Trick Room" );
public static Move DracoMeteor = new Move( 434, "Draco Meteor" );
public static Move Discharge = new Move( 435, "Discharge" );
public static Move LavaPlume = new Move( 436, "Lava Plume" );
public static Move LeafStorm = new Move( 437, "Leaf Storm" );
public static Move PowerWhip = new Move( 438, "Power Whip" );
public static Move RockWrecker = new Move( 439, "Rock Wrecker" );
public static Move CrossPoison = new Move( 440, "Cross Poison" );
public static Move GunkShot = new Move( 441, "Gunk Shot" );
public static Move IronHead = new Move( 442, "Iron Head" );
public static Move MagnetBomb = new Move( 443, "Magnet Bomb" );
public static Move StoneEdge = new Move( 444, "Stone Edge" );
public static Move Captivate = new Move( 445, "Captivate" );
public static Move StealthRock = new Move( 446, "Stealth Rock" );
public static Move GrassKnot = new Move( 447, "Grass Knot" );
public static Move Chatter = new Move( 448, "Chatter" );
public static Move Judgment = new Move( 449, "Judgment" );
public static Move BugBite = new Move( 450, "Bug Bite" );
public static Move ChargeBeam = new Move( 451, "Charge Beam" );
public static Move WoodHammer = new Move( 452, "Wood Hammer" );
public static Move AquaJet = new Move( 453, "Aqua Jet" );
public static Move AttackOrder = new Move( 454, "Attack Order" );
public static Move DefendOrder = new Move( 455, "Defend Order" );
public static Move HealOrder = new Move( 456, "Heal Order" );
public static Move HeadSmash = new Move( 457, "Head Smash" );
public static Move DoubleHit = new Move( 458, "Double Hit" );
public static Move RoarofTime = new Move( 459, "Roar of Time" );
public static Move SpacialRend = new Move( 460, "Spacial Rend" );
public static Move LunarDance = new Move( 461, "Lunar Dance" );
public static Move CrushGrip = new Move( 462, "Crush Grip" );
public static Move MagmaStorm = new Move( 463, "Magma Storm" );
public static Move DarkVoid = new Move( 464, "Dark Void" );
public static Move SeedFlare = new Move( 465, "Seed Flare" );
public static Move OminousWind = new Move( 466, "Ominous Wind" );
public static Move ShadowForce = new Move( 467, "Shadow Force" );
public static Move HoneClaws = new Move( 468, "Hone Claws" );
public static Move WideGuard = new Move( 469, "Wide Guard" );
public static Move GuardSplit = new Move( 470, "Guard Split" );
public static Move PowerSplit = new Move( 471, "Power Split" );
public static Move WonderRoom = new Move( 472, "Wonder Room" );
public static Move Psyshock = new Move( 473, "Psyshock" );
public static Move Venoshock = new Move( 474, "Venoshock" );
public static Move Autotomize = new Move( 475, "Autotomize" );
public static Move RagePowder = new Move( 476, "Rage Powder" );
public static Move Telekinesis = new Move( 477, "Telekinesis" );
public static Move MagicRoom = new Move( 478, "Magic Room" );
public static Move SmackDown = new Move( 479, "Smack Down" );
public static Move StormThrow = new Move( 480, "Storm Throw" );
public static Move FlameBurst = new Move( 481, "Flame Burst" );
public static Move SludgeWave = new Move( 482, "Sludge Wave" );
public static Move QuiverDance = new Move( 483, "Quiver Dance" );
public static Move HeavySlam = new Move( 484, "Heavy Slam" );
public static Move Synchronoise = new Move( 485, "Synchronoise" );
public static Move ElectroBall = new Move( 486, "Electro Ball" );
public static Move Soak = new Move( 487, "Soak" );
public static Move FlameCharge = new Move( 488, "Flame Charge" );
public static Move Coil = new Move( 489, "Coil" );
public static Move LowSweep = new Move( 490, "Low Sweep" );
public static Move AcidSpray = new Move( 491, "Acid Spray" );
public static Move FoulPlay = new Move( 492, "Foul Play" );
public static Move SimpleBeam = new Move( 493, "Simple Beam" );
public static Move Entrainment = new Move( 494, "Entrainment" );
public static Move AfterYou = new Move( 495, "After You" );
public static Move Round = new Move( 496, "Round" );
public static Move EchoedVoice = new Move( 497, "Echoed Voice" );
public static Move ChipAway = new Move( 498, "Chip Away" );
public static Move ClearSmog = new Move( 499, "Clear Smog" );
public static Move StoredPower = new Move( 500, "Stored Power" );
public static Move QuickGuard = new Move( 501, "Quick Guard" );
public static Move AllySwitch = new Move( 502, "Ally Switch" );
public static Move Scald = new Move( 503, "Scald" );
public static Move ShellSmash = new Move( 504, "Shell Smash" );
public static Move HealPulse = new Move( 505, "Heal Pulse" );
public static Move Hex = new Move( 506, "Hex" );
public static Move SkyDrop = new Move( 507, "Sky Drop" );
public static Move ShiftGear = new Move( 508, "Shift Gear" );
public static Move CircleThrow = new Move( 509, "Circle Throw" );
public static Move Incinerate = new Move( 510, "Incinerate" );
public static Move Quash = new Move( 511, "Quash" );
public static Move Acrobatics = new Move( 512, "Acrobatics" );
public static Move ReflectType = new Move( 513, "Reflect Type" );
public static Move Retaliate = new Move( 514, "Retaliate" );
public static Move FinalGambit = new Move( 515, "Final Gambit" );
public static Move Bestow = new Move( 516, "Bestow" );
public static Move Inferno = new Move( 517, "Inferno" );
public static Move WaterPledge = new Move( 518, "Water Pledge" );
public static Move FirePledge = new Move( 519, "Fire Pledge" );
public static Move GrassPledge = new Move( 520, "Grass Pledge" );
public static Move VoltSwitch = new Move( 521, "Volt Switch" );
public static Move StruggleBug = new Move( 522, "Struggle Bug" );
public static Move Bulldoze = new Move( 523, "Bulldoze" );
public static Move FrostBreath = new Move( 524, "Frost Breath" );
public static Move DragonTail = new Move( 525, "Dragon Tail" );
public static Move WorkUp = new Move( 526, "Work Up" );
public static Move Electroweb = new Move( 527, "Electroweb" );
public static Move WildCharge = new Move( 528, "Wild Charge" );
public static Move DrillRun = new Move( 529, "Drill Run" );
public static Move DualChop = new Move( 530, "Dual Chop" );
public static Move HeartStamp = new Move( 531, "Heart Stamp" );
public static Move HornLeech = new Move( 532, "Horn Leech" );
public static Move SacredSword = new Move( 533, "Sacred Sword" );
public static Move RazorShell = new Move( 534, "Razor Shell" );
public static Move HeatCrash = new Move( 535, "Heat Crash" );
public static Move LeafTornado = new Move( 536, "Leaf Tornado" );
public static Move Steamroller = new Move( 537, "Steamroller" );
public static Move CottonGuard = new Move( 538, "Cotton Guard" );
public static Move NightDaze = new Move( 539, "Night Daze" );
public static Move Psystrike = new Move( 540, "Psystrike" );
public static Move TailSlap = new Move( 541, "Tail Slap" );
public static Move Hurricane = new Move( 542, "Hurricane" );
public static Move HeadCharge = new Move( 543, "Head Charge" );
public static Move GearGrind = new Move( 544, "Gear Grind" );
public static Move SearingShot = new Move( 545, "Searing Shot" );
public static Move TechnoBlast = new Move( 546, "Techno Blast" );
public static Move RelicSong = new Move( 547, "Relic Song" );
public static Move SecretSword = new Move( 548, "Secret Sword" );
public static Move Glaciate = new Move( 549, "Glaciate" );
public static Move BoltStrike = new Move( 550, "Bolt Strike" );
public static Move BlueFlare = new Move( 551, "Blue Flare" );
public static Move FieryDance = new Move( 552, "Fiery Dance" );
public static Move FreezeShock = new Move( 553, "Freeze Shock" );
public static Move IceBurn = new Move( 554, "Ice Burn" );
public static Move Snarl = new Move( 555, "Snarl" );
public static Move IcicleCrash = new Move( 556, "Icicle Crash" );
public static Move Vcreate = new Move( 557, "V-create" );
public static Move FusionFlare = new Move( 558, "Fusion Flare" );
public static Move FusionBolt = new Move( 559, "Fusion Bolt" );
public static Move FlyingPress = new Move( 560, "Flying Press" );
public static Move MatBlock = new Move( 561, "Mat Block" );
public static Move Belch = new Move( 562, "Belch" );
public static Move Rototiller = new Move( 563, "Rototiller" );
public static Move StickyWeb = new Move( 564, "Sticky Web" );
public static Move FellStinger = new Move( 565, "Fell Stinger" );
public static Move PhantomForce = new Move( 566, "Phantom Force" );
public static Move TrickorTreat = new Move( 567, "Trick-or-Treat" );
public static Move NobleRoar = new Move( 568, "Noble Roar" );
public static Move IonDeluge = new Move( 569, "Ion Deluge" );
public static Move ParabolicCharge = new Move( 570, "Parabolic Charge" );
public static Move ForestsCurse = new Move( 571, "Forest’s Curse" );
public static Move PetalBlizzard = new Move( 572, "Petal Blizzard" );
public static Move FreezeDry = new Move( 573, "Freeze-Dry" );
public static Move DisarmingVoice = new Move( 574, "Disarming Voice" );
public static Move PartingShot = new Move( 575, "Parting Shot" );
public static Move TopsyTurvy = new Move( 576, "Topsy-Turvy" );
public static Move DrainingKiss = new Move( 577, "Draining Kiss" );
public static Move CraftyShield = new Move( 578, "Crafty Shield" );
public static Move FlowerShield = new Move( 579, "Flower Shield" );
public static Move GrassyTerrain = new Move( 580, "Grassy Terrain" );
public static Move MistyTerrain = new Move( 581, "Misty Terrain" );
public static Move Electrify = new Move( 582, "Electrify" );
public static Move PlayRough = new Move( 583, "Play Rough" );
public static Move FairyWind = new Move( 584, "Fairy Wind" );
public static Move Moonblast = new Move( 585, "Moonblast" );
public static Move Boomburst = new Move( 586, "Boomburst" );
public static Move FairyLock = new Move( 587, "Fairy Lock" );
public static Move KingsShield = new Move( 588, "King’s Shield" );
public static Move PlayNice = new Move( 589, "Play Nice" );
public static Move Confide = new Move( 590, "Confide" );
public static Move DiamondStorm = new Move( 591, "Diamond Storm" );
public static Move SteamEruption = new Move( 592, "Steam Eruption" );
public static Move HyperspaceHole = new Move( 593, "Hyperspace Hole" );
public static Move WaterShuriken = new Move( 594, "Water Shuriken" );
public static Move MysticalFire = new Move( 595, "Mystical Fire" );
public static Move SpikyShield = new Move( 596, "Spiky Shield" );
public static Move AromaticMist = new Move( 597, "Aromatic Mist" );
public static Move EerieImpulse = new Move( 598, "Eerie Impulse" );
public static Move VenomDrench = new Move( 599, "Venom Drench" );
public static Move Powder = new Move( 600, "Powder" );
public static Move Geomancy = new Move( 601, "Geomancy" );
public static Move MagneticFlux = new Move( 602, "Magnetic Flux" );
public static Move HappyHour = new Move( 603, "Happy Hour" );
public static Move ElectricTerrain = new Move( 604, "Electric Terrain" );
public static Move DazzlingGleam = new Move( 605, "Dazzling Gleam" );
public static Move Celebrate = new Move( 606, "Celebrate" );
public static Move HoldHands = new Move( 607, "Hold Hands" );
public static Move BabyDollEyes = new Move( 608, "Baby-Doll Eyes" );
public static Move Nuzzle = new Move( 609, "Nuzzle" );
public static Move HoldBack = new Move( 610, "Hold Back" );
public static Move Infestation = new Move( 611, "Infestation" );
public static Move PowerUpPunch = new Move( 612, "Power-Up Punch" );
public static Move OblivionWing = new Move( 613, "Oblivion Wing" );
public static Move ThousandArrows = new Move( 614, "Thousand Arrows" );
public static Move ThousandWaves = new Move( 615, "Thousand Waves" );
public static Move LandsWrath = new Move( 616, "Land’s Wrath" );
public static Move LightofRuin = new Move( 617, "Light of Ruin" );
public static Move OriginPulse = new Move( 618, "Origin Pulse" );
public static Move PrecipiceBlades = new Move( 619, "Precipice Blades" );
public static Move DragonAscent = new Move( 620, "Dragon Ascent" );
public static Move HyperspaceFury = new Move( 621, "Hyperspace Fury" );
public static Move GetValueFrom( int id ) => staticValues[ id ];
private static readonly Move[] staticValues = { null,
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
	HyperspaceFury };
public static IEnumerable<Move> AllMoves => staticValues.AsEnumerable();
}

public sealed class SpeciesType : BaseSpeciesType {
    public SpeciesType( int id, string name ) : base( id, name ) { }
    public static explicit operator SpeciesType( int id ) => Species.GetValueFrom( id );
    public static explicit operator int( SpeciesType val ) => val.Id;
}
public static partial class Species {
public static SpeciesType Egg = new SpeciesType( 0, "Egg" );
public static SpeciesType Bulbasaur = new SpeciesType( 1, "Bulbasaur" );
public static SpeciesType Ivysaur = new SpeciesType( 2, "Ivysaur" );
public static SpeciesType Venusaur = new SpeciesType( 3, "Venusaur" );
public static SpeciesType Charmander = new SpeciesType( 4, "Charmander" );
public static SpeciesType Charmeleon = new SpeciesType( 5, "Charmeleon" );
public static SpeciesType Charizard = new SpeciesType( 6, "Charizard" );
public static SpeciesType Squirtle = new SpeciesType( 7, "Squirtle" );
public static SpeciesType Wartortle = new SpeciesType( 8, "Wartortle" );
public static SpeciesType Blastoise = new SpeciesType( 9, "Blastoise" );
public static SpeciesType Caterpie = new SpeciesType( 10, "Caterpie" );
public static SpeciesType Metapod = new SpeciesType( 11, "Metapod" );
public static SpeciesType Butterfree = new SpeciesType( 12, "Butterfree" );
public static SpeciesType Weedle = new SpeciesType( 13, "Weedle" );
public static SpeciesType Kakuna = new SpeciesType( 14, "Kakuna" );
public static SpeciesType Beedrill = new SpeciesType( 15, "Beedrill" );
public static SpeciesType Pidgey = new SpeciesType( 16, "Pidgey" );
public static SpeciesType Pidgeotto = new SpeciesType( 17, "Pidgeotto" );
public static SpeciesType Pidgeot = new SpeciesType( 18, "Pidgeot" );
public static SpeciesType Rattata = new SpeciesType( 19, "Rattata" );
public static SpeciesType Raticate = new SpeciesType( 20, "Raticate" );
public static SpeciesType Spearow = new SpeciesType( 21, "Spearow" );
public static SpeciesType Fearow = new SpeciesType( 22, "Fearow" );
public static SpeciesType Ekans = new SpeciesType( 23, "Ekans" );
public static SpeciesType Arbok = new SpeciesType( 24, "Arbok" );
public static SpeciesType Pikachu = new SpeciesType( 25, "Pikachu" );
public static SpeciesType Raichu = new SpeciesType( 26, "Raichu" );
public static SpeciesType Sandshrew = new SpeciesType( 27, "Sandshrew" );
public static SpeciesType Sandslash = new SpeciesType( 28, "Sandslash" );
public static SpeciesType NidoranF = new SpeciesType( 29, "Nidoran♀" );
public static SpeciesType Nidorina = new SpeciesType( 30, "Nidorina" );
public static SpeciesType Nidoqueen = new SpeciesType( 31, "Nidoqueen" );
public static SpeciesType NidoranM = new SpeciesType( 32, "Nidoran♂" );
public static SpeciesType Nidorino = new SpeciesType( 33, "Nidorino" );
public static SpeciesType Nidoking = new SpeciesType( 34, "Nidoking" );
public static SpeciesType Clefairy = new SpeciesType( 35, "Clefairy" );
public static SpeciesType Clefable = new SpeciesType( 36, "Clefable" );
public static SpeciesType Vulpix = new SpeciesType( 37, "Vulpix" );
public static SpeciesType Ninetales = new SpeciesType( 38, "Ninetales" );
public static SpeciesType Jigglypuff = new SpeciesType( 39, "Jigglypuff" );
public static SpeciesType Wigglytuff = new SpeciesType( 40, "Wigglytuff" );
public static SpeciesType Zubat = new SpeciesType( 41, "Zubat" );
public static SpeciesType Golbat = new SpeciesType( 42, "Golbat" );
public static SpeciesType Oddish = new SpeciesType( 43, "Oddish" );
public static SpeciesType Gloom = new SpeciesType( 44, "Gloom" );
public static SpeciesType Vileplume = new SpeciesType( 45, "Vileplume" );
public static SpeciesType Paras = new SpeciesType( 46, "Paras" );
public static SpeciesType Parasect = new SpeciesType( 47, "Parasect" );
public static SpeciesType Venonat = new SpeciesType( 48, "Venonat" );
public static SpeciesType Venomoth = new SpeciesType( 49, "Venomoth" );
public static SpeciesType Diglett = new SpeciesType( 50, "Diglett" );
public static SpeciesType Dugtrio = new SpeciesType( 51, "Dugtrio" );
public static SpeciesType Meowth = new SpeciesType( 52, "Meowth" );
public static SpeciesType Persian = new SpeciesType( 53, "Persian" );
public static SpeciesType Psyduck = new SpeciesType( 54, "Psyduck" );
public static SpeciesType Golduck = new SpeciesType( 55, "Golduck" );
public static SpeciesType Mankey = new SpeciesType( 56, "Mankey" );
public static SpeciesType Primeape = new SpeciesType( 57, "Primeape" );
public static SpeciesType Growlithe = new SpeciesType( 58, "Growlithe" );
public static SpeciesType Arcanine = new SpeciesType( 59, "Arcanine" );
public static SpeciesType Poliwag = new SpeciesType( 60, "Poliwag" );
public static SpeciesType Poliwhirl = new SpeciesType( 61, "Poliwhirl" );
public static SpeciesType Poliwrath = new SpeciesType( 62, "Poliwrath" );
public static SpeciesType Abra = new SpeciesType( 63, "Abra" );
public static SpeciesType Kadabra = new SpeciesType( 64, "Kadabra" );
public static SpeciesType Alakazam = new SpeciesType( 65, "Alakazam" );
public static SpeciesType Machop = new SpeciesType( 66, "Machop" );
public static SpeciesType Machoke = new SpeciesType( 67, "Machoke" );
public static SpeciesType Machamp = new SpeciesType( 68, "Machamp" );
public static SpeciesType Bellsprout = new SpeciesType( 69, "Bellsprout" );
public static SpeciesType Weepinbell = new SpeciesType( 70, "Weepinbell" );
public static SpeciesType Victreebel = new SpeciesType( 71, "Victreebel" );
public static SpeciesType Tentacool = new SpeciesType( 72, "Tentacool" );
public static SpeciesType Tentacruel = new SpeciesType( 73, "Tentacruel" );
public static SpeciesType Geodude = new SpeciesType( 74, "Geodude" );
public static SpeciesType Graveler = new SpeciesType( 75, "Graveler" );
public static SpeciesType Golem = new SpeciesType( 76, "Golem" );
public static SpeciesType Ponyta = new SpeciesType( 77, "Ponyta" );
public static SpeciesType Rapidash = new SpeciesType( 78, "Rapidash" );
public static SpeciesType Slowpoke = new SpeciesType( 79, "Slowpoke" );
public static SpeciesType Slowbro = new SpeciesType( 80, "Slowbro" );
public static SpeciesType Magnemite = new SpeciesType( 81, "Magnemite" );
public static SpeciesType Magneton = new SpeciesType( 82, "Magneton" );
public static SpeciesType Farfetchd = new SpeciesType( 83, "Farfetch’d" );
public static SpeciesType Doduo = new SpeciesType( 84, "Doduo" );
public static SpeciesType Dodrio = new SpeciesType( 85, "Dodrio" );
public static SpeciesType Seel = new SpeciesType( 86, "Seel" );
public static SpeciesType Dewgong = new SpeciesType( 87, "Dewgong" );
public static SpeciesType Grimer = new SpeciesType( 88, "Grimer" );
public static SpeciesType Muk = new SpeciesType( 89, "Muk" );
public static SpeciesType Shellder = new SpeciesType( 90, "Shellder" );
public static SpeciesType Cloyster = new SpeciesType( 91, "Cloyster" );
public static SpeciesType Gastly = new SpeciesType( 92, "Gastly" );
public static SpeciesType Haunter = new SpeciesType( 93, "Haunter" );
public static SpeciesType Gengar = new SpeciesType( 94, "Gengar" );
public static SpeciesType Onix = new SpeciesType( 95, "Onix" );
public static SpeciesType Drowzee = new SpeciesType( 96, "Drowzee" );
public static SpeciesType Hypno = new SpeciesType( 97, "Hypno" );
public static SpeciesType Krabby = new SpeciesType( 98, "Krabby" );
public static SpeciesType Kingler = new SpeciesType( 99, "Kingler" );
public static SpeciesType Voltorb = new SpeciesType( 100, "Voltorb" );
public static SpeciesType Electrode = new SpeciesType( 101, "Electrode" );
public static SpeciesType Exeggcute = new SpeciesType( 102, "Exeggcute" );
public static SpeciesType Exeggutor = new SpeciesType( 103, "Exeggutor" );
public static SpeciesType Cubone = new SpeciesType( 104, "Cubone" );
public static SpeciesType Marowak = new SpeciesType( 105, "Marowak" );
public static SpeciesType Hitmonlee = new SpeciesType( 106, "Hitmonlee" );
public static SpeciesType Hitmonchan = new SpeciesType( 107, "Hitmonchan" );
public static SpeciesType Lickitung = new SpeciesType( 108, "Lickitung" );
public static SpeciesType Koffing = new SpeciesType( 109, "Koffing" );
public static SpeciesType Weezing = new SpeciesType( 110, "Weezing" );
public static SpeciesType Rhyhorn = new SpeciesType( 111, "Rhyhorn" );
public static SpeciesType Rhydon = new SpeciesType( 112, "Rhydon" );
public static SpeciesType Chansey = new SpeciesType( 113, "Chansey" );
public static SpeciesType Tangela = new SpeciesType( 114, "Tangela" );
public static SpeciesType Kangaskhan = new SpeciesType( 115, "Kangaskhan" );
public static SpeciesType Horsea = new SpeciesType( 116, "Horsea" );
public static SpeciesType Seadra = new SpeciesType( 117, "Seadra" );
public static SpeciesType Goldeen = new SpeciesType( 118, "Goldeen" );
public static SpeciesType Seaking = new SpeciesType( 119, "Seaking" );
public static SpeciesType Staryu = new SpeciesType( 120, "Staryu" );
public static SpeciesType Starmie = new SpeciesType( 121, "Starmie" );
public static SpeciesType MrMime = new SpeciesType( 122, "Mr. Mime" );
public static SpeciesType Scyther = new SpeciesType( 123, "Scyther" );
public static SpeciesType Jynx = new SpeciesType( 124, "Jynx" );
public static SpeciesType Electabuzz = new SpeciesType( 125, "Electabuzz" );
public static SpeciesType Magmar = new SpeciesType( 126, "Magmar" );
public static SpeciesType Pinsir = new SpeciesType( 127, "Pinsir" );
public static SpeciesType Tauros = new SpeciesType( 128, "Tauros" );
public static SpeciesType Magikarp = new SpeciesType( 129, "Magikarp" );
public static SpeciesType Gyarados = new SpeciesType( 130, "Gyarados" );
public static SpeciesType Lapras = new SpeciesType( 131, "Lapras" );
public static SpeciesType Ditto = new SpeciesType( 132, "Ditto" );
public static SpeciesType Eevee = new SpeciesType( 133, "Eevee" );
public static SpeciesType Vaporeon = new SpeciesType( 134, "Vaporeon" );
public static SpeciesType Jolteon = new SpeciesType( 135, "Jolteon" );
public static SpeciesType Flareon = new SpeciesType( 136, "Flareon" );
public static SpeciesType Porygon = new SpeciesType( 137, "Porygon" );
public static SpeciesType Omanyte = new SpeciesType( 138, "Omanyte" );
public static SpeciesType Omastar = new SpeciesType( 139, "Omastar" );
public static SpeciesType Kabuto = new SpeciesType( 140, "Kabuto" );
public static SpeciesType Kabutops = new SpeciesType( 141, "Kabutops" );
public static SpeciesType Aerodactyl = new SpeciesType( 142, "Aerodactyl" );
public static SpeciesType Snorlax = new SpeciesType( 143, "Snorlax" );
public static SpeciesType Articuno = new SpeciesType( 144, "Articuno" );
public static SpeciesType Zapdos = new SpeciesType( 145, "Zapdos" );
public static SpeciesType Moltres = new SpeciesType( 146, "Moltres" );
public static SpeciesType Dratini = new SpeciesType( 147, "Dratini" );
public static SpeciesType Dragonair = new SpeciesType( 148, "Dragonair" );
public static SpeciesType Dragonite = new SpeciesType( 149, "Dragonite" );
public static SpeciesType Mewtwo = new SpeciesType( 150, "Mewtwo" );
public static SpeciesType Mew = new SpeciesType( 151, "Mew" );
public static SpeciesType Chikorita = new SpeciesType( 152, "Chikorita" );
public static SpeciesType Bayleef = new SpeciesType( 153, "Bayleef" );
public static SpeciesType Meganium = new SpeciesType( 154, "Meganium" );
public static SpeciesType Cyndaquil = new SpeciesType( 155, "Cyndaquil" );
public static SpeciesType Quilava = new SpeciesType( 156, "Quilava" );
public static SpeciesType Typhlosion = new SpeciesType( 157, "Typhlosion" );
public static SpeciesType Totodile = new SpeciesType( 158, "Totodile" );
public static SpeciesType Croconaw = new SpeciesType( 159, "Croconaw" );
public static SpeciesType Feraligatr = new SpeciesType( 160, "Feraligatr" );
public static SpeciesType Sentret = new SpeciesType( 161, "Sentret" );
public static SpeciesType Furret = new SpeciesType( 162, "Furret" );
public static SpeciesType Hoothoot = new SpeciesType( 163, "Hoothoot" );
public static SpeciesType Noctowl = new SpeciesType( 164, "Noctowl" );
public static SpeciesType Ledyba = new SpeciesType( 165, "Ledyba" );
public static SpeciesType Ledian = new SpeciesType( 166, "Ledian" );
public static SpeciesType Spinarak = new SpeciesType( 167, "Spinarak" );
public static SpeciesType Ariados = new SpeciesType( 168, "Ariados" );
public static SpeciesType Crobat = new SpeciesType( 169, "Crobat" );
public static SpeciesType Chinchou = new SpeciesType( 170, "Chinchou" );
public static SpeciesType Lanturn = new SpeciesType( 171, "Lanturn" );
public static SpeciesType Pichu = new SpeciesType( 172, "Pichu" );
public static SpeciesType Cleffa = new SpeciesType( 173, "Cleffa" );
public static SpeciesType Igglybuff = new SpeciesType( 174, "Igglybuff" );
public static SpeciesType Togepi = new SpeciesType( 175, "Togepi" );
public static SpeciesType Togetic = new SpeciesType( 176, "Togetic" );
public static SpeciesType Natu = new SpeciesType( 177, "Natu" );
public static SpeciesType Xatu = new SpeciesType( 178, "Xatu" );
public static SpeciesType Mareep = new SpeciesType( 179, "Mareep" );
public static SpeciesType Flaaffy = new SpeciesType( 180, "Flaaffy" );
public static SpeciesType Ampharos = new SpeciesType( 181, "Ampharos" );
public static SpeciesType Bellossom = new SpeciesType( 182, "Bellossom" );
public static SpeciesType Marill = new SpeciesType( 183, "Marill" );
public static SpeciesType Azumarill = new SpeciesType( 184, "Azumarill" );
public static SpeciesType Sudowoodo = new SpeciesType( 185, "Sudowoodo" );
public static SpeciesType Politoed = new SpeciesType( 186, "Politoed" );
public static SpeciesType Hoppip = new SpeciesType( 187, "Hoppip" );
public static SpeciesType Skiploom = new SpeciesType( 188, "Skiploom" );
public static SpeciesType Jumpluff = new SpeciesType( 189, "Jumpluff" );
public static SpeciesType Aipom = new SpeciesType( 190, "Aipom" );
public static SpeciesType Sunkern = new SpeciesType( 191, "Sunkern" );
public static SpeciesType Sunflora = new SpeciesType( 192, "Sunflora" );
public static SpeciesType Yanma = new SpeciesType( 193, "Yanma" );
public static SpeciesType Wooper = new SpeciesType( 194, "Wooper" );
public static SpeciesType Quagsire = new SpeciesType( 195, "Quagsire" );
public static SpeciesType Espeon = new SpeciesType( 196, "Espeon" );
public static SpeciesType Umbreon = new SpeciesType( 197, "Umbreon" );
public static SpeciesType Murkrow = new SpeciesType( 198, "Murkrow" );
public static SpeciesType Slowking = new SpeciesType( 199, "Slowking" );
public static SpeciesType Misdreavus = new SpeciesType( 200, "Misdreavus" );
public static SpeciesType Unown = new SpeciesType( 201, "Unown" );
public static SpeciesType Wobbuffet = new SpeciesType( 202, "Wobbuffet" );
public static SpeciesType Girafarig = new SpeciesType( 203, "Girafarig" );
public static SpeciesType Pineco = new SpeciesType( 204, "Pineco" );
public static SpeciesType Forretress = new SpeciesType( 205, "Forretress" );
public static SpeciesType Dunsparce = new SpeciesType( 206, "Dunsparce" );
public static SpeciesType Gligar = new SpeciesType( 207, "Gligar" );
public static SpeciesType Steelix = new SpeciesType( 208, "Steelix" );
public static SpeciesType Snubbull = new SpeciesType( 209, "Snubbull" );
public static SpeciesType Granbull = new SpeciesType( 210, "Granbull" );
public static SpeciesType Qwilfish = new SpeciesType( 211, "Qwilfish" );
public static SpeciesType Scizor = new SpeciesType( 212, "Scizor" );
public static SpeciesType Shuckle = new SpeciesType( 213, "Shuckle" );
public static SpeciesType Heracross = new SpeciesType( 214, "Heracross" );
public static SpeciesType Sneasel = new SpeciesType( 215, "Sneasel" );
public static SpeciesType Teddiursa = new SpeciesType( 216, "Teddiursa" );
public static SpeciesType Ursaring = new SpeciesType( 217, "Ursaring" );
public static SpeciesType Slugma = new SpeciesType( 218, "Slugma" );
public static SpeciesType Magcargo = new SpeciesType( 219, "Magcargo" );
public static SpeciesType Swinub = new SpeciesType( 220, "Swinub" );
public static SpeciesType Piloswine = new SpeciesType( 221, "Piloswine" );
public static SpeciesType Corsola = new SpeciesType( 222, "Corsola" );
public static SpeciesType Remoraid = new SpeciesType( 223, "Remoraid" );
public static SpeciesType Octillery = new SpeciesType( 224, "Octillery" );
public static SpeciesType Delibird = new SpeciesType( 225, "Delibird" );
public static SpeciesType Mantine = new SpeciesType( 226, "Mantine" );
public static SpeciesType Skarmory = new SpeciesType( 227, "Skarmory" );
public static SpeciesType Houndour = new SpeciesType( 228, "Houndour" );
public static SpeciesType Houndoom = new SpeciesType( 229, "Houndoom" );
public static SpeciesType Kingdra = new SpeciesType( 230, "Kingdra" );
public static SpeciesType Phanpy = new SpeciesType( 231, "Phanpy" );
public static SpeciesType Donphan = new SpeciesType( 232, "Donphan" );
public static SpeciesType Porygon2 = new SpeciesType( 233, "Porygon2" );
public static SpeciesType Stantler = new SpeciesType( 234, "Stantler" );
public static SpeciesType Smeargle = new SpeciesType( 235, "Smeargle" );
public static SpeciesType Tyrogue = new SpeciesType( 236, "Tyrogue" );
public static SpeciesType Hitmontop = new SpeciesType( 237, "Hitmontop" );
public static SpeciesType Smoochum = new SpeciesType( 238, "Smoochum" );
public static SpeciesType Elekid = new SpeciesType( 239, "Elekid" );
public static SpeciesType Magby = new SpeciesType( 240, "Magby" );
public static SpeciesType Miltank = new SpeciesType( 241, "Miltank" );
public static SpeciesType Blissey = new SpeciesType( 242, "Blissey" );
public static SpeciesType Raikou = new SpeciesType( 243, "Raikou" );
public static SpeciesType Entei = new SpeciesType( 244, "Entei" );
public static SpeciesType Suicune = new SpeciesType( 245, "Suicune" );
public static SpeciesType Larvitar = new SpeciesType( 246, "Larvitar" );
public static SpeciesType Pupitar = new SpeciesType( 247, "Pupitar" );
public static SpeciesType Tyranitar = new SpeciesType( 248, "Tyranitar" );
public static SpeciesType Lugia = new SpeciesType( 249, "Lugia" );
public static SpeciesType HoOh = new SpeciesType( 250, "Ho-Oh" );
public static SpeciesType Celebi = new SpeciesType( 251, "Celebi" );
public static SpeciesType Treecko = new SpeciesType( 252, "Treecko" );
public static SpeciesType Grovyle = new SpeciesType( 253, "Grovyle" );
public static SpeciesType Sceptile = new SpeciesType( 254, "Sceptile" );
public static SpeciesType Torchic = new SpeciesType( 255, "Torchic" );
public static SpeciesType Combusken = new SpeciesType( 256, "Combusken" );
public static SpeciesType Blaziken = new SpeciesType( 257, "Blaziken" );
public static SpeciesType Mudkip = new SpeciesType( 258, "Mudkip" );
public static SpeciesType Marshtomp = new SpeciesType( 259, "Marshtomp" );
public static SpeciesType Swampert = new SpeciesType( 260, "Swampert" );
public static SpeciesType Poochyena = new SpeciesType( 261, "Poochyena" );
public static SpeciesType Mightyena = new SpeciesType( 262, "Mightyena" );
public static SpeciesType Zigzagoon = new SpeciesType( 263, "Zigzagoon" );
public static SpeciesType Linoone = new SpeciesType( 264, "Linoone" );
public static SpeciesType Wurmple = new SpeciesType( 265, "Wurmple" );
public static SpeciesType Silcoon = new SpeciesType( 266, "Silcoon" );
public static SpeciesType Beautifly = new SpeciesType( 267, "Beautifly" );
public static SpeciesType Cascoon = new SpeciesType( 268, "Cascoon" );
public static SpeciesType Dustox = new SpeciesType( 269, "Dustox" );
public static SpeciesType Lotad = new SpeciesType( 270, "Lotad" );
public static SpeciesType Lombre = new SpeciesType( 271, "Lombre" );
public static SpeciesType Ludicolo = new SpeciesType( 272, "Ludicolo" );
public static SpeciesType Seedot = new SpeciesType( 273, "Seedot" );
public static SpeciesType Nuzleaf = new SpeciesType( 274, "Nuzleaf" );
public static SpeciesType Shiftry = new SpeciesType( 275, "Shiftry" );
public static SpeciesType Taillow = new SpeciesType( 276, "Taillow" );
public static SpeciesType Swellow = new SpeciesType( 277, "Swellow" );
public static SpeciesType Wingull = new SpeciesType( 278, "Wingull" );
public static SpeciesType Pelipper = new SpeciesType( 279, "Pelipper" );
public static SpeciesType Ralts = new SpeciesType( 280, "Ralts" );
public static SpeciesType Kirlia = new SpeciesType( 281, "Kirlia" );
public static SpeciesType Gardevoir = new SpeciesType( 282, "Gardevoir" );
public static SpeciesType Surskit = new SpeciesType( 283, "Surskit" );
public static SpeciesType Masquerain = new SpeciesType( 284, "Masquerain" );
public static SpeciesType Shroomish = new SpeciesType( 285, "Shroomish" );
public static SpeciesType Breloom = new SpeciesType( 286, "Breloom" );
public static SpeciesType Slakoth = new SpeciesType( 287, "Slakoth" );
public static SpeciesType Vigoroth = new SpeciesType( 288, "Vigoroth" );
public static SpeciesType Slaking = new SpeciesType( 289, "Slaking" );
public static SpeciesType Nincada = new SpeciesType( 290, "Nincada" );
public static SpeciesType Ninjask = new SpeciesType( 291, "Ninjask" );
public static SpeciesType Shedinja = new SpeciesType( 292, "Shedinja" );
public static SpeciesType Whismur = new SpeciesType( 293, "Whismur" );
public static SpeciesType Loudred = new SpeciesType( 294, "Loudred" );
public static SpeciesType Exploud = new SpeciesType( 295, "Exploud" );
public static SpeciesType Makuhita = new SpeciesType( 296, "Makuhita" );
public static SpeciesType Hariyama = new SpeciesType( 297, "Hariyama" );
public static SpeciesType Azurill = new SpeciesType( 298, "Azurill" );
public static SpeciesType Nosepass = new SpeciesType( 299, "Nosepass" );
public static SpeciesType Skitty = new SpeciesType( 300, "Skitty" );
public static SpeciesType Delcatty = new SpeciesType( 301, "Delcatty" );
public static SpeciesType Sableye = new SpeciesType( 302, "Sableye" );
public static SpeciesType Mawile = new SpeciesType( 303, "Mawile" );
public static SpeciesType Aron = new SpeciesType( 304, "Aron" );
public static SpeciesType Lairon = new SpeciesType( 305, "Lairon" );
public static SpeciesType Aggron = new SpeciesType( 306, "Aggron" );
public static SpeciesType Meditite = new SpeciesType( 307, "Meditite" );
public static SpeciesType Medicham = new SpeciesType( 308, "Medicham" );
public static SpeciesType Electrike = new SpeciesType( 309, "Electrike" );
public static SpeciesType Manectric = new SpeciesType( 310, "Manectric" );
public static SpeciesType Plusle = new SpeciesType( 311, "Plusle" );
public static SpeciesType Minun = new SpeciesType( 312, "Minun" );
public static SpeciesType Volbeat = new SpeciesType( 313, "Volbeat" );
public static SpeciesType Illumise = new SpeciesType( 314, "Illumise" );
public static SpeciesType Roselia = new SpeciesType( 315, "Roselia" );
public static SpeciesType Gulpin = new SpeciesType( 316, "Gulpin" );
public static SpeciesType Swalot = new SpeciesType( 317, "Swalot" );
public static SpeciesType Carvanha = new SpeciesType( 318, "Carvanha" );
public static SpeciesType Sharpedo = new SpeciesType( 319, "Sharpedo" );
public static SpeciesType Wailmer = new SpeciesType( 320, "Wailmer" );
public static SpeciesType Wailord = new SpeciesType( 321, "Wailord" );
public static SpeciesType Numel = new SpeciesType( 322, "Numel" );
public static SpeciesType Camerupt = new SpeciesType( 323, "Camerupt" );
public static SpeciesType Torkoal = new SpeciesType( 324, "Torkoal" );
public static SpeciesType Spoink = new SpeciesType( 325, "Spoink" );
public static SpeciesType Grumpig = new SpeciesType( 326, "Grumpig" );
public static SpeciesType Spinda = new SpeciesType( 327, "Spinda" );
public static SpeciesType Trapinch = new SpeciesType( 328, "Trapinch" );
public static SpeciesType Vibrava = new SpeciesType( 329, "Vibrava" );
public static SpeciesType Flygon = new SpeciesType( 330, "Flygon" );
public static SpeciesType Cacnea = new SpeciesType( 331, "Cacnea" );
public static SpeciesType Cacturne = new SpeciesType( 332, "Cacturne" );
public static SpeciesType Swablu = new SpeciesType( 333, "Swablu" );
public static SpeciesType Altaria = new SpeciesType( 334, "Altaria" );
public static SpeciesType Zangoose = new SpeciesType( 335, "Zangoose" );
public static SpeciesType Seviper = new SpeciesType( 336, "Seviper" );
public static SpeciesType Lunatone = new SpeciesType( 337, "Lunatone" );
public static SpeciesType Solrock = new SpeciesType( 338, "Solrock" );
public static SpeciesType Barboach = new SpeciesType( 339, "Barboach" );
public static SpeciesType Whiscash = new SpeciesType( 340, "Whiscash" );
public static SpeciesType Corphish = new SpeciesType( 341, "Corphish" );
public static SpeciesType Crawdaunt = new SpeciesType( 342, "Crawdaunt" );
public static SpeciesType Baltoy = new SpeciesType( 343, "Baltoy" );
public static SpeciesType Claydol = new SpeciesType( 344, "Claydol" );
public static SpeciesType Lileep = new SpeciesType( 345, "Lileep" );
public static SpeciesType Cradily = new SpeciesType( 346, "Cradily" );
public static SpeciesType Anorith = new SpeciesType( 347, "Anorith" );
public static SpeciesType Armaldo = new SpeciesType( 348, "Armaldo" );
public static SpeciesType Feebas = new SpeciesType( 349, "Feebas" );
public static SpeciesType Milotic = new SpeciesType( 350, "Milotic" );
public static SpeciesType Castform = new SpeciesType( 351, "Castform" );
public static SpeciesType Kecleon = new SpeciesType( 352, "Kecleon" );
public static SpeciesType Shuppet = new SpeciesType( 353, "Shuppet" );
public static SpeciesType Banette = new SpeciesType( 354, "Banette" );
public static SpeciesType Duskull = new SpeciesType( 355, "Duskull" );
public static SpeciesType Dusclops = new SpeciesType( 356, "Dusclops" );
public static SpeciesType Tropius = new SpeciesType( 357, "Tropius" );
public static SpeciesType Chimecho = new SpeciesType( 358, "Chimecho" );
public static SpeciesType Absol = new SpeciesType( 359, "Absol" );
public static SpeciesType Wynaut = new SpeciesType( 360, "Wynaut" );
public static SpeciesType Snorunt = new SpeciesType( 361, "Snorunt" );
public static SpeciesType Glalie = new SpeciesType( 362, "Glalie" );
public static SpeciesType Spheal = new SpeciesType( 363, "Spheal" );
public static SpeciesType Sealeo = new SpeciesType( 364, "Sealeo" );
public static SpeciesType Walrein = new SpeciesType( 365, "Walrein" );
public static SpeciesType Clamperl = new SpeciesType( 366, "Clamperl" );
public static SpeciesType Huntail = new SpeciesType( 367, "Huntail" );
public static SpeciesType Gorebyss = new SpeciesType( 368, "Gorebyss" );
public static SpeciesType Relicanth = new SpeciesType( 369, "Relicanth" );
public static SpeciesType Luvdisc = new SpeciesType( 370, "Luvdisc" );
public static SpeciesType Bagon = new SpeciesType( 371, "Bagon" );
public static SpeciesType Shelgon = new SpeciesType( 372, "Shelgon" );
public static SpeciesType Salamence = new SpeciesType( 373, "Salamence" );
public static SpeciesType Beldum = new SpeciesType( 374, "Beldum" );
public static SpeciesType Metang = new SpeciesType( 375, "Metang" );
public static SpeciesType Metagross = new SpeciesType( 376, "Metagross" );
public static SpeciesType Regirock = new SpeciesType( 377, "Regirock" );
public static SpeciesType Regice = new SpeciesType( 378, "Regice" );
public static SpeciesType Registeel = new SpeciesType( 379, "Registeel" );
public static SpeciesType Latias = new SpeciesType( 380, "Latias" );
public static SpeciesType Latios = new SpeciesType( 381, "Latios" );
public static SpeciesType Kyogre = new SpeciesType( 382, "Kyogre" );
public static SpeciesType Groudon = new SpeciesType( 383, "Groudon" );
public static SpeciesType Rayquaza = new SpeciesType( 384, "Rayquaza" );
public static SpeciesType Jirachi = new SpeciesType( 385, "Jirachi" );
public static SpeciesType Deoxys = new SpeciesType( 386, "Deoxys" );
public static SpeciesType Turtwig = new SpeciesType( 387, "Turtwig" );
public static SpeciesType Grotle = new SpeciesType( 388, "Grotle" );
public static SpeciesType Torterra = new SpeciesType( 389, "Torterra" );
public static SpeciesType Chimchar = new SpeciesType( 390, "Chimchar" );
public static SpeciesType Monferno = new SpeciesType( 391, "Monferno" );
public static SpeciesType Infernape = new SpeciesType( 392, "Infernape" );
public static SpeciesType Piplup = new SpeciesType( 393, "Piplup" );
public static SpeciesType Prinplup = new SpeciesType( 394, "Prinplup" );
public static SpeciesType Empoleon = new SpeciesType( 395, "Empoleon" );
public static SpeciesType Starly = new SpeciesType( 396, "Starly" );
public static SpeciesType Staravia = new SpeciesType( 397, "Staravia" );
public static SpeciesType Staraptor = new SpeciesType( 398, "Staraptor" );
public static SpeciesType Bidoof = new SpeciesType( 399, "Bidoof" );
public static SpeciesType Bibarel = new SpeciesType( 400, "Bibarel" );
public static SpeciesType Kricketot = new SpeciesType( 401, "Kricketot" );
public static SpeciesType Kricketune = new SpeciesType( 402, "Kricketune" );
public static SpeciesType Shinx = new SpeciesType( 403, "Shinx" );
public static SpeciesType Luxio = new SpeciesType( 404, "Luxio" );
public static SpeciesType Luxray = new SpeciesType( 405, "Luxray" );
public static SpeciesType Budew = new SpeciesType( 406, "Budew" );
public static SpeciesType Roserade = new SpeciesType( 407, "Roserade" );
public static SpeciesType Cranidos = new SpeciesType( 408, "Cranidos" );
public static SpeciesType Rampardos = new SpeciesType( 409, "Rampardos" );
public static SpeciesType Shieldon = new SpeciesType( 410, "Shieldon" );
public static SpeciesType Bastiodon = new SpeciesType( 411, "Bastiodon" );
public static SpeciesType Burmy = new SpeciesType( 412, "Burmy" );
public static SpeciesType Wormadam = new SpeciesType( 413, "Wormadam" );
public static SpeciesType Mothim = new SpeciesType( 414, "Mothim" );
public static SpeciesType Combee = new SpeciesType( 415, "Combee" );
public static SpeciesType Vespiquen = new SpeciesType( 416, "Vespiquen" );
public static SpeciesType Pachirisu = new SpeciesType( 417, "Pachirisu" );
public static SpeciesType Buizel = new SpeciesType( 418, "Buizel" );
public static SpeciesType Floatzel = new SpeciesType( 419, "Floatzel" );
public static SpeciesType Cherubi = new SpeciesType( 420, "Cherubi" );
public static SpeciesType Cherrim = new SpeciesType( 421, "Cherrim" );
public static SpeciesType Shellos = new SpeciesType( 422, "Shellos" );
public static SpeciesType Gastrodon = new SpeciesType( 423, "Gastrodon" );
public static SpeciesType Ambipom = new SpeciesType( 424, "Ambipom" );
public static SpeciesType Drifloon = new SpeciesType( 425, "Drifloon" );
public static SpeciesType Drifblim = new SpeciesType( 426, "Drifblim" );
public static SpeciesType Buneary = new SpeciesType( 427, "Buneary" );
public static SpeciesType Lopunny = new SpeciesType( 428, "Lopunny" );
public static SpeciesType Mismagius = new SpeciesType( 429, "Mismagius" );
public static SpeciesType Honchkrow = new SpeciesType( 430, "Honchkrow" );
public static SpeciesType Glameow = new SpeciesType( 431, "Glameow" );
public static SpeciesType Purugly = new SpeciesType( 432, "Purugly" );
public static SpeciesType Chingling = new SpeciesType( 433, "Chingling" );
public static SpeciesType Stunky = new SpeciesType( 434, "Stunky" );
public static SpeciesType Skuntank = new SpeciesType( 435, "Skuntank" );
public static SpeciesType Bronzor = new SpeciesType( 436, "Bronzor" );
public static SpeciesType Bronzong = new SpeciesType( 437, "Bronzong" );
public static SpeciesType Bonsly = new SpeciesType( 438, "Bonsly" );
public static SpeciesType MimeJr = new SpeciesType( 439, "Mime Jr." );
public static SpeciesType Happiny = new SpeciesType( 440, "Happiny" );
public static SpeciesType Chatot = new SpeciesType( 441, "Chatot" );
public static SpeciesType Spiritomb = new SpeciesType( 442, "Spiritomb" );
public static SpeciesType Gible = new SpeciesType( 443, "Gible" );
public static SpeciesType Gabite = new SpeciesType( 444, "Gabite" );
public static SpeciesType Garchomp = new SpeciesType( 445, "Garchomp" );
public static SpeciesType Munchlax = new SpeciesType( 446, "Munchlax" );
public static SpeciesType Riolu = new SpeciesType( 447, "Riolu" );
public static SpeciesType Lucario = new SpeciesType( 448, "Lucario" );
public static SpeciesType Hippopotas = new SpeciesType( 449, "Hippopotas" );
public static SpeciesType Hippowdon = new SpeciesType( 450, "Hippowdon" );
public static SpeciesType Skorupi = new SpeciesType( 451, "Skorupi" );
public static SpeciesType Drapion = new SpeciesType( 452, "Drapion" );
public static SpeciesType Croagunk = new SpeciesType( 453, "Croagunk" );
public static SpeciesType Toxicroak = new SpeciesType( 454, "Toxicroak" );
public static SpeciesType Carnivine = new SpeciesType( 455, "Carnivine" );
public static SpeciesType Finneon = new SpeciesType( 456, "Finneon" );
public static SpeciesType Lumineon = new SpeciesType( 457, "Lumineon" );
public static SpeciesType Mantyke = new SpeciesType( 458, "Mantyke" );
public static SpeciesType Snover = new SpeciesType( 459, "Snover" );
public static SpeciesType Abomasnow = new SpeciesType( 460, "Abomasnow" );
public static SpeciesType Weavile = new SpeciesType( 461, "Weavile" );
public static SpeciesType Magnezone = new SpeciesType( 462, "Magnezone" );
public static SpeciesType Lickilicky = new SpeciesType( 463, "Lickilicky" );
public static SpeciesType Rhyperior = new SpeciesType( 464, "Rhyperior" );
public static SpeciesType Tangrowth = new SpeciesType( 465, "Tangrowth" );
public static SpeciesType Electivire = new SpeciesType( 466, "Electivire" );
public static SpeciesType Magmortar = new SpeciesType( 467, "Magmortar" );
public static SpeciesType Togekiss = new SpeciesType( 468, "Togekiss" );
public static SpeciesType Yanmega = new SpeciesType( 469, "Yanmega" );
public static SpeciesType Leafeon = new SpeciesType( 470, "Leafeon" );
public static SpeciesType Glaceon = new SpeciesType( 471, "Glaceon" );
public static SpeciesType Gliscor = new SpeciesType( 472, "Gliscor" );
public static SpeciesType Mamoswine = new SpeciesType( 473, "Mamoswine" );
public static SpeciesType PorygonZ = new SpeciesType( 474, "Porygon-Z" );
public static SpeciesType Gallade = new SpeciesType( 475, "Gallade" );
public static SpeciesType Probopass = new SpeciesType( 476, "Probopass" );
public static SpeciesType Dusknoir = new SpeciesType( 477, "Dusknoir" );
public static SpeciesType Froslass = new SpeciesType( 478, "Froslass" );
public static SpeciesType Rotom = new SpeciesType( 479, "Rotom" );
public static SpeciesType Uxie = new SpeciesType( 480, "Uxie" );
public static SpeciesType Mesprit = new SpeciesType( 481, "Mesprit" );
public static SpeciesType Azelf = new SpeciesType( 482, "Azelf" );
public static SpeciesType Dialga = new SpeciesType( 483, "Dialga" );
public static SpeciesType Palkia = new SpeciesType( 484, "Palkia" );
public static SpeciesType Heatran = new SpeciesType( 485, "Heatran" );
public static SpeciesType Regigigas = new SpeciesType( 486, "Regigigas" );
public static SpeciesType Giratina = new SpeciesType( 487, "Giratina" );
public static SpeciesType Cresselia = new SpeciesType( 488, "Cresselia" );
public static SpeciesType Phione = new SpeciesType( 489, "Phione" );
public static SpeciesType Manaphy = new SpeciesType( 490, "Manaphy" );
public static SpeciesType Darkrai = new SpeciesType( 491, "Darkrai" );
public static SpeciesType Shaymin = new SpeciesType( 492, "Shaymin" );
public static SpeciesType Arceus = new SpeciesType( 493, "Arceus" );
public static SpeciesType Victini = new SpeciesType( 494, "Victini" );
public static SpeciesType Snivy = new SpeciesType( 495, "Snivy" );
public static SpeciesType Servine = new SpeciesType( 496, "Servine" );
public static SpeciesType Serperior = new SpeciesType( 497, "Serperior" );
public static SpeciesType Tepig = new SpeciesType( 498, "Tepig" );
public static SpeciesType Pignite = new SpeciesType( 499, "Pignite" );
public static SpeciesType Emboar = new SpeciesType( 500, "Emboar" );
public static SpeciesType Oshawott = new SpeciesType( 501, "Oshawott" );
public static SpeciesType Dewott = new SpeciesType( 502, "Dewott" );
public static SpeciesType Samurott = new SpeciesType( 503, "Samurott" );
public static SpeciesType Patrat = new SpeciesType( 504, "Patrat" );
public static SpeciesType Watchog = new SpeciesType( 505, "Watchog" );
public static SpeciesType Lillipup = new SpeciesType( 506, "Lillipup" );
public static SpeciesType Herdier = new SpeciesType( 507, "Herdier" );
public static SpeciesType Stoutland = new SpeciesType( 508, "Stoutland" );
public static SpeciesType Purrloin = new SpeciesType( 509, "Purrloin" );
public static SpeciesType Liepard = new SpeciesType( 510, "Liepard" );
public static SpeciesType Pansage = new SpeciesType( 511, "Pansage" );
public static SpeciesType Simisage = new SpeciesType( 512, "Simisage" );
public static SpeciesType Pansear = new SpeciesType( 513, "Pansear" );
public static SpeciesType Simisear = new SpeciesType( 514, "Simisear" );
public static SpeciesType Panpour = new SpeciesType( 515, "Panpour" );
public static SpeciesType Simipour = new SpeciesType( 516, "Simipour" );
public static SpeciesType Munna = new SpeciesType( 517, "Munna" );
public static SpeciesType Musharna = new SpeciesType( 518, "Musharna" );
public static SpeciesType Pidove = new SpeciesType( 519, "Pidove" );
public static SpeciesType Tranquill = new SpeciesType( 520, "Tranquill" );
public static SpeciesType Unfezant = new SpeciesType( 521, "Unfezant" );
public static SpeciesType Blitzle = new SpeciesType( 522, "Blitzle" );
public static SpeciesType Zebstrika = new SpeciesType( 523, "Zebstrika" );
public static SpeciesType Roggenrola = new SpeciesType( 524, "Roggenrola" );
public static SpeciesType Boldore = new SpeciesType( 525, "Boldore" );
public static SpeciesType Gigalith = new SpeciesType( 526, "Gigalith" );
public static SpeciesType Woobat = new SpeciesType( 527, "Woobat" );
public static SpeciesType Swoobat = new SpeciesType( 528, "Swoobat" );
public static SpeciesType Drilbur = new SpeciesType( 529, "Drilbur" );
public static SpeciesType Excadrill = new SpeciesType( 530, "Excadrill" );
public static SpeciesType Audino = new SpeciesType( 531, "Audino" );
public static SpeciesType Timburr = new SpeciesType( 532, "Timburr" );
public static SpeciesType Gurdurr = new SpeciesType( 533, "Gurdurr" );
public static SpeciesType Conkeldurr = new SpeciesType( 534, "Conkeldurr" );
public static SpeciesType Tympole = new SpeciesType( 535, "Tympole" );
public static SpeciesType Palpitoad = new SpeciesType( 536, "Palpitoad" );
public static SpeciesType Seismitoad = new SpeciesType( 537, "Seismitoad" );
public static SpeciesType Throh = new SpeciesType( 538, "Throh" );
public static SpeciesType Sawk = new SpeciesType( 539, "Sawk" );
public static SpeciesType Sewaddle = new SpeciesType( 540, "Sewaddle" );
public static SpeciesType Swadloon = new SpeciesType( 541, "Swadloon" );
public static SpeciesType Leavanny = new SpeciesType( 542, "Leavanny" );
public static SpeciesType Venipede = new SpeciesType( 543, "Venipede" );
public static SpeciesType Whirlipede = new SpeciesType( 544, "Whirlipede" );
public static SpeciesType Scolipede = new SpeciesType( 545, "Scolipede" );
public static SpeciesType Cottonee = new SpeciesType( 546, "Cottonee" );
public static SpeciesType Whimsicott = new SpeciesType( 547, "Whimsicott" );
public static SpeciesType Petilil = new SpeciesType( 548, "Petilil" );
public static SpeciesType Lilligant = new SpeciesType( 549, "Lilligant" );
public static SpeciesType Basculin = new SpeciesType( 550, "Basculin" );
public static SpeciesType Sandile = new SpeciesType( 551, "Sandile" );
public static SpeciesType Krokorok = new SpeciesType( 552, "Krokorok" );
public static SpeciesType Krookodile = new SpeciesType( 553, "Krookodile" );
public static SpeciesType Darumaka = new SpeciesType( 554, "Darumaka" );
public static SpeciesType Darmanitan = new SpeciesType( 555, "Darmanitan" );
public static SpeciesType Maractus = new SpeciesType( 556, "Maractus" );
public static SpeciesType Dwebble = new SpeciesType( 557, "Dwebble" );
public static SpeciesType Crustle = new SpeciesType( 558, "Crustle" );
public static SpeciesType Scraggy = new SpeciesType( 559, "Scraggy" );
public static SpeciesType Scrafty = new SpeciesType( 560, "Scrafty" );
public static SpeciesType Sigilyph = new SpeciesType( 561, "Sigilyph" );
public static SpeciesType Yamask = new SpeciesType( 562, "Yamask" );
public static SpeciesType Cofagrigus = new SpeciesType( 563, "Cofagrigus" );
public static SpeciesType Tirtouga = new SpeciesType( 564, "Tirtouga" );
public static SpeciesType Carracosta = new SpeciesType( 565, "Carracosta" );
public static SpeciesType Archen = new SpeciesType( 566, "Archen" );
public static SpeciesType Archeops = new SpeciesType( 567, "Archeops" );
public static SpeciesType Trubbish = new SpeciesType( 568, "Trubbish" );
public static SpeciesType Garbodor = new SpeciesType( 569, "Garbodor" );
public static SpeciesType Zorua = new SpeciesType( 570, "Zorua" );
public static SpeciesType Zoroark = new SpeciesType( 571, "Zoroark" );
public static SpeciesType Minccino = new SpeciesType( 572, "Minccino" );
public static SpeciesType Cinccino = new SpeciesType( 573, "Cinccino" );
public static SpeciesType Gothita = new SpeciesType( 574, "Gothita" );
public static SpeciesType Gothorita = new SpeciesType( 575, "Gothorita" );
public static SpeciesType Gothitelle = new SpeciesType( 576, "Gothitelle" );
public static SpeciesType Solosis = new SpeciesType( 577, "Solosis" );
public static SpeciesType Duosion = new SpeciesType( 578, "Duosion" );
public static SpeciesType Reuniclus = new SpeciesType( 579, "Reuniclus" );
public static SpeciesType Ducklett = new SpeciesType( 580, "Ducklett" );
public static SpeciesType Swanna = new SpeciesType( 581, "Swanna" );
public static SpeciesType Vanillite = new SpeciesType( 582, "Vanillite" );
public static SpeciesType Vanillish = new SpeciesType( 583, "Vanillish" );
public static SpeciesType Vanilluxe = new SpeciesType( 584, "Vanilluxe" );
public static SpeciesType Deerling = new SpeciesType( 585, "Deerling" );
public static SpeciesType Sawsbuck = new SpeciesType( 586, "Sawsbuck" );
public static SpeciesType Emolga = new SpeciesType( 587, "Emolga" );
public static SpeciesType Karrablast = new SpeciesType( 588, "Karrablast" );
public static SpeciesType Escavalier = new SpeciesType( 589, "Escavalier" );
public static SpeciesType Foongus = new SpeciesType( 590, "Foongus" );
public static SpeciesType Amoonguss = new SpeciesType( 591, "Amoonguss" );
public static SpeciesType Frillish = new SpeciesType( 592, "Frillish" );
public static SpeciesType Jellicent = new SpeciesType( 593, "Jellicent" );
public static SpeciesType Alomomola = new SpeciesType( 594, "Alomomola" );
public static SpeciesType Joltik = new SpeciesType( 595, "Joltik" );
public static SpeciesType Galvantula = new SpeciesType( 596, "Galvantula" );
public static SpeciesType Ferroseed = new SpeciesType( 597, "Ferroseed" );
public static SpeciesType Ferrothorn = new SpeciesType( 598, "Ferrothorn" );
public static SpeciesType Klink = new SpeciesType( 599, "Klink" );
public static SpeciesType Klang = new SpeciesType( 600, "Klang" );
public static SpeciesType Klinklang = new SpeciesType( 601, "Klinklang" );
public static SpeciesType Tynamo = new SpeciesType( 602, "Tynamo" );
public static SpeciesType Eelektrik = new SpeciesType( 603, "Eelektrik" );
public static SpeciesType Eelektross = new SpeciesType( 604, "Eelektross" );
public static SpeciesType Elgyem = new SpeciesType( 605, "Elgyem" );
public static SpeciesType Beheeyem = new SpeciesType( 606, "Beheeyem" );
public static SpeciesType Litwick = new SpeciesType( 607, "Litwick" );
public static SpeciesType Lampent = new SpeciesType( 608, "Lampent" );
public static SpeciesType Chandelure = new SpeciesType( 609, "Chandelure" );
public static SpeciesType Axew = new SpeciesType( 610, "Axew" );
public static SpeciesType Fraxure = new SpeciesType( 611, "Fraxure" );
public static SpeciesType Haxorus = new SpeciesType( 612, "Haxorus" );
public static SpeciesType Cubchoo = new SpeciesType( 613, "Cubchoo" );
public static SpeciesType Beartic = new SpeciesType( 614, "Beartic" );
public static SpeciesType Cryogonal = new SpeciesType( 615, "Cryogonal" );
public static SpeciesType Shelmet = new SpeciesType( 616, "Shelmet" );
public static SpeciesType Accelgor = new SpeciesType( 617, "Accelgor" );
public static SpeciesType Stunfisk = new SpeciesType( 618, "Stunfisk" );
public static SpeciesType Mienfoo = new SpeciesType( 619, "Mienfoo" );
public static SpeciesType Mienshao = new SpeciesType( 620, "Mienshao" );
public static SpeciesType Druddigon = new SpeciesType( 621, "Druddigon" );
public static SpeciesType Golett = new SpeciesType( 622, "Golett" );
public static SpeciesType Golurk = new SpeciesType( 623, "Golurk" );
public static SpeciesType Pawniard = new SpeciesType( 624, "Pawniard" );
public static SpeciesType Bisharp = new SpeciesType( 625, "Bisharp" );
public static SpeciesType Bouffalant = new SpeciesType( 626, "Bouffalant" );
public static SpeciesType Rufflet = new SpeciesType( 627, "Rufflet" );
public static SpeciesType Braviary = new SpeciesType( 628, "Braviary" );
public static SpeciesType Vullaby = new SpeciesType( 629, "Vullaby" );
public static SpeciesType Mandibuzz = new SpeciesType( 630, "Mandibuzz" );
public static SpeciesType Heatmor = new SpeciesType( 631, "Heatmor" );
public static SpeciesType Durant = new SpeciesType( 632, "Durant" );
public static SpeciesType Deino = new SpeciesType( 633, "Deino" );
public static SpeciesType Zweilous = new SpeciesType( 634, "Zweilous" );
public static SpeciesType Hydreigon = new SpeciesType( 635, "Hydreigon" );
public static SpeciesType Larvesta = new SpeciesType( 636, "Larvesta" );
public static SpeciesType Volcarona = new SpeciesType( 637, "Volcarona" );
public static SpeciesType Cobalion = new SpeciesType( 638, "Cobalion" );
public static SpeciesType Terrakion = new SpeciesType( 639, "Terrakion" );
public static SpeciesType Virizion = new SpeciesType( 640, "Virizion" );
public static SpeciesType Tornadus = new SpeciesType( 641, "Tornadus" );
public static SpeciesType Thundurus = new SpeciesType( 642, "Thundurus" );
public static SpeciesType Reshiram = new SpeciesType( 643, "Reshiram" );
public static SpeciesType Zekrom = new SpeciesType( 644, "Zekrom" );
public static SpeciesType Landorus = new SpeciesType( 645, "Landorus" );
public static SpeciesType Kyurem = new SpeciesType( 646, "Kyurem" );
public static SpeciesType Keldeo = new SpeciesType( 647, "Keldeo" );
public static SpeciesType Meloetta = new SpeciesType( 648, "Meloetta" );
public static SpeciesType Genesect = new SpeciesType( 649, "Genesect" );
public static SpeciesType Chespin = new SpeciesType( 650, "Chespin" );
public static SpeciesType Quilladin = new SpeciesType( 651, "Quilladin" );
public static SpeciesType Chesnaught = new SpeciesType( 652, "Chesnaught" );
public static SpeciesType Fennekin = new SpeciesType( 653, "Fennekin" );
public static SpeciesType Braixen = new SpeciesType( 654, "Braixen" );
public static SpeciesType Delphox = new SpeciesType( 655, "Delphox" );
public static SpeciesType Froakie = new SpeciesType( 656, "Froakie" );
public static SpeciesType Frogadier = new SpeciesType( 657, "Frogadier" );
public static SpeciesType Greninja = new SpeciesType( 658, "Greninja" );
public static SpeciesType Bunnelby = new SpeciesType( 659, "Bunnelby" );
public static SpeciesType Diggersby = new SpeciesType( 660, "Diggersby" );
public static SpeciesType Fletchling = new SpeciesType( 661, "Fletchling" );
public static SpeciesType Fletchinder = new SpeciesType( 662, "Fletchinder" );
public static SpeciesType Talonflame = new SpeciesType( 663, "Talonflame" );
public static SpeciesType Scatterbug = new SpeciesType( 664, "Scatterbug" );
public static SpeciesType Spewpa = new SpeciesType( 665, "Spewpa" );
public static SpeciesType Vivillon = new SpeciesType( 666, "Vivillon" );
public static SpeciesType Litleo = new SpeciesType( 667, "Litleo" );
public static SpeciesType Pyroar = new SpeciesType( 668, "Pyroar" );
public static SpeciesType Flabb = new SpeciesType( 669, "Flabébé" );
public static SpeciesType Floette = new SpeciesType( 670, "Floette" );
public static SpeciesType Florges = new SpeciesType( 671, "Florges" );
public static SpeciesType Skiddo = new SpeciesType( 672, "Skiddo" );
public static SpeciesType Gogoat = new SpeciesType( 673, "Gogoat" );
public static SpeciesType Pancham = new SpeciesType( 674, "Pancham" );
public static SpeciesType Pangoro = new SpeciesType( 675, "Pangoro" );
public static SpeciesType Furfrou = new SpeciesType( 676, "Furfrou" );
public static SpeciesType Espurr = new SpeciesType( 677, "Espurr" );
public static SpeciesType Meowstic = new SpeciesType( 678, "Meowstic" );
public static SpeciesType Honedge = new SpeciesType( 679, "Honedge" );
public static SpeciesType Doublade = new SpeciesType( 680, "Doublade" );
public static SpeciesType Aegislash = new SpeciesType( 681, "Aegislash" );
public static SpeciesType Spritzee = new SpeciesType( 682, "Spritzee" );
public static SpeciesType Aromatisse = new SpeciesType( 683, "Aromatisse" );
public static SpeciesType Swirlix = new SpeciesType( 684, "Swirlix" );
public static SpeciesType Slurpuff = new SpeciesType( 685, "Slurpuff" );
public static SpeciesType Inkay = new SpeciesType( 686, "Inkay" );
public static SpeciesType Malamar = new SpeciesType( 687, "Malamar" );
public static SpeciesType Binacle = new SpeciesType( 688, "Binacle" );
public static SpeciesType Barbaracle = new SpeciesType( 689, "Barbaracle" );
public static SpeciesType Skrelp = new SpeciesType( 690, "Skrelp" );
public static SpeciesType Dragalge = new SpeciesType( 691, "Dragalge" );
public static SpeciesType Clauncher = new SpeciesType( 692, "Clauncher" );
public static SpeciesType Clawitzer = new SpeciesType( 693, "Clawitzer" );
public static SpeciesType Helioptile = new SpeciesType( 694, "Helioptile" );
public static SpeciesType Heliolisk = new SpeciesType( 695, "Heliolisk" );
public static SpeciesType Tyrunt = new SpeciesType( 696, "Tyrunt" );
public static SpeciesType Tyrantrum = new SpeciesType( 697, "Tyrantrum" );
public static SpeciesType Amaura = new SpeciesType( 698, "Amaura" );
public static SpeciesType Aurorus = new SpeciesType( 699, "Aurorus" );
public static SpeciesType Sylveon = new SpeciesType( 700, "Sylveon" );
public static SpeciesType Hawlucha = new SpeciesType( 701, "Hawlucha" );
public static SpeciesType Dedenne = new SpeciesType( 702, "Dedenne" );
public static SpeciesType Carbink = new SpeciesType( 703, "Carbink" );
public static SpeciesType Goomy = new SpeciesType( 704, "Goomy" );
public static SpeciesType Sliggoo = new SpeciesType( 705, "Sliggoo" );
public static SpeciesType Goodra = new SpeciesType( 706, "Goodra" );
public static SpeciesType Klefki = new SpeciesType( 707, "Klefki" );
public static SpeciesType Phantump = new SpeciesType( 708, "Phantump" );
public static SpeciesType Trevenant = new SpeciesType( 709, "Trevenant" );
public static SpeciesType Pumpkaboo = new SpeciesType( 710, "Pumpkaboo" );
public static SpeciesType Gourgeist = new SpeciesType( 711, "Gourgeist" );
public static SpeciesType Bergmite = new SpeciesType( 712, "Bergmite" );
public static SpeciesType Avalugg = new SpeciesType( 713, "Avalugg" );
public static SpeciesType Noibat = new SpeciesType( 714, "Noibat" );
public static SpeciesType Noivern = new SpeciesType( 715, "Noivern" );
public static SpeciesType Xerneas = new SpeciesType( 716, "Xerneas" );
public static SpeciesType Yveltal = new SpeciesType( 717, "Yveltal" );
public static SpeciesType Zygarde = new SpeciesType( 718, "Zygarde" );
public static SpeciesType Diancie = new SpeciesType( 719, "Diancie" );
public static SpeciesType Hoopa = new SpeciesType( 720, "Hoopa" );
public static SpeciesType Volcanion = new SpeciesType( 721, "Volcanion" );
public static SpeciesType GetValueFrom( int id ) => staticValues[ id ];
private static readonly SpeciesType[] staticValues = { Egg,
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
	Volcanion };
public static IEnumerable<SpeciesType> AllSpecies => staticValues.AsEnumerable();
}

public sealed class PokemonType : BasePokemonType {
    public PokemonType( int id, string name ) : base( id, name ) { }
    public static explicit operator PokemonType( int id ) => PokemonTypes.GetValueFrom( id );
    public static explicit operator int( PokemonType val ) => val.Id;
}
public static partial class PokemonTypes {
public static PokemonType Normal = new PokemonType( 0, "Normal" );
public static PokemonType Fighting = new PokemonType( 1, "Fighting" );
public static PokemonType Flying = new PokemonType( 2, "Flying" );
public static PokemonType Poison = new PokemonType( 3, "Poison" );
public static PokemonType Ground = new PokemonType( 4, "Ground" );
public static PokemonType Rock = new PokemonType( 5, "Rock" );
public static PokemonType Bug = new PokemonType( 6, "Bug" );
public static PokemonType Ghost = new PokemonType( 7, "Ghost" );
public static PokemonType Steel = new PokemonType( 8, "Steel" );
public static PokemonType Fire = new PokemonType( 9, "Fire" );
public static PokemonType Water = new PokemonType( 10, "Water" );
public static PokemonType Grass = new PokemonType( 11, "Grass" );
public static PokemonType Electric = new PokemonType( 12, "Electric" );
public static PokemonType Psychic = new PokemonType( 13, "Psychic" );
public static PokemonType Ice = new PokemonType( 14, "Ice" );
public static PokemonType Dragon = new PokemonType( 15, "Dragon" );
public static PokemonType Dark = new PokemonType( 16, "Dark" );
public static PokemonType Fairy = new PokemonType( 17, "Fairy" );
public static PokemonType GetValueFrom( int id ) => staticValues[ id ];
private static readonly PokemonType[] staticValues = { Normal,
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
	Fairy };
public static IEnumerable<PokemonType> AllPokemonTypes => staticValues.AsEnumerable();
}

}