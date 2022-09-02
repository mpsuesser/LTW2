public class ClientWrecker : ClientTower
{
    public override TowerType Type => TowerType.Wrecker;
    
    protected override void Awake() {
        PointInAttackAnimationOfAttack = 0.52f;

        base.Awake();
    }
}