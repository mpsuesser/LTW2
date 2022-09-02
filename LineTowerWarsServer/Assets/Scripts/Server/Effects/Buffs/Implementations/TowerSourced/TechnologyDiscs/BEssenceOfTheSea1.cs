public class BEssenceOfTheSea1 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.EssenceOfTheSea1;
    
    public BEssenceOfTheSea1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ManaRegenPerSecondDiff
        => TraitConstants.EssenceOfTheSea1ManaRegenPerSecondDiff;
}