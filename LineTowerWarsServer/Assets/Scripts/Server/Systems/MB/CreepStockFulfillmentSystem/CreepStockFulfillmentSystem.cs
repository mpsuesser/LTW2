using System;
using UnityEngine;

public class CreepStockFulfillmentSystem : SingletonBehaviour<CreepStockFulfillmentSystem> {
    private int frameCount;
    
    // i.e. Update the stock timers for each lane every x frames
    private const int StockTimerFrameFrequency = 5;

    private void Awake() {
        InitializeSingleton(this);

        frameCount = 0;

        ServerEventBus.OnCreepSendRequestFulfilledPost += HandleCreepSend;
    }

    private void OnDestroy() {
        ServerEventBus.OnCreepSendRequestFulfilledPost -= HandleCreepSend;
    }

    private void Update() {
        if (++frameCount >= StockTimerFrameFrequency) {
            UpdateStockTimers();

            frameCount = 0;
        }
    }

    private static void UpdateStockTimers() {
        float curTime = Time.time;
        foreach (Lane lane in LaneSystem.Singleton.Lanes) {
            foreach (EnemyType creepType in EnemyConstants.FullyImplementedCreeps) {
                int stockForCreep = lane.Stock.GetStockForCreep(creepType);
                if (stockForCreep >= EnemyConstants.MaxStock[creepType]) {
                    continue;
                }

                float timeOfLastFulfillment = lane.Stock.GetMostRecentIncrementTimeForCreep(creepType);
                if (stockForCreep == CreepStock.InitialDelayNotYetFinished) {
                    if (curTime - timeOfLastFulfillment > EnemyConstants.InitialStockDelay[creepType]) {
                        SetStockForCreepType(
                            lane,
                            creepType,
                            EnemyConstants.InitialStockAmountAfterDelay[creepType],
                            true
                        );
                        continue;
                    }
                }
                else {
                    if (curTime - timeOfLastFulfillment > EnemyConstants.StockIncrementTimer[creepType]) {
                        SetStockForCreepType(
                            lane,
                            creepType,
                            stockForCreep + 1,
                            true
                        );
                    }
                }
            }
        }
    }

    private static void SetStockForCreepType(Lane sendingLane, EnemyType creepType, int stockAmount, bool isFulfillment) {
        sendingLane.Stock.SetStockForCreep(
            creepType,
            stockAmount,
            isFulfillment
        );

        int clientID = ServerLedgerSystem.Singleton.GetPlayerInLane(sendingLane.ID).ClientID;
        float timeSinceLastFulfillment =
            Time.time - sendingLane.Stock.GetMostRecentIncrementTimeForCreep(creepType);
        
        ServerSend.CreepStockUpdated(
            creepType,
            stockAmount,
            timeSinceLastFulfillment,
            clientID
        );
    }

    private static void HandleCreepSend(Lane sendingLane, EnemyType creepType) {
        int stockForCreep = sendingLane.Stock.GetStockForCreep(creepType);
        bool resetFulfillmentTimer =
            stockForCreep == EnemyConstants.MaxStock[creepType];
        
        SetStockForCreepType(
            sendingLane,
            creepType,
            stockForCreep - 1,
            resetFulfillmentTimer
        );
    }
    
    // On stock usage, if creep was previously at max stock, then set the fulfillment time at that time too
}