public class ProximityBuffApplicator_AncientAura1 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.AncientAura1Range;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.Ancient1;

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