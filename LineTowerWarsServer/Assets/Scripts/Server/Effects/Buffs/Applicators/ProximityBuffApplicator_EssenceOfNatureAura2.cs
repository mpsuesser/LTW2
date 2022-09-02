public class ProximityBuffApplicator_EssenceOfNatureAura2 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EssenceOfNatureAura2Range;
    private static readonly EntityFilter Filter = new TowerEntityFilter();
    private static BuffType Type => BuffType.EssenceOfNature2;

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