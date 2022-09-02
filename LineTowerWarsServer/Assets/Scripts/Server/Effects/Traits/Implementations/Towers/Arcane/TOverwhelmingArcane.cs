using System.Collections.Generic;
using DataStructures.PriorityQueue;
using UnityEngine;

public class TOverwhelmingArcane : Trait {
    public override TraitType Type => TraitType.OverwhelmingArcane;

    public TOverwhelmingArcane(ServerEntity entity) : base(entity) {
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
        
        float manaGained = TraitConstants.OverwhelmingArcaneManaPerTargetHit;
        
        HashSet<ServerEntity> enemiesInRangeForBounce =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                target,
                TraitConstants.OverwhelmingArcaneBounceRange,
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
            && bounceTargets.Count < TraitConstants.OverwhelmingArcaneMaxBounces
        ) {
            bounceTargets.Add(distanceHeap.Pop());
        }

        double damage = towerAttacker.Attack.GetAttackDamage();
        foreach (ServerEntity bounceTarget in bounceTargets) {
            attacker.DealDamageTo(
                bounceTarget,
                damage,
                DamageType.Physical,
                DamageSourceType.OverwhelmingArcaneBounce
            );

            manaGained += TraitConstants.OverwhelmingArcaneManaPerTargetHit;

            if (bounceTarget.HealthRatio > TraitConstants.OverwhelmingArcaneAdditionalSpellDamageHealthMinimum) {
                attacker.DealDamageTo(
                    bounceTarget,
                    damage * TraitConstants.OverwhelmingArcaneAdditionalSpellDamagePercentage,
                    DamageType.Spell,
                    DamageSourceType.OverwhelmingArcaneBounce
                );

                manaGained += TraitConstants.OverwhelmingArcaneHighHealthAdditionalManaGain;
            }
        }
        
        attacker.Status.GainMana(manaGained);
    }

    public override float ManaRegenPerSecondDiff =>
        -TraitConstants.OverwhelmingArcaneManaLossPerSecond;

    public override float AttackSpeedMultiplier =>
        1 - TraitConstants.OverwhelmingArcaneDamageModifierPerMana * E.MP / E.MaxMana * 100;
}