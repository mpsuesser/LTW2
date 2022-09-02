using UnityEngine;
using System.Collections.Generic;
using RiptideNetworking;

public static class ServerReceive  {

    public static void NewPlayerConnected(object sender, ServerClientConnectedEventArgs e) {
        ServerEventBus.NewClientConnected(e.Client.Id, "temp test username");
    }

    public static void PlayerLeft(object sender, ClientDisconnectedEventArgs e) {
        ServerEventBus.ClientDisconnected(e.Id);
    }

    [MessageHandler((ushort) RiptideMessageIDs.ClientToServer.PresentTableStakes)]
    private static void PresentTableStakes(ushort fromClientID, Message message) {
        string username = message.GetString();
        string playfabID = message.GetString();
        string playfabSessionToken = message.GetString();
        int clientStateID = message.GetInt();
        
        ServerEventBus.TableStakesPresented(fromClientID, username, playfabID, playfabSessionToken, clientStateID);
    }

    [MessageHandler((ushort) RiptideMessageIDs.ClientToServer.SendChatMessage)]
    private static void SendChatMessage(ushort fromClientID, Message message) {
        string chatMessage = message.GetString();

        ServerEventBus.ChatMessageSent(fromClientID, chatMessage);
    }

    [MessageHandler((ushort) RiptideMessageIDs.ClientToServer.RequestNewLobbySlot)]
    private static void RequestNewLobbySlot(ushort fromClientID, Message message) {
        int lobbySlot = message.GetInt();

        ServerEventBus.RequestNewLobbySlot(fromClientID, lobbySlot);
    }
    
    [MessageHandler((ushort) RiptideMessageIDs.ClientToServer.UpdateClientState)]
    private static void UpdateClientState(ushort fromClientID, Message message) {
        int clientStateID = message.GetInt();
        
        ServerEventBus.ClientGameStateUpdated(fromClientID, clientStateID);
    }

    [MessageHandler((ushort) RiptideMessageIDs.ClientToServer.RequestSendCreep)]
    private static void RequestSendCreep(ushort fromClientID, Message message) {
        int enemyTypeID = message.GetInt();
        int laneID = message.GetInt();
        
        ServerEventBus.RequestSendEnemy(fromClientID, enemyTypeID, laneID);
    }

    [MessageHandler((ushort) RiptideMessageIDs.ClientToServer.RequestBuildTower)]
    private static void RequestBuildTower(ushort fromClientID, Message message) {
        int towerTypeID = message.GetInt();
        int laneID = message.GetInt();
        int cellsCount = message.GetInt();
        
        int[] cellIDs = new int[cellsCount];
        for (int i = 0; i < cellsCount; i++) {
            cellIDs[i] = message.GetInt();
        }

        bool isQueuedAction = message.GetBool();
        
        ServerEventBus.RequestBuildTower(
            fromClientID,
            towerTypeID,
            laneID,
            cellIDs,
            isQueuedAction
        );
    }

    [MessageHandler((ushort) RiptideMessageIDs.ClientToServer.RequestPurchaseElementTech)]
    private static void RequestPurchaseElementTech(ushort fromClientID, Message message) {
        LTWLogger.Log("Elemental tech purchase message received");
        int elementalTechID = message.GetInt();
        int laneID = message.GetInt();
        
        ServerEventBus.RequestPurchaseElementalTech(fromClientID, elementalTechID, laneID);
    }

    [MessageHandler((ushort) RiptideMessageIDs.ClientToServer.RequestTowerUpgrade)]
    private static void RequestTowerUpgrade(ushort fromClientID, Message message) {
        int sourceTowerTypeID = message.GetInt();
        int targetTowerTypeID = message.GetInt();

        int towersCount = message.GetInt();
        HashSet<int> towerIDs = new HashSet<int>();
        for (int i = 0; i < towersCount; i++) {
            towerIDs.Add(message.GetInt());
        }

        ServerEventBus.RequestTowerUpgrade(fromClientID, sourceTowerTypeID, targetTowerTypeID, towerIDs);
    }

    [MessageHandler((ushort) RiptideMessageIDs.ClientToServer.RequestTowerUpgradeCancellation)]
    private static void RequestTowerUpgradeCancellation(ushort fromClientID, Message message) {
        int towersCount = message.GetInt();
        HashSet<int> towerIDs = new HashSet<int>();
        for (int i = 0; i < towersCount; i++) {
            towerIDs.Add(message.GetInt());
        }

        ServerEventBus.RequestTowerUpgradeCancellation(fromClientID, towerIDs);
    }

    [MessageHandler((ushort) RiptideMessageIDs.ClientToServer.RequestTowerSale)]
    private static void RequestTowerSale(ushort fromClientID, Message message) {
        int towersCount = message.GetInt();
        HashSet<int> towerIDs = new HashSet<int>();
        for (int i = 0; i < towersCount; i++) {
            towerIDs.Add(message.GetInt());
        }

        ServerEventBus.RequestTowerSale(fromClientID, towerIDs);
    }

    [MessageHandler((ushort) RiptideMessageIDs.ClientToServer.RequestTowerSaleCancellation)]
    private static void RequestTowerSaleCancellation(ushort fromClientID, Message message) {
        int towersCount = message.GetInt();
        HashSet<int> towerIDs = new HashSet<int>();
        for (int i = 0; i < towersCount; i++) {
            towerIDs.Add(message.GetInt());
        }

        ServerEventBus.RequestTowerSaleCancellation(fromClientID, towerIDs);
    }

    [MessageHandler((ushort) RiptideMessageIDs.ClientToServer.RequestEntityMove)]
    private static void RequestEntitiesMove(ushort fromClientID, Message message) {
        HashSet<int> entityIDs = new HashSet<int>();
        int entityCount = message.GetInt();
        for (int i = 0; i < entityCount; i++) {
            entityIDs.Add(message.GetInt());
        }
        
        Vector3 location = message.GetVector3();
        bool isQueuedAction = message.GetBool();
        
        ServerEventBus.RequestEntitiesMove(
            fromClientID,
            entityIDs,
            location,
            isQueuedAction);
    }
    
    [MessageHandler((ushort) RiptideMessageIDs.ClientToServer.RequestEntityAttackTarget)]
    private static void RequestEntitiesAttackTarget(ushort fromClientID, Message message) {
        HashSet<int> entityIDs = new HashSet<int>();
        int entityCount = message.GetInt();
        for (int i = 0; i < entityCount; i++) {
            entityIDs.Add(message.GetInt());
        }
        
        int targetID = message.GetInt();
        bool isQueuedAction = message.GetBool();

        ServerEventBus.RequestEntitiesAttackTarget(
            fromClientID,
            entityIDs, 
            targetID, 
            isQueuedAction
        );
    }
    
    [MessageHandler((ushort) RiptideMessageIDs.ClientToServer.RequestEntityAttackLocation)]
    private static void RequestEntitiesAttackLocation(ushort fromClientID, Message message) {
        HashSet<int> entityIDs = new HashSet<int>();
        int entityCount = message.GetInt();
        for (int i = 0; i < entityCount; i++) {
            entityIDs.Add(message.GetInt());
        }
        
        Vector3 location = message.GetVector3();
        bool isQueuedAction = message.GetBool();

        ServerEventBus.RequestEntitiesAttackLocation(
            fromClientID,
            entityIDs, 
            location, 
            isQueuedAction
        );
    }
}
