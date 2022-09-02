public class TFrostBlast2 : Trait {
    public override TraitType Type => TraitType.FrostBlast2;

    public TFrostBlast2(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPost += ApplyDebuffToTarget;
    }

    private static void ApplyDebuffToTarget(ServerEntity attacker, ServerEntity target) {
        BuffFactory.ApplyBuff(
            BuffType.FrostBlastSlow2,
            target,
            attacker
        );
    }
}