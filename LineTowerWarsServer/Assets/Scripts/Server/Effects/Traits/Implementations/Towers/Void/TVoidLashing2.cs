public class TVoidLashing2 : Trait {
    public override TraitType Type => TraitType.VoidLashing2;

    public TVoidLashing2(ServerEntity entity) : base(entity) {
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
            BuffType.VoidLashingDebuff2,
            target,
            attacker
        );

        BuffFactory.ApplyBuff(
            BuffType.VoidLashingBuff2,
            attacker,
            target
        );
    }
}