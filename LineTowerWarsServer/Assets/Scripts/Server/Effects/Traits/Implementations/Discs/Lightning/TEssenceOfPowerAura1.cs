public class TEssenceOfPowerAura1 : Trait {
    public override TraitType Type => TraitType.EssenceOfPowerAura1;

    public TEssenceOfPowerAura1(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EssenceOfPowerAura1.Create(entity);
    }
}