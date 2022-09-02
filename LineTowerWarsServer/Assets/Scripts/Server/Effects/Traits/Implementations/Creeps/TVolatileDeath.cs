using System.Collections.Generic;
using UnityEngine;

public class TVolatileDeath : Trait {
    public override TraitType Type => TraitType.VolatileDeath;

    public TVolatileDeath(ServerEntity entity) : base(entity) { }

    protected override void OnHolderDeath(ServerEntity entity) {
        HashSet<ServerEntity> towersInRange =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                entity,
                TraitConstants.VolatileDeathRange,
                new TowerEntityFilter()
            );

        foreach (ServerEntity tower in towersInRange) {
            float distance = Vector3.Distance(
                entity.transform.position,
                tower.transform.position
            );
            float damageMultiplierBasedOnDistance =
                1 - distance / TraitConstants.VolatileDeathRange;
            
            entity.DealDamageTo(
                tower,
                TraitConstants.VolatileDeathMaxDamage * damageMultiplierBasedOnDistance,
                DamageType.Spell,
                DamageSourceType.VolatileDeath
            );
        }
    }
}