public class ProximityBuffApplicator_EssenceOfDarknessAura2 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EssenceOfDarknessAura2Range;
    private static readonly EntityFilter Filter = new TowerEntityFilter();
    private static BuffType Type => BuffType.EssenceOfDarkness2;

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