using System.Collections.Generic;

public class TRapidInfection1 : Trait {
    public override TraitType Type => TraitType.RapidInfection1;
    
    private Dictionary<
        /* target ent ID */ int,
        /* mana level */ Queue<int>
    > ManaLevelAtTimeOfProjectileFiring { get; }

    public TRapidInfection1(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        ManaLevelAtTimeOfProjectileFiring = new Dictionary<int, Queue<int>>();

        tower.Attack.OnAttackFiredPre += SnapshotManaForDebuffApplications;
        tower.Attack.OnAttackLandedPost += ApplyDebuffToTarget;
    }

    public override float ManaRegenPerSecondDiff => TraitConstants.RapidInfection1ManaRegenPerSecond;
    public override int AdditionalAttackTargets => TraitConstants.RapidInfection1AdditionalTargets;

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

        BuffFactory.ApplyBuff(
            BuffType.Plague,
            target,
            attacker,
            stacksToApply
        );
    }
}