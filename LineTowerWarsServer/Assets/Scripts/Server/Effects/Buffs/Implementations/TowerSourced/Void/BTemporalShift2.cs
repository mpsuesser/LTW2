using System.Collections.Generic;
using UnityEngine;

public class BTemporalShift2 : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.TemporalShift2;

    protected override double BaseDuration => TraitConstants.TemporalShift2Duration;

    private Vector3 InitialPosition { get; }
    
    public BTemporalShift2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) {
        InitialPosition = affectedEntity.transform.position;

        OnRemoved += ReturnToInitialPositionAndIncurDamage;
    }

    private void ReturnToInitialPositionAndIncurDamage(Buff _b) {
        if (!(AffectedEntity is ServerEnemy creep) || !creep.IsAlive) {
            return;
        }
        
        creep.Navigation.UpdatePositionTo(InitialPosition);

        double damage = TraitConstants.TemporalShift2ExpirationBaseDamage
                        + TraitConstants.TemporalShift2ExpirationMaxHealthDamage
                        * creep.MaxHealth;

        if (AppliedByEntity != null) {
            AppliedByEntity.DealDamageTo(
                creep,
                TraitConstants.TemporalShift2ExpirationBaseDamage
                + TraitConstants.TemporalShift2ExpirationMaxHealthDamage * creep.MaxHealth,
                DamageType.Spell,
                DamageSourceType.TemporalShift2
            );
        }
        else {
            creep.TakeDamageFrom(
                null,
                damage,
                DamageType.Spell,
                DamageSourceType.TemporalShift2
            );
        }
    }
}