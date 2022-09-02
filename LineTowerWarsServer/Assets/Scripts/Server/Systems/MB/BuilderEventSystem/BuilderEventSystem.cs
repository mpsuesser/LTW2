using System.Linq;
using UnityEngine;

public class BuilderEventSystem : SingletonBehaviour<BuilderEventSystem> {
    private void Awake() {
        ServerEventBus.OnGameStarted += CreateBuilders;
        ServerEventBus.OnRequestBuildTower += ProcessTowerBuildRequest;
    }

    private void OnDestroy() {
        ServerEventBus.OnGameStarted -= CreateBuilders;
        ServerEventBus.OnRequestBuildTower -= ProcessTowerBuildRequest;
    }

    private static void CreateBuilders() {
        for (int i = 0; i < LaneSystem.Singleton.MaxLaneCount; i++) {
            ServerBuilder builder = ServerBuilder.SpawnInLane(LaneSystem.Singleton.Lanes[i]);
            ServerSend.BuilderSpawned(builder);
        }
    }
    
    private static void ProcessTowerBuildRequest(
        int fromClientID,
        TowerType type,
        Lane lane,
        MazeGridCell[] cells,
        bool isQueuedAction
    ) {
        try {
            ServerBuilder builder = ServerEntitySystem.Singleton.GetBuilderByLane(lane);
            BuildCommand command = new BuildCommand(
                fromClientID,
                builder,
                type,
                cells,
                TowerConstants.BuildCost[type]
            );
            builder.Commands.ProcessNewCommand(command, isQueuedAction);
        }
        catch (NotFoundException) {
            LTWLogger.Log($"There was no builder in lane {lane.ID}!");
            return;
        }
    }
}