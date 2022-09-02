public class BVoidLashingDebuff1 : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.VoidLashingDebuff1;

    protected override double BaseDuration => TraitConstants.VoidLashing1Duration;
    
    public BVoidLashingDebuff1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override int HealingReceivedDiff =>
        -TraitConstants.VoidLashing1HealingReceivedFlatDeduction;
    public override float HealingReceivedMultiplier =>
        TraitConstants.VoidLashing1HealingReceivedMultiplier;
}