public class TGroundAttackOnly : Trait {
    public override TraitType Type => TraitType.GroundAttackOnly;

    public TGroundAttackOnly(ServerEntity entity) : base(entity) {}

    public override EntityFilter AttackTargetEligibilityFilter =>
        new GroundCreepEntityFilter();
}