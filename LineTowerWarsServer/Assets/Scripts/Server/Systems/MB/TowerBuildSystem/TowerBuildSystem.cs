/*
using System.Linq;
using UnityEngine;

public class TowerBuildSystem : SingletonBehaviour<TowerBuildSystem>
{
    private void Awake() {
        InitializeSingleton(this);
    }

    private void Start() {
        ServerEventBus.OnRequestBuildTower += ProcessTowerBuildRequest;
    }

    private static void ProcessTowerBuildRequest(
        TowerType type,
        Lane lane,
        MazeGridCell[] cells,
        int fromClientID
    ) {
        if (lane.Gold < TowerConstants.BuildCost[type]) {
            LTWLogger.Log($"Could not place tower as the lane did not have sufficient resources to build it");
            return;
        }

        foreach (MazeGridCell cell in cells) {
            if (cell.Occupied) {
                LTWLogger.Log($"Could not place tower on requested cell with ID {cell.ID}, as it was occupied");
                return;
            }
        }
        
        Vector3 midpoint = ServerUtil.GetMidpointOfTransforms(cells.Select(cell => cell.transform).ToArray());
        Vector3 buildLocation = new Vector3(
            midpoint.x, 
            (float)TowerConstants.PlacementHeight[type],
            midpoint.z
        );

        if (
            !IsTechnologyDiscType(type)
            && !NavMeshSystem.Singleton.IsNonBlockingBuildLocationForLane(buildLocation, lane)
        ) {
            ServerSend.DisplayChatMessageToClientWithID(
                "Building that tower would block your maze!",
                true,
                fromClientID
            );
            
            return;
        }

        FulfillTowerBuildRequest(lane, type, cells, buildLocation);
    }

    private static void FulfillTowerBuildRequest(Lane lane, TowerType type, MazeGridCell[] cells, Vector3 buildLocation) {
        lane.DeductGold(TowerConstants.BuildCost[type]);

        ServerTower t = EntityCreationEngine.CreateTower(
            type,
            buildLocation,
            lane,
            TowerConstants.BuildCost[type]
        );

        ServerSideGridSystem.Singleton.SetCellsOccupiedByTower(cells, t);

        NavMeshSystem.Singleton.Rebake();
    }

    private static bool IsTechnologyDiscType(TowerType type) {
        return 
            type >= TowerType.TechnologyDisc 
            && type <= TowerType.TechnologyDisc_Void_Ultimate;
    }
}
*/
