using UnityEngine;
using System.Collections.Generic;

// Server implementation
public abstract class ServerEnemy : ServerEntity,
                                    INavigable
{
    public static ServerEnemy RespawnInLane(
        EnemyType type,
        Lane activeLane,
        Lane sendingLane,
        float healthPct,
        Vector3 position,
        Quaternion rotation
    ) {
        ServerEnemy creep = Spawn(type, position);
        creep.transform.rotation = rotation;
        creep.SetActiveLane(activeLane);
        creep.SetSendingLane(sendingLane);
        creep.Status.SetInitialHealthPercentage(healthPct);
        creep.OnSpawningCompleted();

        return creep;
    }
    
    public static ServerEnemy SpawnInLane(
        EnemyType type,
        Lane activeLane,
        Lane sendingLane
    ) {
        Vector3 spawnLocation = activeLane.SpawnArea.GetSpawnLocation();
        ServerEnemy enemy = Spawn(type, spawnLocation);
        enemy.SetActiveLane(activeLane);
        enemy.SetSendingLane(sendingLane);
        enemy.OnSpawningCompleted();
        return enemy;
    }

    private static ServerEnemy Spawn(
        EnemyType type,
        Vector3 location
    ) {
        ServerEnemy enemy = Instantiate(
            ServerPrefabs.Enemies[type],
            location, 
            Quaternion.Euler(0, 180, 0)
        );
        
        return enemy;
    }

    public void MoveToLane(Lane lane) {
        SetActiveLane(lane);
        Navigation.UpdatePositionTo(lane.SpawnArea.GetSpawnLocation());

        ServerSend.CreepLaneUpdated(this);
    }

    public abstract EnemyType Type { get; }

    public Lane SendingLane { get; private set; }
    
    public override HashSet<TraitType> AssociatedTraitTypes { get; protected set; }

    // Attributes
    public int LivesToSteal => 1 + Effects.AggregateLivesStolenDiff;

    public override int MaxHealth { get; protected set; }
    public override int MaxMana { get; protected set; }
    public override int Armor { get; protected set; }
    public override int SpellResist { get; protected set; }

    // Systems
    public NavigationSystem Navigation { get; private set; }

    public override ArmorType ArmorModifier => EnemyConstants.ArmorModifier[Type];
    
    protected override void Awake() {
        AssociatedTraitTypes = TraitConstants.EnemyTraitMap[Type];
        
        MaxHealth = EnemyConstants.HP[Type];
        MaxMana = EnemyConstants.MP[Type];
        Armor = EnemyConstants.ArmorAmount[Type];
        SpellResist = EnemyConstants.SpellResistanceAmount[Type];

        base.Awake();

        Navigation = new CreepNavigationSystem(this, Effects);
    }

    protected virtual void Start() {
        ServerEventBus.EnemySpawned(this);
    }

    protected virtual void Update() {
        Navigation.Update();
        
        ServerSend.EnemyMovementSync(this);
    }

    private void SetSendingLane(Lane lane) {
        SendingLane = lane;
        lane.AddActiveUnits(1);
    }

    public override void Die() {
        SendingLane.DeductActiveUnits(1);

        if (!Effects.AggregateRemovesCreepKillBounty) {
            ActiveLane.AddGold(EnemyConstants.KillReward[Type]);
        }

        base.Die();
    }

    public override void Despawn() {
        SendingLane.DeductActiveUnits(1);

        base.Despawn();
    }

    private void OnTriggerEnter(Collider other) {
        LaneEndzone endzone = other.gameObject.GetComponent<LaneEndzone>();
        if (endzone == null) {
            return;
        }

        UnitSpawnSystem.EnemyReachedEndOfLane(this);
    }
}
