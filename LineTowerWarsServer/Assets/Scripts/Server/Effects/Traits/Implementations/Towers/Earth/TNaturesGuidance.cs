using System.Collections.Generic;

public class TNaturesGuidance : Trait {
    public override TraitType Type => TraitType.NaturesGuidance;

    public TNaturesGuidance(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPostIncludeSplashTargets += ApplyDebuffToTargets;
        tower.Attack.OnAttackLandedPostAggregateDamage += HealBasedOnDamageDealt;
    }

    private static void ApplyDebuffToTargets(
        ServerEntity attacker,
        List<ServerEntity> targets
    ) {
        foreach (ServerEntity target in targets) {
            BuffFactory.ApplyBuff(
                BuffType.NaturesForce,
                target,
                attacker
            );
        }
    }

    private static void HealBasedOnDamageDealt(
        ServerEntity attacker,
        double damageDealt
    ) {
        attacker.DoHealingTo(
            attacker,
            damageDealt * TraitConstants.NaturesGuidanceSelfHealByDamageDealtMultiplier
        );
    }
}