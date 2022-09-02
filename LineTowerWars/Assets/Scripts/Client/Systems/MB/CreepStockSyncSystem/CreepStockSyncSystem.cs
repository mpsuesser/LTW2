public class CreepStockSyncSystem : SingletonBehaviour<CreepStockSyncSystem> {
    private void Awake() {
        InitializeSingleton(this);

        EventBus.OnCreepStockUpdated += HandleUpdatedCreepStock;
    }

    private void OnDestroy() {
        EventBus.OnCreepStockUpdated -= HandleUpdatedCreepStock;
    }

    private static void HandleUpdatedCreepStock(
        EnemyType creepType,
        int stockAmount,
        float timeSinceLastIncrement
    ) {
        Lane lane = ClientLaneTracker.Singleton.MyLane;
        lane.Stock.SetStockForCreep(
            creepType,
            stockAmount,
            timeSinceLastIncrement
        );
    }
}