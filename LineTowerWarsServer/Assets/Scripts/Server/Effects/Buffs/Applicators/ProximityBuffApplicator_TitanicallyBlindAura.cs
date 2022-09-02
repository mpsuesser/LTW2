public class ProximityBuffApplicator_TitanicallyBlindAura : ProximityBuffApplicator {
    private static float AuraRange => TraitConstants.TitanDefenseMechanismTitanicallyBlindAuraRadius;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.TitanicallyBlind;

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