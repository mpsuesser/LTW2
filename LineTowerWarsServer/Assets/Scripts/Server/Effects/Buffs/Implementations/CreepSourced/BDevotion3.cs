public class BDevotion3 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.Devotion3;
    
    public BDevotion3(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorDiff =>
        TraitConstants.DevotionAura3ArmorDiff;
}