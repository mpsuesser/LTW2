public class BVolcanicOpportunity : Buff_Static_NoStacks {
    public override BuffType Type => BuffType.VolcanicOpportunity;

    public BVolcanicOpportunity(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float AttackSpeedMultiplier =>
        TraitConstants.VolcanicEruptionLowHealthAttackSpeedMultiplier;
}