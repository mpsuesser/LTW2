using System.Collections.Generic;
using RiptideNetworking;

// All functions here are intended to eventually be split up into ServerSend -> ClientReceive
public class ServerSend {

    public static void SetCellsOccupancy(Lane lane, MazeGridCell[] cells, bool occupied) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.GridCellsOccupancyUpdated
        );

        message.AddInt(lane.ID);
        message.AddInt(cells.Length);
        foreach (MazeGridCell cell in cells) {
            message.AddInt(cell.ID);
        }
        
        message.AddBool(occupied);

        SendToAll(message);
    }

    public static void GameStateUpdated(ServerGameStateType newState) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.GameStateUpdated
        );

        message.AddInt((int) newState);

        SendToAll(message);
    }

    public static void PlayersUpdated(Dictionary<int, PlayerInfo> playersByClientID) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.PlayersUpdated
        );

        message.AddInt(playersByClientID.Count);
        foreach (int clientID in playersByClientID.Keys) {
            message.AddPlayerInfo(playersByClientID[clientID]);
        }
        
        SendToAll(message);
    }

    public static void CreepSpawned(ServerEnemy e) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.CreepSpawned
        );

        message.AddInt(e.ID);
        message.AddInt((int) e.Type);
        message.AddInt(e.HP);
        message.AddInt(e.MP);
        message.AddInt(e.ActiveLane.ID);
        message.AddVector3(e.transform.position);
        message.AddQuaternion(e.transform.rotation);

        SendToAll(message);
    }

    public static void CreepLaneUpdated(ServerEnemy e) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.CreepLaneUpdated
        );

        message.AddInt(e.ID);
        message.AddInt(e.ActiveLane.ID);
        
        SendToAll(message);
    }

    public static void EnemyMovementSync(ServerEnemy e) {
        Message message = RiptideHelper.CreateUnreliableMessage(
            RiptideMessageIDs.ServerToClient.CreepPositionUpdated
        );

        message.AddInt(e.ID);
        message.AddVector3(e.transform.position);
        message.AddQuaternion(e.transform.rotation);

        SendToAll(message);
    }

    public static void CreateTower(ServerTower tower) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.TowerCreated
        );

        message.AddInt(tower.ID);
        message.AddInt((int) tower.Type);
        message.AddInt(tower.ActiveLane.ID);
        message.AddVector3(tower.transform.position);
        message.AddInt(tower.HP);
        message.AddInt(tower.MP);
        
        SendToAll(message);
    }

    public static void TowerRotationSync(ServerTower t) {
        Message message = RiptideHelper.CreateUnreliableMessage(
            RiptideMessageIDs.ServerToClient.TowerRotationUpdated
        );

        message.AddInt(t.ID);
        message.AddQuaternion(t.transform.rotation);
        
        SendToAll(message);
    }

    public static void TowerUpgradeStarted(ServerTower t, TowerUpgrade upgrade) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.TowerUpgradeStarted
        );

        message.AddInt(t.ID);
        message.AddInt((int) upgrade.TargetTowerType);
        message.AddDouble(upgrade.Duration);
        
        SendToAll(message);
    }

    public static void TowerUpgradeCancelled(ServerTower t) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.TowerUpgradeCancelled
        );

        message.AddInt(t.ID);
        
        SendToAll(message);
    }

    public static void TowerUpgradeFinished(ServerTower oldTower, ServerTower newTower) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.TowerUpgradeFinished
        );

        message.AddInt(oldTower.ID);
        message.AddInt(newTower.ID);
        
        SendToAll(message);
    }

    public static void TowerSaleStarted(ServerTower t) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.TowerSaleStarted
        );

        message.AddInt(t.ID);
        
        SendToAll(message);
    }

    public static void TowerSaleCancelled(ServerTower t) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.TowerSaleCancelled
        );

        message.AddInt(t.ID);

        SendToAll(message);
    }

    public static void TowerSaleFinished(ServerTower t) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.TowerSaleFinished
        );

        message.AddInt(t.ID);
        
        SendToAll(message);
    }

    public static void EntityAttacked(ServerEntity attacker, AttackEventData eventData) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.EntityAttacked
        );

        message.AddInt(attacker.ID);
        eventData.WriteDataToMessage(ref message);
        
        SendToAll(message);
    }

    public static void BuilderSpawned(ServerBuilder builder) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.BuilderSpawned
        );

        message.AddInt(builder.ID);
        message.AddInt(builder.ActiveLane.ID);
        message.AddVector3(builder.transform.position);
        message.AddQuaternion(builder.transform.rotation);
        
        SendToAll(message);
    }

    public static void BuilderMovementSync(ServerBuilder builder) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.BuilderPositionUpdated
        );

        message.AddInt(builder.ID);
        message.AddVector3(builder.transform.position);
        message.AddQuaternion(builder.transform.rotation);
        message.AddBool(builder.Navigation.HasPath());

        SendToAll(message);
    }

    public static void EntityDied(ServerEntity e) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.EntityDied
        );

        message.AddInt(e.ID);
        
        SendToAll(message);
    }

    public static void EntityDespawned(ServerEntity e) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.EntityDespawned
        );

        message.AddInt(e.ID);
        
        SendToAll(message);
    }

    public static void EntityStatusSync(ServerEntity e) {
        Message message = RiptideHelper.CreateUnreliableMessage(
            RiptideMessageIDs.ServerToClient.EntityStatusUpdated
        );

        message.AddInt(e.ID);
        message.AddInt(e.HP);
        message.AddInt(e.MP);
        SendToAll(message);
    }

    public static void LaneGoldUpdated(Lane lane) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.LaneGoldUpdated
        );

        message.AddInt(lane.ID);
        message.AddInt(lane.Gold);
        
        SendToAll(message);
    }

    public static void LaneIncomeUpdated(Lane lane) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.LaneIncomeUpdated
        );

        message.AddInt(lane.ID);
        message.AddInt(lane.Income);

        SendToAll(message);
    }

    public static void LaneLivesUpdated(Lane lane) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.LaneLivesUpdated
        );

        message.AddInt(lane.ID);
        message.AddInt(lane.Lives);

        SendToAll(message);
    }

    public static void LaneActiveUnitsUpdated(Lane lane) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.LaneActiveUnitsUpdated
        );

        message.AddInt(lane.ID);
        message.AddInt(lane.ActiveUnits);

        SendToAll(message);
    }
    
    public static void LaneTechCostUpdated(Lane lane) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.LaneTechCostUpdated
        );

        message.AddInt(lane.ID);
        message.AddInt(lane.TechCost);

        SendToAll(message);
    }
    
    public static void LaneTechsUpdated(Lane lane) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.LaneTechsUpdated
        );

        message.AddInt(lane.ID);
        message.AddInt(lane.Techs.Count);
        foreach (ElementalTechType tech in lane.Techs) {
            message.AddInt((int) tech);
        }

        SendToAll(message);
    }

    public static void LaneCleanedUp(Lane lane) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.LaneCleanedUp
        );

        message.AddInt(lane.ID);
        
        SendToAll(message);
    }

    public static void IncomeDelivered(float timeToNextIncome) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.IncomeDelivered
        );

        message.AddFloat(timeToNextIncome);
        
        SendToAll(message);
    }

    public static void ChatMessageReceived(ChatMessage chatMessage) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.ChatMessageReceived
        );

        message.AddChatMessage(chatMessage);
        
        SendToAll(message);
    }

    public static void DisplayChatMessageToClientWithID(string messageText, bool isNegativeMessage, int clientID) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.DisplayScreenMessage
        );

        message.AddString(messageText);
        message.AddBool(isNegativeMessage);

        Send(message, (ushort) clientID);
    }

    public static void BuffAppliedToEntity(Buff buff, ServerEntity entity) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.BuffAppliedToEntity
        );

        message.AddInt(entity.ID);
        message.AddBuffTransitData(buff.PackageDataForTransit());
        
        SendToAll(message);
    }

    public static void BuffUpdated(Buff buff) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.BuffUpdated
        );

        message.AddBuffTransitData(buff.PackageDataForTransit());

        SendToAll(message);
    }

    public static void BuffRemovedFromEntity(Buff buff, ServerEntity entity) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.BuffRemovedFromEntity
        );

        message.AddInt(entity.ID);
        message.AddInt(buff.ID);
        
        SendToAll(message);
    }

    public static void CreepStockUpdated(
        EnemyType creepType,
        int stockAmount,
        float timeSinceLastIncrement,
        int clientID
    ) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.CreepStockUpdated
        );

        message.AddInt((int) creepType);
        message.AddInt(stockAmount);
        message.AddFloat(timeSinceLastIncrement);

        Send(message, (ushort) clientID);
    }

    public static void LivesExchanged(
        Lane losingLane,
        Lane gainingLane,
        int amount,
        ServerEnemy enemy
    ) {
        Message message = RiptideHelper.CreateReliableMessage(
            RiptideMessageIDs.ServerToClient.LivesExchanged
        );

        message.AddInt(losingLane.ID);
        message.AddInt(gainingLane.ID);
        message.AddInt(amount);
        message.AddInt((int) enemy.Type);
        
        SendToAll(message);
    }
    
    private static void Send(Message message, ushort clientID) {
        ServerNetworkManager.Singleton.Server.Send(message, clientID);
    }

    private static void SendToAll(Message message) {
        ServerNetworkManager.Singleton.Server.SendToAll(message);
    }
}