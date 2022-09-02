public class BDevotion1 : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.Devotion1;
    
    public BDevotion1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorDiff =>
        TraitConstants.DevotionAura1ArmorDiff;
}