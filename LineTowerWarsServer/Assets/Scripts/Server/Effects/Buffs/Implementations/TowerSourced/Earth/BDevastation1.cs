public class BDevastation1 : Buff_Static_Stacks {
    public override BuffType Type => BuffType.Devastation1;

    public BDevastation1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorReductionNotBelowZero =>
        TraitConstants.DevastatingAttack1ArmorReductionPerHit * Stacks;
}