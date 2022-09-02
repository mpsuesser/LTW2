public class ProximityBuffApplicator_WarCryAura : ProximityBuffApplicator {
    private static float AuraRange => TraitConstants.WarCryAuraRange;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.WarCry;

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