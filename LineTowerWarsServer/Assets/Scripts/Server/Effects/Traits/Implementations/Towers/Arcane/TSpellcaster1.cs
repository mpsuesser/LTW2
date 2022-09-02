using System;
using System.Collections.Generic;

public class TSpellcaster1 : Trait {
    public override TraitType Type => TraitType.Spellcaster1;
    
    private System.Random RNG { get; }
    private ServerTower T { get; }

    public TSpellcaster1(ServerEntity entity) : base(entity) {
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
            CastFireboltAtTarget(targets[0]);
        }
        else {
            CastFrostboltAtTarget(targets[0]);
        }
        
        E.Status.DumpAllMana();
    }

    private void CastFireboltAtTarget(ServerEntity target) {
        // TODO: Send message to client about spellcast event
        // TODO: Create projectile instead

        E.DealDamageTo(
            target,
            TraitConstants.Spellcaster1FireboltDamage,
            DamageType.Spell,
            DamageSourceType.SpellcastFirebolt
        );

        BuffFactory.ApplyBuff(
            BuffType.FireboltStun,
            target,
            E
        );
    }

    private void CastFrostboltAtTarget(ServerEntity mainTarget) {
        // TODO: Send message to client about spellcast event

        HashSet<ServerEntity> potentialTargets =
            TraitUtils.GetEntitiesPassingFilterWithinGameRangeOfEntity(
                E,
                T.Threat.GameRange,
                new CreepEntityFilter()
            );

        HashSet<ServerEntity> targets = new HashSet<ServerEntity> {mainTarget};
        foreach (ServerEntity potentialTarget in potentialTargets) {
            if (targets.Count >= TraitConstants.Spellcaster1FrostboltTargetMaximum) {
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
                TraitConstants.Spellcaster1FrostboltDamage,
                DamageType.Spell,
                DamageSourceType.SpellcastFrostbolt
            );

            BuffFactory.ApplyBuff(
                BuffType.FrostboltSlow,
                target,
                E
            );
        }
    }

    public override float ManaRegenPerSecondDiff =>
        TraitConstants.Spellcaster1ManaRegenerationPerSecond;
}