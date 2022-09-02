using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ServerEventBus
{
    public static event Action<int, string> OnNewClientConnected;
    public static event Action<int, string, string, ClientGameStateType> OnTableStakesPresented;
    public static event Action<int> OnClientDisconnected;
    public static event Action<int, ClientGameStateType> OnClientGameStateUpdated;
    public static event Action OnLedgerUpdated;

    public static event Action<ChatMessage> OnNewChatMessageReceived;
    public static event Action<PlayerInfo, int> OnRequestNewLobbySlot;

    // Client request events
    public static event Action<EnemyType, Lane> OnRequestSendEnemy;
    public static event Action<int, TowerType, Lane, MazeGridCell[], bool> OnRequestBuildTower;
    public static event Action<Lane, ElementalTechType> OnRequestPurchaseElementalTech;
    public static event Action<HashSet<ServerTower>, TowerUpgrade> OnRequestTowerUpgrade;
    public static event Action<HashSet<ServerTower>> OnRequestTowerUpgradeCancellation;
    public static event Action<HashSet<ServerTower>> OnRequestTowerSale;
    public static event Action<HashSet<ServerTower>> OnRequestTowerSaleCancellation;
    public static event Action<HashSet<ServerEntity>, Vector3, bool> OnRequestEntitiesMove;
    public static event Action<HashSet<ServerEntity>, ServerEntity, bool> OnRequestEntitiesAttackTarget;
    public static event Action<HashSet<ServerEntity>, Vector3, bool> OnRequestEntitiesAttackLocation;

    public static event Action<Lane, EnemyType> OnCreepSendRequestFulfilledPost;
    
    // Game loop events
    public static event Action<int> OnGameStaging;
    public static event Action OnGameStarted;
    public static event Action<Lane> OnLaneCleanedUp;
    public static event Action OnFrameRefresh;

    // Entity spawn events
    public static event Action<ServerEnemy> OnEnemySpawn;
    public static event Action<ServerTower> OnTowerSpawn;
    public static event Action<ServerBuilder> OnBuilderSpawn;
    public static event Action<ServerEntity> OnEntityDestroyed;

    // Tower events
    public static event Action<ServerTower, TowerUpgrade> OnTowerUpgradeStarted;
    public static event Action<ServerTower, TowerUpgrade> OnTowerUpgradeCanceled;
    public static event Action<ServerTower, TowerUpgrade> OnTowerUpgradeFinished;
    public static event Action<ServerTower> OnTowerSaleStarted;
    public static event Action<ServerTower> OnTowerSaleCanceled;
    public static event Action<ServerTower> OnTowerSaleFinished;
    public static event Action<ServerTower, AttackEventData> OnTowerAttack;
    public static event Action<ServerTower> OnTowerDied;
    

    #region Incoming events from client

    public static void TableStakesPresented(int clientID, string username, string playfabID, string playfabSessionToken, int clientStateID) {
        try {
            if (!Enum.IsDefined(typeof(ClientGameStateType), clientStateID))
                throw new ArgumentOutOfRangeException();
            
            // TODO: Confirm sessionToken is valid and player is actually who they're presented as

            ClientGameStateType clientState = (ClientGameStateType) clientStateID;
            OnTableStakesPresented?.Invoke(clientID, username, playfabID, clientState);
        } catch (ArgumentOutOfRangeException) {
            LTWLogger.LogError($"Enum input value {clientStateID} was out of range!");
        }
    }

    public static void ChatMessageSent(int clientID, string chatMessage) {
        try {
            if (chatMessage.Length > ChatConstants.MaxCharsPerMessage)
                throw new MessageTooLongException(chatMessage, ChatConstants.MaxCharsPerMessage);

            PlayerInfo sendingPlayer = ServerLedgerSystem.Singleton.GetPlayerByClientID(clientID);
            ChatMessage message = new ChatMessage(
                clientID,
                sendingPlayer,
                chatMessage
            );
            
            OnNewChatMessageReceived?.Invoke(message);
        }
        catch (MessageTooLongException e) {
            LTWLogger.LogError(e.Message);
        }
        catch (NotFoundException) {
            LTWLogger.LogError($"Could not get info for player with client ID {clientID}");
        }
    }

    public static void RequestNewLobbySlot(int clientID, int lobbySlot) {
        try {
            PlayerInfo player = ServerLedgerSystem.Singleton.GetPlayerByClientID(clientID);
            
            OnRequestNewLobbySlot?.Invoke(player, lobbySlot);
        } catch (NotFoundException) {
            LTWLogger.LogError($"Could not get info for player with client ID {clientID}");
        }
    }
    
    public static void ClientGameStateUpdated(int clientID, int newStateID) {
        try {
            if (!Enum.IsDefined(typeof(ClientGameStateType), newStateID))
                throw new ArgumentOutOfRangeException();
            
            ClientGameStateType clientState = (ClientGameStateType) newStateID;

            OnClientGameStateUpdated?.Invoke(clientID, clientState);
        } catch (ArgumentOutOfRangeException) {
            LTWLogger.LogError($"Enum input value {newStateID} was out of range!");
        }
    }

    public static void RequestSendEnemy(int clientID, int enemyTypeID, int laneID) {
        try {
            if (!Enum.IsDefined(typeof(EnemyType), enemyTypeID))
                throw new ArgumentOutOfRangeException();

            EnemyType enemyType = (EnemyType) enemyTypeID;
            Lane lane = LaneSystem.Singleton.Lanes[laneID];

            AssertClientIDIsInLaneWithID(clientID, laneID);

            OnRequestSendEnemy?.Invoke(enemyType, lane);
        }
        catch (IndexOutOfRangeException) {
            LTWLogger.LogError(
                $"Could not find lane with ID {laneID}... there are only {LaneSystem.Singleton.Lanes.Length} lanes!");
        }
        catch (ArgumentOutOfRangeException) {
            LTWLogger.LogError($"Enum input value {enemyTypeID} was out of range!");
        }
        catch (ClientDoesNotControlLaneException e) {
            LTWLogger.LogError(e.Message);
        }
    }

    public static void RequestBuildTower(
        int clientID,
        int towerTypeID,
        int laneID,
        int[] cellIDs,
        bool isQueuedAction
    ) {
        if (cellIDs.Length != 4) {
            return;
        }

        try {
            if (!Enum.IsDefined(typeof(TowerType), towerTypeID))
                throw new ArgumentOutOfRangeException();
            
            TowerType towerType = (TowerType) towerTypeID;
            Lane lane = LaneSystem.Singleton.Lanes[laneID];

            AssertClientIDIsInLaneWithID(clientID, laneID);
            
            MazeGridCell[] cells = lane.Grid.GetCellsByID(cellIDs);

            OnRequestBuildTower?.Invoke(clientID, towerType, lane, cells, isQueuedAction);
        } catch (IndexOutOfRangeException) {
            LTWLogger.LogError($"Could not find lane with ID {laneID}... there are only {LaneSystem.Singleton.Lanes.Length} lanes!");
        } catch (NotFoundException e) {
            LTWLogger.LogError(e.Message);
        } catch (ArgumentOutOfRangeException) {
            LTWLogger.LogError($"Enum input value {towerTypeID} was out of range!");
        } catch (ClientDoesNotControlLaneException e) {
            LTWLogger.LogError(e.Message);
        }
    }

    public static void RequestPurchaseElementalTech(int clientID, int elementalTechID, int laneID) {
        try {
            if (!Enum.IsDefined(typeof(ElementalTechType), elementalTechID))
                throw new ArgumentOutOfRangeException();
            
            Lane lane = LaneSystem.Singleton.Lanes[laneID];
            
            AssertClientIDIsInLaneWithID(clientID, laneID);
            
            ElementalTechType tech = (ElementalTechType) elementalTechID;

            OnRequestPurchaseElementalTech?.Invoke(lane, tech);
        } catch (IndexOutOfRangeException) {
            LTWLogger.LogError($"Could not find lane with ID {laneID}... there are only {LaneSystem.Singleton.Lanes.Length} lanes!");
        } catch (ArgumentOutOfRangeException) {
            LTWLogger.LogError($"Enum input value {elementalTechID} was out of range!");
        } catch (ClientDoesNotControlLaneException e) {
            LTWLogger.LogError(e.Message);
        }
    }

    public static void RequestTowerUpgrade(
        int clientID,
        int sourceTowerTypeID,
        int targetTowerTypeID,
        HashSet<int> towerEntityIDs
    ) {
        try {
            if (!Enum.IsDefined(typeof(TowerType), sourceTowerTypeID) ||
                !Enum.IsDefined(typeof(TowerType), targetTowerTypeID))
                throw new ArgumentOutOfRangeException();

            TowerType sourceTowerType = (TowerType) sourceTowerTypeID;
            TowerType targetTowerType = (TowerType) targetTowerTypeID;
            TowerUpgrade upgrade =
                TowerUpgrades.Singleton.GetUpgradeBySourceAndTargetTowerType(
                    sourceTowerType,
                    targetTowerType
                );

            HashSet<ServerTower> towers = GetTowersFromIDs(towerEntityIDs);

            AssertTowersAreInSameLane(towers, out int laneID);
            AssertClientIDIsInLaneWithID(clientID, laneID);

            OnRequestTowerUpgrade?.Invoke(towers, upgrade);
        }
        catch (ArgumentOutOfRangeException) {
            LTWLogger.LogError($"Enum input value was out of range!");
        }
        catch (ResourceNotFoundException e) {
            LTWLogger.LogError(e.Message);
        }
        catch (EntitiesNotInUniformLaneException e) {
            LTWLogger.LogError(e.Message);
        }
        catch (ClientDoesNotControlLaneException e) {
            LTWLogger.LogError(e.Message);
        }
    }

    public static void RequestTowerUpgradeCancellation(int clientID, HashSet<int> towerEntityIDs) {
        HashSet<ServerTower> towers = GetTowersFromIDs(towerEntityIDs);

        try {
            AssertTowersAreInSameLane(towers, out int laneID);
            AssertClientIDIsInLaneWithID(clientID, laneID);
            
            OnRequestTowerUpgradeCancellation?.Invoke(towers);
        }
        catch (EntitiesNotInUniformLaneException e) {
            LTWLogger.LogError(e.Message);
        }
        catch (ClientDoesNotControlLaneException e) {
            LTWLogger.LogError(e.Message);
        }
    }

    public static void RequestTowerSale(int clientID, HashSet<int> towerEntityIDs) {
        HashSet<ServerTower> towers = GetTowersFromIDs(towerEntityIDs);
        
        try {
            AssertTowersAreInSameLane(towers, out int laneID);
            AssertClientIDIsInLaneWithID(clientID, laneID);
            
            OnRequestTowerSale?.Invoke(towers);
        }
        catch (EntitiesNotInUniformLaneException e) {
            LTWLogger.LogError(e.Message);
        }
        catch (ClientDoesNotControlLaneException e) {
            LTWLogger.LogError(e.Message);
        }
    }

    public static void RequestTowerSaleCancellation(int clientID, HashSet<int> towerEntityIDs) {
        HashSet<ServerTower> towers = GetTowersFromIDs(towerEntityIDs);
        
        try {
            AssertTowersAreInSameLane(towers, out int laneID);
            AssertClientIDIsInLaneWithID(clientID, laneID);
            
            OnRequestTowerSaleCancellation?.Invoke(towers);
        }
        catch (EntitiesNotInUniformLaneException e) {
            LTWLogger.LogError(e.Message);
        }
        catch (ClientDoesNotControlLaneException e) {
            LTWLogger.LogError(e.Message);
        }
    }

    public static void RequestEntitiesMove(
        int clientID,
        HashSet<int> entityIDs,
        Vector3 location,
        bool isQueuedAction
    ) {
        HashSet<ServerEntity> entities = new HashSet<ServerEntity>();
        foreach (int entityID in entityIDs) {
            try {
                ServerEntity entity = ServerEntitySystem.Singleton.GetEntityByID(entityID);

                AssertClientIDIsInLaneWithID(clientID, entity.ActiveLane.ID);
                AssertEntityIsCommandable(entity);

                entities.Add(entity);
            }
            catch (NotFoundException) { }
            catch (ClientDoesNotControlLaneException) { }
            catch (EntityIsNotCommandableException) { }
        }

        try {
            AssertCollectionIsNotEmpty(entities);
        }
        catch (EmptyCollectionException) {
            return;
        }
        
        OnRequestEntitiesMove?.Invoke(entities, location, isQueuedAction);
    }

    public static void RequestEntitiesAttackTarget(
        int clientID,
        HashSet<int> attackingEntityIDs,
        int targetEntityID,
        bool isQueuedAction
    ) {
        HashSet<ServerEntity> attackingEntities = new HashSet<ServerEntity>();
        foreach (int entityID in attackingEntityIDs) {
            try {
                ServerEntity attackingEntity = ServerEntitySystem.Singleton.GetEntityByID(entityID);

                AssertClientIDIsInLaneWithID(clientID, attackingEntity.ActiveLane.ID);
                AssertEntityIsCommandable(attackingEntity);
                AssertEntityIsAttacker(attackingEntity);

                attackingEntities.Add(attackingEntity);
            }
            catch (NotFoundException) { }
            catch (ClientDoesNotControlLaneException) { }
            catch (EntityIsNotCommandableException) { }
            catch (EntityIsNotAttackerException) { }
        }
        
        try {
            AssertCollectionIsNotEmpty(attackingEntities);
            
            ServerEntity target =
                ServerEntitySystem.Singleton.GetEntityByID(targetEntityID);
            AssertClientIDIsInLaneWithID(clientID, target.ActiveLane.ID);

            OnRequestEntitiesAttackTarget?.Invoke(
                attackingEntities,
                target,
                isQueuedAction
            );
        }
        catch (EmptyCollectionException) {
            return;
        }
        catch (NotFoundException e) {
            LTWLogger.Log(e.Message);
            return;
        }
        catch (ClientDoesNotControlLaneException e) {
            LTWLogger.Log(e.Message);
            return;
        }
    }
    
    public static void RequestEntitiesAttackLocation(
        int clientID,
        HashSet<int> attackingEntityIDs,
        Vector3 targetLocation,
        bool isQueuedAction
    ) {
        HashSet<ServerEntity> attackingEntities = new HashSet<ServerEntity>();
        foreach (int entityID in attackingEntityIDs) {
            try {
                ServerEntity attackingEntity = ServerEntitySystem.Singleton.GetEntityByID(entityID);

                AssertClientIDIsInLaneWithID(clientID, attackingEntity.ActiveLane.ID);
                AssertEntityIsCommandable(attackingEntity);
                AssertEntityIsAttacker(attackingEntity);

                attackingEntities.Add(attackingEntity);
            }
            catch (NotFoundException) { }
            catch (ClientDoesNotControlLaneException) { }
            catch (EntityIsNotCommandableException) { }
            catch (EntityIsNotAttackerException) { }
        }
        
        try {
            AssertCollectionIsNotEmpty(attackingEntities);
        
            OnRequestEntitiesAttackLocation?.Invoke(
                attackingEntities,
                targetLocation,
                isQueuedAction
            );
        }
        catch (EmptyCollectionException) {
            return;
        }
        catch (NotFoundException e) {
            LTWLogger.Log(e.Message);
            return;
        }
    }

    private static void AssertClientIDIsInLaneWithID(int clientID, int laneID) {
        // TODO: When a more flexible clientID->lane system is set up, e.g. for 
        // team LTW modes, update this to use that
        int clientSlot = ServerLedgerSystem.Singleton.GetSlotForClientID(clientID);
        if (clientSlot != laneID) {
            throw new ClientDoesNotControlLaneException(clientID, laneID);
        }
    }

    private static void AssertTowersAreInSameLane(HashSet<ServerTower> towers, out int laneID) {
        laneID = -1;
        foreach (ServerTower t in towers) {
            if (laneID == -1) {
                laneID = t.ActiveLane.ID;
                continue;
            }

            if (t.ActiveLane.ID != laneID) {
                throw new EntitiesNotInUniformLaneException(laneID, t.ActiveLane.ID);
            }
        }
    }

    private static IAttacker AssertEntityIsAttacker(ServerEntity entity) {
        if (entity is IAttacker attacker) {
            return attacker;
        }

        throw new EntityIsNotAttackerException(entity);
    }

    private static ICommandable AssertEntityIsCommandable(ServerEntity entity) {
        if (entity is ICommandable commandable) {
            return commandable;
        }

        throw new EntityIsNotCommandableException(entity);
    }

    private static void AssertCollectionIsNotEmpty<T>(ICollection<T> collection) {
        if (collection.Count == 0) {
            throw new EmptyCollectionException();
        }
    }
    #endregion

    #region Server-side events
    public static void NewClientConnected(int clientID, string username) {
        OnNewClientConnected?.Invoke(clientID, username);
    }

    public static void ClientDisconnected(int clientID) {
        OnClientDisconnected?.Invoke(clientID);
    }

    public static void LedgerUpdated() {
        OnLedgerUpdated?.Invoke();
    }

    public static void GameStaging(int numPlayers) {
        OnGameStaging?.Invoke(numPlayers);
    }

    public static void GameStarted() {
        OnGameStarted?.Invoke();
    }

    public static void LaneCleanedUp(Lane lane) {
        OnLaneCleanedUp?.Invoke(lane);
    }

    public static void CreepSendRequestFulfilledPost(Lane sendingLane, EnemyType creepType) {
        OnCreepSendRequestFulfilledPost?.Invoke(sendingLane, creepType);
    }

    public static void EnemySpawned(ServerEnemy enemy) {
        OnEnemySpawn?.Invoke(enemy);
    }

    public static void TowerSpawned(ServerTower tower) {
        OnTowerSpawn?.Invoke(tower);
    }

    public static void BuilderSpawned(ServerBuilder builder) {
        OnBuilderSpawn?.Invoke(builder);
    }

    public static void EntityDestroyed(ServerEntity entity) {
        OnEntityDestroyed?.Invoke(entity);
    }

    public static void TowerUpgradeStarted(ServerTower tower, TowerUpgrade upgrade) {
        OnTowerUpgradeStarted?.Invoke(tower, upgrade);
    }

    public static void TowerUpgradeCanceled(ServerTower tower, TowerUpgrade upgrade) {
        OnTowerUpgradeCanceled?.Invoke(tower, upgrade);
    }

    public static void TowerUpgradeFinished(ServerTower tower, TowerUpgrade upgrade) {
        OnTowerUpgradeFinished?.Invoke(tower, upgrade);
    }

    public static void TowerSaleStarted(ServerTower tower) {
        OnTowerSaleStarted?.Invoke(tower);
    }

    public static void TowerSaleCanceled(ServerTower tower) {
        OnTowerSaleCanceled?.Invoke(tower);
    }

    public static void TowerSaleFinished(ServerTower tower) {
        OnTowerSaleFinished?.Invoke(tower);
    }

    public static void TowerDied(ServerTower tower) {
        OnTowerDied?.Invoke(tower);
    }
    #endregion

    private static HashSet<ServerTower> GetTowersFromIDs(HashSet<int> entityIDs) {
        HashSet<ServerTower> towers = new HashSet<ServerTower>();
        foreach (int entityID in entityIDs) {
            try {
                ServerTower tower = ServerEntitySystem.Singleton.GetTowerByEntityID(entityID);
                towers.Add(tower);
            } catch (NotFoundException) { }
        }

        return towers;
    }
}
