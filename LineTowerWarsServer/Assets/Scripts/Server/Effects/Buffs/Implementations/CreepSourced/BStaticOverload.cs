public class BStaticOverload : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.StaticOverload;

    protected override double BaseDuration => TraitConstants.EngineOverloadDuration;
    
    public BStaticOverload(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float AttackSpeedMultiplier => TraitConstants.EngineOverloadAttackSpeedMultiplier;
}