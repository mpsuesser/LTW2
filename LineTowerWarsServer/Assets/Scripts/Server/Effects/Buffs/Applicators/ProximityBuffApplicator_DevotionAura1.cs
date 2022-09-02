public class ProximityBuffApplicator_DevotionAura1 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.DevotionAura1Range;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.Devotion1;

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