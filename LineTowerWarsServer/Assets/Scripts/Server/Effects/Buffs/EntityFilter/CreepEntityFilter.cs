using System.Collections.Generic;

public class CreepEntityFilter : EntityFilter {
    public override int InitialLayerMask => LayerMaskConstants.EnemyLayerMask;
    
    private HashSet<EnemyType> PassingCreepTypes { get; }
    
    public CreepEntityFilter() : base() {
        PassingCreepTypes = null;
    }

    public CreepEntityFilter(HashSet<EnemyType> passingCreepTypes) : base() {
        PassingCreepTypes = passingCreepTypes;
    }
    
    public override bool PassesFilter(ServerEntity entity) {
        if (!(entity is ServerEnemy creep)) {
            return false;
        }

        if (PassingCreepTypes != null && !PassingCreepTypes.Contains(creep.Type)) {
            return false;
        }

        return true;
    }
}