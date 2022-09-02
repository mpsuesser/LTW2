public class ProximityBuffApplicator_GeomancyAura2 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.GeomancyAura2Range;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.Geomancy2;

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