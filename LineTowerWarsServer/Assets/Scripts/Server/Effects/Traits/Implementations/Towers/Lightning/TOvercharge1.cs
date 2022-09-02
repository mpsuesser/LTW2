public class TOvercharge1 : Trait {
    public override TraitType Type => TraitType.Overcharge1;

    public TOvercharge1(ServerEntity entity) : base(entity) { }

    public override bool HasPreMultiplierDamageDealtAdjustment => true;
    public override void PreMultiplierDamageDealtAdjustment(
        ServerEntity target,
        ref double damageAmount
    ) {
        damageAmount += (
            ((int) target.HealthPercentage) / 10
        ) * TraitConstants.Overcharge1DamagePerTenPercentCurrentHealth;
    }
}