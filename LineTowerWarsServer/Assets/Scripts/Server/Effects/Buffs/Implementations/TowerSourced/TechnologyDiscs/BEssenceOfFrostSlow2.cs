public class BEssenceOfFrostSlow2 : Buff_DurationBased_StacksRefreshDuration {
    public override BuffType Type => BuffType.EssenceOfFrostSlow2;

    protected override double BaseDuration => TraitConstants.EssenceOfFrostSlowDuration;
    
    public BEssenceOfFrostSlow2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier 
        => 1 - TraitConstants.EssenceOfFrostSlow2PerStackMultiplier * Stacks;
}