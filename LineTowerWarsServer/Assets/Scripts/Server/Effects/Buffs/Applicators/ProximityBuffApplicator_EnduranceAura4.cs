public class ProximityBuffApplicator_EnduranceAura4 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EnduranceAura4Range;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.Endurance4;

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