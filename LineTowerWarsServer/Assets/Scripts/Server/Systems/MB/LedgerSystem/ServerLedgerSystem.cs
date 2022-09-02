using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerLedgerSystem : SingletonBehaviour<ServerLedgerSystem>
{
    public Dictionary<int, PlayerInfo> PlayersByClientID { get; private set; }
    private int[] ClientIDInSlot;
    
    // TEMP, MaxSlots value is to be replaced by lobby details retrieved from PlayFab at server instantiation
    private const int MaxSlots = 10;
    private const int OpenSlot = -1;
    
    private void Awake() {
        InitializeSingleton(this);

        PlayersByClientID = new Dictionary<int, PlayerInfo>();
        ClientIDInSlot = new int[MaxSlots];
        for (int i = 0; i < MaxSlots; i++) {
            ClientIDInSlot[i] = OpenSlot;
        }
    }

    private void Start() {
        ServerEventBus.OnTableStakesPresented += RegisterNewPlayer;
        ServerEventBus.OnClientDisconnected += HandleDisconnection;
        ServerEventBus.OnClientGameStateUpdated += UpdatePlayerGameState;
    }

    private void OnDestroy() {
        ServerEventBus.OnTableStakesPresented -= RegisterNewPlayer;
        ServerEventBus.OnClientDisconnected -= HandleDisconnection;
        ServerEventBus.OnClientGameStateUpdated -= UpdatePlayerGameState;
    }

    private void RegisterNewPlayer(
        int clientID,
        string username,
        string playfabID,
        ClientGameStateType clientState
    ) {
        if (PlayersByClientID.ContainsKey(clientID)) {
            return;
        }

        int slot;
        try { slot = GetFreeSlot(); }
        catch (NoSlotsRemainingException) {
            // TODO: Disconnect the player or send them a rejection message
            LTWLogger.Log($"TODO: Player connected but there are no slots remaining... do something!");
            
            return;
        }

        ClientIDInSlot[slot] = clientID;
        PlayersByClientID.Add(
            clientID,
            new PlayerInfo(clientID, username, playfabID, slot, clientState)
        );
        
        LedgerUpdated();
    }

    private void HandleDisconnection(int clientID) {
        LTWLogger.Log("[SLS] Disconnection handler called...");
        LTWLogger.Log($"[SLS] GameMaster GameState: {GameMaster.Singleton.State.Type}");
        
        switch (GameMaster.Singleton.State.Type) {
            case ServerGameStateType.Staging:
            case ServerGameStateType.Active:
            case ServerGameStateType.Completed:
                ServerEventBus.ClientGameStateUpdated(clientID, (int) ClientGameStateType.Disconnected);
                break;
            
            case ServerGameStateType.Lobby:
            default:
                DeregisterPlayer(clientID);
                break;
        }
    }

    private void DeregisterPlayer(int clientID) {
        if (PlayersByClientID.Remove(clientID)) {
            LedgerUpdated();
        }

        ClearSlotWithClient(clientID);
    }

    private void UpdatePlayerGameState(int clientID, ClientGameStateType newState) {
        if (!PlayersByClientID.ContainsKey(clientID)) {
            LTWLogger.Log($"Players ledger did not contain key {clientID} when trying to update state to {newState}!");
            return;
        }

        if (PlayersByClientID[clientID].UpdateState(newState)) {
            LedgerUpdated();
        }
    }

    private void LedgerUpdated() {
        LTWLogger.Log($"[SLS] Ledger updated called");
        ServerEventBus.LedgerUpdated();
        ServerSend.PlayersUpdated(PlayersByClientID);
    }

    // TODO: Move a lot of this slot stuff to LobbyGameState
    private int GetFreeSlot() {
        for (int i = 0; i < MaxSlots; i++) {
            if (IsSlotAvailable(i)) {
                return i;
            }
        }

        throw new NoSlotsRemainingException(MaxSlots);
    }

    private void ClearSlotWithClient(int clientID) {
        for (int i = 0; i < MaxSlots; i++) {
            if (ClientIDInSlot[i] == clientID) {
                ClientIDInSlot[i] = OpenSlot;
            }
        }
    }

    public PlayerInfo GetPlayerByClientID(int clientID) {
        if (!PlayersByClientID.ContainsKey(clientID)) {
            throw new NotFoundException();
        }

        return PlayersByClientID[clientID];
    }

    public int GetSlotForClientID(int clientID) {
        PlayerInfo player = GetPlayerByClientID(clientID);
        return player.Slot;
    }

    public PlayerInfo GetPlayerInSlot(int slotNumber) {
        if (ClientIDInSlot[slotNumber] == OpenSlot) {
            throw new NotFoundException();
        }

        return GetPlayerByClientID(ClientIDInSlot[slotNumber]);
    }

    private bool IsSlotAvailable(int slot) {
        return ClientIDInSlot[slot] == OpenSlot;
    }

    public void TryMoveSlot(int clientID, int requestedSlot) {
        if (!IsSlotAvailable(requestedSlot)) {
            return;
        }

        ClearSlotWithClient(clientID);
        ClientIDInSlot[requestedSlot] = clientID;
        PlayersByClientID[clientID].UpdateSlot(requestedSlot);
        LedgerUpdated();
    }

    public PlayerInfo GetPlayerInLane(int laneID) {
        // TEMP: Might need updating if/when the lane-to-clientID mapping gets changed
        return GetPlayerInSlot(laneID);
    }
}
