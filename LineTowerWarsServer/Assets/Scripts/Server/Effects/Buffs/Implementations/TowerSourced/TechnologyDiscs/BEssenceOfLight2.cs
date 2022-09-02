public class BEssenceOfLight2 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.EssenceOfLight2;
    
    public BEssenceOfLight2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorDiff =>
        TraitConstants.EssenceOfLight2ArmorDiff;
    
    public override float HealingReceivedMultiplier =>
        TraitConstants.EssenceOfLight2HealingReceivedMultiplier;
}