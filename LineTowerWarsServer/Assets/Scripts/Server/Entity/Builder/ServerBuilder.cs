using System.Collections.Generic;
using UnityEngine;

public class ServerBuilder : ServerEntity,
                             INavigable, ICommandable, IAttacker {
    public static ServerBuilder SpawnInLane(
        Lane lane
    ) {
        Vector3 spawnLocation = lane.transform.position;
        ServerBuilder builder = Spawn(spawnLocation);
        builder.SetActiveLane(lane);
        return builder;
    }

    private static ServerBuilder Spawn(
        Vector3 location
    ) {
        ServerBuilder builder = Instantiate(
            ServerPrefabs.Singleton.pfBuilder,
            location, 
            Quaternion.Euler(0, 180, 0)
        );
        
        return builder;
    }
    
    // TODO: Would be nice to abstract all this properly and not need dummy values here.
    public override HashSet<TraitType> AssociatedTraitTypes { get; protected set; }
    public override int MaxHealth { get; protected set; }
    public override int MaxMana { get; protected set; }
    public override int Armor { get; protected set; }
    public override int SpellResist { get; protected set; }
    
    // Systems
    public NavigationSystem Navigation { get; private set; }
    public CommandSystem Commands { get; private set; }
    public AttackSystem Attack { get; private set; }

    protected override void Awake() {
        AssociatedTraitTypes = new HashSet<TraitType> { TraitType.Flying };
        
        MaxHealth = 1;
        MaxMana = 0;
        Armor = 0;
        SpellResist = 0;

        base.Awake();
        
        Navigation = new BuilderNavigationSystem(this);
        Commands = new CommandSystem(this);
        Attack = new BuilderAttackSystem(this);
    }
    
    protected virtual void Start() {
        ServerEventBus.BuilderSpawned(this);
    }

    private void Update() {
        Commands.Update();
        Navigation.Update();
    }

    // TODO: This can be optimized to only send when there is an actual movement update.
    private void LateUpdate() {
        ServerSend.BuilderMovementSync(this);
    }
}