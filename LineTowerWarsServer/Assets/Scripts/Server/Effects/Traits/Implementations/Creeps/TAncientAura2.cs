public class TAncientAura2 : Trait {
    public override TraitType Type => TraitType.AncientAura2;

    public TAncientAura2(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_AncientAura2.Create(entity);
    }
}