using System.Collections.Generic;

public class TPestilence : Trait {
    public override TraitType Type => TraitType.Pestilence;
    
    private Dictionary<
        /* target ent ID */ int,
        /* mana level */ Queue<int>
    > ManaLevelAtTimeOfProjectileFiring { get; }

    public TPestilence(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        ManaLevelAtTimeOfProjectileFiring = new Dictionary<int, Queue<int>>();

        tower.Attack.OnAttackFiredPre += SnapshotManaForDebuffApplications;
        tower.Attack.OnAttackLandedPost += ApplyDebuffToTarget;
    }

    public override float ManaRegenPerSecondDiff => TraitConstants.PestilenceManaRegenPerSecond;
    public override int AdditionalAttackTargets => TraitConstants.PestilenceAdditionalTargets;

    private void SnapshotManaForDebuffApplications(
        ServerEntity attacker,
        List<ServerEntity> targets
    ) {
        int attackerManaLevel = attacker.MP;
        foreach (ServerEntity target in targets) {
            if (!ManaLevelAtTimeOfProjectileFiring.ContainsKey(target.ID)) {
                ManaLevelAtTimeOfProjectileFiring.Add(target.ID, new Queue<int>());
            }
            
            ManaLevelAtTimeOfProjectileFiring[target.ID].Enqueue(attackerManaLevel);
        }

        attacker.Status.LoseMana(attackerManaLevel);
    }

    private void ApplyDebuffToTarget(
        ServerEntity attacker,
        ServerEntity target
    ) {
        int stacksToApply = 1;
        if (ManaLevelAtTimeOfProjectileFiring[target.ID].Count > 0) {
            stacksToApply = ManaLevelAtTimeOfProjectileFiring[target.ID].Dequeue();
        }
        else {
            LTWLogger.Log("Mana level queue count did not line up!");
        }

        BPlague b = (BPlague) BuffFactory.ApplyBuff(
            BuffType.Plague,
            target,
            attacker,
            stacksToApply
        );
        b.MakeSticky();
    }
}