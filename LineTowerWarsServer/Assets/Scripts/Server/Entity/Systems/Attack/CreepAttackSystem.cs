public class CreepAttackSystem : NavigableEntityAttackSystem {
    public CreepAttackSystem(
        ServerEnemy creep
    ) : base(
        creep,
        new EntityAttackScheme(creep),
        EnemyConstants.SplashDamageGameRadius.ContainsKey(creep.Type),
        EnemyConstants.SplashDamageGameRadius.TryGetValue(creep.Type, out float radius)
            ? radius
            : 0f
    ) { }
}