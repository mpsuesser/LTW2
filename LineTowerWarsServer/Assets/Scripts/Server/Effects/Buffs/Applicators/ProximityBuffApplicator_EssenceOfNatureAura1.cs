public class ProximityBuffApplicator_EssenceOfNatureAura1 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EssenceOfNatureAura1Range;
    private static readonly EntityFilter Filter = new TowerEntityFilter();
    private static BuffType Type => BuffType.EssenceOfNature1;

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