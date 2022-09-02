public class LobbyGameState : GameState
{
    public override ServerGameStateType Type => ServerGameStateType.Lobby;

    private ServerLedgerSystem Ledger { get; set; }

    public LobbyGameState(TransitionFunction tf) : base(tf) {
        Ledger = ServerLedgerSystem.Singleton;

        ServerEventBus.OnLedgerUpdated += CheckLobbyStatus;
        ServerEventBus.OnRequestNewLobbySlot += HandleLobbySlotMoveRequest;
    }

    private void CheckLobbyStatus() {
        foreach (PlayerInfo player in Ledger.PlayersByClientID.Values) {
            if (player.State != ClientGameStateType.LobbyReady) {
                return;
            }
        }

        Transition(ServerGameStateType.Staging);
    }

    private void HandleLobbySlotMoveRequest(PlayerInfo requestingPlayer, int requestedSlot) {
        Ledger.TryMoveSlot(requestingPlayer.ClientID, requestedSlot);
    }

    public override void Leave() {
        ServerEventBus.OnLedgerUpdated -= CheckLobbyStatus;
        ServerEventBus.OnRequestNewLobbySlot -= HandleLobbySlotMoveRequest;
    }
}
