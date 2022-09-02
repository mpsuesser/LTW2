public class ProximityBuffApplicator_EssenceOfBlightAura1 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EssenceOfBlightAura1Range;
    private static readonly EntityFilter Filter = new TowerEntityFilter();
    private static BuffType Type => BuffType.EssenceOfBlight1;

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