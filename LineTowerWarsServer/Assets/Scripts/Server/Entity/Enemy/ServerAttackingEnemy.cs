public abstract class ServerAttackingEnemy : ServerEnemy,
                                             IAttacker, ICommandable {
    public AttackSystem Attack { get; private set; }
    public CommandSystem Commands { get; private set; }

    protected override void Awake() {
        Attack = new CreepAttackSystem(this);
        
        base.Awake();

        Commands = new CommandSystem(this);

        OnLaneSet += IssueInitialAttackLocationCommand;
    }
    
    // Instead of the default initial behavior being to run to the
    // end of the lane, we want to issue an attack-to-location command
    // to the end of the lane. If the client decides to issue other
    // non-queued commands it will overwrite this command.
    private void IssueInitialAttackLocationCommand(ServerEntity _this) {
        AttackLocationCommand command = new AttackLocationCommand(
            this,
            ActiveLane.Endzone.GetClosestDestinationTo(transform.position)
        );
        Commands.ProcessNewCommand(command, false);
    }

    protected override void Update() {
        Commands.Update();

        base.Update();
    }
}