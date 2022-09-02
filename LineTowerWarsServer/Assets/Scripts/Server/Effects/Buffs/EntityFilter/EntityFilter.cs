using UnityEngine;

public class EntityFilter {
    public EntityFilter() {}

    public virtual int InitialLayerMask => LayerMaskConstants.EntityLayerMask;

    public virtual bool PassesFilter(ServerEntity entity) {
        return true;
    }
}