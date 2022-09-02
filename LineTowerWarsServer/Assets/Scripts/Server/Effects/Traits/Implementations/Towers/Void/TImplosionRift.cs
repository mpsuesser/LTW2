using UnityEngine;

public class TImplosionRift : Trait {
    public override TraitType Type => TraitType.ImplosionRift;

    public TImplosionRift(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPost += ApplyDebuffToTargetAndCheckForEffectTrigger;
    }

    public override float ManaRegenPerSecondDiff => TraitConstants.ImplosionRiftManaRegenPerSecond;

    private static void ApplyDebuffToTargetAndCheckForEffectTrigger(
        ServerEntity attacker,
        ServerEntity target
    ) {
        BuffFactory.ApplyBuff(
            BuffType.TemporalImplosion,
            target,
            attacker
        );
        
        if (attacker.MP != attacker.MaxMana) {
            return;
        }

        BTemporalImplosion.TriggerImplosionEffectAroundEntity(
            target, 
            attacker
        );
        attacker.Status.DumpAllMana();
    }
}