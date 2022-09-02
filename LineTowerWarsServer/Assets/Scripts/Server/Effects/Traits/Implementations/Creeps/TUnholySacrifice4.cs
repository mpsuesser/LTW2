public class TUnholySacrifice4 : Trait {
    public override TraitType Type => TraitType.UnholySacrifice4;

    public TUnholySacrifice4(ServerEntity entity) : base(entity) { }

    protected override void OnHolderDeath(ServerEntity entity) {
        TraitUtils.TriggerUnholySacrificeHeal(
            entity,
            TraitConstants.UnholySacrifice4HealAmount,
            TraitConstants.UnholySacrifice4Range
        );
    }
}