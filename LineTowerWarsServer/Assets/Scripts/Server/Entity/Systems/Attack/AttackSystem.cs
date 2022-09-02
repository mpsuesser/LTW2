using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class AttackSystem : IEntitySystem {
    public event Action<ServerEntity, List<ServerEntity>> OnAttackFiredPre;
    public event Action<ServerEntity, ServerEntity> OnAttackLandedPost;
    public event Action<ServerEntity, List<ServerEntity>> OnAttackLandedPostIncludeSplashTargets;
    public event Action<ServerEntity, double> OnAttackLandedPostAggregateDamage;

    private ServerEntity E { get; set; }

    private EntityAttackScheme AttackScheme { get; set; }
    public double GameRange => AttackScheme.Range;
    public double UnityRange => ServerUtil.ConvertGameRangeToUnityRange((float)GameRange);

    public AttackType AtkType => AttackScheme.Modifier;

    private double ActiveCooldown { get; set; }
    private bool IsOnCooldown => ActiveCooldown > 0f;
    
    private bool DoesSplashDamage { get; }
    public float SplashDamageGameRadius { get; }

    public AttackSystem(
        ServerEntity entity,
        EntityAttackScheme attackScheme,
        bool doesSplashDamage,
        float splashDamageGameRadius = 0f
    ) {
        E = entity;
       
        AttackScheme = attackScheme;
        DoesSplashDamage = doesSplashDamage;
        SplashDamageGameRadius = splashDamageGameRadius;
    }

    public void Update(ServerEntity target) {
        if (IsOnCooldown) {
            ActiveCooldown -= Time.deltaTime;
        } else {
            if (target != null) {
                InitiateAttack(target);
            }
        }
    }

    private void InitiateAttack(ServerEntity target) {
        SetCooldown();

        double snapshotDamage = GetAttackDamage();
        AttackEventData eventData;

        if (E.Effects.AggregateAdditionalAttackTargets > 0) {
            List<ServerEntity> allTargets = GetAdditionalAttackTargets(
                target,
                E.Effects.AggregateAdditionalAttackTargets
            );
            allTargets.Insert(0, target);
            eventData = CreateEventDataForMultipleTargets(allTargets, snapshotDamage);
        }
        else {
            eventData = CreateEventData(target, snapshotDamage);
        }

        // TODO: Get rid of coroutine, handle this in Update() above instead
        E.StartCoroutine(Attack(eventData, target));
        SendEventData(eventData);
    }

    private List<ServerEntity> GetAdditionalAttackTargets(
        ServerEntity mainTarget,
        int desiredAdditionalTargetCount
    ) {
        List<ServerEntity> additionalTargets = new List<ServerEntity>();

        HashSet<ServerEntity> potentialTargets =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                E,
                (float) AttackScheme.Range,
                E.Effects.AggregateAttackTargetEligibilityFilter,
                false
            );

        foreach (ServerEntity potentialTarget in potentialTargets) {
            if (
                potentialTarget == mainTarget
                || !potentialTarget.IsAlive
            ) {
                continue;
            }
            
            additionalTargets.Add(potentialTarget);
            if (additionalTargets.Count == desiredAdditionalTargetCount) {
                break;
            }
        }
        
        return additionalTargets;
    }

    private IEnumerator Attack(AttackEventData eventData, ServerEntity mainTarget) {
        if (AttackScheme.Delivery.InitialAnimationDelay > 0) {
            yield return new WaitForSeconds((float)AttackScheme.Delivery.InitialAnimationDelay);

            if (E == null || mainTarget == null) {
                yield break;
            }
        }

        List<ServerEntity> targets = GetTargetsFromEventData(eventData);

        OnAttackFiredPre?.Invoke(E, targets);

        switch (AttackScheme.Delivery.Type) {
            case AttackDeliveryType.ProjectileFromSource:
            case AttackDeliveryType.ProjectileFromAboveTarget:
                CreateProjectile(eventData);
                break;

            case AttackDeliveryType.Instant:
            default:
                HandleAttackLanded(eventData, mainTarget.gameObject);
                break;
        }
    }

    private void CreateProjectile(AttackEventData eventData) {
        ProjectileAttackDelivery projectileDelivery = AttackScheme.Delivery as ProjectileAttackDelivery;

        List<ServerEntity> targets = GetTargetsFromEventData(eventData);
        foreach (ServerEntity target in targets) {
            Vector3 projectileStartLocation = E.transform.position;
            projectileStartLocation += projectileDelivery.ProjectileInitialOffset;

            ProjectileBehaviour projectile = GameObject.Instantiate(ServerPrefabs.Singleton.pfArrowProjectile, projectileStartLocation, Quaternion.identity);
            projectile.Load(projectileDelivery, target.gameObject, eventData);
            projectile.OnLanded += HandleAttackLanded;
        }
    }

    private void HandleAttackLanded(AttackEventData eventData, GameObject landedOnTarget) {
        if (E == null || landedOnTarget == null) {
            return;
        }

        ServerEntity target = landedOnTarget.GetComponent<ServerEntity>();
        if (target == null) {
            LTWLogger.Log("Target GameObject was not null but ServerEntity component was!");
            return;
        }

        double totalDamageDealt = 0;
        List<ServerEntity> allTargets = new List<ServerEntity>();

        if (E.Effects.AggregateHasCustomHandleAttackLandedImplementation) {
            E.Effects.CustomHandleAttackLandedImplementation(
                target,
                eventData,
                ref totalDamageDealt,
                ref allTargets
            );
        }
        else {
            HandleAttackLandedImpl(
                target,
                eventData,
                ref totalDamageDealt,
                ref allTargets
            );
        }

        OnAttackLandedPost?.Invoke(E, target);
        OnAttackLandedPostIncludeSplashTargets?.Invoke(E, allTargets);
        OnAttackLandedPostAggregateDamage?.Invoke(E, totalDamageDealt);
    }

    protected virtual void HandleAttackLandedImpl(
        ServerEntity target,
        AttackEventData eventData,
        ref double damageAccumulator,
        ref List<ServerEntity> allTargetsAccumulator
    ) {
        allTargetsAccumulator.Add(target);
        
        damageAccumulator += E.DealDamageTo(
            target,
            eventData.InitialSnapshotDamage,
            eventData.DmgType,
            DamageSourceType.AutoAttack
        );
        
        if (DoesSplashDamage) {
            HashSet<ServerEntity> splashTargets =
                TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                    target,
                    SplashDamageGameRadius,
                    E.Effects.AggregateAttackTargetEligibilityFilter,
                    false
                );
            foreach (ServerEntity splashTarget in splashTargets) {
                if (!(splashTarget is ServerEnemy e) || !e.IsAlive) {
                    continue;
                }
                
                damageAccumulator += E.DealDamageTo(
                    e,
                    eventData.InitialSnapshotDamage,
                    eventData.DmgType,
                    DamageSourceType.AutoAttackSplash
                );

                allTargetsAccumulator.Add(splashTarget);
            }
        }
    }

    public double GetAttackDamage() {
        (double damageMin, double damageMax) = AttackScheme.DamageRange;
        double damage = ServerUtil.RNG.NextDouble() * (damageMax - damageMin) + damageMin;
        return damage;
    }

    private void SetCooldown() {
        ActiveCooldown = AttackScheme.Cooldown / E.Effects.AggregateAttackSpeedMultiplier;
    }

    private static AttackEventData CreateEventData(ServerEntity target, double snapshotDamage) {
        List<int> targetEntityIDs = new List<int> {target.ID};
        return new AttackEventData(
            targetEntityIDs,
            snapshotDamage,
            DamageType.Physical
        );
    }

    private static AttackEventData CreateEventDataForMultipleTargets(
        List<ServerEntity> targetEntities,
        double snapshotDamage
    ) {
        List<int> targetEntityIDs = targetEntities.ConvertAll(entity => entity.ID);
        return new AttackEventData(
            targetEntityIDs,
            snapshotDamage,
            DamageType.Physical
        );
    }

    private void SendEventData(AttackEventData eventData) {
        ServerSend.EntityAttacked(E, eventData);
    }

    private static List<ServerEntity> GetTargetsFromEventData(AttackEventData eventData) {
        List<ServerEntity> targets = new List<ServerEntity>();
        foreach (int targetEntityID in eventData.TargetEntityIDs) {
            try {
                ServerEntity target = ServerEntitySystem.Singleton.GetEntityByID(targetEntityID);
                targets.Add(target);
            } catch (NotFoundException) { }
        }

        return targets;
    }
}
