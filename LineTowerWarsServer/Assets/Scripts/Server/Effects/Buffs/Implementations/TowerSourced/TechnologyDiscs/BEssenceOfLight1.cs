public class BEssenceOfLight1 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.EssenceOfLight1;
    
    public BEssenceOfLight1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorDiff =>
        TraitConstants.EssenceOfLight1ArmorDiff;
    
    public override float HealingReceivedMultiplier =>
        TraitConstants.EssenceOfLight1HealingReceivedMultiplier;
}