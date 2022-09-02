public class BDevotion2 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.Devotion2;
    
    public BDevotion2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorDiff =>
        TraitConstants.DevotionAura2ArmorDiff;
}