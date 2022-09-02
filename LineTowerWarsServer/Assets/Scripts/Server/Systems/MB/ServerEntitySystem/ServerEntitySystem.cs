using System.Collections.Generic;

public class ServerEntitySystem : SingletonBehaviour<ServerEntitySystem>
{
    private Dictionary<int, ServerEnemy> EnemiesByID { get; set; }
    private Dictionary<int, ServerTower> TowersByID { get; set; }
    private Dictionary<int, ServerBuilder> BuildersByID { get; set; }
    private Dictionary<Lane, ServerBuilder> BuildersByLane { get; set; }

    private void Awake() {
        InitializeSingleton(this);

        EnemiesByID = new Dictionary<int, ServerEnemy>();
        TowersByID = new Dictionary<int, ServerTower>();
        BuildersByID = new Dictionary<int, ServerBuilder>();
        BuildersByLane = new Dictionary<Lane, ServerBuilder>();
    }

    private void Start() {
        ServerEventBus.OnEnemySpawn += RegisterEnemy;
        ServerEventBus.OnTowerSpawn += RegisterTower;
        ServerEventBus.OnBuilderSpawn += RegisterBuilder;
        ServerEventBus.OnEntityDestroyed += DeregisterEntity;
    }

    private void OnDestroy() {
        ServerEventBus.OnEnemySpawn -= RegisterEnemy;
        ServerEventBus.OnTowerSpawn -= RegisterTower;
        ServerEventBus.OnBuilderSpawn -= RegisterBuilder;
        ServerEventBus.OnEntityDestroyed -= DeregisterEntity;
    }

    private void RegisterEnemy(ServerEnemy e) {
        EnemiesByID.Add(e.ID, e);
    }

    private void RegisterTower(ServerTower t) {
        TowersByID.Add(t.ID, t);
    }

    private void RegisterBuilder(ServerBuilder builder) {
        BuildersByID.Add(builder.ID, builder);
        BuildersByLane.Add(builder.ActiveLane, builder);
    }

    private void DeregisterEntity(ServerEntity e) {
        if (e is ServerTower) {
            TowersByID.Remove(e.ID);
        } else if (e is ServerEnemy) {
            EnemiesByID.Remove(e.ID);
        } else if (e is ServerBuilder) {
            BuildersByID.Remove(e.ID);
            BuildersByLane.Remove(e.ActiveLane);
        }
    }

    public ServerEntity GetEntityByID(int entityID) {
        // TODO: Keep an "entity by ID" dict instead
        if (EnemiesByID.ContainsKey(entityID)) {
            return EnemiesByID[entityID];
        } else if (TowersByID.ContainsKey(entityID)) {
            return TowersByID[entityID];
        } else if (BuildersByID.ContainsKey(entityID)) {
            return BuildersByID[entityID];
        }

        throw new NotFoundException();
    }

    public ServerEnemy GetEnemyByEntityID(int entityID) {
        if (!EnemiesByID.ContainsKey(entityID)) {
            throw new NotFoundException();
        }

        return EnemiesByID[entityID];
    }

    public ServerTower GetTowerByEntityID(int entityID) {
        if (!TowersByID.ContainsKey(entityID)) {
            throw new NotFoundException();
        }

        return TowersByID[entityID];
    }

    public ServerBuilder GetBuilderByEntityID(int entityID) {
        if (!BuildersByID.ContainsKey(entityID)) {
            throw new NotFoundException();
        }

        return BuildersByID[entityID];
    }

    public HashSet<ServerTower> GetAllTowersInLane(Lane lane) {
        HashSet<ServerTower> towers = new HashSet<ServerTower>();
        foreach (ServerTower tower in TowersByID.Values) {
            if (tower.ActiveLane == lane) {
                towers.Add(tower);
            }
        }

        return towers;
    }

    public HashSet<ServerEnemy> GetAllCreepsInLane(Lane lane) {
        HashSet<ServerEnemy> creeps = new HashSet<ServerEnemy>();
        foreach (ServerEnemy creep in EnemiesByID.Values) {
            if (creep.ActiveLane == lane) {
                creeps.Add(creep);
            }
        }

        return creeps;
    }
    
    public HashSet<ServerEnemy> GetAllCreepsFromLane(Lane lane) {
        HashSet<ServerEnemy> creeps = new HashSet<ServerEnemy>();
        foreach (ServerEnemy creep in EnemiesByID.Values) {
            if (creep.SendingLane == lane) {
                creeps.Add(creep);
            }
        }

        return creeps;
    }

    public ServerBuilder GetBuilderByLane(Lane lane) {
        if (!BuildersByLane.TryGetValue(lane, out ServerBuilder builder)) {
            throw new NotFoundException();
        }

        return builder;
    }
}
