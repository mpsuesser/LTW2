using System.Collections.Generic;

public class TUnholyMiasma2 : Trait {
    public override TraitType Type => TraitType.UnholyMiasma2;

    public TUnholyMiasma2(ServerEntity entity) : base(entity) { }

    public override float ManaRegenPerSecondDiff =>
        -TraitConstants.UnholyMiasma2ManaLossPerSecond;

    public override bool AttacksIgnoreArmorValue => true;

    public override bool HasCustomHandleAttackLandedImplementation => true;
    public override void CustomHandleAttackLandedImplementation(
        ServerEntity mainTarget,
        AttackEventData eventData,
        ref double totalDamageAccumulator,
        ref List<ServerEntity> allTargetsAccumulator
    ) {
        E.Status.GainMana(
            TraitConstants.UnholyMiasma2ManaPerAttack
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

        foreach (ServerEntity target in splashTargets) {
            allTargetsAccumulator.Add(target);
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