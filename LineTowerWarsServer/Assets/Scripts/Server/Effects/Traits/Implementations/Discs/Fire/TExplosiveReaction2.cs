using UnityEngine;

public class TExplosiveReaction2 : Trait {
    public override TraitType Type => TraitType.ExplosiveReaction2;
    
    private float LastExplosionTime { get; set; }

    public TExplosiveReaction2(ServerEntity entity) : base(entity) {
        DiscStepTrigger trigger = DiscStepTrigger.Create(entity);

        LastExplosionTime = Time.time - TraitConstants.ExplosiveReaction2Cooldown;

        trigger.OnStepTriggerActivated += CheckForExplosionCondition;
    }

    private void CheckForExplosionCondition(ServerEntity triggeringEntity) {
        if (Time.time - LastExplosionTime < TraitConstants.ExplosiveReaction2Cooldown) {
            return;
        }

        float timeSinceLastReaction =
            Time.time - ExplosiveReactionHistory.GetTimeOfLastReactionForEntity(triggeringEntity);
        if (timeSinceLastReaction < TraitConstants.ExplosiveReaction2ImmunityDuration) {
            return;
        }

        DoExplosion(triggeringEntity);
    }

    private void DoExplosion(ServerEntity target) {
        // TODO: Send client message about explosion event
        
        LastExplosionTime = Time.time;

        E.DealDamageTo(
            target,
            target.MaxHealth * TraitConstants.ExplosiveReaction2DamageAsRatioOfMaxHealth,
            DamageType.Pure,
            DamageSourceType.ExplosiveReaction2
        );
        
        ExplosiveReactionHistory.RegisterReactionOnEntity(target);
    }
}