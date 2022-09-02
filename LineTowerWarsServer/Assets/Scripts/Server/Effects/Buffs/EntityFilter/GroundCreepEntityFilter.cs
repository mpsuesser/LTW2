public class GroundCreepEntityFilter : CreepEntityFilter {
    public override int InitialLayerMask => LayerMaskConstants.EnemyLayerMask;
    
    public override bool PassesFilter(ServerEntity entity) {
        if (
            !(entity is ServerEnemy)
            || entity.AssociatedTraitTypes.Contains(TraitType.Flying)
        ) {
            return false;
        }

        return true;
    }
}