public class TEssenceOfFrostAura2 : Trait {
    public override TraitType Type => TraitType.EssenceOfFrostAura2;

    public TEssenceOfFrostAura2(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_EssenceOfFrostAura2.Create(entity);
    }
}