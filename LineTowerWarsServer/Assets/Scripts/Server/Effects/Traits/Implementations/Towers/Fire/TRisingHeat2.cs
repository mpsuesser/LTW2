using System.Collections.Generic;

public class TRisingHeat2 : Trait {
    public override TraitType Type => TraitType.RisingHeat2;

    public TRisingHeat2(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackFiredPre += AddStackOfRisingHeat;
    }

    private static void AddStackOfRisingHeat(
        ServerEntity attacker,
        List<ServerEntity> _targets
    ) {
        BuffFactory.ApplyBuff(
            BuffType.RisingHeat2,
            attacker,
            attacker
        );
    }
}