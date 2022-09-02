public class ProximityBuffApplicator_EnduranceAura3 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EnduranceAura3Range;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.Endurance3;

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