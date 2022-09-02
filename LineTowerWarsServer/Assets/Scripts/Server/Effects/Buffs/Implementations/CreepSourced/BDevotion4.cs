public class BDevotion4 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.Devotion4;
    
    public BDevotion4(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorDiff =>
        TraitConstants.DevotionAura4ArmorDiff;
}