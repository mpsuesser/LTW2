using System.Collections.Generic;

public class TAnnihilation : Trait {
    public override TraitType Type => TraitType.Annihilation;

    private ServerEntity PreviousTarget { get; set; }
    private int ConsecutiveStrikesOnSameTarget { get; set; }

    public TAnnihilation(ServerEntity entity) : base(entity) {
        PreviousTarget = null;
        ConsecutiveStrikesOnSameTarget = 0;
        
        // TODO: Add chaining
    }

    public override bool HasPreMultiplierDamageDealtAdjustment => true;
    public override void PreMultiplierDamageDealtAdjustment(
        ServerEntity target,
        ref double damageAmount
    ) {
        UpdateConsecutiveStrikesCount(target);

        damageAmount *= 1 + (
            ConsecutiveStrikesOnSameTarget
            * TraitConstants.AnnihilationDamageIncreasePerHit
        );
    }

    public override bool HasCustomHandleAttackLandedImplementation => true;
    public override void CustomHandleAttackLandedImplementation(
        ServerEntity target,
        AttackEventData eventData,
        ref double totalDamageAccumulator,
        ref List<ServerEntity> allTargetsAccumulator
    ) {
        HashSet<ServerEntity> nearbyCreeps =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                target,
                TraitConstants.AnnihilationAdditionalTargetsRadius,
                E.Effects.AggregateAttackTargetEligibilityFilter,
                false
            );
        
        allTargetsAccumulator.Add(target);
        foreach (ServerEntity nearbyCreep in nearbyCreeps) {
            if (allTargetsAccumulator.Count >= TraitConstants.AnnihilationAdditionalTargetsCount + 1) {
                break;
            }
            
            allTargetsAccumulator.Add(nearbyCreep);
        }
        
        // TODO: Send the event to client to indicate the chaining animation

        for (int i = 0; i < allTargetsAccumulator.Count; i++) {
            totalDamageAccumulator += E.DealDamageTo(
                allTargetsAccumulator[i],
                eventData.InitialSnapshotDamage
                    * (1 - i * TraitConstants.AnnihilationChainedTargetDamageDropoffPerHit),
                eventData.DmgType,
                i == 0
                    ? DamageSourceType.AutoAttack
                    : DamageSourceType.AnnihilationChain
            );
        }
    }

    private void UpdateConsecutiveStrikesCount(ServerEntity target) {
        if (target != PreviousTarget) {
            ConsecutiveStrikesOnSameTarget = 0;
            PreviousTarget = target;
            return;
        }

        ConsecutiveStrikesOnSameTarget++;
    }
}