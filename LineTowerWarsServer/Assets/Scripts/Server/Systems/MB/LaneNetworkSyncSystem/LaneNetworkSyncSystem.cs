using System.Collections.Generic;

public class LaneNetworkSyncSystem : SingletonBehaviour<LaneNetworkSyncSystem> {
    private void Awake() {
        InitializeSingleton(this);

        ServerEventBus.OnLaneCleanedUp += LaneCleanedUp;
        LaneEventBus.OnLaneGoldUpdated += LaneGoldUpdated;
        LaneEventBus.OnLaneIncomeUpdated += LaneIncomeUpdated;
        LaneEventBus.OnLaneLivesUpdated += LaneLivesUpdated;
        LaneEventBus.OnLaneActiveUnitsUpdated += LaneActiveUnitsUpdated;
        LaneEventBus.OnLaneTechsUpdated += LaneTechsUpdated;
        LaneEventBus.OnLaneTechCostUpdated += LaneTechCostUpdated;
    }

    private void OnDestroy() {
        ServerEventBus.OnLaneCleanedUp -= LaneCleanedUp;
        LaneEventBus.OnLaneGoldUpdated -= LaneGoldUpdated;
        LaneEventBus.OnLaneIncomeUpdated -= LaneIncomeUpdated;
        LaneEventBus.OnLaneLivesUpdated -= LaneLivesUpdated;
        LaneEventBus.OnLaneActiveUnitsUpdated -= LaneActiveUnitsUpdated;
        LaneEventBus.OnLaneTechsUpdated -= LaneTechsUpdated;
        LaneEventBus.OnLaneTechCostUpdated -= LaneTechCostUpdated;
    }

    private static void LaneIncomeUpdated(Lane lane) {
        ServerSend.LaneIncomeUpdated(lane);
    }

    private static void LaneGoldUpdated(Lane lane) {
        ServerSend.LaneGoldUpdated(lane);
    }

    private static void LaneLivesUpdated(Lane lane) {
        ServerSend.LaneLivesUpdated(lane);
    }

    private static void LaneActiveUnitsUpdated(Lane lane) {
        ServerSend.LaneActiveUnitsUpdated(lane);
    }

    private static void LaneTechsUpdated(Lane lane) {
        ServerSend.LaneTechsUpdated(lane);
    }

    private static void LaneTechCostUpdated(Lane lane) {
        ServerSend.LaneTechCostUpdated(lane);
    }

    private static void LaneCleanedUp(Lane lane) {
        ServerSend.LaneCleanedUp(lane);
    }
}