public class BEssenceOfTheSea2 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.EssenceOfTheSea2;
    
    public BEssenceOfTheSea2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ManaRegenPerSecondDiff
        => TraitConstants.EssenceOfTheSea2ManaRegenPerSecondDiff;
}