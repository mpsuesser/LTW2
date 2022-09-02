public class ProximityBuffApplicator_ChillingDeathAura : ProximityBuffApplicator {
    private static float AuraRange => TraitConstants.ChillingDeathRadius;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.ChillingDeathAttackSpeedSlow;

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