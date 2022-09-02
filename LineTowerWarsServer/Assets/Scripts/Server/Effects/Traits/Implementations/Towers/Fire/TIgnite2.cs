public class TIgnite2 : Trait, IDoesThingsPeriodically {
    public override TraitType Type => TraitType.Ignite2;

    public TIgnite2(ServerEntity entity) : base(entity) {
        Ticker.Subscribe(this);
    }

    protected override void CleanUp() {
        Ticker.Unsubscribe(this);
    }
    
    public double GetInterval() => TraitConstants.Ignite2Interval;

    public void DoPeriodicThing() {
        try {
            BuffFactory.ApplyBuff(
                BuffType.Ignite2,
                TraitUtils.GetRandomCreepWithinGameRangeOfEntity(
                    E,
                    TraitConstants.Ignite2Range
                ),
                E
            );
        }
        catch (NoNearbyCreepsException) { }
    }
}