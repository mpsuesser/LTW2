public class TEnduranceAura3 : Trait {
    public override TraitType Type => TraitType.EnduranceAura3;

    public TEnduranceAura3(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EnduranceAura3.Create(entity);
    }
}