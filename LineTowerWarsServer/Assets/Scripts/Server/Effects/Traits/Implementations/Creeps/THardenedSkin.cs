public class THardenedSkin : Trait {
    public override TraitType Type => TraitType.HardenedSkin;

    public THardenedSkin(ServerEntity entity) : base(entity) { }

    protected override void OnHolderSpawned(ServerEntity entity) {
        BuffFactory.ApplyBuff(
            BuffType.HardenedSkin, 
            entity, 
            entity
        );
    }
}