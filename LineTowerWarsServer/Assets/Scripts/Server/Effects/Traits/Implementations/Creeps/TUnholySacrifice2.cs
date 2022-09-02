public class TUnholySacrifice2 : Trait {
    public override TraitType Type => TraitType.UnholySacrifice2;

    public TUnholySacrifice2(ServerEntity entity) : base(entity) { }

    protected override void OnHolderDeath(ServerEntity entity) {
        TraitUtils.TriggerUnholySacrificeHeal(
            entity,
            TraitConstants.UnholySacrifice2HealAmount,
            TraitConstants.UnholySacrifice2Range
        );
    }
}