public class ProximityBuffApplicator_EnduranceAura2 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EnduranceAura2Range;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.Endurance2;

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