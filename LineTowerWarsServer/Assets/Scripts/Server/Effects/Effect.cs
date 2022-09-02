using System.Collections.Generic;
using UnityEngine;

public abstract class Effect {
    public delegate void BaseDamageDealtAdjustment(
        ServerEntity target,
        ref double damageAmount
    );

    public delegate void PostDamageTakenAdjustment(
        ServerEntity damageDealer,
        ref double damageAmount
    );

    public delegate void HandleAttackLandedImplementation(
        ServerEntity target,
        AttackEventData eventData,
        ref double totalDamageAccumulator,
        ref List<ServerEntity> allTargetsAccumulator
    );

    protected virtual void CleanUp() { }
    
    public virtual float DamageDoneMultiplier => 1f;
    public virtual bool HasPreMultiplierDamageDealtAdjustment => false;
    public virtual void PreMultiplierDamageDealtAdjustment(
        ServerEntity target,
        ref double damageAmount
    ) { }

    public virtual bool HasPostMultiplierDamageTakenAdjustment => false;
    public virtual void PostMultiplierDamageTakenAdjustment(
        ServerEntity damageDealer,
        ref double damageAmount
    ) { }

    public virtual bool HasCustomHandleAttackLandedImplementation => false;
    public virtual void CustomHandleAttackLandedImplementation(
        ServerEntity target,
        AttackEventData eventData,
        ref double totalDamageAccumulator,
        ref List<ServerEntity> allTargetsAccumulator
    ) { }

    public virtual float DamageTakenMultiplier => 1f;
    public virtual float HealingReceivedMultiplier => 1f;
    public virtual int HealingReceivedDiff => 0;
    public virtual float AttackSpeedMultiplier => 1f;
    public virtual float MovementSpeedMultiplier => 1f;
    public virtual float MovementSpeedMultiplierFloor => 0f;

    public virtual int AdditionalAttackTargets => 0;
    public virtual int LivesStolenDiff => 0;
    public virtual float ArmorDiff => 0;
    public virtual float ArmorReductionNotBelowZero => 0;
    public virtual float SpellResistDiff => 0;

    public virtual float ManaRegenPerSecondDiff => 0;

    public virtual float PhysicalSplashDamageTakenMultiplier => 1f;
    public virtual float HarmfulEffectDurationMultiplier => 1f;
    public virtual float ArmorReductionEffectEffectivenessMultiplier => 1f;
    public virtual float SlowEffectEffectivenessMultiplier => 1f;
    public virtual bool IsImmuneToSlowEffects => false;
    public virtual bool IsImmuneToHarmfulSpellEffects => false;
    public virtual bool IsStunned => false;
    public virtual bool RemovesCreepKillBounty => true;
    public virtual bool AttacksIgnoreArmorValue => false;

    // Only supports a single override per entity currently. Multiple would result in
    // only the last-read effect's override being used.
    public virtual EntityFilter AttackTargetEligibilityFilter => null;
}