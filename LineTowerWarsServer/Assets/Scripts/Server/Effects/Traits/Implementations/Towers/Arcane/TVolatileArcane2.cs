using System.Collections.Generic;
using DataStructures.PriorityQueue;
using UnityEngine;

public class TVolatileArcane2 : Trait {
    public override TraitType Type => TraitType.VolatileArcane2;

    public TVolatileArcane2(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPost += CheckForBounces;
    }

    private static void CheckForBounces(
        ServerEntity attacker,
        ServerEntity target
    ) {
        if (!(attacker is ServerTower towerAttacker)) {
            return;
        }
        
        int manaGained = 1; // 1 per target hit
        
        HashSet<ServerEntity> enemiesInRangeForBounce =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                target,
                TraitConstants.VolatileArcane2BounceRange,
                new CreepEntityFilter(),
                false
            );

        PriorityQueue<ServerEntity, float> distanceHeap = new PriorityQueue<ServerEntity, float>(0f);
        foreach (ServerEntity entity in enemiesInRangeForBounce) {
            distanceHeap.Insert(
                entity,
                Vector3.Distance(
                    target.transform.position,
                    entity.transform.position
                )
            );
        }

        HashSet<ServerEntity> bounceTargets = new HashSet<ServerEntity>();
        while (
            !distanceHeap.IsEmpty()
            && bounceTargets.Count < TraitConstants.VolatileArcane2MaxBounces
        ) {
            bounceTargets.Add(distanceHeap.Pop());
        }

        double damage = towerAttacker.Attack.GetAttackDamage();
        foreach (ServerEntity bounceTarget in bounceTargets) {
            attacker.DealDamageTo(
                bounceTarget,
                damage * TraitConstants.VolatileArcane2SpellDamagePercentage,
                DamageType.Spell,
                DamageSourceType.VolatileArcane2Bounce
            );
            attacker.DealDamageTo(
                bounceTarget,
                damage * (1 - TraitConstants.VolatileArcane2SpellDamagePercentage),
                DamageType.Spell,
                DamageSourceType.VolatileArcane2Bounce
            );

            manaGained++;
        }
        
        attacker.Status.GainMana(manaGained);
    }

    public override float ManaRegenPerSecondDiff =>
        -TraitConstants.VolatileArcane2ManaLossPerSecond;

    public override float AttackSpeedMultiplier =>
        1 - TraitConstants.VolatileArcane2AttackSpeedDropoff * E.MP / E.MaxMana * 100;
}