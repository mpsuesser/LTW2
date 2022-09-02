public class BIgnite2 : Buff_DurationBased_NoStacks, IDoesThingsPeriodically {
    public override BuffType Type => BuffType.Ignite2;

    protected override double BaseDuration => TraitConstants.Ignite2Duration;

    public BIgnite2(
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
            TraitConstants.Ignite2DamagePerSecond,
            DamageType.Spell,
            DamageSourceType.Ignite2Tick
        );
    }
}