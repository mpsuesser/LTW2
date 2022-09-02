using System;

public class BLingeringVoid : Buff_DurationBased_StacksRefreshDuration {
    public override BuffType Type => BuffType.LingeringVoid;

    protected override double BaseDuration => TraitConstants.LingeringVoidDuration;
    
    public BLingeringVoid(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float AttackSpeedMultiplier =>
        1 - (TraitConstants.LingeringVoidAttackSpeedReductionPerStack * Stacks);
}