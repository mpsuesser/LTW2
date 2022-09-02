public class ActiveStateTransitionData : GameStateTransitionData {
    public GameModeType GameMode { get; }

    public ActiveStateTransitionData(GameModeType gameMode) {
        GameMode = gameMode;
    }
}