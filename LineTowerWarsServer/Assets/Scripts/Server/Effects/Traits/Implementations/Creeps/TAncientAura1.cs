public class TAncientAura1 : Trait {
    public override TraitType Type => TraitType.AncientAura1;

    public TAncientAura1(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_AncientAura1.Create(entity);
    }
}