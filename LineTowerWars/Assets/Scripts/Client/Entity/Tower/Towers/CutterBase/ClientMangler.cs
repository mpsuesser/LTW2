public class ClientMangler : ClientTower
{
    public override TowerType Type => TowerType.Mangler;
    
    protected override void Awake() {
        PointInAttackAnimationOfAttack = 0.52f;

        base.Awake();
    }
}