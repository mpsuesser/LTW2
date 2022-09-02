using System.Collections.Generic;

public class TCrushingWave2 : Trait {
    public override TraitType Type => TraitType.CrushingWave2;

    private int AttackCounter { get; set; }
    
    public TCrushingWave2(ServerEntity entity) : base(entity) {
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
        if (++AttackCounter < TraitConstants.CrushingWave2AttacksToTriggerWave) {
            return;
        }

        foreach (ServerEntity target in targets) {
            CrushingWave.Create(
                E,
                target,
                TraitConstants.CrushingWave2Damage,
                DamageSourceType.CrushingWave2
            );
        }
        
        AttackCounter = 0;
    }
}