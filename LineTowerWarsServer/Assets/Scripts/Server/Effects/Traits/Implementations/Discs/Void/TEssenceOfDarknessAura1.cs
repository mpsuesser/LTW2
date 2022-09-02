public class TEssenceOfDarknessAura1 : Trait {
    public override TraitType Type => TraitType.EssenceOfDarknessAura1;

    public TEssenceOfDarknessAura1(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EssenceOfDarknessAura1.Create(entity);
    }
}