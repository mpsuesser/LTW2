public class CompletedStateTransitionData : GameStateTransitionData {
    public GameModeType GameMode { get; }
    public PlayerInfo Winner { get; }

    public CompletedStateTransitionData(GameModeType gameMode, PlayerInfo winner) {
        GameMode = gameMode;
        Winner = winner;
    }
}