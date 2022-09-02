public class ProximityBuffApplicator_EssenceOfPowerAura2 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EssenceOfPowerAura2Range;
    private static readonly EntityFilter Filter = new TowerEntityFilter();
    private static BuffType Type => BuffType.EssenceOfPower2;

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