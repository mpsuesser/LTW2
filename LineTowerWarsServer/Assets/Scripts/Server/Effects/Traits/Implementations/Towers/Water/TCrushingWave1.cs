using System.Collections.Generic;

public class TCrushingWave1 : Trait {
    public override TraitType Type => TraitType.CrushingWave1;

    private int AttackCounter { get; set; }
    
    public TCrushingWave1(ServerEntity entity) : base(entity) {
        AttackCounter = 0;

        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackFiredPre += CheckForWaveCondition;
    }

    private void CheckForWaveCondition(
        ServerEntity attacker,
        List<ServerEntity> targets
    ) {
        if (++AttackCounter < TraitConstants.CrushingWave1AttacksToTriggerWave) {
            return;
        }

        foreach (ServerEntity target in targets) {
            CrushingWave.Create(
                E,
                target,
                TraitConstants.CrushingWave1Damage,
                DamageSourceType.CrushingWave1
            );
        }
        
        AttackCounter = 0;
    }
}