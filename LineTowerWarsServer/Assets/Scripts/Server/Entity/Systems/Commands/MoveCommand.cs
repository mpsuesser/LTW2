using UnityEngine;

public class MoveCommand : ICommand {
    public CommandState State { get; private set; }
    
    private INavigable Navigable { get; }
    private Vector3 Destination { get; }
    
    public MoveCommand(
        ServerEntity entity,
        Vector3 destination
    ) {
        if (!(entity is INavigable navigable)) {
            State = CommandState.Failed_ShouldBeSkipped;
            return;
        }

        Navigable = navigable;
        Destination = destination;
        
        State = CommandState.PendingExecution;
    }
    public void PrepareForExecution() {
        Navigable.Navigation.SetDestination(Destination);
        
        State = CommandState.Executing;
    }

    public void Update() {
        if (Navigable.Navigation.HasReachedInternalDestination()) {
            State = CommandState.Finished;
        }
    }

    public void CleanUp() {
        Navigable.Navigation.Stop();
    }
}