using UnityEngine;
using System.Collections.Generic;

public class UnitSpawnSystem : SingletonBehaviour<UnitSpawnSystem>
{
    private struct RespawnEvent {
        public EnemyType Type;
        public Lane ReceivingLane;
        public Lane SendingLane;
        public Vector3 Location;
        public Quaternion Rotation;
        public float HealthPercentage;
        public float TimeToRespawn;
    }
    
    // TODO: A PriorityQueue / Heap would be a better DS for this
    private List<RespawnEvent> PendingRespawnEvents { get; set; }
    
    private void Awake() {
        InitializeSingleton(this);

        PendingRespawnEvents = new List<RespawnEvent>();
    }

    private void Start() {
        ServerEventBus.OnRequestSendEnemy += ProcessSendEnemyRequest;
    }

    private void OnDestroy() {
        ServerEventBus.OnRequestSendEnemy -= ProcessSendEnemyRequest;
    }

    public void RespawnUnitInSeconds(ServerEnemy e, float healthPct, float seconds) {
        RespawnEvent respawnEvent;
        respawnEvent.Type = e.Type;
        respawnEvent.ReceivingLane = e.ActiveLane;
        respawnEvent.SendingLane = e.SendingLane;
        respawnEvent.Location = e.transform.position;
        respawnEvent.Rotation = e.transform.rotation;
        respawnEvent.HealthPercentage = healthPct;
        respawnEvent.TimeToRespawn = Time.time + seconds;
        
        PendingRespawnEvents.Add(respawnEvent);
    }

    private static void FulfillRespawnEvent(RespawnEvent respawnEvent) {
        ServerEnemy e = ServerEnemy.RespawnInLane(
            respawnEvent.Type, 
            respawnEvent.ReceivingLane,
            respawnEvent.SendingLane,
            respawnEvent.HealthPercentage,
            respawnEvent.Location,
            respawnEvent.Rotation
        );
        
        ServerSend.CreepSpawned(e);
    }

    private void Update() {
        float curTime = Time.time;
        for (int i = 0; i < PendingRespawnEvents.Count; i++) {
            if (PendingRespawnEvents[i].TimeToRespawn > curTime) {
                continue;
            }
            
            FulfillRespawnEvent(PendingRespawnEvents[i]);
            PendingRespawnEvents.RemoveAt(i);
            i--;
        }
    }

    private static void ProcessSendEnemyRequest(EnemyType type, Lane sendingLane) {
        int cost = EnemyConstants.GoldCost[type];
        if (
            sendingLane.Stock.GetStockForCreep(type) < 1
            || sendingLane.Gold < cost 
            || sendingLane.ActiveUnits >= LaneSystem.Singleton.MaxActiveUnits) {
            return;
        }

        sendingLane.DeductGold(cost);
        sendingLane.AddIncome(EnemyConstants.IncomeReward[type]);

        Lane receivingLane = LaneSendMappingSystem.Singleton.GetLaneReceivingFrom(sendingLane);

        for (int i = 0; i < EnemyConstants.NumberOfUnitsPerSend[type]; i++) {
            ServerEnemy e = ServerEnemy.SpawnInLane(
                type, 
                receivingLane,
                sendingLane
            );
            ServerSend.CreepSpawned(e);
        }
        
        ServerEventBus.CreepSendRequestFulfilledPost(sendingLane, type);
    }

    public static void EnemyReachedEndOfLane(ServerEnemy e) {
        if (e.ActiveLane.IsActive) {
            int livesStolen = Mathf.Min(
                e.LivesToSteal,
                e.SendingLane.Lives
            );
            
            e.ActiveLane.DeductLives(livesStolen);
            if (e.SendingLane.IsActive && e.SendingLane != e.ActiveLane) {
                e.SendingLane.AddLives(livesStolen);
            }
            
            ServerSend.LivesExchanged(
                e.ActiveLane,
                e.SendingLane,
                livesStolen,
                e
            );
        }

        try {
            e.MoveToLane(LaneSendMappingSystem.Singleton.GetLaneReceivingFrom(e.ActiveLane));
        }
        catch (LaneNotFoundException) {
            LTWLogger.Log($"Could not find an active lane to move life-stealing creep to, so the creep will be destroyed instead");
            Destroy(e.gameObject);
        }
    }
}