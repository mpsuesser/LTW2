public class ProximityBuffApplicator_EssenceOfTheSeaAura2 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EssenceOfTheSeaAura2Range;
    private static readonly EntityFilter Filter = new TowerEntityFilter();
    private static BuffType Type => BuffType.EssenceOfTheSea2;

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