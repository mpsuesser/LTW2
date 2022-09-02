public class TTitanDefenseMechanism : Trait {
    public override TraitType Type => TraitType.TitanDefenseMechanism;

    public TTitanDefenseMechanism(ServerEntity entity) : base(entity) {
        ProximityBuffApplicator_TitanicallyBlindAura.Create(entity);
    }

    public override int AdditionalAttackTargets =>
        TraitConstants.TitanDefenseMechanismAdditionalTargets;
}