using System.Collections.Generic;

public class TDevastatingAttack2 : Trait {
    public override TraitType Type => TraitType.DevastatingAttack2;

    public TDevastatingAttack2(ServerEntity entity) : base(entity) {
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
                BuffType.Devastation2,
                target,
                attacker
            );
        }
    }
}