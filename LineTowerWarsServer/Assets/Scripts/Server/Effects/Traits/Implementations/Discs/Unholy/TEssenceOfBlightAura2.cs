public class TEssenceOfBlightAura2 : Trait {
    public override TraitType Type => TraitType.EssenceOfBlightAura2;

    public TEssenceOfBlightAura2(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EssenceOfBlightAura2.Create(entity);
    }
}