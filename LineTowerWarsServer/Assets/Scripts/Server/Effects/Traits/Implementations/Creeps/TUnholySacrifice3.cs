public class TUnholySacrifice3 : Trait {
    public override TraitType Type => TraitType.UnholySacrifice3;

    public TUnholySacrifice3(ServerEntity entity) : base(entity) { }

    protected override void OnHolderDeath(ServerEntity entity) {
        TraitUtils.TriggerUnholySacrificeHeal(
            entity,
            TraitConstants.UnholySacrifice3HealAmount,
            TraitConstants.UnholySacrifice3Range
        );
    }
}