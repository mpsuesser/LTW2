public class ProximityBuffApplicator_TurbulentWeather2 : ProximityBuffApplicator {
    private static float AuraRange => TraitConstants.TurbulentWeatherAura2Radius;
    private static readonly EntityFilter Filter = new FlyingCreepEntityFilter();
    private static BuffType Type => BuffType.TurbulentWeather2;

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