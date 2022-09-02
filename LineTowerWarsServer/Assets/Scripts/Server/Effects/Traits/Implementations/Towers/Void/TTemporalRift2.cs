using UnityEngine;

public class TTemporalRift2 : Trait {
    public override TraitType Type => TraitType.TemporalRift2;

    public TTemporalRift2(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPost += CheckApplyDebuffToTarget;
    }

    public override float ManaRegenPerSecondDiff => TraitConstants.TemporalRift2ManaRegenPerSecond;

    private static void CheckApplyDebuffToTarget(
        ServerEntity attacker,
        ServerEntity target
    ) {
        if (attacker.MP != attacker.MaxMana) {
            return;
        }

        if (Time.time - TemporalRiftHistory.GetTimeOfLastApplicationForEntity(target) <
            TraitConstants.TemporalShift2PerUnitCooldownPeriod) {
            return;
        }

        BuffFactory.ApplyBuff(
            BuffType.TemporalShift2,
            target,
            attacker
        );
        TemporalRiftHistory.RegisterApplicationToEntity(target);
        attacker.Status.DumpAllMana();
    }
}