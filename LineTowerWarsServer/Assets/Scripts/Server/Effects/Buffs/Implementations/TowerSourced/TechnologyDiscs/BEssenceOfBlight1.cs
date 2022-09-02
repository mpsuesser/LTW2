using System.Collections.Generic;

public class BEssenceOfBlight1 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.EssenceOfBlight1;

    public BEssenceOfBlight1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) {
        if (!(affectedEntity is IAttacker attacker)) {
            return;
        }

        attacker.Attack.OnAttackLandedPostIncludeSplashTargets += CheckForKillingBlows;
    }

    private void CheckForKillingBlows(
        ServerEntity attacker,
        List<ServerEntity> targets
    ) {
        foreach (ServerEntity target in targets) {
            if (target.HP == 0) {
                TriggerExplosion(target);
            }
        }
    }

    private void TriggerExplosion(ServerEntity explodingEntity) {
        // TODO: Send message to client about explosion event
        
        HashSet<ServerEntity> creepsInRange =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                explodingEntity,
                TraitConstants.EssenceOfBlight1ExplosionRadius,
                new CreepEntityFilter()
            );

        double damage =
            explodingEntity.MaxHealth
            * TraitConstants.EssenceOfBlight1ExplosionMultiplierOfMaxHealth;
        foreach (ServerEntity creep in creepsInRange) {
            explodingEntity.DealDamageTo(
                creep,
                damage,
                DamageType.Spell,
                DamageSourceType.EssenceOfBlight1Explosion
            );
        }
    }
}