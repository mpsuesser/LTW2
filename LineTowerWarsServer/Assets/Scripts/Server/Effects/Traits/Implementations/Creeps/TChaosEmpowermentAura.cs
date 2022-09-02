public class TChaosEmpowermentAura : Trait {
    public override TraitType Type => TraitType.ChaosEmpowermentAura;

    public TChaosEmpowermentAura(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_ChaosEmpowermentAura.Create(entity);
    }
}