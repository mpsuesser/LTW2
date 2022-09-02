public class ProximityBuffApplicator_EssenceOfLightAura2 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EssenceOfLightAura2Range;
    private static readonly EntityFilter Filter = new TowerEntityFilter();
    private static BuffType Type => BuffType.EssenceOfLight2;

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