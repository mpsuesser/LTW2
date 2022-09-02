using System.Collections.Generic;

public class TStaticCharge1 : Trait {
    public override TraitType Type => TraitType.StaticCharge1;

    public TStaticCharge1(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackFiredPre += GainManaAndCheckForLightningStrike;
    }

    private void GainManaAndCheckForLightningStrike(
        ServerEntity _attacker,
        List<ServerEntity> _targets
    ) {
        if (E.MP >= TraitConstants.StaticCharge1LightningStrikeManaThreshold) {
            DoLightningStrike();
        }
        
        E.Status.GainMana(TraitConstants.StaticCharge1ManaPerAttack);
    }

    private void DoLightningStrike() {
        HashSet<ServerEntity> flyingCreepsWithinRange =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                E,
                TraitConstants.StaticCharge1LightningStrikeRadius,
                new FlyingCreepEntityFilter()
            );

        if (flyingCreepsWithinRange.Count < 1) {
            return;
        }

        E.DealDamageTo(
            ServerUtil.GetRandomItemFromHashSet(flyingCreepsWithinRange),
            TraitConstants.StaticCharge1LightningStrikeDamage,
            DamageType.Spell,
            DamageSourceType.StaticChargeLightningStrike1
        );
        
        E.Status.DumpAllMana();
    }
    
    public override bool HasPreMultiplierDamageDealtAdjustment => true;
    public override void PreMultiplierDamageDealtAdjustment(
        ServerEntity target,
        ref double damageAmount
    ) {
        damageAmount +=
            TraitConstants.StaticCharge1AdditionalDamageTargetHealthMultiplier
            * target.HP;
    }
}