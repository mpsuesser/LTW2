public class TEssenceOfTheSeaAura1 : Trait {
    public override TraitType Type => TraitType.EssenceOfTheSeaAura1;

    public TEssenceOfTheSeaAura1(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EssenceOfTheSeaAura1.Create(entity);
    }
}