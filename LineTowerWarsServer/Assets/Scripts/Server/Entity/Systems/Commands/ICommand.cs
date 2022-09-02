public interface ICommand {
    void PrepareForExecution();
    void Update();
    
    CommandState State { get; }

    void CleanUp();
}

public enum CommandState {
    PendingExecution = 0,
    Executing,
    Finished,
    Failed_ShouldBeSkipped,
    Failed_ShouldKillQueue,
}