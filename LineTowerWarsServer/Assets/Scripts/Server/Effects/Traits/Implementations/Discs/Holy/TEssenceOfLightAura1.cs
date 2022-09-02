public class TEssenceOfLightAura1 : Trait {
    public override TraitType Type => TraitType.EssenceOfLightAura1;

    public TEssenceOfLightAura1(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EssenceOfLightAura1.Create(entity);
    }
}