public class TEssenceOfNatureAura1 : Trait {
    public override TraitType Type => TraitType.EssenceOfNatureAura1;

    public TEssenceOfNatureAura1(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EssenceOfNatureAura1.Create(entity);
    }
}