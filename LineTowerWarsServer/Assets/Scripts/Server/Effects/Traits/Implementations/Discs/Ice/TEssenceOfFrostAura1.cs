public class TEssenceOfFrostAura1 : Trait {
    public override TraitType Type => TraitType.EssenceOfFrostAura1;

    public TEssenceOfFrostAura1(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EssenceOfFrostAura1.Create(entity);
    }
}