public class BChainsOfIceSlow : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.ChainsOfIceSlow;

    protected override double BaseDuration => TraitConstants.KirinTorMasteryChainsOfIceDebuffDuration;

    public BChainsOfIceSlow(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier =>
        TraitConstants.KirinTorMasteryChainsOfIceMovementSpeedMultiplier;

    public override float AttackSpeedMultiplier =>
        TraitConstants.KirinTorMasteryChainsOfIceAttackSpeedMultiplier;
}