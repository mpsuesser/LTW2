public class BWarStance : Buff_ProximityBased_NoStacks {
    public override BuffType Type => BuffType.WarStance;

    public BWarStance(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorDiff =>
        TraitConstants.WarStanceArmorBonus;
}