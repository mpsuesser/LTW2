public class BEssenceOfNature2 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.EssenceOfNature2;
    
    public BEssenceOfNature2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float AttackSpeedMultiplier =>
        TraitConstants.EssenceOfNature2AttackSpeedMultiplier;
}