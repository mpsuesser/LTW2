using System.Collections.Generic;
using UnityEngine;

public class TNecroticTransfusion : Trait {
    public override TraitType Type => TraitType.NecroticTransfusion;

    public TNecroticTransfusion(ServerEntity entity) : base(entity) { }

    protected override void OnHolderDeath(ServerEntity entity) {
        HashSet<ServerEntity> creepsInRange =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                entity,
                TraitConstants.NecroticTransfusionRange,
                new CreepEntityFilter()
            );

        ServerEntity closest = null;
        float closestDistance = Mathf.Infinity;
        foreach (ServerEntity creep in creepsInRange) {
            float distance = Vector3.Distance(
                entity.transform.position,
                creep.transform.position
            );
            
            if (distance < closestDistance) {
                closest = creep;
                closestDistance = distance;
            }
        }

        if (closest != null) {
            BuffFactory.ApplyBuff(
                BuffType.NecroticTransfusion,
                closest,
                entity
            );
        }
    }
}