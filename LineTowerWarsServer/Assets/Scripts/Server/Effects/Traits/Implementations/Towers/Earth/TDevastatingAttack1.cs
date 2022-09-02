using System.Collections.Generic;

public class TDevastatingAttack1 : Trait {
    public override TraitType Type => TraitType.DevastatingAttack1;

    public TDevastatingAttack1(ServerEntity entity) : base(entity) {
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
                BuffType.Devastation1,
                target,
                attacker
            );
        }
    }
}