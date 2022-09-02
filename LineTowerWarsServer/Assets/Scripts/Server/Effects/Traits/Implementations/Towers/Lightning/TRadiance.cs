using System.Collections.Generic;
using UnityEngine;

public class TRadiance : Trait {
    public override TraitType Type => TraitType.Radiance;
    
    private System.Random RNG { get; }

    public TRadiance(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        RNG = new System.Random();

        tower.Attack.OnAttackFiredPre += GainManaAndCheckForAbility;
    }

    private void GainManaAndCheckForAbility(
        ServerEntity _attacker,
        List<ServerEntity> _targets
    ) {
        if (E.MP >= TraitConstants.RadianceMinManaForAbilityThreshold) {
            DoStrikeOrAOE();
        }
        
        E.Status.GainMana(TraitConstants.RadianceManaPerAttack);
    }

    private void DoStrikeOrAOE() {
        HashSet<ServerEntity> flyingCreepsWithinRange =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                E,
                TraitConstants.RadianceLightningStrikeRadius,
                new FlyingCreepEntityFilter()
            );
        HashSet<ServerEntity> groundCreepsWithinRange =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                E,
                TraitConstants.RadianceAbilityRadius,
                new GroundCreepEntityFilter()
            );

        if (flyingCreepsWithinRange.Count > 0 && groundCreepsWithinRange.Count > 0) {
            // coin flip
            if (RNG.Next(2) == 1) {
                DoLightningStrikeTo(
                    ServerUtil.GetRandomItemFromHashSet(
                        flyingCreepsWithinRange
                    )
                );
            }
            else {
                DoRadianceAbilityTo(groundCreepsWithinRange);
            }
        } else if (flyingCreepsWithinRange.Count > 0) {
            DoLightningStrikeTo(
                ServerUtil.GetRandomItemFromHashSet(
                    flyingCreepsWithinRange
                )
            );
        } else if (groundCreepsWithinRange.Count > 0) {
            DoRadianceAbilityTo(groundCreepsWithinRange);
        }
        // else: don't do anything and keep the mana
    }

    private void DoLightningStrikeTo(ServerEntity flyingCreep) {
        E.DealDamageTo(
            flyingCreep,
            TraitConstants.RadianceLightningStrikeDamage,
            DamageType.Spell,
            DamageSourceType.RadianceLightningStrike
        );
        E.Status.DumpAllMana();
    }

    private void DoRadianceAbilityTo(HashSet<ServerEntity> groundCreeps) {
        foreach (ServerEntity groundCreep in groundCreeps) {
            E.DealDamageTo(
                groundCreep,
                TraitConstants.RadianceAbilityCurrentHealthReduction * groundCreep.HP,
                DamageType.Spell,
                DamageSourceType.RadianceAbility
            );
        }
        E.Status.DumpAllMana();
    }
    
    public override bool HasPreMultiplierDamageDealtAdjustment => true;
    public override void PreMultiplierDamageDealtAdjustment(
        ServerEntity target,
        ref double damageAmount
    ) {
        damageAmount +=
            TraitConstants.RadianceAdditionalDamageTargetHealthMultiplier
            * target.HP;
    }
}