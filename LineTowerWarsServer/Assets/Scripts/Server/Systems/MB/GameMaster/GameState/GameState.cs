public abstract class GameState
{
    public abstract ServerGameStateType Type { get; }

    public delegate void TransitionFunction(ServerGameStateType newStateType, GameStateTransitionData transitionData);
    private readonly TransitionFunction _stateMachineTransition;

    protected GameState(TransitionFunction tf) {
        _stateMachineTransition = tf;
    }

    public virtual void HandleTransitionData(GameStateTransitionData transitionData) {
        
    }

    public virtual void OnLobbyReadyUp() { }
    public virtual void OnLobbyReadyDown() { }
    public virtual void OnGameReadyUp() { }
    public virtual void Leave() { }

    protected void Transition(ServerGameStateType newStateType) {
        _stateMachineTransition(newStateType, null);
    }

    protected void TransitionWithData(ServerGameStateType newStateType, GameStateTransitionData transitionData) {
        _stateMachineTransition(newStateType, transitionData);
    }
}
