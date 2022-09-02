using System.Collections.Generic;

public class TShatterArmor2 : Trait {
    public override TraitType Type => TraitType.ShatterArmor2;

    public TShatterArmor2(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPostIncludeSplashTargets += ApplyDebuffToTargets;
    }

    private static void ApplyDebuffToTargets(
        ServerEntity attacker,
        List<ServerEntity> targets
    ) {
        foreach (ServerEntity target in targets) {
            BuffFactory.ApplyBuff(
                BuffType.ShatteredArmor2,
                target,
                attacker
            );
        }
    }
}