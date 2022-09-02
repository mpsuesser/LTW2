public class TEssenceOfLightAura2 : Trait {
    public override TraitType Type => TraitType.EssenceOfLightAura2;

    public TEssenceOfLightAura2(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EssenceOfLightAura2.Create(entity);
    }
}