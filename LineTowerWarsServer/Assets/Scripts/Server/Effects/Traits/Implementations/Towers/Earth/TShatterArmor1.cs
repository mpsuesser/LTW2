using System.Collections.Generic;

public class TShatterArmor1 : Trait {
    public override TraitType Type => TraitType.ShatterArmor1;

    public TShatterArmor1(ServerEntity entity) : base(entity) {
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
                BuffType.ShatteredArmor1,
                target,
                attacker
            );
        }
    }
}