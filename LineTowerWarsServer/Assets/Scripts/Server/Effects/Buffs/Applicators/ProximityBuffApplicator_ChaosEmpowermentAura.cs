public class ProximityBuffApplicator_ChaosEmpowermentAura : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.ChaosEmpowermentAuraRange;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.ChaosEmpowerment;

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