public class TEnduranceAura4 : Trait {
    public override TraitType Type => TraitType.EnduranceAura4;

    public TEnduranceAura4(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EnduranceAura4.Create(entity);
    }
}