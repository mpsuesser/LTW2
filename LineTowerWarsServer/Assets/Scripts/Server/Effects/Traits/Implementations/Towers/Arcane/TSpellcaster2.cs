using System;
using System.Collections.Generic;

public class TSpellcaster2 : Trait {
    public override TraitType Type => TraitType.Spellcaster2;
    
    private System.Random RNG { get; }
    private ServerTower T { get; }

    public TSpellcaster2(ServerEntity entity) : base(entity) {
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

        // coin flip
        if (RNG.Next(2) == 1) {
            CastFireblastAtTarget(targets[0]);
        }
        else {
            CastIceblastAtTarget(targets[0]);
        }
        
        E.Status.DumpAllMana();
    }

    private void CastFireblastAtTarget(ServerEntity target) {
        // TODO: Send message to client about spellcast event
        // TODO: Create projectile instead

        E.DealDamageTo(
            target,
            TraitConstants.Spellcaster2FireblastDamage,
            DamageType.Spell,
            DamageSourceType.SpellcastFireblast
        );

        BuffFactory.ApplyBuff(
            BuffType.FireblastStun,
            target,
            E
        );
    }

    private void CastIceblastAtTarget(ServerEntity mainTarget) {
        // TODO: Send message to client about spellcast event

        HashSet<ServerEntity> potentialTargets =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                E,
                T.Threat.GameRange,
                new CreepEntityFilter()
            );

        HashSet<ServerEntity> targets = new HashSet<ServerEntity> {mainTarget};
        foreach (ServerEntity potentialTarget in potentialTargets) {
            if (targets.Count >= TraitConstants.Spellcaster2IceblastTargetMaximum) {
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
                TraitConstants.Spellcaster2IceblastDamage,
                DamageType.Spell,
                DamageSourceType.SpellcastIceblast
            );

            BuffFactory.ApplyBuff(
                BuffType.IceblastSlow,
                target,
                E
            );
        }
    }

    public override float ManaRegenPerSecondDiff =>
        TraitConstants.Spellcaster2ManaRegenerationPerSecond;
}