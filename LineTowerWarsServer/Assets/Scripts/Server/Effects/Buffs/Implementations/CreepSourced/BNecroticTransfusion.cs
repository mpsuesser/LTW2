public class BNecroticTransfusion : Buff_DurationBased_StacksIndependentDuration {
    public override BuffType Type => BuffType.NecroticTransfusion;

    protected override double BaseDuration => TraitConstants.NecroticTransfusionDuration;
    
    public BNecroticTransfusion(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override int LivesStolenDiff => Stacks;
}