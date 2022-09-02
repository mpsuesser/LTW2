public class TCorruption2 : Trait {
    public override TraitType Type => TraitType.Corruption2;

    public TCorruption2(ServerEntity entity) : base(entity) {
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
            BuffType.Corrupted2,
            target,
            attacker
        );
    }
}