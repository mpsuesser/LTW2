public class TEssenceOfNatureAura2 : Trait {
    public override TraitType Type => TraitType.EssenceOfNatureAura2;

    public TEssenceOfNatureAura2(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EssenceOfNatureAura2.Create(entity);
    }
}