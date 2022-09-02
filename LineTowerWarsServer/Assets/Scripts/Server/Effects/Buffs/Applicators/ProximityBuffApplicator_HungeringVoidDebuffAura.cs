public class ProximityBuffApplicator_HungeringVoidDebuffAura : ProximityBuffApplicator {
    private static float AuraRange => TraitConstants.HungeringVoidAuraRadius;
    private static readonly EntityFilter Filter = new CreepEntityFilter();
    private static BuffType Type => BuffType.HungeringVoidDebuff;

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