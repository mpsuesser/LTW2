public class TEarthShieldProvider : Trait, IDoesThingsPeriodically {
    public override TraitType Type => TraitType.EarthShieldProvider;

    public TEarthShieldProvider(ServerEntity entity) : base(entity) { }

    public double GetInterval() =>
        TraitConstants.EarthShieldProviderCooldown;

    public void DoPeriodicThing() {
        try {
            ServerEntity earthShieldCandidate =
                TraitUtils.GetRandomCreepWithinGameRangeOfEntity(
                    E,
                    TraitConstants.EarthShieldProviderRange
                );

            BuffFactory.ApplyBuff(
                BuffType.EarthShield,
                earthShieldCandidate,
                E
            );
        }
        catch (NoNearbyCreepsException) { }
    }
}