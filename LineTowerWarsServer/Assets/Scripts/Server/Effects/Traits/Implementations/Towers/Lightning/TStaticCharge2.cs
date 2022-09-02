using System.Collections.Generic;

public class TStaticCharge2 : Trait {
    public override TraitType Type => TraitType.StaticCharge2;

    public TStaticCharge2(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackFiredPre += GainManaAndCheckForLightningStrike;
    }

    private void GainManaAndCheckForLightningStrike(
        ServerEntity _attacker,
        List<ServerEntity> _targets
    ) {
        if (E.MP >= TraitConstants.StaticCharge2LightningStrikeManaThreshold) {
            DoLightningStrike();
        }
        
        E.Status.GainMana(TraitConstants.StaticCharge2ManaPerAttack);
    }

    private void DoLightningStrike() {
        HashSet<ServerEntity> flyingCreepsWithinRange =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                E,
                TraitConstants.StaticCharge2LightningStrikeRadius,
                new FlyingCreepEntityFilter()
            );

        if (flyingCreepsWithinRange.Count < 1) {
            return;
        }

        E.DealDamageTo(
            ServerUtil.GetRandomItemFromHashSet(flyingCreepsWithinRange),
            TraitConstants.StaticCharge2LightningStrikeDamage,
            DamageType.Spell,
            DamageSourceType.StaticChargeLightningStrike2
        );
        
        E.Status.DumpAllMana();
    }
    
    public override bool HasPreMultiplierDamageDealtAdjustment => true;
    public override void PreMultiplierDamageDealtAdjustment(
        ServerEntity target,
        ref double damageAmount
    ) {
        damageAmount +=
            TraitConstants.StaticCharge2AdditionalDamageTargetHealthMultiplier
            * target.HP;
    }
}