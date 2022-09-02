public class CompletedGameState : GameState
{
    public override ServerGameStateType Type => ServerGameStateType.Completed;
    
    private GameModeType GameMode { get; set; }
    private PlayerInfo Winner { get; set; }

    public CompletedGameState(TransitionFunction tf) : base(tf) { }
    
    public override void HandleTransitionData(GameStateTransitionData transitionData) {
        if (!(transitionData is CompletedStateTransitionData completedTransitionData)) {
            return;
        }

        GameMode = completedTransitionData.GameMode;
        Winner = completedTransitionData.Winner;

        RecordGameData();
        LTWLogger.Log($"Completed game state transition data ingest... GameMode: {GameMode}");
    }

    private void RecordGameData() {
        LTWLogger.Log("TODO: Record the results of the match");
    }
}