public class ProximityBuffApplicator_DevotionAura2 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.DevotionAura2Range;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.Devotion2;

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