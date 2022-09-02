public class TEssenceOfDarknessAura2 : Trait {
    public override TraitType Type => TraitType.EssenceOfDarknessAura2;

    public TEssenceOfDarknessAura2(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EssenceOfDarknessAura2.Create(entity);
    }
}