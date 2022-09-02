public class BEssenceOfNature1 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.EssenceOfNature1;
    
    public BEssenceOfNature1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float AttackSpeedMultiplier =>
        TraitConstants.EssenceOfNature1AttackSpeedMultiplier;
}