public class ProximityBuffApplicator_AncientAura2 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.AncientAura2Range;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.Ancient2;

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