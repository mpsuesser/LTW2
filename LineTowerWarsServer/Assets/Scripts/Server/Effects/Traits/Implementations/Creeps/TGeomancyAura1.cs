public class TGeomancyAura1 : Trait {
    public override TraitType Type => TraitType.GeomancyAura1;

    public TGeomancyAura1(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_GeomancyAura1.Create(entity);
    }
}