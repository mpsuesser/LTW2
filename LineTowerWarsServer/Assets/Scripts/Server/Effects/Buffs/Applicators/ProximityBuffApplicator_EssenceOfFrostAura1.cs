public class ProximityBuffApplicator_EssenceOfFrostAura1 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EssenceOfFrostAura1Range;
    private static readonly EntityFilter Filter = new TowerEntityFilter();
    private static BuffType Type => BuffType.EssenceOfFrost1;

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