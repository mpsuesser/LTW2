using System.Collections.Generic;
using UnityEngine;

public static class TraitUtils {
    public static void TriggerUnholySacrificeHeal(
        ServerEntity e,
        int healAmount,
        float radius
    ) {
        Collider[] colliders = Physics.OverlapSphere(
            e.transform.position,
            radius / 10,
            LayerMaskConstants.EnemyLayerMask
        );

        foreach (Collider collider in colliders) {
            ServerEnemy creep = collider.gameObject.GetComponent<ServerEnemy>();
            if (creep == null || creep == e) {
                return;
            }
            
            e.DoHealingTo(creep, healAmount);
        }
    }

    public static void TriggerDeathPactRevive(
        ServerEntity entity,
        float reviveHealthMultiplier,
        float reviveDelay
    ) {
        if (!(entity is ServerEnemy creep)) {
            return;
        }
        
        UnitSpawnSystem.Singleton.RespawnUnitInSeconds(
            creep,
            reviveHealthMultiplier,
            reviveDelay
        );
    }

    private static readonly float SizeOfTowerInUnityRange =
        ServerUtil.ConvertGameRangeToUnityRange(50f);
    public static float GetDamageMultiplierBasedOnRangeFromSource(
        ServerEntity sourceEntity,
        ServerEntity target,
        float sourceAttackRange,
        float maxDamageMultiplier
    ) {
        float distanceToTarget = Vector3.Distance(
            sourceEntity.transform.position,
            target.transform.position
        );

        sourceAttackRange -= SizeOfTowerInUnityRange;
        distanceToTarget -= SizeOfTowerInUnityRange;
        float multiplierProportion = 1 - distanceToTarget / sourceAttackRange;
        return Mathf.Lerp(1f, maxDamageMultiplier, multiplierProportion);
    }

    public static HashSet<ServerEntity> GetEntitiesPassingFilterWithinGameRangeOfEntity(
        ServerEntity sourceEntity,
        float gameRange,
        EntityFilter filter,
        bool includeSourceEntity = false
    ) {
        HashSet<ServerEntity> qualifyingEntities = new HashSet<ServerEntity>();
        
        Collider[] colliders = Physics.OverlapSphere(
            sourceEntity.transform.position,
            ServerUtil.ConvertGameRangeToUnityRange(gameRange),
            filter.InitialLayerMask
        );

        foreach (Collider collider in colliders) {
            ServerEntity e = collider.GetComponent<ServerEntity>();
            if (e == sourceEntity && !includeSourceEntity) {
                continue;
            }

            if (!filter.PassesFilter(e)) {
                continue;
            }

            qualifyingEntities.Add(e);
        }

        return qualifyingEntities;
    }
    
    public static HashSet<ServerEntity> GetEntitiesPassingFilterWithinGameRangeOfObject(
        GameObject sourceObject,
        float gameRange,
        EntityFilter filter,
        bool includeSourceObject = false
    ) {
        HashSet<ServerEntity> qualifyingEntities = new HashSet<ServerEntity>();
        
        Collider[] colliders = Physics.OverlapSphere(
            sourceObject.transform.position,
            ServerUtil.ConvertGameRangeToUnityRange(gameRange),
            filter.InitialLayerMask
        );

        foreach (Collider collider in colliders) {
            ServerEntity e = collider.GetComponent<ServerEntity>();
            if (e.gameObject == sourceObject && !includeSourceObject) {
                continue;
            }

            if (!filter.PassesFilter(e)) {
                continue;
            }

            qualifyingEntities.Add(e);
        }

        return qualifyingEntities;
    }

    public static ServerEnemy GetRandomCreepWithinGameRangeOfEntity(
        ServerEntity sourceEntity,
        float gameRange
    ) {
        Collider[] colliders = Physics.OverlapSphere(
            sourceEntity.transform.position,
            ServerUtil.ConvertGameRangeToUnityRange(gameRange),
            LayerMaskConstants.EnemyLayerMask
        );

        foreach (Collider collider in colliders) {
            return collider.gameObject.GetComponent<ServerEnemy>();
        }

        throw new NoNearbyCreepsException();
    }
    
    public static ServerTower GetRandomTowerWithinGameRangeOfEntity(
        ServerEntity sourceEntity,
        float gameRange
    ) {
        Collider[] colliders = Physics.OverlapSphere(
            sourceEntity.transform.position,
            ServerUtil.ConvertGameRangeToUnityRange(gameRange),
            LayerMaskConstants.TowerLayerMask
        );

        foreach (Collider collider in colliders) {
            return collider.gameObject.GetComponent<ServerTower>();
        }

        throw new NoNearbyTowersException();
    }
}