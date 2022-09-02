public class BIceLanced2 : Buff_Static_Stacks {
    public override BuffType Type => BuffType.IceLanced2;

    public BIceLanced2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorReductionNotBelowZero =>
        TraitConstants.IceLance2ArmorReductionPerHit;
}