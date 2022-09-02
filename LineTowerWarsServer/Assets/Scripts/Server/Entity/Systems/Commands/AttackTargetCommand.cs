using UnityEngine;

public class AttackTargetCommand : AttackCommand {
    private INavigable CommandingNavigable { get; set; }
    private ServerEntity Target { get; }
    
    public AttackTargetCommand(
        ServerEntity attackingEntity,
        ServerEntity targetEntity
    ) : base(attackingEntity) {
        Target = targetEntity;
        
        State = CommandState.PendingExecution;
    }
    
    public override void PrepareForExecution() {
        // TODO: This means you can't queue attack commands from a tower. Fix somehow.
        if (CommandingAttacker.Attack is StationaryThreatenableEntityAttackSystem threatenableAttackSystem) {
            threatenableAttackSystem.Threatenable.Threat.SetTarget(Target);
            State = CommandState.Finished;
            return;
        }

        if (!(CommandingAttacker.Attack is NavigableEntityAttackSystem navigableAttackSystem)) {
            State = CommandState.Failed_ShouldBeSkipped;
            LTWLogger.LogError("Attack system was neither stationary nor navigable! Should not happen!");
            return;
        }

        CommandingNavigable = navigableAttackSystem.Navigable;
        State = CommandState.Executing;
    }

    public override void Update() {
        if (
            Target == null
            || !Target.IsAlive
            || CommandingEntity.ActiveLane != Target.ActiveLane
        ) {
            State = CommandState.Finished;
            return;
        }
        
        float distanceToTarget = Vector3.Distance(
            CommandingNavigable.Navigation.CurrentPosition,
            Target.transform.position
        );
        if (distanceToTarget < CommandingAttacker.Attack.UnityRange) {
            CommandingNavigable.Navigation.Stop();
            CommandingAttacker.Attack.Update(Target);
        }
        else {
            CommandingNavigable.Navigation.SetDestination(
                GetClosestNavigablePointTo(Target)
            );
        }
    }

    public override void CleanUp() {
        CommandingNavigable?.Navigation.Stop();
    }
}