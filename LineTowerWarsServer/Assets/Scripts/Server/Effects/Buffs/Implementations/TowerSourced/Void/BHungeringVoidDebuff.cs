public class BHungeringVoidDebuff : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.HungeringVoidDebuff;

    public BHungeringVoidDebuff(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override int HealingReceivedDiff =>
        -TraitConstants.HungeringVoidHealingReceivedFlatDeduction;

    public override float HealingReceivedMultiplier =>
        TraitConstants.HungeringVoidHealingReceivedMultiplier;
}