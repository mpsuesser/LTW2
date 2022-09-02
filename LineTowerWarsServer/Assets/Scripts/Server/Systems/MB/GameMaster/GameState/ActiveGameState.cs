using System.Collections.Generic;

public class ActiveGameState : GameState
{
    public override ServerGameStateType Type => ServerGameStateType.Active;
    
    public GameModeType GameMode { get; private set; }

    public ActiveGameState(TransitionFunction tf) : base(tf) {
        ServerEventBus.GameStarted();

        ServerEventBus.OnLaneCleanedUp += HandleLaneCleanedUp;
        
        LTWLogger.Log($"Entered active game state!");
    }

    public override void HandleTransitionData(GameStateTransitionData transitionData) {
        if (!(transitionData is ActiveStateTransitionData activeTransitionData)) {
            return;
        }

        GameMode = activeTransitionData.GameMode;
        
        LTWLogger.Log($"GameMode upon transition data ingestion: {GameMode}");
    }

    private void HandleLaneCleanedUp(Lane lane) {
        CheckForWinCondition();
    }

    private void CheckForWinCondition() {
        int lanesAlive = 0;
        List<PlayerInfo> alivePlayers = new List<PlayerInfo>();
        foreach (Lane lane in LaneSystem.Singleton.Lanes) {
            if (lane.Lives <= 0) {
                continue;
            }

            lanesAlive++;

            try {
                PlayerInfo playerInLane = ServerLedgerSystem.Singleton.GetPlayerInLane(lane.ID);
                alivePlayers.Add(playerInLane);
            }
            catch (NotFoundException) { }
        }

        LTWLogger.Log($"Win condition check, lanes alive: {lanesAlive}");
        switch (GameMode) {
            case GameModeType.Survival when lanesAlive == 0:
                EndGameWithWinner(null);
                break;
            case GameModeType.FFA when lanesAlive == 1:
                EndGameWithWinner(alivePlayers[0]);
                break;
        }
    }

    private void EndGameWithWinner(PlayerInfo winner) {
        CompletedStateTransitionData transitionData = new CompletedStateTransitionData(
            GameMode,
            winner
        );
        
        TransitionWithData(ServerGameStateType.Completed, transitionData);
    }
}
