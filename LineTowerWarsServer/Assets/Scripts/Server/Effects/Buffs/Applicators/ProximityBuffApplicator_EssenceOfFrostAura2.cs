public class ProximityBuffApplicator_EssenceOfFrostAura2 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EssenceOfFrostAura2Range;
    private static readonly EntityFilter Filter = new TowerEntityFilter();
    private static BuffType Type => BuffType.EssenceOfFrost2;

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