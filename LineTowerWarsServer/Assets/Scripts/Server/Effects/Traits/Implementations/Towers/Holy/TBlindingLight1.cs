public class TBlindingLight1 : Trait {
    public override TraitType Type => TraitType.BlindingLight1;

    public TBlindingLight1(ServerEntity entity) : base(entity) {
        if (!(entity is ServerTower tower)) {
            return;
        }
        
        tower.Attack.OnAttackLandedPost += ApplyDebuffToTarget;
    }

    private static void ApplyDebuffToTarget(ServerEntity attacker, ServerEntity target) {
        BuffFactory.ApplyBuff(
            BuffType.BlindedByTheLight1,
            target,
            attacker
        );
    }

    public override int AdditionalAttackTargets =>
        TraitConstants.BlindingLight1AdditionalTargets;
}