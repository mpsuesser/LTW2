public class NavigableEntityAttackSystem : AttackSystem {
    public INavigable Navigable { get; }
    
    public NavigableEntityAttackSystem(
        ServerEntity entity,
        EntityAttackScheme attackScheme,
        bool doesSplashDamage,
        float splashDamageGameRadius = 0f
    ) : base(
        entity,
        attackScheme,
        doesSplashDamage,
        splashDamageGameRadius
    ) {
        if (!(entity is INavigable navigable)) {
            throw new EntityIsNotConfiguredProperlyException(entity);
        }

        Navigable = navigable;
    }
}