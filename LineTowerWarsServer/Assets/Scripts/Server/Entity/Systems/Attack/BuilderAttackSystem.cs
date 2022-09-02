public class BuilderAttackSystem : NavigableEntityAttackSystem {
    public BuilderAttackSystem(
        ServerBuilder builder
    ) : base(
        builder,
        new EntityAttackScheme(builder),
        false
    ) { }
}