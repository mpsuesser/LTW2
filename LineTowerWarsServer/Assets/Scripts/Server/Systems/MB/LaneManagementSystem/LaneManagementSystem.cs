using System.Collections.Generic;

public class LaneManagementSystem : SingletonBehaviour<LaneManagementSystem> {
    private void Awake() {
        InitializeSingleton(this);

        ServerEventBus.OnClientGameStateUpdated += HandleClientGameStateUpdated;
        LaneEventBus.OnLaneLivesUpdated += HandleLaneLivesUpdated;
    }

    private void OnDestroy() {
        ServerEventBus.OnClientGameStateUpdated -= HandleClientGameStateUpdated;
        LaneEventBus.OnLaneLivesUpdated -= HandleLaneLivesUpdated;
    }

    private void HandleLaneLivesUpdated(Lane lane) {
        if (lane.Lives <= 0) {
            CleanUpLane(lane);
        }
    }

    private void HandleClientGameStateUpdated(int clientID, ClientGameStateType newState) {
        if (newState != ClientGameStateType.Disconnected) {
            return;
        }

        try {
            int slot = ServerLedgerSystem.Singleton.GetSlotForClientID(clientID);
            Lane lane = LaneSystem.Singleton.Lanes[slot];
            lane.SetLives(0);
        }
        catch (NotFoundException e) {
            LTWLogger.Log(e.Message);
            return;
        }
    }

    private void CleanUpLane(Lane lane) {
        HashSet<ServerTower> towers = ServerEntitySystem.Singleton.GetAllTowersInLane(lane);
        foreach (ServerTower tower in towers) {
            tower.Despawn();
        }

        HashSet<ServerEnemy> creeps = ServerEntitySystem.Singleton.GetAllCreepsInLane(lane);
        foreach (ServerEnemy creep in creeps) {
            creep.Despawn();
        }

        creeps = ServerEntitySystem.Singleton.GetAllCreepsFromLane(lane);
        foreach (ServerEnemy creep in creeps) {
            creep.Despawn();
        }

        ServerEventBus.LaneCleanedUp(lane);
    }
}