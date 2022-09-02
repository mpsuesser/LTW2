using System.Collections.Generic;

public class TowerEntityFilter : EntityFilter {
    public override int InitialLayerMask => LayerMaskConstants.TowerLayerMask;
    
    private HashSet<TowerType> PassingTowerTypes { get; }
    
    public TowerEntityFilter() : base() {
        PassingTowerTypes = null;
    }

    public TowerEntityFilter(HashSet<TowerType> passingTowerTypes) : base() {
        PassingTowerTypes = passingTowerTypes;
    }
    
    public override bool PassesFilter(ServerEntity entity) {
        if (!(entity is ServerTower tower)) {
            return false;
        }

        if (PassingTowerTypes != null && !PassingTowerTypes.Contains(tower.Type)) {
            return false;
        }

        return true;
    }
}