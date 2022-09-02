using System;
using System.Collections.Generic;

public class TKirinTorMastery : Trait {
    public override TraitType Type => TraitType.KirinTorMastery;
    
    private System.Random RNG { get; }
    private ServerTower T { get; }

    public TKirinTorMastery(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        T = tower;
        RNG = new Random();

        tower.Attack.OnAttackFiredPre += CheckForSpellcastCondition;
    }

    private void CheckForSpellcastCondition(
        ServerEntity attacker,
        List<ServerEntity> targets
    ) {
        if (E.MP < E.MaxMana || targets.Count < 1) {
            return;
        }

        switch (RNG.Next(3)) {
            case 0:
                CastPyroblastAtTarget(targets[0]);
                break;
            case 1:
                CastChainsOfIceAtTarget(targets[0]);
                break;
            case 2:
                CastArcaneExplosion();
                break;
        }
        
        E.Status.DumpAllMana();
    }

    private void CastPyroblastAtTarget(ServerEntity target) {
        // TODO: Send message to client about spellcast event
        // TODO: Create projectile instead

        E.DealDamageTo(
            target,
            TraitConstants.KirinTorMasteryPyroblastDamage,
            DamageType.Spell,
            DamageSourceType.SpellcastPyroblast
        );

        BuffFactory.ApplyBuff(
            BuffType.PyroblastStun,
            target,
            E
        );
    }

    private void CastChainsOfIceAtTarget(ServerEntity mainTarget) {
        // TODO: Send message to client about spellcast event

        HashSet<ServerEntity> potentialTargets =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                E,
                T.Threat.GameRange,
                new CreepEntityFilter()
            );

        HashSet<ServerEntity> targets = new HashSet<ServerEntity> {mainTarget};
        foreach (ServerEntity potentialTarget in potentialTargets) {
            if (targets.Count >= TraitConstants.KirinTorMasteryChainsOfIceTargetMaximum) {
                break;
            }
            
            if (potentialTarget == mainTarget) {
                continue;
            }

            targets.Add(potentialTarget);
        }

        foreach (ServerEntity target in targets) {
            // TODO: Create projectile instead
            
            E.DealDamageTo(
                target,
                TraitConstants.KirinTorMasteryChainsOfIceDamage,
                DamageType.Spell,
                DamageSourceType.SpellcastChainsOfIce
            );

            BuffFactory.ApplyBuff(
                BuffType.ChainsOfIceSlow,
                target,
                E
            );
        }
    }

    private void CastArcaneExplosion() {
        // TODO: Send spellcast event to client
        
        HashSet<ServerEntity> creepsInRange =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                E,
                TraitConstants.KirinTorMasteryArcaneExplosionRadius,
                new CreepEntityFilter()
            );

        foreach (ServerEntity creep in creepsInRange) {
            E.DealDamageTo(
                creep,
                TraitConstants.KirinTorMasteryArcaneExplosionDamage,
                DamageType.Spell,
                DamageSourceType.SpellcastArcaneExplosion
            );

            BuffFactory.ApplyBuff(
                BuffType.ArcaneExposure,
                creep,
                E
            );
        }
    }

    public override float ManaRegenPerSecondDiff =>
        TraitConstants.Spellcaster2ManaRegenerationPerSecond;
}