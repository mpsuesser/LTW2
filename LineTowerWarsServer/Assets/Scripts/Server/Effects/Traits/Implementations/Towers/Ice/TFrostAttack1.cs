public class TFrostAttack1 : Trait {
    public override TraitType Type => TraitType.FrostAttack1;

    public TFrostAttack1(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPost += ApplyDebuffToTarget;
    }

    private static void ApplyDebuffToTarget(ServerEntity attacker, ServerEntity target) {
        BuffFactory.ApplyBuff(
            BuffType.FrostAttackSlow1,
            target,
            attacker
        );
    }
}