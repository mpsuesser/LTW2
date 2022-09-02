public class ProximityBuffApplicator_EssenceOfBlightAura2 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EssenceOfBlightAura2Range;
    private static readonly EntityFilter Filter = new TowerEntityFilter();
    private static BuffType Type => BuffType.EssenceOfBlight2;

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