public class TCorruption1 : Trait {
    public override TraitType Type => TraitType.Corruption1;

    public TCorruption1(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }

        tower.Attack.OnAttackLandedPost += ApplyDebuffToTarget;
    }

    private static void ApplyDebuffToTarget(
        ServerEntity attacker,
        ServerEntity target
    ) {
        BuffFactory.ApplyBuff(
            BuffType.Corrupted1,
            target,
            attacker
        );
    }
}