public class ProximityBuffApplicator_EtherealAura : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EtherealAuraRange;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.Ethereal;

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