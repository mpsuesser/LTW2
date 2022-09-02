using UnityEngine;

public class THypothermia : Trait {
    public override TraitType Type => TraitType.Hypothermia;

    public THypothermia(ServerEntity entity) : base(entity) { }

    public override float SlowEffectEffectivenessMultiplier =>
        TraitConstants.HypothermiaSlowEffectEffectivenessMultiplier;

    public override float DamageTakenMultiplier =>
        Mathf.Clamp(
            1 + (1 - E.Effects.AggregateMovementSpeedMultiplier),
            1f,
            TraitConstants.HypothermiaMovementSpeedBasedDamageTakenCap
        );
}