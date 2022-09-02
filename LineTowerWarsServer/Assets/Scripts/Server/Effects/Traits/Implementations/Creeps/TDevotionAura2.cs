public class TDevotionAura2 : Trait {
    public override TraitType Type => TraitType.DevotionAura2;

    public TDevotionAura2(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_DevotionAura2.Create(entity);
    }
}