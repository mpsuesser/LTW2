using UnityEngine;
using System.Collections.Generic;

public abstract class ServerTower : ServerEntity,
                                    IAttacker, IThreatenable, IUpgradeable,
                                    ISellable, IRotationSyncable, ICommandable
{
    public static ServerTower CreateTowerAtPointInLane(TowerType type, Vector3 buildLocation, Lane lane, int goldValue) {
        ServerTower tower = Instantiate(ServerPrefabs.Towers[type], buildLocation, Quaternion.identity);
        tower.SetActiveLane(lane);
        ServerSend.CreateTower(tower);
        tower.OnSpawningCompleted();
        
        tower.GoldValue = goldValue;

        return tower;
    }
    
    public abstract TowerType Type { get; }
    
    public override HashSet<TraitType> AssociatedTraitTypes { get; protected set; }

    // Attributes
    public override int MaxHealth { get; protected set; }
    public override int MaxMana { get; protected set; }
    public override int Armor { get; protected set; }
    public override int SpellResist { get; protected set; }
    
    public int GoldValue { get; private set; }

    public ThreatSystem Threat { get; private set; }
    public AttackSystem Attack { get; private set; }
    public UpgradeSystem Upgrade { get; private set; }
    public SellSystem Sell { get; private set; }
    public RotationSyncSystem RotationSync { get; private set; }
    public CommandSystem Commands { get; private set; }

    public override AttackType AttackModifier => Attack.AtkType;

    public System.Random RNG { get; private set; }

    protected override void Awake() {
        AssociatedTraitTypes = TraitConstants.TowerTraitMap[Type];
        
        MaxHealth = TowerConstants.HP[Type];
        MaxMana = TowerConstants.MP[Type];

        Upgrade = new UpgradeSystem(this);
        Sell = new SellSystem(this);
        RotationSync = new RotationSyncSystem(this);
        Commands = new CommandSystem(this);
        InitializeOffensiveSystems();

        Armor = 0; // TODO: Implement armor for towers
        SpellResist = 0;
        
        RNG = new System.Random();

        base.Awake();
    }

    private void Start() {
        ServerEventBus.TowerSpawned(this);
    }

    protected virtual void Update() {
        Upgrade.Update();
        if (Upgrade.InProgress) {
            return;
        }

        Commands.Update();
        Sell.Update();
        UpdateOffensiveSystems();
        RotationSync.Update();
    }

    // These virtualizations are here in case we have a non-offensive tower
    protected virtual void InitializeOffensiveSystems() {
        Threat = new TowerThreatSystem(this);
        Attack = new TowerAttackSystem(this);
    }

    protected virtual void UpdateOffensiveSystems() {
        Threat.Update();
        Attack.Update(Threat.Target);
    }

    public override void Die() {
        ServerEventBus.TowerDied(this);
        base.Die();
    }
}
