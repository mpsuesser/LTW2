public class BVoidLashingDebuff2 : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.VoidLashingDebuff2;

    protected override double BaseDuration => TraitConstants.VoidLashing2Duration;
    
    public BVoidLashingDebuff2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override int HealingReceivedDiff =>
        -TraitConstants.VoidLashing2HealingReceivedFlatDeduction;
    public override float HealingReceivedMultiplier =>
        TraitConstants.VoidLashing2HealingReceivedMultiplier;
}