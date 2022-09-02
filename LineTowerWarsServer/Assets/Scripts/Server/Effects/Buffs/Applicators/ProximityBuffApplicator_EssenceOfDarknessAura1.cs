using System;

public class ProximityBuffApplicator_EssenceOfDarknessAura1 : ProximityBuffApplicator {
    private static float AuraRange => (float) TraitConstants.EssenceOfDarknessAura1Range;
    private static readonly EntityFilter Filter = new TowerEntityFilter();
    private static BuffType Type => BuffType.EssenceOfDarkness1;

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

    protected override void EntitiesAppliedToUpdatedPost() {
        int desiredStackCount = Math.Min(
            EntitiesAppliedTo.Count,
            TraitConstants.EssenceOfDarkness1MaxStacks
        );
        
        foreach (ServerEntity entity in EntitiesAppliedTo) {
            if (!entity.Buffs.TryGetBuffOfType(BuffType.EssenceOfDarkness1, out Buff b)) {
                continue;
            }

            if (b.Stacks != desiredStackCount) {
                b.SetStacks(desiredStackCount);
            }
        }
    }
}