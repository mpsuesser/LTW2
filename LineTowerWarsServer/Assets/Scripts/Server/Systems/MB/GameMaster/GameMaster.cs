using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : SingletonBehaviour<GameMaster> {
    public GameState State { get; private set; }

    private void Awake() {
        InitializeSingleton(this);

        State = new LobbyGameState(TransitionToGameState);
    }

    private void TransitionToGameState(ServerGameStateType newStateType, GameStateTransitionData transitionData) {
        if (State.Type == newStateType) {
            return;
        }

        State.Leave();
        State = newStateType switch
        {
            ServerGameStateType.Lobby => new LobbyGameState(TransitionToGameState),
            ServerGameStateType.Staging => new StagingGameState(TransitionToGameState),
            ServerGameStateType.Active => new ActiveGameState(TransitionToGameState),
            ServerGameStateType.Completed => new CompletedGameState(TransitionToGameState),
            _ => throw new InvalidStateException()
        };

        if (transitionData != null) {
            State.HandleTransitionData(transitionData);
        }

        ServerSend.GameStateUpdated(State.Type);
    }

    public static GameModeType DetermineGameModeByNumberOfPlayers(int numPlayers) {
        return numPlayers == 1 ? GameModeType.Survival : GameModeType.FFA;
    }
}
