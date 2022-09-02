public class TDevotionAura3 : Trait {
    public override TraitType Type => TraitType.DevotionAura3;

    public TDevotionAura3(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_DevotionAura3.Create(entity);
    }
}