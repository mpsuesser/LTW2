public class BNaturesForce : Buff_Static_Stacks {
    public override BuffType Type => BuffType.NaturesForce;

    public BNaturesForce(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float ArmorReductionNotBelowZero =>
        TraitConstants.NaturesGuidanceArmorReductionPerHit * Stacks;
}