public class StationaryThreatenableEntityAttackSystem : AttackSystem {
    public IThreatenable Threatenable { get; }
    
    public StationaryThreatenableEntityAttackSystem(
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
        if (!(entity is IThreatenable threatenable)) {
            throw new EntityIsNotConfiguredProperlyException(entity);
        }

        Threatenable = threatenable;
    }
}