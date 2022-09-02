public class BBlindedByTheLight2 : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.BlindedByTheLight2;

    protected override double BaseDuration => TraitConstants.BlindedByTheLight2Duration;
    
    public BBlindedByTheLight2(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float DamageDoneMultiplier =>
        TraitConstants.BlindedByTheLight2DamageDealtMultiplier;
    
    public override float SpellResistDiff =>
        TraitConstants.BlindedByTheLight2SpellResistDiff;
}