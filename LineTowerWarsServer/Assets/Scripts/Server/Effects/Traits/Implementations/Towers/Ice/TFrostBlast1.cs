public class TFrostBlast1 : Trait {
    public override TraitType Type => TraitType.FrostBlast1;

    public TFrostBlast1(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPost += ApplyDebuffToTarget;
    }

    private static void ApplyDebuffToTarget(ServerEntity attacker, ServerEntity target) {
        BuffFactory.ApplyBuff(
            BuffType.FrostBlastSlow1,
            target,
            attacker
        );
    }
}