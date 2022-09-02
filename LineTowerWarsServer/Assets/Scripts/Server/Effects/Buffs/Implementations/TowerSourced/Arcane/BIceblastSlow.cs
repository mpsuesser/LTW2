public class BIceblastSlow : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.IceblastSlow;

    protected override double BaseDuration => TraitConstants.Spellcaster2IceblastDebuffDuration;

    public BIceblastSlow(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier =>
        TraitConstants.Spellcaster2IceblastMovementSpeedMultiplier;

    public override float AttackSpeedMultiplier =>
        TraitConstants.Spellcaster2IceblastAttackSpeedMultiplier;
}