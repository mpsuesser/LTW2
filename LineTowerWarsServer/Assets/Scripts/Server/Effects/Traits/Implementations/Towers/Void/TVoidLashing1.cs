public class TVoidLashing1 : Trait {
    public override TraitType Type => TraitType.VoidLashing1;

    public TVoidLashing1(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPost += ApplyBuffs;
    }

    private static void ApplyBuffs(
        ServerEntity attacker,
        ServerEntity target
    ) {
        BuffFactory.ApplyBuff(
            BuffType.VoidLashingDebuff1,
            target,
            attacker
        );

        BuffFactory.ApplyBuff(
            BuffType.VoidLashingBuff1,
            attacker,
            target
        );
    }
}