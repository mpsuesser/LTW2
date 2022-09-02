using System.Collections.Generic;
using UnityEngine;

public class BTemporalImplosion : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.TemporalImplosion;

    protected override double BaseDuration => TraitConstants.TemporalImplosionDuration;

    public BTemporalImplosion(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) {
        OnRemoved += CheckForImplosionEffect;
    }

    private void CheckForImplosionEffect(Buff _b) {
        if (!(AffectedEntity is ServerEnemy creep) || creep.IsAlive) {
            return;
        }
        
        TriggerImplosionEffectAroundEntity(creep, AppliedByEntity);
    }

    public static void TriggerImplosionEffectAroundEntity(
        ServerEntity entity,
        ServerEntity attackingEntity
    ) {
        HashSet<ServerEntity> eligibleCreeps =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                entity,
                TraitConstants.TemporalImplosionDetonationRadius,
                new CreepEntityFilter(),
                true
            );
        if (eligibleCreeps.Count < 1) {
            return;
        }

        ServerEnemy target = (ServerEnemy) ServerUtil.GetRandomItemFromHashSet(eligibleCreeps);
        if (!entity.Effects.AggregateIsImmuneToHarmfulSpellEffects) {
            target.Navigation.UpdatePositionTo(target.ActiveLane.SpawnArea.GetSpawnLocation());
        }

        double damage =
            TraitConstants.TemporalImplosionExpirationBaseDamage
            + TraitConstants.TemporalImplosionExpirationMaxHealthDamage * target.MaxHealth;
        // TODO: Abstract this out to a static function that accepts null entities
        if (attackingEntity != null) {
            attackingEntity.DealDamageTo(
                target,
                damage,
                DamageType.Spell,
                DamageSourceType.TemporalImplosion
            );
        }
        else {
            target.TakeDamageFrom(
                null,
                damage,
                DamageType.Spell,
                DamageSourceType.TemporalImplosion
            );
        }
    }
}