public class TAttacker : Trait {
    public override TraitType Type => TraitType.Attacker;

    public TAttacker(ServerEntity entity) : base(entity) {
        if (!(entity is IAttacker)) {
            throw new EntityIsNotConfiguredProperlyException(entity);
        }
    }

    public override EntityFilter AttackTargetEligibilityFilter => new TowerEntityFilter();
}