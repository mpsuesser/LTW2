public class TEssenceOfTheSeaAura2 : Trait {
    public override TraitType Type => TraitType.EssenceOfTheSeaAura2;

    public TEssenceOfTheSeaAura2(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EssenceOfTheSeaAura2.Create(entity);
    }
}