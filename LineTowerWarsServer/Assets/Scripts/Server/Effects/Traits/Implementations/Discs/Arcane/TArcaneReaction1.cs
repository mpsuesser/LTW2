using System.Collections.Generic;
using UnityEngine;

public class TArcaneReaction1 : Trait {
    public override TraitType Type => TraitType.ArcaneReaction1;
    
    private float LastPurgeTime { get; set; }

    public TArcaneReaction1(ServerEntity entity) : base(entity) {
        DiscStepTrigger trigger = DiscStepTrigger.Create(entity);

        LastPurgeTime = Time.time - TraitConstants.ArcaneReaction1Cooldown;

        trigger.OnStepTriggerActivated += CheckForPurgeCondition;
    }

    private void CheckForPurgeCondition(ServerEntity triggeringEntity) {
        if (Time.time - LastPurgeTime < TraitConstants.ArcaneReaction1Cooldown) {
            return;
        }

        foreach (TraitType traitType in triggeringEntity.AssociatedTraitTypes) {
            if (TraitConstants.SpellResistanceTraitTypes.Contains(traitType)) {
                DoPurge(triggeringEntity);
                return;
            }
        }
    }

    private void DoPurge(ServerEntity mainTarget) {
        // TODO: Send message to client indicating purge event
        
        LastPurgeTime = Time.time;
        
        HashSet<ServerEntity> actualPurgeTargets = new HashSet<ServerEntity> {mainTarget};
        HashSet<ServerEntity> eligiblePurgeTargets =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                mainTarget,
                TraitConstants.ArcaneReaction1Radius,
                new SpellResistantGroundCreepEntityFilter()
            );

        foreach (ServerEntity eligiblePurgeTarget in eligiblePurgeTargets) {
            if (actualPurgeTargets.Count >= TraitConstants.ArcaneReaction1MaxPurgeTargets) {
                break;
            }

            actualPurgeTargets.Add(eligiblePurgeTarget);
        }

        foreach (ServerEntity purgeTarget in actualPurgeTargets) {
            foreach (TraitType traitType in purgeTarget.AssociatedTraitTypes) {
                if (TraitConstants.SpellResistanceTraitTypes.Contains(traitType)) {
                    purgeTarget.Traits.PurgeTraitType(traitType);
                }
            }
        }
    }
}