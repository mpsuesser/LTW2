using System.Collections.Generic;
using UnityEngine;

public class THellfire : Trait {
    public override TraitType Type => TraitType.Hellfire;

    public THellfire(ServerEntity entity) : base(entity) { }

    public override float ManaRegenPerSecondDiff =>
        -TraitConstants.HellfireManaLossPerSecond;

    public override bool AttacksIgnoreArmorValue => true;

    public override bool HasCustomHandleAttackLandedImplementation => true;
    public override void CustomHandleAttackLandedImplementation(
        ServerEntity mainTarget,
        AttackEventData eventData,
        ref double totalDamageAccumulator,
        ref List<ServerEntity> allTargetsAccumulator
    ) {
        E.Status.GainMana(
            TraitConstants.HellfireManaPerAttack
        );

        float baseGameSplashRadius = (E as ServerTower).Attack.SplashDamageGameRadius;
        float adjustedGameSplashRadius = baseGameSplashRadius + E.MP;

        HashSet<ServerEntity> splashTargets =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                mainTarget,
                adjustedGameSplashRadius,
                E.Effects.AggregateAttackTargetEligibilityFilter,
                true
            );

        float instantKillHealthRatioThreshold = Mathf.Lerp(
            TraitConstants.HellfireInstantKillCurrentHealthMinThreshold,
            TraitConstants.HellfireInstantKillCurrentHealthMaxThreshold,
            (float) E.ManaRatio
        );
        
        foreach (ServerEntity target in splashTargets) {
            allTargetsAccumulator.Add(target);

            if (target.HealthRatio < instantKillHealthRatioThreshold) {
                totalDamageAccumulator += E.DealDamageTo(
                    target,
                    target.HP + 1,
                    DamageType.Pure,
                    DamageSourceType.HellfireInstantKill
                );
            }
            else {
                totalDamageAccumulator += E.DealDamageTo(
                    target,
                    eventData.InitialSnapshotDamage,
                    eventData.DmgType,
                    target == mainTarget
                        ? DamageSourceType.AutoAttack
                        : DamageSourceType.AutoAttackSplash
                );
            }
        }
    }
}