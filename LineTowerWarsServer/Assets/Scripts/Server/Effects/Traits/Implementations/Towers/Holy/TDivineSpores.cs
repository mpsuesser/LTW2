using System;
using System.Collections.Generic;
using UnityEngine;

public class TDivineSpores : Trait {
    public override TraitType Type => TraitType.DivineSpores;

    public TDivineSpores(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPostIncludeSplashTargets += Pushback;
        tower.Attack.OnAttackLandedPostAggregateDamage += HealSurroundingTowers;
    }

    private void Pushback(ServerEntity attacker, List<ServerEntity> targets) {
        if (E.MP < 1) {
            return;
        }
        
        HashSet<ServerEnemy> targetsToPushback = new HashSet<ServerEnemy>();
        foreach (ServerEntity target in targets) {
            if (!(target is ServerEnemy e)) {
                continue;
            }

            if (
                !e.IsAlive
                || !e.AssociatedTraitTypes.Contains(TraitType.Flying)
                || e.AssociatedTraitTypes.Contains(TraitType.Boss)
            ) {
                continue;
            }

            targetsToPushback.Add(e);
        }

        int manaToConsume = Math.Min(E.MP, targetsToPushback.Count);
        float gameDistanceToPushback =
            manaToConsume * TraitConstants.DivineSporesDistancePerManaConsumed;
        float unityDistanceToPushback =
            ServerUtil.ConvertGameRangeToUnityRange(gameDistanceToPushback);
        Vector3 pushbackVector = new Vector3(
            0,
            0,
            unityDistanceToPushback
        );
        foreach (ServerEnemy targetToPushback in targetsToPushback) {
            targetToPushback.Navigation.UpdatePositionTo(
                targetToPushback.transform.position + pushbackVector
            );
        }
        
        E.Status.LoseMana(manaToConsume);
    }

    private void HealSurroundingTowers(ServerEntity attacker, double damageDealt) {
        HashSet<ServerEntity> towersToHeal = 
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                attacker,
                TraitConstants.DivineSporesHealRadius,
                new TowerEntityFilter(),
                true
            );

        foreach (ServerEntity tower in towersToHeal) {
            E.DoHealingTo(
                tower,
                damageDealt * TraitConstants.DivineSporesHealAmountByDamageDealtMultiplier
            );
        }
    }

    public override float ManaRegenPerSecondDiff =>
        TraitConstants.DivineSporesManaRegenPerSecond;
}