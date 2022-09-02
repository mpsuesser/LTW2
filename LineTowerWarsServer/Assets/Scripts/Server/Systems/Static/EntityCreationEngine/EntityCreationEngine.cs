using UnityEngine;

public static class EntityCreationEngine {
    public static ServerTower CreateTower(
        TowerType type,
        Vector3 location,
        Lane lane,
        int goldValue,
        ServerTower prevTowerReference = null
    ) {
        ServerTower newTower = ServerTower.CreateTowerAtPointInLane(
            type,
            location,
            lane, 
            goldValue
        );

        if (prevTowerReference != null) {
            ServerSideGridSystem.Singleton.UpdateTowerReference(
                prevTowerReference,
                newTower
            );
        
            Object.Destroy(prevTowerReference.gameObject);
        }

        return newTower;
    }
    
    // TODO: Creep
    
    // TODO: Builder
}