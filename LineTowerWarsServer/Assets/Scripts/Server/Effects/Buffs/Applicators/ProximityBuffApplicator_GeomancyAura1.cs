public class ProximityBuffApplicator_GeomancyAura1 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.GeomancyAura1Range;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.Geomancy1;

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