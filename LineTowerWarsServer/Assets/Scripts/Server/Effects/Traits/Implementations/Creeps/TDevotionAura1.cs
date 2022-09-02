public class TDevotionAura1 : Trait {
    public override TraitType Type => TraitType.DevotionAura1;

    public TDevotionAura1(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_DevotionAura1.Create(entity);
    }
}