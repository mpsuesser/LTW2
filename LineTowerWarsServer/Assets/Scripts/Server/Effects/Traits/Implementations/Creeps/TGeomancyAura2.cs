public class TGeomancyAura2 : Trait {
    public override TraitType Type => TraitType.GeomancyAura2;

    public TGeomancyAura2(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_GeomancyAura2.Create(entity);
    }
}