public class ProximityBuffApplicator_EssenceOfTheSeaAura1 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EssenceOfTheSeaAura1Range;
    private static readonly EntityFilter Filter = new TowerEntityFilter();
    private static BuffType Type => BuffType.EssenceOfTheSea1;

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