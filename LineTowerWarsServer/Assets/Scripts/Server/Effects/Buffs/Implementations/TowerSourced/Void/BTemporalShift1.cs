using System.Collections.Generic;
using UnityEngine;

public class BTemporalShift1 : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.TemporalShift1;

    protected override double BaseDuration => TraitConstants.TemporalShift1Duration;

    private Vector3 InitialPosition { get; }
    
    public BTemporalShift1(
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

        double damage = TraitConstants.TemporalShift1ExpirationBaseDamage
                        + TraitConstants.TemporalShift1ExpirationMaxHealthDamage
                        * creep.MaxHealth;

        if (AppliedByEntity != null) {
            AppliedByEntity.DealDamageTo(
                creep,
                TraitConstants.TemporalShift1ExpirationBaseDamage
                + TraitConstants.TemporalShift1ExpirationMaxHealthDamage * creep.MaxHealth,
                DamageType.Spell,
                DamageSourceType.TemporalShift1
            );
        }
        else {
            creep.TakeDamageFrom(
                null,
                damage,
                DamageType.Spell,
                DamageSourceType.TemporalShift1
            );
        }
    }
}