public class CreepNavigationSystem : NavigationSystem {
    public CreepNavigationSystem(
        ServerEnemy e,
        EffectSystem eEffectSystem
    ) : base(
        e,
        EnemyConstants.MoveSpeed[e.Type]
    ) {
        eEffectSystem.OnMoveSpeedMultiplierUpdated += SetMoveSpeedWithMultiplier;
        eEffectSystem.OnStunnedStatusUpdated += UpdateMoveSpeedBasedOnStunStatus;
    }

    private void SetMoveSpeedWithMultiplier(double aggregateMoveSpeedMultiplier) {
        if (NMA == null) {
            return;
        }
        
        NMA.speed = (float) (BaseMoveSpeed * aggregateMoveSpeedMultiplier);
    }

    private void UpdateMoveSpeedBasedOnStunStatus(bool isStunned) {
        if (!isStunned) {
            SetMoveSpeedWithMultiplier(E.Effects.AggregateMovementSpeedMultiplier);
            return;
        }

        NMA.speed = 0;
    }

    public override void Update() {
        if (
            !(E is IAttacker)
            && !DestinationSet
            && NumPathsCalculatedThisFrame < MAX_PATH_CALCULATIONS_PER_FRAME
        ) {
            RecalculateDestination();
        }
    }

    private void RecalculateDestination() {
        LTWLogger.Log("Recalculating destination...");
        ++NumPathsCalculatedThisFrame;
        
        SetDestination(
            E.ActiveLane.Endzone.GetClosestDestinationTo(E.transform.position)
        );
    }
}