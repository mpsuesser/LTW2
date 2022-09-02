public class BDevastation2 : Buff_Static_Stacks {
    public override BuffType Type => BuffType.Devastation2;

    public BDevastation2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorReductionNotBelowZero =>
        TraitConstants.DevastatingAttack2ArmorReductionPerHit * Stacks;
}