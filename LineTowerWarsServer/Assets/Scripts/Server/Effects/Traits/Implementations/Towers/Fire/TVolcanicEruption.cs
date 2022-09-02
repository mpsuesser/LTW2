using System;
using System.Collections.Generic;

public class TVolcanicEruption : Trait {
    public override TraitType Type => TraitType.VolcanicEruption;

    private Random RNG { get; }

    public TVolcanicEruption(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        RNG = new Random();

        tower.Attack.OnAttackFiredPre += CheckForVolcanicEruptionTrigger;
    }

    private void CheckForVolcanicEruptionTrigger(
        ServerEntity attacker,
        List<ServerEntity> targets
    ) {
        foreach (ServerEntity target in targets) {
            if (target.HealthRatio > TraitConstants.VolcanicEruptionGuaranteedProcHealthThreshold) {
                // Remove VolcanicOpportunity from the attacker
                if (
                    attacker.Buffs.TryGetBuffOfType(
                        BuffType.VolcanicOpportunity,
                        out Buff existingVolcanicOpportunityBuff
                    )
                ) {
                    existingVolcanicOpportunityBuff.Purge();
                }
                
                // Roll the dice on volcanic eruption proc
                if (RNG.NextDouble() < TraitConstants.VolcanicEruptionProcChance) {
                    VolcanicEruption(target);
                }
            }
            else {
                // Add VolcanicOpportunity to the attacker
                if (!attacker.Buffs.HasBuffOfType(BuffType.VolcanicOpportunity)) {
                    BuffFactory.ApplyBuff(
                        BuffType.VolcanicOpportunity,
                        attacker,
                        attacker
                    );
                }
                
                VolcanicEruption(target);
            }
        }
    }

    private void VolcanicEruption(ServerEntity target) {
        double damage = (E as ServerTower).Attack.GetAttackDamage();
        E.DealDamageTo(
            target,
            damage * TraitConstants.VolcanicEruptionDamageMultiplier,
            DamageType.Spell,
            DamageSourceType.VolcanicEruption
        );
        
        BuffFactory.ApplyBuff(
            BuffType.VolcanicExposure,
            target,
            E
        );
    }
}