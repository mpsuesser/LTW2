public class StagingGameState : GameState
{
    public override ServerGameStateType Type => ServerGameStateType.Staging;

    private ServerLedgerSystem Ledger { get; }
    
    private GameModeType GameMode { get; set; }

    public StagingGameState(TransitionFunction tf) : base(tf) {
        Ledger = ServerLedgerSystem.Singleton;

        CatchClientsStagingStatus();
        InitiateStaging();
    }

    private void CatchClientsStagingStatus() {
        ServerEventBus.OnLedgerUpdated += CheckStagingStatus;
    }

    private void CheckStagingStatus() {
        foreach (PlayerInfo player in Ledger.PlayersByClientID.Values) {
            if (player.State != ClientGameStateType.StagingReady) {
                return;
            }
        }

        ActiveStateTransitionData transitionData = new ActiveStateTransitionData(GameMode);
        TransitionWithData(ServerGameStateType.Active, transitionData);
    }

    private void InitiateStaging() {
        int numPlayers = Ledger.PlayersByClientID.Count;
        GameMode = GameMaster.DetermineGameModeByNumberOfPlayers(numPlayers);
        
        LaneSystem.Singleton.InitializeLanes(numPlayers);
        
        ServerEventBus.GameStaging(numPlayers);
    }

    public override void Leave() {
        ServerEventBus.OnLedgerUpdated -= CheckStagingStatus;
    }
}
