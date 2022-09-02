public class TIgnite1 : Trait, IDoesThingsPeriodically {
    public override TraitType Type => TraitType.Ignite1;

    public TIgnite1(ServerEntity entity) : base(entity) {
        Ticker.Subscribe(this);
    }

    protected override void CleanUp() {
        Ticker.Unsubscribe(this);
    }
    
    public double GetInterval() => TraitConstants.Ignite1Interval;

    public void DoPeriodicThing() {
        try {
            BuffFactory.ApplyBuff(
                BuffType.Ignite1,
                TraitUtils.GetRandomCreepWithinGameRangeOfEntity(
                    E,
                    TraitConstants.Ignite1Range
                ),
                E
            );
        }
        catch (NoNearbyCreepsException) { }
    }
}