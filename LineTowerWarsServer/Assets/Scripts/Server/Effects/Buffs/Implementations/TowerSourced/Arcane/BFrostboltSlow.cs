public class BFrostboltSlow : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.FrostboltSlow;

    protected override double BaseDuration => TraitConstants.Spellcaster1FrostboltDebuffDuration;

    public BFrostboltSlow(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float MovementSpeedMultiplier =>
        TraitConstants.Spellcaster1FrostboltMovementSpeedMultiplier;
}