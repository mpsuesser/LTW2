using UnityEngine;

public class TTemporalRift1 : Trait {
    public override TraitType Type => TraitType.TemporalRift1;

    public TTemporalRift1(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPost += CheckApplyDebuffToTarget;
    }

    public override float ManaRegenPerSecondDiff => TraitConstants.TemporalRift1ManaRegenPerSecond;

    private static void CheckApplyDebuffToTarget(
        ServerEntity attacker,
        ServerEntity target
    ) {
        if (attacker.MP != attacker.MaxMana) {
            return;
        }

        if (Time.time - TemporalRiftHistory.GetTimeOfLastApplicationForEntity(target) <
            TraitConstants.TemporalShift1PerUnitCooldownPeriod) {
            return;
        }

        BuffFactory.ApplyBuff(
            BuffType.TemporalShift1,
            target,
            attacker
        );
        TemporalRiftHistory.RegisterApplicationToEntity(target);
        attacker.Status.DumpAllMana();
    }
}