using UnityEngine;

public class TExplosiveReaction1 : Trait {
    public override TraitType Type => TraitType.ExplosiveReaction1;
    
    private float LastExplosionTime { get; set; }

    public TExplosiveReaction1(ServerEntity entity) : base(entity) {
        DiscStepTrigger trigger = DiscStepTrigger.Create(entity);

        LastExplosionTime = Time.time - TraitConstants.ExplosiveReaction1Cooldown;

        trigger.OnStepTriggerActivated += CheckForExplosionCondition;
    }

    private void CheckForExplosionCondition(ServerEntity triggeringEntity) {
        if (Time.time - LastExplosionTime < TraitConstants.ExplosiveReaction1Cooldown) {
            return;
        }

        float timeSinceLastReaction =
            Time.time - ExplosiveReactionHistory.GetTimeOfLastReactionForEntity(triggeringEntity);
        if (timeSinceLastReaction < TraitConstants.ExplosiveReaction1ImmunityDuration) {
            return;
        }

        DoExplosion(triggeringEntity);
    }

    private void DoExplosion(ServerEntity target) {
        // TODO: Send client message about explosion event
        
        LastExplosionTime = Time.time;

        E.DealDamageTo(
            target,
            target.MaxHealth * TraitConstants.ExplosiveReaction1DamageAsRatioOfMaxHealth,
            DamageType.Pure,
            DamageSourceType.ExplosiveReaction1
        );
        
        ExplosiveReactionHistory.RegisterReactionOnEntity(target);
    }
}