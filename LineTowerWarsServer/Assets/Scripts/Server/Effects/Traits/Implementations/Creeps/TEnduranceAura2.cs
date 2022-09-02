public class TEnduranceAura2 : Trait {
    public override TraitType Type => TraitType.EnduranceAura2;

    public TEnduranceAura2(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EnduranceAura2.Create(entity);
    }
}