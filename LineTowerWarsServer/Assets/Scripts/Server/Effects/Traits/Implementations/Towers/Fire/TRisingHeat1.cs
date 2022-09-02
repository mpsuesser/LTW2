using System.Collections.Generic;

public class TRisingHeat1 : Trait {
    public override TraitType Type => TraitType.RisingHeat1;

    public TRisingHeat1(ServerEntity entity) : base(entity) {
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
            BuffType.RisingHeat1,
            attacker,
            attacker
        );
    }
}