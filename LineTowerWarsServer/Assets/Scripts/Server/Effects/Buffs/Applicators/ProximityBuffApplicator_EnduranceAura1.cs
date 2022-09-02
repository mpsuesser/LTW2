public class ProximityBuffApplicator_EnduranceAura1 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EnduranceAura1Range;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.Endurance1;

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