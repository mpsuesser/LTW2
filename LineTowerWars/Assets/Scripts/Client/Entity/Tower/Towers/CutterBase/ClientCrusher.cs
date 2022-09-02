public class ClientCrusher : ClientTower
{
    public override TowerType Type => TowerType.Crusher;

    protected override void Awake() {
        PointInAttackAnimationOfAttack = 0.52f;

        base.Awake();
    }
}