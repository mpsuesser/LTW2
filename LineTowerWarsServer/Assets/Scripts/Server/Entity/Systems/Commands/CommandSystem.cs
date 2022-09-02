using System.Collections.Generic;

public class CommandSystem : IEntitySystem {

    private Queue<ICommand> CommandQueue { get; set; }
    
    private ServerEntity E { get; }

    public CommandSystem(ServerEntity entity) {
        E = entity;
        
        CommandQueue = new Queue<ICommand>();
    }

    public void Update() {
        HandleCommands();
    }

    private void HandleCommands() {
        FilterTerminatedCommandsFromTopOfQueue();
        if (CommandQueue.Count == 0) return;

        ICommand activeCommand = CommandQueue.Peek();
        if (activeCommand.State == CommandState.PendingExecution) {
            activeCommand.PrepareForExecution();
            if (activeCommand.State != CommandState.Executing) {
                // PrepareForExecution() may have flagged the command for completion
                // or for failure, so we'll return and filter it on the next frame.
                return;
            }
        }
        
        activeCommand.Update();
    }

    private void FilterTerminatedCommandsFromTopOfQueue() {
        while (CommandQueue.Count > 0) {
            ICommand top = CommandQueue.Peek();
            switch (top.State) {
                case CommandState.Finished:
                case CommandState.Failed_ShouldBeSkipped:
                    top.CleanUp();
                    CommandQueue.Dequeue();
                    break;
                case CommandState.Failed_ShouldKillQueue:
                    top.CleanUp();
                    DiscardAllCommands();
                    return;
                default:
                    return;
            }
        }
    }

    public void ProcessNewCommand(ICommand command, bool isQueued) {
        if (!isQueued) {
            DiscardAllCommands();
        }
        
        CommandQueue.Enqueue(command);
    }

    private void DiscardAllCommands() {
        if (CommandQueue.Count > 0 && CommandQueue.Peek().State != CommandState.PendingExecution) {
            CommandQueue.Peek().CleanUp();
        }

        CommandQueue.Clear();
    }
}