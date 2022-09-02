public class TAirAttackOnly : Trait {
    public override TraitType Type => TraitType.AirAttackOnly;

    public TAirAttackOnly(ServerEntity entity) : base(entity) {}

    public override EntityFilter AttackTargetEligibilityFilter =>
        new FlyingCreepEntityFilter();
}