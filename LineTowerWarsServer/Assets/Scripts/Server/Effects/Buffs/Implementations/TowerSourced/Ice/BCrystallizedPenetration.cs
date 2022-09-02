public class BCrystallizedPenetration : Buff_Static_Stacks {
    public override BuffType Type => BuffType.CrystallizedPenetration;

    public BCrystallizedPenetration(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorReductionNotBelowZero =>
        TraitConstants.CrystallizedLightArmorReductionPerHit;
}