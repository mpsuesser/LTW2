public class ProximityBuffApplicator_DevotionAura3 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.DevotionAura3Range;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.Devotion3;

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