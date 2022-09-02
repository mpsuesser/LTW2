public class TEnduranceAura1 : Trait {
    public override TraitType Type => TraitType.EnduranceAura1;

    public TEnduranceAura1(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EnduranceAura1.Create(entity);
    }
}