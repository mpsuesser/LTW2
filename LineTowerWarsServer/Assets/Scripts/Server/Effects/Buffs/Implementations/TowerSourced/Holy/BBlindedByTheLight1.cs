public class BBlindedByTheLight1 : Buff_DurationBased_NoStacks {
    public override BuffType Type => BuffType.BlindedByTheLight1;

    protected override double BaseDuration => TraitConstants.BlindedByTheLight1Duration;
    
    public BBlindedByTheLight1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) { }

    public override float DamageDoneMultiplier =>
        TraitConstants.BlindedByTheLight1DamageDealtMultiplier;
    
    public override float SpellResistDiff =>
        TraitConstants.BlindedByTheLight1SpellResistDiff;
}