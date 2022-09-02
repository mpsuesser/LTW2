public class TFrostAttack2 : Trait {
    public override TraitType Type => TraitType.FrostAttack2;

    public TFrostAttack2(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPost += ApplyDebuffToTarget;
    }

    private static void ApplyDebuffToTarget(ServerEntity attacker, ServerEntity target) {
        BuffFactory.ApplyBuff(
            BuffType.FrostAttackSlow2,
            target,
            attacker
        );
    }
}