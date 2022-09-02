using System.Linq;
using UnityEngine;

public static class VisualEffectManager {
    public static void CreateEntityMovementCommandIndicator(Vector3 location) {
        VisualEffect indicator = Object.Instantiate(
            ClientPrefabs.Singleton.pfEntityMoveCommandIndicator,
            location,
            Quaternion.identity,
            DynamicObjects.Singleton.MiscEffects
        );
    }
    
    public static void CreateEntityAttackCommandIndicator(Vector3 location) {
        VisualEffect indicator = Object.Instantiate(
            ClientPrefabs.Singleton.pfEntityAttackCommandIndicator,
            location,
            Quaternion.identity,
            DynamicObjects.Singleton.MiscEffects
        );
    }

    public static void CreateBuildProjection(
        TowerType towerType,
        MazeGridCell[] cells
    ) {
        Vector3 midpoint = ClientUtil.GetMidpointOfTransforms(
            cells.Select(cell => cell.transform).ToArray()
        );
        Vector3 buildLocation = new Vector3(
            midpoint.x, 
            (float)TowerConstants.PlacementHeight[towerType],
            midpoint.z
        );

        TowerBuildProjection.Create(towerType, buildLocation);
    }
}