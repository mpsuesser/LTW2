using System.Collections.Generic;

public class SpellResistantGroundCreepEntityFilter : CreepEntityFilter {
    public override int InitialLayerMask => LayerMaskConstants.EnemyLayerMask;

    public override bool PassesFilter(ServerEntity entity) {
        if (
            !(entity is ServerEnemy)
            || entity.AssociatedTraitTypes.Contains(TraitType.Flying)
        ) {
            return false;
        }

        foreach (TraitType trait in entity.AssociatedTraitTypes) {
            if (TraitConstants.SpellResistanceTraitTypes.Contains(trait)) {
                return true;
            }
        }

        return false;
    }
}