public class TEssenceOfPowerAura2 : Trait {
    public override TraitType Type => TraitType.EssenceOfPowerAura2;

    public TEssenceOfPowerAura2(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EssenceOfPowerAura2.Create(entity);
    }
}