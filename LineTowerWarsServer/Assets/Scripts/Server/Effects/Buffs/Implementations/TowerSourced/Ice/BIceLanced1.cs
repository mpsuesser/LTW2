public class BIceLanced1 : Buff_Static_Stacks {
    public override BuffType Type => BuffType.IceLanced1;

    public BIceLanced1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorReductionNotBelowZero =>
        TraitConstants.IceLance1ArmorReductionPerHit;
}