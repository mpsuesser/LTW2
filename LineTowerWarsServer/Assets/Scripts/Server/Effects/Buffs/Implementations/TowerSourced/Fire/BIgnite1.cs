public class BIgnite1 : Buff_DurationBased_NoStacks, IDoesThingsPeriodically {
    public override BuffType Type => BuffType.Ignite1;

    protected override double BaseDuration => TraitConstants.Ignite1Duration;

    public BIgnite1(
        ServerEntity affectedEntity,
        ServerEntity appliedByEntity
    ) : base(
        affectedEntity,
        appliedByEntity
    ) {
        Ticker.Subscribe(this);
    }

    protected override void CleanUp() {
        Ticker.Unsubscribe(this);
    }
    
    public double GetInterval() => 0.98; // guarantees full ticks before expiration
    public void DoPeriodicThing() {
        AppliedByEntity.DealDamageTo(
            AffectedEntity,
            TraitConstants.Ignite1DamagePerSecond,
            DamageType.Spell,
            DamageSourceType.Ignite1Tick
        );
    }
}