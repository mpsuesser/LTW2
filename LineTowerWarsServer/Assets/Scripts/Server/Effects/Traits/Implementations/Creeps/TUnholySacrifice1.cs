public class TUnholySacrifice1 : Trait {
    public override TraitType Type => TraitType.UnholySacrifice1;

    public TUnholySacrifice1(ServerEntity entity) : base(entity) { }

    protected override void OnHolderDeath(ServerEntity entity) {
        TraitUtils.TriggerUnholySacrificeHeal(
            entity,
            TraitConstants.UnholySacrifice1HealAmount,
            TraitConstants.UnholySacrifice1Range
        );
    }
}