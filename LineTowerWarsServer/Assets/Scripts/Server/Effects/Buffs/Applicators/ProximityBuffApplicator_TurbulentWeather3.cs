public class ProximityBuffApplicator_TurbulentWeather3 : ProximityBuffApplicator {
    private static float AuraRange => TraitConstants.TurbulentWeatherAura3Radius;
    private static readonly EntityFilter Filter = new FlyingCreepEntityFilter();
    private static BuffType Type => BuffType.TurbulentWeather3;

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