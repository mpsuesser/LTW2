public class ProximityBuffApplicator_EssenceOfLightAura1 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EssenceOfLightAura1Range;
    private static readonly EntityFilter Filter = new TowerEntityFilter();
    private static BuffType Type => BuffType.EssenceOfLight1;

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