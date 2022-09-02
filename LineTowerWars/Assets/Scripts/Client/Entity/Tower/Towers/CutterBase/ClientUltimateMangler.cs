public class ClientUltimateMangler : ClientTower
{
    public override TowerType Type => TowerType.UltimateMangler;
    
    protected override void Awake() {
        PointInAttackAnimationOfAttack = 0.52f;

        base.Awake();
    }
}