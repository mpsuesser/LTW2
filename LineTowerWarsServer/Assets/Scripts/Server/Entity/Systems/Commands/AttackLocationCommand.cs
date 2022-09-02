using System.Diagnostics;
using UnityEngine;

public class AttackLocationCommand : AttackCommand {
    private enum AttackLocationState {
        MovingTowardDestinationAndSeekingTarget,
        PursuingTarget,
        AttackingTarget,
    }

    private AttackLocationState InternalState { get; set; }
    
    private INavigable CommandingNavigable { get; }
    private Vector3 Destination { get; }

    private ThreatSystem CommandLocalThreatSystem { get; }
    
    // Width of lane: 8 towers x 100 width per cell
    private const float ThreatGameRange = 800;
    
    public AttackLocationCommand(
        ServerEntity attackingEntity,
        Vector3 location
    ) : base(attackingEntity) {
        if (
            CommandingAttacker.Attack is StationaryThreatenableEntityAttackSystem
            || !(CommandingAttacker.Attack is NavigableEntityAttackSystem navigableAttackSystem)
        ) {
            State = CommandState.Failed_ShouldBeSkipped;
            return;
        }

        CommandingNavigable = navigableAttackSystem.Navigable;
        CommandLocalThreatSystem = new ThreatSystem(attackingEntity, ThreatGameRange);

        Destination = CommandingNavigable.Navigation.NormalizeDestination(location);
        
        State = CommandState.PendingExecution;
    }
    
    public override void PrepareForExecution() {
        SwitchToState(AttackLocationState.MovingTowardDestinationAndSeekingTarget);
        
        State = CommandState.Executing;
    }

    // TODO: This is a dinky implementation of a state system. It would be better to
    // do it properly by splitting these state behaviors up into their own classes.
    public override void Update() {
        CommandLocalThreatSystem.Update();
        EnforceInternalState();
        UpdateByInternalState();
    }

    private void EnforceInternalState() {
        float distanceToTarget;
        switch (InternalState) {
            case AttackLocationState.MovingTowardDestinationAndSeekingTarget:
                if (CommandLocalThreatSystem.HasTarget) {
                    SwitchToState(AttackLocationState.PursuingTarget);
                }
                break;
            
            case AttackLocationState.PursuingTarget:
                if (!CommandLocalThreatSystem.HasTarget) {
                    SwitchToState(AttackLocationState.MovingTowardDestinationAndSeekingTarget);
                    break;
                }
                
                distanceToTarget = Vector3.Distance(
                    CommandingNavigable.Navigation.CurrentPosition,
                    CommandLocalThreatSystem.Target.transform.position
                );
                if (distanceToTarget < CommandingAttacker.Attack.UnityRange) {
                    SwitchToState(AttackLocationState.AttackingTarget);
                }
                break;
            
            case AttackLocationState.AttackingTarget:
                if (!CommandLocalThreatSystem.HasTarget) {
                    SwitchToState(AttackLocationState.MovingTowardDestinationAndSeekingTarget);
                    break;
                }
                
                distanceToTarget = Vector3.Distance(
                    CommandingNavigable.Navigation.CurrentPosition,
                    CommandLocalThreatSystem.Target.transform.position
                );
                if (distanceToTarget > CommandingAttacker.Attack.UnityRange) {
                    SwitchToState(AttackLocationState.PursuingTarget);
                }
                break;
        }
    }

    private void SwitchToState(AttackLocationState newState) {
        LTWLogger.Log($"Switching to {newState} state!");
        switch (newState) {
            case AttackLocationState.MovingTowardDestinationAndSeekingTarget:
                CommandingNavigable.Navigation.SetDestination(Destination);
                break;
            
            case AttackLocationState.PursuingTarget:
                try {
                    Vector3 closestNavigablePoint = GetClosestNavigablePointTo(
                        CommandLocalThreatSystem.Target
                    );
                    CommandingNavigable.Navigation.SetDestination(closestNavigablePoint);
                }
                catch (NotFoundException e) {
                    LTWLogger.LogError(e.Message);
                    State = CommandState.Failed_ShouldBeSkipped;
                }
                break;
            
            case AttackLocationState.AttackingTarget:
                CommandingNavigable.Navigation.StopWithoutUnsettingDestinationFlag();
                break;
        }
        
        InternalState = newState;
    }

    private void UpdateByInternalState() {
        switch (InternalState) {
            case AttackLocationState.MovingTowardDestinationAndSeekingTarget:
                if (CommandingNavigable.Navigation.HasReachedDestination(Destination)) {
                    State = CommandState.Finished;
                }
                break;
            
            case AttackLocationState.PursuingTarget:
                break;
            
            case AttackLocationState.AttackingTarget:
                CommandingAttacker.Attack.Update(CommandLocalThreatSystem.Target);
                break;
        }
    }

    public override void CleanUp() {
        CommandingNavigable?.Navigation.Stop();
    }
}