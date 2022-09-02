public class TDevotionAura4 : Trait {
    public override TraitType Type => TraitType.DevotionAura4;

    public TDevotionAura4(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        ProximityBuffApplicator_DevotionAura4.Create(entity);
    }
}