public class ProximityBuffApplicator_DevotionAura4 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.DevotionAura4Range;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.Devotion4;

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