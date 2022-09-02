public class TOvercharge2 : Trait {
    public override TraitType Type => TraitType.Overcharge2;

    public TOvercharge2(ServerEntity entity) : base(entity) { }

    public override bool HasPreMultiplierDamageDealtAdjustment => true;
    public override void PreMultiplierDamageDealtAdjustment(
        ServerEntity target,
        ref double damageAmount
    ) {
        damageAmount += (
            ((int) target.HealthPercentage) / 10
        ) * TraitConstants.Overcharge2DamagePerTenPercentCurrentHealth;
    }
}