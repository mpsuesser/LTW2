public class TowerAttackSystem : StationaryThreatenableEntityAttackSystem {
    public TowerAttackSystem(
        ServerTower tower
    ) : base(
        tower,
        new EntityAttackScheme(tower),
        TowerConstants.SplashDamageRadius.ContainsKey(tower.Type),
        (float) (TowerConstants.SplashDamageRadius.TryGetValue(tower.Type, out double radius)
            ? radius
            : 0)
    ) { }
}