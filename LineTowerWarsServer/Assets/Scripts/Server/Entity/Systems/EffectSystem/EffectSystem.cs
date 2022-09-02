using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EffectSystem : IEntitySystem {
    // TODO: This could be scaled if there are many other similar usecases
    public event Action<double> OnMoveSpeedMultiplierUpdated;
    public event Action<bool> OnStunnedStatusUpdated;
    private const float MinimumPrecisionChangeForUpdate = 0.005f;
    
    private ServerEntity E { get; }

    public EffectSystem(ServerEntity e) {
        E = e;
        
        // Serves same purpose as initializing all multipliers to 1 here
        UpdateMultipliers();
        
        E.Traits.OnActiveTraitsUpdated += UpdateMultipliers;
        E.Buffs.OnActiveBuffsUpdated += UpdateMultipliers;
    }
    
    public float AggregateDamageDealtMultiplier { get; private set; }
    public HashSet<Effect.BaseDamageDealtAdjustment> AggregatePreMultiplierDamageDealtAdjustments { get; private set; }
    public HashSet<Effect.PostDamageTakenAdjustment> AggregatePostMultiplierDamageTakenAdjustments { get; private set; }
    
    public bool AggregateHasCustomHandleAttackLandedImplementation { get; private set; }
    public Effect.HandleAttackLandedImplementation CustomHandleAttackLandedImplementation { get; private set; }

    public float AggregateDamageTakenMultiplier { get; private set; }
    public float AggregateAttackSpeedMultiplier { get; private set; }
    public float AggregateMovementSpeedMultiplier { get; private set; }
    public float AggregateHealingReceivedMultiplier { get; private set; }
    
    public int AggregateAdditionalAttackTargets { get; private set; }
    public int AggregateHealingReceivedDiff { get; private set; }
    public int AggregateLivesStolenDiff { get; private set; }
    public float AggregateArmorReductionNotBelowZero { get; private set; }
    public float AggregateArmorDiff { get; private set; }
    public float AggregateSpellResistDiff { get; private set; }
    public float AggregateManaRegenPerSecondDiff { get; private set; }
    
    public float AggregatePhysicalSplashDamageTakenMultiplier { get; private set; }
    public float AggregateHarmfulEffectDurationMultiplier { get; private set; }
    public float AggregateArmorReductionEffectEffectivenessMultiplier { get; private set; }
    public float AggregateSlowEffectEffectivenessMultiplier { get; private set; }
    public float AggregateMovementSpeedMultiplierFloor { get; private set; }
    public bool AggregateIsImmuneToSlowEffects { get; private set; }
    public bool AggregateIsImmuneToHarmfulSpellEffects { get; private set; }
    public bool AggregateIsStunned { get; private set; }
    public bool AggregateRemovesCreepKillBounty { get; private set; }
    public bool AggregateAttacksIgnoreArmorValue { get; private set; }
    
    public EntityFilter AggregateAttackTargetEligibilityFilter { get; private set; }

    public void UpdateMultipliers() {
        double prevMovementSpeedMultiplier = AggregateMovementSpeedMultiplier;
        bool prevIsStunned = AggregateIsStunned;
        
        AggregateDamageDealtMultiplier = 1;
        AggregatePreMultiplierDamageDealtAdjustments = new HashSet<Effect.BaseDamageDealtAdjustment>();
        AggregatePostMultiplierDamageTakenAdjustments = new HashSet<Effect.PostDamageTakenAdjustment>();
        AggregateHasCustomHandleAttackLandedImplementation = false;
        AggregateDamageTakenMultiplier = 1;
        AggregateAttackSpeedMultiplier = 1;
        AggregateMovementSpeedMultiplier = 1;
        AggregateHealingReceivedMultiplier = 1;
        AggregateAdditionalAttackTargets = 0;
        AggregateHealingReceivedDiff = 0;
        AggregateArmorReductionNotBelowZero = 0;
        AggregateArmorDiff = 0;
        AggregateSpellResistDiff = 0;
        AggregateLivesStolenDiff = 0;
        AggregateManaRegenPerSecondDiff = 0;
        AggregateHarmfulEffectDurationMultiplier = 1;
        AggregateArmorReductionEffectEffectivenessMultiplier = 1;
        AggregateSlowEffectEffectivenessMultiplier = 1;
        AggregatePhysicalSplashDamageTakenMultiplier = 1;
        AggregateMovementSpeedMultiplierFloor = 0;
        AggregateIsImmuneToSlowEffects = false;
        AggregateIsImmuneToHarmfulSpellEffects = false;
        AggregateIsStunned = false;
        AggregateRemovesCreepKillBounty = false;
        AggregateAttacksIgnoreArmorValue = false;
        AggregateAttackTargetEligibilityFilter = new CreepEntityFilter();

        float aggregateNegativeMovementSpeedEffectsMultiplier = 1;
        float aggregateArmorDiffBeforeAdjustment = 0;
        float aggregateArmorReductionDiff = 0;

        HashSet<Effect> activeEffects = new HashSet<Effect>(
            E.Traits.ActiveTraits.Union<Effect>(
                    E.Buffs.ActiveBuffs
                )
            );
        foreach (Effect effect in activeEffects) {
            AggregateDamageDealtMultiplier *= effect.DamageDoneMultiplier;
            AggregateDamageTakenMultiplier *= effect.DamageTakenMultiplier;
            AggregateAttackSpeedMultiplier *= effect.AttackSpeedMultiplier;
            AggregateMovementSpeedMultiplier *= effect.MovementSpeedMultiplier;
            AggregateHealingReceivedMultiplier *= effect.HealingReceivedMultiplier;
            if (effect.MovementSpeedMultiplier < 1) {
                aggregateNegativeMovementSpeedEffectsMultiplier *= effect.MovementSpeedMultiplier;
            }

            AggregateAdditionalAttackTargets += effect.AdditionalAttackTargets;

            AggregateArmorReductionNotBelowZero += effect.ArmorReductionNotBelowZero;
            aggregateArmorDiffBeforeAdjustment += effect.ArmorDiff;
            if (effect.ArmorDiff < 0) {
                aggregateArmorReductionDiff += effect.ArmorDiff;
            }

            AggregateHealingReceivedDiff += effect.HealingReceivedDiff;
            AggregateSpellResistDiff += effect.SpellResistDiff;
            AggregateLivesStolenDiff += effect.LivesStolenDiff;
            AggregateManaRegenPerSecondDiff += effect.ManaRegenPerSecondDiff;
            
            AggregateHarmfulEffectDurationMultiplier *= effect.HarmfulEffectDurationMultiplier;
            AggregateArmorReductionEffectEffectivenessMultiplier *= effect.ArmorReductionEffectEffectivenessMultiplier;
            AggregateSlowEffectEffectivenessMultiplier *= effect.SlowEffectEffectivenessMultiplier;
            AggregatePhysicalSplashDamageTakenMultiplier *= effect.PhysicalSplashDamageTakenMultiplier;
            AggregateMovementSpeedMultiplierFloor = Math.Max(
                AggregateMovementSpeedMultiplierFloor,
                effect.MovementSpeedMultiplierFloor
            );
            
            AggregateIsImmuneToSlowEffects = AggregateIsImmuneToSlowEffects || effect.IsImmuneToSlowEffects;
            AggregateIsImmuneToHarmfulSpellEffects =
                AggregateIsImmuneToHarmfulSpellEffects || effect.IsImmuneToHarmfulSpellEffects;
            AggregateIsStunned = AggregateIsStunned || effect.IsStunned;
            AggregateRemovesCreepKillBounty = AggregateRemovesCreepKillBounty || effect.RemovesCreepKillBounty;
            AggregateAttacksIgnoreArmorValue = AggregateAttacksIgnoreArmorValue || effect.AttacksIgnoreArmorValue;
            
            if (effect.HasPreMultiplierDamageDealtAdjustment) {
                AggregatePreMultiplierDamageDealtAdjustments.Add(effect.PreMultiplierDamageDealtAdjustment);
            }

            if (effect.HasPostMultiplierDamageTakenAdjustment) {
                AggregatePostMultiplierDamageTakenAdjustments.Add(effect.PostMultiplierDamageTakenAdjustment);
            }

            if (effect.HasCustomHandleAttackLandedImplementation) {
                AggregateHasCustomHandleAttackLandedImplementation = true;
                CustomHandleAttackLandedImplementation = effect.CustomHandleAttackLandedImplementation;
            }

            if (effect.AttackTargetEligibilityFilter != null) {
                AggregateAttackTargetEligibilityFilter = effect.AttackTargetEligibilityFilter;
            }
        }

        AggregateArmorReductionNotBelowZero *= AggregateArmorReductionEffectEffectivenessMultiplier;
        AggregateArmorDiff = aggregateArmorDiffBeforeAdjustment
                             + aggregateArmorReductionDiff
                             - (
                                 aggregateArmorReductionDiff
                                 * AggregateArmorReductionEffectEffectivenessMultiplier
                             );

        if (Math.Abs(AggregateSlowEffectEffectivenessMultiplier - 1) > Mathf.Epsilon) {
            // Recalculate movement speed based on recalculated slow effectiveness...
            
            // First, undo negative movement speed changes aggregated in the loop above 
            AggregateMovementSpeedMultiplier /= aggregateNegativeMovementSpeedEffectsMultiplier;
            
            // Then update the aggregate negative movement speed changes based on aggregate slow effectiveness multiplier
            aggregateNegativeMovementSpeedEffectsMultiplier += (1 - aggregateNegativeMovementSpeedEffectsMultiplier) *
                                                               AggregateSlowEffectEffectivenessMultiplier;
            
            // Finally, re-apply the aggregate movement speed based on this new aggregate negative effect number
            AggregateMovementSpeedMultiplier *= aggregateNegativeMovementSpeedEffectsMultiplier;
        }

        // If we're immune to slow effects, undo all slow effects from the aggregate
        if (AggregateIsImmuneToSlowEffects) {
            AggregateMovementSpeedMultiplier /= aggregateNegativeMovementSpeedEffectsMultiplier;
        }

        AggregateMovementSpeedMultiplier = Math.Max(
            AggregateMovementSpeedMultiplier,
            AggregateMovementSpeedMultiplierFloor
        );

        if (
            Math.Abs(
                AggregateMovementSpeedMultiplier - prevMovementSpeedMultiplier
            ) >= MinimumPrecisionChangeForUpdate
        ) {
            OnMoveSpeedMultiplierUpdated?.Invoke(AggregateMovementSpeedMultiplier);
        }

        if (prevIsStunned != AggregateIsStunned) {
            OnStunnedStatusUpdated?.Invoke(AggregateIsStunned);
        }
    }
}
