using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class ServerPrefabs : SingletonBehaviour<ServerPrefabs>
{
    public static Dictionary<EnemyType, ServerEnemy> Enemies { get; set; }
    public static Dictionary<TowerType, ServerTower> Towers { get; set; }
    
    [Space(10)]
    [Header("Enemies")]
    [SerializeField] private ServerSheep pfSheep;
    [SerializeField] private ServerWolf pfWolf;
    [SerializeField] private ServerSkeleton pfSkeleton;
    [SerializeField] private ServerAcolyte pfAcolyte;
    [SerializeField] private ServerSpider pfSpider;
    [SerializeField] private ServerSwordsman pfSwordsman;
    [SerializeField] private ServerGrunt pfGrunt;
    [SerializeField] private ServerTemptress pfTemptress;
    [SerializeField] private ServerShade pfShade;
    [SerializeField] private ServerMudGolem pfMudGolem;
    [SerializeField] private ServerTreant pfTreant;
    [SerializeField] private ServerRotGolem pfRotGolem;
    [SerializeField] private ServerKnight pfKnight;
    [SerializeField] private ServerVengefulSpirit pfVengefulSpirit;
    [SerializeField] private ServerForestTroll pfForestTroll;
    [SerializeField] private ServerWyvern pfWyvern;
    [SerializeField] private ServerVoidwalker pfVoidwalker;
    [SerializeField] private ServerFacelessOne pfFacelessOne;
    [SerializeField] private ServerDragonspawn pfDragonspawn;
    [SerializeField] private ServerSeaTurtle pfSeaTurtle;
    [SerializeField] private ServerBanshee pfBanshee;
    [SerializeField] private ServerSiegeEngine pfSiegeEngine;
    [SerializeField] private ServerKoboldGeomancer pfKoboldGeomancer;
    [SerializeField] private ServerInfernal pfInfernal;
    [SerializeField] private ServerDeathRevenant pfDeathRevenant;
    [SerializeField] private ServerSatyrShadowdancer pfSatyrShadowdancer;
    [SerializeField] private ServerCryptFiend pfCryptFiend;
    [SerializeField] private ServerSpiritWalker pfSpiritWalker;
    [SerializeField] private ServerNecromancer pfNecromancer;
    [SerializeField] private ServerAncientWendigo pfAncientWendigo;
    [SerializeField] private ServerGryphonRider pfGryphonRider;
    [SerializeField] private ServerOgreMagi pfOgreMagi;
    [SerializeField] private ServerAbomination pfAbomination;
    [SerializeField] private ServerChaosWarden pfChaosWarden;
    [SerializeField] private ServerMountainGiant pfMountainGiant;
    [SerializeField] private ServerGoblinShredder pfGoblinShredder;
    [SerializeField] private ServerKodoRider pfKodoRider;
    [SerializeField] private ServerFrostWyrm pfFrostWyrm;
    [SerializeField] private ServerPhoenix pfPhoenix;

    // Towers
    [Space(4)]
    [Header("Towers")]
    [Space(5)]
    [Header("Archer Base")]
    [SerializeField] private ServerArcher pfArcher;
    [SerializeField] private ServerGunner pfGunner;
    [SerializeField] private ServerWatchTower pfWatchTower;
    [SerializeField] private ServerGuardTower pfGuardTower;
    [SerializeField] private ServerWardTower pfWardTower;
    [SerializeField] private ServerUltimateWardTower pfUltimateWardTower;
    [SerializeField] private ServerCannonTower pfCannonTower;
    [SerializeField] private ServerBombardTower pfBombardTower;
    [SerializeField] private ServerArtilleryTower pfArtilleryTower;
    [SerializeField] private ServerUltimateArtilleryTower pfUltimateArtilleryTower;

    [Space(5)]
    [Header("Cutter Base")]
    [SerializeField] private ServerCutter pfCutter;
    [SerializeField] private ServerGrinder pfGrinder;
    [SerializeField] private ServerCarver pfCarver;
    [SerializeField] private ServerExecutioner pfExecutioner;
    [SerializeField] private ServerMauler pfMauler;
    [SerializeField] private ServerUltimateMauler pfUltimateMauler;
    [SerializeField] private ServerCrusher pfCrusher;
    [SerializeField] private ServerWrecker pfWrecker;
    [SerializeField] private ServerMangler pfMangler;
    [SerializeField] private ServerUltimateMangler pfUltimateMangler;

    [Space(5)]
    [Header("Elemental Core Base")]
    [SerializeField] private ServerElementalCore pfElementalCore;
    
    [Space(3)]
    [Header("Arcane")]
    [SerializeField] private ServerSpellslinger pfSpellslinger;
    [SerializeField] private ServerSpellslingerMaster pfSpellslingerMaster;
    [SerializeField] private ServerArcanePylon pfArcanePylon;
    [SerializeField] private ServerArcaneRepository pfArcaneRepository;
    [SerializeField] private ServerArchmage pfArchmage;
    [SerializeField] private ServerGrandArchmage pfGrandArchmage;
    [SerializeField] private ServerUltimateKirinTorWizard pfUltimateKirinTorWizard;
    [SerializeField] private ServerUltimateArcaneOrb pfUltimateArcaneOrb;
    
    [Space(3)]
    [Header("Earth")]
    [SerializeField] private ServerRockfall pfRockfall;
    [SerializeField] private ServerAvalanche pfAvalanche;
    [SerializeField] private ServerEarthGuardian pfEarthGuardian;
    [SerializeField] private ServerAncientProtector pfAncientProtector;
    [SerializeField] private ServerUltimateAncientWarden pfUltimateAncientWarden;
    [SerializeField] private ServerNoxiousWeed pfNoxiousWeed;
    [SerializeField] private ServerNoxiousThorn pfNoxiousThorn;
    [SerializeField] private ServerUltimateScorpion pfUltimateScorpion;
    
    [Space(3)]
    [Header("Fire")]
    [SerializeField] private ServerFirePit pfFirePit;
    [SerializeField] private ServerMagmaWell pfMagmaWell;
    [SerializeField] private ServerLivingFlame pfLivingFlame;
    [SerializeField] private ServerHellfire pfHellfire;
    [SerializeField] private ServerUltimateFirelord pfUltimateFirelord;
    [SerializeField] private ServerMeteorAttractor pfMeteorAttractor;
    [SerializeField] private ServerArmageddon pfArmageddon;
    [SerializeField] private ServerUltimateMoonbeamProjector pfUltimateMoonbeamProjector;
    
    [Space(3)]
    [Header("Holy")]
    [SerializeField] private ServerLightFlies pfLightFlies;
    [SerializeField] private ServerHolyLantern pfHolyLantern;
    [SerializeField] private ServerSunrayTower pfSunrayTower;
    [SerializeField] private ServerSunbeamTower pfSunbeamTower;
    [SerializeField] private ServerUltimateTitanVault pfUltimateTitanVault;
    [SerializeField] private ServerGlowshroom pfGlowshroom;
    [SerializeField] private ServerLightshroom pfLightshroom;
    [SerializeField] private ServerUltimateDivineshroom pfUltimateDivineshroom;
    
    [Space(3)]
    [Header("Ice")]
    [SerializeField] private ServerFrozenObelisk pfFrozenObelisk;
    [SerializeField] private ServerRunicObelisk pfRunicObelisk;
    [SerializeField] private ServerFrozenWatcher pfFrozenWatcher;
    [SerializeField] private ServerFrozenCore pfFrozenCore;
    [SerializeField] private ServerUltimateLich pfUltimateLich;
    [SerializeField] private ServerIcicle pfIcicle;
    [SerializeField] private ServerTricicle pfTricicle;
    [SerializeField] private ServerUltimateCrystal pfUltimateCrystal;

    [Space(3)]
    [Header("Lightning")]
    [SerializeField] private ServerShockParticle pfShockParticle;
    [SerializeField] private ServerShockGenerator pfShockGenerator;
    [SerializeField] private ServerVoltage pfVoltage;
    [SerializeField] private ServerHighVoltage pfHighVoltage;
    [SerializeField] private ServerUltimateOrbKeeper pfUltimateOrbKeeper;
    [SerializeField] private ServerLightningBeacon pfLightningBeacon;
    [SerializeField] private ServerLightningGenerator pfLightningGenerator;
    [SerializeField] private ServerUltimateAnnihilationGlyph pfUltimateAnnihilationGlyph;
    
    [Space(3)]
    [Header("Unholy")]
    [SerializeField] private ServerPlagueWell pfPlagueWell;
    [SerializeField] private ServerCatacomb pfCatacomb;
    [SerializeField] private ServerSepticTank pfSepticTank;
    [SerializeField] private ServerPlagueFanatic pfPlagueFanatic;
    [SerializeField] private ServerUltimateGravedigger pfUltimateGravedigger;
    [SerializeField] private ServerObsidianDestroyer pfObsidianDestroyer;
    [SerializeField] private ServerDecayedHorror pfDecayedHorror;
    [SerializeField] private ServerUltimateDiabolist pfUltimateDiabolist;
    
    [Space(3)]
    [Header("Void")]
    [SerializeField] private ServerVoidling pfVoidling;
    [SerializeField] private ServerVoidalisk pfVoidalisk;
    [SerializeField] private ServerRiftweaver pfRiftweaver;
    [SerializeField] private ServerRiftLord pfRiftLord;
    [SerializeField] private ServerUltimateDevourer pfUltimateDevourer;
    [SerializeField] private ServerLasher pfLasher;
    [SerializeField] private ServerRavager pfRavager;
    [SerializeField] private ServerUltimateLeviathan pfUltimateLeviathan;
    
    [Space(3)]
    [Header("Water")]
    [SerializeField] private ServerSplasher pfSplasher;
    [SerializeField] private ServerTidecaller pfTidecaller;
    [SerializeField] private ServerWaterElemental pfWaterElemental;
    [SerializeField] private ServerSeaElemental pfSeaElemental;
    [SerializeField] private ServerUltimateHurricaneElemental pfUltimateHurricaneElemental;
    [SerializeField] private ServerTideLurker pfTideLurker;
    [SerializeField] private ServerAbyssStalker pfAbyssStalker;
    [SerializeField] private ServerUltimateSludgeMonstrosity pfUltimateSludgeMonstrosity;

    [Space(5)]
    [Header("Technology Disc Base")]
    [SerializeField] private ServerTechnologyDisc pfTechnologyDisc;
    [SerializeField] private ServerTechnologyDisc_Arcane pfTechnologyDisc_Arcane;
    [SerializeField] private ServerTechnologyDisc_Fire pfTechnologyDisc_Fire;
    [SerializeField] private ServerTechnologyDisc_Ice pfTechnologyDisc_Ice;
    [SerializeField] private ServerTechnologyDisc_Earth pfTechnologyDisc_Earth;
    [SerializeField] private ServerTechnologyDisc_Lightning pfTechnologyDisc_Lightning;
    [SerializeField] private ServerTechnologyDisc_Holy pfTechnologyDisc_Holy;
    [SerializeField] private ServerTechnologyDisc_Water pfTechnologyDisc_Water;
    [SerializeField] private ServerTechnologyDisc_Unholy pfTechnologyDisc_Unholy;
    [SerializeField] private ServerTechnologyDisc_Void pfTechnologyDisc_Void;
    [SerializeField] private ServerTechnologyDisc_Arcane_Ultimate pfTechnologyDisc_Arcane_Ultimate;
    [SerializeField] private ServerTechnologyDisc_Fire_Ultimate pfTechnologyDisc_Fire_Ultimate;
    [SerializeField] private ServerTechnologyDisc_Ice_Ultimate pfTechnologyDisc_Ice_Ultimate;
    [SerializeField] private ServerTechnologyDisc_Earth_Ultimate pfTechnologyDisc_Earth_Ultimate;
    [SerializeField] private ServerTechnologyDisc_Lightning_Ultimate pfTechnologyDisc_Lightning_Ultimate;
    [SerializeField] private ServerTechnologyDisc_Holy_Ultimate pfTechnologyDisc_Holy_Ultimate;
    [SerializeField] private ServerTechnologyDisc_Water_Ultimate pfTechnologyDisc_Water_Ultimate;
    [SerializeField] private ServerTechnologyDisc_Unholy_Ultimate pfTechnologyDisc_Unholy_Ultimate;
    [SerializeField] private ServerTechnologyDisc_Void_Ultimate pfTechnologyDisc_Void_Ultimate;
    
    [Space(5)]
    [Header("Builder")]
    [SerializeField] public ServerBuilder pfBuilder;
    
    [Space(10)]
    
    [Header("Buff Applicator")]
    [SerializeField] public ProximityBuffApplicator pfProximityBuffApplicator;

    [FormerlySerializedAs("pfProjectile")]
    [Header("Projectiles")]
    [SerializeField] public ProjectileBehaviour pfArrowProjectile;
    [SerializeField] public ProjectileBehaviour pfUniversalProjectile;
    
    [Header("NavMesh Testers")]
    [SerializeField] public NavMeshInvisiblePathBlocker pfNavMeshInvisiblePathBlocker;
    [SerializeField] public NavMeshInvisiblePathTester pfNavMeshInvisiblePathTester;

    [Header("Misc")]
    [SerializeField] public MeteoricGroundBurner pfMeteoricGroundBurner;
    [SerializeField] public CrushingWave pfCrushingWave;
    [SerializeField] public DiscStepTrigger pfDiscStepTrigger;
    [SerializeField] public TowerBuildLocationTrigger pfTowerBuildLocationTrigger;
    
    private void Awake() {
        InitializeSingleton(this);

        Enemies = new Dictionary<EnemyType, ServerEnemy>() {
            { EnemyType.Sheep, pfSheep },
            { EnemyType.Wolf, pfWolf },
            { EnemyType.Skeleton, pfSkeleton },
            { EnemyType.Acolyte, pfAcolyte },
            { EnemyType.Spider, pfSpider },
            { EnemyType.Swordsman, pfSwordsman },
            { EnemyType.Grunt, pfGrunt },
            { EnemyType.Temptress, pfTemptress },
            { EnemyType.Shade, pfShade },
            { EnemyType.MudGolem, pfMudGolem },
            { EnemyType.Treant, pfTreant },
            { EnemyType.RotGolem, pfRotGolem },
            { EnemyType.Knight, pfKnight },
            { EnemyType.VengefulSpirit, pfVengefulSpirit },
            { EnemyType.ForestTroll, pfForestTroll },
            { EnemyType.Wyvern, pfWyvern },
            { EnemyType.Voidwalker, pfVoidwalker },
            { EnemyType.FacelessOne, pfFacelessOne },
            { EnemyType.Dragonspawn, pfDragonspawn },
            { EnemyType.SeaTurtle, pfSeaTurtle },
            { EnemyType.Banshee, pfBanshee },
            { EnemyType.SiegeEngine, pfSiegeEngine },
            { EnemyType.KoboldGeomancer, pfKoboldGeomancer },
            { EnemyType.Infernal, pfInfernal },
            { EnemyType.DeathRevenant, pfDeathRevenant },
            { EnemyType.SatyrShadowdancer, pfSatyrShadowdancer },
            { EnemyType.CryptFiend, pfCryptFiend },
            { EnemyType.SpiritWalker, pfSpiritWalker },
            { EnemyType.Necromancer, pfNecromancer },
            { EnemyType.AncientWendigo, pfAncientWendigo },
            { EnemyType.GryphonRider, pfGryphonRider },
            { EnemyType.OgreMagi, pfOgreMagi },
            { EnemyType.Abomination, pfAbomination },
            { EnemyType.ChaosWarden, pfChaosWarden },
            { EnemyType.MountainGiant, pfMountainGiant },
            { EnemyType.GoblinShredder, pfGoblinShredder },
            { EnemyType.KodoRider, pfKodoRider },
            { EnemyType.FrostWyrm, pfFrostWyrm },
            { EnemyType.Phoenix, pfPhoenix },
        };

        Towers = new Dictionary<TowerType, ServerTower>() {
            { TowerType.Archer, pfArcher },
            { TowerType.Gunner, pfGunner },
            { TowerType.WatchTower, pfWatchTower },
            { TowerType.GuardTower, pfGuardTower },
            { TowerType.WardTower, pfWardTower },
            { TowerType.UltimateWardTower, pfUltimateWardTower },
            { TowerType.CannonTower, pfCannonTower },
            { TowerType.BombardTower, pfBombardTower },
            { TowerType.ArtilleryTower, pfArtilleryTower },
            { TowerType.UltimateArtilleryTower, pfUltimateArtilleryTower },
            { TowerType.Cutter, pfCutter },
            { TowerType.Grinder, pfGrinder },
            { TowerType.Carver, pfCarver },
            { TowerType.Executioner, pfExecutioner },
            { TowerType.Mauler, pfMauler },
            { TowerType.UltimateMauler, pfUltimateMauler },
            { TowerType.Crusher, pfCrusher },
            { TowerType.Wrecker, pfWrecker },
            { TowerType.Mangler, pfMangler },
            { TowerType.UltimateMangler, pfUltimateMangler },
            { TowerType.ElementalCore, pfElementalCore },
            { TowerType.Spellslinger, pfSpellslinger },
            { TowerType.SpellslingerMaster, pfSpellslingerMaster },
            { TowerType.ArcanePylon, pfArcanePylon },
            { TowerType.ArcaneRepository, pfArcaneRepository },
            { TowerType.Archmage, pfArchmage },
            { TowerType.GrandArchmage, pfGrandArchmage },
            { TowerType.UltimateKirinTorWizard, pfUltimateKirinTorWizard },
            { TowerType.UltimateArcaneOrb, pfUltimateArcaneOrb },
            { TowerType.FirePit, pfFirePit },
            { TowerType.MagmaWell, pfMagmaWell },
            { TowerType.LivingFlame, pfLivingFlame },
            { TowerType.Hellfire, pfHellfire },
            { TowerType.UltimateFirelord, pfUltimateFirelord },
            { TowerType.MeteorAttractor, pfMeteorAttractor },
            { TowerType.Armageddon, pfArmageddon },
            { TowerType.UltimateMoonbeamProjector, pfUltimateMoonbeamProjector },
            { TowerType.FrozenObelisk, pfFrozenObelisk },
            { TowerType.RunicObelisk, pfRunicObelisk },
            { TowerType.FrozenWatcher, pfFrozenWatcher },
            { TowerType.FrozenCore, pfFrozenCore },
            { TowerType.UltimateLich, pfUltimateLich },
            { TowerType.Icicle, pfIcicle },
            { TowerType.Tricicle, pfTricicle },
            { TowerType.UltimateCrystal, pfUltimateCrystal },
            { TowerType.Rockfall, pfRockfall },
            { TowerType.Avalanche, pfAvalanche },
            { TowerType.EarthGuardian, pfEarthGuardian },
            { TowerType.AncientProtector, pfAncientProtector },
            { TowerType.UltimateAncientWarden, pfUltimateAncientWarden },
            { TowerType.NoxiousWeed, pfNoxiousWeed },
            { TowerType.NoxiousThorn, pfNoxiousThorn },
            { TowerType.UltimateScorpion, pfUltimateScorpion },
            { TowerType.ShockParticle, pfShockParticle },
            { TowerType.ShockGenerator, pfShockGenerator },
            { TowerType.Voltage, pfVoltage },
            { TowerType.HighVoltage, pfHighVoltage },
            { TowerType.UltimateOrbKeeper, pfUltimateOrbKeeper },
            { TowerType.LightningBeacon, pfLightningBeacon },
            { TowerType.LightningGenerator, pfLightningGenerator },
            { TowerType.UltimateAnnihilationGlyph, pfUltimateAnnihilationGlyph },
            { TowerType.LightFlies, pfLightFlies },
            { TowerType.HolyLantern, pfHolyLantern },
            { TowerType.SunrayTower, pfSunrayTower },
            { TowerType.SunbeamTower, pfSunbeamTower },
            { TowerType.UltimateTitanVault, pfUltimateTitanVault },
            { TowerType.Glowshroom, pfGlowshroom },
            { TowerType.Lightshroom, pfLightshroom },
            { TowerType.UltimateDivineshroom, pfUltimateDivineshroom },
            { TowerType.Splasher, pfSplasher },
            { TowerType.Tidecaller, pfTidecaller },
            { TowerType.WaterElemental, pfWaterElemental },
            { TowerType.SeaElemental, pfSeaElemental },
            { TowerType.UltimateHurricaneElemental, pfUltimateHurricaneElemental },
            { TowerType.TideLurker, pfTideLurker },
            { TowerType.AbyssStalker, pfAbyssStalker },
            { TowerType.UltimateSludgeMonstrosity, pfUltimateSludgeMonstrosity },
            { TowerType.PlagueWell, pfPlagueWell },
            { TowerType.Catacomb, pfCatacomb },
            { TowerType.SepticTank, pfSepticTank },
            { TowerType.PlagueFanatic, pfPlagueFanatic },
            { TowerType.UltimateGravedigger, pfUltimateGravedigger },
            { TowerType.ObsidianDestroyer, pfObsidianDestroyer },
            { TowerType.DecayedHorror, pfDecayedHorror },
            { TowerType.UltimateDiabolist, pfUltimateDiabolist },
            { TowerType.Voidling, pfVoidling },
            { TowerType.Voidalisk, pfVoidalisk },
            { TowerType.Riftweaver, pfRiftweaver },
            { TowerType.RiftLord, pfRiftLord },
            { TowerType.UltimateDevourer, pfUltimateDevourer },
            { TowerType.Lasher, pfLasher },
            { TowerType.Ravager, pfRavager },
            { TowerType.UltimateLeviathan, pfUltimateLeviathan },
            { TowerType.TechnologyDisc, pfTechnologyDisc },
            { TowerType.TechnologyDisc_Arcane, pfTechnologyDisc_Arcane },
            { TowerType.TechnologyDisc_Fire, pfTechnologyDisc_Fire },
            { TowerType.TechnologyDisc_Ice, pfTechnologyDisc_Ice },
            { TowerType.TechnologyDisc_Lightning, pfTechnologyDisc_Lightning },
            { TowerType.TechnologyDisc_Holy, pfTechnologyDisc_Holy },
            { TowerType.TechnologyDisc_Earth, pfTechnologyDisc_Earth },
            { TowerType.TechnologyDisc_Unholy, pfTechnologyDisc_Unholy },
            { TowerType.TechnologyDisc_Water, pfTechnologyDisc_Water },
            { TowerType.TechnologyDisc_Void, pfTechnologyDisc_Void },
            { TowerType.TechnologyDisc_Arcane_Ultimate, pfTechnologyDisc_Arcane_Ultimate },
            { TowerType.TechnologyDisc_Fire_Ultimate, pfTechnologyDisc_Fire_Ultimate },
            { TowerType.TechnologyDisc_Ice_Ultimate, pfTechnologyDisc_Ice_Ultimate },
            { TowerType.TechnologyDisc_Lightning_Ultimate, pfTechnologyDisc_Lightning_Ultimate },
            { TowerType.TechnologyDisc_Holy_Ultimate, pfTechnologyDisc_Holy_Ultimate },
            { TowerType.TechnologyDisc_Earth_Ultimate, pfTechnologyDisc_Earth_Ultimate },
            { TowerType.TechnologyDisc_Unholy_Ultimate, pfTechnologyDisc_Unholy_Ultimate },
            { TowerType.TechnologyDisc_Water_Ultimate, pfTechnologyDisc_Water_Ultimate },
            { TowerType.TechnologyDisc_Void_Ultimate, pfTechnologyDisc_Void_Ultimate },
        };
    }
}
