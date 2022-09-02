public class BEssenceOfFrostSlow1 : Buff_DurationBased_StacksRefreshDuration {
    public override BuffType Type => BuffType.EssenceOfFrostSlow1;

    protected override double BaseDuration => TraitConstants.EssenceOfFrostSlowDuration;
    
    public BEssenceOfFrostSlow1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier 
        => 1 - TraitConstants.EssenceOfFrostSlow1PerStackMultiplier * Stacks;
}