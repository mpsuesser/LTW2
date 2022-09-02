public class ProximityBuffApplicator_EssenceOfPowerAura1 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EssenceOfPowerAura1Range;
    private static readonly EntityFilter Filter = new TowerEntityFilter();
    private static BuffType Type => BuffType.EssenceOfPower1;

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