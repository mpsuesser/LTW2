using System;
using System.Collections.Generic;
using UnityEngine;

public class TLightBurst2 : Trait {
    public override TraitType Type => TraitType.LightBurst2;

    public TLightBurst2(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPostIncludeSplashTargets += Pushback;
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
            manaToConsume * TraitConstants.LightBurst2DistancePerManaConsumed;
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

    public override float ManaRegenPerSecondDiff =>
        TraitConstants.LightBurst2ManaRegenPerSecond;
}