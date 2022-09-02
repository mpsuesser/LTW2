using System.Collections.Generic;

public enum TraitType {
    FastProducing = 0,
    DeathPact,
    UnholySacrifice,
    Exoskeleton, // armored (1)
    DevotionAura,
    FelBlood, // health regen 200%
    EnduranceAura,
    Flying,
    SolidRock, // maybe rework, mud golem resistance
    Attacker,
    LingeringVoid,
    Boss,
    BasicSpellResistance,
    BasicSpellResistanceWithNoMentionOfImmunity,
    Chainmail, // armored (2)
    VengefulReturn, // death pact (2)
    AncientAura,
    Dash,
    ChaoticVoid,
    SuperEnduranceAura, // maybe rename
    SuperDash, // maybe rename
    SuperDevotionAura, // maybe rename
    SuperUnholySacrifice, // maybe rename
    Bombardment,
    Geomancy,
    MegaDeathPact,
    NecroticTransfusion,
    MegaEnduranceAura,
    MegaDash,
    Skittering,
    DeathReinstated, // death pact (3)
    SpiritualResistance, // armored (3)
    EtherealAura,
    MajorSpellResistance,
    MegaUnholySacrifice,
    HardenedSkin,
    MegaAncientAura,
    MegaDevotionAura,
    EarthShield,
    MegaGeomancy,
    UltimateDash,
    ChaosPortal,
    ChaosEmpowerment,
    StoneskinFortitude,
    GoblinEngineering,
    ReactiveArmor,
    EngineOverload,
    Hypothermia,
    WarStance,
    UltimateEnduranceAura,
    UltimateDevotionAura,
    UltimateUnholySacrifice,
    VolatileDeath,
    LegendarySpellResistance,
    
    Inactive,
    ThoughtfulReaction, // arcane
    ExplosiveReaction, // fire
    EssenceOfNatureAura, // earth
    EssenceOfFrostAura, // ice
    EssenceOfPowerAura, // lightning
    EssenceOfTheSeaAura, // water
    EssenceOfLightAura, // holy
    EssenceOfBlightAura, // unholy
    EssenceOfDarknessAura, // void
    AdvancedThoughtfulReaction, // arcane
    AdvancedExplosiveReaction, // fire
    AdvancedEssenceOfNatureAura, // earth
    AdvancedEssenceOfFrostAura, // ice
    AdvancedEssenceOfPowerAura, // lightning
    AdvancedEssenceOfTheSeaAura, // water
    AdvancedEssenceOfLightAura, // holy
    AdvancedEssenceOfBlightAura, // unholy
    AdvancedEssenceOfDarknessAura, // void
}

public class TraitDescriptor {
    public string Name { get; }
    public string Description { get; }
    public List<BuffType> AssociatedBuffs { get; }

    public TraitDescriptor(
        string name,
        string desc,
        params BuffType[] buffTypes
    ) {
        Description = desc;
        AssociatedBuffs = new List<BuffType>(buffTypes);
    }
}

public static class TraitConstants {
    public const int FastProducingInitialStockAmount = 3;
    public const int FastProducingRestockInterval = 3;
    
    
    
    // To be replaced with a buff in the descriptor
    private const string BI = "%B"; // BI = buff indicator

    public static Dictionary<TraitType, TraitDescriptor> Descriptor
        = new Dictionary<TraitType, TraitDescriptor> {
        {
            TraitType.FastProducing,
            new TraitDescriptor(
                "Fast Producing",
                $"The initial stock of this unit after the delay is {FastProducingInitialStockAmount} and stock replenishes more quickly, at a rate of once every {FastProducingRestockInterval} seconds."
            )
        },
        {
            TraitType.DevotionAura,
            new TraitDescriptor(
                "Devotion Aura",
                $"Applies {BI} to all creeps within a {BuffConstants.DevotionAuraRange} radius.",
                BuffType.Devotion
            )
        },
        {
            TraitType.EnduranceAura,
            new TraitDescriptor(
                "Endurance Aura",
                $"Applies {BI} to all creeps within a {BuffConstants.EnduranceAuraRange} radius.",
                BuffType.Endurance
            )
        },
        {
            TraitType.EssenceOfNatureAura,
            new TraitDescriptor(
                "Essence of Nature Aura",
                $"Applies {BI} to all towers within a {BuffConstants.EarthTechnologyDiscAuraRange}",
                BuffType.EssenceOfNature
            )
        }
    };

    public static Dictionary<EnemyType, TraitType[]> EnemyTraitMap
        = new Dictionary<EnemyType, TraitType[]> {
            { EnemyType.Sheep, new TraitType[] { TraitType.FastProducing } },
            { EnemyType.Wolf, new TraitType[] { TraitType.FastProducing } },
            { EnemyType.Skeleton, new TraitType[] { TraitType.DeathPact } },
            { EnemyType.Acolyte, new TraitType[] { TraitType.UnholySacrifice } },
            { EnemyType.Spider, new TraitType[] { TraitType.Exoskeleton } },
            { EnemyType.Swordsman, new TraitType[] { TraitType.DevotionAura } },
            { EnemyType.Grunt, new TraitType[] { TraitType.FelBlood } },
            { EnemyType.Temptress, new TraitType[] { TraitType.EnduranceAura } },
            { EnemyType.Shade, new TraitType[] { TraitType.Flying } },
            { EnemyType.MudGolem, new TraitType[] { TraitType.BasicSpellResistance } },
            { EnemyType.Treant, new TraitType[] { TraitType.Attacker, TraitType.LingeringVoid } },
            { EnemyType.RotGolem, new TraitType[] { TraitType.Boss, TraitType.BasicSpellResistanceWithNoMentionOfImmunity } },
            { EnemyType.Knight, new TraitType[] { TraitType.Chainmail } },
            { EnemyType.VengefulSpirit, new TraitType[] { TraitType.VengefulReturn, TraitType.AncientAura } },
            { EnemyType.ForestTroll, new TraitType[] { TraitType.Dash } },
            { EnemyType.Wyvern, new TraitType[] { TraitType.Flying } },
            { EnemyType.Voidwalker, new TraitType[] { TraitType.ChaoticVoid } },
            { EnemyType.FacelessOne, new TraitType[] { TraitType.EnduranceAura } },
            { EnemyType.Dragonspawn, new TraitType[] { TraitType.SuperDash, TraitType.BasicSpellResistance } },
            { EnemyType.SeaTurtle, new TraitType[] { TraitType.SuperDevotionAura } },
            { EnemyType.Banshee, new TraitType[] { TraitType.Flying, TraitType.SuperUnholySacrifice } },
            { EnemyType.SiegeEngine, new TraitType[] { TraitType.Attacker, TraitType.Bombardment } },
            { EnemyType.KoboldGeomancer, new TraitType[] { TraitType.Geomancy } },
            { EnemyType.Infernal, new TraitType[] { TraitType.Boss, TraitType.BasicSpellResistanceWithNoMentionOfImmunity } },
            { EnemyType.DeathRevenant, new TraitType[] { TraitType.DeathReinstated, TraitType.NecroticTransfusion } },
            { EnemyType.SatyrShadowdancer, new TraitType[] { TraitType.MegaEnduranceAura } },
            { EnemyType.CryptFiend, new TraitType[] { TraitType.MegaDash, TraitType.Skittering } },
            { EnemyType.SpiritWalker, new TraitType[] { TraitType.SpiritualResistance, TraitType.EtherealAura } },
            { EnemyType.Necromancer, new TraitType[] { TraitType.MajorSpellResistance, TraitType.MegaUnholySacrifice } },
            { EnemyType.AncientWendigo, new TraitType[] { TraitType.HardenedSkin, TraitType.AncientAura } },
            { EnemyType.GryphonRider, new TraitType[] { TraitType.Flying, TraitType.MegaDevotionAura } },
            { EnemyType.OgreMagi, new TraitType[] { TraitType.EarthShield, TraitType.MegaGeomancy } },
            { EnemyType.Abomination, new TraitType[] { TraitType.UltimateDash } },
            { EnemyType.ChaosWarden, new TraitType[] { TraitType.ChaosPortal, TraitType.ChaosEmpowerment } },
            { EnemyType.MountainGiant, new TraitType[] { TraitType.Attacker, TraitType.StoneskinFortitude } },
            { EnemyType.GoblinShredder, new TraitType[] { TraitType.GoblinEngineering, TraitType.ReactiveArmor, TraitType.EngineOverload } },
            { EnemyType.KodoRider, new TraitType[] { TraitType.Hypothermia, TraitType.WarStance, TraitType.UltimateEnduranceAura, TraitType.UltimateDevotionAura } },
            { EnemyType.FrostWyrm, new TraitType[] { TraitType.Flying, TraitType.MajorSpellResistance, TraitType.UltimateUnholySacrifice, TraitType.NecroticTransfusion } },
            { EnemyType.Phoenix, new TraitType[] { TraitType.Boss, TraitType.Flying, TraitType.Attacker, TraitType.VolatileDeath, TraitType.LegendarySpellResistance } },
        };

    public static Dictionary<TowerType, TraitType[]> TowerTraitMap
        = new Dictionary<TowerType, TraitType[]> {
            // Ranged base
            {TowerType.Archer, new TraitType[] {} },
            {TowerType.Gunner, new TraitType[] {} },
            {TowerType.WatchTower, new TraitType[] {} },
            {TowerType.GuardTower, new TraitType[] {} },
            {TowerType.WardTower, new TraitType[] {} },
            {TowerType.UltimateWardTower, new TraitType[] {} },
            {TowerType.CannonTower, new TraitType[] {} },
            {TowerType.BombardTower, new TraitType[] {} },
            {TowerType.ArtilleryTower, new TraitType[] {} },
            {TowerType.UltimateArtilleryTower, new TraitType[] {} },

            // Melee base
            {TowerType.Cutter, new TraitType[] {} },
            {TowerType.Grinder, new TraitType[] {} },
            {TowerType.Carver, new TraitType[] {} },
            {TowerType.Executioner, new TraitType[] {} },
            {TowerType.Mauler, new TraitType[] {} },
            {TowerType.UltimateMauler, new TraitType[] {} },
            {TowerType.Crusher, new TraitType[] {} },
            {TowerType.Wrecker, new TraitType[] {} },
            {TowerType.Mangler, new TraitType[] {} },
            {TowerType.UltimateMangler, new TraitType[] {} },

            // Elemental core
            {TowerType.ElementalCore, new TraitType[] {} },

            // Arcane
            {TowerType.Spellslinger, new TraitType[] {} },
            {TowerType.SpellslingerMaster, new TraitType[] {} },
            {TowerType.ArcanePylon, new TraitType[] {} },
            {TowerType.ArcaneRepository, new TraitType[] {} },
            {TowerType.Archmage, new TraitType[] {} },
            {TowerType.GrandArchmage, new TraitType[] {} },
            {TowerType.UltimateKirinTorWizard, new TraitType[] {} },
            {TowerType.UltimateArcaneOrb, new TraitType[] {} },

            // Fire
            {TowerType.FirePit, new TraitType[] {} },
            {TowerType.MagmaWell, new TraitType[] {} },
            {TowerType.LivingFlame, new TraitType[] {} },
            {TowerType.Hellfire, new TraitType[] {} },
            {TowerType.UltimateFirelord, new TraitType[] {} },
            {TowerType.MeteorAttractor, new TraitType[] {} },
            {TowerType.Armageddon, new TraitType[] {} },
            {TowerType.UltimateMoonbeamProjector, new TraitType[] {} },

            // Ice
            {TowerType.FrozenObelisk, new TraitType[] {} },
            {TowerType.RunicObelisk, new TraitType[] {} },
            {TowerType.FrozenWatcher, new TraitType[] {} },
            {TowerType.FrozenCore, new TraitType[] {} },
            {TowerType.UltimateLich, new TraitType[] {} },
            {TowerType.Icicle, new TraitType[] {} },
            {TowerType.Tricicle, new TraitType[] {} },
            {TowerType.UltimateCrystal, new TraitType[] {} },

            // Earth
            {TowerType.Rockfall, new TraitType[] {} },
            {TowerType.Avalanche, new TraitType[] {} },
            {TowerType.EarthGuardian, new TraitType[] {} },
            {TowerType.AncientProtector, new TraitType[] {} },
            {TowerType.UltimateAncientWarden, new TraitType[] {} },
            {TowerType.NoxiousWeed, new TraitType[] {} },
            {TowerType.NoxiousThorn, new TraitType[] {} },
            {TowerType.UltimateScorpion, new TraitType[] {} },

            // Lightning
            {TowerType.ShockParticle, new TraitType[] {} },
            {TowerType.ShockGenerator, new TraitType[] {} },
            {TowerType.Voltage, new TraitType[] {} },
            {TowerType.HighVoltage, new TraitType[] {} },
            {TowerType.UltimateOrbKeeper, new TraitType[] {} },
            {TowerType.LightningBeacon, new TraitType[] {} },
            {TowerType.LightningGenerator, new TraitType[] {} },
            {TowerType.UltimateAnnihilationGlyph, new TraitType[] {} },

            // Holy
            {TowerType.LightFlies, new TraitType[] {} },
            {TowerType.HolyLantern, new TraitType[] {} },
            {TowerType.SunrayTower, new TraitType[] {} },
            {TowerType.SunbeamTower, new TraitType[] {} },
            {TowerType.UltimateTitanVault, new TraitType[] {} },
            {TowerType.Glowshroom, new TraitType[] {} },
            {TowerType.Lightshroom, new TraitType[] {} },
            {TowerType.UltimateDivineshroom, new TraitType[] {} },

            // Water
            {TowerType.Splasher, new TraitType[] {} },
            {TowerType.Tidecaller, new TraitType[] {} },
            {TowerType.WaterElemental, new TraitType[] {} },
            {TowerType.SeaElemental, new TraitType[] {} },
            {TowerType.UltimateHurricaneElemental, new TraitType[] {} },
            {TowerType.TideLurker, new TraitType[] {} },
            {TowerType.AbyssStalker, new TraitType[] {} },
            {TowerType.UltimateSludgeMonstrosity, new TraitType[] {} },

            // Unholy
            {TowerType.PlagueWell, new TraitType[] {} },
            {TowerType.Catacomb, new TraitType[] {} },
            {TowerType.SepticTank, new TraitType[] {} },
            {TowerType.PlagueFanatic, new TraitType[] {} },
            {TowerType.UltimateGravedigger, new TraitType[] {} },
            {TowerType.ObsidianDestroyer, new TraitType[] {} },
            {TowerType.DecayedHorror, new TraitType[] {} },
            {TowerType.UltimateDiabolist, new TraitType[] {} },

            // Void
            {TowerType.Voidling, new TraitType[] {} },
            {TowerType.Voidalisk, new TraitType[] {} },
            {TowerType.Riftweaver, new TraitType[] {} },
            {TowerType.RiftLord, new TraitType[] {} },
            {TowerType.UltimateDevourer, new TraitType[] {} },
            {TowerType.Lasher, new TraitType[] {} },
            {TowerType.Ravager, new TraitType[] {} },
            {TowerType.UltimateLeviathan, new TraitType[] {} },

            // Disc
            {TowerType.TechnologyDisc, new TraitType[] { TraitType.Inactive } },
            {TowerType.TechnologyDisc_Arcane, new TraitType[] { TraitType.ThoughtfulReaction } },
            {TowerType.TechnologyDisc_Fire, new TraitType[] { TraitType.ExplosiveReaction } },
            {TowerType.TechnologyDisc_Ice, new TraitType[] { TraitType.EssenceOfFrostAura } },
            {TowerType.TechnologyDisc_Earth, new TraitType[] { TraitType.EssenceOfNatureAura } },
            {TowerType.TechnologyDisc_Lightning, new TraitType[] { TraitType.EssenceOfPowerAura } },
            {TowerType.TechnologyDisc_Holy, new TraitType[] { TraitType.EssenceOfLightAura } },
            {TowerType.TechnologyDisc_Water, new TraitType[] { TraitType.EssenceOfTheSeaAura } },
            {TowerType.TechnologyDisc_Unholy, new TraitType[] { TraitType.EssenceOfBlightAura } },
            {TowerType.TechnologyDisc_Void, new TraitType[] { TraitType.EssenceOfDarknessAura } },
            {TowerType.TechnologyDisc_Arcane_Ultimate, new TraitType[] { TraitType.AdvancedThoughtfulReaction } },
            {TowerType.TechnologyDisc_Fire_Ultimate, new TraitType[] { TraitType.AdvancedExplosiveReaction } },
            {TowerType.TechnologyDisc_Ice_Ultimate, new TraitType[] { TraitType.AdvancedEssenceOfFrostAura } },
            {TowerType.TechnologyDisc_Earth_Ultimate, new TraitType[] { TraitType.AdvancedEssenceOfNatureAura } },
            {TowerType.TechnologyDisc_Lightning_Ultimate, new TraitType[] { TraitType.AdvancedEssenceOfPowerAura } },
            {TowerType.TechnologyDisc_Holy_Ultimate, new TraitType[] { TraitType.AdvancedEssenceOfLightAura } },
            {TowerType.TechnologyDisc_Water_Ultimate, new TraitType[] { TraitType.AdvancedEssenceOfTheSeaAura } },
            {TowerType.TechnologyDisc_Unholy_Ultimate, new TraitType[] { TraitType.AdvancedEssenceOfBlightAura } },
            {TowerType.TechnologyDisc_Void_Ultimate, new TraitType[] { TraitType.AdvancedEssenceOfDarknessAura } },
        };
}