public class TEssenceOfBlightAura1 : Trait {
    public override TraitType Type => TraitType.EssenceOfBlightAura1;

    public TEssenceOfBlightAura1(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EssenceOfBlightAura1.Create(entity);
    }
}