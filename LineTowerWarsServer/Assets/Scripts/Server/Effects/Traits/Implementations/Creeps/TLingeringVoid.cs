public class TLingeringVoid : Trait {
    public override TraitType Type => TraitType.LingeringVoid;

    public TLingeringVoid(ServerEntity entity) : base(entity) {
        if (!(entity is IAttacker attacker)) {
            return;
        }

        attacker.Attack.OnAttackLandedPost += ApplyDebuffToTarget;
    }

    private void ApplyDebuffToTarget(
        ServerEntity attacker,
        ServerEntity target
    ) {
        BuffFactory.ApplyBuff(
            BuffType.LingeringVoid,
            target,
            attacker
        );
    }
}