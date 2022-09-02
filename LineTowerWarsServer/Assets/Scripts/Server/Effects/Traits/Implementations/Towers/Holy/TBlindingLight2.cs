public class TBlindingLight2 : Trait {
    public override TraitType Type => TraitType.BlindingLight2;

    public TBlindingLight2(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }
        
        tower.Attack.OnAttackLandedPost += ApplyDebuffToTarget;
    }

    private static void ApplyDebuffToTarget(ServerEntity attacker, ServerEntity target) {
        BuffFactory.ApplyBuff(
            BuffType.BlindedByTheLight2,
            target,
            attacker
        );
    }

    public override int AdditionalAttackTargets =>
        TraitConstants.BlindingLight2AdditionalTargets;
}