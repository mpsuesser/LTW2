public class ProximityBuffApplicator_TurbulentWeather1 : ProximityBuffApplicator {
    private static float AuraRange => TraitConstants.TurbulentWeatherAura1Radius;
    private static readonly EntityFilter Filter = new FlyingCreepEntityFilter();
    private static BuffType Type => BuffType.TurbulentWeather1;

    public static ProximityBuffApplicator Create(
        ServerEntity auraProvider
    ) {
        ProximityBuffApplicator applicator =
            Create(
                Type,
                auraProvider,
                AuraRange,
                Filter
            );

        return applicator;
    }
}